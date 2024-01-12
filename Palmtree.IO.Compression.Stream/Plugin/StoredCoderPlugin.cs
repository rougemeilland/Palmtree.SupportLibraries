namespace Palmtree.IO.Compression.Stream.Plugin
{
    public class StoredCoderPlugin
        : ICompressionCoderPlugin
    {
        internal const CompressionMethodId COMPRESSION_METHOD_ID = CompressionMethodId.Stored;

        public static void EnablePlugin()
        {
            CompressionCoderPlugin.Register(new StoredDecoderPlugin());
            CompressionCoderPlugin.Register(new StoredEncoderPlugin());
        }
    }
}
