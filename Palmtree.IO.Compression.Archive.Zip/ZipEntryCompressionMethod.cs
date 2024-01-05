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

        private static readonly IDictionary<(CompressionMethodId CompressionMethodId, CoderType CoderType), ICompressionCoder> _compresssionMethods;
        private static readonly ZipEntryCompressionMethod? _stored;
        private static readonly ZipEntryCompressionMethod? _deflateWithNormal;
        private static readonly ZipEntryCompressionMethod? _deflateWithMaximum;
        private static readonly ZipEntryCompressionMethod? _deflateWithFast;
        private static readonly ZipEntryCompressionMethod? _deflateWithSuperFast;
        private static readonly ZipEntryCompressionMethod? _deflate64WithNormal;
        private static readonly ZipEntryCompressionMethod? _deflate64WithMaximum;
        private static readonly ZipEntryCompressionMethod? _deflate64WithFast;
        private static readonly ZipEntryCompressionMethod? _deflate64WithSuperFast;
        private static readonly ZipEntryCompressionMethod? _bzip2;
        private static readonly ZipEntryCompressionMethod? _lzmaWithEOS;
        private static readonly ZipEntryCompressionMethod? _lzmaWithoutEOS;
        private static readonly ZipEntryCompressionMethod? _ppmd;

        private readonly ICompressionCoder? _decoderPlugin;
        private readonly ICoderOption? _decoderOption;
        private readonly ICompressionCoder? _encoderPlugin;
        private readonly ICoderOption? _encoderOption;

        static ZipEntryCompressionMethod()
        {
            Stream.Stored.StoredCoderPlugin.EnablePlugin();

            _compresssionMethods = EnumeratePlugin();

            _stored =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.Stored,
                    plugin => plugin.DefaultOption);
            _deflateWithNormal =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.Deflate,
                    _ => CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.Normal));
            _deflateWithMaximum =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.Deflate,
                    _ => CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.Maximum));
            _deflateWithFast =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.Deflate,
                    _ => CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.Fast));
            _deflateWithSuperFast =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.Deflate,
                    _ => CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.SuperFast));
            _deflate64WithNormal =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.Deflate64,
                    _ => CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.Normal));
            _deflate64WithMaximum =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.Deflate64,
                    _ => CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.Maximum));
            _deflate64WithFast =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.Deflate64,
                    _ => CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.Fast));
            _deflate64WithSuperFast =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.Deflate64,
                    _ => CompressionOption.GetDeflateCompressionOption(DeflateCompressionLevel.SuperFast));
            _bzip2 =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.BZIP2,
                    plugin => plugin.DefaultOption);
            _lzmaWithEOS =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.LZMA,
                    _ => CompressionOption.GetLzmaCompressionOption(true));
            _lzmaWithoutEOS =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.LZMA,
                    _ => CompressionOption.GetLzmaCompressionOption(false));
            _ppmd =
                CreateCompressionMethodDefaultInstance(
                    _compresssionMethods,
                    Stream.CompressionMethodId.PPMd,
                    plugin => plugin.DefaultOption);
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

        public static IEnumerable<ZipEntryCompressionMethodId> SupportedCompresssionMethodIds => _compresssionMethods.Keys.Select(key => GetCompressionMethodId(key.CompressionMethodId));
        public static ZipEntryCompressionMethod Stored => _stored ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.Stored);
        public static ZipEntryCompressionMethod DeflateWithNormal => _deflateWithNormal ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.Deflate);
        public static ZipEntryCompressionMethod DeflateWithMaximum => _deflateWithMaximum ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.Deflate);
        public static ZipEntryCompressionMethod DeflateWithFast => _deflateWithFast ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.Deflate);
        public static ZipEntryCompressionMethod DeflateWithSuperFast => _deflateWithSuperFast ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.Deflate);
        public static ZipEntryCompressionMethod Deflate64WithNormal => _deflate64WithNormal ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.Deflate64);
        public static ZipEntryCompressionMethod Deflate64WithMaximum => _deflate64WithMaximum ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.Deflate64);
        public static ZipEntryCompressionMethod Deflate64WithFast => _deflate64WithFast ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.Deflate64);
        public static ZipEntryCompressionMethod Deflate64WithSuperFast => _deflate64WithSuperFast ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.Deflate64);
        public static ZipEntryCompressionMethod BZIP2 => _bzip2 ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.BZIP2);
        public static ZipEntryCompressionMethod LZMAWithEOS => _lzmaWithEOS ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.LZMA);
        public static ZipEntryCompressionMethod LZMAWithoutEOS => _lzmaWithoutEOS ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.LZMA);
        public static ZipEntryCompressionMethod PPMd => _ppmd ?? throw new CompressionMethodNotSupportedException(ZipEntryCompressionMethodId.PPMd);

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
                    _compresssionMethods,
                    GetPluginId(compressionMethodId),
                    plugin => plugin.GetOptionFromGeneralPurposeFlag(

                    flag.HasFlag(ZipEntryGeneralPurposeBitFlag.CompresssionOption0),
                    flag.HasFlag(ZipEntryGeneralPurposeBitFlag.CompresssionOption1)));
            return
                instance ?? throw new CompressionMethodNotSupportedException(compressionMethodId);
        }

        private static ZipEntryCompressionMethod? CreateCompressionMethodDefaultInstance(IDictionary<(CompressionMethodId CompressionMethodId, CoderType CoderType), ICompressionCoder> compresssionMethodSource, CompressionMethodId compressionMethodId, Func<ICompressionCoder, ICoderOption> optionGetter)
        {
            if (!compresssionMethodSource.TryGetValue((compressionMethodId, CoderType.Decoder), out ICompressionCoder? deoderPlugin))
                deoderPlugin = null;
            if (!compresssionMethodSource.TryGetValue((compressionMethodId, CoderType.Encoder), out ICompressionCoder? enoderPlugin))
                enoderPlugin = null;
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

        private static IDictionary<(CompressionMethodId CompressionMethodId, CoderType CoderType), ICompressionCoder> EnumeratePlugin()
        {
            var plugins = new Dictionary<(CompressionMethodId CompressionMethodId, CoderType CoderType), ICompressionCoder>();
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

            return plugins;
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
