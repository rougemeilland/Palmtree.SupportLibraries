using System;
using System.IO;
using System.Text;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class TextWriterBySequentialOutputByteStream
        : StreamWriter
    {
        private readonly ISequentialOutputByteStream _baseStream;
        private readonly Boolean _leaveOpen;

        private Boolean _isDisposed;

        public TextWriterBySequentialOutputByteStream(ISequentialOutputByteStream baseStream, Stream rawStream, Encoding? encoding = null, Int32 bufferSize = -1, Boolean autoFlush = false, Boolean leaveOpen = false)
            : base(rawStream ?? throw new ArgumentNullException(nameof(rawStream)), encoding, bufferSize, true)
        {
            _baseStream = baseStream ?? throw new ArgumentNullException(nameof(baseStream));
            AutoFlush = autoFlush;
            _leaveOpen = leaveOpen;
            _isDisposed = false;
        }

        protected override void Dispose(Boolean disposing)
        {
            base.Dispose(disposing);

            if (!_isDisposed)
            {
                Flush();

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
