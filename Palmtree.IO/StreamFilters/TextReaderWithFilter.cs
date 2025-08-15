using System;
using System.IO;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class TextReaderWithFilter
        : TextReader
    {
        private readonly TextReader _reader;
        private readonly Func<String, Boolean> _filter;
        private readonly Boolean _leaveOpen;
        private readonly TextLineReader _textLineReader;
#if DEBUG
        private Char[] _buffer = new Char[2];
#else
        private Char[] _buffer = new Char[256];
#endif
        private Int32 _offsetOfData;
        private Int32 _lengthOfData;
        private Boolean _isDisposed;

        public TextReaderWithFilter(TextReader reader, Func<String, Boolean> filter, Boolean leaveOpen)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
            _textLineReader = new TextLineReader(reader);
            _leaveOpen = leaveOpen;
        }

        public override Int32 Peek()
        {
            if (!FillBuffer())
                return -1;
#if DEBUG
            Validation.Assert(_offsetOfData < _buffer.Length);
            Validation.Assert(_lengthOfData > 0);
#endif
            return _buffer[_offsetOfData];
        }

        public override Int32 Read()
        {
            if (!FillBuffer())
                return -1;
#if DEBUG
            Validation.Assert(_offsetOfData < _buffer.Length);
            Validation.Assert(_lengthOfData > 0);
#endif
            var c = _buffer[_offsetOfData];
            ++_offsetOfData;
            --_lengthOfData;
            return c;
        }

        public override Int32 Read(Char[] buffer, Int32 index, Int32 count)
            => Read(buffer.AsSpan(index, count));

        public override Int32 Read(Span<Char> buffer)
        {
            if (!FillBuffer())
                return -1;
#if DEBUG
            Validation.Assert(_offsetOfData < _buffer.Length);
            Validation.Assert(_lengthOfData > 0);
#endif

            var lengthToRead = buffer.Length.Minimum(_lengthOfData);
            _buffer.AsReadOnlySpan(_offsetOfData, lengthToRead).CopyTo(buffer);
            _offsetOfData += lengthToRead;
            _lengthOfData -= lengthToRead;
            return lengthToRead;
        }

        protected override void Dispose(Boolean disposing)
        {
            if (_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                        _reader.Dispose();
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }

        private Boolean FillBuffer()
        {
            while (_lengthOfData <= 0)
            {
                var result = _textLineReader.ReadLine();
                try
                {
                    if (result is null)
                        return false;
                    if (_filter(new String(result.Value.textLineWithoutNewLineChars.Span)))
                    {
                        var sourceChars = result.Value.textLineWithNewLineChars;
                        while (_buffer.Length < sourceChars.Length)
                            Array.Resize(ref _buffer, _buffer.Length << 1);
#if DEBUG
                        Validation.Assert(_buffer.Length >= sourceChars.Length);
#endif
                        sourceChars.Span.CopyTo(_buffer);
                        _offsetOfData = 0;
                        _lengthOfData = sourceChars.Length;
                    }
                }
                finally
                {
                    _textLineReader.CleanUp();
                }
            }

            return true;
        }
    }
}
