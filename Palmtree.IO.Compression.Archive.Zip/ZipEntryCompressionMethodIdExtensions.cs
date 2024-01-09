namespace Palmtree.IO.Compression.Archive.Zip
{
    internal static class ZipEntryCompressionMethodIdExtensions
    {
        public static ZipEntryCompressionMethod GetCompressionMethod(this ZipEntryCompressionMethodId compressionMethodId)
            => ZipEntryCompressionMethod.GetCompressionMethod(compressionMethodId);
    }
}
