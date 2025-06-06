using System;
using System.IO.Compression;
using System.Runtime.CompilerServices;

namespace Palmtree.IO.Compression.Stream.Plugin
{
    internal sealed class DeflateEncoderPlugin
        : ICompressionCoder, ICompressionHierarchicalEncoder
    {
        private sealed class Encoder
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

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Encoder Create(
                ISequentialOutputByteStream baseStream,
                IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress,
                Boolean leaveOpen,
                CompressionLevel level)
                => new(
                    baseStream,
                    progress,
                    leaveOpen,
                    stream => new DeflateStream(stream.AsDotNetStream(), level).AsOutputByteStream());
        }

        CompressionMethodId ICompressionCoder.CompressionMethodId => DeflateCoderPlugin.COMPRESSION_METHOD_ID;

        ISequentialOutputByteStream IHierarchicalEncoder.CreateEncoderStream(
            ISequentialOutputByteStream baseStream,
            ICoderOption option,
            IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress,
            Boolean leaveOpen)
        {
            ArgumentNullException.ThrowIfNull(baseStream);
            ArgumentNullException.ThrowIfNull(option);
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
