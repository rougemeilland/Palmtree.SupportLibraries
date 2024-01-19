using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmtree
{
    partial class ArrayExtensions
    {
        #region InternalQuickSortManaged

        ///<summary>
        /// A quicksort method that allows duplicate keys.
        ///</summary>
        /// <remarks>
        /// See also <seealso href="https://kankinkon.hatenadiary.org/entry/20120202/1328133196">kanmo's blog</seealso>. 
        /// </remarks>
        private static void InternalQuickSortManaged<ELEMENT_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
#if DEBUG
#if false
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
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[startIndex] のキー値を pivotKey とする。
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = source[startIndex];
                var lowerBoundary = startIndex + 1;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex + 1;
#else
                var pivotKey = SelectPivotKey(keySelector(source[startIndex]), keySelector(source[endIndex]), keySelector(source[(startIndex + endIndex) / 2]), keyComparer);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex;
#endif

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
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
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
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
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - startIndex).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = startIndex;

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
                // region-c) [lowerBoundary, endIndex]: x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = startIndex; index <= startIndex + upperBoundary - endOfPivotKeys; ++index)
                    Assert(source[index].CompareTo(pivotKey) < 0);
                for (var index = startIndex + lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(source[index].CompareTo(pivotKey) == 0);
                for (var index = lowerBoundary; index <= endIndex; ++index)
                    Assert(source[index].CompareTo(pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, startIndex, upperBoundary - endOfPivotKeys + startIndex);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, lowerBoundary, endIndex);
#if DEBUG
            }
            finally
            {
                AssertSortResult<ELEMENT_T>(source, startIndex, endIndex);
#if false
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
        private static void InternalQuickSortManaged<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
#if DEBUG
#if false
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
                    if (keySelector(source[startIndex]).CompareTo(keySelector(source[endIndex])) > 0)
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[startIndex] のキー値を pivotKey とする。
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = keySelector(source[startIndex]);
                var lowerBoundary = startIndex + 1;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex + 1;
#else
                var pivotKey = SelectPivotKey(keySelector(source[startIndex]), keySelector(source[endIndex]), keySelector(source[(startIndex + endIndex) / 2]), keyComparer);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex;
#endif

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - startIndex).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = startIndex;

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
                // region-c) [lowerBoundary, endIndex]: x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = startIndex; index <= startIndex + upperBoundary - endOfPivotKeys; ++index)
                    Assert(keySelector(source[index]).CompareTo(pivotKey) < 0);
                for (var index = startIndex + lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(keySelector(source[index]).CompareTo(pivotKey) == 0);
                for (var index = lowerBoundary; index <= endIndex; ++index)
                    Assert(keySelector(source[index]).CompareTo(pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, startIndex, upperBoundary - endOfPivotKeys + startIndex, keySelector);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, lowerBoundary, endIndex, keySelector);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, keySelector);
#if false
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
        private static void InternalQuickSortManaged<ELEMENT_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex, IComparer<ELEMENT_T> keyComparer)
        {
#if DEBUG
#if false
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
                    if (keyComparer.Compare(source[startIndex], source[endIndex]) > 0)
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[startIndex] のキー値を pivotKey とする。
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = source[startIndex];
                var lowerBoundary = startIndex + 1;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex + 1;
#else
                var pivotKey = SelectPivotKey(keySelector(source[startIndex]), keySelector(source[endIndex]), keySelector(source[(startIndex + endIndex) / 2]), keyComparer);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex;
#endif

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
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
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
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
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - startIndex).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = startIndex;

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
                // region-c) [lowerBoundary, endIndex]: x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = startIndex; index <= startIndex + upperBoundary - endOfPivotKeys; ++index)
                    Assert(keyComparer.Compare(source[index], pivotKey) < 0);
                for (var index = startIndex + lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(keyComparer.Compare(source[index], pivotKey) == 0);
                for (var index = lowerBoundary; index <= endIndex; ++index)
                    Assert(keyComparer.Compare(source[index], pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, startIndex, upperBoundary - endOfPivotKeys + startIndex, keyComparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, lowerBoundary, endIndex, keyComparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, keyComparer);
#if false
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
        private static void InternalQuickSortManaged<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)

        {
#if DEBUG
#if false
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
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[startIndex] のキー値を pivotKey とする。
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = keySelector(source[startIndex]);
                var lowerBoundary = startIndex + 1;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex + 1;
#else
                var pivotKey = SelectPivotKey(keySelector(source[startIndex]), keySelector(source[endIndex]), keySelector(source[(startIndex + endIndex) / 2]), keyComparer);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex;
#endif

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - startIndex).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = startIndex;

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
                // region-c) [lowerBoundary, endIndex]: x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = startIndex; index <= startIndex + upperBoundary - endOfPivotKeys; ++index)
                    Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) < 0);
                for (var index = startIndex + lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) == 0);
                for (var index = lowerBoundary; index <= endIndex; ++index)
                    Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, startIndex, upperBoundary - endOfPivotKeys + startIndex, keySelector, keyComparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, lowerBoundary, endIndex, keySelector, keyComparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, keySelector, keyComparer);
