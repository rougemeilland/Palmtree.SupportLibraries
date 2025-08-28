using System;
using System.Threading;

namespace Palmtree
{
    public abstract class ValidationLogger
        : IValidationLogger
    {
        private const Int32 _indentSize = 4;
        private readonly IValidationLoggerSource _loggerSource;
        private Int32 _indentLevel;

        protected ValidationLogger(IValidationLoggerSource loggerSource)
        {
            _loggerSource = loggerSource ?? throw new ArgumentNullException(nameof(loggerSource));
        }

        /// <inheritdoc/>
        public void Indent() => Interlocked.Increment(ref _indentLevel);

        /// <inheritdoc/>
        public void Unindent()
        {
            var newIndent = Interlocked.Decrement(ref _indentLevel);
            if (newIndent < 0)
                throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        public void WriteLog()
        {
            lock (this)
            {
                var state = _loggerSource.State;
                try
                {
                    _loggerSource.WriteLine();
                }
                finally
                {
                    _loggerSource.State = state;
                }
            }
        }

        /// <inheritdoc/>
        public void WriteLog(String message, Boolean writeIndent = false)
        {
            lock (this)
            {
                var originalState = _loggerSource.State;
                var newState = _loggerSource.GetStateFromCategory();
                try
                {
                    if (writeIndent)
                    {
                        var indent = _indentSize * Interlocked.CompareExchange(ref _indentLevel, 0, 0);
                        _loggerSource.State = newState;
                        _loggerSource.WriteSpaces(indent);
                    }

                    _loggerSource.State = newState;
                    _loggerSource.WriteLogMessage(message);

                    _loggerSource.State = newState;
                    _loggerSource.WriteLine();
                }
                finally
                {
                    _loggerSource.State = originalState;
                }
            }
        }

        /// <inheritdoc/>
        public void WriteLog(LogCategory category, String message)
        {
            ArgumentException.ThrowIfNullOrEmpty(message);

            WriteLogCore(
                Validation.DefaultApplicationName ?? throw new InvalidOperationException("The default application name could not be resolved."),
                category,
                message);
        }

        /// <inheritdoc/>
        public void WriteLog(String applicationName, LogCategory category, String message)
        {
            ArgumentException.ThrowIfNullOrEmpty(applicationName);
            if (category == LogCategory.None)
                throw new ArgumentException($"{nameof(LogCategory.None)} cannot be specified.", nameof(category));
            ArgumentException.ThrowIfNullOrEmpty(message);

            WriteLogCore(applicationName, category, message);
        }

        private void WriteLogCore(String applicationName, LogCategory category, String message)
        {
            Validation.Assert(category != LogCategory.None);
            var indent = _indentSize * Interlocked.CompareExchange(ref _indentLevel, 0, 0);
            lock (this)
            {
                var originalState = _loggerSource.State;
                var newState = _loggerSource.GetStateFromCategory(category);
                try
                {
                    _loggerSource.State = newState;
                    _loggerSource.WriteApplicationName(applicationName);

                    _loggerSource.State = newState;
                    _loggerSource.WriteLogSeparator();

                    _loggerSource.State = newState;
                    _loggerSource.WriteLogCategory(category);

                    _loggerSource.State = newState;
                    _loggerSource.WriteLogSeparator();

                    _loggerSource.State = newState;
                    _loggerSource.WriteSpaces(indent);

                    _loggerSource.State = newState;
                    _loggerSource.WriteLogMessage(message);

                    _loggerSource.State = newState;
                    _loggerSource.WriteLine();
                }
                finally
                {
                    _loggerSource.State = originalState;
                }
            }
        }
    }
}
