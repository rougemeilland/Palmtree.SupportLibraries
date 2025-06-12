using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class DotNetStreamBySequentialOutputByteStream
        : Stream
    {
        private readonly ISequentialOutputByteStream _baseStream;
        private readonly Boolean _leaveOpen;
        private readonly IRandomOutputByteStream<UInt64>? _randomAccessStream;

        private Boolean _isDisposed;

        public DotNetStreamBySequentialOutputByteStream(ISequentialOutputByteStream baseStream, Boolean leaveOpen)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(baseStream);

                _baseStream = baseStream;
                _leaveOpen = leaveOpen;
                _isDisposed = false;
                _randomAccessStream = baseStream as IRandomOutputByteStream<UInt64>;
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    baseStream?.Dispose();
                throw;
            }
        }

        public override Boolean CanSeek => _randomAccessStream is not null;
        public override Boolean CanRead => false;
        public override Boolean CanWrite => true;
        public override Int64 Length
        {
            get
            {
                if (_randomAccessStream is null)
                    throw new NotSupportedException();
                ObjectDisposedException.ThrowIf(_isDisposed, this);

                return checked((Int64)_randomAccessStream.Length);
            }
        }

        public override void SetLength(Int64 value)
        {
            if (_randomAccessStream is null)
                throw new NotSupportedException();
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            ArgumentOutOfRangeException.ThrowIfNegative(value);

            _randomAccessStream.Length = checked((UInt64)value);
        }

        public override Int64 Position
        {
            get
            {
                if (_randomAccessStream is null)
                    throw new NotSupportedException();
                ObjectDisposedException.ThrowIf(_isDisposed, this);

                return checked((Int64)_randomAccessStream.Position);
            }

            set
            {
                if (_randomAccessStream is null)
                    throw new NotSupportedException();
                ObjectDisposedException.ThrowIf(_isDisposed, this);
                ObjectDisposedException.ThrowIf(_isDisposed, this);
                ArgumentOutOfRangeException.ThrowIfNegative(value);

                _randomAccessStream.Seek(checked((UInt64)value));
            }
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            if (_randomAccessStream is null)
                throw new NotSupportedException();
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            UInt64 absoluteOffset;
            switch (origin)
            {
                case SeekOrigin.Begin:
                    ArgumentOutOfRangeException.ThrowIfNegative(offset);
                    absoluteOffset = checked((UInt64)offset);
                    break;
                case SeekOrigin.Current:
                    try
                    {
                        absoluteOffset = _randomAccessStream.Position.AddAsUInt(offset);
                    }
                    catch (OverflowException ex)
                    {
                        throw new ArgumentOutOfRangeException($"Invalid {nameof(offset)} value", ex);
                    }

                    break;
                case SeekOrigin.End:
                    try
                    {
                        absoluteOffset = _randomAccessStream.Length.AddAsUInt(offset);
                    }
                    catch (OverflowException ex)
                    {
                        throw new ArgumentOutOfRangeException($"Invalid {nameof(offset)} value", ex);
                    }

                    break;
                default:
                    throw new ArgumentException($"Invalid {nameof(SeekOrigin)} value", nameof(origin));
            }

            if (absoluteOffset > Int64.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(offset));
            _randomAccessStream.Seek(absoluteOffset);
            return checked((Int64)absoluteOffset);
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count) => throw new NotSupportedException();
        public override Int32 Read(Span<Byte> buffer) => throw new NotSupportedException();
        public override Int32 ReadByte() => throw new NotSupportedException();
        public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken) => throw new NotSupportedException();
        public override ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken cancellationToken = default) => throw new NotSupportedException();

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            _baseStream.WriteBytes(buffer, offset, count);
        }

        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            _baseStream.WriteBytes(buffer);
        }

        public override void WriteByte(Byte value)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            ReadOnlySpan<Byte> buffer = [value];
            _baseStream.WriteBytes(buffer);
        }

        public override Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            return _baseStream.WriteBytesAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public override async ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            await _baseStream.WriteBytesAsync(buffer, cancellationToken).ConfigureAwait(false);
        }

        public override void Flush()
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            _baseStream.Flush();
        }

        public override Task FlushAsync(CancellationToken cancellationToken = default)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            return _baseStream.FlushAsync(cancellationToken);
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                        _baseStream.Dispose();
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }

        public override async ValueTask DisposeAsync()
        {
            if (!_isDisposed)
            {
                if (!_leaveOpen)
                    await _baseStream.DisposeAsync().ConfigureAwait(false);
                _isDisposed = true;
            }

            await base.DisposeAsync().ConfigureAwait(false);
        }
    }
}
