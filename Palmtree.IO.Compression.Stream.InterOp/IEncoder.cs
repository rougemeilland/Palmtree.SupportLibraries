using System;

namespace Palmtree.IO.Compression.Stream
{
    public interface IEncoder
    {
        void Encode(
            ISequentialInputByteStream sourceStream,
            ISequentialOutputByteStream destinationStream,
            ICoderOption option,
            IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress);
    }
}
