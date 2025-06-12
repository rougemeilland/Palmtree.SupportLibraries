using System;
using System.Text;
using Palmtree.IO.Console;

namespace Palmtree.Application
{
    public abstract class ApplicationBase
    {
        protected enum ResultCode
        {
            Success = 0,
            Failed = 1,
            Cancelled = -1,
        }

        private readonly Boolean _isLaunchedByThisLauncher = ConsoleApplicationLauncher.IsLaunchedByThisLauncher;

        private Boolean _isPressedBreak;

        public virtual Int32 Run(String[] args)
        {
            ResultCode result;
            try
            {
                TinyConsole.DefaultTextWriter = ConsoleTextWriterType.StandardError;
                if (!CursorVisible)
                    TinyConsole.CursorVisible = ConsoleCursorVisiblity.Invisible;
                if (_isLaunchedByThisLauncher)
                    TinyConsole.Title = ConsoleWindowTitle;
                if (DelayBreak)
                    TinyConsole.CancelKeyPress += TinyConsole_CancelKeyPress;
                var encoding = InputOutputEncoding;
                if (encoding is not null)
                {
                    TinyConsole.InputEncoding = encoding;
                    TinyConsole.OutputEncoding = encoding;
                }

                result = Main(args);
            }
            finally
            {
                TinyConsole.CancelKeyPress -= TinyConsole_CancelKeyPress;
                TinyConsole.CursorVisible = ConsoleCursorVisiblity.NormalMode;
            }

            CleanUp(result);
            Finish(result, _isLaunchedByThisLauncher);
            return (Int32)result;
        }

        protected virtual Boolean DelayBreak => true;
        protected virtual Encoding? InputOutputEncoding => null;
        protected virtual String ConsoleWindowTitle => Validation.DefaultApplicationName ?? GetType().Assembly.GetAssemblyFileNameWithoutExtension();
        protected virtual Boolean CursorVisible => false;

        protected abstract ResultCode Main(String[] args);

        protected virtual void CleanUp(ResultCode result)
        {
        }

        protected virtual void Finish(ResultCode result, Boolean isLaunchedByConsoleApplicationLauncher)
        {
            if (isLaunchedByConsoleApplicationLauncher)
            {
                TinyConsole.Beep();
                _ = TinyConsole.ReadLine();
            }
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
                    TinyConsole.WriteLog(exception);
                }
                finally
                {
                    ResetConsoleTextStatus();
                }
            }
        }

        protected void ReportInformationMessage(String message)
        {
            lock (this)
            {
                try
                {
                    TinyConsole.WriteLog(LogCategory.Information, message);
                }
                finally
                {
                    ResetConsoleTextStatus();
                }
            }
        }

        protected void ReportWarningMessage(String message)
        {
            lock (this)
            {
                try
                {
                    TinyConsole.WriteLog(LogCategory.Warning, message);
                }
                finally
                {
                    ResetConsoleTextStatus();
                }
            }
        }

        protected void ReportErrorMessage(String message)
        {
            lock (this)
            {
                try
                {
                    TinyConsole.WriteLog(LogCategory.Error, message);
                }
                finally
                {
                    ResetConsoleTextStatus();
                }
            }
        }

        protected virtual void ResetConsoleTextStatus()
            => TinyConsole.ResetColor();

        private void TinyConsole_CancelKeyPress(Object? sender, ConsoleCancelEventArgs e)
        {
            lock (this)
            {
                _isPressedBreak = true;
            }

            e.Cancel = true;
        }
    }
}
