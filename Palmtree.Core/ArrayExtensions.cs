using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

//#error // TODO: Experiment.CSharp.Library プロジェクトの VectorizedCalculation クラスの実装
//#error // TODO: Experiment.CSharp.Library プロジェクトで、Sum, SumNumber, UncheckedSum の性能評価⇒要素型毎のハードウェアアクセラレーションの是非の決定
//#error // TODO: ArrayExtensions クラスにて Sum, SumNumber, UncheckedSum メソッドを正式実装

namespace Palmtree
{
    public static partial class ArrayExtensions
    {
        // ・ジェネリックメソッドにおいて、typeof() による型分岐のコストは JIT の最適化によりほぼゼロになるらしい。
        //   出典: https://qiita.com/aka-nse/items/2f45f056262d2d5c6df7
        //   自分でも実験済み。JIT での最適化により分岐処理のコードがゼロになる。
        //
        // ・sizeof(NFloat) と sizeof(Single) または sizeof(Double) の比較は実行時に最適化される。(実験結果より) ただし、sizeof(NFloat)の取得は unsafe コンテキスト内にて行う必要がある。
        //   例: 64bit 環境にて、if (sizeof(NFloat) == sizeof(Single)) {} というステートメントを記述すると、条件が常に false であるため、実行時のコンパイル結果に残らない。

        #region GetOffsetAndLength

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static (Int32 Offset, Int32 Length) GetOffsetAndLength<ELEMENT_T>(this ELEMENT_T[] source, Range range)
        {
            ArgumentNullException.ThrowIfNull(source);

            return range.GetOffsetAndLength(source.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static (Int32 Offset, Int32 Length) GetOffsetAndLength<ELEMENT_T>(this Span<ELEMENT_T> source, Range range)
            => range.GetOffsetAndLength(source.Length);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static (Int32 Offset, Int32 Length) GetOffsetAndLength<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> source, Range range)
            => range.GetOffsetAndLength(source.Length);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static (Int32 offset, Int32 count) GetOffsetAndLength<ELEMENT_T>(this ELEMENT_T[] array, Range range, [CallerArgumentExpression(nameof(range))] String? parameterName = null)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentException.ThrowIfNullOrEmpty(parameterName);

            try
            {
                return array.GetOffsetAndLength(range);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(parameterName, ex);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static (Int32 offset, Int32 count) GetOffsetAndLength<ELEMENT_T>(this Span<ELEMENT_T> array, Range range, [CallerArgumentExpression(nameof(range))] String? parameterName = null)
        {
            try
            {
                return array.GetOffsetAndLength(range);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(parameterName, ex);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (Int32 offset, Int32 count) GetOffsetAndLength<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array, Range range, [CallerArgumentExpression(nameof(range))] String? parameterName = null)
        {
            try
            {
                return array.GetOffsetAndLength(range);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(parameterName, ex);
            }
        }

        #endregion

        #region AsReadOnly

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnly<ELEMENT_T>(this ELEMENT_T[] source)
        {
            ArgumentNullException.ThrowIfNull(source);

            return new ReadOnlyMemory<ELEMENT_T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnly<ELEMENT_T>(this ELEMENT_T[] source, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, source.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, source.Length - offset);

            return new ReadOnlyMemory<ELEMENT_T>(source, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnly<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 length)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(length, (UInt32)sourceArray.Length - offset);

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnly<ELEMENT_T>(this Span<ELEMENT_T> sourceArray) => sourceArray;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnly<ELEMENT_T>(this Memory<ELEMENT_T> sourceArray) => sourceArray;

        #endregion

        #region AsMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Memory<ELEMENT_T> AsMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);

            return new Memory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)(sourceArray.Length - offset));
        }

#if false
        public static Memory<ELEMENT_T> AsMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 length)
        {
            throw new NotImplementedException(); // defined in System.MemoryExtensions.AsMemory<T>(this T[]? array, int start, int length)
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Memory<ELEMENT_T> AsMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 length)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(length, (UInt32)sourceArray.Length - offset);

