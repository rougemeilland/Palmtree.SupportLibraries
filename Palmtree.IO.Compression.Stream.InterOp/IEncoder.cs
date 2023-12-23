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
            IProgress<UInt64>? unpackedCountProgress = null);

        Task<Exception?> EncodeAsync(
            ISequentialInputByteStream sourceStream,
            ISequentialOutputByteStream destinationStream,
            ICoderOption option,
            IProgress<UInt64>? unpackedCountProgress = null,
            CancellationToken cancellationToken = default);
    }
}
