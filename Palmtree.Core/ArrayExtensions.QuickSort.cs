//#define DEBUG_QUICKSORT
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static partial class ArrayExtensions
    {
        #region QuickSort (ELEMENT_T[])

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);

            sourceArray.AsSpan().QuickSortCore();
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(keySelecter);

            sourceArray.AsSpan().QuickSortCore(keySelecter);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(comparer);

            sourceArray.AsSpan().QuickSortCore(comparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            sourceArray.AsSpan().QuickSortCore(keySelecter, keyComparer);
            return sourceArray;
        }

#if false // これらのオーバーロードは拡張メソッドにして使用する場合にパラメタの順序が紛らわしいので削除する
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);

            var (offset, count) = sourceArray.GetOffsetAndLength(range);
            sourceArray.AsSpan(offset, count).QuickSortCore();
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Range range, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(keySelecter);

            var (offset, count) = sourceArray.GetOffsetAndLength(range);
            sourceArray.AsSpan(offset, count).QuickSortCore(keySelecter);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(comparer);

            var (offset, count) = sourceArray.GetOffsetAndLength(range);
            sourceArray.AsSpan(offset, count).QuickSortCore(comparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Range range, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            var (offset, count) = sourceArray.GetOffsetAndLength(range);
            sourceArray.AsSpan(offset, count).QuickSortCore(keySelecter, keyComparer);
            return sourceArray;
        }
#endif

#if false // これらのオーバーロードは拡張メソッドにして使用する場合にパラメタの順序が紛らわしいので削除する
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Length - offset);

            sourceArray.AsSpan(offset, count).QuickSortCore();
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);

            sourceArray.AsSpan(offset, count).QuickSortCore(keySelecter);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 count, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(comparer);

            sourceArray.AsSpan(offset, count).QuickSortCore(comparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            sourceArray.AsSpan(offset, count).QuickSortCore(keySelecter, keyComparer);
            return sourceArray;
        }
#endif

#if false // これらのオーバーロードは拡張メソッドにして使用する場合にパラメタの順序が紛らわしいので削除する
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Length - offset);

            sourceArray.AsSpan(checked((Int32)offset), checked((Int32)count)).QuickSortCore();
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);

            sourceArray.AsSpan(checked((Int32)offset), checked((Int32)count)).QuickSortCore(keySelecter);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(comparer);

            sourceArray.AsSpan(checked((Int32)offset), checked((Int32)count)).QuickSortCore(comparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            sourceArray.AsSpan(checked((Int32)offset), checked((Int32)count)).QuickSortCore(keySelecter, keyComparer);
            return sourceArray;
        }
#endif

        #endregion

        #region QuickSort (Span<ELEMENT_T>)

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Span<ELEMENT_T> QuickSort<ELEMENT_T>(this Span<ELEMENT_T> sourceArray)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            sourceArray.QuickSortCore();
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Span<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> sourceArray, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);

            sourceArray.QuickSortCore(keySelecter);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Span<ELEMENT_T> QuickSort<ELEMENT_T>(this Span<ELEMENT_T> sourceArray, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(comparer);

            sourceArray.QuickSortCore(comparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Span<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> sourceArray, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            sourceArray.QuickSortCore(keySelecter, keyComparer);
            return sourceArray;
        }

        #endregion

        #region QuickSort (IList<ELEMENT_T>)

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T>(this IList<ELEMENT_T> sourceArray)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);

            QuickSortCore(sourceArray, 0, sourceArray.Count - 1);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IList<ELEMENT_T> sourceArray, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(keySelecter);

            QuickSortCore(sourceArray, 0, sourceArray.Count - 1, keySelecter);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T>(this IList<ELEMENT_T> sourceArray, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(comparer);

            QuickSortCore(sourceArray, 0, sourceArray.Count - 1, comparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IList<ELEMENT_T> sourceArray, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            QuickSortCore(sourceArray, 0, sourceArray.Count - 1, keySelecter, keyComparer);
            return sourceArray;
        }

#if false // これらのオーバーロードは拡張メソッドにして使用する場合にパラメタの順序が紛らわしいので削除する
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T>(this IList<ELEMENT_T> sourceArray, Int32 offset, Int32 count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Count);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Count - offset);

            QuickSortCore(sourceArray, offset, offset + count - 1);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IList<ELEMENT_T> sourceArray, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Count);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Count - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);

            QuickSortCore(sourceArray, offset, offset + count - 1, keySelecter);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T>(this IList<ELEMENT_T> sourceArray, Int32 offset, Int32 count, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Count);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Count - offset);
            ArgumentNullException.ThrowIfNull(comparer);

            QuickSortCore(sourceArray, offset, offset + count - 1, comparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IList<ELEMENT_T> sourceArray, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Count);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Count - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            QuickSortCore(sourceArray, offset, offset + count - 1, keySelecter, keyComparer);
            return sourceArray;
        }
