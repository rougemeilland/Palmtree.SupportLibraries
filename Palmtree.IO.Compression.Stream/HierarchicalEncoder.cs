using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.Compression.Stream
{
    public abstract class HierarchicalEncoder
        : SequentialOutputByteStream
    {
        private readonly ISequentialOutputByteStream _baseStream;
        private readonly IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? _progress;
        private readonly Boolean _leaveOpen;
        private readonly ValueHolder<UInt64> _uncomprssedStreamProcessedCount;
        private readonly ValueHolder<UInt64> _comprssedStreamProcessedCount;

        private Boolean _isDisposed;

        public HierarchicalEncoder(
            ISequentialOutputByteStream baseStream,
            IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress,
            Boolean leaveOpen,
            Func<ISequentialOutputByteStream, ISequentialOutputByteStream> encoderStreamCreator)
        {
            if (baseStream is null)
                throw new ArgumentNullException(nameof(baseStream));
            if (encoderStreamCreator is null)
                throw new ArgumentNullException(nameof(encoderStreamCreator));

            _comprssedStreamProcessedCount = new ValueHolder<UInt64>();
            _uncomprssedStreamProcessedCount = new ValueHolder<UInt64>();
            _baseStream =
                encoderStreamCreator(
                    baseStream
                    .WithProgression(
                        new SimpleProgress<UInt64>(value =>
                        {
                            _comprssedStreamProcessedCount.Value = value;
                            progress?.Report((_uncomprssedStreamProcessedCount.Value, _comprssedStreamProcessedCount.Value));
                        })));
            _progress = progress;
            _leaveOpen = leaveOpen;
            _isDisposed = false;
        }

        protected override Int32 WriteCore(ReadOnlySpan<Byte> buffer)
        {
            if (_uncomprssedStreamProcessedCount.Value <= 0)
                _progress?.Report((_uncomprssedStreamProcessedCount.Value, _comprssedStreamProcessedCount.Value));
            if (buffer.Length <= 0)
                return 0;
            ProcessProgress(buffer.Length);
            _baseStream.WriteBytes(buffer);
            return buffer.Length;
        }

        protected override async Task<Int32> WriteAsyncCore(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken)
        {
            if (_uncomprssedStreamProcessedCount.Value <= 0)
                _progress?.Report((_uncomprssedStreamProcessedCount.Value, _comprssedStreamProcessedCount.Value));
            if (buffer.Length <= 0)
                return 0;
            ProcessProgress(buffer.Length);
            await _baseStream.WriteBytesAsync(buffer, cancellationToken).ConfigureAwait(false);
            return buffer.Length;
        }

        protected override void FlushCore() => _baseStream.Flush();
        protected override Task FlushAsyncCore(CancellationToken cancellationToken = default) => _baseStream.FlushAsync(cancellationToken);

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                        _baseStream.Dispose();
                    _progress?.Report((_uncomprssedStreamProcessedCount.Value, _comprssedStreamProcessedCount.Value));
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
                _progress?.Report((_uncomprssedStreamProcessedCount.Value, _comprssedStreamProcessedCount.Value));
                _isDisposed = true;
            }

            await base.DisposeAsyncCore().ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ProcessProgress(Int32 length)
        {
            if (length > 0)
            {
                checked
                {
                    _uncomprssedStreamProcessedCount.Value += (UInt64)length;
                }

                _progress?.Report((_uncomprssedStreamProcessedCount.Value, _comprssedStreamProcessedCount.Value));
            }
        }
    }
}
