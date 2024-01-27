using System;
using Palmtree.IO.Console;

namespace Palmtree.Application
{
    public abstract class BatchApplication
        : ApplicationBase
    {
        protected delegate String FormatMessageDelegate(String percentageText, String shortenedPartOfMessage);

        private const String _CARRIGE_RETURN = "\r";

        private String _currentProgressMessage;

        public BatchApplication()
        {
            _currentProgressMessage = "";
        }

        protected override void CleanUp(ResultCode result)
        {
            if (result == ResultCode.Success)
                TinyConsole.Erase(ConsoleEraseMode.FromCursorToEndOfScreen);
            else if (result == ResultCode.Cancelled)
                TinyConsole.WriteLine();

            base.CleanUp(result);
        }

        protected void ReportProgress(String message)
        {
            var consoleWidth = TinyConsole.WindowWidth;
            lock (this)
            {
                if (message != _currentProgressMessage)
                {
                    WriteMessage(message, TinyConsole.GetWidth(message), consoleWidth);
                    _currentProgressMessage = message;
                }
            }
        }

        protected void ReportProgress(Double progressRate)
            => ReportProgress(progressRate, "", (percentage, s) => percentage);

        protected void ReportProgress(Double progressRate, String shortnenablePartOfMessage, FormatMessageDelegate messageFormatter)
        {
            var consoleWidth = TinyConsole.WindowWidth;
            var messageText =
                BuildProgressMessage(
                    $"{progressRate * 100.0:##0.00}%",
                    shortnenablePartOfMessage,
                    messageFormatter,
                    consoleWidth);
            lock (this)
            {
                if (messageText != _currentProgressMessage)
                {
                    WriteMessage(messageText, TinyConsole.GetWidth(messageText), consoleWidth);
                    _currentProgressMessage = messageText;
                }
            }
        }

        private static void WriteMessage(String message, Int32 messageWidth, Int32 consoleWidth)
        {
            if (messageWidth < consoleWidth)
            {
                // メッセージの幅がコンソールの幅未満である場合

                TinyConsole.Write(message);
                TinyConsole.Erase(ConsoleEraseMode.FromCursorToEndOfScreen);
                TinyConsole.Write(_CARRIGE_RETURN);
            }
            else
            {
                // メッセージの幅がコンソールの幅以上である場合

                // message を表示するために予想し得る最大の行数を計算する。
                // (windowWidth - 1) としているのは、行の終端に全角文字を表示するのに 1 桁足りない場合を想定しているから。
                var rows = (messageWidth + (consoleWidth - 1) - 1) / (consoleWidth - 1);

                // rows 行だけ改行して、rows 行だけカーソルを上に移動する。
                TinyConsole.Write($"{new String('\n', rows)}");
                TinyConsole.CursorUp(rows);

                // これ以降、message を表示してもスクロールは発生しないはず。

                // 現在のカーソル位置を取得する。
                var (cursorLeft, cursorTop) = TinyConsole.GetCursorPosition();

                // メッセージを表示する。
                TinyConsole.Write(message);

                // カーソル以降の文字列を消去する。
                TinyConsole.Erase(ConsoleEraseMode.FromCursorToEndOfScreen);

                // カーソル位置を元に戻す。
                TinyConsole.SetCursorPosition(cursorLeft, cursorTop);
            }
        }

        private static String BuildProgressMessage(String percentageText, String shortnenablePartOfMessage, FormatMessageDelegate messageFormatter, Int32 consoleWidth)
        {
            var minimumMessageWidth = TinyConsole.GetWidth(messageFormatter(percentageText, ""));
            if (minimumMessageWidth >= consoleWidth - 1)
                return messageFormatter(percentageText, shortnenablePartOfMessage);
            else
                return messageFormatter(percentageText, TinyConsole.ShrinkText(shortnenablePartOfMessage, consoleWidth - 1 - minimumMessageWidth, "…", TextShrinkingStyle.OmitCenter));
        }
    }
}
