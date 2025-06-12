using System;

namespace Palmtree.IO.Console
{
    public static partial class TinyConsole
    {
        private const String _lineSeparator = "--------------------";
        private static readonly Char[] _textLineSeparator = ['\r', '\n'];

        private static readonly ILazyValue<String?> _escapeCodeToSetForegroundColorOnError = LazyValue.Create(() => _thisTerminalInfo.Value.SetAForeground(Color256.FromRgb(255, 165, 0)));

        #region WriteLog

        /// <summary>
        /// 指定したメッセージを表示して改行します。
        /// </summary>
        /// <param name="message">表示するメッセージを示す <see cref="String"/> オブジェクトです。</param>
        public static void WriteLog(String message) => WriteLog(null, LogCategory.None, message);

        /// <summary>
        /// 指定したメッセージをカテゴリとともに表示して改行します。
        /// </summary>
        /// <param name="category">メッセージのカテゴリを示す <see cref="LogCategory"/> 列挙体です。</param>
        /// <param name="message">表示するメッセージを示す <see cref="String"/> オブジェクトです。</param>
        public static void WriteLog(LogCategory category, String message) => WriteLog(null, category, message);

        /// <summary>
        /// 指定したメッセージを明示的なアプリケーション名とともに表示して改行します。
        /// </summary>
        /// <param name="applicationName">アプリケーションの名前を示す <see cref="String"/> オブジェクトです。</param>
        /// <param name="message">表示するメッセージを示す <see cref="String"/> オブジェクトです。</param>
        public static void WriteLog(String? applicationName, String message) => WriteLog(applicationName, LogCategory.None, message);

        /// <summary>
        /// 指定したメッセージを明示的なアプリケーション名とカテゴリとともに表示して改行します。
        /// </summary>
        /// <param name="applicationName">アプリケーションの名前を示す <see cref="String"/> オブジェクトです。</param>
        /// <param name="category">メッセージのカテゴリを示す <see cref="LogCategory"/> 列挙体です。</param>
        /// <param name="message">表示するメッセージを示す <see cref="String"/> オブジェクトです。</param>
        public static void WriteLog(String? applicationName, LogCategory category, String message)
        {
            lock (_lockObject)
            {
                var currentForeGroundColor = ForegroundColor;
                try
                {
                    ForegroundColor = ConsoleColor.White;
                    Write(GetApplicationNamePartText(applicationName));
                    switch (category)
                    {
                        case LogCategory.None:
                            break;
                        case LogCategory.Information:
                            ForegroundColor = ConsoleColor.Cyan;
                            Write("INFORMATION");
                            ForegroundColor = ConsoleColor.White;
                            Write(':');
                            break;
                        case LogCategory.Warning:
                            ForegroundColor = ConsoleColor.Yellow;
                            Write("WARNING");
                            ForegroundColor = ConsoleColor.White;
                            Write(':');
                            break;
                        case LogCategory.Error:
                        {
                            var foregrroundColorOnError = _escapeCodeToSetForegroundColorOnError.Value;
                            if (foregrroundColorOnError is null)
                            {
                                ForegroundColor = ConsoleColor.Red;
                            }
                            else
                            {
                                try
                                {
                                    OutputEscapeCode(foregrroundColorOnError);
                                }
                                catch (Exception)
                                {
                                    ForegroundColor = ConsoleColor.Red;
                                }
                            }

                            Write("ERROR");
                            ForegroundColor = ConsoleColor.White;
                            Write(':');
                            break;
                        }
                        case LogCategory.Critical:
                            ForegroundColor = ConsoleColor.Red;
                            Write("CRITICAL");
                            ForegroundColor = ConsoleColor.White;
                            Write(':');
                            break;
                        default:
                            break;
                    }

                    WriteLine(message);
                }
                finally
                {
                    ForegroundColor = currentForeGroundColor;
                }
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
        }

        /// <summary>
        /// 指定した例外オブジェクトを表示します。
        /// </summary>
        /// <param name="ex">表示する例外オブジェクトを示す <see cref="Exception"/> オブジェクトです。 </param>
        public static void WriteLog(Exception ex) => WriteLog(null, ex);

        /// <summary>
        /// 指定した例外オブジェクトを明示的なアプリケーション名とともに表示します。
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="ex">表示する例外オブジェクトを示す <see cref="Exception"/> オブジェクトです。 </param>
        public static void WriteLog(String? applicationName, Exception ex)
        {
            lock (_lockObject)
            {
                WriteExceptionLog(applicationName, ex, true, "");
            }

            static void WriteExceptionLog(String? applicationName, Exception ex, Boolean writePrefix, String indent)
            {
                if (writePrefix)
                {
                    var category = ex is AssertionException ? LogCategory.Critical : LogCategory.Error;
                    WriteLog(applicationName, category, $"{indent}{(ex is ApplicationException ? "" : $"({ex.GetType().FullName}) ")}{ex.Message}");
                }
                else
                {
                    WriteLine($"{indent}{(ex is ApplicationException ? "" : $"({ex.GetType().FullName}) ")}{ex.Message}");
                }

                foreach (var stackTraceLine in (ex.StackTrace ?? "").Split(_textLineSeparator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                    WriteLine($"{indent}    {stackTraceLine}");
                if (ex.InnerException is not null)
                {
                    WriteLine(_lineSeparator);
                    WriteExceptionLog(null, ex.InnerException, false, $"{indent}    ");
                }

                if (ex is AggregateException aggregateException)
                {
                    foreach (var innerException in aggregateException.InnerExceptions)
                    {
                        WriteLine(_lineSeparator);
                        WriteExceptionLog(null, innerException, false, $"{indent}    ");
                    }
                }
            }
        }

        #endregion
    }
}
