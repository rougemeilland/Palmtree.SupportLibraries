using System;
using Palmtree.IO.Console;

namespace Experiment.CSharp
{
    internal class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:未使用のパラメーターを削除します", Justification = "<保留中>")]
        static void Main(string[] args)
        {
            Console.WriteLine(TinyConsole.GetWidth("あいうえお"));
            Console.WriteLine(TinyConsole.GetWidth("ABCDE"));
            Console.WriteLine(TinyConsole.GetWidth("あいうえおABCDE"));

            TinyConsole.Beep();
            _ = TinyConsole.ReadLine();
        }
    }
}
