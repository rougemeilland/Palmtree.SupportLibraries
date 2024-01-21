using System;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static class EnumExtensions
    {
        #region IsNoneOf

        /// <summary>
        /// 指定された値が、別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="otherValue1">
        /// 1番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue2">
        /// 2番目の比較対象の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/> の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードと等価です。
        /// <code>
        /// !<paramref name="value"/>.Equals(<paramref name="otherValue1"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue2"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<ELEMENT_T>(this ELEMENT_T value, ELEMENT_T otherValue1, ELEMENT_T otherValue2)
            where ELEMENT_T : Enum
            => value is null
                ? otherValue1 is not null && otherValue2 is not null
                : !value.Equals(otherValue1) && !value.Equals(otherValue2);

        /// <summary>
        /// 指定された値が、別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="otherValue1">
        /// 1番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue2">
        /// 2番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue3">
        /// 3番目の比較対象の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/> の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードと等価です。
        /// <code>
        /// !<paramref name="value"/>.Equals(<paramref name="otherValue1"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue2"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue3"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<ELEMENT_T>(this ELEMENT_T value, ELEMENT_T otherValue1, ELEMENT_T otherValue2, ELEMENT_T otherValue3)
            where ELEMENT_T : Enum
            => value is null
                ? otherValue1 is not null && otherValue2 is not null && otherValue3 is not null
                : !value.Equals(otherValue1) && !value.Equals(otherValue2) && !value.Equals(otherValue3);

        /// <summary>
        /// 指定された値が、別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="otherValue1">
        /// 1番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue2">
        /// 2番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue3">
        /// 3番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue4">
        /// 4番目の比較対象の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/> の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードと等価です。
        /// <code>
        /// !<paramref name="value"/>.Equals(<paramref name="otherValue1"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue2"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue3"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue4"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<ELEMENT_T>(this ELEMENT_T value, ELEMENT_T otherValue1, ELEMENT_T otherValue2, ELEMENT_T otherValue3, ELEMENT_T otherValue4)
            where ELEMENT_T
            : Enum => value is null
                ? otherValue1 is not null && otherValue2 is not null && otherValue3 is not null && otherValue4 is not null
                : !value.Equals(otherValue1) && !value.Equals(otherValue2) && !value.Equals(otherValue3) && !value.Equals(otherValue4);

        /// <summary>
        /// 指定された値が、別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="otherValues">
        /// 比較対象の値の値の配列です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="otherValues"/> の要素の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードと等価です。
        /// <code>
        /// !<paramref name="value"/>.Equals(<paramref name="otherValues"/>[0]) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValues"/>[1]) &amp;&amp; ... &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValues"/>[<paramref name="otherValues"/>.Length - 2]) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValues"/>[<paramref name="otherValues"/>.Length - 1])
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<ELEMENT_T>(this ELEMENT_T value, params ELEMENT_T[] otherValues)
            where ELEMENT_T : Enum
            => !value.IsAnyOf(otherValues);

        #endregion

        #region IsAnyOf

        /// <summary>
        /// 指定された値が、別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="otherValue1">
        /// 1番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue2">
        /// 2番目の比較対象の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/> の何れかと等しい場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードと等価です。
        /// <code>
        /// <paramref name="value"/>.Equals(<paramref name="otherValue1"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue2"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<ELEMENT_T>(this ELEMENT_T value, ELEMENT_T otherValue1, ELEMENT_T otherValue2)
            where ELEMENT_T : Enum
            => value is null
                ? otherValue1 is null || otherValue2 is null : value.Equals(otherValue1) || value.Equals(otherValue2);

        /// <summary>
        /// 指定された値が、別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="otherValue1">
        /// 1番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue2">
        /// 2番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue3">
        /// 3番目の比較対象の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/> の何れかと等しい場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードと等価です。
        /// <code>
        /// <paramref name="value"/>.Equals(<paramref name="otherValue1"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue2"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue3"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<ELEMENT_T>(this ELEMENT_T value, ELEMENT_T otherValue1, ELEMENT_T otherValue2, ELEMENT_T otherValue3)
            where ELEMENT_T : Enum
            => value is null
                ? otherValue1 is null || otherValue2 is null || otherValue3 is null
                : value.Equals(otherValue1) || value.Equals(otherValue2) || value.Equals(otherValue3);

        /// <summary>
        /// 指定された値が、別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="otherValue1">
        /// 1番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue2">
        /// 2番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue3">
        /// 3番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue4">
        /// 4番目の比較対象の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/> の何れかと等しい場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードと等価です。
        /// <code>
        /// <paramref name="value"/>.Equals(<paramref name="otherValue1"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue2"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue3"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue4"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<ELEMENT_T>(this ELEMENT_T value, ELEMENT_T otherValue1, ELEMENT_T otherValue2, ELEMENT_T otherValue3, ELEMENT_T otherValue4)
            where ELEMENT_T : Enum
            => value is null
                ? otherValue1 is null || otherValue2 is null || otherValue3 is null || otherValue4 is null
                : value.Equals(otherValue1) || value.Equals(otherValue2) || value.Equals(otherValue3) || value.Equals(otherValue4);

        /// <summary>
        /// 指定された値が、別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="otherValues">
        /// 比較対象の値の配列です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="otherValues"/> の要素の何れかと等しい場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードと等価です。
        /// <code>
        /// <paramref name="value"/>.Equals(<paramref name="otherValues"/>[0]) || <paramref name="value"/>.Equals(<paramref name="otherValues"/>[1]) || ... || <paramref name="value"/>.Equals(<paramref name="otherValues"/>[<paramref name="otherValues"/>.Length - 2]) || <paramref name="value"/>.Equals(<paramref name="otherValues"/>.Length - 1)
        /// </code>
        /// </para>
        /// </returns>
        public static Boolean IsAnyOf<ELEMENT_T>(this ELEMENT_T value, params ELEMENT_T[] otherValues)
            where ELEMENT_T : Enum
        {
            if (otherValues is null)
                throw new ArgumentNullException(nameof(otherValues));

            if (value is null)
            {
                for (var index = 0; index < otherValues.Length; ++index)
                {
                    if (otherValues[index] is null)
                        return true;
                }

                return false;
            }
            else
            {
                for (var index = 0; index < otherValues.Length; ++index)
                {
                    if (value.Equals(otherValues[index]))
                        return true;
                }

                return false;
            }
        }

        #endregion

        /// <summary>
        /// ある値が範囲内にあるかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 調べる値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値である <typeparamref name="VALUE_T"/> オブジェクトです。
        /// </param>
        /// <param name="minimumValue">
        /// 範囲の最小値である <typeparamref name="VALUE_T"/> オブジェクトです。
        /// </param>
        /// <param name="maximumValue">
        /// 範囲の最大値である <typeparamref name="VALUE_T"/> オブジェクトです。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="minimumValue"/> 以上、かつ <paramref name="maximumValue"/> 以下である場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// これは、以下の不等式が成立する条件と同等です。
        /// </para>
        /// <code>
        /// <paramref name="minimumValue"/> &lt;= <paramref name="value"/> &lt;= <paramref name="maximumValue"/>
        /// </code>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsBetween<VALUE_T>(this VALUE_T value, VALUE_T minimumValue, VALUE_T maximumValue)
            where VALUE_T : Enum
            => value.CompareTo(minimumValue) >= 0 && value.CompareTo(maximumValue) <= 0;

        /// <summary>
        /// ある値が範囲内にあるかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 調べる値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値である <typeparamref name="VALUE_T"/> オブジェクトです。
        /// </param>
        /// <param name="minimumValue">
        /// 範囲の下限値である <typeparamref name="VALUE_T"/> オブジェクトです。
        /// </param>
        /// <param name="maximumValue">
        /// 範囲の上限値である <typeparamref name="VALUE_T"/> オブジェクトです。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="lowerBoundary"/> 以上、かつ <paramref name="upperBoundary"/> 未満である場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// これは、以下の不等式が成立する条件と同等です。
        /// </para>
        /// <code>
        /// <paramref name="lowerBoundary"/> &lt;= <paramref name="value"/> &lt; <paramref name="upperBoundary"/>
        /// </code>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange<VALUE_T>(this VALUE_T value, VALUE_T lowerBoundary, VALUE_T upperBoundary)
            where VALUE_T : Enum
            => value.CompareTo(lowerBoundary) >= 0 && value.CompareTo(upperBoundary) < 0;
    }
}
