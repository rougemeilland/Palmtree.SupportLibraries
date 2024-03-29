﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Palmtree.IO.Compression.Archive.Zip.Headers.Parser
{
    internal class ZipFileLastDiskHeader
    {
        private class DiskHeaderEnumeratorParameter
        {
            private readonly IZipInputStream _zipStream;

            public DiskHeaderEnumeratorParameter(IZipInputStream zipStream, ValidationStringency stringency)
            {
                _zipStream = zipStream;
                ValidationStringency = stringency;
            }

            public Boolean AssumedToBeSingleVolume => !_zipStream.IsMultiVolumeZipStream;
            public ZipStreamPosition LastDiskStartPosition => _zipStream.LastDiskStartPosition;
            public UInt64 LastDiskSize => _zipStream.LastDiskSize;
            public UInt64 TotalVolumeSize => _zipStream.Length;
            public ValidationStringency ValidationStringency { get; }

            public Boolean ValidatePosition(UInt32 diskNumber, UInt64 offsetOnTheDisk)
                => _zipStream.GetPosition(diskNumber, offsetOnTheDisk) is not null;

        }

        private ZipFileLastDiskHeader(ZipFileEOCDR eocdr, ZipFileZip64EOCDL? zip64EOCDL)
        {
            EOCDR = eocdr;
            Zip64EOCDL = zip64EOCDL;
        }

        public ZipFileEOCDR EOCDR { get; }
        public ZipFileZip64EOCDL? Zip64EOCDL { get; }

        public static ZipFileLastDiskHeader Parse(IZipInputStream zipInputStream, ValidationStringency stringency)
        {
            var assumedToBeSingleVolume = !zipInputStream.IsMultiVolumeZipStream;
            var lastDiskNumber = zipInputStream.LastDiskStartPosition.DiskNumber;
            var lastDiskSize = zipInputStream.LastDiskSize;
            var zipArchiveSizeExceptLastDisk = zipInputStream.Length - zipInputStream.LastDiskSize;

            // EOCDR (および ZIP64 EOCDL) が存在し得る最初の場所を求める
            var possibleFirstHeaderOffsetOnLastDisk =
                zipInputStream.LastDiskSize >= ZipFileEOCDR.MaximumHeaderSize + ZipFileZip64EOCDL.FixedHeaderSize
                ? zipInputStream.LastDiskSize - (ZipFileEOCDR.MaximumHeaderSize + ZipFileZip64EOCDL.FixedHeaderSize)
                : 0;
            if (zipInputStream.LastDiskSize - possibleFirstHeaderOffsetOnLastDisk < ZipFileEOCDR.MinimumHeaderSize)
                throw new BadZipFileFormatException("The length of the ZIP archive file (or its last file in case of multi-volume) is too short.");
            var possibleFirstHeaderPosition = zipInputStream.LastDiskStartPosition + possibleFirstHeaderOffsetOnLastDisk;

            // ヘッダのありそうな位置に Seek し、それ以降のデータをすべて読み込む。
            zipInputStream.Seek(possibleFirstHeaderPosition);
            var buffer = new Byte[checked((Int32)(zipInputStream.LastDiskSize - possibleFirstHeaderOffsetOnLastDisk))];
            var length = zipInputStream.ReadBytes(buffer);
            Validation.Assert(length == buffer.Length, "length == buffer.Length");

            var foundHeaders =
                EnumerateLastDiskHeaders(
                    zipInputStream,
                    buffer,
                    zipInputStream.LastDiskStartPosition + possibleFirstHeaderOffsetOnLastDisk,
                    new DiskHeaderEnumeratorParameter(zipInputStream, stringency))
                .OrderBy(item => item.mayBeMultiVolume) // シングルボリュームと仮定されていて実はマルチボリュームである疑いがあるものは後回し
                .ThenByDescending(item => item.header.EOCDR.HeaderPosition) // オフセットが大きい (つまりディスクの終端に近い) ものを優先
                .Take(1) // 候補を最大 1 つまで絞り込む
                .ToArray();

            if (foundHeaders.Length <= 0)
                // 該当するヘッダの候補が一つも見つからなかった場合
                throw new BadZipFileFormatException($"EOCDR (and ZIP64 EOCDL) is missing or has incorrect contents.");

            if (foundHeaders[0].mayBeMultiVolume)
                // シングルボリュームと仮定された上で実はマルチボリュームであることが判明した場合
                throw new MultiVolumeDetectedException(foundHeaders[0].lastDiskNumber);

            return foundHeaders[0].header;
        }

        private static IEnumerable<(ZipFileLastDiskHeader header, Boolean mayBeMultiVolume, UInt32 lastDiskNumber)> EnumerateLastDiskHeaders(
            IZipInputStream zipInputStream,
            ReadOnlyMemory<Byte> buffer,
            ZipStreamPosition possibleFirstHeaderPosition,
            DiskHeaderEnumeratorParameter parameter)
        {
            foreach (var eocdr in ZipFileEOCDR.EnumerateEOCDR(buffer, possibleFirstHeaderPosition))
            {
                var endOfEOCDR = checked(eocdr.HeaderPosition.OffsetOnTheDisk + ZipFileEOCDR.MinimumHeaderSize + (UInt32)eocdr.CommentBytes.Length);
                if (endOfEOCDR != parameter.LastDiskSize)
                {
                    // コメントを含めた EOCDR の終端が最後のディスクの終端と一致しない場合

                    if (endOfEOCDR > parameter.LastDiskSize)
                    {
                        // コメントを含めた EOCDR の終端が最後のディスクの終端の後にある場合

                        // これは正しい EOCDR ではない。
                        continue;
                    }

                    if (!parameter.ValidationStringency.HasFlag(ValidationStringency.AllowNullPayloadAfterEOCDR))
                    {
                        // EOCDR の後に null bytes を許容しない場合

                        // これは正しい EOCDR ではない。
                        continue;
                    }

                    // EOCDR の後に null bytes を許容する場合

                    // EOCDR の終端から最後のディスクの終端までのデータがすべて 0 であるかどうかを調べる

                    if (CheckIfExistsNonNullByte(buffer, checked((Int32)(endOfEOCDR - possibleFirstHeaderPosition.OffsetOnTheDisk)), checked((Int32)(parameter.LastDiskSize - endOfEOCDR))))
                    {
                        // これは正しい EOCDR ではない。
                        continue;
                    }
                }

                var zip64EOCDL = (ZipFileZip64EOCDL?)null;
                if (eocdr.HeaderPosition.OffsetOnTheDisk >= ZipFileZip64EOCDL.FixedHeaderSize)
                {
                    // EOCDR の前に ZIP64 EOCDL のサイズ以上の余白がある場合

                    var positionWhereZip64EOCDLMayBe = checked(eocdr.HeaderPosition - ZipFileZip64EOCDL.FixedHeaderSize);
                    var eocdlBuffer = buffer.Slice(checked((Int32)(positionWhereZip64EOCDLMayBe - possibleFirstHeaderPosition)), checked((Int32)ZipFileZip64EOCDL.FixedHeaderSize));

                    var tryParseZip64EOCDL = eocdr.IsRequiresZip64;
                    if (!tryParseZip64EOCDL)
                    {
                        // EOCDR が ZIP64 拡張を要求していない場合

                        // セントラルディレクトリヘッダの開始位置を取得する。
                        var firstCentralDirectoryHeaderPosition = zipInputStream.GetPosition(eocdr.DiskWhereCentralDirectoryStarts, eocdr.OffsetOfStartOfCentralDirectory);
                        if (firstCentralDirectoryHeaderPosition is null)
                        {
                            // セントラルディレクトリヘッダの開始位置が無効である場合

                            // 原因は、シングルボリュームと仮定しているにもかかわらず、実はマルチボリュームであった場合など。
                            // この時点で ZIP64 EOCDL の存在の確認はしない。
                            tryParseZip64EOCDL = false;
                        }
                        else
                        {
                            // セントラルディレクトリヘッダの開始位置が有効である場合

                            if (eocdr.HeaderPosition - firstCentralDirectoryHeaderPosition < checked(eocdr.SizeOfCentralDirectory + ZipFileZip64EOCDR.MinimumHeaderSize + ZipFileZip64EOCDL.FixedHeaderSize))
                            {
                                // セントラルディレクトリヘッダの開始位置とEOCDRの開始位置との差が、期待されている最低限の値より小さい場合

                                // この場合、EOCDL は存在し得ない。
                                tryParseZip64EOCDL = false;
                            }
                            else
                            {
                                // セントラルディレクトリヘッダの開始位置とEOCDRの開始位置との差が、期待されている最低限の値と等しいかより大きい場合

                                // この場合、EOCDL は存在するかもしれない。
                                tryParseZip64EOCDL = true;
                            }
                        }
                    }

                    if (tryParseZip64EOCDL)
                    {
                        // ZIP64 EOCDL を解析する
                        zip64EOCDL = ZipFileZip64EOCDL.Parse(eocdlBuffer.Span, positionWhereZip64EOCDLMayBe);
                    }
                }

                var mayBeMultiVolume = false;
                if (zip64EOCDL is null)
                {
                    // ZIP64 EOCDL が存在しない場合

                    if (eocdr.IsRequiresZip64)
                    {
                        // ZIP64 EOCDL が存在しないにもかかわらず、EOCDR は ZIP64 拡張仕様を要求している場合

                        // これは正しい EOCDR ではない。
                        continue;
                    }

                    if (parameter.AssumedToBeSingleVolume)
                    {
                        // シングルボリュームと仮定されている場合

                        if (!eocdr.CheckDiskNumber(parameter.LastDiskStartPosition.DiskNumber))
                        {
                            // シングルボリュームであると仮定されており、かつ EOCDR のディスク番号のフィールドに正しくない値が含まれている場合

                            // EOCDR は正しいかもしれないが、しかしアーカイブはマルチボリュームかもしれない
                            mayBeMultiVolume = true;
                        }
                    }
                    else
                    {
                        // マルチボリュームであると確定している場合

                        if (!eocdr.CheckDiskNumber(parameter.LastDiskStartPosition.DiskNumber))
                        {
                            // マルチボリュームであると確定しており、かつ EOCDR のディスク番号のフィールドに正しくない値が含まれている場合

                            // これは正しい EOCDR ではない。
                            continue;
                        }
                    }

                    if (!mayBeMultiVolume)
                    {
                        if (eocdr.DiskWhereCentralDirectoryStarts > eocdr.NumberOfThisDisk)
                        {
                            // 最初のセントラルディレクトリがあるディスクの番号が最後のディスクの番号より大きい場合

                            // これは正しい EOCDR ではない。
                            continue;
                        }

                        if ((!parameter.ValidationStringency.HasFlag(ValidationStringency.StrictlyCheckNumberOfCentralDirectoryHeadersOnLastDisk) || eocdr.NumberOfCentralDirectoryHeadersOnThisDisk != 1)
                            && eocdr.NumberOfCentralDirectoryHeadersOnThisDisk > eocdr.TotalNumberOfCentralDirectoryHeaders)
                        {
                            // 最後のディスクに存在するセントラルディレクトリの個数が 1 でなく、かつ、合計のセントラルディレクトリの数より大きいならば、これは正しい EOCDR ではない。
                            // ※最後のディスクに存在するセントラルディレクトリの個数と 1 を比較しているのは、このフィールドが常に 1 となる ZIP アーカイバの実装が存在するため。

                            // これは正しい EOCDR ではない。
                            continue;
                        }

                        if (eocdr.SizeOfCentralDirectory > checked(parameter.TotalVolumeSize - parameter.LastDiskSize + eocdr.HeaderPosition.OffsetOnTheDisk))
                        {
                            // セントラルディレクトリの合計サイズが、現在調べているヘッダを除く全ボリュームのサイズより大きい場合

                            // これは正しい EOCDR ではない。
                            continue;
                        }

                        if (!parameter.ValidatePosition(eocdr.DiskWhereCentralDirectoryStarts, eocdr.OffsetOfStartOfCentralDirectory))
                        {
                            // セントラルディレクトリの位置が ZIP アーカイブ上の正しい位置を指していない場合

                            // これは正しい EOCDR ではない。
                            continue;
                        }
                    }

                    yield return (new ZipFileLastDiskHeader(eocdr, zip64EOCDL), mayBeMultiVolume, eocdr.NumberOfThisDisk);
                }
                else
                {
                    // ZIP64 EOCDL が存在する場合

                    if (parameter.AssumedToBeSingleVolume)
                    {
                        // シングルボリュームと仮定されている場合

                        if (!eocdr.CheckDiskNumber(parameter.LastDiskStartPosition.DiskNumber))
                        {
                            // シングルボリュームであると仮定されており、かつ EOCDR のディスク番号のフィールドに正しくない値が含まれている場合

                            // EOCDR は正しいかもしれないが、しかしアーカイブはマルチボリュームかもしれない
                            mayBeMultiVolume = true;
                        }

                        if (!zip64EOCDL.CheckDiskNumber(parameter.LastDiskStartPosition.DiskNumber))
                        {
                            // シングルボリュームであると仮定されており、かつ ZIP64 EOCDL のディスク数/ディスク番号を示すフィールドの値が正しくない場合

                            // ZIP64 EOCDL は正しいかもしれないが、しかしアーカイブはマルチボリュームかもしれない
                            mayBeMultiVolume = true;
                        }
                    }
                    else
                    {
                        // マルチボリュームであると確定している場合

                        if (!eocdr.CheckDiskNumber(parameter.LastDiskStartPosition.DiskNumber))
                        {
                            // マルチボリュームであると確定しており、かつ EOCDR のディスク番号のフィールドに正しくない値が含まれている場合

                            // これは正しい EOCDR ではない。
                            continue;
                        }

                        if (!zip64EOCDL.CheckDiskNumber(parameter.LastDiskStartPosition.DiskNumber))
                        {
                            // マルチボリュームと確定しており、かつ ZIP64 EOCDL のディスク数/ディスク番号を示すフィールドの値が正しくない場合

                            // これは正しい ZIP64 EOCDL ではない。
                            continue;
                        }
                    }

                    if (!mayBeMultiVolume)
                    {
                        if (zip64EOCDL.NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDirectory >= zip64EOCDL.TotalNumberOfDisks)
                        {
                            // 最初のセントラルディレクトリがあるディスクの番号が合計ディスク数以上である場合

                            // これは正しい ZIP64 EOCDL ではない。
                            continue;
                        }

                        if (!parameter.ValidatePosition(zip64EOCDL.NumberOfTheDiskWithTheStartOfTheZip64EndOfCentralDirectory, zip64EOCDL.OffsetOfTheZip64EndOfCentralDirectoryRecord))
                        {
                            // ZIP64 EOCDR の位置が正しいディスク上の場所ではない場合

                            // これは正しい ZIP64 EOCDL ではない。
                            continue;
                        }
                    }

                    yield return (new ZipFileLastDiskHeader(eocdr, zip64EOCDL), mayBeMultiVolume, checked(zip64EOCDL.TotalNumberOfDisks - 1));
                }
            }
        }

        private static Boolean CheckIfExistsNonNullByte(ReadOnlyMemory<Byte> buffer, Int32 pos, Int32 size)
        {
            var endOfRegion = checked(pos + size);
            for (var index = pos; index < endOfRegion; ++index)
            {
                if (buffer.Span[index] != 0x00)
                    return true;
            }

            return false;
        }
    }
}
