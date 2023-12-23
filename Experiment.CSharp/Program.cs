using System;

namespace Experiment.CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (var count = 0; count < 50; ++count)
                Console.WriteLine("Hello, World!");
            Console.Write("\x0c");
            Console.Beep();
            _ = Console.ReadLine();
        }
    }
}