#endif

#if false // これらのオーバーロードは拡張メソッドにして使用する場合にパラメタの順序が紛らわしいので削除する
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T>(this IList<ELEMENT_T> sourceArray, UInt32 offset, UInt32 count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Count - offset);

            QuickSortCore(sourceArray, checked((Int32)offset), checked((Int32)(offset + count - 1)));
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IList<ELEMENT_T> sourceArray, UInt32 offset, UInt32 count, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Count - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);

            QuickSortCore(sourceArray, checked((Int32)offset), checked((Int32)(offset + count - 1)), keySelecter);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T>(this IList<ELEMENT_T> sourceArray, UInt32 offset, UInt32 count, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Count - offset);
            ArgumentNullException.ThrowIfNull(comparer);

            QuickSortCore(sourceArray, checked((Int32)offset), checked((Int32)(offset + count - 1)), comparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IList<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IList<ELEMENT_T> sourceArray, UInt32 offset, UInt32 count, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Count - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            QuickSortCore(sourceArray, checked((Int32)offset), checked((Int32)(offset + count - 1)), keySelecter, keyComparer);
            return sourceArray;
        }
#endif

        #endregion

        #region QuickSort (IArray<ELEMENT_T>)

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T>(this IArray<ELEMENT_T> sourceArray)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);

            QuickSortCore(sourceArray, 0, sourceArray.Length - 1);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IArray<ELEMENT_T> sourceArray, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(keySelecter);

            QuickSortCore(sourceArray, 0, sourceArray.Length - 1, keySelecter);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T>(this IArray<ELEMENT_T> sourceArray, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(comparer);

            QuickSortCore(sourceArray, 0, sourceArray.Length - 1, comparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IArray<ELEMENT_T> sourceArray, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            QuickSortCore(sourceArray, 0, sourceArray.Length - 1, keySelecter, keyComparer);
            return sourceArray;
        }

#if false // これらのオーバーロードは拡張メソッドにして使用する場合にパラメタの順序が紛らわしいので削除する
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T>(this IArray<ELEMENT_T> sourceArray, Int32 offset, Int32 count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Length - offset);

            QuickSortCore(sourceArray, offset, offset + count - 1);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IArray<ELEMENT_T> sourceArray, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);

            QuickSortCore(sourceArray, offset, offset + count - 1, keySelecter);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T>(this IArray<ELEMENT_T> sourceArray, Int32 offset, Int32 count, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(comparer);

            QuickSortCore(sourceArray, offset, offset + count - 1, comparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IArray<ELEMENT_T> sourceArray, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            QuickSortCore(sourceArray, offset, offset + count - 1, keySelecter, keyComparer);
            return sourceArray;
        }
#endif

#if false // これらのオーバーロードは拡張メソッドにして使用する場合にパラメタの順序が紛らわしいので削除する
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T>(this IArray<ELEMENT_T> sourceArray, UInt32 offset, UInt32 count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Length - offset);

            QuickSortCore(sourceArray, checked((Int32)offset), checked((Int32)(offset + count - 1)));
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IArray<ELEMENT_T> sourceArray, UInt32 offset, UInt32 count, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);

            QuickSortCore(sourceArray, checked((Int32)offset), checked((Int32)(offset + count - 1)), keySelecter);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T>(this IArray<ELEMENT_T> sourceArray, UInt32 offset, UInt32 count, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(comparer);

            QuickSortCore(sourceArray, checked((Int32)offset), checked((Int32)(offset + count - 1)), comparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IArray<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IArray<ELEMENT_T> sourceArray, UInt32 offset, UInt32 count, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Length - offset);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            QuickSortCore(sourceArray, checked((Int32)offset), checked((Int32)(offset + count - 1)), keySelecter, keyComparer);
            return sourceArray;
        }
