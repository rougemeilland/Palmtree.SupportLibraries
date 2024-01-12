namespace Palmtree.IO.Compression.Stream.Plugin
{
    public class DeflateCoderPlugin
        : ICompressionCoderPlugin
    {
        internal DeflateCoderPlugin()
        {
        }

        internal const CompressionMethodId COMPRESSION_METHOD_ID = CompressionMethodId.Deflate;

        public static void EnablePlugin()
        {
            CompressionCoderPlugin.Register(new DeflateDecoderPlugin());
            CompressionCoderPlugin.Register(new DeflateEncoderPlugin());
        }
    }
}
