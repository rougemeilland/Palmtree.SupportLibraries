using System;

namespace Palmtree.IO.Compression.Stream.Plugin
{
    internal class StoredDecoderPlugin
        : StoredCoderPlugin, ICompressionHierarchicalDecoder
    {
        private class Decoder
            : HierarchicalDecoder
        {
            public Decoder(ISequentialInputByteStream baseStream, UInt64 unpackedStreamSize, IProgress<UInt64>? unpackedCountProgress, Boolean leaveOpen)
                : base(baseStream, unpackedStreamSize, unpackedCountProgress, leaveOpen)
            {
            }
        }

        ISequentialInputByteStream IHierarchicalDecoder.GetDecodingStream(
            ISequentialInputByteStream baseStream,
            ICoderOption option,
            UInt64 unpackedStreamSize,
            UInt64 packedStreamSize,
            IProgress<UInt64>? unpackedCountProgress,
            Boolean leaveOpen)
            => new Decoder(baseStream, unpackedStreamSize, unpackedCountProgress, leaveOpen);
    }
}
