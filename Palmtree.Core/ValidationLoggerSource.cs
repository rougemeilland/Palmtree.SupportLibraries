using System;

namespace Palmtree
{
    public abstract class ValidationLoggerSource
        : IValidationLoggerSource
    {
        /// <inheritdoc/>
        public virtual Object? State
        {
            get => null;
            set { }
        }

        /// <inheritdoc/>
        public virtual Object? GetStateFromCategory(LogCategory category) => null;

        /// <inheritdoc/>
        public virtual void WriteApplicationName(String applicationName)
        {
            ArgumentNullException.ThrowIfNull(applicationName);

            Write(applicationName);
        }

        /// <inheritdoc/>
        public virtual void WriteLogCategory(LogCategory category)
        {
            if (category == LogCategory.None)
                throw new ArgumentException($"{nameof(LogCategory.None)} cannot be specified.", nameof(category));

            Write(GetLogCategoryString(category));
        }

        /// <inheritdoc/>
        public virtual void WriteLogMessage(String message) => Write(message);

        /// <inheritdoc/>
        public void WriteLogSeparator() => Write(":");

        public void WriteSpaces(Int32 n)
        {
            while (n >= 4)
            {
                Write("    ");
                n -= 4;
            }

            while (n > 0)
            {
                Write(" ");
                --n;
            }
        }

        /// <inheritdoc/>
        public abstract void WriteLine();

        /// <summary>
        /// ログに文字列を出力します。
        /// </summary>
        /// <param name="s">
        /// 出力する文字列を示す <see cref="String"/> です。
        /// </param>
        protected abstract void Write(String s);

        private String GetLogCategoryString(LogCategory category)
            => category switch
            {
                LogCategory.Information => "INFORMATION",
                LogCategory.Warning => "WARNING",
                LogCategory.Error => "ERROR",
                LogCategory.Critical => "CRITICAL",
                _ => throw Validation.GetFatalErrorException(),
            };
    }
}
