using System;
using System.Diagnostics;
using System.Text;
using Palmtree.IO.Console;

namespace Palmtree.Application
{
    public abstract class ApplicationBase
        : IDisposable
    {
        protected enum ResultCode
        {
            Success = 0,
            Failed = 1,
            Cancelled = -1,
        }

        private const Int32 _INDENT_STEP = 2;

        private readonly String _thisProgramName;

        private Boolean _isDisposed;
        private Boolean _isPressedBreak;

        protected ApplicationBase()
        {
            _isDisposed = false;
            _thisProgramName = GetType().Assembly.GetAssemblyFileNameWithoutExtension();
            if (DelayBreak)
                TinyConsole.CancelKeyPress += TinyConsole_CancelKeyPress;
            var encoding = InputOutputEncoding;
            if (encoding is not null)
            {
                TinyConsole.InputEncoding = encoding;
                TinyConsole.OutputEncoding = encoding;
            }
        }

        ~ApplicationBase()
        {
            Dispose(disposing: false);
        }

        public virtual Int32 Run(String[] args)
        {
            ResultCode result;
            try
            {
                TinyConsole.CursorVisible = ConsoleCursorVisiblity.Invisible;
                TinyConsole.Title = ConsoleWindowTitle;
                result = Main(args);
            }
            finally
            {
                TinyConsole.CursorVisible = ConsoleCursorVisiblity.NormalMode;
            }

            CleanUp(result);
            Finish(result);
            return (Int32)result;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual Boolean DelayBreak => true;
        protected virtual Encoding? InputOutputEncoding => null;
        protected virtual String ConsoleWindowTitle => _thisProgramName;

        protected abstract ResultCode Main(String[] args);

        protected virtual void CleanUp(ResultCode result)
        {
        }

        protected virtual void Finish(ResultCode result)
        {
        }

        protected Boolean IsPressedBreak
        {
            get
            {
                lock (this)
                {
                    return _isPressedBreak;
                }
            }
        }

        protected void ReportException(Exception exception)
        {
            lock (this)
            {
                try
                {
                    InternalReportException(exception, 0);
                }
                finally
                {
                    TinyConsole.ResetColor();
                }
            }
        }

        protected void ReportInformationMessage(String message)
        {
            lock (this)
            {
                try
                {
                    InternalReportInformationMessage(message);
                }
                finally
                {
                    TinyConsole.ResetColor();
                }
            }
        }

        protected void ReportWarningMessage(String message)
        {
            lock (this)
            {
                try
                {
                    InternalReportWarningMessage(message);
                }
                finally
                {
                    TinyConsole.ResetColor();
                }
            }
        }

        protected void ReportErrorMessage(String message)
        {
            lock (this)
            {
                try
                {
                    InternalReportErrorMessage(message, 0);
                }
                finally
                {
                    TinyConsole.ResetColor();
                }
            }
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                }

                TinyConsole.CancelKeyPress -= TinyConsole_CancelKeyPress;

                _isDisposed = true;
            }
        }

        private void TinyConsole_CancelKeyPress(Object? sender, ConsoleCancelEventArgs e)
        {
            lock (this)
            {
                _isPressedBreak = true;
            }

            e.Cancel = true;
        }

        private void InternalReportException(Exception exception, Int32 indent)
        {
            InternalReportErrorMessage(exception.Message, indent);
            if (exception.InnerException is not null)
                InternalReportException(exception.InnerException, indent + _INDENT_STEP);
            if (exception is AggregateException aggregateException)
            {
                foreach (var ex in aggregateException.InnerExceptions)
                    InternalReportException(ex, indent + _INDENT_STEP);
            }
        }

        private void InternalReportInformationMessage(String message)
        {
            TinyConsole.Erase(ConsoleEraseMode.FromCursorToEndOfScreen);
            TinyConsole.ForegroundColor = ConsoleColor.Yellow;
            TinyConsole.BackgroundColor = ConsoleColor.Black;
            TinyConsole.Error.WriteLine($"{ConsoleWindowTitle}:INFORMATION:{message}");
        }

        private void InternalReportWarningMessage(String message)
        {
            TinyConsole.Erase(ConsoleEraseMode.FromCursorToEndOfScreen);
            TinyConsole.ForegroundColor = ConsoleColor.Yellow;
            TinyConsole.BackgroundColor = ConsoleColor.Black;
            TinyConsole.Error.WriteLine($"{ConsoleWindowTitle}:WARNING:{message}");
        }

        private void InternalReportErrorMessage(String message, Int32 indent)
        {
            TinyConsole.Erase(ConsoleEraseMode.FromCursorToEndOfScreen);
            TinyConsole.ForegroundColor = ConsoleColor.Red;
            TinyConsole.BackgroundColor = ConsoleColor.Black;
            TinyConsole.Error.WriteLine($"{new String(' ', indent)}{ConsoleWindowTitle}:ERROR:{message}");
        }
    }
}
