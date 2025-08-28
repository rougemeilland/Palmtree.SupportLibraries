using System;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Benchmark.CUI
{
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public partial class VectorizedCalculation
    {
        private static readonly Random _randomNumberGenerator = new(Environment.TickCount);

        private static ReadOnlyMemory<ELEMENT_T> CreateTestData<ELEMENT_T>(Int32 length)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            
            var buffer = new ELEMENT_T[length];
            for (var index = 0; index < buffer.Length; ++index)
                buffer[index] = ELEMENT_T.CreateTruncating(_randomNumberGenerator.Next(Int32.MinValue, Int32.MaxValue));
            return buffer;
        }
    }
}
