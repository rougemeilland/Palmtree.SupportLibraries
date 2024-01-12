using System;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.Compression.Stream
{
    public interface IEncoder
    {
        void Encode(
            ISequentialInputByteStream sourceStream,
            ISequentialOutputByteStream destinationStream,
            ICoderOption option,
            IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress);

        Task<Exception?> EncodeAsync(
            ISequentialInputByteStream sourceStream,
            ISequentialOutputByteStream destinationStream,
            ICoderOption option,
            IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? progress,
            CancellationToken cancellationToken = default);
    }
}
