using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static partial class ArrayExtensions
    {
        #region SequenceEqual

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(array2);

            return ((ReadOnlySpan<ELEMENT_T>)array1).SequenceEqual((ReadOnlySpan<ELEMENT_T>)array2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(equalityComparer);

            return ((ReadOnlySpan<ELEMENT_T>)array1).SequenceEqual((ReadOnlySpan<ELEMENT_T>)array2, equalityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(keySelecter);

            return SequenceEqualCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return SequenceEqualCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyEqualityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array1);

            return ((ReadOnlySpan<ELEMENT_T>)array1).SequenceEqual(array2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(equalityComparer);

            return ((ReadOnlySpan<ELEMENT_T>)array1).SequenceEqual(array2, equalityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(keySelecter);

            return SequenceEqualCore((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return SequenceEqualCore((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter, keyEqualityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array2);

            return ((ReadOnlySpan<ELEMENT_T>)array1).SequenceEqual((ReadOnlySpan<ELEMENT_T>)array2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(equalityComparer);

            return ((ReadOnlySpan<ELEMENT_T>)array1).SequenceEqual((ReadOnlySpan<ELEMENT_T>)array2, equalityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(keySelecter);

            return SequenceEqualCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return SequenceEqualCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyEqualityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);

            return SequenceEqualCore((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return SequenceEqualCore((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter, keyEqualityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(equalityComparer);

            return array1.SequenceEqual((ReadOnlySpan<ELEMENT_T>)array2, equalityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(keySelecter);

            return SequenceEqualCore(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return SequenceEqualCore(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyEqualityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);

            return SequenceEqualCore(array1, array2, keySelecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return SequenceEqualCore(array1, array2, keySelecter, keyEqualityComparer);
        }

        #endregion

        #region SequenceEqualCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Boolean SequenceEqualCore<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (array1.Length != array2.Length)
                return false;

            var count = array1.Length;
            for (var index = 0; index < count; index++)
            {
                var key1 = keySelecter(array1[index]);
                var key2 = keySelecter(array2[index]);
                if (!Equal(key1, key2))
                    return false;
            }

            return true;

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Boolean Equal([AllowNull] KEY_T key1, [AllowNull] KEY_T key2)
            {
                return
                    key1 is null
                    ? key2 is null
                    : key1.Equals(key2);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Boolean SequenceEqualCore<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
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
    }
}
