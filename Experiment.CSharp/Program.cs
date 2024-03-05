using System;
using System.Linq;
using Palmtree.IO;
using Palmtree.IO.Console;
using Palmtree.IO.Serialization;

namespace Experiment.CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var now = DateTimeOffset.UtcNow;
            var t1 = now.UtcDateTime;
            var t2 = t1.ToLocalTime();
            var t3 = now;
            var t4 = t3.ToLocalTime();
            Console.WriteLine($"{t1} ({t1.Kind})");
            Console.WriteLine($"{t2} ({t2.Kind})");
            Console.WriteLine(t3);
            Console.WriteLine(t4);

            TinyConsole.Beep();
            _ = TinyConsole.ReadLine();
        }
    }
}
