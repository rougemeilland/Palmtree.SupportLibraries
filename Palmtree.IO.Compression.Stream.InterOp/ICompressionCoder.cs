using System;

namespace Palmtree.IO.Compression.Stream
{
    public interface ICompressionCoder
    {
        CompressionMethodId CompressionMethodId { get; }
        ICoderOption GetOptionFromGeneralPurposeFlag(Boolean bit1, Boolean bit2);
    }
}
