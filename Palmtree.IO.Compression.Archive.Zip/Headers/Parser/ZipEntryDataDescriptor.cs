﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.Compression.Archive.Zip.Headers.Parser
{
    internal class ZipEntryDataDescriptor
    {
        public const Int32 MinimumHeaderSize = 12;
        public const Int32 MaximumHeaderSize = 16;
        public const Int32 MinimumHeaderSizeForZip64 = 20;
        public const Int32 MaximumHeaderSizeForZip64 = 24;

        private static readonly UInt32 _dataDescriptorSignature;

        static ZipEntryDataDescriptor()
        {
            _dataDescriptorSignature = Signature.MakeUInt32LESignature(0x50, 0x4b, 0x07, 0x08);
        }

        private ZipEntryDataDescriptor(ZipStreamPosition dataDescriptorPosition, UInt32 crc, UInt64 packedSize, UInt64 size, UInt64 headerSize)
        {
            DataDescriptorPosition = dataDescriptorPosition;
            Crc = crc;
            PackedSize = packedSize;
            Size = size;
            HeaderSize = headerSize;
        }

        public ZipStreamPosition DataDescriptorPosition { get; }
        public UInt32 Crc { get; }
        public UInt64 PackedSize { get; }
        public UInt64 Size { get; }
        public UInt64 HeaderSize { get; }

        public static ZipEntryDataDescriptor Parse(
            IZipInputStream zipInputStrem,
            ZipStreamPosition dataPosition,
            UInt64 packedSizeValueInCentralDirectoryHeader,
            UInt64 sizeValueInCentralDirectoryHeader,
            UInt32 crcValueInCentralDirectoryHeader)
        {
            // 以下の条件でこのデータディスクリプタは正しく読まれない。ZIP の仕様がこんなので大丈夫？
            // 1) データディスクリプタのシグニチャが存在せず、かつ
            // 2) CRC == 0x08074b50 となるデータが書き込まれている場合。

            try
            {
                zipInputStrem.Seek(dataPosition + packedSizeValueInCentralDirectoryHeader);
            }
            catch (ArgumentException ex)
            {
                throw new BadZipFileFormatException($"Unable to read data descriptor on ZIP archive.: {nameof(dataPosition)}=\"{dataPosition}\", {nameof(packedSizeValueInCentralDirectoryHeader)}=0x{packedSizeValueInCentralDirectoryHeader:x16}", ex);
            }

            // APPNOTE (https://pkware.cachefly.net/webdocs/casestudies/APPNOTE.TXT) 4.3.9.2 より
            // When compressing files, compressed and uncompressed sizes  SHOULD be stored in ZIP64 format (as 8 byte values) when a file's size exceeds 0xFFFFFFFF. 
            // ファイルを圧縮するとき、ファイルのサイズが 0xFFFFFFFF を超える場合、圧縮サイズと非圧縮サイズを ZIP64 形式 (8 バイト値として) で保存する必要があります (SHOULD)。
            var requiredZip64 = packedSizeValueInCentralDirectoryHeader > UInt32.MaxValue || sizeValueInCentralDirectoryHeader > UInt32.MaxValue;

            // ZIP ストリームに対し、最低読み込みバイト数とともに、これからボリュームをロックした状態でのヘッダの読み込みを行うことを宣言する。
            // これが必要な理由は、もしこの時点でアクセス位置がボリュームの終端にある場合は、次のボリュームへの移動を促すため。
            // もし、現在のボリュームの残りバイト数がヘッダの最小サイズ未満である場合は、ヘッダがボリューム間で分割されていると判断し、ZIP アーカイブの破損とみなす。
            // ※ ZIP の仕様上、すべてのヘッダは ボリューム境界をまたいではならない。
            if (!zipInputStrem.CheckIfCanAtomicRead(MinimumHeaderSize))
                throw new BadZipFileFormatException($"The data descriptor is not in the expected position or is fragmented.: position=\"{zipInputStrem.Position}\"");

            var dataDescriptorPosition = zipInputStrem.Position;

            ZipEntryDataDescriptor? dataDescriptor;

            // ボリュームをロックする。これ以降、ボリュームをまたいだ読み込みが禁止される。
            zipInputStrem.LockVolumeDisk();
            try
            {
                // APPNOTE (https://pkware.cachefly.net/webdocs/casestudies/APPNOTE.TXT) 4.3.9.2 より
                //     However ZIP64 format MAY be used regardless of the size of a file.
                //     (ただし、ファイルのサイズに関係なく、ZIP64 形式を使用することができます。)
                // …どうやって解析しろと… F〇ck!!

                var headerBytes = ReadOnlyMemory<Byte>.Empty;
                if (!requiredZip64)
                {
                    // ZIP64 拡張子仕様が要求されていない場合

                    // 1) シグニチャなし・非 ZIP64 フォーマットでの解析を試みる。
                    headerBytes = ReadBuffer(zipInputStrem, dataDescriptorPosition, headerBytes, MinimumHeaderSize);
                    dataDescriptor = TryParse(headerBytes.Span, dataDescriptorPosition, crcValueInCentralDirectoryHeader, packedSizeValueInCentralDirectoryHeader, sizeValueInCentralDirectoryHeader);
                    if (dataDescriptor is not null)
                        return dataDescriptor;

                    // 2) シグニチャあり・非 ZIP64 フォーマットでの解析を試みる。
                    headerBytes = ReadBuffer(zipInputStrem, dataDescriptorPosition, headerBytes, MaximumHeaderSize);
                    dataDescriptor = TryParseWithSignature(headerBytes.Span, dataDescriptorPosition, crcValueInCentralDirectoryHeader, packedSizeValueInCentralDirectoryHeader, sizeValueInCentralDirectoryHeader);
                    if (dataDescriptor is not null)
                        return dataDescriptor;
                }

                // 3) シグニチャなし・ZIP64 フォーマットでの解析を試みる。
                headerBytes = ReadBuffer(zipInputStrem, dataDescriptorPosition, headerBytes, MinimumHeaderSizeForZip64);
                dataDescriptor = TryParseAsZip64(headerBytes.Span, dataDescriptorPosition, crcValueInCentralDirectoryHeader, packedSizeValueInCentralDirectoryHeader, sizeValueInCentralDirectoryHeader);
                if (dataDescriptor is not null)
                    return dataDescriptor;

                // 4) シグニチャあり・ZIP64 フォーマットでの解析を試みる。
                headerBytes = ReadBuffer(zipInputStrem, dataDescriptorPosition, headerBytes, MaximumHeaderSizeForZip64);
                dataDescriptor = TryParseAsZip64WithSignature(headerBytes.Span, dataDescriptorPosition, crcValueInCentralDirectoryHeader, packedSizeValueInCentralDirectoryHeader, sizeValueInCentralDirectoryHeader);
                if (dataDescriptor is not null)
                    return dataDescriptor;

                // すべての試みが失敗した場合

                // 存在するはずのデータディスクリプタが存在しない (または壊れている) と判断し、ZIP アーカイブの破損とみなす。
                throw new BadZipFileFormatException($"Data descriptor does not exist.: position=\"{dataDescriptorPosition}\"");
            }
            catch (InvalidOperationException ex)
            {
                // ボリュームがロックされている最中に、ボリュームをまたいだ読み込みが行われた場合

                // ヘッダがボリュームをまたいでいると判断し、ZIP アーカイブの破損とみなす。
                throw new BadZipFileFormatException($"It is possible that the data descriptor is split across multiple disks.: position=\"{dataDescriptorPosition}\"", ex);
            }
            catch (EndOfStreamException ex)
            {
                // ヘッダの読み込み中に ZIP アーカイブの終端に達した場合

                // ZIP アーカイブの破損とみなす。
                throw new BadZipFileFormatException($"Unable to read data descriptor.: position=\"{dataDescriptorPosition}\"", ex);
            }
            finally
            {
                // ボリュームのロックを解除する。
                zipInputStrem.UnlockVolumeDisk();
            }
        }

        public static async Task<ZipEntryDataDescriptor> ParseAsync(
            IZipInputStream zipInputStrem,
            ZipStreamPosition dataPosition,
            UInt64 packedSizeValueInCentralDirectoryHeader,
            UInt64 sizeValueInCentralDirectoryHeader,
            UInt32 crcValueInCentralDirectoryHeader,
            CancellationToken cancellationToken)
        {
            // 以下の条件でこのデータディスクリプタは正しく読まれない。ZIP の仕様がこんなので大丈夫？
            // 1) データディスクリプタのシグニチャが存在せず、かつ
            // 2) CRC == 0x08074b50 となるデータが書き込まれている場合。

            try
            {
                zipInputStrem.Seek(dataPosition + packedSizeValueInCentralDirectoryHeader);
            }
            catch (ArgumentException ex)
            {
                throw new BadZipFileFormatException($"Unable to read data descriptor on ZIP archive.: {nameof(dataPosition)}=\"{dataPosition}\", {nameof(packedSizeValueInCentralDirectoryHeader)}=0x{packedSizeValueInCentralDirectoryHeader:x16}", ex);
            }

            // APPNOTE (https://pkware.cachefly.net/webdocs/casestudies/APPNOTE.TXT) 4.3.9.2 より
            // When compressing files, compressed and uncompressed sizes  SHOULD be stored in ZIP64 format (as 8 byte values) when a file's size exceeds 0xFFFFFFFF. 
            // ファイルを圧縮するとき、ファイルのサイズが 0xFFFFFFFF を超える場合、圧縮サイズと非圧縮サイズを ZIP64 形式 (8 バイト値として) で保存する必要があります (SHOULD)。
            var requiredZip64 = packedSizeValueInCentralDirectoryHeader > UInt32.MaxValue || sizeValueInCentralDirectoryHeader > UInt32.MaxValue;

            // ZIP ストリームに対し、最低読み込みバイト数とともに、これからボリュームをロックした状態でのヘッダの読み込みを行うことを宣言する。
            // これが必要な理由は、もしこの時点でアクセス位置がボリュームの終端にある場合は、次のボリュームへの移動を促すため。
            // もし、現在のボリュームの残りバイト数がヘッダの最小サイズ未満である場合は、ヘッダがボリューム間で分割されていると判断し、ZIP アーカイブの破損とみなす。
            // ※ ZIP の仕様上、すべてのヘッダは ボリューム境界をまたいではならない。
            if (!zipInputStrem.CheckIfCanAtomicRead(MinimumHeaderSize))
                throw new BadZipFileFormatException($"The data descriptor is not in the expected position or is fragmented.: position=\"{zipInputStrem.Position}\"");

            var dataDescriptorPosition = zipInputStrem.Position;

            ZipEntryDataDescriptor? dataDescriptor;

            // ボリュームをロックする。これ以降、ボリュームをまたいだ読み込みが禁止される。
            zipInputStrem.LockVolumeDisk();
            try
            {
                // APPNOTE (https://pkware.cachefly.net/webdocs/casestudies/APPNOTE.TXT) 4.3.9.2 より
                // However ZIP64 format MAY be used regardless of the size of a file.
                // ただし、ファイルのサイズに関係なく、ZIP64 形式を使用することができます。
                // …どうやって解析しろと… F〇ck!!

                var headerBytes = ReadOnlyMemory<Byte>.Empty;
                if (!requiredZip64)
                {
                    // ZIP64 拡張子仕様が要求されていない場合

                    // 1) シグニチャなし・非 ZIP64 フォーマットでの解析を試みる。
                    headerBytes = await ReadBufferAsync(zipInputStrem, dataDescriptorPosition, headerBytes, MinimumHeaderSize, cancellationToken).ConfigureAwait(false);
                    dataDescriptor = TryParse(headerBytes.Span, dataDescriptorPosition, crcValueInCentralDirectoryHeader, packedSizeValueInCentralDirectoryHeader, sizeValueInCentralDirectoryHeader);
                    if (dataDescriptor is not null)
                        return dataDescriptor;

                    // 2) シグニチャあり・非 ZIP64 フォーマットでの解析を試みる。
                    headerBytes = await ReadBufferAsync(zipInputStrem, dataDescriptorPosition, headerBytes, MaximumHeaderSize, cancellationToken).ConfigureAwait(false);
                    dataDescriptor = TryParseWithSignature(headerBytes.Span, dataDescriptorPosition, crcValueInCentralDirectoryHeader, packedSizeValueInCentralDirectoryHeader, sizeValueInCentralDirectoryHeader);
                    if (dataDescriptor is not null)
                        return dataDescriptor;
                }

                // 3) シグニチャなし・ZIP64 フォーマットでの解析を試みる。
                headerBytes = await ReadBufferAsync(zipInputStrem, dataDescriptorPosition, headerBytes, MinimumHeaderSizeForZip64, cancellationToken).ConfigureAwait(false);
                dataDescriptor = TryParseAsZip64(headerBytes.Span, dataDescriptorPosition, crcValueInCentralDirectoryHeader, packedSizeValueInCentralDirectoryHeader, sizeValueInCentralDirectoryHeader);
                if (dataDescriptor is not null)
                    return dataDescriptor;

                // 4) シグニチャあり・ZIP64 フォーマットでの解析を試みる。
                headerBytes = await ReadBufferAsync(zipInputStrem, dataDescriptorPosition, headerBytes, MaximumHeaderSizeForZip64, cancellationToken).ConfigureAwait(false);
                dataDescriptor = TryParseAsZip64WithSignature(headerBytes.Span, dataDescriptorPosition, crcValueInCentralDirectoryHeader, packedSizeValueInCentralDirectoryHeader, sizeValueInCentralDirectoryHeader);
                if (dataDescriptor is not null)
                    return dataDescriptor;

                // すべての試みが失敗した場合

                // 存在するはずのデータディスクリプタが存在しない (または壊れている) と判断し、ZIP アーカイブの破損とみなす。
                throw new BadZipFileFormatException($"Data descriptor does not exist.: position=\"{dataDescriptorPosition}\"");
            }
            catch (InvalidOperationException ex)
            {
                // ボリュームがロックされている最中に、ボリュームをまたいだ読み込みが行われた場合

                // ヘッダがボリュームをまたいでいると判断し、ZIP アーカイブの破損とみなす。
                throw new BadZipFileFormatException($"It is possible that the data descriptor is split across multiple disks.: position=\"{dataDescriptorPosition}\"", ex);
            }
            catch (EndOfStreamException ex)
            {
                // ヘッダの読み込み中に ZIP アーカイブの終端に達した場合

                // ZIP アーカイブの破損とみなす。
                throw new BadZipFileFormatException($"Unable to read data descriptor.: position=\"{dataDescriptorPosition}\"", ex);
            }
            finally
            {
                // ボリュームのロックを解除する。
                zipInputStrem.UnlockVolumeDisk();
            }
        }

        private static ReadOnlyMemory<Byte> ReadBuffer(ISequentialInputByteStream inputStream, ZipStreamPosition dataDescriptorPosition, ReadOnlyMemory<Byte> currentBuffer, Int32 size)
        {
            if (currentBuffer.Length >= size)
                return currentBuffer;
            var newBuffer = new Byte[size];
            currentBuffer.CopyTo(newBuffer.AsMemory(0, currentBuffer.Length));
            var lengthToRead = newBuffer.Length - currentBuffer.Length;
            var length = inputStream.ReadBytes(newBuffer.AsMemory(currentBuffer.Length, lengthToRead));
            if (length != newBuffer.Length - currentBuffer.Length)
                throw new BadZipFileFormatException($"Data descriptor does not exist.: position=\"{dataDescriptorPosition}\"");
            return newBuffer;
        }

        private static async Task<ReadOnlyMemory<Byte>> ReadBufferAsync(ISequentialInputByteStream inputStream, ZipStreamPosition dataDescriptorPosition, ReadOnlyMemory<Byte> currentBuffer, Int32 size, CancellationToken cancellationToken = default)
        {
            if (currentBuffer.Length >= size)
                return currentBuffer;
            var newBuffer = new Byte[size];
            currentBuffer.CopyTo(newBuffer.AsMemory(0, currentBuffer.Length));
            var lengthToRead = newBuffer.Length - currentBuffer.Length;
            var length = await inputStream.ReadBytesAsync(newBuffer.AsMemory(currentBuffer.Length, lengthToRead), cancellationToken).ConfigureAwait(false);
            if (length != newBuffer.Length - currentBuffer.Length)
                throw new BadZipFileFormatException($"Data descriptor does not exist.: position=\"{dataDescriptorPosition}\"");
            return newBuffer;
        }

        private static ZipEntryDataDescriptor? TryParse(ReadOnlySpan<Byte> headerBytes, ZipStreamPosition dataDescriptorPosition, UInt32 crc, UInt64 packedSize, UInt64 size)
            => headerBytes[..4].ToUInt32LE() == crc
                && headerBytes.Slice(4, 4).ToUInt32LE() == packedSize
                && headerBytes.Slice(8, 4).ToUInt32LE() == size
                ? new ZipEntryDataDescriptor(dataDescriptorPosition, crc, packedSize, size, MinimumHeaderSize)
                : null;

        private static ZipEntryDataDescriptor? TryParseWithSignature(ReadOnlySpan<Byte> headerBytes, ZipStreamPosition dataDescriptorPosition, UInt32 crc, UInt64 packedSize, UInt64 size)
            => headerBytes[..4].ToUInt32LE() == _dataDescriptorSignature
                && headerBytes.Slice(4, 4).ToUInt32LE() == crc
                && headerBytes.Slice(8, 4).ToUInt32LE() == packedSize
                && headerBytes.Slice(12, 4).ToUInt32LE() == size
                ? new ZipEntryDataDescriptor(dataDescriptorPosition, crc, packedSize, size, MaximumHeaderSize)
                : null;

        private static ZipEntryDataDescriptor? TryParseAsZip64(ReadOnlySpan<Byte> headerBytes, ZipStreamPosition dataDescriptorPosition, UInt32 crc, UInt64 packedSize, UInt64 size)
            => headerBytes[..4].ToUInt32LE() == crc
                && headerBytes.Slice(4, 8).ToUInt64LE() == packedSize
                && headerBytes.Slice(12, 8).ToUInt64LE() == size
                ? new ZipEntryDataDescriptor(dataDescriptorPosition, crc, packedSize, size, MinimumHeaderSizeForZip64)
                : null;

        private static ZipEntryDataDescriptor? TryParseAsZip64WithSignature(ReadOnlySpan<Byte> headerBytes, ZipStreamPosition dataDescriptorPosition, UInt32 crc, UInt64 packedSize, UInt64 size)
            => headerBytes[..4].ToUInt32LE() == _dataDescriptorSignature
                && headerBytes.Slice(4, 4).ToUInt32LE() == crc
                && headerBytes.Slice(8, 8).ToUInt64LE() == packedSize
                && headerBytes.Slice(16, 8).ToUInt64LE() == size
                ? new ZipEntryDataDescriptor(dataDescriptorPosition, crc, packedSize, size, MaximumHeaderSizeForZip64)
                : null;
    }
}
