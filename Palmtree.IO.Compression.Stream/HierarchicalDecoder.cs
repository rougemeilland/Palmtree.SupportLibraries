using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.Compression.Stream
{
    public abstract class HierarchicalDecoder
        : SequentialInputByteStream
    {
        private readonly ISequentialInputByteStream _baseStream;
        private readonly UInt64 _unpackedStreamSize;
        private readonly IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? _progress;
        private readonly Boolean _leaveOpen;
        private readonly ValueHolder<UInt64> _comprssedStreamProcessedCount;
        private readonly ValueHolder<UInt64> _uncomprssedStreamProcessedCount;

        private Boolean _isDisposed;
        private Boolean _isEndOfStream;

        public HierarchicalDecoder(
            ISequentialInputByteStream baseStream,
            UInt64 unpackedStreamSize,
            IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? progress,
            Boolean leaveOpen,
            Func<ISequentialInputByteStream, ISequentialInputByteStream> decoderStreamCreator)
        {
            if (baseStream is null)
                throw new ArgumentNullException(nameof(baseStream));
            if (decoderStreamCreator is null)
                throw new ArgumentNullException(nameof(decoderStreamCreator));

            _comprssedStreamProcessedCount = new ValueHolder<UInt64>();
            _uncomprssedStreamProcessedCount = new ValueHolder<UInt64>();

            _baseStream =
                decoderStreamCreator(
                    progress is null
                    ? baseStream
                    : baseStream
                        .WithProgression(
                            new SimpleProgress<UInt64>(value =>
                            {
                                _comprssedStreamProcessedCount.Value = value;
                                progress.Report((_comprssedStreamProcessedCount.Value, _uncomprssedStreamProcessedCount.Value));
                            })));
            _unpackedStreamSize = unpackedStreamSize;
            _progress = progress;
            _leaveOpen = leaveOpen;
            _isDisposed = false;
        }

        protected override Int32 ReadCore(Span<Byte> buffer)
        {
            if (_uncomprssedStreamProcessedCount.Value <= 0 && _progress is not null)
                _progress.Report((_comprssedStreamProcessedCount.Value, _uncomprssedStreamProcessedCount.Value));
            if (_isEndOfStream || buffer.Length <= 0)
                return 0;

            try
            {
                var length = _baseStream.Read(buffer);
                ProcessProgress(length);
                return length;
            }
            catch (Exception ex)
            {
                throw new DataErrorException("Failed to uncompression.", ex);
            }
        }

        protected override async Task<Int32> ReadAsyncCore(Memory<Byte> buffer, CancellationToken cancellationToken)
        {
            if (_uncomprssedStreamProcessedCount.Value <= 0 && _progress is not null)
                _progress.Report((_comprssedStreamProcessedCount.Value, _uncomprssedStreamProcessedCount.Value));
            if (_isEndOfStream || buffer.Length <= 0)
                return 0;
            try
            {
                var length = await _baseStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
                ProcessProgress(length);
                return length;
            }
            catch (Exception ex)
            {
                throw new DataErrorException("Failed to uncompression.", ex);
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                        _baseStream.Dispose();
                    _progress?.Report((_comprssedStreamProcessedCount.Value, _uncomprssedStreamProcessedCount.Value));
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }

        protected override async Task DisposeAsyncCore()
        {
            if (!_isDisposed)
            {
                if (!_leaveOpen)
                    await _baseStream.DisposeAsync().ConfigureAwait(false);
                _progress?.Report((_comprssedStreamProcessedCount.Value, _uncomprssedStreamProcessedCount.Value));
                _isDisposed = true;
            }

            await base.DisposeAsyncCore().ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessProgress(Int32 length)
        {
            if (length > 0)
            {
                if (_progress is not null)
                {
                    checked
                    {
                        _uncomprssedStreamProcessedCount.Value += (UInt64)length;
                    }

                    _progress.Report((_comprssedStreamProcessedCount.Value, _uncomprssedStreamProcessedCount.Value));
                }
            }
            else
            {
                _isEndOfStream = true;
                if (_uncomprssedStreamProcessedCount.Value != _unpackedStreamSize)
                    throw new IOException("Size not match");
            }
        }
    }
}
