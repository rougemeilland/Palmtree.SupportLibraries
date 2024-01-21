using System.Runtime.CompilerServices;

namespace Palmtree.IO.Compression.Stream
{
    public class ZipDeflate64CompressionCoderOption
        : ZipCompressionCoderOption
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ICoderOption CreateDecoderOption(ZipCompressionDecoderParameter parameter)
            => new ZipDeflate64CompressionCoderOption
            {
                Level = parameter switch
                {
                    ZipCompressionDecoderParameter.Value1 => ZipCompressionLevel.Maximum,
                    ZipCompressionDecoderParameter.Value2 => ZipCompressionLevel.Fast,
                    ZipCompressionDecoderParameter.Value3 => ZipCompressionLevel.SuperFast,
                    _ => ZipCompressionLevel.Normal,
                }
            };
    }
}
