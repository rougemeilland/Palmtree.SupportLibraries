using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Palmtree
{
    public static partial class ArrayExtensions
    {
        #region Max

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Max<ELEMENT_T>(this ELEMENT_T[] array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnlySpan().MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? Max<ELEMENT_T>(this ELEMENT_T?[] array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnlySpan().MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T Max<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? Max<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Max<ELEMENT_T>(this ELEMENT_T[] array, Int32 offset)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);

            return array.AsReadOnlySpan(offset).MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? Max<ELEMENT_T>(this ELEMENT_T?[] array, Int32 offset)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);

            return array.AsReadOnlySpan(offset).MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T Max<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? Max<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Max<ELEMENT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? Max<ELEMENT_T>(this ELEMENT_T?[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T Max<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset, count).MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? Max<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset, count).MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Max<ELEMENT_T>(this Span<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnly().MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? Max<ELEMENT_T>(this Span<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnly().MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T Max<ELEMENT_T, VALUE_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? Max<ELEMENT_T, VALUE_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Max<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? Max<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.MaxCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T Max<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.MaxCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? Max<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.MaxCore(selector);
        }

        #endregion

        #region Min

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Min<ELEMENT_T>(this ELEMENT_T[] array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnlySpan().MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? Min<ELEMENT_T>(this ELEMENT_T?[] array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnlySpan().MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T Min<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? Min<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Min<ELEMENT_T>(this ELEMENT_T[] array, Int32 offset)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);

            return array.AsReadOnlySpan(offset).MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? Min<ELEMENT_T>(this ELEMENT_T?[] array, Int32 offset)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);

            return array.AsReadOnlySpan(offset).MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T Min<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? Min<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Min<ELEMENT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? Min<ELEMENT_T>(this ELEMENT_T?[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T Min<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset, count).MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? Min<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset, count).MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Min<ELEMENT_T>(this Span<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnly().MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? Min<ELEMENT_T>(this Span<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnly().MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T Min<ELEMENT_T, VALUE_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? Min<ELEMENT_T, VALUE_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Min<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? Min<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.MinCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T Min<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.MinCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? Min<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.MinCore(selector);
        }

        #endregion

        #region MaxNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MaxNumber<ELEMENT_T>(this ELEMENT_T[] array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnlySpan().MaxNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? MaxNumber<ELEMENT_T>(this ELEMENT_T?[] array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnlySpan().MaxNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T MaxNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().MaxNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? MaxNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().MaxNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MaxNumber<ELEMENT_T>(this ELEMENT_T[] array, Int32 offset)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);

            return array.AsReadOnlySpan(offset).MaxNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? MaxNumber<ELEMENT_T>(this ELEMENT_T?[] array, Int32 offset)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);

            return array.AsReadOnlySpan(offset).MaxNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T MaxNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).MaxNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? MaxNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).MaxNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MaxNumber<ELEMENT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).MaxNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? MaxNumber<ELEMENT_T>(this ELEMENT_T?[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).MaxNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T MaxNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset, count).MaxNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? MaxNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset, count).MaxNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MaxNumber<ELEMENT_T>(this Span<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnly().MaxNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? MaxNumber<ELEMENT_T>(this Span<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnly().MaxNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T MaxNumber<ELEMENT_T, VALUE_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().MaxNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? MaxNumber<ELEMENT_T, VALUE_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().MaxNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MaxNumber<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.MaxNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? MaxNumber<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.MaxNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T MaxNumber<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.MaxNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? MaxNumber<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.MaxNumberCore(selector);
        }

        #endregion

        #region MinNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MinNumber<ELEMENT_T>(this ELEMENT_T[] array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnlySpan().MinNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? MinNumber<ELEMENT_T>(this ELEMENT_T?[] array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnlySpan().MinNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T MinNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().MinNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? MinNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().MinNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MinNumber<ELEMENT_T>(this ELEMENT_T[] array, Int32 offset)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);

            return array.AsReadOnlySpan(offset).MinNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? MinNumber<ELEMENT_T>(this ELEMENT_T?[] array, Int32 offset)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);

            return array.AsReadOnlySpan(offset).MinNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T MinNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).MinNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? MinNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).MinNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MinNumber<ELEMENT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).MinNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? MinNumber<ELEMENT_T>(this ELEMENT_T?[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).MinNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T MinNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset, count).MinNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? MinNumber<ELEMENT_T, VALUE_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset, count).MinNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MinNumber<ELEMENT_T>(this Span<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnly().MinNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? MinNumber<ELEMENT_T>(this Span<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.AsReadOnly().MinNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T MinNumber<ELEMENT_T, VALUE_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().MinNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? MinNumber<ELEMENT_T, VALUE_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().MinNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MinNumber<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.MinNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T? MinNumber<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");

            return array.MinNumberCore();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T MinNumber<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.MinNumberCore(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static VALUE_T? MinNumber<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            if (array.Length <= 0)
                throw new InvalidOperationException($"\"{nameof(array)}.Length\" is zero.");
            ArgumentNullException.ThrowIfNull(selector);

            return array.MinNumberCore(selector);
        }

        #endregion

        #region MaxCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxCore<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            Validation.Assert(array.Length > 0);

            if (typeof(ELEMENT_T) == typeof(Char) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)(Char)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);

            if (typeof(ELEMENT_T) == typeof(SByte) && Vector<SByte>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, SByte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int16) && Vector<Int16>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Int16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int32) && Vector<Int32>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int64) && Vector<Int64>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32) && Vector<Int32>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64) && Vector<Int64>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw Validation.GetFatalErrorException();
            }

            if (typeof(ELEMENT_T) == typeof(Byte) && Vector<Byte>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Byte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt16) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt32) && Vector<UInt32>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt64) && Vector<UInt64>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32) && Vector<UInt32>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64) && Vector<UInt64>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw Validation.GetFatalErrorException();
            }

