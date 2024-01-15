using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Palmtree.IO.Compression.Stream;
using Palmtree.IO.Compression.Stream.Plugin;
using Palmtree.Threading;

namespace Palmtree.IO.Compression.Archive.Zip
{
    internal class ZipEntryCompressionMethod
    {
        private enum CoderType
        {
            Decoder,
            Encoder,
        }

        private static readonly Object _lockObject;
        private static readonly IDictionary<(CompressionMethodId CompressionMethodId, CoderType CoderType), ICompressionCoder> _compresssionMethods;

        private readonly ICompressionCoder? _decoderPlugin;
        private readonly ICompressionCoder? _encoderPlugin;

        static ZipEntryCompressionMethod()
        {
            _lockObject = new Object();
            _compresssionMethods = new Dictionary<(CompressionMethodId CompressionMethodId, CoderType CoderType), ICompressionCoder>();

            StoredCoderPlugin.EnablePlugin();
            SearchPlugins(_compresssionMethods);
            CompressionCoderPlugin.PluginsUpdated += (s, e) =>
            {
                lock (_lockObject)
                {
                    SearchPlugins(_compresssionMethods);
                }
            };
        }

        internal ZipEntryCompressionMethod(ZipEntryCompressionMethodId compressMethodId, ICompressionCoder? decoderPlugin, ICompressionCoder? encoderPlugin)
        {
            if (decoderPlugin is null && encoderPlugin is null)
                throw new ArgumentException($"Both {nameof(decoderPlugin)} and {nameof(encoderPlugin)} are null.");
            if (decoderPlugin is not null and not ICompressionDecoder and not ICompressionHierarchicalDecoder)
                throw new ArgumentException($"{nameof(decoderPlugin)} does not implement the required interface.");
            if (encoderPlugin is not null and not ICompressionEncoder and not ICompressionHierarchicalEncoder)
                throw new ArgumentException($"{nameof(encoderPlugin)} does not implement the required interface.");

            CompressionMethodId = compressMethodId;
            _decoderPlugin = decoderPlugin;
            _encoderPlugin = encoderPlugin;
        }

        public static IEnumerable<ZipEntryCompressionMethodId> SupportedCompresssionMethodIds
        {
            get
            {
                lock (_lockObject)
                {
                    return
                        _compresssionMethods.Keys
                        .Select(key => GetCompressionMethodId(key.CompressionMethodId))
                        .ToArray();
                }
            }
        }

        public ZipEntryCompressionMethodId CompressionMethodId { get; }

        public ISequentialInputByteStream CreateDecoderStream(
            ISequentialInputByteStream baseStream,
            ICoderOption decoderOption,
            UInt64 unpackedSize,
            UInt64 packedSize,
            IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? progress)
        {
            if (baseStream is null)
                throw new ArgumentNullException(nameof(baseStream));
            if (decoderOption is null)
                throw new ArgumentNullException(nameof(decoderOption));

            return
                InternalCreateDecoderStream(
                    baseStream.WithCache(),
                    decoderOption,
                    unpackedSize,
                    packedSize,
                    progress);
        }

        public ISequentialOutputByteStream CreateEncoderStream(
            ISequentialOutputByteStream baseStream,
            ICoderOption encoderOption,
            IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress)
        {
            if (baseStream is null)
                throw new ArgumentNullException(nameof(baseStream));
            if (encoderOption is null)
                throw new ArgumentNullException(nameof(encoderOption));

            return
                InternalCreateEncoderStream(
                    baseStream.WithCache(),
                    encoderOption,
                    progress);
        }

        internal static ZipEntryCompressionMethod GetCompressionMethod(ZipEntryCompressionMethodId compressionMethodId)
        {
            var pluginId = GetPluginId(compressionMethodId);
            ICompressionCoder? deoderPlugin;
            ICompressionCoder? enoderPlugin;
            lock (_lockObject)
            {
                if (!_compresssionMethods.TryGetValue((pluginId, CoderType.Decoder), out deoderPlugin))
                    deoderPlugin = null;
                if (!_compresssionMethods.TryGetValue((pluginId, CoderType.Encoder), out enoderPlugin))
                    enoderPlugin = null;
            }

            if (deoderPlugin is null && enoderPlugin is null)
                throw new CompressionMethodNotSupportedException(compressionMethodId);

            return
                new ZipEntryCompressionMethod(
                    compressionMethodId,
                    deoderPlugin,
                    enoderPlugin);
        }

