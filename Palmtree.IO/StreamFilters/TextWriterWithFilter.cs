using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Palmtree.IO.StreamFilters
{
    internal sealed class TextWriterWithFilter
        : TextWriter
    {
        private enum State
        {
            Unknown = 0,
            EndsWithCarriageReturn,
            NotContainNewLines,
        }

        private const Int32 _CHAR_BUFFER_CHUNK_SIZE = 32;
        private static readonly Char[] _delimiterOfTextLine = ['\r', '\n'];
        private readonly TextWriter _writer;
        private readonly Func<String, Boolean> _filter;
        private readonly Boolean _leaveOpen;
        private State _state = State.NotContainNewLines;
#if DEBUG
        private Char[] _charBuffer = new Char[4];
#else
        private Char[] _charBuffer = new Char[256];
#endif
        private Int32 _lengthOfChars;
        private Boolean _isDisposed;

        public TextWriterWithFilter(TextWriter writer, Func<String, Boolean> filter, Boolean leaveOpen)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
            _leaveOpen = leaveOpen;
        }

        public override Encoding Encoding => _writer.Encoding;
        public override IFormatProvider FormatProvider => _writer.FormatProvider;

        [AllowNull]
        public override String NewLine
        {
            get => _writer.NewLine;
            set => _writer.NewLine = value;
        }

        public override void Write(Char value)
        {
            ReadOnlySpan<Char> buffer = [value];
            Write(buffer);
        }

        public override void Write(Char[] buffer, Int32 index, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - index);

            Write(buffer.AsReadOnlySpan(index, count));
        }

        public override void Write(Char[]? buffer)
        {
            if (buffer is null)
                return;

            Write(buffer.AsReadOnlySpan());
        }

        public override void Write(ReadOnlySpan<Char> buffer)
        {
            var limit = buffer.Length;
            for (var offset = 0; offset < limit; offset += _CHAR_BUFFER_CHUNK_SIZE)
            {
                var lengthOfChunk = (limit - offset).Minimum(_CHAR_BUFFER_CHUNK_SIZE);
                AppendToCharBuffer(buffer.Slice(offset, lengthOfChunk), false);
            }
        }

        public override void Write(String? value)
        {
            if (value is null)
                return;

            var buffer = ArrayPool<Char>.Shared.Rent(value.Length);
            try
            {
                value.CopyTo(buffer);
                Write(buffer);
            }
            finally
            {
                ArrayPool<Char>.Shared.Return(buffer);
            }
        }

        public override void Flush() { }

        protected override void Dispose(Boolean disposing)
        {
            if (_isDisposed)
            {
                if (disposing)
                {
                    try
                    {
                        AppendToCharBuffer([], true);
                    }
                    catch (Exception)
                    {
                    }

                    if (!_leaveOpen)
                        _writer.Dispose();
                }

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }

        private void AppendToCharBuffer(ReadOnlySpan<Char> buffer, Boolean final)
        {
#if DEBUG
            Validation.Assert(_state is State.EndsWithCarriageReturn or State.NotContainNewLines);
#endif
            ExpandBuffer(_lengthOfChars + buffer.Length);
#if DEBUG
            Validation.Assert(_charBuffer.Length >= _lengthOfChars + buffer.Length);
#endif
            buffer.CopyTo(_charBuffer.AsSpan(_lengthOfChars, buffer.Length));
            var positionOfAppendedData = _lengthOfChars;
            _lengthOfChars += buffer.Length;
            while (true)
            {
                switch (_state)
                {
                    case State.Unknown:
                        if (UpdateState(_charBuffer.IndexOfAny(_delimiterOfTextLine, 0, _lengthOfChars), final))
                            return;
                        break;
                    case State.EndsWithCarriageReturn:
                        if (UpdateState(_charBuffer.IndexOfAny(_delimiterOfTextLine, positionOfAppendedData - 1, _lengthOfChars - positionOfAppendedData + 1), final))
                            return;
                        break;
                    case State.NotContainNewLines:
                        if (UpdateState(_charBuffer.IndexOfAny(_delimiterOfTextLine, positionOfAppendedData, _lengthOfChars - positionOfAppendedData), final))
                            return;
                        break;
                    default:
                        break;
                }
            }

            Boolean UpdateState(Int32 positionOfNewLine, Boolean final)
            {
                if (positionOfNewLine < 0)
                {
                    // 何れの改行文字も見つからない場合

                    if (final)
                    {
                        // バッファの最後までを出力する。
                        WriteLineCore(_lengthOfChars, _lengthOfChars);
#if DEBUG
                        Validation.Assert(_state == State.Unknown);
#endif
                        return false;
                    }
                    else
                    {
                        _state = State.NotContainNewLines;
#if DEBUG
                        Validation.Assert(_state != State.Unknown);
#endif
                        return true;
                    }
                }

                // 何れかの改行文字が見つかった場合
                if (_charBuffer[positionOfNewLine] == '\n')
                {
                    // 見つかった改行文字が '\n' である場合

                    // '\n' までを出力する。
                    WriteLineCore(positionOfNewLine + 1, positionOfNewLine);
#if DEBUG
                    Validation.Assert(_state == State.Unknown);
#endif
                    return false;
                }

                // 見つかった改行文字が '\r' である場合
                if (positionOfNewLine + 1 >= _lengthOfChars)
                {
                    // '\r' が見つかった位置がデータの終端である場合

#if DEBUG
                    Validation.Assert(positionOfAppendedData + 1 == _lengthOfChars);
                    Validation.Assert(_charBuffer[_lengthOfChars - 1] == '\r');
#endif

                    if (final)
                    {
                        // バッファの最後までを出力する。
                        WriteLineCore(_lengthOfChars, _lengthOfChars - 1);
#if DEBUG
                        Validation.Assert(_state == State.Unknown);
#endif
                        return false;
                    }
                    else
                    {
                        _state = State.EndsWithCarriageReturn;
#if DEBUG
                        Validation.Assert(_state != State.Unknown);
#endif
                        return true;
                    }
                }

                // '\r' が見つかった位置がデータの終端ではない場合
                if (_charBuffer[positionOfNewLine + 1] == '\n')
                {
                    // '\r' の次が '\n' である場合

                    // '\n' までを出力する。
                    WriteLineCore(positionOfNewLine + 2, positionOfNewLine);
#if DEBUG
                    Validation.Assert(_state == State.Unknown);
#endif
                    return false;
                }

                // '\r' の次が '\n' ではない場合

                // '\r' までを出力する。
                WriteLineCore(positionOfNewLine + 1, positionOfNewLine);
#if DEBUG
                Validation.Assert(_state == State.Unknown);
#endif
                return false;
            }
        }

        private void ExpandBuffer(Int32 length)
        {
            while (_charBuffer.Length < length)
                Array.Resize(ref _charBuffer, _charBuffer.Length << 1);
        }

        private void WriteLineCore(Int32 lengthOfTextLineWithNewLine, Int32 lengthOfTextLineWithoutNewLine)
        {
#if DEBUG
            Validation.Assert(_lengthOfChars <= _charBuffer.Length);
            Validation.Assert(lengthOfTextLineWithNewLine <= _lengthOfChars);
            Validation.Assert(lengthOfTextLineWithoutNewLine <= lengthOfTextLineWithNewLine);
#endif
            if (_filter(new String(_charBuffer.AsReadOnlySpan(0, lengthOfTextLineWithoutNewLine))))
                _writer.Write(_charBuffer.AsReadOnlySpan(0, lengthOfTextLineWithNewLine));
            _charBuffer.AsReadOnlySpan(lengthOfTextLineWithNewLine, _lengthOfChars - lengthOfTextLineWithNewLine).CopyTo(_charBuffer);
            _lengthOfChars -= lengthOfTextLineWithNewLine;
            _state = State.Unknown;
        }
    }
}
