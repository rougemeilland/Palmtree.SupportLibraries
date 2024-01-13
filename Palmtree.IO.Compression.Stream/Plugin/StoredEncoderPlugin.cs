using System;

namespace Palmtree.IO.Compression.Stream.Plugin
{
    internal class StoredEncoderPlugin
        : ICompressionCoder, ICompressionHierarchicalEncoder
    {
        private class Encoder
            : HierarchicalEncoder
        {
            private Encoder(
                ISequentialOutputByteStream baseStream,
                IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress,
                Boolean leaveOpen,
                Func<ISequentialOutputByteStream, ISequentialOutputByteStream> encoderStreamCreator)
                : base(baseStream, progress, leaveOpen, encoderStreamCreator)
            {
            }

            public static ISequentialOutputByteStream Create(
                ISequentialOutputByteStream baseStream,
                IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress,
                Boolean leaveOpen)
                => new Encoder(baseStream, progress, leaveOpen, stream => stream);
        }

        CompressionMethodId ICompressionCoder.CompressionMethodId => StoredCoderPlugin.COMPRESSION_METHOD_ID;

        ISequentialOutputByteStream IHierarchicalEncoder.CreateEncoderStream(
            ISequentialOutputByteStream baseStream,
            ICoderOption option,
            IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress,
            Boolean leaveOpen)
            => Encoder.Create(baseStream, progress, leaveOpen);
    }
}