#endif

        #endregion

        #region QuickSortCore (Span<ELEMENT_T>)

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static void QuickSortCore<ELEMENT_T>(this Span<ELEMENT_T> source)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({0}, {source.Length - 1}) {source.Length} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (source.Length <= 1)
                    return;
                if (source.Length == 2)
                {
                    if (source[0].CompareTo(source[^1]) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={0}, index2={source.Length - 1}");
#endif
                        (source[0], source[^1]) = (source[^1], source[0]);
                    }

                    return;
                }

                var pivotKey = source[source.Length / 2];
                var lowerBoundary = 0;
                var upperBoundary = source.Length - 1;
                var startOfPivotKeys = 0;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, source.Length)    : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = source[lowerBoundary].CompareTo(pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(source[lowerBoundary].CompareTo(pivotKey) > 0);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(source[lowerBoundary].CompareTo(pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(source[lowerBoundary].CompareTo(pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || source[lowerBoundary].CompareTo(pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = source[upperBoundary].CompareTo(pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(source[upperBoundary].CompareTo(pivotKey) < 0 && source[lowerBoundary].CompareTo(pivotKey) > 0 && source[startOfPivotKeys].CompareTo(pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(source[upperBoundary].CompareTo(pivotKey) < 0 && source[lowerBoundary].CompareTo(pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, source.Length)    : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source[..startOfPivotKeys]);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source[lowerBoundary..]);
#if DEBUG
            }
            finally
            {
                AssertSortResult<ELEMENT_T>(source, 0, source.Length - 1);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({0}, {source.Length - 1}) {source.Length - 1 - 0 + 1} bytes");
#endif
            }
#endif
        }

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static void QuickSortCore<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({0}, {source.Length - 1}) {source.Length} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (source.Length <= 1)
                    return;
                if (source.Length == 2)
                {
                    if (keySelector(source[0]).CompareTo(keySelector(source[^1])) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={0}, index2={source.Length - 1}");
#endif
                        (source[0], source[^1]) = (source[^1], source[0]);
                    }

                    return;
                }

                var pivotKey = keySelector(source[source.Length / 2]);
                var lowerBoundary = 0;
                var upperBoundary = source.Length - 1;
                var startOfPivotKeys = 0;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, source.Length)    : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keySelector(source[lowerBoundary]).CompareTo(pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keySelector(source[upperBoundary]).CompareTo(pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(keySelector(source[upperBoundary]).CompareTo(pivotKey) < 0 && keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0 && keySelector(source[startOfPivotKeys]).CompareTo(pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(keySelector(source[upperBoundary]).CompareTo(pivotKey) < 0 && keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, source.Length)    : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source[..startOfPivotKeys], keySelector);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source[lowerBoundary..], keySelector);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, 0, source.Length - 1, keySelector);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({0}, {source.Length - 1}) {source.Length - 1 - 0 + 1} bytes");
#endif
            }
#endif
        }

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static void QuickSortCore<ELEMENT_T>(this Span<ELEMENT_T> source, IComparer<ELEMENT_T> comparer)
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({0}, {source.Length - 1}) {source.Length} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (source.Length <= 1)
                    return;
                if (source.Length == 2)
                {
                    if (comparer.Compare(source[0], source[^1]) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={0}, index2={source.Length - 1}");
#endif
                        (source[0], source[^1]) = (source[^1], source[0]);
                    }

                    return;
                }

                var pivotKey = source[source.Length / 2];
                var lowerBoundary = 0;
                var upperBoundary = source.Length - 1;
                var startOfPivotKeys = 0;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, source.Length)    : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = comparer.Compare(source[lowerBoundary], pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(comparer.Compare(source[lowerBoundary], pivotKey) > 0);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(comparer.Compare(source[lowerBoundary], pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(comparer.Compare(source[lowerBoundary], pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || comparer.Compare(source[lowerBoundary], pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = comparer.Compare(source[upperBoundary], pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(comparer.Compare(source[upperBoundary], pivotKey) < 0 && comparer.Compare(source[lowerBoundary], pivotKey) > 0 && comparer.Compare(source[startOfPivotKeys], pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(comparer.Compare(source[upperBoundary], pivotKey) < 0 && comparer.Compare(source[lowerBoundary], pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, source.Length)    : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source[..startOfPivotKeys], comparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source[lowerBoundary..], comparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, 0, source.Length - 1, comparer);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({0}, {source.Length - 1}) {source.Length - 1 - 0 + 1} bytes");
#endif
            }
#endif
        }

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static void QuickSortCore<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({0}, {source.Length -1}) {source.Length} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (source.Length <= 1)
                    return;
                if (source.Length == 2)
                {
                    if (keyComparer.Compare(keySelector(source[0]), keySelector(source[^1])) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={0}, index2={source.Length - 1}");
#endif
                        (source[0], source[^1]) = (source[^1], source[0]);
                    }

                    return;
                }

                var pivotKey = keySelector(source[source.Length / 2]);
                var lowerBoundary = 0;
                var upperBoundary = source.Length - 1;
                var startOfPivotKeys = 0;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, source.Length)    : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey) < 0 && keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0 && keyComparer.Compare(keySelector(source[startOfPivotKeys]), pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey) < 0 && keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, source.Length)    : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source[..startOfPivotKeys], keySelector, keyComparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source[lowerBoundary..], keySelector, keyComparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, 0, source.Length - 1, keySelector, keyComparer);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({0}, {source.Length - 1}) {source.Length} bytes");
#endif
            }
#endif
        }

#if DEBUG
        private static void AssertSortResult<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(source[index].CompareTo(source[index + 1]) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(keySelector(source[index + 1])) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, IComparer<ELEMENT_T> keyComparer)
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(keyComparer.Compare(source[index], source[index + 1]) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), keySelector(source[index + 1])) <= 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys, IComparer<ELEMENT_T> comparer)
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) > 0);
        }
        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
            for (var index = startIndex; index < startfPivotKeys; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) < 0);
            for (var index = startfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys, IComparer<ELEMENT_T> comparer)
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) > 0);
        }

