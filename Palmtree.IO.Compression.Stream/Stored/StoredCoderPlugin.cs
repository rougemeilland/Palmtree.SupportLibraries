namespace Palmtree.IO.Compression.Stream.Stored
{
    public abstract class StoredCoderPlugin
        : ICompressionCoder, ICompressionCoderPlugin
    {
        internal StoredCoderPlugin()
        {
        }

        public CompressionMethodId CompressionMethodId => CompressionMethodId.Stored;

        public static void EnablePlugin()
        {
            CompressionCoderPlugin.Register(new StoredDecoderPlugin());
            CompressionCoderPlugin.Register(new StoredEncoderPlugin());
        }
    }
}
