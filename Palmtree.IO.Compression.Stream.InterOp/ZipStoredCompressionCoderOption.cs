using System.Runtime.CompilerServices;

namespace Palmtree.IO.Compression.Stream
{
    public class ZipStoredCompressionCoderOption
        : ICoderOption
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ICoderOption CreateDecoderOption()
            => new ZipStoredCompressionCoderOption
            {
            };
    }
}
