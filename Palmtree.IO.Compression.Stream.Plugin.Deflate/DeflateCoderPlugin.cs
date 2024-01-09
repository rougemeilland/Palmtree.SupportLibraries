namespace Palmtree.IO.Compression.Stream.Plugin.Deflate
{
    public abstract class DeflateCoderPlugin
        : ICompressionCoder, ICompressionCoderPlugin
    {
        internal DeflateCoderPlugin()
        {
        }

        public CompressionMethodId CompressionMethodId => CompressionMethodId.Deflate;

        public static void EnablePlugin()
        {
            CompressionCoderPlugin.Register(new DeflateDecoderPlugin());
            CompressionCoderPlugin.Register(new DeflateEncoderPlugin());
        }
    }
}
