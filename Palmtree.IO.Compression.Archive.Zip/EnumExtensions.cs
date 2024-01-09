using System;
using Palmtree.IO.Compression.Stream;

namespace Palmtree.IO.Compression.Archive.Zip
{
    static class EnumExtensions
    {
        public static Boolean HasEncryptionFlag(this ZipEntryGeneralPurposeBitFlag flag)
            => (flag &
                    (ZipEntryGeneralPurposeBitFlag.Encrypted |
                     ZipEntryGeneralPurposeBitFlag.EncryptedCentralDirectory |
                     ZipEntryGeneralPurposeBitFlag.StrongEncrypted))
                != ZipEntryGeneralPurposeBitFlag.None;

        public static ZipCompressionDecoderParameter GetDecoderParameter(this ZipEntryGeneralPurposeBitFlag flag)
        {
            var result = ZipCompressionDecoderParameter.None;
            if (flag.HasFlag(ZipEntryGeneralPurposeBitFlag.CompresssionOption0))
                result |= ZipCompressionDecoderParameter.Bit1;
            if (flag.HasFlag(ZipEntryGeneralPurposeBitFlag.CompresssionOption1))
                result |= ZipCompressionDecoderParameter.Bit2;
            return result;
        }
    }
}
