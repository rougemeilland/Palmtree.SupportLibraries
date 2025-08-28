using System;
using System.IO;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class DotNetStreamByStreamWriter
        : Stream, IDirectDotNetStreamWrapper
    {
        private readonly StreamWriter _writer;
        private readonly Stream _baseStream;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;

        public DotNetStreamByStreamWriter(StreamWriter writer, Boolean leaveOpen)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _leaveOpen = leaveOpen;
            _baseStream = writer.BaseStream;
            while (_baseStream is IDirectDotNetStreamWrapper wrapper)
                _baseStream = wrapper.RawStream;
            _writer.Flush();
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

        public override void SetLength(Int64 value) => throw new NotSupportedException();
        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count) => throw new NotSupportedException();
        public override Int64 Seek(Int64 offset, SeekOrigin origin) => throw new NotSupportedException();

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            _baseStream.Write(buffer, offset, count);
        }

        public override void Flush()
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            _baseStream.Flush();
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                        _writer.Dispose();
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }

        Stream IDirectDotNetStreamWrapper.RawStream => _baseStream;
    }
}
