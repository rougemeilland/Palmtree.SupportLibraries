using System.Runtime.CompilerServices;

namespace Palmtree.IO.Compression.Archive.Zip
{
    internal static class ZipEntryCompressionMethodIdExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ZipEntryCompressionMethod GetCompressionMethod(this ZipEntryCompressionMethodId compressionMethodId)
            => ZipEntryCompressionMethod.GetCompressionMethod(compressionMethodId);
    }
}
