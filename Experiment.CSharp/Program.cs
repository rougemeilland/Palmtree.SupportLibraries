using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Experiment.CSharp
{
    internal static partial class Program
    {
        internal static readonly BigInteger[] sourceArray = [479779549, 436412677, 2049404392, 586332822, 840885654, 1024278558, 617518784, 1644205277, 1028760744, 1692973055, 2070899235, 1519778740, 464287074, 1412263654];

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void Main()
        {
            var vector1 = Vector256.Create(1d);
            var vector2 = Vector256.Create(1d);
            _=Experiment.CSharp.Library.VectorizedCalculation.Max256(vector1, vector2);
            _=Experiment.CSharp.Library.VectorizedCalculation.Min256(vector1, vector2);

            Console.Beep();
            Console.WriteLine("Complete");
            _ = Console.ReadLine();

        }

        private static String FormatExpression(Object value, [CallerArgumentExpression(nameof(value))] String? expression = null) => $"{expression}={value}";
    }
}
