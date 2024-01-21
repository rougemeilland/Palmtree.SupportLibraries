using System;
using System.Runtime.CompilerServices;
using Palmtree.IO.Compression.Stream;

namespace Palmtree.IO.Compression.Archive.Zip
{
    static class EnumExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasEncryptionFlag(this ZipEntryGeneralPurposeBitFlag flag)
            => (flag &
                    (ZipEntryGeneralPurposeBitFlag.Encrypted |
                     ZipEntryGeneralPurposeBitFlag.EncryptedCentralDirectory |
                     ZipEntryGeneralPurposeBitFlag.StrongEncrypted))
                != ZipEntryGeneralPurposeBitFlag.None;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ZipCompressionDecoderParameter GetDecoderParameter(this ZipEntryGeneralPurposeBitFlag flag)
        {
            var result = ZipCompressionDecoderParameter.None;
            if (flag.HasFlag(ZipEntryGeneralPurposeBitFlag.CompresssionOption0))
                result |= ZipCompressionDecoderParameter.Bit1;
            if (flag.HasFlag(ZipEntryGeneralPurposeBitFlag.CompresssionOption1))
                result |= ZipCompressionDecoderParameter.Bit2;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ICoderOption GetEncoderOption(this ZipEntryCompressionMethodId compressionMethodId, ZipEntryCompressionLevel compressionLevel)
            => compressionMethodId switch
            {
                ZipEntryCompressionMethodId.Stored => new ZipStoredCompressionCoderOption(),
                ZipEntryCompressionMethodId.Deflate => new ZipDeflateCompressionCoderOption { Level = compressionLevel.ToZipCompressionLevel() },
                ZipEntryCompressionMethodId.Deflate64 => new ZipDeflate64CompressionCoderOption { Level = compressionLevel.ToZipCompressionLevel() },
                ZipEntryCompressionMethodId.BZIP2 => new ZipBzip2CompressionCoderOption { Level = compressionLevel.ToZipCompressionLevel() },
                ZipEntryCompressionMethodId.LZMA => new ZipLzmaCompressionCoderOption { Level = compressionLevel.ToZipCompressionLevel(), UseEndOfStreamMarker = true },
                _ => throw new CompressionMethodNotSupportedException(compressionMethodId),
            };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ZipEntryGeneralPurposeBitFlag GettEncoderOptionFlags(this ZipEntryCompressionMethodId CompressionMethodId, ZipEntryCompressionLevel CompressionLevel)
        {
            switch (CompressionMethodId)
            {
                case ZipEntryCompressionMethodId.Deflate:
                case ZipEntryCompressionMethodId.Deflate64:
                {
                    return CompressionLevel switch
                    {
                        ZipEntryCompressionLevel.Maximum => ZipEntryGeneralPurposeBitFlag.CompresssionOption0,
                        ZipEntryCompressionLevel.Fast => ZipEntryGeneralPurposeBitFlag.CompresssionOption1,
                        ZipEntryCompressionLevel.SuperFast => ZipEntryGeneralPurposeBitFlag.CompresssionOption1 | ZipEntryGeneralPurposeBitFlag.CompresssionOption0,
                        _ => ZipEntryGeneralPurposeBitFlag.None,
                    };
                }
                case ZipEntryCompressionMethodId.LZMA:
                    return ZipEntryGeneralPurposeBitFlag.CompresssionOption0;
                default:
                    return ZipEntryGeneralPurposeBitFlag.None;
            }
        }
    }
}
