using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class DotNetStreamWithBranch
        : Stream
    {
        private readonly Stream _baseStream1;
        private readonly Stream _baseStream2;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;

        public DotNetStreamWithBranch(Stream baseStream1, Stream baseStream2, Boolean leaveOpen)
        {
            Validation.Assert(baseStream1.CanWrite);
            Validation.Assert(baseStream2.CanWrite);
            _baseStream1 = baseStream1;
            _baseStream2 = baseStream2;
            _leaveOpen = leaveOpen;
            _isDisposed = false;
        }

        public override Boolean CanRead => false;
        public override Boolean CanSeek => false;
        public override Boolean CanTimeout => _baseStream1.CanTimeout && _baseStream2.CanTimeout;
        public override Boolean CanWrite => true;

        public override Int32 ReadTimeout
        {
            get => throw new InvalidOperationException();
            set => throw new InvalidOperationException();
        }

        public override Int32 WriteTimeout
        {
            get => throw new InvalidOperationException();
            set => throw new InvalidOperationException();
        }

        public override Int64 Length => throw new NotSupportedException();

        public override Int64 Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override IAsyncResult BeginRead(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state) => throw new NotSupportedException();
        public override Int32 EndRead(IAsyncResult asyncResult) => throw new NotSupportedException();
        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count) => throw new NotSupportedException();
        public override Int32 Read(Span<Byte> buffer) => throw new NotSupportedException();
        public override Int32 ReadByte() => throw new NotSupportedException();

        public override Int64 Seek(Int64 offset, SeekOrigin origin) => throw new NotSupportedException();
        public override Task CopyToAsync(Stream destination, Int32 bufferSize, CancellationToken cancellationToken) => throw new NotSupportedException();

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            var task1 = _baseStream1.FlushAsync(cancellationToken);
            var task2 = _baseStream2.FlushAsync(cancellationToken);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override async Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
        {
            var task1 = _baseStream1.WriteAsync(buffer, offset, count, cancellationToken);
            var task2 = _baseStream1.WriteAsync(buffer, offset, count, cancellationToken);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken) => throw new NotSupportedException();
      
        public override async ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            var task1 = _baseStream1.WriteAsync(buffer, cancellationToken);
            var task2 = _baseStream1.WriteAsync(buffer, cancellationToken);
            await task1.ConfigureAwait(false);
            await task2.ConfigureAwait(false);
        }

        public override ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public override void CopyTo(Stream destination, Int32 bufferSize) => throw new NotSupportedException();

        public override void Flush()
        {
            _baseStream1.Flush();
            _baseStream2.Flush();
        }

        public override void SetLength(Int64 value) => throw new NotSupportedException();
       
        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            _baseStream1.Write(buffer, offset, count);
            _baseStream2.Write(buffer, offset, count);
        }

        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            _baseStream1.Write(buffer);
            _baseStream2.Write(buffer);
        }

        public override void WriteByte(Byte value)
        {
            _baseStream1.WriteByte(value);
            _baseStream2.WriteByte(value);
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                    {
                        _baseStream1.Dispose();
                        _baseStream2.Dispose();
                    }
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
