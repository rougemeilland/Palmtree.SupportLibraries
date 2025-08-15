using System;
using BenchmarkDotNet.Running;

namespace Benchmark.CUI
{
    internal sealed class Program
    {
        private static void Main()
        {
            _ = BenchmarkRunner.Run([typeof(VectorizedCalculation)]);
            Console.Beep();
        }
    }
}