#endif

        #endregion

        #region QuickSortCore (IList<ELEMENT_T>)

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void QuickSortCore<ELEMENT_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (endIndex <= startIndex)
                    return;
                if (endIndex - startIndex == 1)
                {
                    if (source[startIndex].CompareTo(source[endIndex]) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startIndex}, index2={endIndex}");
#endif
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    }

                    return;
                }

                var pivotKey = source[(startIndex + endIndex) / 2];
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var startOfPivotKeys = startIndex;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = source[lowerBoundary].CompareTo(pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(source[lowerBoundary].CompareTo(pivotKey) > 0);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(source[lowerBoundary].CompareTo(pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(source[lowerBoundary].CompareTo(pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || source[lowerBoundary].CompareTo(pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = source[upperBoundary].CompareTo(pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(source[upperBoundary].CompareTo(pivotKey) < 0 && source[lowerBoundary].CompareTo(pivotKey) > 0 && source[startOfPivotKeys].CompareTo(pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(source[upperBoundary].CompareTo(pivotKey) < 0 && source[lowerBoundary].CompareTo(pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, startIndex, startOfPivotKeys - 1);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, lowerBoundary, endIndex);
#if DEBUG
            }
            finally
            {
                AssertSortResult<ELEMENT_T>(source, startIndex, endIndex);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes");
#endif
            }
#endif
        }

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void QuickSortCore<ELEMENT_T, KEY_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({startIndex}, {endIndex}) {endIndex - startIndex} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (endIndex <= startIndex)
                    return;
                if (endIndex - startIndex == 1)
                {
                    if (keySelector(source[startIndex]).CompareTo(keySelector(source[endIndex])) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startIndex}, index2={endIndex}");
#endif
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    }

                    return;
                }

                var pivotKey = keySelector(source[(startIndex + endIndex) / 2]);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var startOfPivotKeys = startIndex;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keySelector(source[lowerBoundary]).CompareTo(pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keySelector(source[upperBoundary]).CompareTo(pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(keySelector(source[upperBoundary]).CompareTo(pivotKey) < 0 && keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0 && keySelector(source[startOfPivotKeys]).CompareTo(pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(keySelector(source[upperBoundary]).CompareTo(pivotKey) < 0 && keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, startIndex, startOfPivotKeys - 1, keySelector);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, lowerBoundary, endIndex, keySelector);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, keySelector);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes");
#endif
            }
#endif
        }

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void QuickSortCore<ELEMENT_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, IComparer<ELEMENT_T> comparer)
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (endIndex <= startIndex)
                    return;
                if (endIndex - startIndex == 1)
                {
                    if (comparer.Compare(source[startIndex], source[endIndex]) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startIndex}, index2={endIndex}");
#endif
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    }

                    return;
                }

                var pivotKey = source[(startIndex + endIndex) / 2];
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var startOfPivotKeys = startIndex;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = comparer.Compare(source[lowerBoundary], pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(comparer.Compare(source[lowerBoundary], pivotKey) > 0);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(comparer.Compare(source[lowerBoundary], pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(comparer.Compare(source[lowerBoundary], pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || comparer.Compare(source[lowerBoundary], pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = comparer.Compare(source[upperBoundary], pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(comparer.Compare(source[upperBoundary], pivotKey) < 0 && comparer.Compare(source[lowerBoundary], pivotKey) > 0 && comparer.Compare(source[startOfPivotKeys], pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(comparer.Compare(source[upperBoundary], pivotKey) < 0 && comparer.Compare(source[lowerBoundary], pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, startIndex, startOfPivotKeys - 1, comparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, lowerBoundary, endIndex, comparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, comparer);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes");
#endif
            }
#endif
        }

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void QuickSortCore<ELEMENT_T, KEY_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (endIndex <= startIndex)
                    return;
                if (endIndex - startIndex == 1)
                {
                    if (keyComparer.Compare(keySelector(source[startIndex]), keySelector(source[endIndex])) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startIndex}, index2={endIndex}");
#endif
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    }

                    return;
                }

                var pivotKey = keySelector(source[(startIndex + endIndex) / 2]);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var startOfPivotKeys = startIndex;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey) < 0 && keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0 && keyComparer.Compare(keySelector(source[startOfPivotKeys]), pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey) < 0 && keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, startIndex, startOfPivotKeys - 1, keySelector, keyComparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, lowerBoundary, endIndex, keySelector, keyComparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, keySelector, keyComparer);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes");
#endif
            }
#endif
        }

#if DEBUG
        private static void AssertSortResult<ELEMENT_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(source[index].CompareTo(source[index + 1]) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T, KEY_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(keySelector(source[index + 1])) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, IComparer<ELEMENT_T> keyComparer)
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(keyComparer.Compare(source[index], source[index + 1]) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T, KEY_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), keySelector(source[index + 1])) <= 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys, IComparer<ELEMENT_T> comparer)
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) > 0);
        }
        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(IList<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
            for (var index = startIndex; index < startfPivotKeys; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) < 0);
            for (var index = startfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) > 0);
        }

