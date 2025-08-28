using System;
using System.Buffers;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Experiment.CSharp.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Experiment.CSharp.Library
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:メンバーを static に設定します", Justification = "テストメソッドはstaticであってはならないため。")]
    public sealed partial class TestVectorizedCalculation
    {
        private static readonly Random _randomIntegerGenerator = new(Environment.TickCount);
        private static readonly ReadOnlyMemory<Int32> _sizes = new[] { 1, 2, 6, 7, 8, 9, 14, 15, 16, 17, 30, 31, 32, 33, 62, 63, 64, 65, 126, 127, 128, 129 };

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMaxCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                foreach (var size in _sizes.Span)
                {
                    var buffer = (ReadOnlySpan<ELEMENT_T>)sourceBuffer[..size];
                    var expected = GetExpected(buffer);
                    var actual = VectorizedCalculation.NonVectorizedMax(buffer);
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
            {
                if (array.Length <= 0)
                    throw new Exception();
                var r = array[0];
                for (var index = 1; index < array.Length; ++index)
                    r = ELEMENT_T.Max(r, array[index]);
                return r;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMaxVectorizedCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                foreach (var size in _sizes.Span)
                {
                    var buffer = (ReadOnlySpan<ELEMENT_T>)sourceBuffer[..size];
                    var expected = GetExpected(buffer);
                    var actual = VectorizedCalculation.VectorizedMax(buffer);
                    try
                    {
                        Assert.AreEqual(expected, actual);
                    }
                    catch (AssertFailedException ex)
                    {
                        var elementTexts = new List<String>();
                        foreach (var element in buffer)
                            elementTexts.Add(element.ToString() ?? "null");
                        throw new Exception($"テストに失敗しました。: buffer=[{String.Join(", ", elementTexts)}]", ex);
                    }
                }
            }
            finally
            {
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
            {
                if (array.Length <= 0)
                    throw new Exception();
                var r = array[0];
                for (var index = 1; index < array.Length; ++index)
                    r = ELEMENT_T.Max(r, array[index]);
                return r;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMinCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                foreach (var size in _sizes.Span)
                {
                    var buffer = (ReadOnlySpan<ELEMENT_T>)sourceBuffer[..size];
                    var expected = GetExpected(buffer);
                    var actual = VectorizedCalculation.NonVectorizedMin(buffer);
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
            {
                if (array.Length <= 0)
                    throw new Exception();
                var r = array[0];
                for (var index = 1; index < array.Length; ++index)
                    r = ELEMENT_T.Min(r, array[index]);
                return r;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMinVectorizedCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                foreach (var size in _sizes.Span)
                {
                    var buffer = (ReadOnlySpan<ELEMENT_T>)sourceBuffer[..size];
                    var expected = GetExpected(buffer);
                    var actual = VectorizedCalculation.VectorizedMin(buffer);
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
            {
                if (array.Length <= 0)
                    throw new Exception();
                var r = array[0];
                for (var index = 1; index < array.Length; ++index)
                    r = ELEMENT_T.Min(r, array[index]);
                return r;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMaxNumberCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                foreach (var size in _sizes.Span)
                {
                    var buffer = (ReadOnlySpan<ELEMENT_T>)sourceBuffer[..size];
                    var expected = GetExpected(buffer);
                    var actual = VectorizedCalculation.NonVectorizedMaxNumber(buffer);
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
            {
                if (array.Length <= 0)
                    throw new Exception();
                var r = array[0];
                for (var index = 1; index < array.Length; ++index)
                    r = ELEMENT_T.MaxNumber(r, array[index]);
                return r;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMaxNumberVectorizedCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                foreach (var size in _sizes.Span)
                {
                    var buffer = (ReadOnlySpan<ELEMENT_T>)sourceBuffer[..size];
                    var expected = GetExpected(buffer);
                    var actual = VectorizedCalculation.VectorizedMaxNumber(buffer);
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
            {
                if (array.Length <= 0)
                    throw new Exception();
                var r = array[0];
                for (var index = 1; index < array.Length; ++index)
                    r = ELEMENT_T.MaxNumber(r, array[index]);
                return r;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMinNumberCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                foreach (var size in _sizes.Span)
                {
                    var buffer = (ReadOnlySpan<ELEMENT_T>)sourceBuffer[..size];
                    var expected = GetExpected(buffer);
                    var actual = VectorizedCalculation.NonVectorizedMinNumber(buffer);
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
            {
                if (array.Length <= 0)
                    throw new Exception();
                var r = array[0];
                for (var index = 1; index < array.Length; ++index)
                    r = ELEMENT_T.MinNumber(r, array[index]);
                return r;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMinNumberVectorizedCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                foreach (var size in _sizes.Span)
                {
                    var buffer = (ReadOnlySpan<ELEMENT_T>)sourceBuffer[..size];
                    var expected = GetExpected(buffer);
                    var actual = VectorizedCalculation.VectorizedMinNumber(buffer);
                    Assert.AreEqual(expected, actual);
                }
            }
            finally
            {
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
            {
                if (array.Length <= 0)
                    throw new Exception();
                var r = array[0];
                for (var index = 1; index < array.Length; ++index)
                    r = ELEMENT_T.MinNumber(r, array[index]);
                return r;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMaxIeee754FloatingCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>, IFloatingPointIeee754<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            var buffer = (ELEMENT_T[]?)null;
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                // NaNなし
                foreach (var size in _sizes.Span)
                    Test(sourceBuffer.AsSpan(0, size));

                buffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);

                // 特定の一か所あるいは二か所だけNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne1 in _sizes.Span)
                    {
                        if (posPlusOne1 <= size)
                        {
                            foreach (var posPlusOne2 in _sizes.Span)
                            {
                                if (posPlusOne2 <= size && posPlusOne2 >= posPlusOne1)
                                {
                                    sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                                    buffer[posPlusOne1 - 1] = ELEMENT_T.NaN;
                                    buffer[posPlusOne2 - 1] = ELEMENT_T.NaN;
                                    Test(buffer.AsSpan(0, size));
                                }
                            }
                        }
                    }
                }

                // 特定の場所より前がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(0, Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }

                // 特定の場所より後がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }
            }
            finally
            {
                if (buffer is not null)
                    ArrayPool<ELEMENT_T>.Shared.Return(buffer);
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static void Test(ReadOnlySpan<ELEMENT_T> buffer)
            {
                var expected = GetExpected(buffer);
                var actual = VectorizedCalculation.NonVectorizedMax(buffer);
                try
                {
                    Assert.AreEqual(expected, actual);
                }
                catch (AssertFailedException ex)
                {
                    var elementTexts = new List<String>();
                    foreach (var element in buffer)
                        elementTexts.Add(element?.ToString() ?? "null");
                    throw new Exception($"テストに失敗しました。: buffer=[{String.Join(", ", elementTexts)}]", ex);
                }

                static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
                {
                    if (array.Length <= 0)
                        throw new Exception();
                    var r = array[0];
                    for (var index = 1; index < array.Length; ++index)
                        r = ELEMENT_T.Max(r, array[index]);
                    return r;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMaxIeee754FloatingVectorizedCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>, IFloatingPointIeee754<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            var buffer = (ELEMENT_T[]?)null;
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                // NaNなし
                foreach (var size in _sizes.Span)
                    Test(sourceBuffer.AsSpan(0, size));

                buffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);

                // 特定の一か所あるいは二か所だけNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne1 in _sizes.Span)
                    {
                        if (posPlusOne1 <= size)
                        {
                            foreach (var posPlusOne2 in _sizes.Span)
                            {
                                if (posPlusOne2 <= size && posPlusOne2 >= posPlusOne1)
                                {
                                    sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                                    buffer[posPlusOne1 - 1] = ELEMENT_T.NaN;
                                    buffer[posPlusOne2 - 1] = ELEMENT_T.NaN;
                                    Test(buffer.AsSpan(0, size));
                                }
                            }
                        }
                    }
                }

                // 特定の場所より前がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(0, Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }

                // 特定の場所より後がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }
            }
            finally
            {
                if (buffer is not null)
                    ArrayPool<ELEMENT_T>.Shared.Return(buffer);
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static void Test(ReadOnlySpan<ELEMENT_T> buffer)
            {
                var expected = GetExpected(buffer);
                var actual = VectorizedCalculation.VectorizedMax(buffer);
                try
                {
                    Assert.AreEqual(expected, actual);
                }
                catch (AssertFailedException ex)
                {
                    var elementTexts = new List<String>();
                    foreach (var element in buffer)
                        elementTexts.Add(element?.ToString() ?? "null");
                    throw new Exception($"テストに失敗しました。: buffer=[{String.Join(", ", elementTexts)}]", ex);
                }

                static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
                {
                    if (array.Length <= 0)
                        throw new Exception();
                    var r = array[0];
                    for (var index = 1; index < array.Length; ++index)
                        r = ELEMENT_T.Max(r, array[index]);
                    return r;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMinIeee754FloatingCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>, IFloatingPointIeee754<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            var buffer = (ELEMENT_T[]?)null;
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                // NaNなし
                foreach (var size in _sizes.Span)
                    Test(sourceBuffer.AsSpan(0, size));

                buffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);

                // 特定の一か所あるいは二か所だけNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne1 in _sizes.Span)
                    {
                        if (posPlusOne1 <= size)
                        {
                            foreach (var posPlusOne2 in _sizes.Span)
                            {
                                if (posPlusOne2 <= size && posPlusOne2 >= posPlusOne1)
                                {
                                    sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                                    buffer[posPlusOne1 - 1] = ELEMENT_T.NaN;
                                    buffer[posPlusOne2 - 1] = ELEMENT_T.NaN;
                                    Test(buffer.AsSpan(0, size));
                                }
                            }
                        }
                    }
                }

                // 特定の場所より前がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(0, Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }

                // 特定の場所より後がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }
            }
            finally
            {
                if (buffer is not null)
                    ArrayPool<ELEMENT_T>.Shared.Return(buffer);
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static void Test(ReadOnlySpan<ELEMENT_T> buffer)
            {
                var expected = GetExpected(buffer);
                var actual = VectorizedCalculation.NonVectorizedMin(buffer);
                try
                {
                    Assert.AreEqual(expected, actual);
                }
                catch (AssertFailedException ex)
                {
                    var elementTexts = new List<String>();
                    foreach (var element in buffer)
                        elementTexts.Add(element?.ToString() ?? "null");
                    throw new Exception($"テストに失敗しました。: buffer=[{String.Join(", ", elementTexts)}]", ex);
                }

                static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
                {
                    if (array.Length <= 0)
                        throw new Exception();
                    var r = array[0];
                    for (var index = 1; index < array.Length; ++index)
                        r = ELEMENT_T.Min(r, array[index]);
                    return r;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMinIeee754FloatingVectorizedCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>, IFloatingPointIeee754<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            var buffer = (ELEMENT_T[]?)null;
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                // NaNなし
                foreach (var size in _sizes.Span)
                    Test(sourceBuffer.AsSpan(0, size));

                buffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);

                // 特定の一か所あるいは二か所だけNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne1 in _sizes.Span)
                    {
                        if (posPlusOne1 <= size)
                        {
                            foreach (var posPlusOne2 in _sizes.Span)
                            {
                                if (posPlusOne2 <= size && posPlusOne2 >= posPlusOne1)
                                {
                                    sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                                    buffer[posPlusOne1 - 1] = ELEMENT_T.NaN;
                                    buffer[posPlusOne2 - 1] = ELEMENT_T.NaN;
                                    Test(buffer.AsSpan(0, size));
                                }
                            }
                        }
                    }
                }

                // 特定の場所より前がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(0, Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }

                // 特定の場所より後がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }
            }
            finally
            {
                if (buffer is not null)
                    ArrayPool<ELEMENT_T>.Shared.Return(buffer);
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static void Test(ReadOnlySpan<ELEMENT_T> buffer)
            {
                var expected = GetExpected(buffer);
                var actual = VectorizedCalculation.VectorizedMin(buffer);
                try
                {
                    Assert.AreEqual(expected, actual);
                }
                catch (AssertFailedException ex)
                {
                    var elementTexts = new List<String>();
                    foreach (var element in buffer)
                        elementTexts.Add(element?.ToString() ?? "null");
                    throw new Exception($"テストに失敗しました。: buffer=[{String.Join(", ", elementTexts)}]", ex);
                }

                static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
                {
                    if (array.Length <= 0)
                        throw new Exception();
                    var r = array[0];
                    for (var index = 1; index < array.Length; ++index)
                        r = ELEMENT_T.Min(r, array[index]);
                    return r;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMaxNumberIeee754FloatingCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>, IFloatingPointIeee754<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            var buffer = (ELEMENT_T[]?)null;
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                // NaNなし
                foreach (var size in _sizes.Span)
                    Test(sourceBuffer.AsSpan(0, size));

                buffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);

                // 特定の一か所あるいは二か所だけNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne1 in _sizes.Span)
                    {
                        if (posPlusOne1 <= size)
                        {
                            foreach (var posPlusOne2 in _sizes.Span)
                            {
                                if (posPlusOne2 <= size && posPlusOne2 >= posPlusOne1)
                                {
                                    sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                                    buffer[posPlusOne1 - 1] = ELEMENT_T.NaN;
                                    buffer[posPlusOne2 - 1] = ELEMENT_T.NaN;
                                    Test(buffer.AsSpan(0, size));
                                }
                            }
                        }
                    }
                }

                // 特定の場所より前がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(0, Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }

                // 特定の場所より後がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }
            }
            finally
            {
                if (buffer is not null)
                    ArrayPool<ELEMENT_T>.Shared.Return(buffer);
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static void Test(ReadOnlySpan<ELEMENT_T> buffer)
            {
                var expected = GetExpected(buffer);
                var actual = VectorizedCalculation.NonVectorizedMaxNumber(buffer);
                Assert.AreEqual(expected, actual);

                static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
                {
                    if (array.Length <= 0)
                        throw new Exception();
                    var r = array[0];
                    for (var index = 1; index < array.Length; ++index)
                        r = ELEMENT_T.MaxNumber(r, array[index]);
                    return r;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMaxNumberIeee754FloatingVectorizedCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>, IFloatingPointIeee754<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            var buffer = (ELEMENT_T[]?)null;
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                // NaNなし
                foreach (var size in _sizes.Span)
                    Test(sourceBuffer.AsSpan(0, size));

                buffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);

                // 特定の一か所あるいは二か所だけNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne1 in _sizes.Span)
                    {
                        if (posPlusOne1 <= size)
                        {
                            foreach (var posPlusOne2 in _sizes.Span)
                            {
                                if (posPlusOne2 <= size && posPlusOne2 >= posPlusOne1)
                                {
                                    sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                                    buffer[posPlusOne1 - 1] = ELEMENT_T.NaN;
                                    buffer[posPlusOne2 - 1] = ELEMENT_T.NaN;
                                    Test(buffer.AsSpan(0, size));
                                }
                            }
                        }
                    }
                }

                // 特定の場所より前がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(0, Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }

                // 特定の場所より後がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }
            }
            finally
            {
                if (buffer is not null)
                    ArrayPool<ELEMENT_T>.Shared.Return(buffer);
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static void Test(ReadOnlySpan<ELEMENT_T> buffer)
            {
                var expected = GetExpected(buffer);
                var actual = VectorizedCalculation.VectorizedMaxNumber(buffer);
                Assert.AreEqual(expected, actual);

                static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
                {
                    if (array.Length <= 0)
                        throw new Exception();
                    var r = array[0];
                    for (var index = 1; index < array.Length; ++index)
                        r = ELEMENT_T.MaxNumber(r, array[index]);
                    return r;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMinNumberIeee754FloatingCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>, IFloatingPointIeee754<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            var buffer = (ELEMENT_T[]?)null;
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                // NaNなし
                foreach (var size in _sizes.Span)
                    Test(sourceBuffer.AsSpan(0, size));

                buffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);

                // 特定の一か所あるいは二か所だけNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne1 in _sizes.Span)
                    {
                        if (posPlusOne1 <= size)
                        {
                            foreach (var posPlusOne2 in _sizes.Span)
                            {
                                if (posPlusOne2 <= size && posPlusOne2 >= posPlusOne1)
                                {
                                    sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                                    buffer[posPlusOne1 - 1] = ELEMENT_T.NaN;
                                    buffer[posPlusOne2 - 1] = ELEMENT_T.NaN;
                                    Test(buffer.AsSpan(0, size));
                                }
                            }
                        }
                    }
                }

                // 特定の場所より前がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(0, Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }

                // 特定の場所より後がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }
            }
            finally
            {
                if (buffer is not null)
                    ArrayPool<ELEMENT_T>.Shared.Return(buffer);
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static void Test(ReadOnlySpan<ELEMENT_T> buffer)
            {
                var expected = GetExpected(buffer);
                var actual = VectorizedCalculation.NonVectorizedMinNumber(buffer);
                Assert.AreEqual(expected, actual);

                static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
                {
                    if (array.Length <= 0)
                        throw new Exception();
                    var r = array[0];
                    for (var index = 1; index < array.Length; ++index)
                        r = ELEMENT_T.MinNumber(r, array[index]);
                    return r;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private void TestMinNumberIeee754FloatingVectorizedCore<ELEMENT_T>()
            where ELEMENT_T : INumber<ELEMENT_T>, IFloatingPointIeee754<ELEMENT_T>
        {
            var sourceBuffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);
            var buffer = (ELEMENT_T[]?)null;
            try
            {
                for (var index = 0; index < sourceBuffer.Length; ++index)
                    sourceBuffer[index] = unchecked(ELEMENT_T.CreateTruncating(_randomIntegerGenerator.Next()));

                // NaNなし
                foreach (var size in _sizes.Span)
                    Test(sourceBuffer.AsSpan(0, size));

                buffer = ArrayPool<ELEMENT_T>.Shared.Rent(129);

                // 特定の一か所あるいは二か所だけNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne1 in _sizes.Span)
                    {
                        if (posPlusOne1 <= size)
                        {
                            foreach (var posPlusOne2 in _sizes.Span)
                            {
                                if (posPlusOne2 <= size && posPlusOne2 >= posPlusOne1)
                                {
                                    sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                                    buffer[posPlusOne1 - 1] = ELEMENT_T.NaN;
                                    buffer[posPlusOne2 - 1] = ELEMENT_T.NaN;
                                    Test(buffer.AsSpan(0, size));
                                }
                            }
                        }
                    }
                }

                // 特定の場所より前がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(0, Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }

                // 特定の場所より後がすべてNaN
                foreach (var size in _sizes.Span)
                {
                    foreach (var posPlusOne in _sizes.Span)
                    {
                        if (posPlusOne <= size)
                        {
                            sourceBuffer.AsSpan(0, size).CopyTo(buffer.AsSpan(0, size));
                            buffer.AsSpan(Int32.Min(size, posPlusOne)).Fill(ELEMENT_T.NaN);
                            Test(buffer.AsSpan(0, size));
                        }
                    }
                }
            }
            finally
            {
                if (buffer is not null)
                    ArrayPool<ELEMENT_T>.Shared.Return(buffer);
                ArrayPool<ELEMENT_T>.Shared.Return(sourceBuffer);
            }

            static void Test(ReadOnlySpan<ELEMENT_T> buffer)
            {
                var expected = GetExpected(buffer);
                var actual = VectorizedCalculation.VectorizedMinNumber(buffer);
                Assert.AreEqual(expected, actual);

                static ELEMENT_T GetExpected(ReadOnlySpan<ELEMENT_T> array)
                {
                    if (array.Length <= 0)
                        throw new Exception();
                    var r = array[0];
                    for (var index = 1; index < array.Length; ++index)
                        r = ELEMENT_T.MinNumber(r, array[index]);
                    return r;
                }
            }
        }
    }
}
