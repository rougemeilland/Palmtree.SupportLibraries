﻿using System;
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

        private const Int32 _INDENT_STEP = 2;

        private readonly String _thisProgramName;
        private readonly Boolean _isLaunchedByThisLauncher;

        private Boolean _isPressedBreak;

        protected ApplicationBase()
        {
            _thisProgramName = GetType().Assembly.GetAssemblyFileNameWithoutExtension();
            _isLaunchedByThisLauncher = ConsoleApplicationLauncher.IsLaunchedByThisLauncher;
        }

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
        protected virtual String ConsoleWindowTitle => _thisProgramName;
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
                    InternalReportException(exception, 0);
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
                    InternalReportInformationMessage(message);
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
                    InternalReportWarningMessage(message);
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
                    InternalReportErrorMessage(message, 0);
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
            TinyConsole.ForegroundColor = ConsoleColor.White;
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
