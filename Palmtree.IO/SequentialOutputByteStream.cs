using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO
{
    public abstract class SequentialOutputByteStream
        : ISequentialOutputByteStream
    {
        private Boolean _isDisposed;

        protected SequentialOutputByteStream()
        {
            _isDisposed = false;
        }

        ~SequentialOutputByteStream()
        {
            Dispose(disposing: false);
        }

        public Int32 Write(ReadOnlySpan<Byte> buffer)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            var length = WriteCore(buffer);
            if (buffer.IsEmpty && length <= 0)
                throw new IOException("Can not write any more");
            return length;
        }

        public async Task<Int32> WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            var length = await WriteAsyncCore(buffer, cancellationToken).ConfigureAwait(false);
            if (buffer.IsEmpty && length <= 0)
                throw new IOException("Can not write any more");
            return length;
        }

        public void Flush()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            FlushCore();
        }

        public Task FlushAsync(CancellationToken cancellationToken = default)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            return FlushAsyncCore(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        protected abstract Int32 WriteCore(ReadOnlySpan<Byte> buffer);
        protected abstract Task<Int32> WriteAsyncCore(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken);
        protected abstract void FlushCore();
        protected abstract Task FlushAsyncCore(CancellationToken cancellationToken = default);

        protected virtual void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                }

                _isDisposed = true;
            }
        }

        protected virtual Task DisposeAsyncCore()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
            }

            return Task.CompletedTask;
        }
    }
}
