using System.Runtime.CompilerServices;

namespace Palmtree.IO.Compression.Stream
{
    public class ZipBzip2CompressionCoderOption
        : ZipCompressionCoderOption
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ICoderOption CreateDecoderOption()
            => new ZipBzip2CompressionCoderOption { Level = ZipCompressionLevel.Normal };
    }
}
