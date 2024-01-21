using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    partial class ArrayExtensions
    {
        #region SequenceCompare

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(),
                    array2.AsReadOnlySpan());
        }

        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, IComparer<ELEMENT_T> comparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(),
                    array2.AsReadOnlySpan(),
                    comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(),
                    array2.AsReadOnlySpan(),
                    keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(),
                    array2.AsReadOnlySpan(),
                    keySelecter,
                    keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, Range array1Range, ELEMENT_T[] array2, Range array2Range)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));

            var (array1Offset, array1Count) = array1.GetOffsetAndLength(array1Range, nameof(array1Range));
            var (array2Offset, array2Count) = array2.GetOffsetAndLength(array2Range, nameof(array2Range));
            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(array1Offset, array1Count),
                    array2.AsReadOnlySpan(array2Offset, array2Count));
        }

        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, Range array1Range, ELEMENT_T[] array2, Range array2Range, IComparer<ELEMENT_T> comparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            var (array1Offset, array1Count) = array1.GetOffsetAndLength(array1Range, nameof(array1Range));
            var (array2Offset, array2Count) = array2.GetOffsetAndLength(array2Range, nameof(array2Range));
            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(array1Offset, array1Count),
                    array2.AsReadOnlySpan(array2Offset, array2Count),
                    comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Range array1Range, ELEMENT_T[] array2, Range array2Range, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            var (array1Offset, array1Count) = array1.GetOffsetAndLength(array1Range, nameof(array1Range));
            var (array2Offset, array2Count) = array2.GetOffsetAndLength(array2Range, nameof(array2Range));
            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(array1Offset, array1Count),
                    array2.AsReadOnlySpan(array2Offset, array2Count),
                    keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Range array1Range, ELEMENT_T[] array2, Range array2Range, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            var (array1Offset, array1Count) = array1.GetOffsetAndLength(array1Range, nameof(array1Range));
            var (array2Offset, array2Count) = array2.GetOffsetAndLength(array2Range, nameof(array2Range));
            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(array1Offset, array1Count),
                    array2.AsReadOnlySpan(array2Offset, array2Count),
                    keySelecter,
                    keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Count, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array1Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Offset));
            if (array1Count < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Count));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (array2Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Offset));
            if (array2Count < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Count));
            if (checked(array1Offset + array1Count) > array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(array1Count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + array2Count) > array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(array2Count)}) is not within the {nameof(array2)}.");

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(array1Offset, array1Count),
                    array2.AsReadOnlySpan(array2Offset, array2Count));
        }

        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Count, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Count, IComparer<ELEMENT_T> comparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array1Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Offset));
            if (array1Count < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Count));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (array2Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Offset));
            if (array2Count < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Count));
            if (checked(array1Offset + array1Count) > array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(array1Count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + array2Count) > array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(array2Count)}) is not within the {nameof(array2)}.");
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(array1Offset, array1Count),
                    array2.AsReadOnlySpan(array2Offset, array2Count),
                    comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Count, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Count, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array1Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Offset));
            if (array1Count < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Count));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (array2Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Offset));
            if (array2Count < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Count));
            if (checked(array1Offset + array1Count) > array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(array1Count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + array2Count) > array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(array2Count)}) is not within the {nameof(array2)}.");
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(array1Offset, array1Count),
                    array2.AsReadOnlySpan(array2Offset, array2Count),
                    keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Count, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Count, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array1Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Offset));
            if (array1Count < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Count));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (array2Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Offset));
            if (array2Count < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Count));
            if (checked(array1Offset + array1Count) > array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(array1Count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + array2Count) > array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(array2Count)}) is not within the {nameof(array2)}.");
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(array1Offset, array1Count),
                    array2.AsReadOnlySpan(array2Offset, array2Count),
                    keySelecter,
                    keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, UInt32 array1Offset, UInt32 array1Count, ELEMENT_T[] array2, UInt32 array2Offset, UInt32 array2Count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (checked(array1Offset + array1Count) > (UInt32)array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(array1Count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + array2Count) > (UInt32)array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(array2Count)}) is not within the {nameof(array2)}.");

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(checked((Int32)array1Offset), checked((Int32)array1Count)),
                    array2.AsReadOnlySpan(checked((Int32)array2Offset), checked((Int32)array2Count)));
        }

        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, UInt32 array1Offset, UInt32 array1Count, ELEMENT_T[] array2, UInt32 array2Offset, UInt32 array2Count, IComparer<ELEMENT_T> comparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (checked(array1Offset + array1Count) > (UInt32)array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(array1Count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + array2Count) > (UInt32)array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(array2Count)}) is not within the {nameof(array2)}.");
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(checked((Int32)array1Offset), checked((Int32)array1Count)),
                    array2.AsReadOnlySpan(checked((Int32)array2Offset), checked((Int32)array2Count)),
                    comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, UInt32 array1Offset, UInt32 array1Count, ELEMENT_T[] array2, UInt32 array2Offset, UInt32 array2Count, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (checked(array1Offset + array1Count) > (UInt32)array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(array1Count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + array2Count) > (UInt32)array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(array2Count)}) is not within the {nameof(array2)}.");
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(checked((Int32)array1Offset), checked((Int32)array1Count)),
                    array2.AsReadOnlySpan(checked((Int32)array2Offset), checked((Int32)array2Count)),
                    keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, UInt32 array1Offset, UInt32 array1Count, ELEMENT_T[] array2, UInt32 array2Offset, UInt32 array2Count, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (checked(array1Offset + array1Count) > (UInt32)array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(array1Count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + array2Count) > (UInt32)array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(array2Count)}) is not within the {nameof(array2)}.");
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return
                InternalSequenceCompare(
                    array1.AsReadOnlySpan(checked((Int32)array1Offset), checked((Int32)array1Count)),
                    array2.AsReadOnlySpan(checked((Int32)array2Offset), checked((Int32)array2Count)),
                    keySelecter,
                    keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, Span<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2);
        }

        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, Span<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, array2);
        }

        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, array2, comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2);
        }

        public static Int32 SequenceCompare<ELEMENT_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, IComparer<ELEMENT_T> comparer)
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this Span<ELEMENT_T> array1, Span<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
            => InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2);

        public static Int32 SequenceCompare<ELEMENT_T>(this Span<ELEMENT_T> array1, Span<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
        {
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
            => InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, array2);

        public static Int32 SequenceCompare<ELEMENT_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
        {
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, array2, comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return InternalSequenceCompare((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));

            return InternalSequenceCompare(array1, (ReadOnlySpan<ELEMENT_T>)array2);
        }

        public static Int32 SequenceCompare<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, IComparer<ELEMENT_T> comparer)
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return InternalSequenceCompare(array1, (ReadOnlySpan<ELEMENT_T>)array2, comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceCompare(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return InternalSequenceCompare(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, Span<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
            => InternalSequenceCompare(array1, (ReadOnlySpan<ELEMENT_T>)array2);

        public static Int32 SequenceCompare<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, Span<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
        {
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return InternalSequenceCompare(array1, (ReadOnlySpan<ELEMENT_T>)array2, comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceCompare(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return InternalSequenceCompare(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 SequenceCompare<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
            => InternalSequenceCompare(array1, array2);

        public static Int32 SequenceCompare<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
        {
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return InternalSequenceCompare(array1, array2, comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceCompare(array1, array2, keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return InternalSequenceCompare(array1, array2, keySelecter, keyComparer);
        }

        #endregion

        #region InternalSequenceCompare

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 InternalSequenceCompare<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> keyComparer)
        {
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            var count = array1.Length.Minimum(array2.Length);
            for (var index = 0; index < count; index++)
            {
                var c = keyComparer.Compare(array1[index], array2[index]);
                if (c != 0)
                    return c;
            }

            return array1.Length.CompareTo(array2.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 InternalSequenceCompare<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            var count = array1.Length.Minimum(array2.Length);
            for (var index = 0; index < count; index++)
            {
                var c = DefaultCompare(keySelecter(array1[index]), keySelecter(array2[index]));
                if (c != 0)
                    return c;
            }

            return array1.Length.CompareTo(array2.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 InternalSequenceCompare<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            var count = array1.Length.Minimum(array2.Length);
            for (var index = 0; index < count; index++)
            {
                var c = keyComparer.Compare(keySelecter(array1[index]), keySelecter(array2[index]));
                if (c != 0)
                    return c;
            }

            return array1.Length.CompareTo(array2.Length);
        }

        #endregion

        #region InternalSequenceCompareManaged

        private static Int32 InternalSequenceCompareManaged<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            var count = array1.Length.Minimum(array2.Length);
            for (var index = 0; index < count; index++)
            {
                var c = array1[index].CompareTo(array2[index]);
                if (c != 0)
                    return c;
            }

            return array1.Length.CompareTo(array2.Length);
        }

        #endregion

        #region InternalSequenceCompareUnmanaged

        private static unsafe Int32 InternalSequenceCompareUnmanaged<ELEMENT_T>(ref ELEMENT_T array1, Int32 array1Length, ref ELEMENT_T array2, Int32 array2Length)
            where ELEMENT_T : unmanaged, IComparable<ELEMENT_T>
        {
            fixed (ELEMENT_T* buffer1 = &array1)
            fixed (ELEMENT_T* buffer2 = &array2)
            {
                return InternalSequenceCompareUnmanaged(buffer1, buffer2, array1Length.Minimum(array2Length));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe Int32 InternalSequenceCompareUnmanaged<ELEMENT_T>(ELEMENT_T* pointer1, ELEMENT_T* pointer2, Int32 count)
            where ELEMENT_T : unmanaged, IComparable<ELEMENT_T>
        {
            Int32 c;
            while (count >= 32)
            {
                if ((c = pointer1[0].CompareTo(pointer2[0])) != 0)
                    return c;
                if ((c = pointer1[1].CompareTo(pointer2[1])) != 0)
                    return c;
                if ((c = pointer1[2].CompareTo(pointer2[2])) != 0)
                    return c;
                if ((c = pointer1[3].CompareTo(pointer2[3])) != 0)
                    return c;
                if ((c = pointer1[4].CompareTo(pointer2[4])) != 0)
                    return c;
                if ((c = pointer1[5].CompareTo(pointer2[5])) != 0)
                    return c;
                if ((c = pointer1[6].CompareTo(pointer2[6])) != 0)
                    return c;
                if ((c = pointer1[7].CompareTo(pointer2[7])) != 0)
                    return c;
                if ((c = pointer1[8].CompareTo(pointer2[8])) != 0)
                    return c;
                if ((c = pointer1[9].CompareTo(pointer2[9])) != 0)
                    return c;
                if ((c = pointer1[10].CompareTo(pointer2[10])) != 0)
                    return c;
                if ((c = pointer1[11].CompareTo(pointer2[11])) != 0)
                    return c;
                if ((c = pointer1[12].CompareTo(pointer2[12])) != 0)
                    return c;
                if ((c = pointer1[13].CompareTo(pointer2[13])) != 0)
                    return c;
                if ((c = pointer1[14].CompareTo(pointer2[14])) != 0)
                    return c;
                if ((c = pointer1[15].CompareTo(pointer2[15])) != 0)
                    return c;
                if ((c = pointer1[16].CompareTo(pointer2[16])) != 0)
                    return c;
                if ((c = pointer1[17].CompareTo(pointer2[17])) != 0)
                    return c;
                if ((c = pointer1[18].CompareTo(pointer2[18])) != 0)
                    return c;
                if ((c = pointer1[19].CompareTo(pointer2[19])) != 0)
                    return c;
                if ((c = pointer1[20].CompareTo(pointer2[20])) != 0)
                    return c;
                if ((c = pointer1[21].CompareTo(pointer2[21])) != 0)
                    return c;
                if ((c = pointer1[22].CompareTo(pointer2[22])) != 0)
                    return c;
                if ((c = pointer1[23].CompareTo(pointer2[23])) != 0)
                    return c;
                if ((c = pointer1[24].CompareTo(pointer2[24])) != 0)
                    return c;
                if ((c = pointer1[25].CompareTo(pointer2[25])) != 0)
                    return c;
                if ((c = pointer1[26].CompareTo(pointer2[26])) != 0)
                    return c;
                if ((c = pointer1[27].CompareTo(pointer2[27])) != 0)
                    return c;
                if ((c = pointer1[28].CompareTo(pointer2[28])) != 0)
                    return c;
                if ((c = pointer1[29].CompareTo(pointer2[29])) != 0)
                    return c;
                if ((c = pointer1[30].CompareTo(pointer2[30])) != 0)
                    return c;
                if ((c = pointer1[31].CompareTo(pointer2[31])) != 0)
                    return c;
                pointer1 += 32;
                pointer2 += 32;
                count -= 32;
            }

            if ((count & 0x10) != 0)
            {
                if ((c = pointer1[0].CompareTo(pointer2[0])) != 0)
                    return c;
                if ((c = pointer1[1].CompareTo(pointer2[1])) != 0)
                    return c;
                if ((c = pointer1[2].CompareTo(pointer2[2])) != 0)
                    return c;
                if ((c = pointer1[3].CompareTo(pointer2[3])) != 0)
                    return c;
                if ((c = pointer1[4].CompareTo(pointer2[4])) != 0)
                    return c;
                if ((c = pointer1[5].CompareTo(pointer2[5])) != 0)
                    return c;
                if ((c = pointer1[6].CompareTo(pointer2[6])) != 0)
                    return c;
                if ((c = pointer1[7].CompareTo(pointer2[7])) != 0)
                    return c;
                if ((c = pointer1[8].CompareTo(pointer2[8])) != 0)
                    return c;
                if ((c = pointer1[9].CompareTo(pointer2[9])) != 0)
                    return c;
                if ((c = pointer1[10].CompareTo(pointer2[10])) != 0)
                    return c;
                if ((c = pointer1[11].CompareTo(pointer2[11])) != 0)
                    return c;
                if ((c = pointer1[12].CompareTo(pointer2[12])) != 0)
                    return c;
                if ((c = pointer1[13].CompareTo(pointer2[13])) != 0)
                    return c;
                if ((c = pointer1[14].CompareTo(pointer2[14])) != 0)
                    return c;
                if ((c = pointer1[15].CompareTo(pointer2[15])) != 0)
                    return c;
                pointer1 += 16;
                pointer2 += 16;
            }

            if ((count & 0x08) != 0)
            {
                if ((c = pointer1[0].CompareTo(pointer2[0])) != 0)
                    return c;
                if ((c = pointer1[1].CompareTo(pointer2[1])) != 0)
                    return c;
                if ((c = pointer1[2].CompareTo(pointer2[2])) != 0)
                    return c;
                if ((c = pointer1[3].CompareTo(pointer2[3])) != 0)
                    return c;
                if ((c = pointer1[4].CompareTo(pointer2[4])) != 0)
                    return c;
                if ((c = pointer1[5].CompareTo(pointer2[5])) != 0)
                    return c;
                if ((c = pointer1[6].CompareTo(pointer2[6])) != 0)
                    return c;
                if ((c = pointer1[7].CompareTo(pointer2[7])) != 0)
                    return c;
                pointer1 += 8;
                pointer2 += 8;
            }

            if ((count & 0x04) != 0)
            {
                if ((c = pointer1[0].CompareTo(pointer2[0])) != 0)
                    return c;
                if ((c = pointer1[1].CompareTo(pointer2[1])) != 0)
                    return c;
                if ((c = pointer1[2].CompareTo(pointer2[2])) != 0)
                    return c;
                if ((c = pointer1[3].CompareTo(pointer2[3])) != 0)
                    return c;
                pointer1 += 4;
                pointer2 += 4;
            }

            if ((count & 0x02) != 0)
            {
                if ((c = pointer1[0].CompareTo(pointer2[0])) != 0)
                    return c;
                if ((c = pointer1[1].CompareTo(pointer2[1])) != 0)
                    return c;
                pointer1 += 2;
                pointer2 += 2;
            }

            if ((count & 0x01) != 0)
            {
                if ((c = pointer1[0].CompareTo(pointer2[0])) != 0)
                    return c;
            }

            return 0;
        }

        #endregion
    }
}
