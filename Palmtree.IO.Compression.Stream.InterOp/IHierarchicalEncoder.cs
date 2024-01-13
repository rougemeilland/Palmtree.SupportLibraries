using System;

namespace Palmtree.IO.Compression.Stream
{
    public interface IHierarchicalEncoder
    {
        ISequentialOutputByteStream CreateEncoderStream(
            ISequentialOutputByteStream baseStream,
            ICoderOption option,
            IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress,
            Boolean leaveOpen = false);
    }
}
