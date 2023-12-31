﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.StreamFilters
{
    internal class BufferedSequentialInputStream
        : SequentialInputByteStreamFilter
    {
        private readonly ISequentialInputByteStream _baseStream;
        private readonly ReadOnlyBytesCache<InvalidPositionType> _cache;

        public BufferedSequentialInputStream(ISequentialInputByteStream baseStream, Boolean leaveOpen)
            : this(baseStream, ReadOnlyBytesCache<InvalidPositionType>.DEFAULT_BUFFER_SIZE, leaveOpen)
        {
        }

        public BufferedSequentialInputStream(ISequentialInputByteStream baseStream, Int32 bufferSize, Boolean leaveOpen)
            : base(baseStream, leaveOpen)
        {
            try
            {
                if (baseStream is null)
                    throw new ArgumentNullException(nameof(baseStream));
                if (bufferSize <= 0)
                    throw new ArgumentOutOfRangeException(nameof(bufferSize));

                _baseStream = baseStream;
                _cache = new ReadOnlyBytesCache<InvalidPositionType>(bufferSize);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    baseStream?.Dispose();
                throw;
            }
        }

        protected override Int32 ReadCore(Span<Byte> buffer)
            => _cache.Read(
                buffer,
                b => (null, _baseStream.Read(b.Span)));

        protected override Task<Int32> ReadAsyncCore(Memory<Byte> buffer, CancellationToken cancellationToken)
            => _cache.ReadAsync(
                buffer,
                async b =>
                {
                    var length = await _baseStream.ReadAsync(b, cancellationToken).ConfigureAwait(false);
                    return (null, length);
                });
    }
}
