//#define DEBUG_QUICKSORT
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    partial class ArrayExtensions
    {
        #region QuickSort

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            InternalQuickSort(sourceArray.AsSpan());
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Func<ELEMENT_T, KEY_T> keySekecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));

            InternalQuickSort(sourceArray.AsSpan(), keySekecter);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, IComparer<ELEMENT_T> comparer)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            InternalQuickSort(sourceArray.AsSpan(), comparer);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Func<ELEMENT_T, KEY_T> keySekecter, IComparer<KEY_T> keyComparer)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            InternalQuickSort(sourceArray.AsSpan(), keySekecter, keyComparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            var (offset, count) = sourceArray.GetOffsetAndLength(range, nameof(range));
            InternalQuickSort(sourceArray.AsSpan(offset, count));
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Range range, Func<ELEMENT_T, KEY_T> keySekecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));

            var (offset, count) = sourceArray.GetOffsetAndLength(range, nameof(range));
            InternalQuickSort(sourceArray.AsSpan(offset, count), keySekecter);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range, IComparer<ELEMENT_T> comparer)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            var (offset, count) = sourceArray.GetOffsetAndLength(range, nameof(range));
            InternalQuickSort(sourceArray.AsSpan(offset, count), comparer);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Range range, Func<ELEMENT_T, KEY_T> keySekecter, IComparer<KEY_T> keyComparer)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            var (offset, count) = sourceArray.GetOffsetAndLength(range, nameof(range));
            InternalQuickSort(sourceArray.AsSpan(offset, count), keySekecter, keyComparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");

            InternalQuickSort(sourceArray.AsSpan(offset, count));
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySekecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));

            InternalQuickSort(sourceArray.AsSpan(offset, count), keySekecter);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 count, IComparer<ELEMENT_T> comparer)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            InternalQuickSort(sourceArray.AsSpan(offset, count), comparer);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySekecter, IComparer<KEY_T> keyComparer)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            InternalQuickSort(sourceArray.AsSpan(offset, count), keySekecter, keyComparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (checked(offset + count) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");

            InternalQuickSort(sourceArray.AsSpan(checked((Int32)offset), checked((Int32)count)));
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count, Func<ELEMENT_T, KEY_T> keySekecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (checked(offset + count) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));

            InternalQuickSort(sourceArray.AsSpan(checked((Int32)offset), checked((Int32)count)), keySekecter);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count, IComparer<ELEMENT_T> comparer)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (checked(offset + count) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            InternalQuickSort(sourceArray.AsSpan(checked((Int32)offset), checked((Int32)count)), comparer);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count, Func<ELEMENT_T, KEY_T> keySekecter, IComparer<KEY_T> keyComparer)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (checked(offset + count) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            InternalQuickSort(sourceArray.AsSpan(checked((Int32)offset), checked((Int32)count)), keySekecter, keyComparer);
            return sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<ELEMENT_T> QuickSort<ELEMENT_T>(this Span<ELEMENT_T> sourceArray)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            InternalQuickSort(sourceArray);
            return sourceArray;
        }

        public static Span<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> sourceArray, Func<ELEMENT_T, KEY_T> keySekecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));

            InternalQuickSort(sourceArray, keySekecter);
            return sourceArray;
        }

        public static Span<ELEMENT_T> QuickSort<ELEMENT_T>(this Span<ELEMENT_T> sourceArray, IComparer<ELEMENT_T> comparer)
        {
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            InternalQuickSort(sourceArray, comparer);
            return sourceArray;
        }

        public static Span<ELEMENT_T> QuickSort<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> sourceArray, Func<ELEMENT_T, KEY_T> keySekecter, IComparer<KEY_T> keyComparer)
        {
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            InternalQuickSort(sourceArray, keySekecter, keyComparer);
            return sourceArray;
        }

        #endregion

        #region InternalQuickSort

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalQuickSort<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
            => InternalQuickSortManaged(source, keySelector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalQuickSort<ELEMENT_T>(Span<ELEMENT_T> source, IComparer<ELEMENT_T> keyComparer)
            => InternalQuickSortManaged(source, keyComparer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalQuickSort<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
            => InternalQuickSortManaged(source, keySelector, keyComparer);

        #endregion

        #region InternalQuickSortManaged

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        private static void InternalQuickSortManaged<ELEMENT_T>(Span<ELEMENT_T> source)
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
                        (source[0], source[^1]) = (source[^1], source[0]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[startIndex] のキー値を pivotKey とする。

                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = source[0];
                var lowerBoundary = 1;
                var upperBoundary = source.Length - 1;
                var endOfPivotKeys = 1;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, source.Length) : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
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
                            Assert(source[lowerBoundary].CompareTo(pivotKey) > 0);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Assert(source[lowerBoundary].CompareTo(pivotKey) <= 0);
#endif
                        if (c == 0)
                        {
                            // source[lowerBoundary] == pivotKey である場合
#if DEBUG
                            Assert(source[lowerBoundary].CompareTo(pivotKey) == 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (endOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[endOfPivotKeys] < pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
                                (source[endOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[endOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Assert(endOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++endOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey && source[endOfPivotKeys] != pivotKey
                    Assert(lowerBoundary > upperBoundary || source[lowerBoundary].CompareTo(pivotKey) > 0 && source[endOfPivotKeys].CompareTo(pivotKey) != 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = source[upperBoundary].CompareTo(pivotKey);
                        if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            if (endOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] == pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[endOfPivotKeys] < pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Assert(source[upperBoundary].CompareTo(pivotKey) == 0 && source[lowerBoundary].CompareTo(pivotKey) > 0 && source[endOfPivotKeys].CompareTo(pivotKey) < 0);
#endif
                                var t = source[endOfPivotKeys];
                                source[endOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] == pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) endOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Assert(source[upperBoundary].CompareTo(pivotKey) == 0 && source[lowerBoundary].CompareTo(pivotKey) > 0 && endOfPivotKeys == lowerBoundary);
#endif
                                (source[endOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[endOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++endOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
                            Assert(endOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より小さい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Assert(endOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startIndex, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, source.Length) : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - 0).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = 0;

                // 入れ替えるもう片方の開始位置 (region-b の終端位置)
                var exEndIndex = upperBoundary;

                // 入れ替える値がなくなるまで繰り返す
                while (exStartIndex < exEndIndex)
                {
                    // 値を入れ替える
                    (source[exStartIndex], source[exEndIndex]) = (source[exEndIndex], source[exStartIndex]);

                    // 入れ替える値の位置を変更する
                    ++exStartIndex;
                    --exEndIndex;
                }

                // この時点で、配列の並びは以下の通り
                // region-b) [startIndex, startIndex + upperBoundary - endOfPivotKeys] : x < pivotKey であるキー値 x を持つ要素の集合
                // region-a) [startIndex + lowerBoundary - endOfPivotKeys, upperBoundary] : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) [lowerBoundary, source.Length): x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = 0; index <= 0 + upperBoundary - endOfPivotKeys; ++index)
                    Assert(source[index].CompareTo(pivotKey) < 0);
                for (var index = 0 + lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(source[index].CompareTo(pivotKey) == 0);
                for (var index = lowerBoundary; index <= source.Length - 1; ++index)
                    Assert(source[index].CompareTo(pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source[..(lowerBoundary - endOfPivotKeys)]);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source[lowerBoundary..]);
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
        private static void InternalQuickSortManaged<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelector)
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
                        (source[0], source[^1]) = (source[^1], source[0]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[0] のキー値を pivotKey とする。

                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = keySelector(source[0]);
                var lowerBoundary = 1;
                var upperBoundary = source.Length - 1;
                var endOfPivotKeys = 1;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [0, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, source.Length) : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
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
                            Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) <= 0);
#endif
                        if (c == 0)
                        {
                            // source[lowerBoundary] == pivotKey である場合
#if DEBUG
                            Assert(keySelector(source[lowerBoundary]).CompareTo(pivotKey) == 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (endOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[endOfPivotKeys] < pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
                                (source[endOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[endOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Assert(endOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++endOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey && source[endOfPivotKeys] != pivotKey
                    Assert(lowerBoundary > upperBoundary || keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0 && keySelector(source[endOfPivotKeys]).CompareTo(pivotKey) != 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keySelector(source[upperBoundary]).CompareTo(pivotKey);
                        if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            if (endOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] == pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[endOfPivotKeys] < pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Assert(keySelector(source[upperBoundary]).CompareTo(pivotKey) == 0 && keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0 && keySelector(source[endOfPivotKeys]).CompareTo(pivotKey) < 0);
#endif
                                var t = source[endOfPivotKeys];
                                source[endOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] == pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) endOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Assert(keySelector(source[upperBoundary]).CompareTo(pivotKey) == 0 && keySelector(source[lowerBoundary]).CompareTo(pivotKey) > 0 && endOfPivotKeys == lowerBoundary);
#endif
                                (source[endOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[endOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++endOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
                            Assert(endOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より小さい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Assert(endOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [0, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, source.Length) : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - 0).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = 0;

                // 入れ替えるもう片方の開始位置 (region-b の終端位置)
                var exEndIndex = upperBoundary;

                // 入れ替える値がなくなるまで繰り返す
                while (exStartIndex < exEndIndex)
                {
                    // 値を入れ替える
                    (source[exStartIndex], source[exEndIndex]) = (source[exEndIndex], source[exStartIndex]);

                    // 入れ替える値の位置を変更する
                    ++exStartIndex;
                    --exEndIndex;
                }

                // この時点で、配列の並びは以下の通り
                // region-b) [0, upperBoundary - endOfPivotKeys] : x < pivotKey であるキー値 x を持つ要素の集合
                // region-a) [lowerBoundary - endOfPivotKeys, upperBoundary] : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) [lowerBoundary, source.Length): x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = 0; index <= 0 + upperBoundary - endOfPivotKeys; ++index)
                    Assert(keySelector(source[index]).CompareTo(pivotKey) < 0);
                for (var index = 0 + lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(keySelector(source[index]).CompareTo(pivotKey) == 0);
                for (var index = lowerBoundary; index <= source.Length - 1; ++index)
                    Assert(keySelector(source[index]).CompareTo(pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source[..(lowerBoundary - endOfPivotKeys)], keySelector);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source[lowerBoundary..], keySelector);
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
        private static void InternalQuickSortManaged<ELEMENT_T>(Span<ELEMENT_T> source, IComparer<ELEMENT_T> keyComparer)
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
                    if (keyComparer.Compare(source[0], source[^1]) > 0)
                        (source[0], source[^1]) = (source[^1], source[0]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[0] のキー値を pivotKey とする。

                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = source[0];
                var lowerBoundary = 1;
                var upperBoundary = source.Length - 1;
                var endOfPivotKeys = 1;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [0, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, source.Length) : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // source[lowerBoundary] に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keyComparer.Compare(source[lowerBoundary], pivotKey);
                        if (c > 0)
                        {
                            // source[lowerBoundary] > pivotKey である場合
#if DEBUG
                            Assert(keyComparer.Compare(source[lowerBoundary], pivotKey) > 0);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Assert(keyComparer.Compare(source[lowerBoundary], pivotKey) <= 0);
#endif
                        if (c == 0)
                        {
                            // source[lowerBoundary] == pivotKey である場合
#if DEBUG
                            Assert(keyComparer.Compare(source[lowerBoundary], pivotKey) == 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (endOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[endOfPivotKeys] < pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
                                (source[endOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[endOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Assert(endOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++endOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey && source[endOfPivotKeys] != pivotKey
                    Assert(lowerBoundary > upperBoundary || keyComparer.Compare(source[lowerBoundary], pivotKey) > 0 && keyComparer.Compare(source[endOfPivotKeys], pivotKey) != 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keyComparer.Compare(source[upperBoundary], pivotKey);
                        if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            if (endOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] == pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[endOfPivotKeys] < pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Assert(keyComparer.Compare(source[upperBoundary], pivotKey) == 0 && keyComparer.Compare(source[lowerBoundary], pivotKey) > 0 && keyComparer.Compare(source[endOfPivotKeys], pivotKey) < 0);
#endif
                                var t = source[endOfPivotKeys];
                                source[endOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] == pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) endOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Assert(keyComparer.Compare(source[upperBoundary], pivotKey) == 0 && keyComparer.Compare(source[lowerBoundary], pivotKey) > 0 && endOfPivotKeys == lowerBoundary);
#endif
                                (source[endOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[endOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++endOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
                            Assert(endOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より小さい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Assert(endOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [0, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, source.Length) : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - 0).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = 0;

                // 入れ替えるもう片方の開始位置 (region-b の終端位置)
                var exEndIndex = upperBoundary;

                // 入れ替える値がなくなるまで繰り返す
                while (exStartIndex < exEndIndex)
                {
                    // 値を入れ替える
                    (source[exStartIndex], source[exEndIndex]) = (source[exEndIndex], source[exStartIndex]);

                    // 入れ替える値の位置を変更する
                    ++exStartIndex;
                    --exEndIndex;
                }

                // この時点で、配列の並びは以下の通り
                // region-b) [0, upperBoundary - endOfPivotKeys] : x < pivotKey であるキー値 x を持つ要素の集合
                // region-a) [lowerBoundary - endOfPivotKeys, upperBoundary] : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) [lowerBoundary, source.Length): x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = 0; index <= 0 + upperBoundary - endOfPivotKeys; ++index)
                    Assert(keyComparer.Compare(source[index], pivotKey) < 0);
                for (var index = 0 + lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(keyComparer.Compare(source[index], pivotKey) == 0);
                for (var index = lowerBoundary; index <= source.Length - 1; ++index)
                    Assert(keyComparer.Compare(source[index], pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source[..(lowerBoundary - endOfPivotKeys)], keyComparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source[lowerBoundary..], keyComparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, 0, source.Length - 1, keyComparer);
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
        private static void InternalQuickSortManaged<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)

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
                        (source[0], source[^1]) = (source[^1], source[0]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[0] のキー値を pivotKey とする。

                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = keySelector(source[0]);
                var lowerBoundary = 1;
                var upperBoundary = source.Length - 1;
                var endOfPivotKeys = 1;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [0, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, source.Length) : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
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
                            Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // source[lowerBoundary] <= pivotKey である場合
#if DEBUG
                        Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) <= 0);
#endif
                        if (c == 0)
                        {
                            // source[lowerBoundary] == pivotKey である場合
#if DEBUG
                            Assert(keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) == 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (endOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり source[endOfPivotKeys] < pivotKey であるはずなので、source[lowerBoundary] と要素を交換する。
                                (source[endOfPivotKeys], source[lowerBoundary]) = (source[lowerBoundary], source[endOfPivotKeys]);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Assert(endOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++endOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif

                    // この時点で lowerBoundary > upperBoundary || source[lowerBoundary] > pivotKey && source[endOfPivotKeys] != pivotKey
                    Assert(lowerBoundary > upperBoundary || keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0 && keyComparer.Compare(keySelector(source[endOfPivotKeys]), pivotKey) != 0);

                    // source[upperBoundary] に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey);
                        if (c == 0)
                        {
                            // source[upperBoundary] == pivotKey である場合

                            if (endOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) source[upperBoundary] == pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) source[endOfPivotKeys] < pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Assert(keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey) == 0 && keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0 && keyComparer.Compare(keySelector(source[endOfPivotKeys]), pivotKey) < 0);
#endif
                                var t = source[endOfPivotKeys];
                                source[endOfPivotKeys] = source[upperBoundary];
                                source[upperBoundary] = source[lowerBoundary];
                                source[lowerBoundary] = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) source[upperBoundary] == pivotKey
                                // 2) source[lowerBoundary] > pivotKey (前の while ループの結果より)
                                // 3) endOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Assert(keyComparer.Compare(keySelector(source[upperBoundary]), pivotKey) == 0 && keyComparer.Compare(keySelector(source[lowerBoundary]), pivotKey) > 0 && endOfPivotKeys == lowerBoundary);
#endif
                                (source[endOfPivotKeys], source[upperBoundary]) = (source[upperBoundary], source[endOfPivotKeys]);
                            }

                            // region-a の終端位置をインクリメントする
                            ++endOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
                            Assert(endOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c < 0)
                        {
                            // source[upperBoundary] < pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より小さい (source[lowerBoundary] > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
                            (source[upperBoundary], source[lowerBoundary]) = (source[lowerBoundary], source[upperBoundary]);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Assert(endOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // source[upperBoundary] > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [0, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, source.Length) : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, 0, source.Length - 1, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - 0).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = 0;

                // 入れ替えるもう片方の開始位置 (region-b の終端位置)
                var exEndIndex = upperBoundary;

                // 入れ替える値がなくなるまで繰り返す
                while (exStartIndex < exEndIndex)
                {
                    // 値を入れ替える
                    (source[exStartIndex], source[exEndIndex]) = (source[exEndIndex], source[exStartIndex]);

                    // 入れ替える値の位置を変更する
                    ++exStartIndex;
                    --exEndIndex;
                }

                // この時点で、配列の並びは以下の通り
                // region-b) [0, upperBoundary - endOfPivotKeys] : x < pivotKey であるキー値 x を持つ要素の集合
                // region-a) [lowerBoundary - endOfPivotKeys, upperBoundary] : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) [lowerBoundary, source.Length): x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = 0; index <= 0 + upperBoundary - endOfPivotKeys; ++index)
                    Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) < 0);
                for (var index = lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) == 0);
                for (var index = lowerBoundary; index <= source.Length - 1; ++index)
                    Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source[..(lowerBoundary - endOfPivotKeys)], keySelector, keyComparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source[lowerBoundary..], keySelector, keyComparer);
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
                Assert(source[index].CompareTo(source[index + 1]) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Assert(keySelector(source[index]).CompareTo(keySelector(source[index + 1])) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, IComparer<ELEMENT_T> keyComparer)
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Assert(keyComparer.Compare(source[index], source[index + 1]) <= 0);
        }

        private static void AssertSortResult<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
            for (var index = startIndex; index < endIndex - 1; ++index)
                Assert(keyComparer.Compare(keySelector(source[index]), keySelector(source[index + 1])) <= 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 endOfPivotKeys)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            for (var index = startIndex; index < endOfPivotKeys; ++index)
                Assert(source[index].CompareTo(pivotKey) == 0);
            for (var index = endOfPivotKeys; index < lowerBoundary; ++index)
                Assert(source[index].CompareTo(pivotKey) < 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Assert(source[index].CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 endOfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
            for (var index = startIndex; index < endOfPivotKeys; ++index)
                Assert(keySelector(source[index]).CompareTo(pivotKey) == 0);
            for (var index = endOfPivotKeys; index < lowerBoundary; ++index)
                Assert(keySelector(source[index]).CompareTo(pivotKey) < 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Assert(keySelector(source[index]).CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 endOfPivotKeys, IComparer<ELEMENT_T> comparer)
        {
            for (var index = startIndex; index < endOfPivotKeys; ++index)
                Assert(comparer.Compare(source[index], pivotKey) == 0);
            for (var index = endOfPivotKeys; index < lowerBoundary; ++index)
                Assert(comparer.Compare(source[index], pivotKey) < 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Assert(comparer.Compare(source[index], pivotKey) > 0);
        }
        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 endOfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
            for (var index = startIndex; index < endOfPivotKeys; ++index)
                Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) == 0);
            for (var index = endOfPivotKeys; index < lowerBoundary; ++index)
                Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) < 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 endOfPivotKeys)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            for (var index = startIndex; index < endOfPivotKeys; ++index)
                Assert(source[index].CompareTo(pivotKey) == 0);
            for (var index = endOfPivotKeys; index < lowerBoundary; ++index)
                Assert(source[index].CompareTo(pivotKey) < 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Assert(source[index].CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 endOfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
            for (var index = startIndex; index < endOfPivotKeys; ++index)
                Assert(keySelector(source[index]).CompareTo(pivotKey) == 0);
            for (var index = endOfPivotKeys; index < lowerBoundary; ++index)
                Assert(keySelector(source[index]).CompareTo(pivotKey) < 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Assert(keySelector(source[index]).CompareTo(pivotKey) > 0);
        }

        private static void AssertQuickSortState<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, ELEMENT_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 endOfPivotKeys, IComparer<ELEMENT_T> comparer)
        {
            for (var index = startIndex; index < endOfPivotKeys; ++index)
                Assert(comparer.Compare(source[index], pivotKey) == 0);
            for (var index = endOfPivotKeys; index < lowerBoundary; ++index)
                Assert(comparer.Compare(source[index], pivotKey) < 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Assert(comparer.Compare(source[index], pivotKey) > 0);
        }
        private static void AssertQuickSortState<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, KEY_T pivotKey, Int32 lowerBoundary, Int32 upperBoundary, Int32 endOfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
            for (var index = startIndex; index < endOfPivotKeys; ++index)
                Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) == 0);
            for (var index = endOfPivotKeys; index < lowerBoundary; ++index)
                Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) < 0);
            for (var index = upperBoundary + 1; index <= endIndex; ++index)
                Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) > 0);
        }

#endif

        #endregion

        #region InternalQuickSortUnmanaged

        private static unsafe void InternalQuickSortUnmanaged<ELEMENT_T>(ref ELEMENT_T source, Int32 count)
            where ELEMENT_T : unmanaged, IComparable<ELEMENT_T>
        {
            // count が 1 以下の場合はソート不要。
            // 特に、source に渡されるはずの配列のサイズが 0 の場合、source は null 参照となるので、このチェックは必要。
            if (count <= 1)
                return;

            fixed (ELEMENT_T* startPointer = &source)
            {
                InternalQuickSortUnmanaged(startPointer, startPointer + count - 1);
            }
        }

        private static unsafe void InternalQuickSortUnmanaged<ELEMENT_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer)
            where ELEMENT_T : unmanaged, IComparable<ELEMENT_T>
        {
#if DEBUG
#if DEBUG_QUICKSORT
            System.Diagnostics.Debug.WriteLine($"Enter QuickSort(0x{(UInt64)startPointer:x16}, 0x{(UInt64)endPointer:x16}) {endPointer - startPointer + 1} bytes.");
            System.Diagnostics.Debug.Indent();
#endif

            try
            {
#endif
                if (endPointer <= startPointer)
                    return;
                if (endPointer - startPointer == 1)
                {
                    if (startPointer->CompareTo(*endPointer) > 0)
                        (*startPointer, *endPointer) = (*endPointer, *startPointer);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、*startPointer のキー値を pivotKey とする。

                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = *startPointer;
                var lowerBoundary = startPointer + 1;
                var upperBoundary = endPointer;
                var endOfPivotKeys = startPointer + 1;

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startPointer, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                while (lowerBoundary <= upperBoundary)
                {
                    // *lowerBoundary に pivotKey より大きいキーが見つかるまで lowerBoundary を増やし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = lowerBoundary->CompareTo(pivotKey);
                        if (c > 0)
                        {
                            // *lowerBoundary > pivotKey である場合
#if DEBUG
                            Assert(lowerBoundary->CompareTo(pivotKey) > 0);
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // *lowerBoundary <= pivotKey である場合
#if DEBUG
                        Assert(lowerBoundary->CompareTo(pivotKey) <= 0);
#endif
                        if (c == 0)
                        {
                            // *lowerBoundary == pivotKey である場合
#if DEBUG
                            Assert(lowerBoundary->CompareTo(pivotKey) == 0);
#endif
                            // region-a に lowerBoundary にある要素を追加する
                            if (endOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // region-b は空ではない、つまり *endOfPivotKeys < pivotKey であるはずなので、*lowerBoundary と要素を交換する。
                                (*endOfPivotKeys, *lowerBoundary) = (*lowerBoundary, *endOfPivotKeys);
                            }
                            else
                            {
                                // region-b が空である場合

                                // endOfPivotKeys == lowerBoundary であるはずなので、要素の交換は不要。
#if DEBUG
                                Assert(endOfPivotKeys == lowerBoundary);
#endif
                            }

                            // region-a の終端位置をインクリメントする
                            ++endOfPivotKeys;
                        }

                        // region-b の終端位置をインクリメントする
                        ++lowerBoundary;
#if DEBUG
                        AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif

                    // この時点で lowerBoundary > upperBoundary || *lowerBoundary > pivotKey && *endOfPivotKeys != pivotKey
                    Assert(lowerBoundary > upperBoundary || lowerBoundary->CompareTo(pivotKey) > 0 && endOfPivotKeys->CompareTo(pivotKey) != 0);

                    // *upperBoundary に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = upperBoundary->CompareTo(pivotKey);
                        if (c == 0)
                        {
                            // *upperBoundary == pivotKey である場合

                            if (endOfPivotKeys < lowerBoundary)
                            {
                                // region-b が空ではない場合

                                // 以下の 3 つの事実が判明しているので、3 つの要素をそれぞれ入れ替える。
                                // 1) *upperBoundary == pivotKey
                                // 2) *lowerBoundary > pivotKey (前の while ループの結果より)
                                // 3) *endOfPivotKeys < pivotKey (regon-b が空ではないことより)
#if DEBUG
                                Assert(upperBoundary->CompareTo(pivotKey) == 0 && lowerBoundary->CompareTo(pivotKey) > 0 && endOfPivotKeys->CompareTo(pivotKey) < 0);
#endif
                                var t = *endOfPivotKeys;
                                *endOfPivotKeys = *upperBoundary;
                                *upperBoundary = *lowerBoundary;
                                *lowerBoundary = t;
                            }
                            else
                            {
                                // region-b が空である場合

                                // 以下の 3 つの事実が判明しているので、2 つの要素を入れ替える。
                                // 1) *upperBoundary == pivotKey
                                // 2) *lowerBoundary > pivotKey (前の while ループの結果より)
                                // 3) endOfPivotKeys == lowerBoundary (regon-b が空ではあることより)
#if DEBUG
                                Assert(upperBoundary->CompareTo(pivotKey) == 0 && lowerBoundary->CompareTo(pivotKey) > 0 && endOfPivotKeys == lowerBoundary);
#endif
                                (*endOfPivotKeys, *upperBoundary) = (*upperBoundary, *endOfPivotKeys);
                            }

                            // region-a の終端位置をインクリメントする
                            ++endOfPivotKeys;

                            // region -b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
                            Assert(endOfPivotKeys <= lowerBoundary);
#endif
                            // pivotKey と等しいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else if (c < 0)
                        {
                            // *upperBoundary < pivotKey である場合

                            // 前の while ループの結果より、region-b の末尾の要素のキー値が pivotKey より小さい (*lowerBoundary > pivotKey) ことが判明しているので、
                            // region-b の終端と要素を入れ替える
                            (*upperBoundary, *lowerBoundary) = (*lowerBoundary, *upperBoundary);

                            // region-b の終端位置をインクリメントする
                            ++lowerBoundary;

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            Assert(endOfPivotKeys <= lowerBoundary);
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                            // pivotKey より小さいキー値を持つ要素が見つかったので、ループを終える。
                            break;
                        }
                        else
                        {
                            // *upperBoundary > pivotKey である場合

                            // region-c の先端位置をデクリメントする
                            --upperBoundary;
#if DEBUG
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                }

                // この時点で region-w のサイズは 0 であり、lowerBoundary == upperBoundary + 1 のはずである。
#if DEBUG
                Assert(lowerBoundary == upperBoundary + 1);
#endif

                // この時点での配列のレイアウトは以下の通り。
                //
                // region-a) [startPointer, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - startPointer).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = startPointer;

                // 入れ替えるもう片方の開始位置 (region-b の終端位置)
                var exEndIndex = upperBoundary;

                // 入れ替える値がなくなるまで繰り返す
                while (exStartIndex < exEndIndex)
                {
                    // 値を入れ替える
                    (*exStartIndex, *exEndIndex) = (*exEndIndex, *exStartIndex);

                    // 入れ替える値の位置を変更する
                    ++exStartIndex;
                    --exEndIndex;
                }

                // この時点で、配列の並びは以下の通り
                // region-b) [startPointer, startPointer + upperBoundary - endOfPivotKeys] : x < pivotKey であるキー値 x を持つ要素の集合
                // region-a) [startPointer + lowerBoundary - endOfPivotKeys, upperBoundary] : x == pivotKey であるキー値 x を持つ要素の集合
                // region-c) [lowerBoundary, endIndex]: x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var p = startPointer; p <= startPointer + (upperBoundary - endOfPivotKeys); ++p)
                    Assert(p->CompareTo(pivotKey) < 0);
                for (var p = startPointer + (lowerBoundary - endOfPivotKeys); p <= upperBoundary; ++p)
                    Assert(p->CompareTo(pivotKey) == 0);
                for (var p = lowerBoundary; p <= endPointer; ++p)
                    Assert(p->CompareTo(pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortUnmanaged(startPointer, upperBoundary - endOfPivotKeys + startPointer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortUnmanaged(lowerBoundary, endPointer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(startPointer, endPointer);
#if DEBUG_QUICKSORT
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort(0x{(UInt64)startPointer:x16}, 0x{(UInt64)endPointer:x16}) {endPointer - startPointer + 1} bytes.");
#endif
            }
#endif
        }

#if DEBUG
        private static unsafe void AssertSortResult<ELEMENT_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer)
            where ELEMENT_T : unmanaged, IComparable<ELEMENT_T>
        {
            for (var p = startPointer; p < endPointer; ++p)
                Assert(p->CompareTo(p[1]) <= 0);
        }

        private static unsafe void AssertSortResult<ELEMENT_T, KEY_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer, Func<ELEMENT_T, KEY_T> keySelector)
            where ELEMENT_T : unmanaged
            where KEY_T : IComparable<KEY_T>
        {
            for (var p = startPointer; p < endPointer; ++p)
                Assert(keySelector(*p).CompareTo(keySelector(p[1])) <= 0);
        }

        private static unsafe void AssertSortResult<ELEMENT_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer, IComparer<ELEMENT_T> keyComparer)
            where ELEMENT_T : unmanaged
        {
            for (var p = startPointer; p < endPointer; ++p)
                Assert(keyComparer.Compare(*p, p[1]) <= 0);
        }

        private static unsafe void AssertSortResult<ELEMENT_T, KEY_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
            where ELEMENT_T : unmanaged
        {
            for (var p = startPointer; p < endPointer - 1; ++p)
                Assert(keyComparer.Compare(keySelector(*p), keySelector(p[1])) <= 0);
        }

        private static unsafe void AssertQuickSortState<ELEMENT_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer, ELEMENT_T pivotKey, ELEMENT_T* lowerBoundary, ELEMENT_T* upperBoundary, ELEMENT_T* endOfPivotKeys)
            where ELEMENT_T : unmanaged, IComparable<ELEMENT_T>
        {
            for (var p = startPointer; p < endOfPivotKeys; ++p)
                Assert(p->CompareTo(pivotKey) == 0);
            for (var p = endOfPivotKeys; p < lowerBoundary; ++p)
                Assert(p->CompareTo(pivotKey) < 0);
            for (var p = upperBoundary + 1; p <= endPointer; ++p)
                Assert(p->CompareTo(pivotKey) > 0);
        }

        private static unsafe void AssertQuickSortState<ELEMENT_T, KEY_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer, KEY_T pivotKey, ELEMENT_T* lowerBoundary, ELEMENT_T* upperBoundary, ELEMENT_T* endOfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector)
            where ELEMENT_T : unmanaged
            where KEY_T : IComparable<KEY_T>
        {
            for (var p = startPointer; p < endOfPivotKeys; ++p)
                Assert(keySelector(*p).CompareTo(pivotKey) == 0);
            for (var p = endOfPivotKeys; p < lowerBoundary; ++p)
                Assert(keySelector(*p).CompareTo(pivotKey) < 0);
            for (var p = upperBoundary + 1; p <= endPointer; ++p)
                Assert(keySelector(*p).CompareTo(pivotKey) > 0);
        }

        private static unsafe void AssertQuickSortState<ELEMENT_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer, ELEMENT_T pivotKey, ELEMENT_T* lowerBoundary, ELEMENT_T* upperBoundary, ELEMENT_T* endOfPivotKeys, IComparer<ELEMENT_T> comparer)
            where ELEMENT_T : unmanaged
        {
            for (var p = startPointer; p < endOfPivotKeys; ++p)
                Assert(comparer.Compare(*p, pivotKey) == 0);
            for (var p = endOfPivotKeys; p < lowerBoundary; ++p)
                Assert(comparer.Compare(*p, pivotKey) < 0);
            for (var p = upperBoundary + 1; p <= endPointer; ++p)
                Assert(comparer.Compare(*p, pivotKey) > 0);
        }
        private static unsafe void AssertQuickSortState<ELEMENT_T, KEY_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer, KEY_T pivotKey, ELEMENT_T* lowerBoundary, ELEMENT_T* upperBoundary, ELEMENT_T* endOfPivotKeys, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
            where ELEMENT_T : unmanaged
        {
            for (var p = startPointer; p < endOfPivotKeys; ++p)
                Assert(keyComparer.Compare(keySelector(*p), pivotKey) == 0);
            for (var p = endOfPivotKeys; p < lowerBoundary; ++p)
                Assert(keyComparer.Compare(keySelector(*p), pivotKey) < 0);
            for (var p = upperBoundary + 1; p <= endPointer; ++p)
                Assert(keyComparer.Compare(keySelector(*p), pivotKey) > 0);
        }
#endif

        #endregion
    }
}
