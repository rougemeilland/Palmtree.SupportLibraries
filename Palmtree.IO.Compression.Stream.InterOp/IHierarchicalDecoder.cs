using System;

namespace Palmtree.IO.Compression.Stream
{
    public interface IHierarchicalDecoder
    {
        ISequentialInputByteStream GetDecodingStream(
            ISequentialInputByteStream baseStream,
            ICoderOption option,
            UInt64 unpackedStreamSize,
            UInt64 packedStreamSize,
            IProgress<UInt64>? unpackedCountProgress,
            Boolean leaveOpen = false);
    }
}
