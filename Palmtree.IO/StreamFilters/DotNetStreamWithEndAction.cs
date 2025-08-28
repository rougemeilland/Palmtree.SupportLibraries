using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class DotNetStreamWithEndAction
        : Stream, IDirectDotNetStreamWrapper
    {
        private readonly Stream _baseStream;
        private readonly Action _endAction;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;

        public DotNetStreamWithEndAction(Stream baseStream, Action endAction, Boolean leaveOpen)
        {
            _baseStream = baseStream ?? throw new ArgumentNullException(nameof(baseStream));
            _endAction = endAction ?? throw new ArgumentNullException(nameof(endAction));
            _leaveOpen = leaveOpen;
            _isDisposed = false;
            while (_baseStream is IDirectDotNetStreamWrapper wrapper)
                _baseStream = wrapper.RawStream;
        }

        public override Boolean CanRead => _baseStream.CanRead;
        public override Boolean CanSeek => _baseStream.CanSeek;
        public override Boolean CanTimeout => _baseStream.CanTimeout;
        public override Boolean CanWrite => _baseStream.CanWrite;
        public override IAsyncResult BeginRead(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state) => _baseStream.BeginRead(buffer, offset, count, callback, state);
        public override IAsyncResult BeginWrite(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state) => _baseStream.BeginWrite(buffer, offset, count, callback, state);
        public override Int32 EndRead(IAsyncResult asyncResult) => _baseStream.EndRead(asyncResult);
        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count) => _baseStream.Read(buffer, offset, count);
        public override Int32 Read(Span<Byte> buffer) => _baseStream.Read(buffer);
        public override Int32 ReadByte() => _baseStream.ReadByte();
        public override Int32 ReadTimeout { get => _baseStream.ReadTimeout; set => _baseStream.ReadTimeout = value; }
        public override Int32 WriteTimeout { get => _baseStream.WriteTimeout; set => _baseStream.WriteTimeout = value; }
        public override Int64 Length => _baseStream.Length;
        public override Int64 Position { get => _baseStream.Position; set => _baseStream.Position = value; }
        public override Int64 Seek(Int64 offset, SeekOrigin origin) => _baseStream.Seek(offset, origin);
        public override Task CopyToAsync(Stream destination, Int32 bufferSize, CancellationToken cancellationToken) => _baseStream.CopyToAsync(destination, bufferSize, cancellationToken);
        public override Task FlushAsync(CancellationToken cancellationToken) => _baseStream.FlushAsync(cancellationToken);
        public override Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken) => _baseStream.WriteAsync(buffer, offset, count, cancellationToken);
        public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken) => _baseStream.ReadAsync(buffer, offset, count, cancellationToken);
        public override ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default) => _baseStream.WriteAsync(buffer, cancellationToken);
        public override ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken cancellationToken = default) => _baseStream.ReadAsync(buffer, cancellationToken);
        public override void CopyTo(Stream destination, Int32 bufferSize) => _baseStream.CopyTo(destination, bufferSize);
        public override void EndWrite(IAsyncResult asyncResult) => _baseStream.EndWrite(asyncResult);
        public override void Flush() => _baseStream.Flush();
        public override void SetLength(Int64 value) => _baseStream.SetLength(value);
        public override void Write(Byte[] buffer, Int32 offset, Int32 count) => _baseStream.Write(buffer, offset, count);
        public override void Write(ReadOnlySpan<Byte> buffer) => _baseStream.Write(buffer);
        public override void WriteByte(Byte value) => _baseStream.WriteByte(value);

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                        _baseStream.Dispose();
                }

                try
                {
                    _endAction();
                }
                catch (Exception)
                {
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }

        Stream IDirectDotNetStreamWrapper.RawStream
        {
            get
            {
                ObjectDisposedException.ThrowIf(_isDisposed, this);

                return _baseStream;
            }
        }
    }
}
