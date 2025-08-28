using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static partial class ArrayExtensions
    {
        #region Sum

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, RESULT_T>(this ELEMENT_T[] array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);

            return array.AsReadOnlySpan().SumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, RESULT_T>(this ELEMENT_T?[] array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);

            return array.AsReadOnlySpan().SumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().SumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().SumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);

            return array.AsReadOnlySpan(offset).SumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, RESULT_T>(this ELEMENT_T?[] array, Int32 offset)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);

            return array.AsReadOnlySpan(offset).SumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).SumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).SumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).SumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, RESULT_T>(this ELEMENT_T?[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).SumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).SumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).SumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, RESULT_T>(this Span<ELEMENT_T> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.AsReadOnly().SumCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, RESULT_T>(this Span<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.AsReadOnly().SumCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.SumCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.SumCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, VALUE_T, RESULT_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().SumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, VALUE_T, RESULT_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().SumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.SumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T Sum<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.SumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        #endregion

        #region SumNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, RESULT_T>(this ELEMENT_T[] array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);

            return array.AsReadOnlySpan().SumNumberCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, RESULT_T>(this ELEMENT_T?[] array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);

            return array.AsReadOnlySpan().SumNumberCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);

            return array.AsReadOnlySpan(offset).SumNumberCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, RESULT_T>(this ELEMENT_T?[] array, Int32 offset)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);

            return array.AsReadOnlySpan(offset).SumNumberCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).SumNumberCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, RESULT_T>(this ELEMENT_T?[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).SumNumberCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, RESULT_T>(this Span<ELEMENT_T> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.AsReadOnly().SumNumberCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, RESULT_T>(this Span<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.AsReadOnly().SumNumberCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.SumNumberCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.SumNumberCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, VALUE_T, RESULT_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, VALUE_T, RESULT_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T SumNumber<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        #endregion

        #region UncheckedSum

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, RESULT_T>(this ELEMENT_T[] array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);

            return array.AsReadOnlySpan().UncheckedSumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, RESULT_T>(this ELEMENT_T?[] array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);

            return array.AsReadOnlySpan().UncheckedSumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan().UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);

            return array.AsReadOnlySpan(offset).UncheckedSumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, RESULT_T>(this ELEMENT_T?[] array, Int32 offset)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);

            return array.AsReadOnlySpan(offset).UncheckedSumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).UncheckedSumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, RESULT_T>(this ELEMENT_T?[] array, Int32 offset, Int32 count)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);

            return array.AsReadOnlySpan(offset, count).UncheckedSumCore<ELEMENT_T, RESULT_T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, VALUE_T, RESULT_T>(this ELEMENT_T[] array, Int32 offset, Int32 count, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, array.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, array.Length - offset);
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnlySpan(offset).UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, RESULT_T>(this Span<ELEMENT_T> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.AsReadOnly().UncheckedSumCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, RESULT_T>(this Span<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.AsReadOnly().UncheckedSumCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.UncheckedSumCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
            => array.UncheckedSumCore<ELEMENT_T, RESULT_T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, VALUE_T, RESULT_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, VALUE_T, RESULT_T>(this Span<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.AsReadOnly().UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static RESULT_T UncheckedSum<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            ArgumentNullException.ThrowIfNull(selector);

            return array.UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(selector);
        }

        #endregion

        #region SumCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T SumCore<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                checked
                {
                    sum += RESULT_T.CreateChecked(array[index]);
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T SumCore<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                var value = array[index];
                if (value is not null)
                {
                    checked
                    {
                        sum += RESULT_T.CreateChecked(value.Value);
                    }
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T SumCore<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                checked
                {
                    sum += RESULT_T.CreateChecked(selector(array[index]));
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T SumCore<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                var value = selector(array[index]);
                if (value is not null)
                {
                    checked
                    {
                        sum += RESULT_T.CreateChecked(value.Value);
                    }
                }
            }

            return sum;
        }

        #endregion

        #region SumNumberCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T SumNumberCore<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                var value = array[index];
                if (!ELEMENT_T.IsNaN(value))
                {
                    checked
                    {
                        sum += RESULT_T.CreateChecked(value);
                    }
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T SumNumberCore<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                var value = array[index];
                if (value is not null && !ELEMENT_T.IsNaN(value.Value))
                {
                    checked
                    {
                        sum += RESULT_T.CreateChecked(value.Value);
                    }
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                var value = selector(array[index]);
                if (!VALUE_T.IsNaN(value))
                {
                    checked
                    {
                        sum += RESULT_T.CreateChecked(value);
                    }
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T SumNumberCore<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                var value = selector(array[index]);
                if (value is not null && !VALUE_T.IsNaN(value.Value))
                {
                    checked
                    {
                        sum += RESULT_T.CreateChecked(value.Value);
                    }
                }
            }

            return sum;
        }

        #endregion

        #region UncheckedSumCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T UncheckedSumCore<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
                sum += RESULT_T.CreateTruncating(array[index]);
            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T UncheckedSumCore<ELEMENT_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T?> array)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                var value = array[index];
                if (value is not null)
                    sum += RESULT_T.CreateTruncating(value.Value);
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
                sum += RESULT_T.CreateTruncating(selector(array[index]));
            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T UncheckedSumCore<ELEMENT_T, VALUE_T, RESULT_T>(this ReadOnlySpan<ELEMENT_T> array, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            var sum = RESULT_T.Zero;
            for (var index = 0; index < array.Length; ++index)
            {
                var value = selector(array[index]);
                if (value is not null)
                    sum += RESULT_T.CreateTruncating(value.Value);
            }

            return sum;
        }

        #endregion
    }
}
