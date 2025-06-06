using System;
using System.IO;
using System.Threading;

namespace Palmtree.IO
{
    internal sealed class TextWriterValidationLogger
        : IValidationLogger, IDisposable
    {
        private readonly TextWriter _writer;
        private readonly Int32 _indentSize;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;
        private Int32 _indentLevel;
        private Boolean _needIndent;

        public TextWriterValidationLogger(TextWriter writer, Int32 indentSize, Boolean leaveOpen)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(indentSize);

            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _indentSize = indentSize;
            _leaveOpen = leaveOpen;
            _isDisposed = false;
            _indentLevel = 0;
            _needIndent = true;
        }

        public void Indent()
        {
            lock (this)
            {
                _ = Interlocked.Increment(ref _indentLevel);
            }
        }

        public void Unindent()
        {
            lock (this)
            {
                if (Interlocked.Decrement(ref _indentLevel) < 0)
                {
                    _ = Interlocked.Exchange(ref _indentLevel, 0);
                    throw new InvalidOperationException("The UnIndent() method is called more times than the Indent() method.");
                }
            }
        }

        public void Write(String message)
        {
            lock (this)
            {
                WriteIndent();
                _writer.Write(message);
            }
        }

        public void WriteLine()
        {
            lock (this)
            {
                _writer.WriteLine();
                _needIndent = true;
            }
        }

        public void WriteLine(String message)
        {
            lock (this)
            {
                WriteIndent();
                _writer.WriteLine(message);
                _needIndent = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void WriteIndent()
        {
            if (_needIndent)
            {
                var spaces = _indentSize * _indentLevel;
                while (spaces >= 4)
                {
                    _writer.Write("    ");
                    spaces -= 4;
                }

                while (spaces > 0)
                {
                    _writer.Write(" ");
                    --spaces;
                }

                _needIndent = false;
            }
        }

        private void Dispose(Boolean disposing)
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
        }
    }
}
