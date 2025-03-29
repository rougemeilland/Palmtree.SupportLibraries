using System;
using System.Linq;
using Palmtree;
using Palmtree.Collections;

namespace Experiment.CSharp
{
    internal class Program
    {
        private class TestClass
            : IComparable<TestClass>
        {
            private readonly int _value;

            public TestClass(int value)
            {
                _value = value;
            }

            public int CompareTo(TestClass? other)
            {
                if (other is null)
                    return -1;
                else
                    return _value.CompareTo(other._value);
            }

            public override string ToString() => _value.ToString();
        }

        static void Main(string[] args)
        {
#if false
            var array = new[] { 6, 7, 7, 7, 1, 7, 0, 0, 5, 7, 2, 7, 4, 4, 2, 6, 6, 6, 1, 5, 5, 4, 2, 4, 4, 5, 5, 6, 4, 2, 6, 4 };
#elif true
            var array = new[] { 6, 7, 7, 7, 1, 7, 0, 0, 5, 7, 2, 7, 4, 4, 2, 6, 6, 6, 1, 5, 5, 4, 2, 4, 4, 5, 5, 6, 4, 2, 6, 4 }.Select(n => new TestClass(n)).ToArray();
#else
            var array = RandomSequence.GetUInt32Sequence().Select(n => new TestClass((int)(n % 8))).Take(32).ToArray();
#endif
            System.Diagnostics.Debug.WriteLine($"var array = new []{{{string.Join(", ", array.Select(element => element.ToString()))}}}.Select(n => new TestClass(n)).ToArray();");
            System.Diagnostics.Debug.WriteLine("----- 1st -----");
            array.QuickSort();
            System.Diagnostics.Debug.WriteLine("----- 2nd -----");
            array.QuickSort();
            System.Diagnostics.Debug.WriteLine("----------");

            Console.Beep();
            _ = Console.ReadLine();
        }
    }
}
