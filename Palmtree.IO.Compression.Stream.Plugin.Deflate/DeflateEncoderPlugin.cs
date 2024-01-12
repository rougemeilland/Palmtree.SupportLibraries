using System;
using System.IO.Compression;

namespace Palmtree.IO.Compression.Stream.Plugin
{
    internal class DeflateEncoderPlugin
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
                Boolean leaveOpen,
                CompressionLevel level)
                => new Encoder(
                    baseStream,
                    progress,
                    leaveOpen,
                    stream => new DeflateStream(stream.AsDotNetStream(), level).AsOutputByteStream());
        }

        CompressionMethodId ICompressionCoder.CompressionMethodId => DeflateCoderPlugin.COMPRESSION_METHOD_ID;

        ISequentialOutputByteStream IHierarchicalEncoder.GetEncodingStream(
            ISequentialOutputByteStream baseStream,
            ICoderOption option,
            IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress,
            Boolean leaveOpen)
        {
            if (baseStream is null)
                throw new ArgumentNullException(nameof(baseStream));
            if (option is null)
                throw new ArgumentNullException(nameof(option));
            if (option is not ZipDeflateCompressionCoderOption deflateOption)
                throw new ArgumentException($"Illegal {nameof(option)} data", nameof(option));

            var level = deflateOption.Level switch
            {
                ZipCompressionLevel.Fast or ZipCompressionLevel.SuperFast => CompressionLevel.Fastest,
                ZipCompressionLevel.Maximum => CompressionLevel.SmallestSize,
                _ => CompressionLevel.Optimal,
            };
            return Encoder.Create(baseStream, progress, leaveOpen, level);
        }
    }
}
