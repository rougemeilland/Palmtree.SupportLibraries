using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class DotNetStreamByTextReader
        : Stream
    {
        private sealed class CharBuffer
        {
            private readonly TextReader _reader;
            private readonly Encoder _encoder;
            private readonly Char[] _buffer;
            private Boolean _isEndOfReader;

            public CharBuffer(TextReader reader, Encoder encoder, Int32 charBufferSize)
            {
                _reader = reader;
                _encoder = encoder;
                _buffer = new Char[charBufferSize];
            }

            public Int32 Read(Span<Byte> buffer)
            {
                if (_isEndOfReader)
                    return 0;
                var lengthOfData = _reader.Read(_buffer);
                if (lengthOfData <= 0)
                {
                    _isEndOfReader = true;
#if DEBUG
                    Validation.Assert(buffer.Length >= _encoder.GetByteCount([], true));
#endif
                    return _encoder.GetBytes([], buffer, true);
                }

                return _encoder.GetBytes(_buffer.AsReadOnlySpan(0, lengthOfData), buffer, false);
            }
        }

        private sealed class ByteBuffer
        {
            private readonly CharBuffer _charBuffer;
            private readonly Byte[] _byteBuffer;
            private Boolean _isClosed;
            private Int32 _offsetOfData;
            private Int32 _lengthOfData;

            public ByteBuffer(TextReader reader, Encoder encoder, Int32 charBufferSize, Int32 byteBufferSize)
            {
                _charBuffer = new CharBuffer(reader, encoder, charBufferSize);
                _byteBuffer = new Byte[byteBufferSize];
            }

            public Int32 Read(Span<Byte> buffer)
            {
                if (_isClosed)
                    return 0;
                if (_lengthOfData <= 0)
                {
                    _offsetOfData = 0;
                    _lengthOfData = _charBuffer.Read(_byteBuffer);
                    if (_lengthOfData <= 0)
                    {
                        _isClosed = true;
                        return 0;
                    }
                }

                var lengthToCopy = buffer.Length.Minimum(_lengthOfData);
                _byteBuffer.AsReadOnlySpan(_offsetOfData, lengthToCopy).CopyTo(buffer);
                _offsetOfData += lengthToCopy;
                _lengthOfData = lengthToCopy;
                return lengthToCopy;
            }
        }

#if DEBUG
        private const Int32 _CHAR_BUFFER_SIZE = 4;
        private const Int32 _BYTE_BUFFER_SIZE = 16;
#else
        private const Int32 _CHAR_BUFFER_SIZE = 256;
        private const Int32 _BYTE_BUFFER_SIZE = 1024;
#endif
        private readonly TextReader _reader;
        private readonly Boolean _leaveOpen;
        private readonly SemaphoreSlim _lockObject = new(1);
        private readonly ByteBuffer _byteBuffer;
        private Boolean _isDisposed;

        public DotNetStreamByTextReader(TextReader reader, Encoding encoding, Boolean leaveOpen)
        {
            ArgumentNullException.ThrowIfNull(encoding);

#if DEBUG
            Validation.Assert(_BYTE_BUFFER_SIZE >= encoding.GetMaxByteCount(1) * _CHAR_BUFFER_SIZE);
#endif
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _leaveOpen = leaveOpen;
            _byteBuffer = new ByteBuffer(reader, encoding.GetEncoder(), _CHAR_BUFFER_SIZE, _BYTE_BUFFER_SIZE);
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

            _lockObject.Wait();
            try
            {
                return _byteBuffer.Read(buffer.AsSpan(offset, count));
            }
            finally
            {
                _ = _lockObject.Release();
            }
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            _lockObject.Wait();
            try
            {
                return _byteBuffer.Read(buffer);
            }
            finally
            {
                _ = _lockObject.Release();
            }
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
                    _lockObject.Dispose();
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