#if false
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
        private static void InternalQuickSortManaged<ELEMENT_T>(Span<ELEMENT_T> source, Int32 startIndex, Int32 endIndex)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
#if DEBUG
#if false
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
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[startIndex] のキー値を pivotKey とする。
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = source[startIndex];
                var lowerBoundary = startIndex + 1;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex + 1;
#else
                var pivotKey = SelectPivotKey(keySelector(source[startIndex]), keySelector(source[endIndex]), keySelector(source[(startIndex + endIndex) / 2]), keyComparer);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex;
#endif

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
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
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
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
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - startIndex).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = startIndex;

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
                // region-c) [lowerBoundary, endIndex]: x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = startIndex; index <= startIndex + upperBoundary - endOfPivotKeys; ++index)
                    Assert(source[index].CompareTo(pivotKey) < 0);
                for (var index = startIndex + lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(source[index].CompareTo(pivotKey) == 0);
                for (var index = lowerBoundary; index <= endIndex; ++index)
                    Assert(source[index].CompareTo(pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, startIndex, upperBoundary - endOfPivotKeys + startIndex);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, lowerBoundary, endIndex);
#if DEBUG
            }
            finally
            {
                AssertSortResult<ELEMENT_T>(source, startIndex, endIndex);
#if false
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
        private static void InternalQuickSortManaged<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
#if DEBUG
#if false
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
                    if (keySelector(source[startIndex]).CompareTo(keySelector(source[endIndex])) > 0)
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[startIndex] のキー値を pivotKey とする。
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = keySelector(source[startIndex]);
                var lowerBoundary = startIndex + 1;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex + 1;
#else
                var pivotKey = SelectPivotKey(keySelector(source[startIndex]), keySelector(source[endIndex]), keySelector(source[(startIndex + endIndex) / 2]), keyComparer);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex;
#endif

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - startIndex).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = startIndex;

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
                // region-c) [lowerBoundary, endIndex]: x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = startIndex; index <= startIndex + upperBoundary - endOfPivotKeys; ++index)
                    Assert(keySelector(source[index]).CompareTo(pivotKey) < 0);
                for (var index = startIndex + lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(keySelector(source[index]).CompareTo(pivotKey) == 0);
                for (var index = lowerBoundary; index <= endIndex; ++index)
                    Assert(keySelector(source[index]).CompareTo(pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, startIndex, upperBoundary - endOfPivotKeys + startIndex, keySelector);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, lowerBoundary, endIndex, keySelector);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, keySelector);
