using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Palmtree;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class DotNetStreamByPassThroughSequentialInputOutputByteStream
        : Stream, IDirectDotNetStreamWrapper
    {
        private readonly IDisposable _baseStream;
        private readonly Stream _rawStream;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;

        public DotNetStreamByPassThroughSequentialInputOutputByteStream(IDisposable baseStream, Stream rawStream, Boolean leaveOpen)
        {
            try
            {
                _baseStream = baseStream ?? throw new ArgumentNullException(nameof(baseStream));
                _rawStream = rawStream ?? throw new ArgumentNullException(nameof(rawStream));
                _leaveOpen = leaveOpen;
                while (_rawStream is IDirectDotNetStreamWrapper wrapper)
                    _rawStream = wrapper.RawStream;
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    baseStream?.Dispose();
                throw;
            }
        }

        public override Boolean CanSeek => _rawStream.CanSeek;
        public override Boolean CanRead => _rawStream.CanRead;
        public override Boolean CanWrite => _rawStream.CanWrite;
        public override Boolean CanTimeout => _rawStream.CanTimeout;

        public override Int64 Length
        {
            get
            {
                ObjectDisposedException.ThrowIf(_isDisposed, this);

                return _rawStream.Length;
            }
        }

        public override void SetLength(Int64 value)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            _rawStream.SetLength(value);
        }

        public override Int64 Position
        {
            get
            {
                ObjectDisposedException.ThrowIf(_isDisposed, this);

                return _rawStream.Position;
            }

            set
            {
                ObjectDisposedException.ThrowIf(_isDisposed, this);

                _rawStream.Position = value;
            }
        }

        public override Int32 ReadTimeout
        {
            get => _rawStream.ReadTimeout;
            set => _rawStream.ReadTimeout = value;
        }

        public override Int32 WriteTimeout
        {
            get => _rawStream.WriteTimeout;
            set => _rawStream.WriteTimeout = value;
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            return _rawStream.Seek(offset, origin);
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return _rawStream.Read(buffer, offset, count);
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            return _rawStream.Read(buffer);
        }

        public override Int32 ReadByte()
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            return _rawStream.ReadByte();
        }

        public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return _rawStream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            return _rawStream.ReadAsync(buffer, cancellationToken);
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            _rawStream.Write(buffer, offset, count);
        }

        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            _rawStream.Write(buffer);
        }

        public override void WriteByte(Byte value)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            _rawStream.WriteByte(value);
        }

        public override Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return _rawStream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            return _rawStream.WriteAsync(buffer, cancellationToken);
        }

        public override void Flush()
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            _rawStream.Flush();
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            return _rawStream.FlushAsync(cancellationToken);
        }

        public override void CopyTo(Stream destination, Int32 bufferSize)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            ArgumentNullException.ThrowIfNull(destination);

            _rawStream.CopyTo(destination, bufferSize);
        }

        public override Task CopyToAsync(Stream destination, Int32 bufferSize, CancellationToken cancellationToken)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            ArgumentNullException.ThrowIfNull(destination);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bufferSize);

            return _rawStream.CopyToAsync(destination, bufferSize, cancellationToken);
        }

        protected override void Dispose(Boolean disposing)
        {
            base.Dispose(disposing);

            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                        _baseStream.Dispose();
                }

                _isDisposed = true;
            }
        }

        Stream IDirectDotNetStreamWrapper.RawStream => _rawStream;
    }
}
