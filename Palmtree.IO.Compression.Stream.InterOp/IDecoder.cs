﻿using System;
using System.Threading;
using System.Threading.Tasks;

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
            IProgress<UInt64>? unpackedCountProgress = null);

        Task<Exception?> DecodeAsync(
            ISequentialInputByteStream sourceStream,
            ISequentialOutputByteStream destinationStream,
            ICoderOption option,
            UInt64 unpackedSize,
            UInt64 packedSize,
            IProgress<UInt64>? unpackedCountProgress = null,
            CancellationToken cancellationToken = default);
    }
}
