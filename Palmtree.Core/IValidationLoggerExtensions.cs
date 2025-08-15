using System;
using System.Threading;

namespace Palmtree
{
    public static class IValidationLoggerExtensions
    {
        private const String _lineSeparator = "----------------------------------------";
        private static readonly Char[] _textLineSeparator = ['\r', '\n'];
#if NET9_0_OR_GREATER
        private static readonly Lock _lockObject = new();
#else
        private static readonly Object _lockObject = new();
#endif

        public static void WriteLog(this IValidationLogger writer, Exception ex) => writer.WriteLogCore(null, ex);

        public static void WriteLog(this IValidationLogger writer, String applicationName, Exception ex) => writer.WriteLogCore(applicationName, ex);

        private static void WriteLogCore(this IValidationLogger writer, String? applicationName, Exception ex)
        {
            lock (_lockObject)
            {
                WriteExceptionLog(writer, applicationName, ex, true);
            }

            static void WriteExceptionLog(IValidationLogger writer, String? applicationName, Exception ex, Boolean writePrefix)
            {
                if (writePrefix)
                {
                    var category = ex is AssertionException ? LogCategory.Critical : LogCategory.Error;
                    if (applicationName is null)
                        writer.WriteLog(category, ex is ApplicationException ? ex.Message : $"({ex.GetType().FullName}) {ex.Message}");
                    else
                        writer.WriteLog(applicationName, category, ex is ApplicationException ? ex.Message : $"({ex.GetType().FullName}) {ex.Message}");
                }
                else
                {
                    writer.WriteLog(ex is ApplicationException ? ex.Message : $"({ex.GetType().FullName}) {ex.Message}", true);
                }

                foreach (var stackTraceLine in (ex.StackTrace ?? "").Split(_textLineSeparator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                    writer.WriteLog(stackTraceLine, true);
                if (ex.InnerException is not null)
                {
                    writer.WriteLog(_lineSeparator);
                    writer.Indent();
                    try
                    {
                        WriteExceptionLog(writer, null, ex.InnerException, false);
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
                        writer.WriteLog(_lineSeparator);
                        writer.Indent();
                        try
                        {
                            WriteExceptionLog(writer, null, innerException, false);
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
}
