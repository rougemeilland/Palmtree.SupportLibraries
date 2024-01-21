using System.Runtime.CompilerServices;
using Palmtree.IO.Compression.Stream;

namespace Palmtree.IO.Compression.Archive.Zip
{
    internal static class ZipEntryCompressionLevelExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ZipCompressionLevel ToZipCompressionLevel(this ZipEntryCompressionLevel level)
            => level switch
            {
                ZipEntryCompressionLevel.Maximum => ZipCompressionLevel.Maximum,
                ZipEntryCompressionLevel.Fast => ZipCompressionLevel.Fast,
                ZipEntryCompressionLevel.SuperFast => ZipCompressionLevel.SuperFast,
                _ => ZipCompressionLevel.Normal,
            };
    }
}