        private static void SearchPlugins(IDictionary<(CompressionMethodId CompressionMethodId, CoderType CoderType), ICompressionCoder> plugins)
        {
            plugins.Clear();
            foreach (var plugin in CompressionCoderPlugin.EnumeratePlugins())
            {
                if (plugin is ICompressionDecoder or ICompressionHierarchicalDecoder)
                {
                    if (!plugins.TryAdd((plugin.CompressionMethodId, CoderType.Decoder), plugin))
                        throw new IllegalRuntimeEnvironmentException($"Duplicate Compress plug-in. : method={plugin.CompressionMethodId}, type={CoderType.Decoder}, new plugin={plugin.GetType()}");
                }

                if (plugin is ICompressionEncoder or ICompressionHierarchicalEncoder)
                {
                    if (!plugins.TryAdd((plugin.CompressionMethodId, CoderType.Encoder), plugin))
                        throw new IllegalRuntimeEnvironmentException($"Duplicate Compress plug-in. : method={plugin.CompressionMethodId}, type={CoderType.Decoder}, new plugin={plugin.GetType()}");
                }
            }
        }

        private static CompressionMethodId GetPluginId(ZipEntryCompressionMethodId compressionMethodId)
            => compressionMethodId switch
            {
                ZipEntryCompressionMethodId.Stored => Stream.CompressionMethodId.Stored,
                ZipEntryCompressionMethodId.Deflate => Stream.CompressionMethodId.Deflate,
                ZipEntryCompressionMethodId.Deflate64 => Stream.CompressionMethodId.Deflate64,
                ZipEntryCompressionMethodId.BZIP2 => Stream.CompressionMethodId.BZIP2,
                ZipEntryCompressionMethodId.LZMA => Stream.CompressionMethodId.LZMA,
                ZipEntryCompressionMethodId.PPMd => Stream.CompressionMethodId.PPMd,
                _ => Stream.CompressionMethodId.Unknown,
            };

        private static ZipEntryCompressionMethodId GetCompressionMethodId(CompressionMethodId pluginId)
            => pluginId switch
            {
                Stream.CompressionMethodId.Stored => ZipEntryCompressionMethodId.Stored,
                Stream.CompressionMethodId.Deflate => ZipEntryCompressionMethodId.Deflate,
                Stream.CompressionMethodId.Deflate64 => ZipEntryCompressionMethodId.Deflate64,
                Stream.CompressionMethodId.BZIP2 => ZipEntryCompressionMethodId.BZIP2,
                Stream.CompressionMethodId.LZMA => ZipEntryCompressionMethodId.LZMA,
                Stream.CompressionMethodId.PPMd => ZipEntryCompressionMethodId.PPMd,
                _ => ZipEntryCompressionMethodId.Unknown,
            };

        private ISequentialInputByteStream InternalCreateDecoderStream(
            ISequentialInputByteStream baseStream,
            ICoderOption decoderOption,
            UInt64 unpackedSize,
            UInt64 packedSize,
            IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? progress)
        {
            switch (_decoderPlugin)
            {
                case ICompressionHierarchicalDecoder hierarchicalDecoder:
                {
                    return
                        hierarchicalDecoder.CreateDecoderStream(
                            baseStream,
                            decoderOption,
                            unpackedSize,
                            packedSize,
                            progress)
                        .WithCache();
                }
                case ICompressionDecoder decoder:
                {
                    var pipe = new InProcessPipe();
                    _ = Task.Run(() =>
                    {
                        try
                        {
                            using var pipeWriter = pipe.OpenOutputStream();
                            decoder.Decode(
                                baseStream,
                                pipeWriter,
                                decoderOption,
                                unpackedSize,
                                packedSize,
                                progress);
                            pipeWriter.Flush();
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            baseStream.Dispose();
                        }
                    });
                    return pipe.OpenInputStream();
                }

                default:
                    throw new NotSupportedException($"Compression is not suppoted. : method = {CompressionMethodId}");
            }
        }

        private ISequentialOutputByteStream InternalCreateEncoderStream(
            ISequentialOutputByteStream baseStream,
            ICoderOption encoderOption,
            IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress)
        {
            switch (_encoderPlugin)
            {
                case ICompressionHierarchicalEncoder hierarchicalEncoder:
                {
                    return
                        hierarchicalEncoder.CreateEncoderStream(
                            baseStream,
                            encoderOption,
                            progress)
                        .WithCache();
                }
                case ICompressionEncoder encoder:
                {
                    var pipe = new InProcessPipe();
                    var syncObject = new ManualResetEventSlim();
                    _ = Task.Run(() =>
                    {
                        try
                        {
                            using var pipeReader = pipe.OpenInputStream();
                            encoder.Encode(
                                pipeReader,
                                baseStream,
                                encoderOption,
                                progress);
                            baseStream.Flush();
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            baseStream.Dispose();
                            syncObject.Set();
                        }
                    });
                    return
                        pipe.OpenOutputStream()
                        .WithEndAction(_ =>
                        {
                            syncObject.Wait();
                            syncObject.Dispose();
                        });
                }

                default:
                    throw new NotSupportedException($"Compression is not suppoted. : method = {CompressionMethodId}");
            }
        }
    }
}