#if NET9_0_OR_GREATER
            if (typeof(ELEMENT_T) == typeof(Single) && Vector<Single>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Double) && Vector<Double>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single) && Vector<Single>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double) && Vector<Double>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                throw Validation.GetFatalErrorException();
            }
#endif

            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32))
                    return (ELEMENT_T)(Object)new IntPtr(MaxCoreByNonVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64))
                    return (ELEMENT_T)(Object)new IntPtr(MaxCoreByNonVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32))
                    return (ELEMENT_T)(Object)new UIntPtr(MaxCoreByNonVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64))
                    return (ELEMENT_T)(Object)new UIntPtr(MaxCoreByNonVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single))
                    return (ELEMENT_T)(Object)new NFloat(MaxCoreByNonVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double))
                    return (ELEMENT_T)(Object)new NFloat(MaxCoreByNonVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            return MaxCoreByNonVector(ref MemoryMarshal.GetReference(array), (UInt32)array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T? MaxCore<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            Validation.Assert(array.Length > 0);

            var max = array[0];
            for (var index = 1; index < array.Length; ++index)
            {
                var value = array[index];
                if (max is null)
                {
                    max = value;
                }
                else if (value is null)
                {
                }
                else
                {
                    max = ELEMENT_T.Max(max.Value, value.Value);
                }
            }

            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T MaxCore<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            Validation.Assert(array.Length > 0);

            var max = selector(array[0]);
            for (var index = 1; index < array.Length; ++index)
                max = VALUE_T.Max(max, selector(array[index]));
            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T? MaxCore<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            Validation.Assert(array.Length > 0);

            var max = selector(array[0]);
            for (var index = 1; index < array.Length; ++index)
            {
                var value = selector(array[index]);
                if (max is null)
                {
                    max = value;
                }
                else if (value is null)
                {
                }
                else
                {
                    max = VALUE_T.Max(max.Value, value.Value);
                }
            }

            return max;
        }

        #endregion

        #region MinCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinCore<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            Validation.Assert(array.Length > 0);

            if (typeof(ELEMENT_T) == typeof(Char) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)(Char)MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);

            if (typeof(ELEMENT_T) == typeof(SByte) && Vector<SByte>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, SByte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int16) && Vector<Int16>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Int16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int32) && Vector<Int32>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int64) && Vector<Int64>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32) && Vector<Int32>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(MinCoreByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64) && Vector<Int64>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(MinCoreByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw Validation.GetFatalErrorException();
            }

            if (typeof(ELEMENT_T) == typeof(Byte) && Vector<Byte>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Byte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt16) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt32) && Vector<UInt32>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt64) && Vector<UInt64>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32) && Vector<UInt32>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64) && Vector<UInt64>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw Validation.GetFatalErrorException();
            }

#if NET9_0_OR_GREATER
            if (typeof(ELEMENT_T) == typeof(Single) && Vector<Single>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Double) && Vector<Double>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single) && Vector<Single>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double) && Vector<Double>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                throw Validation.GetFatalErrorException();
            }
