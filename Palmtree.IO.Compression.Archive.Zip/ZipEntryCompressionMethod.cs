using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Palmtree.IO.Compression.Stream;
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
        private readonly ICoderOption? _decoderOption;
        private readonly ICompressionCoder? _encoderPlugin;
        private readonly ICoderOption? _encoderOption;

        static ZipEntryCompressionMethod()
        {
            _lockObject = new Object();
            _compresssionMethods = new Dictionary<(CompressionMethodId CompressionMethodId, CoderType CoderType), ICompressionCoder>();

            Stream.Stored.StoredCoderPlugin.EnablePlugin();
            SearchPlugins(_compresssionMethods);
            CompressionCoderPlugin.PluginsUpdated += (s, e) =>
            {
                lock (_lockObject)
                {
                    SearchPlugins(_compresssionMethods);
                }
            };
        }

        internal ZipEntryCompressionMethod(ZipEntryCompressionMethodId compressMethodId, ICompressionCoder? decoderPlugin, ICoderOption? decoderOption, ICompressionCoder? encoderPlugin, ICoderOption? encoderOption)
        {
            if (decoderPlugin is null && encoderPlugin is null)
                throw new ArgumentException($"Both {nameof(decoderPlugin)} and {nameof(encoderPlugin)} are null.");
            if (decoderPlugin is not null and not ICompressionDecoder and not ICompressionHierarchicalDecoder)
                throw new ArgumentException($"{nameof(decoderPlugin)} does not implement the required interface.");
            if (encoderPlugin is not null and not ICompressionEncoder and not ICompressionHierarchicalEncoder)
                throw new ArgumentException($"{nameof(encoderPlugin)} does not implement the required interface.");
            if (decoderPlugin is not null && decoderOption is null)
                throw new ArgumentNullException(nameof(decoderOption));
            if (encoderPlugin is not null && encoderOption is null)
                throw new ArgumentNullException(nameof(encoderOption));

            CompressionMethodId = compressMethodId;
            IsSupportedGetDecodingStream = decoderPlugin is ICompressionHierarchicalDecoder;
            IsSupportedDecode = decoderPlugin is ICompressionDecoder;
            IsSupportedGetEncodingStream = encoderPlugin is ICompressionHierarchicalEncoder;
            IsSupportedEncode = encoderPlugin is ICompressionEncoder;
            _decoderPlugin = decoderPlugin;
            _decoderOption = decoderOption;
            _encoderPlugin = encoderPlugin;
            _encoderOption = encoderOption;
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
        public Boolean IsSupportedGetDecodingStream { get; }
        public Boolean IsSupportedDecode { get; }
        public Boolean IsSupportedGetEncodingStream { get; }
        public Boolean IsSupportedEncode { get; }

        public ISequentialInputByteStream GetDecodingStream(ISequentialInputByteStream baseStream, UInt64 unpackedSize, UInt64 packedSize, IProgress<(UInt64 unpackedCount, UInt64 packedCount)>? progress = null)
        {
            if (baseStream is null)
                throw new ArgumentNullException(nameof(baseStream));

            return InternalGetDecodingStream(baseStream, unpackedSize, packedSize, progress);
        }

        public ISequentialOutputByteStream GetEncodingStream(ISequentialOutputByteStream baseStream, IProgress<(UInt64 unpackedCount, UInt64 packedCount)>? progress = null)
        {
            if (baseStream is null)
                throw new ArgumentNullException(nameof(baseStream));

            return InternalGetEncodingStream(baseStream, progress);
        }

        internal static ZipEntryCompressionMethod GetCompressionMethod(ZipEntryCompressionMethodId compressionMethodId, ZipEntryGeneralPurposeBitFlag flag)
        {
            var instance =
                CreateCompressionMethodDefaultInstance(
                    GetPluginId(compressionMethodId),
                    plugin => plugin.GetOptionFromGeneralPurposeFlag(
                        flag.HasFlag(ZipEntryGeneralPurposeBitFlag.CompresssionOption0),
                        flag.HasFlag(ZipEntryGeneralPurposeBitFlag.CompresssionOption1)));
            return
                instance ?? throw new CompressionMethodNotSupportedException(compressionMethodId);
        }

        private static ZipEntryCompressionMethod? CreateCompressionMethodDefaultInstance(CompressionMethodId compressionMethodId, Func<ICompressionCoder, ICoderOption> optionGetter)
        {
            ICompressionCoder? deoderPlugin;
            ICompressionCoder? enoderPlugin;
            lock (_lockObject)
            {
                if (!_compresssionMethods.TryGetValue((compressionMethodId, CoderType.Decoder), out deoderPlugin))
                    deoderPlugin = null;
                if (!_compresssionMethods.TryGetValue((compressionMethodId, CoderType.Encoder), out enoderPlugin))
                    enoderPlugin = null;
            }

            return
                deoderPlugin is null && enoderPlugin is null
                ? null
                : new ZipEntryCompressionMethod(
                    GetCompressionMethodId(compressionMethodId),
                    deoderPlugin,
                    deoderPlugin is null ? null : optionGetter(deoderPlugin),
                    enoderPlugin,
                    enoderPlugin is null ? null : optionGetter(enoderPlugin));
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

        private ISequentialInputByteStream InternalGetDecodingStream(
            ISequentialInputByteStream baseStream,
            UInt64 unpackedSize,
            UInt64 packedSize,
            IProgress<(UInt64 unpackedCount, UInt64 packedCount)>? progress)
        {
            switch (_decoderPlugin)
            {
                case ICompressionHierarchicalDecoder hierarchicalDecoder:
                {
                    Validation.Assert(_decoderOption is not null, "_decoderOption is not null");
                    var progressCounter = new ProgressCounterUint64Uint64(progress);
                    progressCounter.Report();
                    return
                        hierarchicalDecoder.GetDecodingStream(
                            baseStream
                                .WithProgression(SafetyProgress.CreateIncreasingProgress<UInt64>(progressCounter.SetValue2, 0, packedSize)),
                            _decoderOption,
                            unpackedSize,
                            packedSize,
                            SafetyProgress.CreateIncreasingProgress<UInt64>(progressCounter.SetValue1, 0, unpackedSize))
                        .WithCache()
                        .WithEndAction(_ => progressCounter.Report());
                }
                case ICompressionDecoder decoder:
                {
                    Validation.Assert(_decoderOption is not null, "_decoderOption is not null");
                    var progressCounter = new ProgressCounterUint64Uint64(progress);
                    var queue = new InProcessPipe();
                    var decoderOption = _decoderOption;
                    _ = Task.Run(() =>
                    {
                        try
                        {
                            progressCounter.Report();
                            using var queueWriter = queue.OpenOutputStream();
                            decoder.Decode(
                                baseStream
                                    .WithProgression(SafetyProgress.CreateIncreasingProgress<UInt64>(progressCounter.SetValue2, 0, packedSize)),
                                queueWriter
                                    .WithCache(),
                                decoderOption,
                                unpackedSize,
                                packedSize,
                                SafetyProgress.CreateIncreasingProgress<UInt64>(progressCounter.SetValue1, 0, unpackedSize));
                        }
                        catch (Exception)
                        {
                        }
                    });
                    return
                        queue.OpenInputStream()
                        .WithEndAction(_ => progressCounter.Report());
                }

                default:
                    throw new NotSupportedException($"Compression is not suppoted. : method = {CompressionMethodId}");
            }
        }

        private ISequentialOutputByteStream InternalGetEncodingStream(
            ISequentialOutputByteStream baseStream,
            IProgress<(UInt64 unpackedCount, UInt64 packedCount)>? progress)
        {
            switch (_encoderPlugin)
            {
                case ICompressionHierarchicalEncoder hierarchicalEncoder:
                {
                    Validation.Assert(_encoderOption is not null, "_encoderOption is not null");
                    var progressCounter = new ProgressCounterUint64Uint64(progress);
                    progressCounter.Report();
                    return
                        hierarchicalEncoder.GetEncodingStream(
                            baseStream
                                .WithProgression(SafetyProgress.CreateIncreasingProgress<UInt64>(progressCounter.SetValue2))
                                .WithEndAction(_ => progressCounter.Report()),
                            _encoderOption,
                            SafetyProgress.CreateIncreasingProgress<UInt64>(progressCounter.SetValue1))
                        .WithCache();
                }
                case ICompressionEncoder encoder:
                {
                    Validation.Assert(_encoderOption is not null, "_encoderOption is not null");
                    var progressCounter = new ProgressCounterUint64Uint64(progress);
                    progressCounter.Report();
                    var queue = new InProcessPipe();
                    var encoderOption = _encoderOption;
                    _ = Task.Run(() =>
                    {
                        try
                        {
                            using var queueReader = queue.OpenInputStream();
                            encoder.Encode(
                                queueReader,
                                baseStream
                                    .WithProgression(SafetyProgress.CreateIncreasingProgress<UInt64>(progressCounter.SetValue2))
                                    .WithEndAction(_ => progressCounter.Report()),
                                encoderOption,
                                SafetyProgress.CreateIncreasingProgress<UInt64>(progressCounter.SetValue1));
                        }
                        catch (Exception)
                        {
                        }
                    });
                    return
                        queue.OpenOutputStream()
                        .WithCache();
                }

                default:
                    throw new NotSupportedException($"Compression is not suppoted. : method = {CompressionMethodId}");
            }
        }
    }
}
