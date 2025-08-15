using System;

namespace Palmtree.IO.Console
{
    public static partial class TinyConsole
    {
        private sealed class ConsoleLoggerSource
            : ValidationLoggerSource
        {
            private struct ConsoleColors
            {
                public ConsoleColor ForegroundColorCode;
            }

            private const ConsoleColor _logForegroundColor = ConsoleColor.White;
            private static readonly Color256 _color256OfOrange = Color256.FromRgb(255, 165, 0);
            private static readonly ILazyValue<Func<Color256, String?>> _escapeCodeToSetForegroundColor256 = LazyValue.Create((Color256 color) => _thisTerminalInfo.Value.SetAForeground(color));

            /// <inheritdoc/>
            public override Object? State
            {
                get => new ConsoleColors { ForegroundColorCode = ForegroundColor };

                set
                {
                    ArgumentNullException.ThrowIfNull(value);
                    Validation.Assert(value is ConsoleColors);

                    if (value is ConsoleColors consoleColors)
                        ForegroundColor = consoleColors.ForegroundColorCode;
                }
            }

            /// <inheritdoc/>
            public override Object? GetStateFromCategory(LogCategory category) => _logForegroundColor;

            /// <inheritdoc/>
            public override void WriteLogCategory(LogCategory category)
            {
                switch (category)
                {
                    case LogCategory.Information:
                        ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case LogCategory.Warning:
                        ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case LogCategory.Error:
                    {
                        var success = false;
                        try
                        {
                            var escapeCode = _escapeCodeToSetForegroundColor256.Value(_color256OfOrange);
                            if (escapeCode is not null)
                                OutputEscapeCode(escapeCode);
                            else
                                ForegroundColor = ConsoleColor.Red;
                            success = true;
                        }
                        finally
                        {
                            if (!success)
                                ForegroundColor = ConsoleColor.Red;
                        }

                        break;
                    }
                    case LogCategory.Critical:
                        ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        throw Validation.GetFatalErrorException();
                }

                base.WriteLogCategory(category);
            }

            public override void WriteLine() => TinyConsole.WriteLine();

            protected override void Write(String s) => TinyConsole.Write(s);
        }

        private sealed class ConsoleLoger
            : ValidationLogger
        {
            public ConsoleLoger()
                : base(new ConsoleLoggerSource())
            {
            }
        }

        private static readonly ILazyValue<ConsoleLoger> _consoleLogger = LazyValue.Create(() => new ConsoleLoger());

        #region WriteLog

        /// <summary>
        /// 指定したメッセージを表示して改行します。
        /// </summary>
        /// <param name="message">表示するメッセージを示す <see cref="String"/> オブジェクトです。</param>
        public static void WriteLog(String message) => _consoleLogger.Value.WriteLog(message);

        /// <summary>
        /// 指定したメッセージをカテゴリとともに表示して改行します。
        /// </summary>
        /// <param name="category">メッセージのカテゴリを示す <see cref="LogCategory"/> 列挙体です。</param>
        /// <param name="message">表示するメッセージを示す <see cref="String"/> オブジェクトです。</param>
        public static void WriteLog(LogCategory category, String message) => _consoleLogger.Value.WriteLog(category, message);

        /// <summary>
        /// 指定したメッセージを明示的なアプリケーション名とカテゴリとともに表示して改行します。
        /// </summary>
        /// <param name="applicationName">アプリケーションの名前を示す <see cref="String"/> オブジェクトです。</param>
        /// <param name="category">メッセージのカテゴリを示す <see cref="LogCategory"/> 列挙体です。</param>
        /// <param name="message">表示するメッセージを示す <see cref="String"/> オブジェクトです。</param>
        public static void WriteLog(String applicationName, LogCategory category, String message)
            => _consoleLogger.Value.WriteLog(applicationName, category, message);

        /// <summary>
        /// 指定した例外オブジェクトを表示します。
        /// </summary>
        /// <param name="ex">表示する例外オブジェクトを示す <see cref="Exception"/> オブジェクトです。 </param>
        public static void WriteLog(Exception ex) => _consoleLogger.Value.WriteLog(ex);

        /// <summary>
        /// 指定した例外オブジェクトを明示的なアプリケーション名とともに表示します。
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="ex">表示する例外オブジェクトを示す <see cref="Exception"/> オブジェクトです。 </param>
        public static void WriteLog(String applicationName, Exception ex) => _consoleLogger.Value.WriteLog(applicationName, ex);

        #endregion
    }
}
