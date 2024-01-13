using System;

namespace Palmtree.IO.Compression.Stream
{
    public interface IDecoder
    {
        void Decode(
            ISequentialInputByteStream sourceStream,
            ISequentialOutputByteStream destinationStream,
            ICoderOption option,
            UInt64 unpackedSize,
            UInt64 packedSize,
            IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? progress);
    }
}
