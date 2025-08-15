using System;
using System.IO;

namespace Palmtree.IO
{
    internal sealed class TextWriterValidationLogger
        : ValidationLogger, IDisposableValidationLogger
    {
        private sealed class TextWriterValidationLoggerSource
            : ValidationLoggerSource
        {
            private readonly TextWriter _writer;

            public TextWriterValidationLoggerSource(TextWriter writer)
            {
                _writer = writer;
            }

            public override void WriteLine() => _writer.WriteLine();
            protected override void Write(String s) => _writer.Write(s);

        }

        private readonly TextWriter _writer;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;

        public TextWriterValidationLogger(TextWriter writer, Boolean leaveOpen)
            : base(new TextWriterValidationLoggerSource(writer))
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _leaveOpen = leaveOpen;
            _isDisposed = false;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
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