#endif

        #endregion

        #region QuickSortCore (IArray<ELEMENT_T>)

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void QuickSortCore<ELEMENT_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (endIndex <= startIndex)
                    return;
                if (endIndex - startIndex == 1)
                {
                    if (source[startIndex].CompareTo(source[endIndex]) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startIndex}, index2={endIndex}");
#endif
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    }

                    return;
                }

                var pivotKey = source[(startIndex + endIndex) / 2];
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var startOfPivotKeys = startIndex;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = source[lowerBoundary].CompareTo(pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(source[lowerBoundary].CompareTo(pivotKey) > 0);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(source[lowerBoundary].CompareTo(pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(source[lowerBoundary].CompareTo(pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || source[lowerBoundary].CompareTo(pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = source[upperBoundary].CompareTo(pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(source[upperBoundary].CompareTo(pivotKey) < 0 && source[lowerBoundary].CompareTo(pivotKey) > 0 && source[startOfPivotKeys].CompareTo(pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(source[upperBoundary].CompareTo(pivotKey) < 0 && source[lowerBoundary].CompareTo(pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, startIndex, startOfPivotKeys - 1);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, lowerBoundary, endIndex);
#if DEBUG
            }
            finally
            {
                AssertSortResult<ELEMENT_T>(source, startIndex, endIndex);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes");
#endif
            }
#endif
        }

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void QuickSortCore<ELEMENT_T, KEY_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({startIndex}, {endIndex}) {endIndex - startIndex} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (endIndex <= startIndex)
                    return;
                if (endIndex - startIndex == 1)
                {
                    if (keySelector(source[startIndex]).CompareTo(keySelector(source[endIndex])) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startIndex}, index2={endIndex}");
#endif
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    }

                    return;
                }

                var pivotKey = keySelector(source[(startIndex + endIndex) / 2]);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var startOfPivotKeys = startIndex;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keySelector(source[lowerBoundary]).CompareTo(pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keySelector(source[upperBoundary]).CompareTo(pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(keySelector(source[upperBoundary]).CompareTo(pivotKey) < 0 && keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0 && keySelector(source[startOfPivotKeys]).CompareTo(pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(keySelector(source[upperBoundary]).CompareTo(pivotKey) < 0 && keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, startIndex, startOfPivotKeys - 1, keySelector);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, lowerBoundary, endIndex, keySelector);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, keySelector);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes");
#endif
            }
#endif
        }

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void QuickSortCore<ELEMENT_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, IComparer<ELEMENT_T> comparer)
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (endIndex <= startIndex)
                    return;
                if (endIndex - startIndex == 1)
                {
                    if (comparer.Compare(source[startIndex], source[endIndex]) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startIndex}, index2={endIndex}");
#endif
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    }

                    return;
                }

                var pivotKey = source[(startIndex + endIndex) / 2];
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var startOfPivotKeys = startIndex;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = comparer.Compare(source[lowerBoundary], pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(comparer.Compare(source[lowerBoundary], pivotKey) > 0);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(comparer.Compare(source[lowerBoundary], pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(comparer.Compare(source[lowerBoundary], pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || comparer.Compare(source[lowerBoundary], pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = comparer.Compare(source[upperBoundary], pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(comparer.Compare(source[upperBoundary], pivotKey) < 0 && comparer.Compare(source[lowerBoundary], pivotKey) > 0 && comparer.Compare(source[startOfPivotKeys], pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(comparer.Compare(source[upperBoundary], pivotKey) < 0 && comparer.Compare(source[lowerBoundary], pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, comparer);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, startIndex, startOfPivotKeys - 1, comparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, lowerBoundary, endIndex, comparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, comparer);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes");
#endif
            }
#endif
        }

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void QuickSortCore<ELEMENT_T, KEY_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes, ");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (endIndex <= startIndex)
                    return;
                if (endIndex - startIndex == 1)
                {
                    if (keyComparer.Compare(keySelector(source[startIndex]), keySelector(source[endIndex])) > 0)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startIndex}, index2={endIndex}");
#endif
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    }

                    return;
                }

                var pivotKey = keySelector(source[(startIndex + endIndex) / 2]);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var startOfPivotKeys = startIndex;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary]    : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Validation.Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Validation.Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) <= 0);
#endif
                        if (c < 0)
                        {
                            // source[lowerBoundary] < pivotKey である場合
#if DEBUG
                            Validation.Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) < 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[startOfPivotKeys] == pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
#if DEBUG
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={lowerBoundary}");
#endif
                                (source[startOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[startOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Validation.Assert(startOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey
                    Validation.Assert(lowerBoundary > upperBoundary || keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey);
                        if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            if (startOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[startOfPivotKeys] == pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Validation.Assert(keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey) < 0 && keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0 && keyComparer.Compare(keySelector(source[startOfPivotKeys]), pivotKey) == 0);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}, index3={lowerBoundary}");
#endif
                                var t = source[startOfPivotKeys];
                                source[startOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] < pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) startOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Validation.Assert(keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey) < 0 && keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0 && startOfPivotKeys == lowerBoundary);
                                System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={startOfPivotKeys}, index2={upperBoundary}");
#endif
                                (source[startOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[startOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++startOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より大きい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"An array element replacement has occurred.: index1={upperBoundary}, index2={lowerBoundary}");
#endif
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Validation.Assert(startOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Validation.Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, startOfPivotKeys)    : x < pivotKey であるキー値 x を持つ要素の集合
                // region-b) [startOfPivotKeys, lowerBoundary) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, endIndex]         : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, startOfPivotKeys, keySelector, keyComparer);
#endif

                // region-a の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, startIndex, startOfPivotKeys - 1, keySelector, keyComparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                QuickSortCore(source, lowerBoundary, endIndex, keySelector, keyComparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, keySelector, keyComparer);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes");
#endif
            }
#endif
        }

#if DEBUG
        private static void AssertSortResult<ELEMENT_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(source[index].CompareTo(source[index + 1]) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T, KEY_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(keySelector(source[index + 1])) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, IComparer<ELEMENT_T> keyComparer)
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(keyComparer.Compare(source[index], source[index + 1]) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T, KEY_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), keySelector(source[index + 1])) <= 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(source[index].CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(keySelector(source[index]).CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startOfPivotKeys, IComparer<ELEMENT_T> comparer)
        {
            for (var index = startIndex; index < startOfPivotKeys; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) < 0);
            for (var index = startOfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(comparer.Compare(source[index], pivotKey) > 0);
        }
        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(IArray<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 startfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
            for (var index = startIndex; index < startfPivotKeys; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) < 0);
            for (var index = startfPivotKeys; index < lowerBoundary; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) == 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Validation.Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) > 0);
        }

#endif

        #endregion
    }
}
