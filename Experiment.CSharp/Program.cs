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
            Console.WriteLine($"{typeof(Program).Name}: directory={typeof(Program).Assembly.GetBaseDirectory()}");
            Console.WriteLine($"{typeof(Crc24).Name}: directory={typeof(Program).Assembly.GetBaseDirectory()}");
            Console.WriteLine($"{typeof(Console).Name}: directory={typeof(Program).Assembly.GetBaseDirectory()}");
            Console.WriteLine($"{typeof(Program).Name}: name={typeof(Program).Assembly.GetAssemblyFileNameWithoutExtension()}");
            Console.WriteLine($"{typeof(ISequentialInputByteStream).Name}: name={typeof(Program).Assembly.GetAssemblyFileNameWithoutExtension()}");
            Console.WriteLine($"{typeof(Console).Name}: name={typeof(Program).Assembly.GetAssemblyFileNameWithoutExtension()}");
            Console.Beep();
            _ = Console.ReadLine();
        }
    }
}
