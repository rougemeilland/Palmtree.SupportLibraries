using System;
using System.IO.Compression;

namespace Palmtree.IO.Compression.Stream.Plugin.Deflate
{
    internal class DeflateEncoderPlugin
        : DeflateCoderPlugin, ICompressionHierarchicalEncoder
    {
        private class Encoder
            : HierarchicalEncoder
        {
            public Encoder(ISequentialOutputByteStream baseStream, CompressionLevel level, IProgress<UInt64>? unpackedCountProgress, Boolean leaveOpen)
                : base(GetBaseStream(baseStream, level), unpackedCountProgress, leaveOpen)
            {
            }

            private static ISequentialOutputByteStream GetBaseStream(ISequentialOutputByteStream baseStream, CompressionLevel level)
            {
                if (baseStream is null)
                    throw new ArgumentNullException(nameof(baseStream));

                return new DeflateStream(baseStream.AsDotNetStream(), level).AsOutputByteStream();
            }

            protected override void FlushDestinationStream(ISequentialOutputByteStream destinationStream, Boolean isEndOfData)
            {
                if (isEndOfData)
                    destinationStream.Dispose();
            }
        }

        ISequentialOutputByteStream IHierarchicalEncoder.GetEncodingStream(
            ISequentialOutputByteStream baseStream,
            ICoderOption option,
            IProgress<UInt64>? unpackedCountProgress,
            Boolean leaveOpen)
        {
            if (baseStream is null)
                throw new ArgumentNullException(nameof(baseStream));
            if (option is null)
                throw new ArgumentNullException(nameof(option));
            if (option is not DeflateCompressionOption deflateOption)
                throw new ArgumentException($"Illegal {nameof(option)} data", nameof(option));

            var level = deflateOption.CompressionLevel switch
            {
                DeflateCompressionLevel.Fast or DeflateCompressionLevel.SuperFast => CompressionLevel.Fastest,
                DeflateCompressionLevel.Maximum => CompressionLevel.SmallestSize,
                _ => CompressionLevel.Optimal,
            };
            return new Encoder(baseStream, level, unpackedCountProgress, leaveOpen);
        }
    }
}
