namespace Palmtree.IO.Compression.Stream
{
    public abstract class ZipCompressionCoderOption
        : ICoderOption
    {
        public ZipCompressionLevel Level { get; set; }
    }
}
