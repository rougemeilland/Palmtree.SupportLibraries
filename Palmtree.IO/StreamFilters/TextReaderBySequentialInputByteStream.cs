using System;
using System.IO;
using System.Text;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class TextReaderBySequentialInputByteStream
        : StreamReader
    {
        private readonly ISequentialInputByteStream _baseStream;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;

        public TextReaderBySequentialInputByteStream(ISequentialInputByteStream baseStream, Stream rawStream, Encoding? encoding = null, Boolean detectEncodingFromByteOrderMarks = true, Int32 bufferSize = -1, Boolean leaveOpen = false)
            : base(rawStream, encoding, detectEncodingFromByteOrderMarks, bufferSize, true)
        {
            ArgumentNullException.ThrowIfNull(baseStream);

            _baseStream = baseStream;
            _leaveOpen = leaveOpen;
            _isDisposed = false;
        }

        protected override void Dispose(Boolean disposing)
        {
            base.Dispose(disposing);

            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                        _baseStream.Dispose();
                }

                _isDisposed = true;
            }
        }
    }
}