#endif

            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32))
                    return (ELEMENT_T)(Object)new IntPtr(MinCoreByNonVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64))
                    return (ELEMENT_T)(Object)new IntPtr(MinCoreByNonVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32))
                    return (ELEMENT_T)(Object)new UIntPtr(MinCoreByNonVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64))
                    return (ELEMENT_T)(Object)new UIntPtr(MinCoreByNonVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single))
                    return (ELEMENT_T)(Object)new NFloat(MinCoreByNonVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double))
                    return (ELEMENT_T)(Object)new NFloat(MinCoreByNonVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            return MinCoreByNonVector(ref MemoryMarshal.GetReference(array), (UInt32)array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T? MinCore<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            Validation.Assert(array.Length > 0);

            var min = array[0];
            for (var index = 1; index < array.Length; ++index)
            {
                var value = array[index];
                if (min is null)
                    return null;
                else if (value is null)
                    return null;
                else
                    min = ELEMENT_T.Min(min.Value, value.Value);
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T MinCore<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            Validation.Assert(array.Length > 0);

            var min = selector(array[0]);
            for (var index = 1; index < array.Length; ++index)
                min = VALUE_T.Min(min, selector(array[index]));
            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T? MinCore<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            Validation.Assert(array.Length > 0);

            var min = selector(array[0]);
            for (var index = 1; index < array.Length; ++index)
            {
                var value = selector(array[index]);
                if (min is null)
                    return null;
                else if (value is null)
                    return null;
                else
                    min = VALUE_T.Min(min.Value, value.Value);
            }

            return min;
        }

        #endregion

        #region MaxNumberCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxNumberCore<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            Validation.Assert(array.Length > 0);

            if (typeof(ELEMENT_T) == typeof(Char) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)(Char)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);

            if (typeof(ELEMENT_T) == typeof(SByte) && Vector<SByte>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, SByte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int16) && Vector<Int16>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Int16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int32) && Vector<Int32>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int64) && Vector<Int64>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32) && Vector<Int32>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64) && Vector<Int64>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw Validation.GetFatalErrorException();
            }

            if (typeof(ELEMENT_T) == typeof(Byte) && Vector<Byte>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, Byte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt16) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt32) && Vector<UInt32>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt64) && Vector<UInt64>.IsSupported)
                return (ELEMENT_T)(Object)MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32) && Vector<UInt32>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64) && Vector<UInt64>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(MaxCoreByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw Validation.GetFatalErrorException();
            }

#if NET9_0_OR_GREATER
            if (typeof(ELEMENT_T) == typeof(Single) && Vector<Single>.IsSupported)
                return (ELEMENT_T)(Object)MaxNumberCoreByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Double) && Vector<Double>.IsSupported)
                return (ELEMENT_T)(Object)MaxNumberCoreByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single) && Vector<Single>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)MaxNumberCoreByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double) && Vector<Double>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)MaxNumberCoreByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                throw Validation.GetFatalErrorException();
            }
#endif

            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32))
                    return (ELEMENT_T)(Object)new IntPtr(MaxNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64))
                    return (ELEMENT_T)(Object)new IntPtr(MaxNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32))
                    return (ELEMENT_T)(Object)new UIntPtr(MaxNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64))
                    return (ELEMENT_T)(Object)new UIntPtr(MaxNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single))
                    return (ELEMENT_T)(Object)new NFloat(MaxNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double))
                    return (ELEMENT_T)(Object)new NFloat(MaxNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            return MaxNumberCoreByNonVector(ref MemoryMarshal.GetReference(array), (UInt32)array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T? MaxNumberCore<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            Validation.Assert(array.Length > 0);

            var max = array[0];
            for (var index = 1; index < array.Length; ++index)
            {
                var value = array[index];
                if (max is null)
                {
                    max = value;
                }
                else if (value is null)
                {
                }
                else
                {
                    max = ELEMENT_T.MaxNumber(max.Value, value.Value);
                }
            }

            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T MaxNumberCore<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            Validation.Assert(array.Length > 0);

            var max = selector(array[0]);
            for (var index = 1; index < array.Length; ++index)
                max = VALUE_T.MaxNumber(max, selector(array[index]));
            return max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T? MaxNumberCore<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            Validation.Assert(array.Length > 0);

            var max = selector(array[0]);
            for (var index = 1; index < array.Length; ++index)
            {
                var value = selector(array[index]);
                if (max is null)
                {
                    max = value;
                }
                else if (value is null)
                {
                }
                else
                {
                    max = VALUE_T.MaxNumber(max.Value, value.Value);
                }
            }

            return max;
        }

        #endregion

        #region MinNumberCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinNumberCore<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            Validation.Assert(array.Length > 0);

            if (typeof(ELEMENT_T) == typeof(Char) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)(Char)MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);

            if (typeof(ELEMENT_T) == typeof(SByte) && Vector<SByte>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, SByte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int16) && Vector<Int16>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Int16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int32) && Vector<Int32>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Int64) && Vector<Int64>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32) && Vector<Int32>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(MinCoreByVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64) && Vector<Int64>.IsSupported)
                    return (ELEMENT_T)(Object)new IntPtr(MinCoreByVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw Validation.GetFatalErrorException();
            }

            if (typeof(ELEMENT_T) == typeof(Byte) && Vector<Byte>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, Byte>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt16) && Vector<UInt16>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt32) && Vector<UInt32>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UInt64) && Vector<UInt64>.IsSupported)
                return (ELEMENT_T)(Object)MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32) && Vector<UInt32>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64) && Vector<UInt64>.IsSupported)
                    return (ELEMENT_T)(Object)new UIntPtr(MinCoreByVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                throw Validation.GetFatalErrorException();
            }

