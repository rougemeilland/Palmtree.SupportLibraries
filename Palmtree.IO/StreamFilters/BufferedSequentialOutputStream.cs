﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.StreamFilters
{
    internal class BufferedSequentialOutputStream
        : SequentialOutputByteStreamFilter
    {
        private readonly ISequentialOutputByteStream _baseStream;
        private readonly WriteOnlyBytesCache<InvalidPositionType> _cache;
        private Boolean _isDisposed;

        public BufferedSequentialOutputStream(ISequentialOutputByteStream baseStream, Boolean leaveOpen)
            : this(baseStream, WriteOnlyBytesCache<InvalidPositionType>.DEFAULT_BUFFER_SIZE, leaveOpen)
        {
        }

        public BufferedSequentialOutputStream(ISequentialOutputByteStream baseStream, Int32 bufferSize, Boolean leaveOpen)
            : base(baseStream, leaveOpen)
        {
            try
            {
                if (baseStream is null)
                    throw new ArgumentNullException(nameof(baseStream));
                if (bufferSize <= 0)
                    throw new ArgumentOutOfRangeException(nameof(bufferSize));

                _baseStream = baseStream;
                _cache = new WriteOnlyBytesCache<InvalidPositionType>(bufferSize);
                _isDisposed = false;
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    baseStream?.Dispose();
                throw;
            }
        }

        protected override Int32 WriteCore(ReadOnlySpan<Byte> buffer)
            => _cache.Write(
                buffer,
                b =>
                {
                    _baseStream.WriteBytes(b.Span);
                    return null;
                });

        protected override Task<Int32> WriteAsyncCore(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken)
            => _cache.WriteAsync(
                buffer,
                async b =>
                {
                    await _baseStream.WriteBytesAsync(b, cancellationToken).ConfigureAwait(false);
                    return null;
                });

        protected override void FlushCore()
        {
            _cache.Flush(b =>
            {
                _baseStream.WriteBytes(b.Span);
                return null;
            });
            _baseStream.Flush();
        }

        protected override async Task FlushAsyncCore(CancellationToken cancellationToken = default)
        {
            await _cache.FlushAsync(async b =>
            {
                await _baseStream.WriteBytesAsync(b, cancellationToken).ConfigureAwait(false);
                return null;
            }).ConfigureAwait(false);
            await _baseStream.FlushAsync(cancellationToken).ConfigureAwait(false);
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    try
                    {
                        FlushCore();
                    }
                    catch (Exception)
                    {
                    }
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }

        protected override async Task DisposeAsyncCore()
        {
            if (!_isDisposed)
            {
                try
                {
                    await FlushAsyncCore(default).ConfigureAwait(false);
                }
                catch (Exception)
                {
                }

                _isDisposed = true;
            }

            await base.DisposeAsyncCore().ConfigureAwait(false);
        }
    }
}
