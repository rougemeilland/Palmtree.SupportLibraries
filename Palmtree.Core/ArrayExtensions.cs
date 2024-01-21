using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static partial class ArrayExtensions
    {
        // ジェネリックメソッドにおいて、typeof() による型分岐のコストは JIT の最適化によりほぼゼロになるらしい。
        //   出典: https://qiita.com/aka-nse/items/2f45f056262d2d5c6df7
        // 自分でも実験済み。JIT での最適化により分岐処理のコードがゼロになる。

        #region GetOffsetAndLength

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Int32 Offset, Int32 Length) GetOffsetAndLength<ELEMENT_T>(this ELEMENT_T[] source, Range range)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return range.GetOffsetAndLength(source.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Int32 Offset, Int32 Length) GetOffsetAndLength<ELEMENT_T>(this Span<ELEMENT_T> source, Range range)
            => range.GetOffsetAndLength(source.Length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Int32 Offset, Int32 Length) GetOffsetAndLength<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> source, Range range)
            => range.GetOffsetAndLength(source.Length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Int32 offset, Int32 count) GetOffsetAndLength<ELEMENT_T>(this ELEMENT_T[] array, Range range, String parameterName)
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
        public static (Int32 offset, Int32 count) GetOffsetAndLength<ELEMENT_T>(this Span<ELEMENT_T> array, Range range, String parameterName)
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
        internal static (Int32 offset, Int32 count) GetOffsetAndLength<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array, Range range, String parameterName)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnly<ELEMENT_T>(this ELEMENT_T[] source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return new ReadOnlyMemory<ELEMENT_T>(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnly<ELEMENT_T>(this ELEMENT_T[] source, Int32 offset, Int32 count)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > source.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(source)}.");

            return new ReadOnlyMemory<ELEMENT_T>(source, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnly<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 length)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (checked(offset + length) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(length)}) is not within the {nameof(sourceArray)}.");

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnly<ELEMENT_T>(this Span<ELEMENT_T> sourceArray) => sourceArray;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnly<ELEMENT_T>(this Memory<ELEMENT_T> sourceArray) => sourceArray;

        #endregion

        #region AsMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<ELEMENT_T> AsMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset > (UInt32)sourceArray.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new Memory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)(sourceArray.Length - offset));
        }