#if NET9_0_OR_GREATER
            if (typeof(ELEMENT_T) == typeof(Single) && Vector<Single>.IsSupported)
                return (ELEMENT_T)(Object)MinNumberCoreByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(Double) && Vector<Double>.IsSupported)
                return (ELEMENT_T)(Object)MinNumberCoreByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single) && Vector<Single>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)MinNumberCoreByVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double) && Vector<Double>.IsSupported)
                    return (ELEMENT_T)(Object)(NFloat)MinNumberCoreByVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length);
                throw Validation.GetFatalErrorException();
            }
#endif

            if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int32))
                    return (ELEMENT_T)(Object)new IntPtr(MinNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Int64))
                    return (ELEMENT_T)(Object)new IntPtr(MinNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt32))
                    return (ELEMENT_T)(Object)new UIntPtr(MinNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(UInt64))
                    return (ELEMENT_T)(Object)new UIntPtr(MinNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Single))
                    return (ELEMENT_T)(Object)new NFloat(MinNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
                if (Unsafe.SizeOf<ELEMENT_T>() == sizeof(Double))
                    return (ELEMENT_T)(Object)new NFloat(MinNumberCoreByNonVector(ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array)), (UInt32)array.Length));
            }

            return MinNumberCoreByNonVector(ref MemoryMarshal.GetReference(array), (UInt32)array.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T? MinNumberCore<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumber<ELEMENT_T>
        {
            Validation.Assert(array.Length > 0);

            var min = array[0];
            for (var index = 1; index < array.Length; ++index)
            {
                var value = array[index];
                if (min is null)
                {
                    min = value;
                }
                else if (value is null)
                {
                }
                else
                {
                    min = ELEMENT_T.MinNumber(min.Value, value.Value);
                }
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T MinNumberCore<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            Validation.Assert(array.Length > 0);

            var min = selector(array[0]);
            for (var index = 1; index < array.Length; ++index)
                min = VALUE_T.MinNumber(min, selector(array[index]));
            return min;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T? MinNumberCore<ELEMENT_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumber<VALUE_T>
        {
            Validation.Assert(array.Length > 0);

            var min = selector(array[0]);
            for (var index = 1; index < array.Length; ++index)
            {
                var value = selector(array[index]);
                if (min is null)
                {
                    min = value;
                }
                else if (value is null)
                {
                }
                else
                {
                    min = VALUE_T.MinNumber(min.Value, value.Value);
                }
            }

            return min;
        }

        #endregion

        #region MaxCoreByVector

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxCoreByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(elementLength > 0);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MaxCoreByVector512(ref array, elementLength);
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MaxCoreByVector256(ref array, elementLength);
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MaxCoreByVector128(ref array, elementLength);
            else
                return (ELEMENT_T)(Object)MaxCoreByNonVector(ref array, elementLength);
        }

        #endregion

        #region MinCoreByVector

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinCoreByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(elementLength > 0);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MinCoreByVector512(ref array, elementLength);
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MinCoreByVector256(ref array, elementLength);
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MinCoreByVector128(ref array, elementLength);
            else
                return (ELEMENT_T)(Object)MinCoreByNonVector(ref array, elementLength);
        }

        #endregion

        #region MaxNumberCoreByVector

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxNumberCoreByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(elementLength > 0);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MaxNumberCoreByVector512(ref array, elementLength);
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MaxNumberCoreByVector256(ref array, elementLength);
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MaxNumberCoreByVector128(ref array, elementLength);
            else
                return (ELEMENT_T)(Object)MaxNumberCoreByNonVector(ref array, elementLength);
        }
#endif

        #endregion

        #region MinNumberCoreByVector

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinNumberCoreByVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(elementLength > 0);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector512<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MinNumberCoreByVector512(ref array, elementLength);
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector256<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MinNumberCoreByVector256(ref array, elementLength);
            else if (Vector128.IsHardwareAccelerated && Vector128<ELEMENT_T>.IsSupported && elementLength >= (UInt32)Vector128<ELEMENT_T>.Count )
                return (ELEMENT_T)(Object)MinNumberCoreByVector128(ref array, elementLength);
            else
                return (ELEMENT_T)(Object)MinNumberCoreByNonVector(ref array, elementLength);
        }
#endif

        #endregion
    }
}
