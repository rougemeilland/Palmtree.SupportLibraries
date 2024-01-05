namespace Palmtree.IO.Compression.Stream.Plugin.Deflate
{
    public abstract class DeflateCoderPlugin
        : ICompressionCoder, ICompressionCoderPlugin
    {
        internal DeflateCoderPlugin()
        {
        }

        public CompressionMethodId CompressionMethodId => CompressionMethodId.Deflate;

        public ICoderOption DefaultOption
            => CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.Normal);

        public ICoderOption GetOptionFromGeneralPurposeFlag(Boolean bit1, Boolean bit2)
            => bit2
                ? bit1
                    ? CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.SuperFast)
                    : CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.Fast)
                : bit1
                    ? CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.Maximum)
                    : CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.Normal);

        public static void EnablePlugin()
        {
            CompressionCoderPlugin.Register(new DeflateDecoderPlugin());
            CompressionCoderPlugin.Register(new DeflateEncoderPlugin());
        }
    }
}
