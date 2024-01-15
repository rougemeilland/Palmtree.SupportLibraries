using System.Threading;
using Palmtree.IO.Console;

namespace Test.Console.Native
{
    internal class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:未使用のパラメーターを削除します", Justification = "<保留中>")]
        static void Main(string[] args)
        {
            TinyConsole.Clear();
            while (true)
            {
                TinyConsole.SetCursorPosition(0, 0);
                TinyConsole.Write($"({TinyConsole.WindowWidth}, {TinyConsole.WindowHeight})");
                TinyConsole.Erase(ConsoleEraseMode.FromCursorToEndOfLine);
                Thread.Sleep(1000);
            }

            //System.Console.Beep();
            //_ = System.Console.ReadLine();
        }
    }
}
