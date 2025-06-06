using System;

namespace Palmtree
{
    public static class IValidationLIstenerExtensions
    {
        private const String _lineSeparator = "--------------------";
        private static readonly Char[] _textLineSeparator = new[] { '\r', '\n' };

        public static void Write(this IValidationLogger writer, String message)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(message);

            writer.Write(message);
        }

        public static void WriteLine(this IValidationLogger writer, String message)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(message);

            writer.WriteLine(message);
        }

        public static void WriteLine(this IValidationLogger writer, Exception ex)
        {
            writer.WriteLine(ex.Message);
            foreach (var stackTraceLine in (ex.StackTrace ?? "").Split(_textLineSeparator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                writer.WriteLine(stackTraceLine);
            if (ex.InnerException is not null)
            {
                writer.WriteLine(_lineSeparator);
                writer.Indent();
                try
                {
                    writer.WriteLine(ex.InnerException);
                }
                finally
                {
                    writer.Unindent();
                }
            }

            if (ex is AggregateException aggregateException)
            {
                foreach (var innerException in aggregateException.InnerExceptions)
                {
                    writer.WriteLine(_lineSeparator);
                    writer.Indent();
                    try
                    {
                        writer.WriteLine(innerException);
                    }
                    finally
                    {
                        writer.Unindent();
                    }
                }
            }
        }
    }
}
