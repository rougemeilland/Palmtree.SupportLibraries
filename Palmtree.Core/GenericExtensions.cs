using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static class GenericExtensions
    {
        #region IsBetween

        /// <summary>
        /// 指定された値が範囲内にあるかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE1_T">
        /// <para>
        /// 調べる値の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IComparable{VALUE2_T}">IComparable&lt;<typeparamref name="VALUE2_T"/>&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <typeparam name="VALUE2_T">
        /// 範囲の上限/下限の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="lowerValue">
        /// 下限値です。
        /// </param>
        /// <param name="upperValue">
        /// 上限値です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="lowerValue"/> 以上でありかつ <paramref name="upperValue"/> 以下である場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code><paramref name="value"/>.CompareTo(<paramref name="lowerValue"/>) &gt;= 0 &amp;&amp; <paramref name="value"/>.CompareTo(<paramref name="upperValue"/>) &lt;= 0</code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsBetween<VALUE1_T, VALUE2_T>(this VALUE1_T value, VALUE2_T lowerValue, VALUE2_T upperValue)
            where VALUE1_T : IComparable<VALUE2_T>
            => value is null
                ? lowerValue is null
                : value.CompareTo(lowerValue) >= 0 && value.CompareTo(upperValue) <= 0;

        /// <summary>
        /// 指定された比較子を使用して、指定された値が範囲内にあるかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 調べる値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="lowerValue">
        /// 下限値です。
        /// </param>
        /// <param name="upperValue">
        /// 上限値です。
        /// </param>
        /// <param name="comparer">
        /// 値を比較するための比較子です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="lowerValue"/> 以上でありかつ <paramref name="upperValue"/> 以下である場合は true を返します。
        /// そうではない場合は false を返します。
        /// 値の比較には比較子 <paramref name="comparer"/> を使用します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code><paramref name="comparer"/>.Compare(<paramref name="value"/>, <paramref name="lowerValue"/>) &gt;= 0 &amp;&amp; <paramref name="comparer"/>.Compare(<paramref name="value"/>, <paramref name="upperValue"/>) &lt;= 0</code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsBetween<VALUE_T>(this VALUE_T value, VALUE_T lowerValue, VALUE_T upperValue, IComparer<VALUE_T> comparer)
            => comparer.Compare(value, lowerValue) >= 0 && comparer.Compare(value, upperValue) <= 0;

        #endregion

        #region InRange

        /// <summary>
        /// 指定された値が範囲内にあるかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE1_T">
        /// <para>
        /// 調べる値の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IComparable{VALUE2_T}">IComparable&lt;<typeparamref name="VALUE2_T"/>&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <typeparam name="VALUE2_T">
        /// 範囲の上限/下限の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="lowerValue">
        /// 下限値です。
        /// </param>
        /// <param name="upperValue">
        /// 上限値です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="lowerValue"/> 以上でありかつ <paramref name="upperValue"/> 未満である場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code><paramref name="value"/>.CompareTo(<paramref name="lowerValue"/>) &gt;= 0 &amp;&amp; <paramref name="value"/>.CompareTo(<paramref name="upperValue"/>) &lt; 0</code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange<VALUE1_T, VALUE2_T>(this VALUE1_T value, VALUE2_T lowerValue, VALUE2_T upperValue)
            where VALUE1_T : IComparable<VALUE2_T>
            => value is null
                ? lowerValue is null
                : value.CompareTo(lowerValue) >= 0 && value.CompareTo(upperValue) < 0;

        /// <summary>
        /// 指定された比較子を使用して、指定された値が範囲内にあるかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 調べる値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="lowerValue">
        /// 下限値です。
        /// </param>
        /// <param name="upperValue">
        /// 上限値です。
        /// </param>
        /// <param name="comparer">
        /// 値を比較するための比較子です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="lowerValue"/> 以上でありかつ <paramref name="upperValue"/> 未満である場合は true を返します。
        /// そうではない場合は false を返します。
        /// 値の比較には比較子 <paramref name="comparer"/> を使用します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code><paramref name="comparer"/>.Compare(<paramref name="value"/>, <paramref name="lowerValue"/>) &gt;= 0 &amp;&amp; <paramref name="comparer"/>.Compare(<paramref name="value"/>, <paramref name="upperValue"/>) &lt; 0</code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange<VALUE_T>(this VALUE_T value, VALUE_T lowerValue, VALUE_T upperValue, IComparer<VALUE_T> comparer)
            => comparer.Compare(value, lowerValue) >= 0 && comparer.Compare(value, upperValue) < 0;

        #endregion

        #region IsNoneOf

        /// <summary>
        /// 指定された値が指定された別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE1_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IEquatable{VALUE2_T}">IEquatable&lt;<typeparamref name="VALUE2_T"/>&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <typeparam name="VALUE2_T">
        /// 比較対象の値の型です。
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
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// !<paramref name="value"/>.Equals(<paramref name="otherValue1"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue2"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<VALUE1_T, VALUE2_T>(this VALUE1_T value, VALUE2_T otherValue1, VALUE2_T otherValue2)
            where VALUE1_T : IEquatable<VALUE2_T>
            => !value.IsAnyOf(otherValue1, otherValue2);

        /// <summary>
        /// 指定された値が指定された別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE1_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IEquatable{VALUE2_T}">IEquatable&lt;<typeparamref name="VALUE2_T"/>&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <typeparam name="VALUE2_T">
        /// 比較対象の値の型です。
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
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// !<paramref name="value"/>.Equals(<paramref name="otherValue1"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue2"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue3"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<VALUE1_T, VALUE2_T>(this VALUE1_T value, VALUE2_T otherValue1, VALUE2_T otherValue2, VALUE2_T otherValue3)
            where VALUE1_T : IEquatable<VALUE2_T>
            => !value.IsAnyOf(otherValue1, otherValue2, otherValue3);

        /// <summary>
        /// 指定された値が指定された別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE1_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IEquatable{VALUE2_T}">IEquatable&lt;<typeparamref name="VALUE2_T"/>&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <typeparam name="VALUE2_T">
        /// 比較対象の値の型です。
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
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// !<paramref name="value"/>.Equals(<paramref name="otherValue1"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue2"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue3"/>) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValue4"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<VALUE1_T, VALUE2_T>(this VALUE1_T value, VALUE2_T otherValue1, VALUE2_T otherValue2, VALUE2_T otherValue3, VALUE2_T otherValue4)
            where VALUE1_T : IEquatable<VALUE2_T>
            => !value.IsAnyOf(otherValue1, otherValue2, otherValue3, otherValue4);

        /// <summary>
        /// 指定された値が指定された別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE1_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IEquatable{VALUE2_T}">IEquatable&lt;<typeparamref name="VALUE2_T"/>&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <typeparam name="VALUE2_T">
        /// 比較対象の値の型です。
        /// </typeparam>
        /// <param name="value">
        /// 調べる値です。
        /// </param>
        /// <param name="otherValues">
        /// 比較対象の値の配列です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="value"/> が <paramref name="otherValues"/> の要素の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// !<paramref name="value"/>.Equals(<paramref name="otherValues"/>[0]) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValues"/>[1]) &amp;&amp; ...  &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValues"/>[<paramref name="otherValues"/>.Length - 2]) &amp;&amp; !<paramref name="value"/>.Equals(<paramref name="otherValues"/>.Length - 1)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<VALUE1_T, VALUE2_T>(this VALUE1_T value, params VALUE2_T[] otherValues)
            where VALUE1_T : IEquatable<VALUE2_T>
            => !value.IsAnyOf(otherValues);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
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
        /// <param name="equalityComparer">
        /// 値の比較のための等値比較子です。
        /// </param>
        /// <returns>
        /// <para>
        /// 等値比較子 <paramref name="equalityComparer"/> を使用し、<paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/> の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, IEqualityComparer<VALUE_T> equalityComparer)
            => !value.IsAnyOf(otherValue1, otherValue2, equalityComparer);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
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
        /// <param name="equalityComparer">
        /// 値の比較のための等値比較子です。
        /// </param>
        /// <returns>
        /// <para>
        /// 等値比較子 <paramref name="equalityComparer"/> を使用し、<paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/> の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, IEqualityComparer<VALUE_T> equalityComparer)
            => !value.IsAnyOf(otherValue1, otherValue2, otherValue3, equalityComparer);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
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
        /// <param name="equalityComparer">
        /// 値の比較のための等値比較子です。
        /// </param>
        /// <returns>
        /// <para>
        /// 等値比較子 <paramref name="equalityComparer"/> を使用し、<paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/> の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue4"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, VALUE_T otherValue4, IEqualityComparer<VALUE_T> equalityComparer)
            => !value.IsAnyOf(otherValue1, otherValue2, otherValue3, otherValue4, equalityComparer);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
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
        /// <param name="otherValue5">
        /// 5番目の比較対象の値です。
        /// </param>
        /// <param name="equalityComparer">
        /// 値の比較のための等値比較子です。
        /// </param>
        /// <returns>
        /// <para>
        /// 等値比較子 <paramref name="equalityComparer"/> を使用し、<paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/>, <paramref name="otherValue5"/> の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue4"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue5"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, VALUE_T otherValue4, VALUE_T otherValue5, IEqualityComparer<VALUE_T> equalityComparer)
            => !value.IsAnyOf(otherValue1, otherValue2, otherValue3, otherValue4, otherValue5, equalityComparer);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
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
        /// <param name="otherValue5">
        /// 5番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue6">
        /// 6番目の比較対象の値です。
        /// </param>
        /// <param name="equalityComparer">
        /// 値の比較のための等値比較子です。
        /// </param>
        /// <returns>
        /// <para>
        /// 等値比較子 <paramref name="equalityComparer"/> を使用し、<paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/>, <paramref name="otherValue5"/>, <paramref name="otherValue6"/> の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue4"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue5"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue6"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, VALUE_T otherValue4, VALUE_T otherValue5, VALUE_T otherValue6, IEqualityComparer<VALUE_T> equalityComparer)
            => !value.IsAnyOf(otherValue1, otherValue2, otherValue3, otherValue4, otherValue5, otherValue6, equalityComparer);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
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
        /// <param name="otherValue5">
        /// 5番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue6">
        /// 6番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue7">
        /// 7番目の比較対象の値です。
        /// </param>
        /// <param name="equalityComparer">
        /// 値の比較のための等値比較子です。
        /// </param>
        /// <returns>
        /// <para>
        /// 等値比較子 <paramref name="equalityComparer"/> を使用し、<paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/>, <paramref name="otherValue5"/>, <paramref name="otherValue6"/>, <paramref name="otherValue7"/> の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue4"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue5"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue6"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue7"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, VALUE_T otherValue4, VALUE_T otherValue5, VALUE_T otherValue6, VALUE_T otherValue7, IEqualityComparer<VALUE_T> equalityComparer)
            => !value.IsAnyOf(otherValue1, otherValue2, otherValue3, otherValue4, otherValue5, otherValue6, otherValue7, equalityComparer);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れとも等しくないかどうかを調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
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
        /// <param name="otherValue5">
        /// 5番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue6">
        /// 6番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue7">
        /// 7番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue8">
        /// 8番目の比較対象の値です。
        /// </param>
        /// <param name="equalityComparer">
        /// 値の比較のための等値比較子です。
        /// </param>
        /// <returns>
        /// <para>
        /// 等値比較子 <paramref name="equalityComparer"/> を使用し、<paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/>, <paramref name="otherValue5"/>, <paramref name="otherValue6"/>, <paramref name="otherValue7"/>, <paramref name="otherValue8"/> の何れとも等しくない場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue4"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue5"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue6"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue7"/>) &amp;&amp; !<paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue8"/>) 
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, VALUE_T otherValue4, VALUE_T otherValue5, VALUE_T otherValue6, VALUE_T otherValue7, VALUE_T otherValue8, IEqualityComparer<VALUE_T> equalityComparer)
            => !value.IsAnyOf(otherValue1, otherValue2, otherValue3, otherValue4, otherValue5, otherValue6, otherValue7, otherValue8, equalityComparer);

        #endregion

        #region IsAnyOf

        /// <summary>
        /// 指定された値が指定された別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="VALUE1_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IEquatable{VALUE2_T}">IEquatable&lt;<typeparamref name="VALUE2_T"/>&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <typeparam name="VALUE2_T">
        /// 比較対象の値の型です。
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
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// <paramref name="value"/>.Equals(<paramref name="otherValue1"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue2"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<VALUE1_T, VALUE2_T>(this VALUE1_T value, VALUE2_T otherValue1, VALUE2_T otherValue2)
            where VALUE1_T : IEquatable<VALUE2_T>
            => value is null
                ? otherValue1 is null || otherValue2 is null
                : value.Equals(otherValue1) || value.Equals(otherValue2);

        /// <summary>
        /// 指定された値が指定された別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="VALUE1_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IEquatable{VALUE2_T}">IEquatable&lt;<typeparamref name="VALUE2_T"/>&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <typeparam name="VALUE2_T">
        /// 比較対象の値の型です。
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
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// <paramref name="value"/>.Equals(<paramref name="otherValue1"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue2"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue3"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<VALUE1_T, VALUE2_T>(this VALUE1_T value, VALUE2_T otherValue1, VALUE2_T otherValue2, VALUE2_T otherValue3)
            where VALUE1_T : IEquatable<VALUE2_T>
            => value is null
                ? otherValue1 is null || otherValue2 is null || otherValue3 is null
                : value.Equals(otherValue1) || value.Equals(otherValue2) || value.Equals(otherValue3);

        /// <summary>
        /// 指定された値が指定された別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="VALUE1_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IEquatable{VALUE2_T}">IEquatable&lt;<typeparamref name="VALUE2_T"/>&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <typeparam name="VALUE2_T">
        /// 比較対象の値の型です。
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
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// <paramref name="value"/>.Equals(<paramref name="otherValue1"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue2"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue3"/>) || <paramref name="value"/>.Equals(<paramref name="otherValue4"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<VALUE1_T, VALUE2_T>(this VALUE1_T value, VALUE2_T otherValue1, VALUE2_T otherValue2, VALUE2_T otherValue3, VALUE2_T otherValue4)
            where VALUE1_T : IEquatable<VALUE2_T>
            => value is null
                ? otherValue1 is null || otherValue2 is null || otherValue3 is null || otherValue4 is null
                : value.Equals(otherValue1) || value.Equals(otherValue2) || value.Equals(otherValue3) || value.Equals(otherValue4);

        /// <summary>
        /// 指定された値が指定された別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="VALUE1_T">
        /// <para>
        /// <paramref name="value"/> の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IEquatable{VALUE2_T}">IEquatable&lt;<typeparamref name="VALUE2_T"/>&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <typeparam name="VALUE2_T">
        /// 比較対象の値の型です。
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
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// <paramref name="value"/>.Equals(<paramref name="otherValues"/>[0]) || <paramref name="value"/>.Equals(<paramref name="otherValues"/>[1]) || ... || <paramref name="value"/>.Equals(<paramref name="otherValues"/>[<paramref name="otherValues"/>.Length - 2]) || <paramref name="value"/>.Equals(<paramref name="otherValues"/>[<paramref name="otherValues"/>.Length - 1])
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<VALUE1_T, VALUE2_T>(this VALUE1_T value, params VALUE2_T[] otherValues)
            where VALUE1_T : IEquatable<VALUE2_T>
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

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <paramref name="value"/> の型です。
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
        /// 等値比較子 <paramref name="equalityComparer"/> を使用して、 <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/> の何れかと等しい場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, IEqualityComparer<VALUE_T> equalityComparer)
            => equalityComparer.Equals(value, otherValue1) ||
                equalityComparer.Equals(value, otherValue2);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <paramref name="value"/> の型です。
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
        /// 等値比較子 <paramref name="equalityComparer"/> を使用して、 <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/> の何れかと等しい場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, IEqualityComparer<VALUE_T> equalityComparer)
            => equalityComparer.Equals(value, otherValue1) ||
                equalityComparer.Equals(value, otherValue2) ||
                equalityComparer.Equals(value, otherValue3);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <paramref name="value"/> の型です。
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
        /// 等値比較子 <paramref name="equalityComparer"/> を使用して、 <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/> の何れかと等しい場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue4"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, VALUE_T otherValue4, IEqualityComparer<VALUE_T> equalityComparer)
            => equalityComparer.Equals(value, otherValue1) ||
                equalityComparer.Equals(value, otherValue2) ||
                equalityComparer.Equals(value, otherValue3) ||
                equalityComparer.Equals(value, otherValue4);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <paramref name="value"/> の型です。
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
        /// <param name="otherValue5">
        /// 5番目の比較対象の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// 等値比較子 <paramref name="equalityComparer"/> を使用して、 <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/>, <paramref name="otherValue5"/> の何れかと等しい場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue4"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue5"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, VALUE_T otherValue4, VALUE_T otherValue5, IEqualityComparer<VALUE_T> equalityComparer)
            => equalityComparer.Equals(value, otherValue1) ||
                equalityComparer.Equals(value, otherValue2) ||
                equalityComparer.Equals(value, otherValue3) ||
                equalityComparer.Equals(value, otherValue4) ||
                equalityComparer.Equals(value, otherValue5);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <paramref name="value"/> の型です。
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
        /// <param name="otherValue5">
        /// 5番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue6">
        /// 6番目の比較対象の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// 等値比較子 <paramref name="equalityComparer"/> を使用して、 <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/>, <paramref name="otherValue5"/>, <paramref name="otherValue6"/> の何れかと等しい場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue4"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue5"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue6"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, VALUE_T otherValue4, VALUE_T otherValue5, VALUE_T otherValue6, IEqualityComparer<VALUE_T> equalityComparer)
            => equalityComparer.Equals(value, otherValue1) ||
                equalityComparer.Equals(value, otherValue2) ||
                equalityComparer.Equals(value, otherValue3) ||
                equalityComparer.Equals(value, otherValue4) ||
                equalityComparer.Equals(value, otherValue5) ||
                equalityComparer.Equals(value, otherValue6);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <paramref name="value"/> の型です。
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
        /// <param name="otherValue5">
        /// 5番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue6">
        /// 6番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue7">
        /// 7番目の比較対象の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// 等値比較子 <paramref name="equalityComparer"/> を使用して、 <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/>, <paramref name="otherValue5"/>, <paramref name="otherValue6"/>, <paramref name="otherValue7"/> の何れかと等しい場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue4"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue5"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue6"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue7"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, VALUE_T otherValue4, VALUE_T otherValue5, VALUE_T otherValue6, VALUE_T otherValue7, IEqualityComparer<VALUE_T> equalityComparer)
            => equalityComparer.Equals(value, otherValue1) ||
                equalityComparer.Equals(value, otherValue2) ||
                equalityComparer.Equals(value, otherValue3) ||
                equalityComparer.Equals(value, otherValue4) ||
                equalityComparer.Equals(value, otherValue5) ||
                equalityComparer.Equals(value, otherValue6) ||
                equalityComparer.Equals(value, otherValue7);

        /// <summary>
        /// 指定された等値比較子を使用して、指定された値が指定された別の複数の値の何れかと等しいかどうか調べます。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <paramref name="value"/> の型です。
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
        /// <param name="otherValue5">
        /// 5番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue6">
        /// 6番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue7">
        /// 7番目の比較対象の値です。
        /// </param>
        /// <param name="otherValue8">
        /// 8番目の比較対象の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// 等値比較子 <paramref name="equalityComparer"/> を使用して、 <paramref name="value"/> が <paramref name="otherValue1"/>, <paramref name="otherValue2"/>, <paramref name="otherValue3"/>, <paramref name="otherValue4"/>, <paramref name="otherValue5"/>, <paramref name="otherValue6"/>, <paramref name="otherValue7"/>, <paramref name="otherValue8"/> の何れかと等しい場合は true を返します。
        /// そうではない場合は false を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// <code>
        /// <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue1"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue2"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue3"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue4"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue5"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue6"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue7"/>) || <paramref name="equalityComparer"/>.Equals(<paramref name="value"/>, <paramref name="otherValue8"/>)
        /// </code>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf<VALUE_T>(this VALUE_T value, VALUE_T otherValue1, VALUE_T otherValue2, VALUE_T otherValue3, VALUE_T otherValue4, VALUE_T otherValue5, VALUE_T otherValue6, VALUE_T otherValue7, VALUE_T otherValue8, IEqualityComparer<VALUE_T> equalityComparer)
            => equalityComparer.Equals(value, otherValue1) ||
                equalityComparer.Equals(value, otherValue2) ||
                equalityComparer.Equals(value, otherValue3) ||
                equalityComparer.Equals(value, otherValue4) ||
                equalityComparer.Equals(value, otherValue5) ||
                equalityComparer.Equals(value, otherValue6) ||
                equalityComparer.Equals(value, otherValue7) ||
                equalityComparer.Equals(value, otherValue8);

        #endregion

        #region Minimum

        /// <summary>
        /// 2つの値を比較して最小値を取得します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <para>
        /// 値の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IComparable{VALUE_T}">IComparable&lt;VALUE_T&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <param name="x">
        /// 比較する1つ目の値です。
        /// </param>
        /// <param name="y">
        /// 比較する2つ目の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="x"/> と <paramref name="y"/> のうち小さい方の値を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// </para>
        /// <code>
        /// <paramref name="x"/>.CompareTo(<paramref name="y"/>) &gt; 0 ? <paramref name="y"/> : <paramref name="x"/>
        /// </code>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: MaybeNull]
        public static VALUE_T Minimum<VALUE_T>([AllowNull] this VALUE_T x, [AllowNull] VALUE_T y)
            where VALUE_T : IComparable<VALUE_T>
            => x is null
                ? default
                : x.CompareTo(y) > 0
                ? y
                : x;

        /// <summary>
        /// 比較子を指定して、2つの値を比較して最小値を取得します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 値の型です。
        /// </typeparam>
        /// <param name="x">
        /// 比較する1つ目の値です。
        /// </param>
        /// <param name="y">
        /// 比較する2つ目の値です。
        /// </param>
        /// <param name="comparer">
        /// 値を比較するための比較子です。
        /// </param>
        /// <returns>
        /// <para>
        /// 比較子 <paramref name="comparer"/> を使用して、<paramref name="x"/> と <paramref name="y"/> のうち小さい方の値を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// </para>
        /// <code>
        /// <paramref name="comparer"/>.Comparer(<paramref name="x"/>, <paramref name="y"/>) &gt; 0 ? <paramref name="y"/> : <paramref name="x"/>
        /// </code>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: MaybeNull, NotNullIfNotNull(nameof(x)), NotNullIfNotNull(nameof(y))]
        public static VALUE_T Minimum<VALUE_T>([AllowNull] this VALUE_T x, [AllowNull] VALUE_T y, IComparer<VALUE_T> comparer)
        {
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return comparer.Compare(x, y) < 0 ? x : y;
        }

        #endregion

        #region Maximum

        /// <summary>
        /// 2つの値を比較して最大値を取得します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// <para>
        /// 値の型です。
        /// </para>
        /// <para>
        /// この型は <see cref="IComparable{VALUE_T}">IComparable&lt;VALUE_T&gt;</see> を実装している必要があります。
        /// </para>
        /// </typeparam>
        /// <param name="x">
        /// 比較する1つ目の値です。
        /// </param>
        /// <param name="y">
        /// 比較する2つ目の値です。
        /// </param>
        /// <returns>
        /// <para>
        /// <paramref name="x"/> と <paramref name="y"/> のうち大きい方の値を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// </para>
        /// <code>
        /// <paramref name="x"/>.CompareTo(<paramref name="y"/>) &gt; 0 ? <paramref name="x"/> : <paramref name="y"/>
        /// </code>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: MaybeNull, NotNullIfNotNull(nameof(x)), NotNullIfNotNull(nameof(y))]
        public static VALUE_T Maximum<VALUE_T>([AllowNull] this VALUE_T x, [AllowNull] VALUE_T y)
            where VALUE_T : IComparable<VALUE_T>
            => x is null
                ? y
                : x.CompareTo(y) > 0
                ? x
                : y;

        /// <summary>
        /// 比較子を指定して、2つの値を比較して最大値を取得します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 値の型です。
        /// </typeparam>
        /// <param name="x">
        /// 比較する1つ目の値です。
        /// </param>
        /// <param name="y">
        /// 比較する2つ目の値です。
        /// </param>
        /// <param name="comparer">
        /// 値を比較するための比較子です。
        /// </param>
        /// <returns>
        /// <para>
        /// 比較子 <paramref name="comparer"/> を使用して、<paramref name="x"/> と <paramref name="y"/> のうち大きい方の値を返します。
        /// </para>
        /// <para>
        /// 戻り値は以下のコードの実行結果と等価です。
        /// </para>
        /// <code>
        /// <paramref name="comparer"/>.Comparer(<paramref name="x"/>, <paramref name="y"/>) &gt; 0 ? <paramref name="x"/> : <paramref name="y"/>
        /// </code>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: MaybeNull, NotNullIfNotNull(nameof(x)), NotNullIfNotNull(nameof(y))]
        public static VALUE_T Maximum<VALUE_T>([AllowNull] this VALUE_T x, [AllowNull] VALUE_T y, IComparer<VALUE_T> comparer)
        {
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return comparer.Compare(x, y) > 0 ? x : y;
        }

        #endregion

        #region Duplicate

        public static VALUE_T Duplicate<VALUE_T>(this VALUE_T value)
            where VALUE_T : ICloneable<VALUE_T>
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return value.Clone();
        }

        #endregion
    }
}
