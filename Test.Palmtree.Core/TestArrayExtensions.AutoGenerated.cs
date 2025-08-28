using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Palmtree;

namespace Test.Palmtree.Core
{
    public partial class TestArrayExtensions
    {
        #region TestSequenceCompareTo

        [TestMethod]
        public void TestSequenceCompareToSByte()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new SByte[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new SByte[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new SByte[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (SByte)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<SByte> left, ReadOnlySpan<SByte> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToByte()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new Byte[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new Byte[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new Byte[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (Byte)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<Byte> left, ReadOnlySpan<Byte> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToInt16()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new Int16[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new Int16[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new Int16[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (Int16)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<Int16> left, ReadOnlySpan<Int16> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToUInt16()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new UInt16[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new UInt16[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new UInt16[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (UInt16)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<UInt16> left, ReadOnlySpan<UInt16> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToChar()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new Char[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new Char[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new Char[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (Char)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<Char> left, ReadOnlySpan<Char> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToInt32()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new Int32[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new Int32[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new Int32[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (Int32)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<Int32> left, ReadOnlySpan<Int32> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToUInt32()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new UInt32[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new UInt32[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new UInt32[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (UInt32)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<UInt32> left, ReadOnlySpan<UInt32> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToInt64()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new Int64[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new Int64[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new Int64[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (Int64)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<Int64> left, ReadOnlySpan<Int64> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToUInt64()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new UInt64[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new UInt64[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new UInt64[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (UInt64)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<UInt64> left, ReadOnlySpan<UInt64> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToIntPtr()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new IntPtr[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new IntPtr[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new IntPtr[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (IntPtr)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<IntPtr> left, ReadOnlySpan<IntPtr> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToUIntPtr()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new UIntPtr[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new UIntPtr[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new UIntPtr[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (UIntPtr)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<UIntPtr> left, ReadOnlySpan<UIntPtr> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToSingle()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new Single[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new Single[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new Single[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (Single)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<Single> left, ReadOnlySpan<Single> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToDouble()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new Double[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new Double[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new Double[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (Double)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<Double> left, ReadOnlySpan<Double> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToNFloat()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new NFloat[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new NFloat[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new NFloat[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (NFloat)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<NFloat> left, ReadOnlySpan<NFloat> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToDecimal()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new Decimal[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new Decimal[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new Decimal[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (Decimal)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<Decimal> left, ReadOnlySpan<Decimal> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        public void TestSequenceCompareToBigInteger()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new BigInteger[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomData(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new BigInteger[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new BigInteger[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                unchecked
                                {
                                    right[differencePoint] += (BigInteger)differenceValue;
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<BigInteger> left, ReadOnlySpan<BigInteger> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        [TestMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1310:正確さのために StringComparison を指定する", Justification = "テストのために CompareTo を呼び出すことが必要")]
        public void TestSequenceCompareToString()
        {
            foreach (var lengthOfLeft in _listOfLength)
            {
                foreach (var lengthORight in new[] { lengthOfLeft - 2, lengthOfLeft - 1, lengthOfLeft, lengthOfLeft + 1, lengthOfLeft + 2 }.Where(length => length >= 0))
                {
                    var source = new String[Int32.Max(lengthOfLeft, lengthORight)];
                    FillRandomString(source.AsSpan());
                    foreach (var differencePoint in _listOfLength.Where(length => length < lengthOfLeft && length < lengthORight).Prepend(-1))
                    {
                        foreach (var differenceValue in new[] { -1, 0, +1 })
                        {
                            var left = new String[lengthOfLeft];
                            source.Slice(0, left.Length).CopyTo(left);
                            var right = new String[lengthORight];
                            source.Slice(0, right.Length).CopyTo(right);
                            if (differencePoint >= 0)
                            {
                                if (differenceValue < 0)
                                {
                                    right[differencePoint] = new String('	', 16);
                                }
                                else if (differenceValue > 0)
                                {
                                    right[differencePoint] = new String('', 16);
                                }
                                else
                                {
                                }
                            }

                            var expected = GetExpected(left, right);
                            var actual = left.SequenceCompareTo(right);
                            Assert.AreEqual(Int32.Sign(expected), Int32.Sign(actual), $"left={FormatArray(left.AsReadOnlySpan())}, right={FormatArray(right.AsReadOnlySpan())}, expected={expected}, actual={actual}");
                        }
                    }
                }
            }

            static Int32 GetExpected(ReadOnlySpan<String> left, ReadOnlySpan<String> right)
            {
                var length = Int32.Min(left.Length, right.Length);
                for (var index = 0; index < length; ++index)
                {
                    var c = left[index].CompareTo(right[index]);
                    if (c != 0)
                        return c;
                }

                return left.Length - right.Length;
            }
        }

        #endregion

        #region TestMaxSByte

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxSByte() => TestMaxCore<SByte>();

        #endregion

        #region TestMaxByte

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxByte() => TestMaxCore<Byte>();

        #endregion

        #region TestMaxInt16

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxInt16() => TestMaxCore<Int16>();

        #endregion

        #region TestMaxUInt16

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxUInt16() => TestMaxCore<UInt16>();

        #endregion

        #region TestMaxChar

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxChar() => TestMaxCore<Char>();

        #endregion

        #region TestMaxInt32

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxInt32() => TestMaxCore<Int32>();

        #endregion

        #region TestMaxUInt32

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxUInt32() => TestMaxCore<UInt32>();

        #endregion

        #region TestMaxInt64

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxInt64() => TestMaxCore<Int64>();

        #endregion

        #region TestMaxUInt64

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxUInt64() => TestMaxCore<UInt64>();

        #endregion

        #region TestMaxIntPtr

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxIntPtr() => TestMaxCore<IntPtr>();

        #endregion

        #region TestMaxUIntPtr

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxUIntPtr() => TestMaxCore<UIntPtr>();

        #endregion

        #region TestMaxHalf

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxHalf() => TestMaxIeee754FloatingCore<Half>();

        #endregion

        #region TestMaxSingle

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxSingle() => TestMaxIeee754FloatingCore<Single>();

        #endregion

        #region TestMaxDouble

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxDouble() => TestMaxIeee754FloatingCore<Double>();

        #endregion

        #region TestMaxNFloat

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNFloat() => TestMaxIeee754FloatingCore<NFloat>();

        #endregion

        #region TestMaxDecimal

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxDecimal() => TestMaxCore<Decimal>();

        #endregion

        #region TestMaxBigInteger

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxBigInteger() => TestMaxCore<BigInteger>();

        #endregion

        #region TestMinSByte

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinSByte() => TestMinCore<SByte>();

        #endregion

        #region TestMinByte

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinByte() => TestMinCore<Byte>();

        #endregion

        #region TestMinInt16

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinInt16() => TestMinCore<Int16>();

        #endregion

        #region TestMinUInt16

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinUInt16() => TestMinCore<UInt16>();

        #endregion

        #region TestMinChar

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinChar() => TestMinCore<Char>();

        #endregion

        #region TestMinInt32

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinInt32() => TestMinCore<Int32>();

        #endregion

        #region TestMinUInt32

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinUInt32() => TestMinCore<UInt32>();

        #endregion

        #region TestMinInt64

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinInt64() => TestMinCore<Int64>();

        #endregion

        #region TestMinUInt64

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinUInt64() => TestMinCore<UInt64>();

        #endregion

        #region TestMinIntPtr

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinIntPtr() => TestMinCore<IntPtr>();

        #endregion

        #region TestMinUIntPtr

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinUIntPtr() => TestMinCore<UIntPtr>();

        #endregion

        #region TestMinHalf

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinHalf() => TestMinIeee754FloatingCore<Half>();

        #endregion

        #region TestMinSingle

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinSingle() => TestMinIeee754FloatingCore<Single>();

        #endregion

        #region TestMinDouble

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinDouble() => TestMinIeee754FloatingCore<Double>();

        #endregion

        #region TestMinNFloat

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNFloat() => TestMinIeee754FloatingCore<NFloat>();

        #endregion

        #region TestMinDecimal

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinDecimal() => TestMinCore<Decimal>();

        #endregion

        #region TestMinBigInteger

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinBigInteger() => TestMinCore<BigInteger>();

        #endregion

        #region TestMaxNumberSByte

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberSByte() => TestMaxNumberCore<SByte>();

        #endregion

        #region TestMaxNumberByte

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberByte() => TestMaxNumberCore<Byte>();

        #endregion

        #region TestMaxNumberInt16

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberInt16() => TestMaxNumberCore<Int16>();

        #endregion

        #region TestMaxNumberUInt16

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberUInt16() => TestMaxNumberCore<UInt16>();

        #endregion

        #region TestMaxNumberChar

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberChar() => TestMaxNumberCore<Char>();

        #endregion

        #region TestMaxNumberInt32

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberInt32() => TestMaxNumberCore<Int32>();

        #endregion

        #region TestMaxNumberUInt32

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberUInt32() => TestMaxNumberCore<UInt32>();

        #endregion

        #region TestMaxNumberInt64

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberInt64() => TestMaxNumberCore<Int64>();

        #endregion

        #region TestMaxNumberUInt64

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberUInt64() => TestMaxNumberCore<UInt64>();

        #endregion

        #region TestMaxNumberIntPtr

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberIntPtr() => TestMaxNumberCore<IntPtr>();

        #endregion

        #region TestMaxNumberUIntPtr

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberUIntPtr() => TestMaxNumberCore<UIntPtr>();

        #endregion

        #region TestMaxNumberHalf

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberHalf() => TestMaxNumberIeee754FloatingCore<Half>();

        #endregion

        #region TestMaxNumberSingle

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberSingle() => TestMaxNumberIeee754FloatingCore<Single>();

        #endregion

        #region TestMaxNumberDouble

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberDouble() => TestMaxNumberIeee754FloatingCore<Double>();

        #endregion

        #region TestMaxNumberNFloat

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberNFloat() => TestMaxNumberIeee754FloatingCore<NFloat>();

        #endregion

        #region TestMaxNumberDecimal

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberDecimal() => TestMaxNumberCore<Decimal>();

        #endregion

        #region TestMaxNumberBigInteger

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMaxNumberBigInteger() => TestMaxNumberCore<BigInteger>();

        #endregion

        #region TestMinNumberSByte

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberSByte() => TestMinNumberCore<SByte>();

        #endregion

        #region TestMinNumberByte

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberByte() => TestMinNumberCore<Byte>();

        #endregion

        #region TestMinNumberInt16

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberInt16() => TestMinNumberCore<Int16>();

        #endregion

        #region TestMinNumberUInt16

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberUInt16() => TestMinNumberCore<UInt16>();

        #endregion

        #region TestMinNumberChar

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberChar() => TestMinNumberCore<Char>();

        #endregion

        #region TestMinNumberInt32

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberInt32() => TestMinNumberCore<Int32>();

        #endregion

        #region TestMinNumberUInt32

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberUInt32() => TestMinNumberCore<UInt32>();

        #endregion

        #region TestMinNumberInt64

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberInt64() => TestMinNumberCore<Int64>();

        #endregion

        #region TestMinNumberUInt64

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberUInt64() => TestMinNumberCore<UInt64>();

        #endregion

        #region TestMinNumberIntPtr

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberIntPtr() => TestMinNumberCore<IntPtr>();

        #endregion

        #region TestMinNumberUIntPtr

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberUIntPtr() => TestMinNumberCore<UIntPtr>();

        #endregion

        #region TestMinNumberHalf

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberHalf() => TestMinNumberIeee754FloatingCore<Half>();

        #endregion

        #region TestMinNumberSingle

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberSingle() => TestMinNumberIeee754FloatingCore<Single>();

        #endregion

        #region TestMinNumberDouble

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberDouble() => TestMinNumberIeee754FloatingCore<Double>();

        #endregion

        #region TestMinNumberNFloat

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberNFloat() => TestMinNumberIeee754FloatingCore<NFloat>();

        #endregion

        #region TestMinNumberDecimal

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberDecimal() => TestMinNumberCore<Decimal>();

        #endregion

        #region TestMinNumberBigInteger

        [TestMethod]
        [Timeout(10 * 1000)]
        public void TestMinNumberBigInteger() => TestMinNumberCore<BigInteger>();

        #endregion
    }
}