#if false
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
        private static void InternalQuickSortManaged<ELEMENT_T>(Span<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, IComparer<ELEMENT_T> keyComparer)
        {
#if DEBUG
#if false
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
                    if (keyComparer.Compare(source[startIndex], source[endIndex]) > 0)
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[startIndex] のキー値を pivotKey とする。
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = source[startIndex];
                var lowerBoundary = startIndex + 1;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex + 1;
#else
                var pivotKey = SelectPivotKey(keySelector(source[startIndex]), keySelector(source[endIndex]), keySelector(source[(startIndex + endIndex) / 2]), keyComparer);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex;
#endif

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
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
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
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
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keyComparer);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - startIndex).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = startIndex;

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
                // region-c) [lowerBoundary, endIndex]: x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = startIndex; index <= startIndex + upperBoundary - endOfPivotKeys; ++index)
                    Assert(keyComparer.Compare(source[index], pivotKey) < 0);
                for (var index = startIndex + lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(keyComparer.Compare(source[index], pivotKey) == 0);
                for (var index = lowerBoundary; index <= endIndex; ++index)
                    Assert(keyComparer.Compare(source[index], pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, startIndex, upperBoundary - endOfPivotKeys + startIndex, keyComparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, lowerBoundary, endIndex, keyComparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, keyComparer);
#if false
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
        private static void InternalQuickSortManaged<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Int32 startIndex, Int32 endIndex, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)

        {
#if DEBUG
#if false
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
                        (source[startIndex], source[endIndex]) = (source[endIndex], source[startIndex]);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、source[startIndex] のキー値を pivotKey とする。
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = keySelector(source[startIndex]);
                var lowerBoundary = startIndex + 1;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex + 1;
#else
                var pivotKey = SelectPivotKey(keySelector(source[startIndex]), keySelector(source[endIndex]), keySelector(source[(startIndex + endIndex) / 2]), keyComparer);
                var lowerBoundary = startIndex;
                var upperBoundary = endIndex;
                var endOfPivotKeys = startIndex;
#endif

                // この時点での配列のレイアウトは以下の通り
                // region-w を如何に縮小するかがこのループの目的である
                //
                // region-a) [startIndex, endOfPivotKeys) : x == pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 1)
                // region-b) [endOfPivotKeys, lowerBoundary) : x < pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
                // region-w) [lowerBoundary, upperBoundary] : pivotKey との大小関係が不明なキー値を持つ要素の集合
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合 (初期の長さは 0)
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                        AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                            AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                // region-c) (upperBoundary, endIndex] : x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                AssertQuickSortState(source, startIndex, endIndex, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif

                // 配列を [region-b] [region-a] [region-c] の順に並び替えるために、region-b の終端の一部または全部を region-a と入れ替える。

                // 入れ替える長さを求める (region-a の長さと region-b の長さの最小値)
                var lengthToExchange = (endOfPivotKeys - startIndex).Minimum(lowerBoundary - endOfPivotKeys);

                // 入れ替える片方の開始位置 (region-a の先端位置)
                var exStartIndex = startIndex;

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
                // region-c) [lowerBoundary, endIndex]: x > pivotKey であるキー値 x を持つ要素の集合
                // ※ただし lowerBoundary == upperBoundary + 1

#if DEBUG
                for (var index = startIndex; index <= startIndex + upperBoundary - endOfPivotKeys; ++index)
                    Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) < 0);
                for (var index = startIndex + lowerBoundary - endOfPivotKeys; index <= upperBoundary; ++index)
                    Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) == 0);
                for (var index = lowerBoundary; index <= endIndex; ++index)
                    Assert(keyComparer.Compare(keySelector(source[index]), pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, startIndex, upperBoundary - endOfPivotKeys + startIndex, keySelector, keyComparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortManaged(source, lowerBoundary, endIndex, keySelector, keyComparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(source, startIndex, endIndex, keySelector, keyComparer);
#if false
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort({startIndex}, {endIndex}) {endIndex - startIndex + 1} bytes");
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

        private static unsafe void InternalQuickSortUnmanaged<ELEMENT_T, KEY_T>(ref ELEMENT_T source, Int32 count, Func<ELEMENT_T, KEY_T> keySelector)
            where ELEMENT_T : unmanaged
            where KEY_T : IComparable<KEY_T>
        {
            // count が 1 以下の場合はソート不要。
            // 特に、source に渡されるはずの配列のサイズが 0 の場合、source は null 参照となるので、このチェックは必要。
            if (count <= 1)
                return;

            fixed (ELEMENT_T* startPointer = &source)
            {
                InternalQuickSortUnmanaged(startPointer, startPointer + count - 1, keySelector);
            }
        }

        private static unsafe void InternalQuickSortUnmanaged<ELEMENT_T>(ref ELEMENT_T source, Int32 count, IComparer<ELEMENT_T> comparer)
            where ELEMENT_T : unmanaged
        {
            // count が 1 以下の場合はソート不要。
            // 特に、source に渡されるはずの配列のサイズが 0 の場合、source は null 参照となるので、このチェックは必要。
            if (count <= 1)
                return;

            fixed (ELEMENT_T* startPointer = &source)
            {
                InternalQuickSortUnmanaged(startPointer, startPointer + count - 1, comparer);
            }
        }

        private static unsafe void InternalQuickSortUnmanaged<ELEMENT_T, KEY_T>(ref ELEMENT_T source, Int32 count, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
            where ELEMENT_T : unmanaged
        {
            // count が 1 以下の場合はソート不要。
            // 特に、source に渡されるはずの配列のサイズが 0 の場合、source は null 参照となるので、このチェックは必要。
            if (count <= 1)
                return;

            fixed (ELEMENT_T* startPointer = &source)
            {
                InternalQuickSortUnmanaged(startPointer, startPointer + count - 1, keySelector, keyComparer);
            }
        }

        private static unsafe void InternalQuickSortUnmanaged<ELEMENT_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer)
            where ELEMENT_T : unmanaged, IComparable<ELEMENT_T>
        {
#if DEBUG
#if false
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
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = *startPointer;
                var lowerBoundary = startPointer + 1;
                var upperBoundary = endPointer;
                var endOfPivotKeys = startPointer + 1;
#else
                var pivotKey = SelectPivotKey(*startPointer, *endPointer, startPointer[(endPointer - startPointer + 1) / 2], keyComparer);
                var lowerBoundary = startPointer;
                var upperBoundary = endPointer;
                var endOfPivotKeys = startPointer;
#endif

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
#if false
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort(0x{(UInt64)startPointer:x16}, 0x{(UInt64)endPointer:x16}) {endPointer - startPointer + 1} bytes.");
#endif
            }
#endif
        }

        private static unsafe void InternalQuickSortUnmanaged<ELEMENT_T, KEY_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer, Func<ELEMENT_T, KEY_T> keySelector)
            where ELEMENT_T : unmanaged
            where KEY_T : IComparable<KEY_T>
        {
#if DEBUG
#if false
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
                    if (keySelector(*startPointer).CompareTo(keySelector(*endPointer)) > 0)
                        (*startPointer, *endPointer) = (*endPointer, *startPointer);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、*startPointer のキー値を pivotKey とする。
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = keySelector(*startPointer);
                var lowerBoundary = startPointer + 1;
                var upperBoundary = endPointer;
                var endOfPivotKeys = startPointer + 1;
#else
                var pivotKey = SelectPivotKey(keySelector(*startPointer), keySelector(*endPointer), keySelector(startPointer[(endPointer - startPointer + 1) / 2]), keyComparer);
                var lowerBoundary = startPointer;
                var upperBoundary = endPointer;
                var endOfPivotKeys = startPointer;
#endif

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
                        var c = keySelector(*lowerBoundary).CompareTo(pivotKey);
                        if (c > 0)
                        {
                            // *lowerBoundary > pivotKey である場合
#if DEBUG
                            Assert(keySelector(*lowerBoundary).CompareTo(pivotKey) > 0);
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // *lowerBoundary <= pivotKey である場合
#if DEBUG
                        Assert(keySelector(*lowerBoundary).CompareTo(pivotKey) <= 0);
#endif
                        if (c == 0)
                        {
                            // *lowerBoundary == pivotKey である場合
#if DEBUG
                            Assert(keySelector(*lowerBoundary).CompareTo(pivotKey) == 0);
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
                        AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif

                    // この時点で lowerBoundary > upperBoundary || *lowerBoundary > pivotKey && *endOfPivotKeys != pivotKey
                    Assert(lowerBoundary > upperBoundary || keySelector(*lowerBoundary).CompareTo(pivotKey) > 0 && keySelector(*endOfPivotKeys).CompareTo(pivotKey) != 0);

                    // *upperBoundary に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keySelector(*upperBoundary).CompareTo(pivotKey);
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
                                Assert(keySelector(*upperBoundary).CompareTo(pivotKey) == 0 && keySelector(*lowerBoundary).CompareTo(pivotKey) > 0 && keySelector(*endOfPivotKeys).CompareTo(pivotKey) < 0);
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
                                Assert(keySelector(*upperBoundary).CompareTo(pivotKey) == 0 && keySelector(*lowerBoundary).CompareTo(pivotKey) > 0 && endOfPivotKeys == lowerBoundary);
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
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector);
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
                    Assert(keySelector(*p).CompareTo(pivotKey) < 0);
                for (var p = startPointer + (lowerBoundary - endOfPivotKeys); p <= upperBoundary; ++p)
                    Assert(keySelector(*p).CompareTo(pivotKey) == 0);
                for (var p = lowerBoundary; p <= endPointer; ++p)
                    Assert(keySelector(*p).CompareTo(pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortUnmanaged(startPointer, upperBoundary - endOfPivotKeys + startPointer, keySelector);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortUnmanaged(lowerBoundary, endPointer, keySelector);
#if DEBUG
            }
            finally
            {
                AssertSortResult(startPointer, endPointer, keySelector);
#if false
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort(0x{(UInt64)startPointer:x16}, 0x{(UInt64)endPointer:x16}) {endPointer - startPointer + 1} bytes.");
#endif
            }
#endif
        }

        private static unsafe void InternalQuickSortUnmanaged<ELEMENT_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer, IComparer<ELEMENT_T> comparer)
            where ELEMENT_T : unmanaged
        {
#if DEBUG
#if false
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
                    if (comparer.Compare(*startPointer, *endPointer) > 0)
                        (*startPointer, *endPointer) = (*endPointer, *startPointer);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、*startPointer のキー値を pivotKey とする。
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = *startPointer;
                var lowerBoundary = startPointer + 1;
                var upperBoundary = endPointer;
                var endOfPivotKeys = startPointer + 1;
#else
                var pivotKey = SelectPivotKey(*startPointer, *endPointer, startPointer[(endPointer - startPointer + 1) / 2], keyComparer);
                var lowerBoundary = startPointer;
                var upperBoundary = endPointer;
                var endOfPivotKeys = startPointer;
#endif

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
                        var c = comparer.Compare(*lowerBoundary, pivotKey);
                        if (c > 0)
                        {
                            // *lowerBoundary > pivotKey である場合
#if DEBUG
                            Assert(comparer.Compare(*lowerBoundary, pivotKey) > 0);
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, comparer);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // *lowerBoundary <= pivotKey である場合
#if DEBUG
                        Assert(comparer.Compare(*lowerBoundary, pivotKey) <= 0);
#endif
                        if (c == 0)
                        {
                            // *lowerBoundary == pivotKey である場合
#if DEBUG
                            Assert(comparer.Compare(*lowerBoundary, pivotKey) == 0);
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
                        AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, comparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, comparer);
#endif

                    // この時点で lowerBoundary > upperBoundary || *lowerBoundary > pivotKey && *endOfPivotKeys != pivotKey
                    Assert(lowerBoundary > upperBoundary || comparer.Compare(*lowerBoundary, pivotKey) > 0 && comparer.Compare(*endOfPivotKeys, pivotKey) != 0);

                    // *upperBoundary に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = comparer.Compare(*upperBoundary, pivotKey);
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
                                Assert(comparer.Compare(*upperBoundary, pivotKey) == 0 && comparer.Compare(*lowerBoundary, pivotKey) > 0 && comparer.Compare(*endOfPivotKeys, pivotKey) < 0);
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
                                Assert(comparer.Compare(*upperBoundary, pivotKey) == 0 && comparer.Compare(*lowerBoundary, pivotKey) > 0 && endOfPivotKeys == lowerBoundary);
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
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, comparer);
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
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, comparer);
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
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, comparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, comparer);
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
                AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, comparer);
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
                    Assert(comparer.Compare(*p, pivotKey) < 0);
                for (var p = startPointer + (lowerBoundary - endOfPivotKeys); p <= upperBoundary; ++p)
                    Assert(comparer.Compare(*p, pivotKey) == 0);
                for (var p = lowerBoundary; p <= endPointer; ++p)
                    Assert(comparer.Compare(*p, pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortUnmanaged(startPointer, upperBoundary - endOfPivotKeys + startPointer, comparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortUnmanaged(lowerBoundary, endPointer, comparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(startPointer, endPointer, comparer);
#if false
                System.Diagnostics.Debug.Unindent();
                System.Diagnostics.Debug.WriteLine($"Leave QuickSort(0x{(UInt64)startPointer:x16}, 0x{(UInt64)endPointer:x16}) {endPointer - startPointer + 1} bytes.");
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
        private static unsafe void InternalQuickSortUnmanaged<ELEMENT_T, KEY_T>(ELEMENT_T* startPointer, ELEMENT_T* endPointer, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
            where ELEMENT_T : unmanaged
        {
#if DEBUG
#if false
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
                    if (keyComparer.Compare(keySelector(*startPointer), keySelector(*endPointer)) > 0)
                        (*startPointer, *endPointer) = (*endPointer, *startPointer);
                    return;
                }

                // もしキー値が重複していないと仮定すれば、 3 点のキー値の中間値を pivotKey として採用することによりよりよい分割が望めるが、
                // この QuickSort メソッドでは重複キーを許容するので、*startPointer のキー値を pivotKey とする。
#if true
                // 配列の最初の要素のキー値が pivotKey なので、後述の配列レイアウトに従って、lowerBoundary および endOfPivotKeys を +1 しておく。
                var pivotKey = keySelector(*startPointer);
                var lowerBoundary = startPointer + 1;
                var upperBoundary = endPointer;
                var endOfPivotKeys = startPointer + 1;
#else
                var pivotKey = SelectPivotKey(keySelector(*startPointer), keySelector(*endPointer), keySelector(startPointer[(endPointer - startPointer + 1) / 2]), keyComparer);
                var lowerBoundary = startPointer;
                var upperBoundary = endPointer;
                var endOfPivotKeys = startPointer;
#endif

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
                        var c = keyComparer.Compare(keySelector(*lowerBoundary), pivotKey);
                        if (c > 0)
                        {
                            // *lowerBoundary > pivotKey である場合
#if DEBUG
                            Assert(keyComparer.Compare(keySelector(*lowerBoundary), pivotKey) > 0);
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                            // pivotKey より大きいキー値を持つ要素が見つかったので、ループを終える
                            break;
                        }

                        // *lowerBoundary <= pivotKey である場合
#if DEBUG
                        Assert(keyComparer.Compare(keySelector(*lowerBoundary), pivotKey) <= 0);
#endif
                        if (c == 0)
                        {
                            // *lowerBoundary == pivotKey である場合
#if DEBUG
                            Assert(keyComparer.Compare(keySelector(*lowerBoundary), pivotKey) == 0);
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
                        AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                    }

#if DEBUG
                    AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif

                    // この時点で lowerBoundary > upperBoundary || *lowerBoundary > pivotKey && *endOfPivotKeys != pivotKey
                    Assert(lowerBoundary > upperBoundary || keyComparer.Compare(keySelector(*lowerBoundary), pivotKey) > 0 && keyComparer.Compare(keySelector(*endOfPivotKeys), pivotKey) != 0);

                    // *upperBoundary に pivotKey より小さいまたは等しいキー値を持つ要素が見つかるまで upperBoundary を減らし続ける。
                    while (lowerBoundary <= upperBoundary)
                    {
                        var c = keyComparer.Compare(keySelector(*upperBoundary), pivotKey);
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
                                Assert(keyComparer.Compare(keySelector(*upperBoundary), pivotKey) == 0 && keyComparer.Compare(keySelector(*lowerBoundary), pivotKey) > 0 && keyComparer.Compare(keySelector(*endOfPivotKeys), pivotKey) < 0);
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
                                Assert(keyComparer.Compare(keySelector(*upperBoundary), pivotKey) == 0 && keyComparer.Compare(keySelector(*lowerBoundary), pivotKey) > 0 && endOfPivotKeys == lowerBoundary);
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
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                            AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
#endif
                        }
                    }
#if DEBUG
                    AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                AssertQuickSortState(startPointer, endPointer, pivotKey, lowerBoundary, upperBoundary, endOfPivotKeys, keySelector, keyComparer);
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
                    Assert(keyComparer.Compare(keySelector(*p), pivotKey) < 0);
                for (var p = startPointer + (lowerBoundary - endOfPivotKeys); p <= upperBoundary; ++p)
                    Assert(keyComparer.Compare(keySelector(*p), pivotKey) == 0);
                for (var p = lowerBoundary; p <= endPointer; ++p)
                    Assert(keyComparer.Compare(keySelector(*p), pivotKey) > 0);
#endif

                // region-b の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortUnmanaged(startPointer, upperBoundary - endOfPivotKeys + startPointer, keySelector, keyComparer);

                // region-c の内部を並び替えるために、再帰的に QuickSort を呼び出す
                InternalQuickSortUnmanaged(lowerBoundary, endPointer, keySelector, keyComparer);
#if DEBUG
            }
            finally
            {
                AssertSortResult(startPointer, endPointer, keySelector, keyComparer);
#if false
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
