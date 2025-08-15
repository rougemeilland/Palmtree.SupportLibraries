using System;
using System.Numerics;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Benchmark.CUI
{
    public partial class VectorizedCalculation
    {
        private readonly ReadOnlyMemory<SByte> _data0255OfSByte = CreateTestData<SByte>(255);
        private readonly ReadOnlyMemory<Byte> _data0255OfByte = CreateTestData<Byte>(255);
        private readonly ReadOnlyMemory<Int16> _data0255OfInt16 = CreateTestData<Int16>(255);
        private readonly ReadOnlyMemory<UInt16> _data0255OfUInt16 = CreateTestData<UInt16>(255);
        private readonly ReadOnlyMemory<Char> _data0255OfChar = CreateTestData<Char>(255);
        private readonly ReadOnlyMemory<Int32> _data0255OfInt32 = CreateTestData<Int32>(255);
        private readonly ReadOnlyMemory<UInt32> _data0255OfUInt32 = CreateTestData<UInt32>(255);
        private readonly ReadOnlyMemory<Int64> _data0255OfInt64 = CreateTestData<Int64>(255);
        private readonly ReadOnlyMemory<UInt64> _data0255OfUInt64 = CreateTestData<UInt64>(255);
        private readonly ReadOnlyMemory<IntPtr> _data0255OfIntPtr = CreateTestData<IntPtr>(255);
        private readonly ReadOnlyMemory<UIntPtr> _data0255OfUIntPtr = CreateTestData<UIntPtr>(255);
        private readonly ReadOnlyMemory<Half> _data0255OfHalf = CreateTestData<Half>(255);
        private readonly ReadOnlyMemory<Single> _data0255OfSingle = CreateTestData<Single>(255);
        private readonly ReadOnlyMemory<Double> _data0255OfDouble = CreateTestData<Double>(255);
        private readonly ReadOnlyMemory<NFloat> _data0255OfNFloat = CreateTestData<NFloat>(255);
        private readonly ReadOnlyMemory<Decimal> _data0255OfDecimal = CreateTestData<Decimal>(255);
        private readonly ReadOnlyMemory<BigInteger> _data0255OfBigInteger = CreateTestData<BigInteger>(255);

        [Benchmark]
        [BenchmarkCategory("Max", "SByte")]
        public void MaxSByte0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfSByte.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "SByte")]
        public void MaxSByte0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfSByte.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Byte")]
        public void MaxByte0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfByte.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Byte")]
        public void MaxByte0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfByte.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Int16")]
        public void MaxInt160255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfInt16.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Int16")]
        public void MaxInt160255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfInt16.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "UInt16")]
        public void MaxUInt160255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfUInt16.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "UInt16")]
        public void MaxUInt160255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfUInt16.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Char")]
        public void MaxChar0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfChar.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Char")]
        public void MaxChar0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfChar.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Int32")]
        public void MaxInt320255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfInt32.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Int32")]
        public void MaxInt320255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfInt32.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "UInt32")]
        public void MaxUInt320255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfUInt32.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "UInt32")]
        public void MaxUInt320255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfUInt32.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Int64")]
        public void MaxInt640255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfInt64.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Int64")]
        public void MaxInt640255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfInt64.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "UInt64")]
        public void MaxUInt640255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfUInt64.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "UInt64")]
        public void MaxUInt640255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfUInt64.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "IntPtr")]
        public void MaxIntPtr0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "IntPtr")]
        public void MaxIntPtr0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "UIntPtr")]
        public void MaxUIntPtr0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfUIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "UIntPtr")]
        public void MaxUIntPtr0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfUIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Half")]
        public void MaxHalf0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfHalf.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Half")]
        public void MaxHalf0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfHalf.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Single")]
        public void MaxSingle0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfSingle.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Single")]
        public void MaxSingle0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfSingle.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Double")]
        public void MaxDouble0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfDouble.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Double")]
        public void MaxDouble0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfDouble.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "NFloat")]
        public void MaxNFloat0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfNFloat.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "NFloat")]
        public void MaxNFloat0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfNFloat.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Decimal")]
        public void MaxDecimal0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfDecimal.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "Decimal")]
        public void MaxDecimal0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfDecimal.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "BigInteger")]
        public void MaxBigInteger0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMax(_data0255OfBigInteger.Span);

        [Benchmark]
        [BenchmarkCategory("Max", "BigInteger")]
        public void MaxBigInteger0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMax(_data0255OfBigInteger.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "SByte")]
        public void MinSByte0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfSByte.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "SByte")]
        public void MinSByte0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfSByte.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Byte")]
        public void MinByte0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfByte.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Byte")]
        public void MinByte0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfByte.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Int16")]
        public void MinInt160255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfInt16.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Int16")]
        public void MinInt160255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfInt16.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "UInt16")]
        public void MinUInt160255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfUInt16.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "UInt16")]
        public void MinUInt160255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfUInt16.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Char")]
        public void MinChar0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfChar.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Char")]
        public void MinChar0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfChar.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Int32")]
        public void MinInt320255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfInt32.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Int32")]
        public void MinInt320255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfInt32.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "UInt32")]
        public void MinUInt320255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfUInt32.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "UInt32")]
        public void MinUInt320255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfUInt32.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Int64")]
        public void MinInt640255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfInt64.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Int64")]
        public void MinInt640255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfInt64.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "UInt64")]
        public void MinUInt640255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfUInt64.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "UInt64")]
        public void MinUInt640255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfUInt64.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "IntPtr")]
        public void MinIntPtr0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "IntPtr")]
        public void MinIntPtr0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "UIntPtr")]
        public void MinUIntPtr0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfUIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "UIntPtr")]
        public void MinUIntPtr0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfUIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Half")]
        public void MinHalf0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfHalf.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Half")]
        public void MinHalf0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfHalf.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Single")]
        public void MinSingle0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfSingle.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Single")]
        public void MinSingle0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfSingle.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Double")]
        public void MinDouble0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfDouble.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Double")]
        public void MinDouble0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfDouble.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "NFloat")]
        public void MinNFloat0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfNFloat.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "NFloat")]
        public void MinNFloat0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfNFloat.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Decimal")]
        public void MinDecimal0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfDecimal.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "Decimal")]
        public void MinDecimal0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfDecimal.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "BigInteger")]
        public void MinBigInteger0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMin(_data0255OfBigInteger.Span);

        [Benchmark]
        [BenchmarkCategory("Min", "BigInteger")]
        public void MinBigInteger0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMin(_data0255OfBigInteger.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "SByte")]
        public void MaxNumberSByte0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfSByte.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "SByte")]
        public void MaxNumberSByte0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfSByte.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Byte")]
        public void MaxNumberByte0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfByte.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Byte")]
        public void MaxNumberByte0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfByte.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Int16")]
        public void MaxNumberInt160255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfInt16.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Int16")]
        public void MaxNumberInt160255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfInt16.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "UInt16")]
        public void MaxNumberUInt160255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfUInt16.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "UInt16")]
        public void MaxNumberUInt160255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfUInt16.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Char")]
        public void MaxNumberChar0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfChar.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Char")]
        public void MaxNumberChar0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfChar.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Int32")]
        public void MaxNumberInt320255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfInt32.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Int32")]
        public void MaxNumberInt320255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfInt32.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "UInt32")]
        public void MaxNumberUInt320255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfUInt32.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "UInt32")]
        public void MaxNumberUInt320255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfUInt32.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Int64")]
        public void MaxNumberInt640255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfInt64.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Int64")]
        public void MaxNumberInt640255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfInt64.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "UInt64")]
        public void MaxNumberUInt640255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfUInt64.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "UInt64")]
        public void MaxNumberUInt640255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfUInt64.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "IntPtr")]
        public void MaxNumberIntPtr0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "IntPtr")]
        public void MaxNumberIntPtr0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "UIntPtr")]
        public void MaxNumberUIntPtr0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfUIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "UIntPtr")]
        public void MaxNumberUIntPtr0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfUIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Half")]
        public void MaxNumberHalf0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfHalf.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Half")]
        public void MaxNumberHalf0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfHalf.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Single")]
        public void MaxNumberSingle0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfSingle.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Single")]
        public void MaxNumberSingle0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfSingle.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Double")]
        public void MaxNumberDouble0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfDouble.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Double")]
        public void MaxNumberDouble0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfDouble.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "NFloat")]
        public void MaxNumberNFloat0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfNFloat.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "NFloat")]
        public void MaxNumberNFloat0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfNFloat.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Decimal")]
        public void MaxNumberDecimal0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfDecimal.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "Decimal")]
        public void MaxNumberDecimal0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfDecimal.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "BigInteger")]
        public void MaxNumberBigInteger0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMaxNumber(_data0255OfBigInteger.Span);

        [Benchmark]
        [BenchmarkCategory("MaxNumber", "BigInteger")]
        public void MaxNumberBigInteger0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMaxNumber(_data0255OfBigInteger.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "SByte")]
        public void MinNumberSByte0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfSByte.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "SByte")]
        public void MinNumberSByte0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfSByte.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Byte")]
        public void MinNumberByte0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfByte.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Byte")]
        public void MinNumberByte0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfByte.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Int16")]
        public void MinNumberInt160255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfInt16.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Int16")]
        public void MinNumberInt160255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfInt16.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "UInt16")]
        public void MinNumberUInt160255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfUInt16.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "UInt16")]
        public void MinNumberUInt160255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfUInt16.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Char")]
        public void MinNumberChar0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfChar.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Char")]
        public void MinNumberChar0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfChar.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Int32")]
        public void MinNumberInt320255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfInt32.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Int32")]
        public void MinNumberInt320255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfInt32.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "UInt32")]
        public void MinNumberUInt320255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfUInt32.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "UInt32")]
        public void MinNumberUInt320255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfUInt32.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Int64")]
        public void MinNumberInt640255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfInt64.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Int64")]
        public void MinNumberInt640255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfInt64.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "UInt64")]
        public void MinNumberUInt640255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfUInt64.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "UInt64")]
        public void MinNumberUInt640255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfUInt64.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "IntPtr")]
        public void MinNumberIntPtr0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "IntPtr")]
        public void MinNumberIntPtr0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "UIntPtr")]
        public void MinNumberUIntPtr0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfUIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "UIntPtr")]
        public void MinNumberUIntPtr0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfUIntPtr.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Half")]
        public void MinNumberHalf0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfHalf.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Half")]
        public void MinNumberHalf0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfHalf.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Single")]
        public void MinNumberSingle0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfSingle.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Single")]
        public void MinNumberSingle0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfSingle.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Double")]
        public void MinNumberDouble0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfDouble.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Double")]
        public void MinNumberDouble0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfDouble.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "NFloat")]
        public void MinNumberNFloat0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfNFloat.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "NFloat")]
        public void MinNumberNFloat0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfNFloat.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Decimal")]
        public void MinNumberDecimal0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfDecimal.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "Decimal")]
        public void MinNumberDecimal0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfDecimal.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "BigInteger")]
        public void MinNumberBigInteger0255NonVectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.NonVectorizedMinNumber(_data0255OfBigInteger.Span);

        [Benchmark]
        [BenchmarkCategory("MinNumber", "BigInteger")]
        public void MinNumberBigInteger0255Vectorized() => _ = Experiment.CSharp.Library.VectorizedCalculation.VectorizedMinNumber(_data0255OfBigInteger.Span);
    }
}
