using System;
using System.Threading;
using System.Threading.Tasks;
using Palmtree;
using Palmtree.IO;

namespace Experiment.CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{typeof(Program).FullName}: directory={typeof(Program).Assembly.GetBaseDirectory()}");
            Console.WriteLine($"{typeof(Program).FullName}: name={typeof(Program).Assembly.GetAssemblyFileNameWithoutExtension()}");
            Console.Beep();
            _ = Console.ReadLine();
        }
    }
}
