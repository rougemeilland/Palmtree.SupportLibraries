using System;
using System.IO;
using System.Threading;

namespace Palmtree.IO
{
    internal sealed class TextWriterValidationLogger
        : IDisposableValidationLogger
    {
        private readonly TextWriter _writer;
        private readonly Int32 _indentSize;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;
        private Int32 _indentLevel;

        public TextWriterValidationLogger(TextWriter writer, Int32 indentSize, Boolean leaveOpen)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(indentSize);

            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _indentSize = indentSize;
            _leaveOpen = leaveOpen;
            _isDisposed = false;
            _indentLevel = 0;
        }

        public void Indent()
            => _ = Interlocked.Increment(ref _indentLevel);

        public void Unindent()
        {
            if (Interlocked.Decrement(ref _indentLevel) < 0)
            {
                _ = Interlocked.Exchange(ref _indentLevel, 0);
                throw new InvalidOperationException("The UnIndent() method is called more times than the Indent() method.");
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

        public void WriteLog()
        {
            lock (this)
            {
                _writer.WriteLine();
            }
        }

        public void WriteLog(String? prefix, String message)
        {
            lock (this)
            {
                if (prefix is null)
                {
                    _writer.WriteLine(message);
                }
                else
                {
                    _writer.Write(prefix);
                    WriteIndent();
                    _writer.WriteLine(message);
                }
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void WriteIndent()
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
