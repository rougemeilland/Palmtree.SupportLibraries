using System;

namespace Palmtree.IO.Compression.Stream.Plugin
{
    internal sealed class StoredDecoderPlugin
        : ICompressionCoder, ICompressionHierarchicalDecoder
    {
        private sealed class Decoder
            : HierarchicalDecoder
        {
            private Decoder(
                ISequentialInputByteStream baseStream,
                UInt64 unpackedStreamSize,
                IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? progress,
                Boolean leaveOpen,
                Func<ISequentialInputByteStream, ISequentialInputByteStream> decoderStreamCreator)
                : base(baseStream, unpackedStreamSize, progress, leaveOpen, decoderStreamCreator)
            {
            }

            public static Decoder Create(
                ISequentialInputByteStream baseStream,
                UInt64 unpackedStreamSize,
                IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? progress,
                Boolean leaveOpen)
                => new(baseStream, unpackedStreamSize, progress, leaveOpen, stream => stream);
        }

        CompressionMethodId ICompressionCoder.CompressionMethodId => StoredCoderPlugin.COMPRESSION_METHOD_ID;

        ISequentialInputByteStream IHierarchicalDecoder.CreateDecoderStream(
            ISequentialInputByteStream baseStream,
            ICoderOption option,
            UInt64 unpackedStreamSize,
            UInt64 packedStreamSize,
            IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? progress,
            Boolean leaveOpen)
            => Decoder.Create(baseStream, unpackedStreamSize, progress, leaveOpen);
    }
}
