namespace Palmtree.IO.Compression.Stream
{
    public class ZipStoredCompressionCoderOption
        : ICoderOption
    {
        public static ICoderOption CreateDecoderOption()
            => new ZipStoredCompressionCoderOption
            {
            };
    }
}
