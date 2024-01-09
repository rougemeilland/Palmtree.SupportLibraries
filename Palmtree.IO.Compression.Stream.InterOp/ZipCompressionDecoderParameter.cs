using System;

namespace Palmtree.IO.Compression.Stream
{
    [Flags]
    public enum ZipCompressionDecoderParameter
    {
        None = 0,
        Bit1 = 1 << 1,
        Bit2 = 1 << 2,
        Value0 = None,
        Value1 = Bit1,
        Value2 = Bit2,
        Value3 = Bit1 | Bit2,
    }
}
