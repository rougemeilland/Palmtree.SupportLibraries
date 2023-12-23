using System;

namespace Palmtree.IO.Compression.Stream
{
    public interface IHierarchicalEncoder
    {
        ISequentialOutputByteStream GetEncodingStream(
            ISequentialOutputByteStream baseStream,
            ICoderOption option,
            IProgress<UInt64>? unpackedCountProgress,
            Boolean leaveOpen = false);
    }
}
