using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class DotNetStreamByStreamReader
        : Stream, IDirectDotNetStreamWrapper
    {
        private readonly StreamReader _reader;
        private readonly Stream _baseStream;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;

        public DotNetStreamByStreamReader(StreamReader reader, Boolean leaveOpen)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _leaveOpen = leaveOpen;
            _baseStream = reader.BaseStream;
            while (_baseStream is IDirectDotNetStreamWrapper wrapper)
                _baseStream = wrapper.RawStream;
        }

        public override Boolean CanSeek => false;
        public override Boolean CanRead => true;
        public override Boolean CanWrite => false;
        public override Int64 Length => throw new NotSupportedException();

        public override Int64 Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override void Flush() => throw new NotSupportedException();

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return _baseStream.Read(buffer, offset, count);
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            return _baseStream.Read(buffer);
        }

        public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return _baseStream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            return _baseStream.ReadAsync(buffer, cancellationToken);
        }

        public override Int32 ReadByte()
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            return _baseStream.ReadByte();
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(Int64 value) => throw new NotSupportedException();
        public override void Write(Byte[] buffer, Int32 offset, Int32 count) => throw new NotSupportedException();

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                        _reader.Dispose();
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }

        Stream IDirectDotNetStreamWrapper.RawStream => _baseStream;
    }
}
