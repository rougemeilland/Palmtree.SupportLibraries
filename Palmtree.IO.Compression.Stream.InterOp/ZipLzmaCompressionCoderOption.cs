using System;
using System.Runtime.CompilerServices;

namespace Palmtree.IO.Compression.Stream
{
    public class ZipLzmaCompressionCoderOption
        : ZipCompressionCoderOption
    {
        public Boolean UseEndOfStreamMarker { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ICoderOption CreateDecoderOption(ZipCompressionDecoderParameter parameter)
            => new ZipLzmaCompressionCoderOption
            {
                Level = ZipCompressionLevel.Normal,
                UseEndOfStreamMarker = parameter.HasFlag(ZipCompressionDecoderParameter.Bit1),
            };
    }
}
