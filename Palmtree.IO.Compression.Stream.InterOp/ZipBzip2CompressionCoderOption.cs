namespace Palmtree.IO.Compression.Stream
{
    public class ZipBzip2CompressionCoderOption
        : ZipCompressionCoderOption
    {
        public static ICoderOption CreateDecoderOption()
            => new ZipBzip2CompressionCoderOption { Level = ZipCompressionLevel.Normal };
    }
}
