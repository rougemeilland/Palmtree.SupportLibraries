using System;
using Palmtree;
using Palmtree.IO.Console;

namespace Experiment.CSharp
{
    internal class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:未使用のパラメーターを削除します", Justification = "<保留中>")]
        static void Main(string[] args)
        {
            {
                var data = new byte[] { 0, 1, 2, 3, 4, 5 };

                var span1 = data.AsSpan(0, 5);
                var span2 = data.AsSpan(1, 5);
                span1.CopyTo(span2);
                TinyConsole.WriteLine($"0x{data[0]:x2}, 0x{data[1]:x2}, 0x{data[2]:x2}, 0x{data[3]:x2}, 0x{data[4]:x2}, 0x{data[5]:x2}");
            }

            {
                var data = new byte[] { 0, 1, 2, 3, 4, 5 };

                var span1 = data.AsSpan(1, 5);
                var span2 = data.AsSpan(0, 5);
                span1.CopyTo(span2);
                TinyConsole.WriteLine($"0x{data[0]:x2}, 0x{data[1]:x2}, 0x{data[2]:x2}, 0x{data[3]:x2}, 0x{data[4]:x2}, 0x{data[5]:x2}");
            }

            TinyConsole.Beep();
            _ = TinyConsole.ReadLine();
        }
    }
}
