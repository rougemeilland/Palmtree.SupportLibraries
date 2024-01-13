using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Palmtree;

namespace Palmtree.IO.StreamFilters
{
    internal class DotNetStreamBySequentialInputByteStream
        : Stream
    {
        private readonly ISequentialInputByteStream _baseStream;
        private readonly Boolean _leaveOpen;
        private readonly IRandomInputByteStream<UInt64>? _randomAccessStream;

        private Boolean _isDisposed;

        public DotNetStreamBySequentialInputByteStream(ISequentialInputByteStream baseStream, Boolean leaveOpen)
        {
            try
            {
                if (baseStream is null)
                    throw new ArgumentNullException(nameof(baseStream));

                _baseStream = baseStream;
                _leaveOpen = leaveOpen;
                _isDisposed = false;
                _randomAccessStream = baseStream as IRandomInputByteStream<UInt64>;
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    baseStream?.Dispose();
                throw;
            }
        }

        public override Boolean CanSeek => _randomAccessStream is not null;
        public override Boolean CanRead => true;
        public override Boolean CanWrite => false;

        public override Int64 Length
        {
            get
            {
                if (_randomAccessStream is null)
                    throw new NotSupportedException();
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().FullName);

                return checked((Int64)_randomAccessStream.Length);
            }
        }

        public override void SetLength(Int64 value) => throw new NotSupportedException();

        public override Int64 Position
        {
            get
            {
                if (_randomAccessStream is null)
                    throw new NotSupportedException();
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().FullName);

                return checked((Int64)_randomAccessStream.Position);
            }

            set
            {
                if (_randomAccessStream is null)
                    throw new NotSupportedException();
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().FullName);
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _randomAccessStream.Seek(checked((UInt64)value));
            }
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            if (_randomAccessStream is null)
                throw new NotSupportedException();
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            UInt64 absoluteOffset;
            switch (origin)
            {
                case SeekOrigin.Begin:
                    if (offset < 0)
                        throw new ArgumentOutOfRangeException(nameof(offset));

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
                    throw new ArgumentException($"Invalid {nameof(SeekOrigin)} value : {nameof(origin)}=\"{origin}\"", nameof(origin));
            }

            if (absoluteOffset > Int64.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(offset));

            _randomAccessStream.Seek(absoluteOffset);
            return checked((Int64)absoluteOffset);
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().FullName);
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            return _baseStream.Read(buffer.AsSpan(offset, count));
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            return _baseStream.Read(buffer);
        }

        public override Int32 ReadByte()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            Span<Byte> buffer = stackalloc Byte[1];
            var length = _baseStream.ReadBytes(buffer);
            if (length != buffer.Length)
                return -1;
            return buffer[0];
        }

        public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().FullName);
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            return _baseStream.ReadAsync(buffer.AsMemory(offset, count), cancellationToken);
        }

        public override async ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            return await _baseStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count) => throw new NotSupportedException();
        public override void Write(ReadOnlySpan<Byte> buffer) => throw new NotSupportedException();
        public override void WriteByte(Byte value) => throw new NotSupportedException();
        public override Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken) => throw new NotSupportedException();
        public override ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default) => throw new NotSupportedException();

        public override void Flush() { }

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

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (!_isDisposed)
            {
                if (!_leaveOpen)
                    _baseStream.Dispose();
                _isDisposed = true;
            }

            await base.DisposeAsync().ConfigureAwait(false);
        }
    }
}