#if false
        public static Memory<ELEMENT_T> AsMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 length)
        {
            throw new NotImplementedException(); // defined in System.MemoryExtensions.AsMemory<T>(this T[]? array, int start, int length)
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<ELEMENT_T> AsMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 length)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (checked(offset + length) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(length)}) is not within the {nameof(sourceArray)}.");

            return new Memory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)length);
        }

        #endregion

        #region AsSpan

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<ELEMENT_T> AsSpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset > (UInt32)sourceArray.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new Span<ELEMENT_T>(sourceArray, checked((Int32)offset), checked((Int32)((UInt32)sourceArray.Length - offset)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<ELEMENT_T> AsSpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (checked(offset + count) > sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");

            return new Span<ELEMENT_T>(sourceArray, checked((Int32)offset), checked((Int32)count));
        }

        #endregion

        #region AsReadOnlyMemory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (!offset.IsBetween(0, sourceArray.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, offset, sourceArray.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset > (UInt32)sourceArray.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)(sourceArray.Length - offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 length)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            if (checked(offset + length) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(length)}) is not within the {nameof(sourceArray)}.");

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, offset, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 length)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (checked(offset + length) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(length)}) is not within the {nameof(sourceArray)}.");

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)length);
        }

        #endregion

        #region AsReadOnlySpan

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            return (ReadOnlySpan<ELEMENT_T>)sourceArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (!offset.IsBetween(0, sourceArray.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new ReadOnlySpan<ELEMENT_T>(sourceArray, offset, sourceArray.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset > (UInt32)sourceArray.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new Span<ELEMENT_T>(sourceArray, (Int32)offset, sourceArray.Length - (Int32)offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            var (offset, count) = sourceArray.GetOffsetAndLength(range, nameof(range));
            return new ReadOnlySpan<ELEMENT_T>(sourceArray, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 count)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");

            return new ReadOnlySpan<ELEMENT_T>(sourceArray, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (checked(offset + count) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");

            return new Span<ELEMENT_T>(sourceArray, checked((Int32)offset), checked((Int32)count));
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// <see cref="ReadOnlySpan{T}"/> から値が一致する要素を検索します。
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
        /// <see cref="ReadOnlySpan{T}"/> から条件を満たす要素を検索します。
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
        /// <paramref name="buffer"/> 内に <paramref name="predicate"/> を満たす要素が見つかった場合は、最初に見つかった位置を示すインデックス番号が返ります。
        /// 条件を満たす要素が見つからなかった場合は負の整数が返ります。
        /// </returns>
        public static Int32 IndexOf<VALUE_T>(this Span<VALUE_T> buffer, Func<VALUE_T, Boolean> predicate)
        {
            for (var index = 0; index < buffer.Length; ++index)
            {
                if (predicate(buffer[index]))
                    return index;
            }

            return -1;
        }

        #endregion

        #region Slice

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (!offset.IsBetween(0, sourceArray.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new Memory<ELEMENT_T>(sourceArray, offset, sourceArray.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset > (UInt32)sourceArray.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new Memory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)(sourceArray.Length - offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            var (offset, length) = range.GetOffsetAndLength(sourceArray.Length);
            return new Memory<ELEMENT_T>(sourceArray, offset, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset, Int32 length)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            if (checked(offset + length) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(length)}) is not within the {nameof(sourceArray)}.");

            return new Memory<ELEMENT_T>(sourceArray, offset, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 length)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (checked(offset + length) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(length)}) is not within the {nameof(sourceArray)}.");

            return new Memory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<ELEMENT_T> Slice<ELEMENT_T>(this Span<ELEMENT_T> sourceArray, UInt32 offset)
            => sourceArray[(Int32)offset..];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<ELEMENT_T> Slice<ELEMENT_T>(this Span<ELEMENT_T> sourceArray, UInt32 offset, UInt32 length)
            => sourceArray.Slice(checked((Int32)offset), checked((Int32)length));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ELEMENT_T> Slice<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> sourceArray, UInt32 offset)
            => sourceArray[(Int32)offset..];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ELEMENT_T[] Duplicate<ELEMENT_T>(this ELEMENT_T[] sourceArray)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            var buffer = new ELEMENT_T[sourceArray.Length];
            sourceArray.CopyTo(buffer, 0);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Memory<ELEMENT_T> Duplicate<ELEMENT_T>(this Memory<ELEMENT_T> sourceArray)
        {
            var buffer = new ELEMENT_T[sourceArray.Length];
            sourceArray.Span.CopyTo(buffer);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<ELEMENT_T> Duplicate<ELEMENT_T>(this ReadOnlyMemory<ELEMENT_T> source)
        {
            var buffer = new ELEMENT_T[source.Length];
            source.Span.CopyTo(buffer);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<ELEMENT_T> Duplicate<ELEMENT_T>(this Span<ELEMENT_T> sourceArray)
        {
            var buffer = new ELEMENT_T[sourceArray.Length];
            sourceArray.CopyTo(buffer);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<ELEMENT_T> Duplicate<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> source)
        {
            var buffer = new ELEMENT_T[source.Length];
            source.CopyTo(buffer);
            return buffer;
        }

        #endregion

        #region ClearArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            Array.Clear(buffer, 0, buffer.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer, Int32 offset)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            Array.Clear(buffer, offset, buffer.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer, UInt32 offset)
            => buffer.ClearArray(checked((Int32)offset));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer, Range range)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            Array.Clear(buffer, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer, Int32 offset, Int32 count)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            Array.Clear(buffer, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer, UInt32 offset, UInt32 count)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.ClearArray(checked((Int32)offset), checked((Int32)count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearArray<ELEMENT_T>(this Span<ELEMENT_T> buffer) => buffer.Clear();

        #endregion

        #region FillArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            Array.Fill(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, Int32 offset)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            Array.Fill(buffer, value, offset, buffer.Length - offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, UInt32 offset)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.FillArray(value, checked((Int32)offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, Range range)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            Array.Fill(buffer, value, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, Int32 offset, Int32 count)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            Array.Fill(buffer, value, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, UInt32 offset, UInt32 count)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.FillArray(value, checked((Int32)offset), checked((Int32)count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FillArray<ELEMENT_T>(this Span<ELEMENT_T> buffer, ELEMENT_T value)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
            => buffer.Fill(value);

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<Int32, ELEMENT_T> valueGetter)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (valueGetter is null)
                throw new ArgumentNullException(nameof(valueGetter));

            var count = buffer.Length;
            for (var index = 0; index < count; ++index)
                buffer[index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<Int32, ELEMENT_T> valueGetter, Int32 offset)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (valueGetter is null)
                throw new ArgumentNullException(nameof(valueGetter));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            var count = buffer.Length - offset;
            for (var index = 0; index < count; ++index)
                buffer[offset + index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<Int32, ELEMENT_T> valueGetter, Range range)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (valueGetter is null)
                throw new ArgumentNullException(nameof(valueGetter));
            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));

            for (var index = 0; index < count; ++index)
                buffer[offset + index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<Int32, ELEMENT_T> valueGetter, Int32 offset, Int32 count)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (valueGetter is null)
                throw new ArgumentNullException(nameof(valueGetter));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            for (var index = 0; index < count; ++index)
                buffer[offset + index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<UInt32, ELEMENT_T> valueGetter, UInt32 offset)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (valueGetter is null)
                throw new ArgumentNullException(nameof(valueGetter));
            if (offset > (UInt32)buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var count = (UInt32)buffer.Length - offset;
            for (var index = 0U; index < count; ++index)
                buffer[offset + index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, Func<UInt32, ELEMENT_T> valueGetter, UInt32 offset, UInt32 count)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (valueGetter is null)
                throw new ArgumentNullException(nameof(valueGetter));
            if (checked(offset + count) > (UInt32)buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            for (var index = 0U; index < count; ++index)
                buffer[offset + index] = valueGetter(index);
        }

        public static void FillArray<ELEMENT_T>(this Span<ELEMENT_T> buffer, Func<Int32, ELEMENT_T> valueGetter)
        {
            if (valueGetter is null)
                throw new ArgumentNullException(nameof(valueGetter));

            var count = buffer.Length;
            for (var index = 0; index < count; ++index)
                buffer[index] = valueGetter(index);
        }

        #endregion

        #region CopyTo

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, ELEMENT_T[] destinationArray, UInt32 destinationArrayOffset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (destinationArray is null)
                throw new ArgumentNullException(nameof(destinationArray));
            if (checked(destinationArrayOffset + (UInt32)sourceArray.Length) > (UInt32)destinationArray.Length)
                throw new ArgumentException("There is not enough space for the copy destination.");

            sourceArray.CopyTo(destinationArray, (Int32)destinationArrayOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 sourceArrayOffset, ELEMENT_T[] destinationArray, Int32 destinationArrayOffset, Int32 count)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (sourceArrayOffset < 0)
                throw new ArgumentOutOfRangeException(nameof(sourceArrayOffset));
            if (destinationArray is null)
                throw new ArgumentNullException(nameof(destinationArray));
            if (destinationArrayOffset < 0)
                throw new ArgumentOutOfRangeException(nameof(destinationArrayOffset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(sourceArrayOffset + count) > sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(sourceArrayOffset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");
            if (checked(destinationArrayOffset + count) > destinationArray.Length)
                throw new ArgumentException($"The specified range ({nameof(destinationArrayOffset)} and {nameof(count)}) is not within the {nameof(destinationArray)}.");

            Array.Copy(sourceArray, sourceArrayOffset, destinationArray, destinationArrayOffset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 sourceArrayOffset, ELEMENT_T[] destinationArray, UInt32 destinationArrayOffset, UInt32 count)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (destinationArray is null)
                throw new ArgumentNullException(nameof(destinationArray));
            if (checked(sourceArrayOffset + count) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(sourceArrayOffset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");
            if (checked(destinationArrayOffset + count) > (UInt32)destinationArray.Length)
                throw new ArgumentException($"The specified range ({nameof(destinationArrayOffset)} and {nameof(count)}) is not within the {nameof(destinationArray)}.");

            Array.Copy(sourceArray, (Int32)sourceArrayOffset, destinationArray, (Int32)destinationArrayOffset, (Int32)count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, Span<ELEMENT_T> destinationArray)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            ((Span<ELEMENT_T>)sourceArray).CopyTo(destinationArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<ELEMENT_T>(this Span<ELEMENT_T> sourceArray, ELEMENT_T[] destinationArray)
        {
            if (destinationArray is null)
                throw new ArgumentNullException(nameof(destinationArray));

            sourceArray.CopyTo((Span<ELEMENT_T>)destinationArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> sourceArray, ELEMENT_T[] destinationArray)
            => sourceArray.CopyTo((Span<ELEMENT_T>)destinationArray);

        #endregion

        #region ToDictionary

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this ELEMENT_T[] source, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter);
        }

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this ELEMENT_T[] source, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, keyEqualityComparer);
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this ELEMENT_T[] source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (valueSelecter is null)
                throw new ArgumentNullException(nameof(valueSelecter));

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, valueSelecter);
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this ELEMENT_T[] source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (valueSelecter is null)
                throw new ArgumentNullException(nameof(valueSelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, valueSelecter, keyEqualityComparer);
        }

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter);
        }

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, keyEqualityComparer);
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (valueSelecter is null)
                throw new ArgumentNullException(nameof(valueSelecter));

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, valueSelecter);
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (valueSelecter is null)
                throw new ArgumentNullException(nameof(valueSelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            return ((ReadOnlySpan<ELEMENT_T>)source).ToDictionary(keySelecter, valueSelecter, keyEqualityComparer);
        }

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            var dictionary = new Dictionary<KEY_T, ELEMENT_T>();
            foreach (var element in source)
                dictionary.Add(keySelecter(element), element);
            return dictionary;
        }

        public static IDictionary<KEY_T, ELEMENT_T> ToDictionary<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            var dictionary = new Dictionary<KEY_T, ELEMENT_T>(keyEqualityComparer);
            foreach (var element in source)
                dictionary.Add(keySelecter(element), element);
            return dictionary;
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (valueSelecter is null)
                throw new ArgumentNullException(nameof(valueSelecter));

            var dictionary = new Dictionary<KEY_T, VALUE_T>();
            foreach (var element in source)
                dictionary.Add(keySelecter(element), valueSelecter(element));
            return dictionary;
        }

        public static IDictionary<KEY_T, VALUE_T> ToDictionary<ELEMENT_T, KEY_T, VALUE_T>(this ReadOnlySpan<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySelecter, Func<ELEMENT_T, VALUE_T> valueSelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
            where KEY_T : IEquatable<KEY_T>
        {
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (valueSelecter is null)
                throw new ArgumentNullException(nameof(valueSelecter));
            if (keyEqualityComparer is null)
                throw new ArgumentNullException(nameof(keyEqualityComparer));

            var dictionary = new Dictionary<KEY_T, VALUE_T>(keyEqualityComparer);
            foreach (var element in source)
                dictionary.Add(keySelecter(element), valueSelecter(element));
            return dictionary;
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean DefaultEqual<ELEMENT_T>([AllowNull] ELEMENT_T key1, [AllowNull] ELEMENT_T key2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
            => key1 is null
                ? key2 is null
                : key1.Equals(key2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 DefaultCompare<ELEMENT_T>([AllowNull] ELEMENT_T key1, [AllowNull] ELEMENT_T key2)
            where ELEMENT_T : IComparable<ELEMENT_T>
            => key1 is not null
                ? key1.CompareTo(key2)
                : key2 is null
                ? 0
                : -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Assert(Boolean condition)
        {
#if DEBUG
            if (!condition)
                throw new Exception();
#endif
        }
    }
}
