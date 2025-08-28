using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Palmtree;

// public な拡張メソッドのクラスであるため、アセンブリの既定の名前空間に配置した。
#pragma warning disable IDE0130 // Namespace がフォルダー構造と一致しません
namespace Palmtree
#pragma warning restore IDE0130 // Namespace がフォルダー構造と一致しません
{
    public static partial class LinqExtensions
    {
        #region private class

        private sealed class ReadOnlyCollectionWrapper<ELEMENT_T>
            : IReadOnlyCollection<ELEMENT_T>
        {
            private readonly ICollection<ELEMENT_T> _internalCollection;

            public ReadOnlyCollectionWrapper(ICollection<ELEMENT_T> sourceCollection)
            {
                ArgumentNullException.ThrowIfNull(sourceCollection);

                _internalCollection = sourceCollection;
            }

            public Int32 Count => _internalCollection.Count;

            public IEnumerator<ELEMENT_T> GetEnumerator() => _internalCollection.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        #endregion

        #region AsEnumerable

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<ELEMENT_T> AsEnumerable<ELEMENT_T>(this ELEMENT_T[] source, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, source.Length);

            for (var index = offset; index < source.Length; ++index)
                yield return source[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<ELEMENT_T> AsEnumerable<ELEMENT_T>(this ELEMENT_T[] source, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, source.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, source.Length - offset);

            var limit = offset + count;
            for (var index = offset; index < limit; ++index)
                yield return source[index];
        }

        public static IEnumerable<ELEMENT_T> AsEnumerable<ELEMENT_T>(this Memory<ELEMENT_T> source)
        {
            for (var index = 0; index < source.Length; ++index)
                yield return source.Span[index];
        }

        public static IEnumerable<ELEMENT_T> AsEnumerable<ELEMENT_T>(this ReadOnlyMemory<ELEMENT_T> source)
        {
            for (var index = 0; index < source.Length; ++index)
                yield return source.Span[index];
        }

        #endregion

        #region None

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean None<ELEMENT_T>(this IEnumerable<ELEMENT_T> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return !source.Any();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean None<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Boolean> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);
            return !source.Any(predicate);
        }

        #endregion

        #region NotAll

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean NotAll<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Boolean> predicate)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            return !source.All(predicate);
        }

        #endregion

        #region NotAny

        /// <summary>
        /// 与えられた入力シーケンスに与えられた条件を満たさない要素が存在するかどうかを調べます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// シーケンスの要素の型です。
        /// </typeparam>
        /// <param name="source">
        /// 入力シーケンスです。
        /// </param>
        /// <param name="predicate">
        /// シーケンスの要素から真偽値を導き出すデリゲートです。
        /// </param>
        /// <returns>
        /// 与えられた条件 <paramref name="predicate"/> を満たさない要素が入力シーケンス <paramref name="source"/> に一つでも存在するのなら true、そうではないのなら false です。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotAny<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Boolean> predicate)
            => !source.All(predicate);

        #endregion

        #region QuickDistinct

        public static IEnumerable<ELEMENT_T> QuickDistinct<ELEMENT_T>(this IEnumerable<ELEMENT_T> source)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(source);

            return QuickDistinctCore(source, new Dictionary<ELEMENT_T, Object?>());
        }

        public static IEnumerable<ELEMENT_T> QuickDistinct<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, IEqualityComparer<ELEMENT_T> equalityComparer)
            where ELEMENT_T : notnull
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(equalityComparer);

            return QuickDistinctCore(source, new Dictionary<ELEMENT_T, Object?>(equalityComparer));
        }

        #endregion

        #region QuickSort

        public static IEnumerable<ELEMENT_T> QuickSort<ELEMENT_T>(this IEnumerable<ELEMENT_T> source)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(source);

            var sourceArray = source.ToArray();
            sourceArray.AsSpan().QuickSortCore();
            return sourceArray;
        }

        public static IEnumerable<ELEMENT_T> QuickSort<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, IComparer<ELEMENT_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keyComparer);

            var sourceArray = source.ToArray();
            sourceArray.AsSpan().QuickSortCore(keyComparer);
            return sourceArray;
        }

        public static IEnumerable<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySekecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySekecter);

            var sourceArray = source.ToArray();
            sourceArray.AsSpan().QuickSortCore(keySekecter);
            return sourceArray;
        }

        public static IEnumerable<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySekecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySekecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            var sourceArray = source.ToArray();
            sourceArray.AsSpan().QuickSortCore(keySekecter, keyComparer);
            return sourceArray;
        }

        #endregion

        #region SequenceCompareTo

        public static Int32 SequenceCompareTo<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, IEnumerable<ELEMENT_T> other)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var element1 = enumerator1.Current;
                    var element2 = enumerator2.Current;
                    if (element1 is null)
                    {
                        if (element2 is not null)
                            return -1;
                    }
                    else
                    {
                        if (element2 is null)
                        {
                            return 1;
                        }
                        else
                        {
                            if ((c = element1.CompareTo(element2)) != 0)
                                return c;
                        }
                    }
                }
            }
            finally
            {
                enumerator1?.Dispose();
                enumerator2?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(comparer);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var element1 = enumerator1.Current;
                    var element2 = enumerator2.Current;
                    if ((c = comparer.Compare(element1, element2)) != 0)
                        return c;
                }
            }
            finally
            {
                enumerator1?.Dispose();
                enumerator2?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(enumerator2.Current);
                    if (key1 is null)
                    {
                        if (key2 is not null)
                            return -1;
                    }
                    else
                    {
                        if (key2 is null)
                        {
                            return 1;
                        }
                        else
                        {
                            if ((c = key1.CompareTo(key2)) != 0)
                                return c;
                        }
                    }
                }
            }
            finally
            {
                enumerator1?.Dispose();
                enumerator2?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(enumerator2.Current);
                    if ((c = keyComparer.Compare(key1, key2)) != 0)
                        return c;
                }
            }
            finally
            {
                enumerator1?.Dispose();
                enumerator2?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, ELEMENT_T[] other)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var element1 = enumerator1.Current;
                    var element2 = other[index2];
                    if (element1 is null)
                    {
                        if (element2 is not null)
                            return -1;
                    }
                    else
                    {
                        if ((c = element1.CompareTo(element2)) != 0)
                            return c;
                    }

                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, ELEMENT_T[] other, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(comparer);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var element1 = enumerator1.Current;
                    var element2 = other[index2];
                    if ((c = comparer.Compare(element1, element2)) != 0)
                        return c;
                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, ELEMENT_T[] other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(other[index2]);
                    if (key1 is null)
                    {
                        if (key2 is not null)
                            return -1;
                    }
                    else
                    {
                        if ((c = key1.CompareTo(key2)) != 0)
                            return c;
                    }

                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, ELEMENT_T[] other, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(other[index2]);
                    if ((c = keyComparer.Compare(key1, key2)) != 0)
                        return c;
                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Span<ELEMENT_T> other)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SequenceCompareTo((ReadOnlySpan<ELEMENT_T>)other);
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Span<ELEMENT_T> other, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            return source.SequenceCompareTo((ReadOnlySpan<ELEMENT_T>)other, comparer);
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, Span<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelecter);

            return source.SequenceCompareTo((ReadOnlySpan<ELEMENT_T>)other, keySelecter);
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, Span<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            return source.SequenceCompareTo((ReadOnlySpan<ELEMENT_T>)other, keySelecter, keyComparer);
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, ReadOnlySpan<ELEMENT_T> other)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(source);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var element1 = enumerator1.Current;
                    var element2 = other[index2];
                    if (element1 is null)
                    {
                        if (element2 is not null)
                            return -1;
                    }
                    else
                    {
                        if ((c = element1.CompareTo(element2)) != 0)
                            return c;
                    }

                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, ReadOnlySpan<ELEMENT_T> other, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(comparer);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var element1 = enumerator1.Current;
                    var element2 = other[index2];
                    if ((c = comparer.Compare(element1, element2)) != 0)
                        return c;
                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, ReadOnlySpan<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelecter);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(other[index2]);
                    if (key1 is null)
                    {
                        if (key2 is not null)
                            return -1;
                    }
                    else
                    {
                        if ((c = key1.CompareTo(key2)) != 0)
                            return c;
                    }

                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, ReadOnlySpan<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    Int32 c;
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(other[index2]);
                    if ((c = keyComparer.Compare(key1, key2)) != 0)
                        return c;
                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this ELEMENT_T[] source, IEnumerable<ELEMENT_T> other)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var element1 = source[index1];
                    var element2 = enumerator2.Current;
                    if (element1 is null)
                    {
                        if (element2 is not null)
                            return -1;
                    }
                    else
                    {
                        if (element2 is null)
                        {
                            return 1;
                        }
                        else
                        {
                            if ((c = element1.CompareTo(element2)) != 0)
                                return c;
                        }
                    }

                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this ELEMENT_T[] source, IEnumerable<ELEMENT_T> other, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(comparer);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var element1 = source[index1];
                    var element2 = enumerator2.Current;
                    if ((c = comparer.Compare(element1, element2)) != 0)
                        return c;
                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ELEMENT_T[] source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var key1 = keySelecter(source[index1]);
                    var key2 = keySelecter(enumerator2.Current);
                    if (key1 is null)
                    {
                        if (key2 is not null)
                            return -1;
                    }
                    else
                    {
                        if (key2 is null)
                        {
                            return 1;
                        }
                        else
                        {
                            if ((c = key1.CompareTo(key2)) != 0)
                                return c;
                        }
                    }

                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ELEMENT_T[] source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var key1 = keySelecter(source[index1]);
                    var key2 = keySelecter(enumerator2.Current);
                    if ((c = keyComparer.Compare(key1, key2)) != 0)
                        return c;
                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this Span<ELEMENT_T> source, IEnumerable<ELEMENT_T> other)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(other);

            return ((ReadOnlySpan<ELEMENT_T>)source).SequenceCompareTo(other);
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this Span<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(comparer);

            return ((ReadOnlySpan<ELEMENT_T>)source).SequenceCompareTo(other, comparer);
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);

            return ((ReadOnlySpan<ELEMENT_T>)source).SequenceCompareTo(other, keySelecter);
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            return ((ReadOnlySpan<ELEMENT_T>)source).SequenceCompareTo(other, keySelecter, keyComparer);
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> source, IEnumerable<ELEMENT_T> other)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(other);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var element1 = source[index1];
                    var element2 = enumerator2.Current;
                    if (element1 is null)
                    {
                        if (element2 is not null)
                            return -1;
                    }
                    else
                    {
                        if (element2 is null)
                        {
                            return 1;
                        }
                        else
                        {
                            if ((c = element1.CompareTo(element2)) != 0)
                                return c;
                        }
                    }

                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(comparer);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var element1 = source[index1];
                    var element2 = enumerator2.Current;
                    if ((c = comparer.Compare(element1, element2)) != 0)
                        return c;
                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var key1 = keySelecter(source[index1]);
                    var key2 = keySelecter(enumerator2.Current);
                    if (key1 is null)
                    {
                        if (key2 is not null)
                            return -1;
                    }
                    else
                    {
                        if (key2 is null)
                        {
                            return 1;
                        }
                        else
                        {
                            if ((c = key1.CompareTo(key2)) != 0)
                                return c;
                        }
                    }

                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    Int32 c;
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if ((c = isOk1.CompareTo(isOk2)) != 0)
                        return c;
                    if (!isOk1)
                        return 0;
                    var key1 = keySelecter(source[index1]);
                    var key2 = keySelecter(enumerator2.Current);
                    if ((c = keyComparer.Compare(key1, key2)) != 0)
                        return c;
                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        #endregion

        #region SequenceEqual

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = enumerator2.MoveNext();
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(enumerator2.Current);
                    if (key1 is null)
                    {
                        if (key2 is not null)
                            return false;
                    }
                    else
                    {
                        if (!key1.Equals(key2))
                            return false;
                    }
                }
            }
            finally
            {
                enumerator1?.Dispose();
                enumerator2?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = enumerator2.MoveNext();
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(enumerator2.Current);
                    if (!keyEqualityComparer.Equals(key1, key2))
                        return false;
                }
            }
            finally
            {
                enumerator1?.Dispose();
                enumerator2?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, ELEMENT_T[] other)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var element1 = enumerator1.Current;
                    var element2 = other[index2];
                    if (element1 is null)
                    {
                        if (element2 is not null)
                            return false;
                    }
                    else
                    {
                        if (!element1.Equals(element2))
                            return false;
                    }

                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, ELEMENT_T[] other, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(equalityComparer);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var element1 = enumerator1.Current;
                    var element2 = other[index2];
                    if (!equalityComparer.Equals(element1, element2))
                        return false;
                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, ELEMENT_T[] other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(other[index2]);
                    if (key1 is null)
                    {
                        if (key2 is not null)
                            return false;
                    }
                    else
                    {
                        if (!key1.Equals(key2))
                            return false;
                    }

                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, ELEMENT_T[] other, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(other[index2]);
                    if (!keyEqualityComparer.Equals(key1, key2))
                        return false;
                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Span<ELEMENT_T> other)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SequenceEqual((ReadOnlySpan<ELEMENT_T>)other);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Span<ELEMENT_T> other, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(equalityComparer);

            return source.SequenceEqual((ReadOnlySpan<ELEMENT_T>)other, equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, Span<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelecter);

            return source.SequenceEqual((ReadOnlySpan<ELEMENT_T>)other, keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, Span<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return source.SequenceEqual((ReadOnlySpan<ELEMENT_T>)other, keySelecter, keyEqualityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, ReadOnlySpan<ELEMENT_T> other)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(source);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var element1 = enumerator1.Current;
                    var element2 = other[index2];
                    if (element1 is null)
                    {
                        if (element2 is not null)
                            return false;
                    }
                    else
                    {
                        if (!element1.Equals(element2))
                            return false;
                    }

                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, ReadOnlySpan<ELEMENT_T> other, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(equalityComparer);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var element1 = enumerator1.Current;
                    var element2 = other[index2];
                    if (!equalityComparer.Equals(element1, element2))
                        return false;
                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, ReadOnlySpan<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelecter);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(other[index2]);
                    if (key1 is null)
                    {
                        if (key2 is not null)
                            return false;
                    }
                    else
                    {
                        if (!key1.Equals(key2))
                            return false;
                    }

                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this IEnumerable<ELEMENT_T> source, ReadOnlySpan<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            var enumerator1 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                enumerator1 = source.GetEnumerator();
                var index2 = 0;
                while (true)
                {
                    var isOk1 = enumerator1.MoveNext();
                    var isOk2 = index2 < other.Length;
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var key1 = keySelecter(enumerator1.Current);
                    var key2 = keySelecter(other[index2]);
                    if (!keyEqualityComparer.Equals(key1, key2))
                        return false;
                    ++index2;
                }
            }
            finally
            {
                enumerator1?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] source, IEnumerable<ELEMENT_T> other)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var element1 = source[index1];
                    var element2 = enumerator2.Current;
                    if (element1 is null)
                    {
                        if (element2 is not null)
                            return false;
                    }
                    else
                    {
                        if (!element1.Equals(element2))
                            return false;
                    }

                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] source, IEnumerable<ELEMENT_T> other, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(equalityComparer);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var element1 = source[index1];
                    var element2 = enumerator2.Current;
                    if (!equalityComparer.Equals(element1, element2))
                        return false;
                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var key1 = keySelecter(source[index1]);
                    var key2 = keySelecter(enumerator2.Current);
                    if (key1 is null)
                    {
                        if (key2 is not null)
                            return false;
                    }
                    else
                    {
                        if (!key1.Equals(key2))
                            return false;
                    }

                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ELEMENT_T[] source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var key1 = keySelecter(source[index1]);
                    var key2 = keySelecter(enumerator2.Current);
                    if (!keyEqualityComparer.Equals(key1, key2))
                        return false;
                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this Span<ELEMENT_T> source, IEnumerable<ELEMENT_T> other)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(other);

            return ((ReadOnlySpan<ELEMENT_T>)source).SequenceEqual(other);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this Span<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(equalityComparer);

            return ((ReadOnlySpan<ELEMENT_T>)source).SequenceEqual(other, equalityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);

            return ((ReadOnlySpan<ELEMENT_T>)source).SequenceEqual(other, keySelecter);
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return ((ReadOnlySpan<ELEMENT_T>)source).SequenceEqual(other, keySelecter, keyEqualityComparer);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> source, IEnumerable<ELEMENT_T> other)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(other);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var element1 = source[index1];
                    var element2 = enumerator2.Current;
                    if (element1 is null)
                    {
                        if (element2 is not null)
                            return false;
                    }
                    else
                    {
                        if (!element1.Equals(element2))
                            return false;
                    }

                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(equalityComparer);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var element1 = source[index1];
                    var element2 = enumerator2.Current;
                    if (!equalityComparer.Equals(element1, element2))
                        return false;
                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var key1 = keySelecter(source[index1]);
                    var key2 = keySelecter(enumerator2.Current);
                    if (key1 is null)
                    {
                        if (key2 is not null)
                            return false;
                    }
                    else
                    {
                        if (!key1.Equals(key2))
                            return false;
                    }

                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        public static Boolean SequenceEqual<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> source, IEnumerable<ELEMENT_T> other, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            ArgumentNullException.ThrowIfNull(other);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            var enumerator2 = (IEnumerator<ELEMENT_T>?)null;
            try
            {
                var index1 = 0;
                enumerator2 = other.GetEnumerator();
                while (true)
                {
                    var isOk1 = index1 < source.Length;
                    var isOk2 = enumerator2.MoveNext();
                    if (isOk1 != isOk2)
                        return false;
                    if (!isOk1)
                        return true;
                    var key1 = keySelecter(source[index1]);
                    var key2 = keySelecter(enumerator2.Current);
                    if (!keyEqualityComparer.Equals(key1, key2))
                        return false;
                    ++index1;
                }
            }
            finally
            {
                enumerator2?.Dispose();
            }
        }

        #endregion

        #region SingleOrNone

        /// <summary>
        /// 与えられた入力シーケンスから0個または1個の要素を取得します。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// シーケンスの要素の型です。
        /// </typeparam>
        /// <param name="source">
        /// 入力シーケンスです。
        /// </param>
        /// <returns>
        /// 入力シーケンス <paramref name="source"/> が空である場合は要素の default(<typeparamref name="ELEMENT_T"/>) 既定値が返ります。(例えば要素の型が参照型ならば null です)
        /// 入力シーケンス <paramref name="source"/> に要素が 1 つしかない場合はその要素が返ります。
        /// </returns>
        /// <exception cref="ArgumentException">
        /// 入力シーケンス <paramref name="source"/> に要素が 2 つ以上あります。
        /// </exception>
        public static ELEMENT_T? SingleOrNone<ELEMENT_T>(this IEnumerable<ELEMENT_T> source)
        {
            var matchedItems = source.Take(2).ToList();
            if (matchedItems.Count > 1)
                throw new ArgumentException($"{nameof(source)} contains multiple elements.");

            return matchedItems.Count > 0 ? matchedItems.First() : default;
        }

        /// <summary>
        /// 与えられた入力シーケンスから与えられた条件を満たす要素を0個または1個取得します。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// シーケンスの要素の型です。
        /// </typeparam>
        /// <param name="source">
        /// 入力シーケンスです。
        /// </param>
        /// <param name="predicate">
        /// 入力シーケンスの要素から真偽値を導き出すデリゲートです。
        /// </param>
        /// <returns>
        /// 入力シーケンス <paramref name="source"/> に条件 <paramref name="predicate"/> を満たす要素が存在しない場合は default(<typeparamref name="ELEMENT_T"/>) 既定値が返ります。(例えば要素の型が参照型ならば null です)
        /// 入力シーケンス <paramref name="source"/> に条件 <paramref name="predicate"/> を満たす要素が 1 つだけ存在する場合はその要素が返ります。
        /// </returns>
        /// <exception cref="ArgumentException">
        /// 入力シーケンス <paramref name="source"/> に条件 <paramref name="predicate"/> を満たす要素が 2 つ以上あります。
        /// </exception>
        public static ELEMENT_T? SingleOrNone<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, Boolean> predicate)
        {
            var matchedItems = source.Where(predicate).Take(2).ToList();
            if (matchedItems.Count > 1)
                throw new ArgumentException($"More than one element of {nameof(source)} matched the condition of {nameof(predicate)}.");

            return matchedItems.Count > 0 ? matchedItems.First() : default;
        }

        #endregion

        #region WhereNotNull

        public static IEnumerable<ELEMENT_T> WhereNotNull<ELEMENT_T>(this IEnumerable<ELEMENT_T?> source)
            where ELEMENT_T : notnull
        {
            foreach (var element in source)
            {
                if (element is not null)
                    yield return element;
            }
        }

        public static IEnumerable<ELEMENT_T> WhereNotNull<ELEMENT_T>(this IEnumerable<ELEMENT_T?> source)
            where ELEMENT_T : struct
        {
            foreach (var element in source)
            {
                if (element is not null)
                    yield return element.Value;
            }
        }

        #endregion

        #region EnumeratePermutations

        public static IEnumerable<IEnumerable<ELEMENT_T>> EnumeratePermutations<ELEMENT_T>(this IEnumerable<ELEMENT_T> source)
        {
            var sourceArray = (ReadOnlyMemory<ELEMENT_T>)source.ToArray();
            return sourceArray.EnumeratePermutationsCore();
        }

        #endregion

        #region ForEach

        public static void ForEach<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, Action<ELEMENT_T> action)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(action);

            foreach (var element in source)
                action(element);
        }

        #endregion

        #region IsSingle

        public static Boolean IsSingle<ELEMENT_T>(this IEnumerable<ELEMENT_T> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.Take(2).Count() == 1;
        }

        #endregion

        #region ToReadOnlyCollection

        public static IReadOnlyCollection<ELEMENT_T> ToReadOnlyCollection<ELEMENT_T>(this IEnumerable<ELEMENT_T> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return new ReadOnlyCollectionWrapper<ELEMENT_T>([.. source]);
        }

        #endregion

        #region ToReadOnlyMemory

        public static ReadOnlyMemory<ELEMENT_T> ToReadOnlyMemory<ELEMENT_T>(this IEnumerable<ELEMENT_T> source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return (ReadOnlyMemory<ELEMENT_T>)source.ToArray();
        }

        #endregion

        #region EnumeratePermutationsCore

        private static IEnumerable<IEnumerable<ELEMENT_T>> EnumeratePermutationsCore<ELEMENT_T>(this ReadOnlyMemory<ELEMENT_T> source)
        {
            if (source.Length < 2)
            {
                yield return source.AsEnumerable();
            }
            else
            {
                for (var index = 0; index < source.Length; ++index)
                {
                    var firstElement = source.Span[index];
                    var otherElements = new ELEMENT_T[source.Length - 1].AsMemory();
                    source[..index].CopyTo(otherElements[..index]);
                    source[(index + 1)..].CopyTo(otherElements[index..]);
                    foreach (var permutation in otherElements.AsReadOnly().EnumeratePermutationsCore())
                        yield return permutation.Prepend(firstElement);
                }
            }
        }

        #endregion

        #region MaxCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxCore<ELEMENT_T>(this IEnumerable<ELEMENT_T> source)
            where ELEMENT_T : struct, IComparisonOperators<ELEMENT_T, ELEMENT_T, Boolean>
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var maximumValue = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                if (value > maximumValue)
                    maximumValue = value;
            }

            return maximumValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T MaxCore<ELEMENT_T, VALUE_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, IComparisonOperators<VALUE_T, VALUE_T, Boolean>
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var maximumValue = selector(enumerator.Current);
            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);
                if (value > maximumValue)
                    maximumValue = value;
            }

            return maximumValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxCore<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, IComparer<ELEMENT_T> keyComparer)
            where ELEMENT_T : struct
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var maximumValue = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                if (keyComparer.Compare(value, maximumValue) > 0)
                    maximumValue = value;
            }

            return maximumValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T MaxCore<ELEMENT_T, VALUE_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, VALUE_T> selector, IComparer<VALUE_T> keyComparer)
            where VALUE_T : struct
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var maximumValue = selector(enumerator.Current);
            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);
                if (keyComparer.Compare(value, maximumValue) > 0)
                    maximumValue = value;
            }

            return maximumValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T? MaxCore<ELEMENT_T>(this IEnumerable<ELEMENT_T?> source)
            where ELEMENT_T : struct, IComparisonOperators<ELEMENT_T, ELEMENT_T, Boolean>
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var maximumValue = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                if (GreaterThan(value, maximumValue))
                    maximumValue = value;
            }

            return maximumValue;

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Boolean GreaterThan(ELEMENT_T? left, ELEMENT_T? right)
            {
                return left is not null && (right is null || left.Value > right.Value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T? MaxCore<ELEMENT_T, VALUE_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, IComparisonOperators<VALUE_T, VALUE_T, Boolean>
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var maximumValue = selector(enumerator.Current);
            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);
                if (GreaterThan(value, maximumValue))
                    maximumValue = value;
            }

            return maximumValue;

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Boolean GreaterThan(VALUE_T? left, VALUE_T? right)
            {
                return left is not null && (right is null || left.Value > right.Value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T? MaxCore<ELEMENT_T>(this IEnumerable<ELEMENT_T?> source, IComparer<ELEMENT_T?> keyComparer)
            where ELEMENT_T : struct
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var maximumValue = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                if (keyComparer.Compare(value, maximumValue) > 0)
                    maximumValue = value;
            }

            return maximumValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T? MaxCore<ELEMENT_T, VALUE_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, VALUE_T?> selector, IComparer<VALUE_T?> keyComparer)
            where VALUE_T : struct
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var maximumValue = selector(enumerator.Current);
            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);
                if (keyComparer.Compare(value, maximumValue) > 0)
                    maximumValue = value;
            }

            return maximumValue;
        }

        #endregion

        #region MinCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinCore<ELEMENT_T>(this IEnumerable<ELEMENT_T> source)
            where ELEMENT_T : struct, IComparisonOperators<ELEMENT_T, ELEMENT_T, Boolean>
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var minimumValue = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                if (value < minimumValue)
                    minimumValue = value;
            }

            return minimumValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T MinCore<ELEMENT_T, VALUE_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, IComparisonOperators<VALUE_T, VALUE_T, Boolean>
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var minimumValue = selector(enumerator.Current);
            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);
                if (value < minimumValue)
                    minimumValue = value;
            }

            return minimumValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinCore<ELEMENT_T>(this IEnumerable<ELEMENT_T> source, IComparer<ELEMENT_T> keyComparer)
            where ELEMENT_T : struct
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var minimumValue = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                if (keyComparer.Compare(value, minimumValue) < 0)
                    minimumValue = value;
            }

            return minimumValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T MinCore<ELEMENT_T, VALUE_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, VALUE_T> selector, IComparer<VALUE_T> keyComparer)
            where VALUE_T : struct
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var minimumValue = selector(enumerator.Current);
            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);
                if (keyComparer.Compare(value, minimumValue) < 0)
                    minimumValue = value;
            }

            return minimumValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T? MinCore<ELEMENT_T>(this IEnumerable<ELEMENT_T?> source)
            where ELEMENT_T : struct, IComparisonOperators<ELEMENT_T, ELEMENT_T, Boolean>
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var minimumValue = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                if (LesserThan(value, minimumValue))
                    minimumValue = value;
            }

            return minimumValue;

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Boolean LesserThan(ELEMENT_T? left, ELEMENT_T? right)
            {
                return right is not null && (left is null || left.Value < right.Value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T? MinCore<ELEMENT_T, VALUE_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, IComparisonOperators<VALUE_T, VALUE_T, Boolean>
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var minimumValue = selector(enumerator.Current);
            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);
                if (LesserThan(value, minimumValue))
                    minimumValue = value;
            }

            return minimumValue;

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Boolean LesserThan(VALUE_T? left, VALUE_T? right)
            {
                return right is not null && (left is null || left.Value < right.Value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T? MinCore<ELEMENT_T>(this IEnumerable<ELEMENT_T?> source, IComparer<ELEMENT_T?> keyComparer)
            where ELEMENT_T : struct
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var minimumValue = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                if (keyComparer.Compare(value, minimumValue) < 0)
                    minimumValue = value;
            }

            return minimumValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static VALUE_T? MinCore<ELEMENT_T, VALUE_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, VALUE_T?> selector, IComparer<VALUE_T?> keyComparer)
            where VALUE_T : struct
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException();
            var minimumValue = selector(enumerator.Current);
            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);
                if (keyComparer.Compare(value, minimumValue) < 0)
                    minimumValue = value;
            }

            return minimumValue;
        }

        #endregion

        #region SumCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T SumCore<ELEMENT_T, RESULT_T>(this IEnumerable<ELEMENT_T> source)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            using var enumerator = source.GetEnumerator();
            var sum = RESULT_T.Zero;
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
                checked
                {
                    sum += RESULT_T.CreateChecked(value);
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T SumCore<ELEMENT_T, VALUE_T, RESULT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, VALUE_T> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            using var enumerator = source.GetEnumerator();
            var sum = RESULT_T.Zero;
            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);
                checked
                {
                    sum += RESULT_T.CreateChecked(value);
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static RESULT_T? SumCore<ELEMENT_T, RESULT_T>(this IEnumerable<ELEMENT_T?> source)
            where ELEMENT_T : struct, INumberBase<ELEMENT_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            using var enumerator = source.GetEnumerator();
            var sum = RESULT_T.Zero;
            while (enumerator.MoveNext())
            {
                var value = enumerator.Current;
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
        private static RESULT_T? SumCore<ELEMENT_T, VALUE_T, RESULT_T>(this IEnumerable<ELEMENT_T> source, Func<ELEMENT_T, VALUE_T?> selector)
            where VALUE_T : struct, INumberBase<VALUE_T>
            where RESULT_T : struct, INumberBase<RESULT_T>
        {
            using var enumerator = source.GetEnumerator();
            var sum = RESULT_T.Zero;
            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);
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

        #region QuickDistinctCore

        private static IEnumerable<ELEMENT_T> QuickDistinctCore<ELEMENT_T>(IEnumerable<ELEMENT_T> source, IDictionary<ELEMENT_T, Object?> outputElements)
            => source
                .Where(element =>
                {
                    if (outputElements.ContainsKey(element))
                        return false;
                    outputElements[element] = null;
                    return true;
                });

        #endregion

        #region TryGetSpan

        [MethodImpl(MethodImplOptions.AggressiveInlining)] // fast type checks that don't add a lot of overhead
        private static Boolean TryGetSpan<TSource>(this IEnumerable<TSource> source, out ReadOnlySpan<TSource> span)
        {
            if (source.GetType() == typeof(TSource[]))
            {
                span = Unsafe.As<TSource[]>(source);
                return true;
            }
            else if (source.GetType() == typeof(List<TSource>))
            {
                span = CollectionsMarshal.AsSpan(Unsafe.As<List<TSource>>(source));
                return true;
            }
            else
            {
                span = default;
                return false;
            }
        }

        #endregion
    }
}
