using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    partial class ArrayExtensions
    {
        #region SequenceEqual

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));

            return InternalSequenceEqual(array1.AsReadOnlySpan(), array2.AsReadOnlySpan());
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));

            return InternalSequenceEqual(array1.AsReadOnlySpan(), array2.AsReadOnlySpan(), equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceEqual(array1.AsReadOnlySpan(), array2.AsReadOnlySpan(), keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return InternalSequenceEqual(array1.AsReadOnlySpan(), array2.AsReadOnlySpan(), keySelecter, keyEqualityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, Int32 array1Offset, ELEMENT_T[] array2, Int32 array2Offset, Int32 count)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array1Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Offset));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (array2Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(array1Offset + count) > array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + count) > array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(count)}) is not within the {nameof(array2)}.");

            return InternalSequenceEqual(array1.AsReadOnlySpan(array1Offset, count), array2.AsReadOnlySpan(array2Offset, count));
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, Int32 array1Offset, ELEMENT_T[] array2, Int32 array2Offset, Int32 count, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array1Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Offset));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (array2Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));
            if (checked(array1Offset + count) > array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + count) > array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(count)}) is not within the {nameof(array2)}.");

            return InternalSequenceEqual(array1.AsReadOnlySpan(array1Offset, count), array2.AsReadOnlySpan(array2Offset, count), equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Int32 array1Offset, ELEMENT_T[] array2, Int32 array2Offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array1Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Offset));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (array2Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (checked(array1Offset + count) > array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + count) > array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(count)}) is not within the {nameof(array2)}.");

            return InternalSequenceEqual(array1.AsReadOnlySpan(array1Offset, count), array2.AsReadOnlySpan(array2Offset, count), keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Int32 array1Offset, ELEMENT_T[] array2, Int32 array2Offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array1Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array1Offset));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (array2Offset < 0)
                throw new ArgumentOutOfRangeException(nameof(array2Offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));
            if (checked(array1Offset + count) > array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + count) > array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(count)}) is not within the {nameof(array2)}.");

            return InternalSequenceEqual(array1.AsReadOnlySpan(array1Offset, count), array2.AsReadOnlySpan(array2Offset, count), keySelecter, keyEqualityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, UInt32 array1Offset, ELEMENT_T[] array2, UInt32 array2Offset, UInt32 count)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (checked(array1Offset + count) > (UInt32)array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + count) > (UInt32)array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(count)}) is not within the {nameof(array2)}.");

            return
                InternalSequenceEqual(array1.AsReadOnlySpan(
                    checked((Int32)array1Offset),
                    checked((Int32)count)), array2.AsReadOnlySpan(checked((Int32)array2Offset), checked((Int32)count)));
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, UInt32 array1Offset, ELEMENT_T[] array2, UInt32 array2Offset, UInt32 count, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));
            if (checked(array1Offset + count) > (UInt32)array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + count) > (UInt32)array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(count)}) is not within the {nameof(array2)}.");

            return
                InternalSequenceEqual(array1.AsReadOnlySpan(
                    checked((Int32)array1Offset),
                    checked((Int32)count)), array2.AsReadOnlySpan(checked((Int32)array2Offset), checked((Int32)count)),
                    equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, UInt32 array1Offset, ELEMENT_T[] array2, UInt32 array2Offset, UInt32 count, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (checked(array1Offset + count) > (UInt32)array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + count) > (UInt32)array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(count)}) is not within the {nameof(array2)}.");

            return
                InternalSequenceEqual(array1.AsReadOnlySpan(
                    checked((Int32)array1Offset),
                    checked((Int32)count)), array2.AsReadOnlySpan(checked((Int32)array2Offset), checked((Int32)count)),
                    keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, UInt32 array1Offset, ELEMENT_T[] array2, UInt32 array2Offset, UInt32 count, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));
            if (checked(array1Offset + count) > (UInt32)array1.Length)
                throw new ArgumentException($"The specified range ({nameof(array1Offset)} and {nameof(count)}) is not within the {nameof(array1)}.");
            if (checked(array2Offset + count) > (UInt32)array2.Length)
                throw new ArgumentException($"The specified range ({nameof(array2Offset)} and {nameof(count)}) is not within the {nameof(array2)}.");

            return
                InternalSequenceEqual(array1.AsReadOnlySpan(
                    checked((Int32)array1Offset),
                    checked((Int32)count)), array2.AsReadOnlySpan(checked((Int32)array2Offset), checked((Int32)count)),
                    keySelecter,
                    keyEqualityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, Span<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyEqualityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, array2);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, array2, equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter, keyEqualityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<ELEMENT_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyEqualityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this Span<ELEMENT_T> array1, Span<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyEqualityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, array2, equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return InternalSequenceEqual((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter, keyEqualityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SequenceEqual<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));

            return InternalSequenceEqual(array1, (ReadOnlySpan<ELEMENT_T>)array2);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));

            return InternalSequenceEqual(array1, (ReadOnlySpan<ELEMENT_T>)array2, equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceEqual(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return InternalSequenceEqual(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyEqualityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, Span<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));

            return InternalSequenceEqual(array1, (ReadOnlySpan<ELEMENT_T>)array2, equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceEqual(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, Span<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return InternalSequenceEqual(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyEqualityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));

            return InternalSequenceEqual(array1, array2, equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return InternalSequenceEqual(array1, array2, keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return InternalSequenceEqual(array1, array2, keySelecter, keyEqualityComparer);
        }

        #endregion

        #region InternalSequenceEqual

        private static Boolean InternalSequenceEqual<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> keyEqualityComparer)
        {
            if (array1.Length != array2.Length)
                return false;

            var count = array1.Length;
            for (var index = 0; index < count; index++)
            {
                if (!keyEqualityComparer.Equals(array1[index], array2[index]))
                    return false;
            }

            return true;
        }

        private static Boolean InternalSequenceEqual<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (array1.Length != array2.Length)
                return false;

            var count = array1.Length;
            for (var index = 0; index < count; index++)
            {
                var key1 = keySelecter(array1[index]);
                var key2 = keySelecter(array2[index]);
                if (!DefaultEqual(key1, key2))
                    return false;
            }

            return true;
        }

        private static Boolean InternalSequenceEqual<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (array1.Length != array2.Length)
                return false;

            var count = array1.Length;
            for (var index = 0; index < count; index++)
            {
                if (!keyEqualityComparer.Equals(keySelecter(array1[index]), keySelecter(array2[index])))
                    return false;
            }

            return true;
        }

        #endregion

        #region InternalSequenceEqualManaged

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean InternalSequenceEqualManaged<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (array1.Length != array2.Length)
                return false;

            var count = array1.Length;
            for (var index = 0; index < count; index++)
            {
                if (!DefaultEqual(array1[index], array2[index]))
                    return false;
            }

            return true;
        }

        #endregion

        #region InternalSequenceEqualUnmanaged

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe Boolean InternalSequenceEqualUnmanaged<ELEMENT_T>(ref ELEMENT_T array1, ref ELEMENT_T array2, Int32 count)
            where ELEMENT_T : unmanaged
        {
            if (count <= 0)
                return true;

            fixed (ELEMENT_T* pointer1 = &array1)
            fixed (ELEMENT_T* pointer2 = &array2)
            {
                if (pointer1 == pointer2)
                    return true;

                if (sizeof(void*) >= 8 && sizeof(ELEMENT_T) >= 8)
                {
                    // 64 bit で動作しており、かつ ELEMENT_T のアラインメント境界が 8 である場合

                    Validation.Assert(sizeof(ELEMENT_T) is 8 or 16, "sizeof(ELEMENT_T) is 8 or 16");
                    return InternalSequenceEqualUnmanaged((UInt64*)pointer1, (UInt64*)pointer2, unchecked((Int32)((UInt32)count * (sizeof(ELEMENT_T) / sizeof(UInt64)))));
                }
                else if (sizeof(ELEMENT_T) >= 4)
                {
                    // ELEMENT_T のアラインメント境界が 4 である場合

                    Validation.Assert(sizeof(ELEMENT_T) == 4, "sizeof(ELEMENT_T) == 4");
                    if (sizeof(void*) >= 8 && ((UInt32)pointer1 & 0x07) == 0 && ((UInt32)pointer2 & 0x07) == 0 && ((UInt32)count & 0x01) == 0)
                    {
                        // 64 bit で動作しており、かつ pointer1 および pointer2 が 8の倍数であり、かつ array1Length が 2 の倍数である場合

                        // UInt64 の配列として扱う
                        return InternalSequenceEqualUnmanaged((UInt64*)pointer1, (UInt64*)pointer2, unchecked((Int32)((UInt32)count >> 1)));
                    }
                    else
                    {
                        // UInt32 の配列として扱う
                        return InternalSequenceEqualUnmanaged((UInt32*)pointer1, (UInt32*)pointer2, count);
                    }
                }
                else if (sizeof(ELEMENT_T) >= 2)
                {
                    // ELEMENT_T のアラインメント境界が 2 である場合

                    Validation.Assert(sizeof(ELEMENT_T) == 2, "sizeof(ELEMENT_T) == 2");
                    if (sizeof(void*) >= 8 && ((UInt32)pointer1 & 0x07) == 0 && ((UInt32)pointer2 & 0x07) == 0 && ((UInt32)count & 0x03) == 0)
                    {
                        // 64 bit で動作しており、かつ pointer1 および pointer2 が 8の倍数であり、かつ array1Length が 4 の倍数である場合

                        // UInt64 の配列として扱う
                        return InternalSequenceEqualUnmanaged((UInt64*)pointer1, (UInt64*)pointer2, unchecked((Int32)((UInt32)count >> 2)));
                    }
                    else if (((UInt32)pointer1 & 0x03) == 0 && ((UInt32)pointer2 & 0x03) == 0 && ((UInt32)count & 0x01) == 0)
                    {
                        // かつ pointer1 および pointer2 が 4の倍数であり、かつ array1Length が 2 の倍数である場合

                        // UInt32 の配列として扱う
                        return InternalSequenceEqualUnmanaged((UInt32*)pointer1, (UInt32*)pointer2, unchecked((Int32)((UInt32)count >> 1)));
                    }
                    else
                    {
                        // UInt16 の配列として扱う
                        return InternalSequenceEqualUnmanaged((UInt16*)pointer1, (UInt16*)pointer2, count);
                    }
                }
                else
                {
                    // ELEMENT_T のアラインメント境界が 1 である場合

                    Validation.Assert(sizeof(ELEMENT_T) == 1, "sizeof(ELEMENT_T) == 1");
                    if (sizeof(void*) >= 8 && ((UInt32)pointer1 & 0x07) == 0 && ((UInt32)pointer2 & 0x07) == 0 && ((UInt32)count & 0x07) == 0)
                    {
                        // 64 bit で動作しており、かつ pointer1 および pointer2 が 8の倍数であり、かつ array1Length が 8 の倍数である場合

                        // UInt64 の配列として扱う
                        return InternalSequenceEqualUnmanaged((UInt64*)pointer1, (UInt64*)pointer2, unchecked((Int32)((UInt32)count >> 3)));
                    }
                    else if (((UInt32)pointer1 & 0x03) == 0 && ((UInt32)pointer2 & 0x03) == 0 && ((UInt32)count & 0x03) == 0)
                    {
                        // かつ pointer1 および pointer2 が 4の倍数であり、かつ array1Length が 4 の倍数である場合

                        // UInt32 の配列として扱う
                        return InternalSequenceEqualUnmanaged((UInt32*)pointer1, (UInt32*)pointer2, unchecked((Int32)((UInt32)count >> 2)));
                    }
                    else if (((UInt32)pointer1 & 0x01) == 0 && ((UInt32)pointer2 & 0x01) == 0 && ((UInt32)count & 0x01) == 0)
                    {
                        // かつ pointer1 および pointer2 が 2の倍数であり、かつ array1Length が 2 の倍数である場合

                        // UInt16 の配列として扱う
                        return InternalSequenceEqualUnmanaged((UInt16*)pointer1, (UInt16*)pointer2, unchecked((Int32)((UInt32)count >> 1)));
                    }
                    else
                    {
                        // Byte の配列として扱う
                        return InternalSequenceEqualUnmanaged((Byte*)pointer1, (Byte*)pointer2, count);
                    }
                }
            }
        }

        private static unsafe Boolean InternalSequenceEqualUnmanaged(UInt64* pointer1, UInt64* pointer2, Int32 count)
        {
            while (count >= 32)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7]
                    || pointer1[8] != pointer2[8]
                    || pointer1[9] != pointer2[9]
                    || pointer1[10] != pointer2[10]
                    || pointer1[11] != pointer2[11]
                    || pointer1[12] != pointer2[12]
                    || pointer1[13] != pointer2[13]
                    || pointer1[14] != pointer2[14]
                    || pointer1[15] != pointer2[15]
                    || pointer1[16] != pointer2[16]
                    || pointer1[17] != pointer2[17]
                    || pointer1[18] != pointer2[18]
                    || pointer1[19] != pointer2[19]
                    || pointer1[20] != pointer2[20]
                    || pointer1[21] != pointer2[21]
                    || pointer1[22] != pointer2[22]
                    || pointer1[23] != pointer2[23]
                    || pointer1[24] != pointer2[24]
                    || pointer1[25] != pointer2[25]
                    || pointer1[26] != pointer2[26]
                    || pointer1[27] != pointer2[27]
                    || pointer1[28] != pointer2[28]
                    || pointer1[29] != pointer2[29]
                    || pointer1[30] != pointer2[30]
                    || pointer1[31] != pointer2[31])
                {
                    return false;
                }

                pointer1 += 32;
                pointer2 += 32;
                count -= 32;
            }

            if ((count & 0x10) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7]
                    || pointer1[8] != pointer2[8]
                    || pointer1[9] != pointer2[9]
                    || pointer1[10] != pointer2[10]
                    || pointer1[11] != pointer2[11]
                    || pointer1[12] != pointer2[12]
                    || pointer1[13] != pointer2[13]
                    || pointer1[14] != pointer2[14]
                    || pointer1[15] != pointer2[15])
                {
                    return false;
                }

                pointer1 += 16;
                pointer2 += 16;
            }

            if ((count & 0x08) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7])
                {
                    return false;
                }

                pointer1 += 8;
                pointer2 += 8;
            }

            if ((count & 0x04) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3])
                {
                    return false;
                }

                pointer1 += 4;
                pointer2 += 4;
            }

            if ((count & 0x02) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1])
                {
                    return false;
                }

                pointer1 += 2;
                pointer2 += 2;
            }

            if ((count & 0x01) != 0)
            {
                if (pointer1[0] != pointer2[0])
                    return false;
            }

            return true;
        }

        private static unsafe Boolean InternalSequenceEqualUnmanaged(UInt32* pointer1, UInt32* pointer2, Int32 count)
        {
            while (count >= 32)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7]
                    || pointer1[8] != pointer2[8]
                    || pointer1[9] != pointer2[9]
                    || pointer1[10] != pointer2[10]
                    || pointer1[11] != pointer2[11]
                    || pointer1[12] != pointer2[12]
                    || pointer1[13] != pointer2[13]
                    || pointer1[14] != pointer2[14]
                    || pointer1[15] != pointer2[15]
                    || pointer1[16] != pointer2[16]
                    || pointer1[17] != pointer2[17]
                    || pointer1[18] != pointer2[18]
                    || pointer1[19] != pointer2[19]
                    || pointer1[20] != pointer2[20]
                    || pointer1[21] != pointer2[21]
                    || pointer1[22] != pointer2[22]
                    || pointer1[23] != pointer2[23]
                    || pointer1[24] != pointer2[24]
                    || pointer1[25] != pointer2[25]
                    || pointer1[26] != pointer2[26]
                    || pointer1[27] != pointer2[27]
                    || pointer1[28] != pointer2[28]
                    || pointer1[29] != pointer2[29]
                    || pointer1[30] != pointer2[30]
                    || pointer1[31] != pointer2[31])
                {
                    return false;
                }

                pointer1 += 32;
                pointer2 += 32;
                count -= 32;
            }

            if ((count & 0x10) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7]
                    || pointer1[8] != pointer2[8]
                    || pointer1[9] != pointer2[9]
                    || pointer1[10] != pointer2[10]
                    || pointer1[11] != pointer2[11]
                    || pointer1[12] != pointer2[12]
                    || pointer1[13] != pointer2[13]
                    || pointer1[14] != pointer2[14]
                    || pointer1[15] != pointer2[15])
                {
                    return false;
                }

                pointer1 += 16;
                pointer2 += 16;
            }

            if ((count & 0x08) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7])
                {
                    return false;
                }

                pointer1 += 8;
                pointer2 += 8;
            }

            if ((count & 0x04) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3])
                {
                    return false;
                }

                pointer1 += 4;
                pointer2 += 4;
            }

            if ((count & 0x02) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1])
                {
                    return false;
                }

                pointer1 += 2;
                pointer2 += 2;
            }

            if ((count & 0x01) != 0)
            {
                if (pointer1[0] != pointer2[0])
                    return false;
            }

            return true;
        }

        private static unsafe Boolean InternalSequenceEqualUnmanaged(UInt16* pointer1, UInt16* pointer2, Int32 count)
        {
            while (count >= 32)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7]
                    || pointer1[8] != pointer2[8]
                    || pointer1[9] != pointer2[9]
                    || pointer1[10] != pointer2[10]
                    || pointer1[11] != pointer2[11]
                    || pointer1[12] != pointer2[12]
                    || pointer1[13] != pointer2[13]
                    || pointer1[14] != pointer2[14]
                    || pointer1[15] != pointer2[15]
                    || pointer1[16] != pointer2[16]
                    || pointer1[17] != pointer2[17]
                    || pointer1[18] != pointer2[18]
                    || pointer1[19] != pointer2[19]
                    || pointer1[20] != pointer2[20]
                    || pointer1[21] != pointer2[21]
                    || pointer1[22] != pointer2[22]
                    || pointer1[23] != pointer2[23]
                    || pointer1[24] != pointer2[24]
                    || pointer1[25] != pointer2[25]
                    || pointer1[26] != pointer2[26]
                    || pointer1[27] != pointer2[27]
                    || pointer1[28] != pointer2[28]
                    || pointer1[29] != pointer2[29]
                    || pointer1[30] != pointer2[30]
                    || pointer1[31] != pointer2[31])
                {
                    return false;
                }

                pointer1 += 32;
                pointer2 += 32;
                count -= 32;
            }

            if ((count & 0x10) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7]
                    || pointer1[8] != pointer2[8]
                    || pointer1[9] != pointer2[9]
                    || pointer1[10] != pointer2[10]
                    || pointer1[11] != pointer2[11]
                    || pointer1[12] != pointer2[12]
                    || pointer1[13] != pointer2[13]
                    || pointer1[14] != pointer2[14]
                    || pointer1[15] != pointer2[15])
                {
                    return false;
                }

                pointer1 += 16;
                pointer2 += 16;
            }

            if ((count & 0x08) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7])
                {
                    return false;
                }

                pointer1 += 8;
                pointer2 += 8;
            }

            if ((count & 0x04) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3])
                {
                    return false;
                }

                pointer1 += 4;
                pointer2 += 4;
            }

            if ((count & 0x02) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1])
                {
                    return false;
                }

                pointer1 += 2;
                pointer2 += 2;
            }

            if ((count & 0x01) != 0)
            {
                if (pointer1[0] != pointer2[0])
                    return false;
            }

            return true;
        }

        private static unsafe Boolean InternalSequenceEqualUnmanaged(Byte* pointer1, Byte* pointer2, Int32 count)
        {
            while (count >= 32)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7]
                    || pointer1[8] != pointer2[8]
                    || pointer1[9] != pointer2[9]
                    || pointer1[10] != pointer2[10]
                    || pointer1[11] != pointer2[11]
                    || pointer1[12] != pointer2[12]
                    || pointer1[13] != pointer2[13]
                    || pointer1[14] != pointer2[14]
                    || pointer1[15] != pointer2[15]
                    || pointer1[16] != pointer2[16]
                    || pointer1[17] != pointer2[17]
                    || pointer1[18] != pointer2[18]
                    || pointer1[19] != pointer2[19]
                    || pointer1[20] != pointer2[20]
                    || pointer1[21] != pointer2[21]
                    || pointer1[22] != pointer2[22]
                    || pointer1[23] != pointer2[23]
                    || pointer1[24] != pointer2[24]
                    || pointer1[25] != pointer2[25]
                    || pointer1[26] != pointer2[26]
                    || pointer1[27] != pointer2[27]
                    || pointer1[28] != pointer2[28]
                    || pointer1[29] != pointer2[29]
                    || pointer1[30] != pointer2[30]
                    || pointer1[31] != pointer2[31])
                {
                    return false;
                }

                pointer1 += 32;
                pointer2 += 32;
                count -= 32;
            }

            if ((count & 0x10) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7]
                    || pointer1[8] != pointer2[8]
                    || pointer1[9] != pointer2[9]
                    || pointer1[10] != pointer2[10]
                    || pointer1[11] != pointer2[11]
                    || pointer1[12] != pointer2[12]
                    || pointer1[13] != pointer2[13]
                    || pointer1[14] != pointer2[14]
                    || pointer1[15] != pointer2[15])
                {
                    return false;
                }

                pointer1 += 16;
                pointer2 += 16;
            }

            if ((count & 0x08) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3]
                    || pointer1[4] != pointer2[4]
                    || pointer1[5] != pointer2[5]
                    || pointer1[6] != pointer2[6]
                    || pointer1[7] != pointer2[7])
                {
                    return false;
                }

                pointer1 += 8;
                pointer2 += 8;
            }

            if ((count & 0x04) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3])
                {
                    return false;
                }

                pointer1 += 4;
                pointer2 += 4;
            }

            if ((count & 0x02) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1])
                {
                    return false;
                }

                pointer1 += 2;
                pointer2 += 2;
            }

            if ((count & 0x01) != 0)
            {
                if (pointer1[0] != pointer2[0])
                    return false;
            }

            return true;
        }

        #endregion
    }
}
