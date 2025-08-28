using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class DotNetStreamByTextWriter
        : Stream
    {
        private const Int32 _CHAR_BUFFER_SIZE = 1024;
        private const Int32 _BYTE_BUFFER_CHUNK_SIZE = 512;

        private readonly TextWriter _writer;
        private readonly Decoder _decoder;
        private readonly Boolean _leaveOpen;
        private readonly SemaphoreSlim _lockObject = new(1);
        private Boolean _isDisposed;

        public DotNetStreamByTextWriter(TextWriter writer, Encoding encoding, Boolean leaveOpen)
        {
            _writer = writer;
            _decoder = encoding.GetDecoder();
            _leaveOpen = leaveOpen;
        }

        public override Boolean CanRead => false;
        public override Boolean CanSeek => false;
        public override Boolean CanWrite => true;
        public override Int64 Length => throw new NotSupportedException();

        public override Int64 Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count) => throw new NotSupportedException();
        public override Int64 Seek(Int64 offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(Int64 value) => throw new NotSupportedException();

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
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
                var charBuffer = ArrayPool<Char>.Shared.Rent(_CHAR_BUFFER_SIZE);
                try
                {
                    while (count > 0)
                    {
                        var lengthOfBytes = count.Minimum(_BYTE_BUFFER_CHUNK_SIZE);
                        var lengthOfChars = _decoder.GetChars(buffer.AsReadOnlySpan(offset, lengthOfBytes), charBuffer, false);
                        _writer.Write(charBuffer.AsReadOnlySpan(0, lengthOfChars));
                        offset += lengthOfBytes;
                        count -= lengthOfBytes;
                    }
                }
                finally
                {
                    ArrayPool<Char>.Shared.Return(charBuffer);
                }
            }
            finally
            {
                _ = _lockObject.Release();
            }
        }

        public override void Flush() { }

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    var charBuffer = ArrayPool<Char>.Shared.Rent(_CHAR_BUFFER_SIZE);
                    try
                    {
                        var lengthOfChars = _decoder.GetChars([], charBuffer, true);
                        _writer.Write(charBuffer.AsReadOnlySpan(0, lengthOfChars));
                    }
                    finally
                    {
                        ArrayPool<Char>.Shared.Return(charBuffer);
                    }

                    if (!_leaveOpen)
                        _writer.Dispose();
                    _lockObject.Dispose();
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
