using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Test.Experiment.CSharp.Library
{
    public sealed partial class TestVectorizedCalculation
    {
        #region TestMaxSByte

        [TestMethod]
        public void TestMaxSByte() => TestMaxCore<SByte>();

        #endregion

        #region TestMaxSByteVectorized

        [TestMethod]
        public void TestMaxSByteVectorized() => TestMaxVectorizedCore<SByte>();

        #endregion

        #region TestMaxByte

        [TestMethod]
        public void TestMaxByte() => TestMaxCore<Byte>();

        #endregion

        #region TestMaxByteVectorized

        [TestMethod]
        public void TestMaxByteVectorized() => TestMaxVectorizedCore<Byte>();

        #endregion

        #region TestMaxInt16

        [TestMethod]
        public void TestMaxInt16() => TestMaxCore<Int16>();

        #endregion

        #region TestMaxInt16Vectorized

        [TestMethod]
        public void TestMaxInt16Vectorized() => TestMaxVectorizedCore<Int16>();

        #endregion

        #region TestMaxUInt16

        [TestMethod]
        public void TestMaxUInt16() => TestMaxCore<UInt16>();

        #endregion

        #region TestMaxUInt16Vectorized

        [TestMethod]
        public void TestMaxUInt16Vectorized() => TestMaxVectorizedCore<UInt16>();

        #endregion

        #region TestMaxChar

        [TestMethod]
        public void TestMaxChar() => TestMaxCore<Char>();

        #endregion

        #region TestMaxCharVectorized

        [TestMethod]
        public void TestMaxCharVectorized() => TestMaxVectorizedCore<Char>();

        #endregion

        #region TestMaxInt32

        [TestMethod]
        public void TestMaxInt32() => TestMaxCore<Int32>();

        #endregion

        #region TestMaxInt32Vectorized

        [TestMethod]
        public void TestMaxInt32Vectorized() => TestMaxVectorizedCore<Int32>();

        #endregion

        #region TestMaxUInt32

        [TestMethod]
        public void TestMaxUInt32() => TestMaxCore<UInt32>();

        #endregion

        #region TestMaxUInt32Vectorized

        [TestMethod]
        public void TestMaxUInt32Vectorized() => TestMaxVectorizedCore<UInt32>();

        #endregion

        #region TestMaxInt64

        [TestMethod]
        public void TestMaxInt64() => TestMaxCore<Int64>();

        #endregion

        #region TestMaxInt64Vectorized

        [TestMethod]
        public void TestMaxInt64Vectorized() => TestMaxVectorizedCore<Int64>();

        #endregion

        #region TestMaxUInt64

        [TestMethod]
        public void TestMaxUInt64() => TestMaxCore<UInt64>();

        #endregion

        #region TestMaxUInt64Vectorized

        [TestMethod]
        public void TestMaxUInt64Vectorized() => TestMaxVectorizedCore<UInt64>();

        #endregion

        #region TestMaxIntPtr

        [TestMethod]
        public void TestMaxIntPtr() => TestMaxCore<IntPtr>();

        #endregion

        #region TestMaxIntPtrVectorized

        [TestMethod]
        public void TestMaxIntPtrVectorized() => TestMaxVectorizedCore<IntPtr>();

        #endregion

        #region TestMaxUIntPtr

        [TestMethod]
        public void TestMaxUIntPtr() => TestMaxCore<UIntPtr>();

        #endregion

        #region TestMaxUIntPtrVectorized

        [TestMethod]
        public void TestMaxUIntPtrVectorized() => TestMaxVectorizedCore<UIntPtr>();

        #endregion

        #region TestMaxHalf

        [TestMethod]
        public void TestMaxHalf() => TestMaxIeee754FloatingCore<Half>();

        #endregion

        #region TestMaxHalfVectorized

        [TestMethod]
        public void TestMaxHalfVectorized() => TestMaxIeee754FloatingVectorizedCore<Half>();

        #endregion

        #region TestMaxSingle

        [TestMethod]
        public void TestMaxSingle() => TestMaxIeee754FloatingCore<Single>();

        #endregion

        #region TestMaxSingleVectorized

        [TestMethod]
        public void TestMaxSingleVectorized() => TestMaxIeee754FloatingVectorizedCore<Single>();

        #endregion

        #region TestMaxDouble

        [TestMethod]
        public void TestMaxDouble() => TestMaxIeee754FloatingCore<Double>();

        #endregion

        #region TestMaxDoubleVectorized

        [TestMethod]
        public void TestMaxDoubleVectorized() => TestMaxIeee754FloatingVectorizedCore<Double>();

        #endregion

        #region TestMaxNFloat

        [TestMethod]
        public void TestMaxNFloat() => TestMaxIeee754FloatingCore<NFloat>();

        #endregion

        #region TestMaxNFloatVectorized

        [TestMethod]
        public void TestMaxNFloatVectorized() => TestMaxIeee754FloatingVectorizedCore<NFloat>();

        #endregion

        #region TestMaxDecimal

        [TestMethod]
        public void TestMaxDecimal() => TestMaxCore<Decimal>();

        #endregion

        #region TestMaxDecimalVectorized

        [TestMethod]
        public void TestMaxDecimalVectorized() => TestMaxVectorizedCore<Decimal>();

        #endregion

        #region TestMaxBigInteger

        [TestMethod]
        public void TestMaxBigInteger() => TestMaxCore<BigInteger>();

        #endregion

        #region TestMaxBigIntegerVectorized

        [TestMethod]
        public void TestMaxBigIntegerVectorized() => TestMaxVectorizedCore<BigInteger>();

        #endregion

        #region TestMinSByte

        [TestMethod]
        public void TestMinSByte() => TestMinCore<SByte>();

        #endregion

        #region TestMinSByteVectorized

        [TestMethod]
        public void TestMinSByteVectorized() => TestMinVectorizedCore<SByte>();

        #endregion

        #region TestMinByte

        [TestMethod]
        public void TestMinByte() => TestMinCore<Byte>();

        #endregion

        #region TestMinByteVectorized

        [TestMethod]
        public void TestMinByteVectorized() => TestMinVectorizedCore<Byte>();

        #endregion

        #region TestMinInt16

        [TestMethod]
        public void TestMinInt16() => TestMinCore<Int16>();

        #endregion

        #region TestMinInt16Vectorized

        [TestMethod]
        public void TestMinInt16Vectorized() => TestMinVectorizedCore<Int16>();

        #endregion

        #region TestMinUInt16

        [TestMethod]
        public void TestMinUInt16() => TestMinCore<UInt16>();

        #endregion

        #region TestMinUInt16Vectorized

        [TestMethod]
        public void TestMinUInt16Vectorized() => TestMinVectorizedCore<UInt16>();

        #endregion

        #region TestMinChar

        [TestMethod]
        public void TestMinChar() => TestMinCore<Char>();

        #endregion

        #region TestMinCharVectorized

        [TestMethod]
        public void TestMinCharVectorized() => TestMinVectorizedCore<Char>();

        #endregion

        #region TestMinInt32

        [TestMethod]
        public void TestMinInt32() => TestMinCore<Int32>();

        #endregion

        #region TestMinInt32Vectorized

        [TestMethod]
        public void TestMinInt32Vectorized() => TestMinVectorizedCore<Int32>();

        #endregion

        #region TestMinUInt32

        [TestMethod]
        public void TestMinUInt32() => TestMinCore<UInt32>();

        #endregion

        #region TestMinUInt32Vectorized

        [TestMethod]
        public void TestMinUInt32Vectorized() => TestMinVectorizedCore<UInt32>();

        #endregion

        #region TestMinInt64

        [TestMethod]
        public void TestMinInt64() => TestMinCore<Int64>();

        #endregion

        #region TestMinInt64Vectorized

        [TestMethod]
        public void TestMinInt64Vectorized() => TestMinVectorizedCore<Int64>();

        #endregion

        #region TestMinUInt64

        [TestMethod]
        public void TestMinUInt64() => TestMinCore<UInt64>();

        #endregion

        #region TestMinUInt64Vectorized

        [TestMethod]
        public void TestMinUInt64Vectorized() => TestMinVectorizedCore<UInt64>();

        #endregion

        #region TestMinIntPtr

        [TestMethod]
        public void TestMinIntPtr() => TestMinCore<IntPtr>();

        #endregion

        #region TestMinIntPtrVectorized

        [TestMethod]
        public void TestMinIntPtrVectorized() => TestMinVectorizedCore<IntPtr>();

        #endregion

        #region TestMinUIntPtr

        [TestMethod]
        public void TestMinUIntPtr() => TestMinCore<UIntPtr>();

        #endregion

        #region TestMinUIntPtrVectorized

        [TestMethod]
        public void TestMinUIntPtrVectorized() => TestMinVectorizedCore<UIntPtr>();

        #endregion

        #region TestMinHalf

        [TestMethod]
        public void TestMinHalf() => TestMinIeee754FloatingCore<Half>();

        #endregion

        #region TestMinHalfVectorized

        [TestMethod]
        public void TestMinHalfVectorized() => TestMinIeee754FloatingVectorizedCore<Half>();

        #endregion

        #region TestMinSingle

        [TestMethod]
        public void TestMinSingle() => TestMinIeee754FloatingCore<Single>();

        #endregion

        #region TestMinSingleVectorized

        [TestMethod]
        public void TestMinSingleVectorized() => TestMinIeee754FloatingVectorizedCore<Single>();

        #endregion

        #region TestMinDouble

        [TestMethod]
        public void TestMinDouble() => TestMinIeee754FloatingCore<Double>();

        #endregion

        #region TestMinDoubleVectorized

        [TestMethod]
        public void TestMinDoubleVectorized() => TestMinIeee754FloatingVectorizedCore<Double>();

        #endregion

        #region TestMinNFloat

        [TestMethod]
        public void TestMinNFloat() => TestMinIeee754FloatingCore<NFloat>();

        #endregion

        #region TestMinNFloatVectorized

        [TestMethod]
        public void TestMinNFloatVectorized() => TestMinIeee754FloatingVectorizedCore<NFloat>();

        #endregion

        #region TestMinDecimal

        [TestMethod]
        public void TestMinDecimal() => TestMinCore<Decimal>();

        #endregion

        #region TestMinDecimalVectorized

        [TestMethod]
        public void TestMinDecimalVectorized() => TestMinVectorizedCore<Decimal>();

        #endregion

        #region TestMinBigInteger

        [TestMethod]
        public void TestMinBigInteger() => TestMinCore<BigInteger>();

        #endregion

        #region TestMinBigIntegerVectorized

        [TestMethod]
        public void TestMinBigIntegerVectorized() => TestMinVectorizedCore<BigInteger>();

        #endregion

        #region TestMaxNumberSByte

        [TestMethod]
        public void TestMaxNumberSByte() => TestMaxNumberCore<SByte>();

        #endregion

        #region TestMaxNumberSByteVectorized

        [TestMethod]
        public void TestMaxNumberSByteVectorized() => TestMaxNumberVectorizedCore<SByte>();

        #endregion

        #region TestMaxNumberByte

        [TestMethod]
        public void TestMaxNumberByte() => TestMaxNumberCore<Byte>();

        #endregion

        #region TestMaxNumberByteVectorized

        [TestMethod]
        public void TestMaxNumberByteVectorized() => TestMaxNumberVectorizedCore<Byte>();

        #endregion

        #region TestMaxNumberInt16

        [TestMethod]
        public void TestMaxNumberInt16() => TestMaxNumberCore<Int16>();

        #endregion

        #region TestMaxNumberInt16Vectorized

        [TestMethod]
        public void TestMaxNumberInt16Vectorized() => TestMaxNumberVectorizedCore<Int16>();

        #endregion

        #region TestMaxNumberUInt16

        [TestMethod]
        public void TestMaxNumberUInt16() => TestMaxNumberCore<UInt16>();

        #endregion

        #region TestMaxNumberUInt16Vectorized

        [TestMethod]
        public void TestMaxNumberUInt16Vectorized() => TestMaxNumberVectorizedCore<UInt16>();

        #endregion

        #region TestMaxNumberChar

        [TestMethod]
        public void TestMaxNumberChar() => TestMaxNumberCore<Char>();

        #endregion

        #region TestMaxNumberCharVectorized

        [TestMethod]
        public void TestMaxNumberCharVectorized() => TestMaxNumberVectorizedCore<Char>();

        #endregion

        #region TestMaxNumberInt32

        [TestMethod]
        public void TestMaxNumberInt32() => TestMaxNumberCore<Int32>();

        #endregion

        #region TestMaxNumberInt32Vectorized

        [TestMethod]
        public void TestMaxNumberInt32Vectorized() => TestMaxNumberVectorizedCore<Int32>();

        #endregion

        #region TestMaxNumberUInt32

        [TestMethod]
        public void TestMaxNumberUInt32() => TestMaxNumberCore<UInt32>();

        #endregion

        #region TestMaxNumberUInt32Vectorized

        [TestMethod]
        public void TestMaxNumberUInt32Vectorized() => TestMaxNumberVectorizedCore<UInt32>();

        #endregion

        #region TestMaxNumberInt64

        [TestMethod]
        public void TestMaxNumberInt64() => TestMaxNumberCore<Int64>();

        #endregion

        #region TestMaxNumberInt64Vectorized

        [TestMethod]
        public void TestMaxNumberInt64Vectorized() => TestMaxNumberVectorizedCore<Int64>();

        #endregion

        #region TestMaxNumberUInt64

        [TestMethod]
        public void TestMaxNumberUInt64() => TestMaxNumberCore<UInt64>();

        #endregion

        #region TestMaxNumberUInt64Vectorized

        [TestMethod]
        public void TestMaxNumberUInt64Vectorized() => TestMaxNumberVectorizedCore<UInt64>();

        #endregion

        #region TestMaxNumberIntPtr

        [TestMethod]
        public void TestMaxNumberIntPtr() => TestMaxNumberCore<IntPtr>();

        #endregion

        #region TestMaxNumberIntPtrVectorized

        [TestMethod]
        public void TestMaxNumberIntPtrVectorized() => TestMaxNumberVectorizedCore<IntPtr>();

        #endregion

        #region TestMaxNumberUIntPtr

        [TestMethod]
        public void TestMaxNumberUIntPtr() => TestMaxNumberCore<UIntPtr>();

        #endregion

        #region TestMaxNumberUIntPtrVectorized

        [TestMethod]
        public void TestMaxNumberUIntPtrVectorized() => TestMaxNumberVectorizedCore<UIntPtr>();

        #endregion

        #region TestMaxNumberHalf

        [TestMethod]
        public void TestMaxNumberHalf() => TestMaxNumberIeee754FloatingCore<Half>();

        #endregion

        #region TestMaxNumberHalfVectorized

        [TestMethod]
        public void TestMaxNumberHalfVectorized() => TestMaxNumberIeee754FloatingVectorizedCore<Half>();

        #endregion

        #region TestMaxNumberSingle

        [TestMethod]
        public void TestMaxNumberSingle() => TestMaxNumberIeee754FloatingCore<Single>();

        #endregion

        #region TestMaxNumberSingleVectorized

        [TestMethod]
        public void TestMaxNumberSingleVectorized() => TestMaxNumberIeee754FloatingVectorizedCore<Single>();

        #endregion

        #region TestMaxNumberDouble

        [TestMethod]
        public void TestMaxNumberDouble() => TestMaxNumberIeee754FloatingCore<Double>();

        #endregion

        #region TestMaxNumberDoubleVectorized

        [TestMethod]
        public void TestMaxNumberDoubleVectorized() => TestMaxNumberIeee754FloatingVectorizedCore<Double>();

        #endregion

        #region TestMaxNumberNFloat

        [TestMethod]
        public void TestMaxNumberNFloat() => TestMaxNumberIeee754FloatingCore<NFloat>();

        #endregion

        #region TestMaxNumberNFloatVectorized

        [TestMethod]
        public void TestMaxNumberNFloatVectorized() => TestMaxNumberIeee754FloatingVectorizedCore<NFloat>();

        #endregion

        #region TestMaxNumberDecimal

        [TestMethod]
        public void TestMaxNumberDecimal() => TestMaxNumberCore<Decimal>();

        #endregion

        #region TestMaxNumberDecimalVectorized

        [TestMethod]
        public void TestMaxNumberDecimalVectorized() => TestMaxNumberVectorizedCore<Decimal>();

        #endregion

        #region TestMaxNumberBigInteger

        [TestMethod]
        public void TestMaxNumberBigInteger() => TestMaxNumberCore<BigInteger>();

        #endregion

        #region TestMaxNumberBigIntegerVectorized

        [TestMethod]
        public void TestMaxNumberBigIntegerVectorized() => TestMaxNumberVectorizedCore<BigInteger>();

        #endregion

        #region TestMinNumberSByte

        [TestMethod]
        public void TestMinNumberSByte() => TestMinNumberCore<SByte>();

        #endregion

        #region TestMinNumberSByteVectorized

        [TestMethod]
        public void TestMinNumberSByteVectorized() => TestMinNumberVectorizedCore<SByte>();

        #endregion

        #region TestMinNumberByte

        [TestMethod]
        public void TestMinNumberByte() => TestMinNumberCore<Byte>();

        #endregion

        #region TestMinNumberByteVectorized

        [TestMethod]
        public void TestMinNumberByteVectorized() => TestMinNumberVectorizedCore<Byte>();

        #endregion

        #region TestMinNumberInt16

        [TestMethod]
        public void TestMinNumberInt16() => TestMinNumberCore<Int16>();

        #endregion

        #region TestMinNumberInt16Vectorized

        [TestMethod]
        public void TestMinNumberInt16Vectorized() => TestMinNumberVectorizedCore<Int16>();

        #endregion

        #region TestMinNumberUInt16

        [TestMethod]
        public void TestMinNumberUInt16() => TestMinNumberCore<UInt16>();

        #endregion

        #region TestMinNumberUInt16Vectorized

        [TestMethod]
        public void TestMinNumberUInt16Vectorized() => TestMinNumberVectorizedCore<UInt16>();

        #endregion

        #region TestMinNumberChar

        [TestMethod]
        public void TestMinNumberChar() => TestMinNumberCore<Char>();

        #endregion

        #region TestMinNumberCharVectorized

        [TestMethod]
        public void TestMinNumberCharVectorized() => TestMinNumberVectorizedCore<Char>();

        #endregion

        #region TestMinNumberInt32

        [TestMethod]
        public void TestMinNumberInt32() => TestMinNumberCore<Int32>();

        #endregion

        #region TestMinNumberInt32Vectorized

        [TestMethod]
        public void TestMinNumberInt32Vectorized() => TestMinNumberVectorizedCore<Int32>();

        #endregion

        #region TestMinNumberUInt32

        [TestMethod]
        public void TestMinNumberUInt32() => TestMinNumberCore<UInt32>();

        #endregion

        #region TestMinNumberUInt32Vectorized

        [TestMethod]
        public void TestMinNumberUInt32Vectorized() => TestMinNumberVectorizedCore<UInt32>();

        #endregion

        #region TestMinNumberInt64

        [TestMethod]
        public void TestMinNumberInt64() => TestMinNumberCore<Int64>();

        #endregion

        #region TestMinNumberInt64Vectorized

        [TestMethod]
        public void TestMinNumberInt64Vectorized() => TestMinNumberVectorizedCore<Int64>();

        #endregion

        #region TestMinNumberUInt64

        [TestMethod]
        public void TestMinNumberUInt64() => TestMinNumberCore<UInt64>();

        #endregion

        #region TestMinNumberUInt64Vectorized

        [TestMethod]
        public void TestMinNumberUInt64Vectorized() => TestMinNumberVectorizedCore<UInt64>();

        #endregion

        #region TestMinNumberIntPtr

        [TestMethod]
        public void TestMinNumberIntPtr() => TestMinNumberCore<IntPtr>();

        #endregion

        #region TestMinNumberIntPtrVectorized

        [TestMethod]
        public void TestMinNumberIntPtrVectorized() => TestMinNumberVectorizedCore<IntPtr>();

        #endregion

        #region TestMinNumberUIntPtr

        [TestMethod]
        public void TestMinNumberUIntPtr() => TestMinNumberCore<UIntPtr>();

        #endregion

        #region TestMinNumberUIntPtrVectorized

        [TestMethod]
        public void TestMinNumberUIntPtrVectorized() => TestMinNumberVectorizedCore<UIntPtr>();

        #endregion

        #region TestMinNumberHalf

        [TestMethod]
        public void TestMinNumberHalf() => TestMinNumberIeee754FloatingCore<Half>();

        #endregion

        #region TestMinNumberHalfVectorized

        [TestMethod]
        public void TestMinNumberHalfVectorized() => TestMinNumberIeee754FloatingVectorizedCore<Half>();

        #endregion

        #region TestMinNumberSingle

        [TestMethod]
        public void TestMinNumberSingle() => TestMinNumberIeee754FloatingCore<Single>();

        #endregion

        #region TestMinNumberSingleVectorized

        [TestMethod]
        public void TestMinNumberSingleVectorized() => TestMinNumberIeee754FloatingVectorizedCore<Single>();

        #endregion

        #region TestMinNumberDouble

        [TestMethod]
        public void TestMinNumberDouble() => TestMinNumberIeee754FloatingCore<Double>();

        #endregion

        #region TestMinNumberDoubleVectorized

        [TestMethod]
        public void TestMinNumberDoubleVectorized() => TestMinNumberIeee754FloatingVectorizedCore<Double>();

        #endregion

        #region TestMinNumberNFloat

        [TestMethod]
        public void TestMinNumberNFloat() => TestMinNumberIeee754FloatingCore<NFloat>();

        #endregion

        #region TestMinNumberNFloatVectorized

        [TestMethod]
        public void TestMinNumberNFloatVectorized() => TestMinNumberIeee754FloatingVectorizedCore<NFloat>();

        #endregion

        #region TestMinNumberDecimal

        [TestMethod]
        public void TestMinNumberDecimal() => TestMinNumberCore<Decimal>();

        #endregion

        #region TestMinNumberDecimalVectorized

        [TestMethod]
        public void TestMinNumberDecimalVectorized() => TestMinNumberVectorizedCore<Decimal>();

        #endregion

        #region TestMinNumberBigInteger

        [TestMethod]
        public void TestMinNumberBigInteger() => TestMinNumberCore<BigInteger>();

        #endregion

        #region TestMinNumberBigIntegerVectorized

        [TestMethod]
        public void TestMinNumberBigIntegerVectorized() => TestMinNumberVectorizedCore<BigInteger>();

        #endregion
    }
}