            return new Memory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)length);
        }

        #endregion

        #region AsSpan

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Span<ELEMENT_T> AsSpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);

            return new Span<ELEMENT_T>(sourceArray, checked((Int32)offset), checked((Int32)((UInt32)sourceArray.Length - offset)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Span<ELEMENT_T> AsSpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Length - offset);

            return new Span<ELEMENT_T>(sourceArray, checked((Int32)offset), checked((Int32)count));
        }

        #endregion

        #region AsReadOnlyMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, offset, sourceArray.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)(sourceArray.Length - offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 length)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(length, sourceArray.Length - offset);

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, offset, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 length)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(length, (UInt32)sourceArray.Length - offset);

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)length);
        }

        #endregion

        #region AsReadOnlySpan

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);

            return (ReadOnlySpan<ELEMENT_T>)sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);

            return new ReadOnlySpan<ELEMENT_T>(sourceArray, offset, sourceArray.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);

            return new Span<ELEMENT_T>(sourceArray, (Int32)offset, sourceArray.Length - (Int32)offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);

            var (offset, count) = sourceArray.GetOffsetAndLength(range);
            return new ReadOnlySpan<ELEMENT_T>(sourceArray, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Length - offset);

            return new ReadOnlySpan<ELEMENT_T>(sourceArray, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Length - offset);

            return new Span<ELEMENT_T>(sourceArray, checked((Int32)offset), checked((Int32)count));
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// 検索対象の値と検索の開始位置を指定して <typeparamref name="VALUE_T"/> の配列から値が一致する要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <typeparamref name="VALUE_T"/> の配列です。
        /// </param>
        /// <param name="value">
        /// 検索する値を示す <typeparamref name="VALUE_T"/> です。
        /// </param>
        /// <param name="offset">
        /// 検索の開始位置を示す <see cref="Int32"/> です。既定値は 0 です。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> のオフセット <paramref name="offset"/> から始まり <paramref name="buffer"/> の最後までの範囲内に <paramref name="value"/> と一致する要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 一致する要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOf<VALUE_T>(this VALUE_T[] buffer, VALUE_T value, Int32 offset = 0)
        {

            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            var limit = buffer.Length;
            if (value is null)
            {
                for (var index = offset; index < limit; ++index)
                {
                    if (buffer[index] is null)
                        return index;
                }
            }
            else
            {
                for (var index = offset; index < limit; ++index)
                {
                    if (value.Equals(buffer[index]))
                        return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// 検索対象の値と検索の開始位置と長さを指定して <typeparamref name="VALUE_T"/> の配列から値が一致する要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <typeparamref name="VALUE_T"/> の配列です。
        /// </param>
        /// <param name="value">
        /// 検索する値を示す <typeparamref name="VALUE_T"/> です。
        /// </param>
        /// <param name="offset">
        /// 検索の開始位置を示す <see cref="Int32"/> です。
        /// </param>
        /// <param name="count">
        /// 検索する長さを示す <see cref="Int32"/> です。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> のオフセット <paramref name="offset"/> から始まり長さ <paramref name="count"/> の範囲内に <paramref name="value"/> と一致する要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 一致する要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOf<VALUE_T>(this VALUE_T[] buffer, VALUE_T value, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            var limit = offset + count;
            if (value is null)
            {
                for (var index = offset; index < limit; ++index)
                {
                    if (buffer[index] is null)
                        return index;
                }
            }
            else
            {
                for (var index = offset; index < limit; ++index)
                {
                    if (value.Equals(buffer[index]))
                        return index;
                }
            }

            return -1;
        }

        /// <summary>
        /// 一致条件の判定をするデリゲートと検索の開始位置を指定して <typeparamref name="VALUE_T"/> の配列から要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <typeparamref name="VALUE_T"/> の配列です。
        /// </param>
        /// <param name="predicate">
        /// 要素から真偽値を導き出すデリゲートです。
        /// </param>
        /// <param name="offset">
        /// 検索の開始位置を示す <see cref="Int32"/> です。
        /// 既定値は 0 です。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> のオブセット <paramref name="offset"/> から始まり <paramref name="buffer"/> の最後までの範囲内に <paramref name="predicate"/> が <see langword="true"/> を返す要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 条件を満たす要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOf<VALUE_T>(this VALUE_T[] buffer, Func<VALUE_T, Boolean> predicate, Int32 offset = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            var limit = buffer.Length;
            for (var index = offset; index < limit; ++index)
            {
                if (predicate(buffer[index]))
                    return index;
            }

            return -1;
        }

        /// <summary>
        /// 一致条件の判定をするデリゲートと検索の開始位置と長さを指定して <typeparamref name="VALUE_T"/> の配列から要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <typeparamref name="VALUE_T"/> の配列です。
        /// </param>
        /// <param name="predicate">
        /// 要素から真偽値を導き出すデリゲートです。
        /// </param>
        /// <param name="offset">
        /// 検索の開始位置を示す <see cref="Int32"/> です。
        /// </param>
        /// <param name="count">
        /// 検索する長さを示す <see cref="Int32"/> です。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> のオブセット <paramref name="offset"/> から始まり長さ <paramref name="count"/> の範囲内に <paramref name="predicate"/> が <see langword="true"/> を返す要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 条件を満たす要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOf<VALUE_T>(this VALUE_T[] buffer, Func<VALUE_T, Boolean> predicate, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentNullException.ThrowIfNull(predicate);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            var limit = offset + count;
            for (var index = offset; index < limit; ++index)
            {
                if (predicate(buffer[index]))
                    return index;
            }

            return -1;
        }

        /// <summary>
        /// 検索対象の値を指定して <typeparamref name="VALUE_T"/> の配列から値が一致する要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <see cref="ReadOnlySpan{T}"/> です。
        /// </param>
        /// <param name="value">
        /// 検索する値です。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> 内に <paramref name="value"/> と一致する要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 一致する要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOf<VALUE_T>(this ReadOnlySpan<VALUE_T> buffer, VALUE_T value)
        {
            for (var index = 0; index < buffer.Length; ++index)
            {
                var bufferValue = buffer[index];
                if (bufferValue is null && value is null)
                    return index;
                if (bufferValue is not null && bufferValue.Equals(value))
                    return index;
            }

            return -1;
        }

        /// <summary>
        /// 一致条件の判定をするデリゲートを指定して <typeparamref name="VALUE_T"/> の配列から要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <see cref="ReadOnlySpan{T}"/> です。
        /// </param>
        /// <param name="predicate">
        /// 要素から真偽値を導き出すデリゲートです。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> 内に <paramref name="predicate"/> が <see langword="true"/> を返す要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 条件を満たす要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOf<VALUE_T>(this ReadOnlySpan<VALUE_T> buffer, Func<VALUE_T, Boolean> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);

            for (var index = 0; index < buffer.Length; ++index)
            {
                if (predicate(buffer[index]))
                    return index;
            }

            return -1;
        }

        /// <summary>
        /// 検索対象の値を指定して <typeparamref name="VALUE_T"/> の配列から値が一致する要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <see cref="ReadOnlyMemory{T}"/> です。
        /// </param>
        /// <param name="value">
        /// 検索する値です。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> 内に <paramref name="value"/> と一致する要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 一致する要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOf<VALUE_T>(this ReadOnlyMemory<VALUE_T> buffer, VALUE_T value)
            => buffer.Span.IndexOf(value);

        /// <summary>
        /// 一致条件の判定をするデリゲートを指定して <typeparamref name="VALUE_T"/> の配列から要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <see cref="ReadOnlyMemory{T}"/> です。
        /// </param>
        /// <param name="predicate">
        /// 要素から真偽値を導き出すデリゲートです。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> 内に <paramref name="predicate"/> が <see langword="true"/> を返す要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 条件を満たす要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOf<VALUE_T>(this ReadOnlyMemory<VALUE_T> buffer, Func<VALUE_T, Boolean> predicate)
            => buffer.Span.IndexOf(predicate);

        #endregion

        #region IndexOfAny

        /// <summary>
        /// 検索対象の値の配列と検索の開始位置と長さを指定して <typeparamref name="VALUE_T"/> の配列から検索対象の配列の要素の何れかと一致する要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <typeparamref name="VALUE_T"/> の配列です。
        /// </param>
        /// <param name="values">
        /// 検索する値の <typeparamref name="VALUE_T"/> の配列です。
        /// </param>
        /// <param name="offset">
        /// 検索の開始位置を示す <see cref="Int32"/> です。
        /// 既定値は 0 です。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> のオフセット <paramref name="offset"/> から始まり長さ <paramref name="count"/> の範囲内に <paramref name="value"/> 何れかの要素と一致する要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 一致する要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOfAny<VALUE_T>(this VALUE_T[] buffer, VALUE_T[] values, Int32 offset = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentNullException.ThrowIfNull(values);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            var limit = buffer.Length;
            var limit2 = values.Length;
            for (var index = offset; index < limit; ++index)
            {
                var element = buffer[index];
                if (element is null)
                {
                    for (var index2 = 0; index2 < limit2; ++index2)
                    {
                        if (values[index2] is null)
                            return index;
                    }
                }
                else
                {
                    for (var index2 = 0; index2 < limit2; ++index2)
                    {
                        if (element.Equals(values[index2]))
                            return index;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// 検索対象の値の配列と検索の開始位置と長さを指定して <typeparamref name="VALUE_T"/> の配列から検索対象の配列の要素の何れかと一致する要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <typeparamref name="VALUE_T"/> の配列です。
        /// </param>
        /// <param name="values">
        /// 検索する値の <typeparamref name="VALUE_T"/> の配列です。
        /// </param>
        /// <param name="offset">
        /// 検索の開始位置を示す <see cref="Int32"/> です。
        /// </param>
        /// <param name="count">
        /// 検索する長さを示す <see cref="Int32"/> です。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> のオフセット <paramref name="offset"/> から始まり長さ <paramref name="count"/> の範囲内に <paramref name="value"/> 何れかの要素と一致する要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 一致する要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOfAny<VALUE_T>(this VALUE_T[] buffer, VALUE_T[] values, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentNullException.ThrowIfNull(values);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            var limit = offset + count;
            var limit2 = values.Length;
            for (var index = offset; index < limit; ++index)
            {
                var element = buffer[index];
                if (element is null)
                {
                    for (var index2 = 0; index2 < limit2; ++index2)
                    {
                        if (values[index2] is null)
                            return index;
                    }
                }
                else
                {
                    for (var index2 = 0; index2 < limit2; ++index2)
                    {
                        if (element.Equals(values[index2]))
                            return index;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// 検索対象の値を指定して <typeparamref name="VALUE_T"/> の配列から値が一致する要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <see cref="ReadOnlySpan{T}"/> です。
        /// </param>
        /// <param name="value">
        /// 検索する値です。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> 内に <paramref name="value"/> と一致する要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 一致する要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOfAny<VALUE_T>(this ReadOnlySpan<VALUE_T> buffer, VALUE_T[] values)
        {
            ArgumentNullException.ThrowIfNull(values);

            var limit = buffer.Length;
            var limit2 = values.Length;
            for (var index = 0; index < limit; ++index)
            {
                var element = buffer[index];
                if (element is null)
                {
                    for (var index2 = 0; index2 < limit2; ++index2)
                    {
                        if (values[index2] is null)
                            return index;
                    }
                }
                else
                {
                    for (var index2 = 0; index2 < limit2; ++index2)
                    {
                        if (element.Equals(values[index2]))
                            return index;
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// 検索対象の値を指定して <typeparamref name="VALUE_T"/> の配列から値が一致する要素を検索します。
        /// </summary>
        /// <typeparam name="VALUE_T">
        /// 要素の型です。
        /// </typeparam>
        /// <param name="buffer">
        /// 検索対象の <see cref="ReadOnlyMemory{T}"/> です。
        /// </param>
        /// <param name="value">
        /// 検索する値です。
        /// </param>
        /// <returns>
        /// <paramref name="buffer"/> 内に <paramref name="value"/> と一致する要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 一致する要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOfAny<VALUE_T>(this ReadOnlyMemory<VALUE_T> buffer, VALUE_T[] values)
            => buffer.Span.IndexOfAny(values);

        #endregion

        #region Slice

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);

            return new Memory<ELEMENT_T>(sourceArray, offset, sourceArray.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);

            return new Memory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)(sourceArray.Length - offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);

            var (offset, length) = range.GetOffsetAndLength(sourceArray.Length);
            return new Memory<ELEMENT_T>(sourceArray, offset, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 length)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(length, sourceArray.Length - offset);

            return new Memory<ELEMENT_T>(sourceArray, offset, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 length)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(length, (UInt32)sourceArray.Length - offset);

            return new Memory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Span<ELEMENT_T> Slice<ELEMENT_T>(this Span<ELEMENT_T> sourceArray, UInt32 offset)
            => sourceArray[(Int32)offset..];

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Span<ELEMENT_T> Slice<ELEMENT_T>(this Span<ELEMENT_T> sourceArray, UInt32 offset, UInt32 length)
            => sourceArray.Slice(checked((Int32)offset), checked((Int32)length));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlySpan<ELEMENT_T> Slice<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> sourceArray, UInt32 offset)
            => sourceArray[(Int32)offset..];

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlySpan<ELEMENT_T> Slice<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> sourceArray, UInt32 offset, UInt32 length)
            => sourceArray.Slice(checked((Int32)offset), checked((Int32)length));

        #endregion

        #region GetSequence

        public static IEnumerable<ELEMENT_T> GetSequence<ELEMENT_T>(this Memory<ELEMENT_T> source)
        {
            for (var index = 0; index < source.Length; ++index)
                yield return source.Span[index];
        }

        public static IEnumerable<ELEMENT_T> GetSequence<ELEMENT_T>(this ReadOnlyMemory<ELEMENT_T> source)
        {
            for (var index = 0; index < source.Length; ++index)
                yield return source.Span[index];
        }

        #endregion

        #region Duplicate

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T[] Duplicate<ELEMENT_T>(this ELEMENT_T[] sourceArray)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);

            var buffer = new ELEMENT_T[sourceArray.Length];
            sourceArray.CopyTo(buffer, 0);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Memory<ELEMENT_T> Duplicate<ELEMENT_T>(this Memory<ELEMENT_T> sourceArray)
        {
            var buffer = new ELEMENT_T[sourceArray.Length];
            sourceArray.Span.CopyTo(buffer);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlyMemory<ELEMENT_T> Duplicate<ELEMENT_T>(this ReadOnlyMemory<ELEMENT_T> source)
        {
            var buffer = new ELEMENT_T[source.Length];
            source.Span.CopyTo(buffer);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Span<ELEMENT_T> Duplicate<ELEMENT_T>(this Span<ELEMENT_T> sourceArray)
        {
            var buffer = new ELEMENT_T[sourceArray.Length];
            sourceArray.CopyTo(buffer);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ReadOnlySpan<ELEMENT_T> Duplicate<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> source)
        {
            var buffer = new ELEMENT_T[source.Length];
            source.CopyTo(buffer);
            return buffer;
        }

        #endregion

        #region ClearArray

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer)
        {
            ArgumentNullException.ThrowIfNull(buffer);

            Array.Clear(buffer, 0, buffer.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            Array.Clear(buffer, offset, buffer.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer, UInt32 offset)
            => buffer.ClearArray(checked((Int32)offset));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer, Range range)
        {
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            Array.Clear(buffer, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            Array.Clear(buffer, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(buffer);

            buffer.ClearArray(checked((Int32)offset), checked((Int32)count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void ClearArray<ELEMENT_T>(this Span<ELEMENT_T> buffer) => buffer.Clear();

        #endregion

        #region FillArray

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            ArgumentNullException.ThrowIfNull(buffer);

            Array.Fill(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, Int32 offset)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            Array.Fill(buffer, value, offset, buffer.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, UInt32 offset)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            ArgumentNullException.ThrowIfNull(buffer);

            buffer.FillArray(value, checked((Int32)offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, Range range)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            Array.Fill(buffer, value, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, Int32 offset, Int32 count)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            Array.Fill(buffer, value, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, UInt32 offset, UInt32 count)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            ArgumentNullException.ThrowIfNull(buffer);

            buffer.FillArray(value, checked((Int32)offset), checked((Int32)count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void FillArray<ELEMENT_T>(this Span<ELEMENT_T> buffer, ELEMENT_T value)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
            => buffer.Fill(value);

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<Int32, ELEMENT_T> valueGetter)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentNullException.ThrowIfNull(valueGetter);

            var count = buffer.Length;
            for (var index = 0; index < count; ++index)
                buffer[index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<Int32, ELEMENT_T> valueGetter, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentNullException.ThrowIfNull(valueGetter);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            var count = buffer.Length - offset;
            for (var index = 0; index < count; ++index)
                buffer[offset + index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<Int32, ELEMENT_T> valueGetter, Range range)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentNullException.ThrowIfNull(valueGetter);
            var (offset, count) = buffer.GetOffsetAndLength(range);

            for (var index = 0; index < count; ++index)
                buffer[offset + index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<Int32, ELEMENT_T> valueGetter, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentNullException.ThrowIfNull(valueGetter);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            for (var index = 0; index < count; ++index)
                buffer[offset + index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<UInt32, ELEMENT_T> valueGetter, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentNullException.ThrowIfNull(valueGetter);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            var count = (UInt32)buffer.Length - offset;
            for (var index = 0U; index < count; ++index)
                buffer[offset + index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<UInt32, ELEMENT_T> valueGetter, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentNullException.ThrowIfNull(valueGetter);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);

            for (var index = 0U; index < count; ++index)
                buffer[offset + index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this Span<ELEMENT_T> buffer, Func<Int32, ELEMENT_T> valueGetter)
        {
            ArgumentNullException.ThrowIfNull(valueGetter);

            var count = buffer.Length;
            for (var index = 0; index < count; ++index)
                buffer[index] = valueGetter(index);
        }

        #endregion

        #region CopyTo

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void CopyTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, ELEMENT_T[] destinationArray, UInt32 destinationArrayOffset)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(destinationArrayOffset, (UInt32)destinationArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(sourceArray.Length, destinationArray.Length - (Int32)destinationArrayOffset);

            sourceArray.CopyTo(destinationArray, (Int32)destinationArrayOffset);
        }

#if false // 拡張メソッドとしてはわかりにくい構文なので削除
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void CopyTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 sourceArrayOffset, ELEMENT_T[] destinationArray, Int32 destinationArrayOffset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentOutOfRangeException.ThrowIfNegative(sourceArrayOffset);
            ArgumentNullException.ThrowIfNull(destinationArray);
            ArgumentOutOfRangeException.ThrowIfNegative(destinationArrayOffset);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(sourceArrayOffset, sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceArray.Length - sourceArrayOffset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(destinationArrayOffset, destinationArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, destinationArray.Length - destinationArrayOffset);

            Array.Copy(sourceArray, sourceArrayOffset, destinationArray, destinationArrayOffset, count);
        }
#endif

#if false // 拡張メソッドとしてはわかりにくい構文なので削除
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void CopyTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 sourceArrayOffset, ELEMENT_T[] destinationArray, UInt32 destinationArrayOffset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);
            ArgumentNullException.ThrowIfNull(destinationArray);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(sourceArrayOffset, (UInt32)sourceArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)sourceArray.Length - sourceArrayOffset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(destinationArrayOffset, (UInt32)destinationArray.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)destinationArray.Length - destinationArrayOffset);

            Array.Copy(sourceArray, (Int32)sourceArrayOffset, destinationArray, (Int32)destinationArrayOffset, (Int32)count);
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void CopyTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, Span<ELEMENT_T> destinationArray)
        {
            ArgumentNullException.ThrowIfNull(sourceArray);

            ((Span<ELEMENT_T>)sourceArray).CopyTo(destinationArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void CopyTo<ELEMENT_T>(this Span<ELEMENT_T> sourceArray, ELEMENT_T[] destinationArray)
        {
            ArgumentNullException.ThrowIfNull(destinationArray);

            sourceArray.CopyTo((Span<ELEMENT_T>)destinationArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void CopyTo<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> sourceArray, ELEMENT_T[] destinationArray)
            => sourceArray.CopyTo((Span<ELEMENT_T>)destinationArray);

        #endregion

        #region ToDictionary

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this ELEMENT_T[] source, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter);
        }

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this ELEMENT_T[] source, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, keyEqualityComparer);
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this ELEMENT_T[] source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(valueSelecter);

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, valueSelecter);
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this ELEMENT_T[] source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(valueSelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, valueSelecter, keyEqualityComparer);
        }

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter);
        }

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, keyEqualityComparer);
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(valueSelecter);

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, valueSelecter);
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(valueSelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, valueSelecter, keyEqualityComparer);
        }

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);

            var dictionary = new Dictionary<KEY_T, ELEMENT_T>();
            foreach (var element in source)
                dictionary.Add(keySelecter(element), element);
            return dictionary;
        }

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            var dictionary = new Dictionary<KEY_T, ELEMENT_T>(keyEqualityComparer);
            foreach (var element in source)
                dictionary.Add(keySelecter(element), element);
            return dictionary;
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(valueSelecter);

            var dictionary = new Dictionary<KEY_T, VALUE_T>();
            foreach (var element in source)
                dictionary.Add(keySelecter(element), valueSelecter(element));
            return dictionary;
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(valueSelecter);
            ArgumentNullException.ThrowIfNull(keyEqualityComparer);

            var dictionary = new Dictionary<KEY_T, VALUE_T>(keyEqualityComparer);
            foreach (var element in source)
                dictionary.Add(keySelecter(element), valueSelecter(element));
            return dictionary;
        }

        #endregion

        #region IsBitwiseEquatableQuickly
        private static Boolean IsBitwiseEquatableQuickly<ELEMENT_T>()
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                return true;
            if (typeof(ELEMENT_T) == typeof(Char))
                return true;
            if (typeof(ELEMENT_T) == typeof(System.Text.Rune))
                return true;
            if (typeof(ELEMENT_T) == typeof(SByte))
                return true;
            if (typeof(ELEMENT_T) == typeof(Byte))
                return true;
            if (typeof(ELEMENT_T) == typeof(Int16))
                return true;
            if (typeof(ELEMENT_T) == typeof(UInt16))
                return true;
            if (typeof(ELEMENT_T) == typeof(Int32))
                return true;
            if (typeof(ELEMENT_T) == typeof(UInt32))
                return true;
            if (typeof(ELEMENT_T) == typeof(Int64))
                return true;
            if (typeof(ELEMENT_T) == typeof(UInt64))
                return true;
            if (typeof(ELEMENT_T) == typeof(Int128))
                return true;
            if (typeof(ELEMENT_T) == typeof(UInt128))
                return true;
            if (typeof(ELEMENT_T) == typeof(IntPtr))
                return true;
            if (typeof(ELEMENT_T) == typeof(UIntPtr))
                return true;

            if (typeof(ELEMENT_T) == typeof(Single))
                return false;
            if (typeof(ELEMENT_T) == typeof(Double))
                return false;
            if (typeof(ELEMENT_T) == typeof(Decimal))
                return false;
            if (typeof(ELEMENT_T) == typeof(NFloat))
                return false;

            if (typeof(ELEMENT_T).IsEnum)
                return true;

#if false // Pointer cannot be used in type parameters.
            if (typeof(ELEMENT_T).IsPointer)
                return false;
            if (typeof(ELEMENT_T).IsFunctionPointer)
                return false;
            if (typeof(ELEMENT_T).IsUnmanagedFunctionPointer)
                return false;
#endif

            return typeof(ELEMENT_T).IsValueType;
        }

        #endregion
    }
}
