using System;
using System.Threading;

namespace Palmtree
{
    public static class IValidationLoggerExtensions
    {
        private const String _lineSeparator = "--------------------";
        private static readonly Char[] _textLineSeparator = ['\r', '\n'];
#if NET9_0_OR_GREATER
        private static readonly Lock _lockObject = new();
#else
        private static readonly Object _lockObject = new();
#endif
        public static void WriteLog(this IValidationLogger writer, String message) => writer.WriteLog(null, LogCategory.None, message);

        public static void WriteLog(this IValidationLogger writer, LogCategory category, String message) => writer.WriteLog(null, category, message);

        public static void WriteLog(this IValidationLogger writer, String? applicationName, LogCategory category, String message)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(message);

            lock (_lockObject)
            {
                var prefix = $"{GetApplicationNamePartText(applicationName)}{GetCategoryPartText(category)}";
                writer.WriteLog(prefix, message);
            }

            static String GetApplicationNamePartText(String? applicationName)
            {
                return
                    applicationName == ""
                    ? ""
                    : applicationName is not null
                    ? $"{applicationName}:"
                    : Validation.DefaultApplicationName is not null
                    ? $"{Validation.DefaultApplicationName}:"
                    : "";
            }

            static String GetCategoryPartText(LogCategory category)
            {
                return
                    category switch
                    {
                        LogCategory.None => "",
                        LogCategory.Information => "INFORMATION:",
                        LogCategory.Warning => "WARNING:",
                        LogCategory.Error => "ERROR:",
                        LogCategory.Critical => "CRITICAL:",
                        _ => throw Validation.GetFailErrorException($"Unexpected {nameof(LogCategory)} value.: {category}"),
                    };
            }
        }

        public static void WriteLog(this IValidationLogger writer, Exception ex) => writer.WriteLog(null, ex);

        public static void WriteLog(this IValidationLogger writer, String? applicationName, Exception ex)
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
                    writer.WriteLog(applicationName, category, ex is ApplicationException ? ex.Message : $"({ex.GetType().FullName}) {ex.Message}");
                }
                else
                {
                    writer.WriteLog("", ex is ApplicationException ? ex.Message : $"({ex.GetType().FullName}) {ex.Message}");
                }

                foreach (var stackTraceLine in (ex.StackTrace ?? "").Split(_textLineSeparator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                    writer.WriteLog("", stackTraceLine);
                if (ex.InnerException is not null)
                {
                    writer.WriteLog(null, _lineSeparator);
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
                        writer.WriteLog(null, _lineSeparator);
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
