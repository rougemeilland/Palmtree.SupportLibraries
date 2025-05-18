using System;
using System.Collections.ObjectModel;
using System.Linq;
using Palmtree;
using Palmtree.Collections;

namespace Experiment.CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (var index = 0; index < args.Length; ++index)
                Console.WriteLine($"arg[{index}]:{args[index]}");

            Console.Beep();
            _ = Console.ReadLine();
        }
    }
}
