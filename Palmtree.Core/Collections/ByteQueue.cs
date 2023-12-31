using System;
using Palmtree;

namespace Palmtree.Collections
{
    public class ByteQueue
    {
#if DEBUG
        private const Int32 _MINIMUM_BUFFER_SIZE = 4;
#else
        private const Int32 _MINIMUM_BUFFER_SIZE = 1024;
#endif
        private const Int32 _DEFAULT_BUFFER_SIZE = 64 * 1024;
        private const Int32 _MAXIMUM_BUFFER_SIZE = 1024 * 1024;

        private readonly Byte[] _internalBuffer;
        private Int32 _startOfDataInInternalBuffer;

        public ByteQueue(Int32 bufferSize = _DEFAULT_BUFFER_SIZE)
        {
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            _internalBuffer = new Byte[bufferSize.Maximum(_MINIMUM_BUFFER_SIZE).Minimum(_MAXIMUM_BUFFER_SIZE)];
            _startOfDataInInternalBuffer = 0;
            AvailableDataCount = 0;
            IsCompleted = false;
        }

        public Boolean IsCompleted { get; private set; }
        public Boolean IsEmpty => AvailableDataCount <= 0;
        public Boolean IsFull => AvailableDataCount >= _internalBuffer.Length;
        public Int32 AvailableDataCount { get; private set; }
        public Int32 FreeAreaCount => _internalBuffer.Length - AvailableDataCount;
        public Int32 BufferSize => _internalBuffer.Length;

        public Int32 Read(Span<Byte> buffer)
        {
            lock (this)
            {
                var actualCount =
                    buffer.Length
                    .Minimum(AvailableDataCount)
                    .Minimum(_internalBuffer.Length - _startOfDataInInternalBuffer);
                if (actualCount <= 0)
                {
                    Validation.Assert(buffer.Length <= 0 || IsCompleted, "buffer.Length <= 0 || IsCompleted");
                    return 0;
                }

                _internalBuffer.AsSpan(_startOfDataInInternalBuffer, actualCount).CopyTo(buffer[..actualCount]);
                _startOfDataInInternalBuffer += actualCount;
                AvailableDataCount -= actualCount;
                if (_startOfDataInInternalBuffer >= _internalBuffer.Length)
                    _startOfDataInInternalBuffer = 0;
#if DEBUG
                if (!_startOfDataInInternalBuffer.InRange(0, _internalBuffer.Length))
                    throw new Exception();
                if (!AvailableDataCount.IsBetween(0, _internalBuffer.Length - actualCount))
                    throw new Exception();
#endif
                return actualCount;
            }
        }

        public Int32 Write(ReadOnlySpan<Byte> buffer)
        {
            lock (this)
            {
                if (IsCompleted)
                    throw new InvalidOperationException("Can not write any more.");

                var endOfAvailableData = _startOfDataInInternalBuffer + AvailableDataCount;
                var (offsetOnInternalBuffer, actualCount) =
                    endOfAvailableData >= _internalBuffer.Length
                    ? (endOfAvailableData - _internalBuffer.Length, buffer.Length.Minimum(_internalBuffer.Length - AvailableDataCount))
                    : (endOfAvailableData, buffer.Length.Minimum(_internalBuffer.Length - endOfAvailableData));

                buffer[..actualCount].CopyTo(_internalBuffer.AsSpan(offsetOnInternalBuffer, actualCount));
                AvailableDataCount += actualCount;
#if DEBUG
                if (!_startOfDataInInternalBuffer.InRange(0, _internalBuffer.Length))
                    throw new Exception();
                if (!AvailableDataCount.IsBetween(0, _internalBuffer.Length))
                    throw new Exception();
#endif
                return actualCount;
            }
        }

        public void Compete()
        {
            lock (this)
            {
                IsCompleted = true;
            }
        }
    }
}
