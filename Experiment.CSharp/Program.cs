using System;
using Palmtree;
using Palmtree.IO.Console;

namespace Experiment.CSharp
{
    internal sealed class Program
    {
        private static void Main()
        {
            TinyConsole.WriteLog(LogCategory.Information, "情報メッセージ");
            TinyConsole.WriteLog(LogCategory.Warning, "警告メッセージ");
            TinyConsole.WriteLog(LogCategory.Error, "エラーメッセージ");
            TinyConsole.WriteLog(LogCategory.Critical, "致命的エラーメッセージ");

            Console.Beep();
            Console.WriteLine("Complete");
            _ = Console.ReadLine();
        }
    }
}
