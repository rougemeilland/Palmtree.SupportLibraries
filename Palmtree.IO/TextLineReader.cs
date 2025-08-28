using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO
{
    public class TextLineReader
    {
        private enum State
        {
            Unknown = 0,
            Returned,
            ReturnedLastLine,
            EndsWithCarriageReturn,
            NotContainNewLines,
            ReaderIsClosed,
        }

        private static readonly Char[] _delimiterOfTextLine = ['\r', '\n'];
        private readonly TextReader _reader;
        private State _state;
#if DEBUG
        private Char[] _buffer = new Char[4];
#else
        private Char[] _buffer = new Char[128];
#endif
        private Int32 _lengthOfBuffer;
        private Int32 _lengthOfTextLineWithNewLine;

        public TextLineReader(TextReader reader)
        {
            _reader = reader;
            Validation.Assert(_state == State.Unknown);
        }

        public (ReadOnlyMemory<Char> textLineWithNewLineChars, ReadOnlyMemory<Char> textLineWithoutNewLineChars, Boolean isLastLine)? ReadLine()
        {
            while (true)
            {
#if DEBUG
                Validation.Assert(_lengthOfBuffer < _buffer.Length);
                Validation.Assert(_lengthOfTextLineWithNewLine <= _lengthOfBuffer);
#endif
                switch (_state)
                {
                    case State.Unknown:
                    {
                        if (_lengthOfBuffer <= 0)
                        {
                            var length = _reader.Read(_buffer, 0, _buffer.Length);
                            if (length <= 0)
                            {
                                _state = State.ReaderIsClosed;
                                return null;
                            }

                            _lengthOfBuffer = length;
                        }

                        var pos = _buffer.IndexOfAny(_delimiterOfTextLine, 0, _lengthOfBuffer);
                        var result = CutOutOneLine(pos);
                        if (result is not null)
                            return (result.Value.textLineWithNewLineChars, result.Value.textLineWithoutNewLineChars, false);
                        break;
                    }
                    case State.Returned:
                    case State.ReturnedLastLine:
                    {
#if DEBUG
                        Validation.Assert(_lengthOfBuffer > 0);
                        Validation.Assert(_lengthOfTextLineWithNewLine > 0);
#endif
                        _buffer.AsSpan(_lengthOfTextLineWithNewLine, _lengthOfBuffer - _lengthOfTextLineWithNewLine).CopyTo(_buffer.AsSpan());
                        _lengthOfBuffer -= _lengthOfTextLineWithNewLine;
                        _lengthOfTextLineWithNewLine = 0;
                        _state =
                            _state == State.ReturnedLastLine
                            ? State.ReaderIsClosed
                            : State.Unknown;
                        break;
                    }
                    case State.EndsWithCarriageReturn:
                    {
#if DEBUG
                        Validation.Assert(_lengthOfBuffer > 0);
                        Validation.Assert(_buffer[_lengthOfBuffer - 1] == '\r');
#endif
                        var positionOfCarriageReturn = _lengthOfBuffer - 1;
                        ExpandBuffer();
#if DEBUG
                        Validation.Assert(_buffer.Length > _lengthOfBuffer);
#endif
                        var length = _reader.Read(_buffer, _lengthOfBuffer, _buffer.Length - _lengthOfBuffer);
                        if (length <= 0)
                        {
                            _state = State.ReturnedLastLine;
                            return (_buffer.AsReadOnlyMemory(0, positionOfCarriageReturn + 1), _buffer.AsReadOnlyMemory(0, positionOfCarriageReturn), true);
                        }

                        _lengthOfBuffer += length;
                        var pos = _buffer.IndexOfAny(_delimiterOfTextLine, positionOfCarriageReturn, _lengthOfBuffer - positionOfCarriageReturn);
                        var result = CutOutOneLine(pos);
                        if (result is not null)
                            return (result.Value.textLineWithNewLineChars, result.Value.textLineWithoutNewLineChars, false);
                        break;
                    }
                    case State.NotContainNewLines:
                    {
#if DEBUG
                        Validation.Assert(_lengthOfBuffer > 0);
#endif
                        ExpandBuffer();
#if DEBUG
                        Validation.Assert(_buffer.Length > _lengthOfBuffer);
#endif
                        var length = _reader.Read(_buffer, _lengthOfBuffer, _buffer.Length - _lengthOfBuffer);
                        if (length <= 0)
                        {
                            _state = State.ReturnedLastLine;
                            return (_buffer.AsReadOnlyMemory(0, _lengthOfBuffer), _buffer.AsReadOnlyMemory(0, _lengthOfBuffer), true);
                        }

                        var pos = _buffer.IndexOfAny(_delimiterOfTextLine, _lengthOfBuffer, length);
                        _lengthOfBuffer += length;
                        var result = CutOutOneLine(pos);
                        if (result is not null)
                            return (result.Value.textLineWithNewLineChars, result.Value.textLineWithoutNewLineChars, false);
                        break;
                    }
                    case State.ReaderIsClosed:
                    {
#if DEBUG
                        Validation.Assert(_lengthOfBuffer <= 0);
                        Validation.Assert(_lengthOfTextLineWithNewLine <= 0);
#endif
                        return null;
                    }
                    default:
                        throw Validation.GetFatalErrorException();
                }
            }
        }

        public async Task<(ReadOnlyMemory<Char> textLineWithNewLineChars, ReadOnlyMemory<Char> textLineWithoutNewLineChars, Boolean isLastLine)?> ReadLineAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
#if DEBUG
                Validation.Assert(_lengthOfBuffer < _buffer.Length);
                Validation.Assert(_lengthOfTextLineWithNewLine <= _lengthOfBuffer);
#endif
                switch (_state)
                {
                    case State.Unknown:
                    {
                        if (_lengthOfBuffer <= 0)
                        {
                            var length = await _reader.ReadAsync(_buffer.AsMemory(0, _buffer.Length), cancellationToken).ConfigureAwait(false);
                            if (length <= 0)
                            {
                                _state = State.ReaderIsClosed;
                                return null;
                            }

                            _lengthOfBuffer = length;
                        }

                        var pos = _buffer.IndexOfAny(_delimiterOfTextLine, 0, _lengthOfBuffer);
                        var result = CutOutOneLine(pos);
                        if (result is not null)
                            return (result.Value.textLineWithNewLineChars, result.Value.textLineWithoutNewLineChars, false);
                        break;
                    }
                    case State.EndsWithCarriageReturn:
                    {
#if DEBUG
                        Validation.Assert(_lengthOfBuffer > 0);
                        Validation.Assert(_buffer[_lengthOfBuffer - 1] == '\r');
#endif
                        var positionOfCarriageReturn = _lengthOfBuffer - 1;
                        ExpandBuffer();
#if DEBUG
                        Validation.Assert(_buffer.Length > _lengthOfBuffer);
#endif
                        var length = await _reader.ReadAsync(_buffer.AsMemory(_lengthOfBuffer, _buffer.Length - _lengthOfBuffer), cancellationToken).ConfigureAwait(false);
                        if (length <= 0)
                        {
                            _state = State.ReturnedLastLine;
                            return (_buffer.AsReadOnlyMemory(0, positionOfCarriageReturn + 1), _buffer.AsReadOnlyMemory(0, positionOfCarriageReturn), true);
                        }

                        _lengthOfBuffer += length;
                        var pos = _buffer.IndexOfAny(_delimiterOfTextLine, positionOfCarriageReturn, _lengthOfBuffer - positionOfCarriageReturn);
                        var result = CutOutOneLine(pos);
                        if (result is not null)
                            return (result.Value.textLineWithNewLineChars, result.Value.textLineWithoutNewLineChars, false);
                        break;
                    }
                    case State.NotContainNewLines:
                    {
                        ExpandBuffer();
#if DEBUG
                        Validation.Assert(_buffer.Length > _lengthOfBuffer);
#endif
                        var length = await _reader.ReadAsync(_buffer.AsMemory(_lengthOfBuffer, _buffer.Length - _lengthOfBuffer), cancellationToken).ConfigureAwait(false);
                        if (length <= 0)
                        {
                            _state = State.ReturnedLastLine;
                            return (_buffer.AsReadOnlyMemory(0, _lengthOfBuffer), _buffer.AsReadOnlyMemory(0, _lengthOfBuffer), true);
                        }

                        var pos = _buffer.IndexOfAny(_delimiterOfTextLine, _lengthOfBuffer, length);
                        _lengthOfBuffer += length;
                        var result = CutOutOneLine(pos);
                        if (result is not null)
                            return (result.Value.textLineWithNewLineChars, result.Value.textLineWithoutNewLineChars, false);
                        break;
                    }
                    case State.ReaderIsClosed:
                        return null;
                    default:
                        throw Validation.GetFatalErrorException();
                }
            }
        }

        public void CleanUp()
        {
#if DEBUG
            Validation.Assert(_state is State.Returned or State.ReturnedLastLine or State.ReaderIsClosed);
            Validation.Assert(_lengthOfBuffer > 0);
            Validation.Assert(_buffer[_lengthOfBuffer - 1] == '\r');
#endif
            if (_state == State.ReaderIsClosed)
                return;
            _buffer.AsSpan(_lengthOfTextLineWithNewLine, _lengthOfBuffer - _lengthOfTextLineWithNewLine).CopyTo(_buffer.AsSpan());
            _lengthOfBuffer -= _lengthOfTextLineWithNewLine;
            _lengthOfTextLineWithNewLine = 0;
            _state =
                _state == State.ReturnedLastLine
                ? State.ReaderIsClosed
                : State.Unknown;
        }

        private void ExpandBuffer()
        {
            while (_buffer.Length <= _lengthOfBuffer)
                Array.Resize(ref _buffer, _buffer.Length << 1);
        }

        private (ReadOnlyMemory<Char> textLineWithNewLineChars, ReadOnlyMemory<Char> textLineWithoutNewLineChars)? CutOutOneLine(Int32 positionOfDelimiter)
        {
            if (positionOfDelimiter < 0)
            {
                // 改行文字が見つからなかった場合
                _state = State.NotContainNewLines;
                return null;
            }

            // 改行文字が見つかった場合
            if (_buffer[positionOfDelimiter] == '\n')
            {
                // 見つかった改行文字が '\n' である場合
                _state = State.Returned;
                _lengthOfTextLineWithNewLine = positionOfDelimiter + 1;
                return (_buffer.AsReadOnlyMemory(0, positionOfDelimiter + 1), _buffer.AsReadOnlyMemory(0, positionOfDelimiter));
            }

            // 見つかった改行文字が '\r' である場合
            if (positionOfDelimiter + 1 >= _lengthOfBuffer)
            {
                // '\r' が見つかった位置がデータの終端である場合
                _state = State.EndsWithCarriageReturn;
                return null;
            }

            // '\r' が見つかった位置がデータの終端ではない場合
            if (_buffer[positionOfDelimiter + 1] == '\n')
            {
                // '\r' の次が '\n' である場合
                _state = State.Returned;
                _lengthOfTextLineWithNewLine = positionOfDelimiter + 2;
                return (_buffer.AsReadOnlyMemory(0, positionOfDelimiter + 2), _buffer.AsReadOnlyMemory(0, positionOfDelimiter));
            }

            // '\r' の次が '\n' ではない場合
            _state = State.Returned;
            _lengthOfTextLineWithNewLine = positionOfDelimiter + 1;
            return (_buffer.AsReadOnlyMemory(0, positionOfDelimiter + 1), _buffer.AsReadOnlyMemory(0, positionOfDelimiter));
        }
    }
}
