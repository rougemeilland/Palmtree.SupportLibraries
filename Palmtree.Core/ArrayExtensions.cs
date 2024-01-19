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

        private const Int32 _THRESHOLD_ARRAY_EQUAL_BY_LONG_POINTER = 32;
        private const Int32 _THRESHOLD_COPY_MEMORY_BY_LONG_POINTER = 14;

        private readonly static Boolean _is64bitProcess;
        private readonly static Int32 _alignment;
        private readonly static Int32 _alignmentMask;

        static ArrayExtensions()
        {
            _is64bitProcess = Environment.Is64BitProcess;
            _alignment = _is64bitProcess ? sizeof(UInt64) : sizeof(UInt32);
            _alignmentMask = _alignment - 1;
        }

        #region GetOffsetAndLength

        public static (Boolean IsOk, Int32 Offset, Int32 Length) GetOffsetAndLength<ELEMENT_T>(this ELEMENT_T[] source, Range range)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            try
            {
                var (offset, count) = range.GetOffsetAndLength(source.Length);
                return (true, offset, count);
            }
            catch (ArgumentOutOfRangeException)
            {
                return (false, 0, 0);
            }
        }

        public static (Boolean IsOk, Int32 Offset, Int32 Length) GetOffsetAndLength<ELEMENT_T>(this Span<ELEMENT_T> source, Range range)
        {
            try
            {
                var (offset, count) = range.GetOffsetAndLength(source.Length);
                return (true, offset, count);
            }
            catch (ArgumentOutOfRangeException)
            {
                return (false, 0, 0);
            }
        }

        public static (Boolean IsOk, Int32 Offset, Int32 Length) GetOffsetAndLength<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> source, Range range)
        {
            try
            {
                var (offset, count) = range.GetOffsetAndLength(source.Length);
                return (true, offset, count);
            }
            catch (ArgumentOutOfRangeException)
            {
                return (false, 0, 0);
            }
        }

        #endregion

        #region AsReadOnly

        public static ReadOnlyMemory<ELEMENT_T> AsReadOnly<ELEMENT_T>(this ELEMENT_T[] source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return new ReadOnlyMemory<ELEMENT_T>(source);
        }

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

        public static Span<ELEMENT_T> AsSpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset > (UInt32)sourceArray.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new Span<ELEMENT_T>(sourceArray, checked((Int32)offset), checked((Int32)((UInt32)sourceArray.Length - offset)));
        }

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

        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray);
        }

        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (!offset.IsBetween(0, sourceArray.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, offset, sourceArray.Length - offset);
        }

        public static ReadOnlyMemory<ELEMENT_T> AsReadOnlyMemory<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset > (UInt32)sourceArray.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new ReadOnlyMemory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)(sourceArray.Length - offset));
        }

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

        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            return (ReadOnlySpan<ELEMENT_T>)sourceArray;
        }

        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (!offset.IsBetween(0, sourceArray.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new ReadOnlySpan<ELEMENT_T>(sourceArray, offset, sourceArray.Length - offset);
        }

        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset > (UInt32)sourceArray.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new Span<ELEMENT_T>(sourceArray, (Int32)offset, sourceArray.Length - (Int32)offset);
        }

        public static ReadOnlySpan<ELEMENT_T> AsReadOnlySpan<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            var (isOk, offset, count) = sourceArray.GetOffsetAndLength(range);
            if (!isOk)
                throw new ArgumentOutOfRangeException(nameof(range));

            return new ReadOnlySpan<ELEMENT_T>(sourceArray, offset, count);
        }

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

        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (!offset.IsBetween(0, sourceArray.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new Memory<ELEMENT_T>(sourceArray, offset, sourceArray.Length - offset);
        }

        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (offset > (UInt32)sourceArray.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return new Memory<ELEMENT_T>(sourceArray, (Int32)offset, (Int32)(sourceArray.Length - offset));
        }

        public static Memory<ELEMENT_T> Slice<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            Int32 offset;
            Int32 length;
            try
            {
                (offset, length) = range.GetOffsetAndLength(sourceArray.Length);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException(nameof(range));
            }

            return new Memory<ELEMENT_T>(sourceArray, offset, length);
        }

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

        #region QuickSort

        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            InternalQuickSort(sourceArray, 0, sourceArray.Length);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Func<ELEMENT_T, KEY_T> keySekecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));

            InternalQuickSort(sourceArray, 0, sourceArray.Length, keySekecter);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, IComparer<ELEMENT_T> comparer)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            InternalQuickSort(sourceArray, 0, sourceArray.Length, comparer);
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

            InternalQuickSort(sourceArray, 0, sourceArray.Length, keySekecter, keyComparer);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            var (isOk, offset, count) = sourceArray.GetOffsetAndLength(range);
            if (!isOk)
                throw new ArgumentOutOfRangeException(nameof(range));

            InternalQuickSort(sourceArray, offset, count);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T, KEY_T>(this ELEMENT_T[] sourceArray, Range range, Func<ELEMENT_T, KEY_T> keySekecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (keySekecter is null)
                throw new ArgumentNullException(nameof(keySekecter));
            var (isOk, offset, count) = sourceArray.GetOffsetAndLength(range);
            if (!isOk)
                throw new ArgumentOutOfRangeException(nameof(range));

            InternalQuickSort(sourceArray, offset, count, keySekecter);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, Range range, IComparer<ELEMENT_T> comparer)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));
            var (isOk, offset, count) = sourceArray.GetOffsetAndLength(range);
            if (!isOk)
                throw new ArgumentOutOfRangeException(nameof(range));

            InternalQuickSort(sourceArray, offset, count, comparer);
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
            var (isOk, offset, count) = sourceArray.GetOffsetAndLength(range);
            if (!isOk)
                throw new ArgumentOutOfRangeException(nameof(range));

            InternalQuickSort(sourceArray, offset, count, keySekecter, keyComparer);
            return sourceArray;
        }

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

            InternalQuickSort(sourceArray, offset, count);
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

            InternalQuickSort(sourceArray, offset, count, keySekecter);
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

            InternalQuickSort(sourceArray, offset, count, comparer);
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

            InternalQuickSort(sourceArray, offset, count, keySekecter, keyComparer);
            return sourceArray;
        }

        public static ELEMENT_T[] QuickSort<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 offset, UInt32 count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (checked(offset + count) > (UInt32)sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");

            InternalQuickSort(sourceArray, checked((Int32)offset), checked((Int32)count));
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

            InternalQuickSort(sourceArray, checked((Int32)offset), checked((Int32)count), keySekecter);
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

            InternalQuickSort(sourceArray, (Int32)offset, checked((Int32)count), comparer);
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

            InternalQuickSort(sourceArray, checked((Int32)offset), checked((Int32)offset), keySekecter, keyComparer);
            return sourceArray;
        }

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

        #region SequenceEqual

        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));

            return
                InternalSequenceEqual(
                    array1,
                    0,
                    array1.Length,
                    array2,
                    0,
                    array2.Length);
        }

        public static Boolean SequenceEqual<ELEMENT_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            if (equalityComparer is null)
                throw new ArgumentNullException(nameof(equalityComparer));

            return
                InternalSequenceEqual(
                    array1,
                    0,
                    array1.Length,
                    array2,
                    0,
                    array2.Length,
                    equalityComparer);
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

            return
                InternalSequenceEqual(
                    array1,
                    0,
                    array1.Length,
                    array2,
                    0,
                    array2.Length,
                    keySelecter);
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

            return
                InternalSequenceEqual(
                    array1,
                    0,
                    array1.Length,
                    array2,
                    0,
                    array2.Length,
                    keySelecter,
                    keyEqualityComparer);
        }

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

            return
                InternalSequenceEqual(
                    array1,
                    array1Offset,
                    count,
                    array2,
                    array2Offset,
                    count);
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

            return
                InternalSequenceEqual(
                    array1,
                    array1Offset,
                    count,
                    array2,
                    array2Offset,
                    count,
                    equalityComparer);
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

            return
                InternalSequenceEqual(
                    array1,
                    array1Offset,
                    count,
                    array2,
                    array2Offset,
                    count,
                    keySelecter);
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

            return
                InternalSequenceEqual(
                    array1,
                    array1Offset,
                    count,
                    array2,
                    array2Offset,
                    count,
                    keySelecter,
                    keyEqualityComparer);
        }

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
                InternalSequenceEqual(
                    array1,
                    (Int32)array1Offset,
                    (Int32)count,
                    array2,
                    (Int32)array2Offset,
                    (Int32)count);
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
                InternalSequenceEqual(
                    array1,
                    (Int32)array1Offset,
                    (Int32)count,
                    array2,
                    (Int32)array2Offset,
                    (Int32)count,
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
                InternalSequenceEqual(
                    array1,
                    (Int32)array1Offset,
                    (Int32)count,
                    array2,
                    (Int32)array2Offset,
                    (Int32)count,
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
                InternalSequenceEqual(
                    array1,
                    (Int32)array1Offset,
                    (Int32)count,
                    array2,
                    (Int32)array2Offset,
                    (Int32)count,
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

        #region SequenceCompare

        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));

            return
                InternalSequenceCompare(
                    array1,
                    0,
                    array1.Length,
                    array2,
                    0,
                    array2.Length);
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
                    array1,
                    0,
                    array1.Length,
                    array2,
                    0,
                    array2.Length,
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
                    array1,
                    0,
                    array1.Length,
                    array2,
                    0,
                    array2.Length,
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
                    array1,
                    0,
                    array1.Length,
                    array2,
                    0,
                    array2.Length,
                    keySelecter,
                    keyComparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, Range array1Range, ELEMENT_T[] array2, Range array2Range)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            var (isOk1, array1Offset, array1Count) = array1.GetOffsetAndLength(array1Range);
            if (!isOk1)
                throw new ArgumentOutOfRangeException(nameof(array1Range));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            var (isOk2, array2Offset, array2Count) = array1.GetOffsetAndLength(array2Range);
            if (!isOk2)
                throw new ArgumentOutOfRangeException(nameof(array2Range));

            return
                InternalSequenceCompare(
                    array1,
                    array1Offset,
                    array1Count,
                    array2,
                    array2Offset,
                    array2Count);
        }

        public static Int32 SequenceCompare<ELEMENT_T>(this ELEMENT_T[] array1, Range array1Range, ELEMENT_T[] array2, Range array2Range, IComparer<ELEMENT_T> comparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            var (isOk1, array1Offset, array1Count) = array1.GetOffsetAndLength(array1Range);
            if (!isOk1)
                throw new ArgumentOutOfRangeException(nameof(array1Range));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            var (isOk2, array2Offset, array2Count) = array1.GetOffsetAndLength(array2Range);
            if (!isOk2)
                throw new ArgumentOutOfRangeException(nameof(array2Range));
            if (comparer is null)
                throw new ArgumentNullException(nameof(comparer));

            return
                InternalSequenceCompare(
                    array1,
                    array1Offset,
                    array1Count,
                    array2,
                    array2Offset,
                    array2Count,
                    comparer);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Range array1Range, ELEMENT_T[] array2, Range array2Range, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            var (isOk1, array1Offset, array1Count) = array1.GetOffsetAndLength(array1Range);
            if (!isOk1)
                throw new ArgumentOutOfRangeException(nameof(array1Range));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            var (isOk2, array2Offset, array2Count) = array1.GetOffsetAndLength(array2Range);
            if (!isOk2)
                throw new ArgumentOutOfRangeException(nameof(array2Range));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));

            return
                InternalSequenceCompare(
                    array1,
                    array1Offset,
                    array1Count,
                    array2,
                    array2Offset,
                    array2Count,
                    keySelecter);
        }

        public static Int32 SequenceCompare<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, Range array1Range, ELEMENT_T[] array2, Range array2Range, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            if (array1 is null)
                throw new ArgumentNullException(nameof(array1));
            var (isOk1, array1Offset, array1Count) = array1.GetOffsetAndLength(array1Range);
            if (!isOk1)
                throw new ArgumentOutOfRangeException(nameof(array1Range));
            if (array2 is null)
                throw new ArgumentNullException(nameof(array2));
            var (isOk2, array2Offset, array2Count) = array1.GetOffsetAndLength(array2Range);
            if (!isOk2)
                throw new ArgumentOutOfRangeException(nameof(array2Range));
            if (keySelecter is null)
                throw new ArgumentNullException(nameof(keySelecter));
            if (keyComparer is null)
                throw new ArgumentNullException(nameof(keyComparer));

            return
                InternalSequenceCompare(
                    array1,
                    array1Offset,
                    array1Count,
                    array2,
                    array2Offset,
                    array2Count,
                    keySelecter,
                    keyComparer);
        }

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
                    array1,
                    array1Offset,
                    array1Count,
                    array2,
                    array2Offset,
                    array2Count);
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
                    array1,
                    array1Offset,
                    array1Count,
                    array2,
                    array2Offset,
                    array2Count,
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
                    array1,
                    array1Offset,
                    array1Count,
                    array2,
                    array2Offset,
                    array2Count,
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
                    array1,
                    array1Offset,
                    array1Count,
                    array2,
                    array2Offset,
                    array2Count,
                    keySelecter,
                    keyComparer);
        }

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
                    array1,
                    (Int32)array1Offset,
                    (Int32)array1Count,
                    array2,
                    (Int32)array2Offset,
                    (Int32)array2Count);
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
                    array1,
                    (Int32)array1Offset,
                    (Int32)array1Count,
                    array2,
                    (Int32)array2Offset,
                    (Int32)array2Count,
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
                    array1,
                    (Int32)array1Offset,
                    (Int32)array1Count,
                    array2,
                    (Int32)array2Offset,
                    (Int32)array2Count,
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
                    array1,
                    (Int32)array1Offset,
                    (Int32)array1Count,
                    array2,
                    (Int32)array2Offset,
                    (Int32)array2Count,
                    keySelecter,
                    keyComparer);
        }

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

        #region Duplicate

        public static ELEMENT_T[] Duplicate<ELEMENT_T>(this ELEMENT_T[] sourceArray)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            var buffer = new ELEMENT_T[sourceArray.Length];
            sourceArray.CopyTo(buffer, 0);
            return buffer;
        }

        public static Memory<ELEMENT_T> Duplicate<ELEMENT_T>(this Memory<ELEMENT_T> sourceArray)
        {
            var buffer = new ELEMENT_T[sourceArray.Length];
            sourceArray.Span.CopyTo(buffer);
            return buffer;
        }

        public static ReadOnlyMemory<ELEMENT_T> Duplicate<ELEMENT_T>(this ReadOnlyMemory<ELEMENT_T> source)
        {
            var buffer = new ELEMENT_T[source.Length];
            source.Span.CopyTo(buffer);
            return buffer;
        }

        public static Span<ELEMENT_T> Duplicate<ELEMENT_T>(this Span<ELEMENT_T> sourceArray)
        {
            var buffer = new ELEMENT_T[sourceArray.Length];
            sourceArray.CopyTo(buffer);
            return buffer;
        }

        public static ReadOnlySpan<ELEMENT_T> Duplicate<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> source)
        {
            var buffer = new ELEMENT_T[source.Length];
            source.CopyTo(buffer);
            return buffer;
        }

        #endregion

        #region ClearArray

        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            Array.Clear(buffer, 0, buffer.Length);
        }

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

        public static void ClearArray<ELEMENT_T>(this ELEMENT_T[] buffer, Range range)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            var (isOk, offset, count) = buffer.GetOffsetAndLength(range);
            if (!isOk)
                throw new ArgumentOutOfRangeException(nameof(range));

            Array.Clear(buffer, offset, count);
        }

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

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            Array.Fill(buffer, value);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, Int32 offset)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            Array.Fill(buffer, value, offset, buffer.Length - offset);
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, UInt32 offset)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.FillArray(value, checked((Int32)offset));
        }

        public static void FillArray<ELEMENT_T>(this ELEMENT_T[] buffer, ELEMENT_T value, Range range)
            where ELEMENT_T : struct // もし ELEMENT_T が参照型だと同じ参照がすべての要素にコピーされバグの原因となりやすいため、値型に限定する
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            var (isOk, offset, count) = buffer.GetOffsetAndLength(range);
            if (!isOk)
                throw new ArgumentOutOfRangeException(nameof(range));

            Array.Fill(buffer, value, offset, count);
        }

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
            var (isOk, offset, count) = buffer.GetOffsetAndLength(range);
            if (!isOk)
                throw new ArgumentOutOfRangeException(nameof(range));

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

        public static void CopyTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, Span<ELEMENT_T> destinationArray)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));

            ((Span<ELEMENT_T>)sourceArray).CopyTo(destinationArray);
        }

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

        #region CopyMemoryTo

        public static void CopyMemoryTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, ELEMENT_T[] destinationArray)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (destinationArray is null)
                throw new ArgumentNullException(nameof(destinationArray));
            if (sourceArray.Length > destinationArray.Length)
                throw new ArgumentException("There is not enough space for the copy destination.");

            InternalCopyMemory(sourceArray, 0, destinationArray, 0, sourceArray.Length);
        }

        public static void CopyMemoryTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, ELEMENT_T[] destinationArray, Int32 destinationArrayOffset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (destinationArray is null)
                throw new ArgumentNullException(nameof(destinationArray));
            if (destinationArrayOffset < 0)
                throw new ArgumentOutOfRangeException(nameof(destinationArrayOffset));
            if (checked(destinationArrayOffset + sourceArray.Length) > destinationArray.Length)
                throw new ArgumentException("There is not enough space for the copy destination.");

            InternalCopyMemory(sourceArray, 0, destinationArray, destinationArrayOffset, sourceArray.Length);
        }

        public static void CopyMemoryTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, ELEMENT_T[] destinationArray, UInt32 destinationArrayOffset)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (destinationArray is null)
                throw new ArgumentNullException(nameof(destinationArray));
            if (checked(destinationArrayOffset + (UInt32)sourceArray.Length) > (UInt32)destinationArray.Length)
                throw new ArgumentException("There is not enough space for the copy destination.");

            InternalCopyMemory(sourceArray, 0, destinationArray, (Int32)destinationArrayOffset, sourceArray.Length);
        }

        public static void CopyMemoryTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, Int32 sourceArrayOffset, ELEMENT_T[] destinationArray, Int32 destinationArrayOffset, Int32 count)
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

            InternalCopyMemory(sourceArray, sourceArrayOffset, destinationArray, destinationArrayOffset, count);
        }

        public static void CopyMemoryTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, UInt32 sourceArrayOffset, ELEMENT_T[] destinationArray, UInt32 destinationArrayOffset, UInt32 count)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (destinationArray is null)
                throw new ArgumentNullException(nameof(destinationArray));
            if (checked(sourceArrayOffset + count) > sourceArray.Length)
                throw new ArgumentException($"The specified range ({nameof(sourceArrayOffset)} and {nameof(count)}) is not within the {nameof(sourceArray)}.");
            if (checked(destinationArrayOffset + count) > destinationArray.Length)
                throw new ArgumentException($"The specified range ({nameof(destinationArrayOffset)} and {nameof(count)}) is not within the {nameof(destinationArray)}.");

            InternalCopyMemory(sourceArray, (Int32)sourceArrayOffset, destinationArray, (Int32)destinationArrayOffset, (Int32)count);
        }

        public static void CopyMemoryTo<ELEMENT_T>(this ELEMENT_T[] sourceArray, Span<ELEMENT_T> destinationArray)
        {
            if (sourceArray is null)
                throw new ArgumentNullException(nameof(sourceArray));
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException($"{nameof(destinationArray)} is shorter than {nameof(sourceArray)}");

            InternalCopyMemory((ReadOnlySpan<ELEMENT_T>)sourceArray, destinationArray);
        }

        public static void CopyMemoryTo<ELEMENT_T>(this Span<ELEMENT_T> sourceArray, ELEMENT_T[] destinationArray)
        {
            if (destinationArray is null)
                throw new ArgumentNullException(nameof(destinationArray));
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException($"{nameof(destinationArray)} is shorter than {nameof(sourceArray)}");

            InternalCopyMemory((ReadOnlySpan<ELEMENT_T>)sourceArray, (Span<ELEMENT_T>)destinationArray);
        }

        public static void CopyMemoryTo<ELEMENT_T>(this Span<ELEMENT_T> sourceArray, Span<ELEMENT_T> destinationArray)
        {
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException($"{nameof(destinationArray)} is shorter than {nameof(sourceArray)}");

            InternalCopyMemory((ReadOnlySpan<ELEMENT_T>)sourceArray, destinationArray);
        }

        public static void CopyMemoryTo<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> sourceArray, ELEMENT_T[] destinationArray)
        {
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException($"{nameof(destinationArray)} is shorter than {nameof(sourceArray)}");

            InternalCopyMemory(sourceArray, (Span<ELEMENT_T>)destinationArray);
        }

        public static void CopyMemoryTo<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> sourceArray, Span<ELEMENT_T> destinationArray)
        {
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException($"{nameof(destinationArray)} is shorter than {nameof(sourceArray)}");

            InternalCopyMemory(sourceArray, destinationArray);
        }

        #endregion

        #region ReverseArray

        /// <summary>
        /// 与えられた配列の要素を逆順に並べ替えます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 配列の要素の型です。
        /// </typeparam>
        /// <param name="source">
        /// 並び替える配列です。
        /// </param>
        /// <returns>
        /// 並び替えられた配列です。この配列は <paramref name="source"/> と同じ参照です。
        /// </returns>
        /// <remarks>
        /// このメソッドは<paramref name="source"/> で与えられた配列の内容を変更します。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> が nullです。
        /// </exception>
        public static ELEMENT_T[] ReverseArray<ELEMENT_T>(this ELEMENT_T[] source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            InternalReverseArray(source, 0, source.Length);
            return source;
        }

        /// <summary>
        /// 与えられた配列の指定された範囲の要素を逆順に並べ替えます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 配列の要素の型です。
        /// </typeparam>
        /// <param name="source">
        /// 並び替える配列です。
        /// </param>
        /// <param name="offset">
        /// 並び替える範囲の開始位置です。
        /// </param>
        /// <param name="count">
        /// 並び替える範囲の長さです。
        /// </param>
        /// <returns>
        /// 並び替えられた配列です。この配列は <paramref name="source"/> と同じ参照です。
        /// </returns>
        /// <remarks>
        /// このメソッドは<paramref name="source"/> で与えられた配列の内容を変更します。
        /// </remarks>
        /// <paramref name="source"/> が nullです。
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="offset"/> または <paramref name="count"/> が負の値です。
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="offset"/> および <paramref name="count"/> で指定された範囲が <paramref name="source"/> の範囲外です。
        /// </exception>
        public static ELEMENT_T[] ReverseArray<ELEMENT_T>(this ELEMENT_T[] source, Int32 offset, Int32 count)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > source.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(source)}.");

            InternalReverseArray(source, offset, count);
            return source;
        }

        /// <summary>
        /// 与えられた配列の要素を逆順に並べ替えます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 配列の要素の型です。
        /// </typeparam>
        /// <param name="source">
        /// 並び替える配列です。
        /// </param>
        /// <returns>
        /// 並び替えられた配列です。この配列は <paramref name="source"/> と同じ参照です。
        /// </returns>
        /// <remarks>
        /// このメソッドは<paramref name="source"/> で与えられた配列の内容を変更します。
        /// </remarks>
        public static Memory<ELEMENT_T> ReverseArray<ELEMENT_T>(this Memory<ELEMENT_T> source)
        {
            InternalReverseArray(source.Span);
            return source;
        }

        /// <summary>
        /// 与えられた配列の要素を逆順に並べ替えます。
        /// </summary>
        /// <typeparam name="ELEMENT_T">
        /// 配列の要素の型です。
        /// </typeparam>
        /// <param name="source">
        /// 並び替える配列です。
        /// </param>
        /// <returns>
        /// 並び替えられた配列です。この配列は <paramref name="source"/> と同じ参照です。
        /// </returns>
        /// <remarks>
        /// このメソッドは<paramref name="source"/> で与えられた配列の内容を変更します。
        /// </remarks>
        public static Span<ELEMENT_T> ReverseArray<ELEMENT_T>(this Span<ELEMENT_T> source)
        {
            InternalReverseArray(source);
            return source;
        }

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

        #region InternalSequenceEqual

        private static Boolean InternalSequenceEqual<ELEMENT_T, KEY_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IEquatable<KEY_T>
        {
            if (array1Length != array2Length)
                return false;
            for (var index = 0; index < array1Length; index++)
            {
                var key1 = keySelecter(array1[array1Offset + index]);
                var key2 = keySelecter(array2[array2Offset + index]);
                if (!DefaultEqual(key1, key2))
                    return false;
            }

            return true;
        }

        private static Boolean InternalSequenceEqual<ELEMENT_T, KEY_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, Func<ELEMENT_T, KEY_T> keySelecter, IEqualityComparer<KEY_T> keyEqualityComparer)
        {
            if (array1Length != array2Length)
                return false;
            for (var index = 0; index < array1Length; index++)
            {
                if (!keyEqualityComparer.Equals(keySelecter(array1[array1Offset + index]), keySelecter(array2[array2Offset + index])))
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

        private static Boolean InternalSequenceEqualManaged<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (array1Length != array2Length)
                return false;

            for (var index = 0; index < array1Length; index++)
            {
                if (!DefaultEqual(array1[array1Offset + index], array2[array2Offset + index]))
                    return false;
            }

            return true;
        }

        private static Boolean InternalSequenceEqualManaged<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (array1Length != array2Length)
                return false;

            for (var index = 0; index < array1Length; index++)
            {
                if (!equalityComparer.Equals(array1[array1Offset + index], array2[array2Offset + index]))
                    return false;
            }

            return true;
        }

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

        private static Boolean InternalSequenceEqualManaged<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (array1.Length != array2.Length)
                return false;

            var count = array1.Length;
            for (var index = 0; index < count; index++)
            {
                if (!equalityComparer.Equals(array1[index], array2[index]))
                    return false;
            }

            return true;
        }

        #endregion

        #region InternalSequenceEqualUnmanaged

        private static unsafe Boolean InternalSequenceEqualUnmanaged<ELEMENT_T>(ref ELEMENT_T array1, Int32 array1Length, ref ELEMENT_T array2, Int32 array2Length)
            where ELEMENT_T : unmanaged
        {
            if (array1Length != array2Length)
                return false;

            if (array1Length <= 0)
                return true;

            fixed (ELEMENT_T* pointer1 = &array1)
            fixed (ELEMENT_T* pointer2 = &array2)
            {
                if (pointer1 == pointer2)
                    return true;

                return
                    array1Length < _THRESHOLD_ARRAY_EQUAL_BY_LONG_POINTER
                    ? InternalSequenceEqualUnmanagedByByte((Byte*)pointer1, (Byte*)pointer2, array1Length * sizeof(ELEMENT_T))
                    : _is64bitProcess
                    ? InternalSequenceEqualUnmanagedByUInt64((Byte*)pointer1, (Byte*)pointer2, array1Length * sizeof(ELEMENT_T))
                    : InternalSequenceEqualUnmanagedByUInt32((Byte*)pointer1, (Byte*)pointer2, array1Length * sizeof(ELEMENT_T));
            }
        }

        private static unsafe Boolean InternalSequenceEqualUnmanaged<ELEMENT_T>(ref ELEMENT_T array1, Int32 array1Length, ref ELEMENT_T array2, Int32 array2Length, IEqualityComparer<ELEMENT_T> equalityComparer)
            where ELEMENT_T : unmanaged
        {
            if (array1Length != array2Length)
                return false;

            if (array1Length <= 0)
                return true;

            fixed (ELEMENT_T* buffer1 = &array1)
            fixed (ELEMENT_T* buffer2 = &array2)
            {
                if (buffer1 == buffer2)
                    return true;

                var count = array1Length;
                var pointer1 = buffer1;
                var pointer2 = buffer2;
                while (count-- > 0)
                {
                    if (!equalityComparer.Equals(*pointer1++, *pointer2++))
                        return false;
                }

                return true;
            }
        }

        #endregion

        private static unsafe Boolean InternalSequenceEqualUnmanagedByUInt32(Byte* pointer1, Byte* pointer2, Int32 count)
        {
            const Int32 alignmentMask = sizeof(UInt32) - 1;

            // 先行してバイト単位で比較する長さを計算する
            {
                var offset = (Int32)pointer1 & alignmentMask;
                var preCount = (-offset & alignmentMask).Minimum(count);
                var __count = preCount;
                while (__count-- > 0)
                {
                    if (*pointer1++ != *pointer2++)
                        return false;
                }

                count -= preCount;
            }

            // この時点で pointer1 が sizeof(UInt32) バイトバウンダリ、または count == 0 のはず。
#if DEBUG
            Assert((UInt32)pointer1 % sizeof(UInt32) == 0 || count == 0);
#endif

            var longPointer1 = (UInt32*)pointer1;
            var longpointer2 = (UInt32*)pointer2;

            while (count >= 8 * sizeof(UInt32))
            {
                if (longPointer1[0] != longpointer2[0]
                    || longPointer1[1] != longpointer2[1]
                    || longPointer1[2] != longpointer2[2]
                    || longPointer1[3] != longpointer2[3]
                    || longPointer1[4] != longpointer2[4]
                    || longPointer1[5] != longpointer2[5]
                    || longPointer1[6] != longpointer2[6]
                    || longPointer1[7] != longpointer2[7])
                {
                    return false;
                }

                count -= 8 * sizeof(UInt32);
                longPointer1 += 8;
                longpointer2 += 8;
            }
#if DEBUG
            Assert((count & ~((1 << 5) - 1)) == 0);
#endif
            if ((count & (1 << 4)) != 0)
            {
                if (longPointer1[0] != longpointer2[0]
                    || longPointer1[1] != longpointer2[1]
                    || longPointer1[2] != longpointer2[2]
                    || longPointer1[3] != longpointer2[3])
                {
                    return false;
                }
#if DEBUG
                count &= ~(1 << 4);
#endif
                longPointer1 += 4;
                longpointer2 += 4;
            }

            if ((count & (1 << 3)) != 0)
            {
                if (longPointer1[0] != longpointer2[0]
                    || longPointer1[1] != longpointer2[1])
                {
                    return false;
                }
#if DEBUG
                count &= ~(1 << 3);
#endif
                longPointer1 += 2;
                longpointer2 += 2;
            }

            if ((count & (1 << 2)) != 0)
            {
                if (*longPointer1 != *longpointer2)
                    return false;
#if DEBUG
                count &= ~(1 << 2);
#endif
                ++longPointer1;
                ++longpointer2;
            }

            pointer1 = (Byte*)longPointer1;
            pointer2 = (Byte*)longpointer2;
            if ((count & (1 << 1)) != 0)
            {
                if (*(UInt16*)pointer1 != *(UInt16*)pointer2)
                    return false;
#if DEBUG
                count &= ~(1 << 1);
#endif
                pointer1 += sizeof(UInt16);
                pointer2 += sizeof(UInt16);
            }

            if ((count & (1 << 0)) != 0)
            {
                if (*pointer1 != *pointer2)
                    return false;
#if DEBUG
                count &= ~(1 << 0);
#endif
                ++pointer1;
                ++pointer2;
            }

            // この時点で count は 0 のはず
#if DEBUG
            Assert(count == 0);
#endif

            return true;
        }

        private static unsafe Boolean InternalSequenceEqualUnmanagedByUInt64(Byte* pointer1, Byte* pointer2, Int32 count)
        {
            const Int32 alignmentMask = sizeof(UInt64) - 1;

            // 先行してバイト単位で比較する長さを計算する
            {
                var offset = (Int32)pointer1 & alignmentMask;
                var preCount = (-offset & alignmentMask).Minimum(count);
                var __count = preCount;
                while (__count-- > 0)
                {
                    if (*pointer1++ != *pointer2++)
                        return false;
                }

                count -= preCount;
            }

            // この時点で pointer1 が sizeof(UInt64) バイトバウンダリ、または count == 0 のはず。
#if DEBUG
            Assert((UInt64)pointer1 % sizeof(UInt64) == 0 || count == 0);
#endif

            var longPointer1 = (UInt64*)pointer1;
            var longPointer2 = (UInt64*)pointer2;

            while (count >= 8 * sizeof(UInt64))
            {
                if (longPointer1[0] != longPointer2[0]
                    || longPointer1[1] != longPointer2[1]
                    || longPointer1[2] != longPointer2[2]
                    || longPointer1[3] != longPointer2[3]
                    || longPointer1[4] != longPointer2[4]
                    || longPointer1[5] != longPointer2[5]
                    || longPointer1[6] != longPointer2[6]
                    || longPointer1[7] != longPointer2[7])
                {
                    return false;
                }

                count -= 8 * sizeof(UInt64);
                longPointer1 += 8;
                longPointer2 += 8;
            }
#if DEBUG
            Assert((count & ~((1 << 6) - 1)) == 0);
#endif
            if ((count & (1 << 5)) != 0)
            {
                if (longPointer1[0] != longPointer2[0]
                    || longPointer1[1] != longPointer2[1]
                    || longPointer1[2] != longPointer2[2]
                    || longPointer1[3] != longPointer2[3])
                {
                    return false;
                }
#if DEBUG
                count &= ~(1 << 5);
#endif
                longPointer1 += 4;
                longPointer2 += 4;
            }

            if ((count & (1 << 4)) != 0)
            {
                if (longPointer1[0] != longPointer2[0]
                    || longPointer1[1] != longPointer2[1])
                {
                    return false;
                }
#if DEBUG
                count &= ~(1 << 4);
#endif
                longPointer1 += 2;
                longPointer2 += 2;
            }

            if ((count & (1 << 3)) != 0)
            {
                if (*longPointer1 != *longPointer2)
                    return false;
#if DEBUG
                count &= ~(1 << 3);
#endif
                ++longPointer1;
                ++longPointer2;
            }

            pointer1 = (Byte*)longPointer1;
            pointer2 = (Byte*)longPointer2;
            if ((count & (1 << 2)) != 0)
            {
                if (*(UInt32*)pointer1 != *(UInt32*)pointer2)
                    return false;
#if DEBUG
                count &= ~(1 << 2);
#endif
                pointer1 += sizeof(UInt32);
                pointer2 += sizeof(UInt32);
            }

            if ((count & (1 << 1)) != 0)
            {
                if (*(UInt16*)pointer1 != *(UInt16*)pointer2)
                    return false;
#if DEBUG
                count &= ~(1 << 1);
#endif
                pointer1 += sizeof(UInt16);
                pointer2 += sizeof(UInt16);
            }

            if ((count & (1 << 0)) != 0)
            {
                if (*pointer1 != *pointer2)
                    return false;
#if DEBUG
                count &= ~(1 << 0);
#endif
                ++pointer1;
                ++pointer2;
            }

            // この時点で count は 0 のはず
#if DEBUG
            Assert(count == 0);
#endif

            return true;
        }

        private static unsafe Boolean InternalSequenceEqualUnmanagedByByte(Byte* pointer1, Byte* pointer2, Int32 count)
        {
#if DEBUG
            Assert(count < _THRESHOLD_ARRAY_EQUAL_BY_LONG_POINTER);
#endif
            while (count >= 8)
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

                count -= 8;
                pointer1 += 8;
                pointer2 += 8;
            }
#if DEBUG
            Assert((count & ~((1 << 3) - 1)) == 0);
#endif
            if ((count & (1 << 2)) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1]
                    || pointer1[2] != pointer2[2]
                    || pointer1[3] != pointer2[3])
                {
                    return false;
                }
#if DEBUG
                count &= ~(1 << 2);
#endif
                pointer1 += 4;
                pointer2 += 4;
            }

            if ((count & (1 << 1)) != 0)
            {
                if (pointer1[0] != pointer2[0]
                    || pointer1[1] != pointer2[1])
                {
                    return false;
                }
#if DEBUG
                count &= ~(1 << 1);
#endif
                pointer1 += 2;
                pointer2 += 2;
            }

            if ((count & (1 << 0)) != 0)
            {
                if (*pointer1 != *pointer2)
                    return false;
#if DEBUG
                count &= ~(1 << 0);
#endif
                ++pointer1;
                ++pointer2;
            }

            // この時点で count は 0 のはず
#if DEBUG
            Assert(count == 0);
#endif

            return true;
        }

        #region InternalSequenceCompare

        private static Int32 InternalSequenceCompare<ELEMENT_T, KEY_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            var count = array1Length.Minimum(array2Length);
            for (var index = 0; index < count; index++)
            {
                var c = DefaultCompare(keySelecter(array1[array1Offset + index]), keySelecter(array2[array2Offset + index]));
                if (c != 0)
                    return c;
            }

            return array1Length.CompareTo(array2Length);
        }

        private static Int32 InternalSequenceCompare<ELEMENT_T, KEY_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            var count = array1Length.Minimum(array2Length);
            for (var index = 0; index < count; index++)
            {
                var c = keyComparer.Compare(keySelecter(array1[array1Offset + index]), keySelecter(array2[array2Offset + index]));
                if (c != 0)
                    return c;
            }

            return array1Length.CompareTo(array2Length);
        }

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

        private static Int32 InternalSequenceCompareManaged<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            var count = array1Length.Minimum(array2Length);
            for (var index = 0; index < count; index++)
            {
                var c = array1[array1Offset + index].CompareTo(array2[array2Offset + index]);
                if (c != 0)
                    return c;
            }

            return array1Length.CompareTo(array2Length);
        }

        private static Int32 InternalSequenceCompareManaged<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, IComparer<ELEMENT_T> comparer)
        {
            var count = array1Length.Minimum(array2Length);
            for (var index = 0; index < count; index++)
            {
                var c = comparer.Compare(array1[array1Offset + index], array2[array2Offset + index]);
                if (c != 0)
                    return c;
            }

            return array1Length.CompareTo(array2Length);
        }

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

        private static Int32 InternalSequenceCompareManaged<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
        {
            var count = array1.Length.Minimum(array2.Length);
            for (var index = 0; index < count; index++)
            {
                var c = comparer.Compare(array1[index], array2[index]);
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
                var count = array1Length.Minimum(array2Length);
                var pointer1 = buffer1;
                var pointer2 = buffer2;
                while (count-- > 0)
                {
                    var c = (*pointer1++).CompareTo(*pointer2++);
                    if (c != 0)
                        return c;
                }

                return array1Length.CompareTo(array2Length);
            }
        }

        private static unsafe Int32 InternalSequenceCompareUnmanaged<ELEMENT_T>(ref ELEMENT_T array1, Int32 array1Length, ref ELEMENT_T array2, Int32 array2Length, IComparer<ELEMENT_T> comparer)
            where ELEMENT_T : unmanaged
        {
            fixed (ELEMENT_T* buffer1 = &array1)
            fixed (ELEMENT_T* buffer2 = &array2)
            {
                var count = array1Length.Minimum(array2Length);
                var pointer1 = buffer1;
                var pointer2 = buffer2;
                while (count-- > 0)
                {
                    var c = comparer.Compare(*pointer1++, *pointer2++);
                    if (c != 0)
                        return c;
                }

                return array1Length.CompareTo(array2Length);
            }
        }

        private static unsafe Int32 InternalSequenceCompareUnmanaged<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, Int32 array1Length, ReadOnlySpan<ELEMENT_T> array2, Int32 array2Length)
            where ELEMENT_T : unmanaged, IComparable<ELEMENT_T>
        {
            fixed (ELEMENT_T* buffer1 = array1)
            fixed (ELEMENT_T* buffer2 = array2)
            {
                var count = array1Length.Minimum(array2Length);
                var pointer1 = buffer1;
                var pointer2 = buffer2;
                while (count-- > 0)
                {
                    var c = (*pointer1++).CompareTo(*pointer2++);
                    if (c != 0)
                        return c;
                }

                return array1Length.CompareTo(array2Length);
            }
        }

        private static unsafe Int32 InternalSequenceCompareUnmanaged<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, Int32 array1Length, ReadOnlySpan<ELEMENT_T> array2, Int32 array2Length, IComparer<ELEMENT_T> comparer)
            where ELEMENT_T : unmanaged
        {
            fixed (ELEMENT_T* buffer1 = array1)
            fixed (ELEMENT_T* buffer2 = array2)
            {
                var count = array1Length.Minimum(array2Length);
                var pointer1 = buffer1;
                var pointer2 = buffer2;
                while (count-- > 0)
                {
                    var c = comparer.Compare(*pointer1++, *pointer2++);
                    if (c != 0)
                        return c;
                }

                return array1Length.CompareTo(array2Length);
            }
        }

        #endregion

        #region InternalCopyMemoryManaged

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalCopyMemoryManaged<ELEMENT_T>(ELEMENT_T[] sourceArray, ref Int32 sourceArrayOffset, ELEMENT_T[] destinationArray, ref Int32 destinationArrayOffset, Int32 count)
        {
            while (count-- > 0)
                destinationArray[destinationArrayOffset++] = sourceArray[sourceArrayOffset++];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalCopyMemoryManaged<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> sourceArray, Span<ELEMENT_T> destinationArray)
        {
            var count = sourceArray.Length;
            var index = 0;
            while (count-- > 0)
            {
                destinationArray[index] = sourceArray[index];
                ++index;
            }
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void InternalCopyMemoryUnmanaged<ELEMENT_T>(ref ELEMENT_T sourceArray, ref ELEMENT_T destinationArray, Int32 count)
            where ELEMENT_T : unmanaged
        {
            if (count <= 0)
                return;

            fixed (ELEMENT_T* sourcePointer = &sourceArray)
            fixed (ELEMENT_T* destinationPointer = &destinationArray)
            {
                if (sourcePointer == destinationPointer)
                    return;

                //
                // Either 'Unsafe.CopyBlock' or 'Unsafe.CopyBlockUnaligned' MUST NOT be called if the sourceArray and destinationArray overlap.
                //
                // The 'cpblk' instruction is used in 'Unsafe.CopyBlock' and 'Unsafe.CopyBlockUnaligned'.
                // https://github.com/dotnet/runtime/blob/main/src/libraries/System.Runtime.CompilerServices.Unsafe/src/System.Runtime.CompilerServices.Unsafe.il
                //
                // The behavior of cpblk is unspecified if the source and destination areas overlap.
                // https://docs.microsoft.com/en-us/dotnet/api/system.reflection.emit.opcodes.cpblk?view=net-6.0
                //

                if (destinationPointer + count <= sourcePointer || destinationPointer >= sourcePointer + count)
                {
                    // When sourceArray and destinationArray do not overlap

                    // Here I can safely call 'Unsafe.CopyBlock' or 'Unsafe.CopyBlock'.
                    var aligned = (sizeof(ELEMENT_T) & _alignmentMask) == 0;
                    if (aligned)
                        Unsafe.CopyBlock(destinationPointer, sourcePointer, (UInt32)(count * sizeof(ELEMENT_T) / sizeof(Byte)));
                    else
                        Unsafe.CopyBlockUnaligned(destinationPointer, sourcePointer, (UInt32)(count * sizeof(ELEMENT_T) / sizeof(Byte)));
                }
                else if (count * sizeof(ELEMENT_T) / sizeof(Byte) < _THRESHOLD_COPY_MEMORY_BY_LONG_POINTER)
                {
                    // Since byteCount is small enough, copy every byte.
                    InternalCopyMemoryUnmanagedByByte((Byte*)sourcePointer, (Byte*)destinationPointer, count * sizeof(ELEMENT_T) / sizeof(Byte));
                }
                else if (destinationPointer <= sourcePointer || (Byte*)destinationPointer >= (Byte*)sourcePointer + _alignment)
                {
                    // When sourceArray and destinationArray overlap, but destinationPointer <= sourcePointer or (Byte*)destinationPointer >= (Byte*)sourcePointer + _alignment

                    // Since no undesired overwrite occurs here, copy each UInt64 or UInt32.
                    if (_is64bitProcess)
                    {
                        var byteCount = sizeof(ELEMENT_T) * count;
                        if ((sizeof(ELEMENT_T) & (1 << 0)) != 0 || (byteCount & (1 << 0)) != 0 || ((Int32)sourcePointer & (1 << 0)) != 0 || ((Int32)destinationPointer & (1 << 0)) != 0)
                            InternalCopyMemoryUnmanagedByUInt64((Byte*)sourcePointer, (Byte*)destinationPointer, count * sizeof(ELEMENT_T) / sizeof(Byte));
                        else if ((sizeof(ELEMENT_T) & (1 << 1)) != 0 || (byteCount & (1 << 1)) != 0 || ((Int32)sourcePointer & (1 << 1)) != 0 || ((Int32)destinationPointer & (1 << 1)) != 0)
                            InternalCopyMemoryUnmanagedByUInt64((UInt16*)sourcePointer, (UInt16*)destinationPointer, count * sizeof(ELEMENT_T) / sizeof(UInt16));
                        else if ((sizeof(ELEMENT_T) & (1 << 2)) != 0 || (byteCount & (1 << 2)) != 0 || ((Int32)sourcePointer & (1 << 2)) != 0 || ((Int32)destinationPointer & (1 << 2)) != 0)
                            InternalCopyMemoryUnmanagedByUInt64((UInt32*)sourcePointer, (UInt32*)destinationPointer, count * sizeof(ELEMENT_T) / sizeof(UInt32));
                        else
                            InternalCopyMemoryUnmanagedByUInt64((UInt64*)sourcePointer, (UInt64*)destinationPointer, count * sizeof(ELEMENT_T) / sizeof(UInt64));
                    }
                    else
                    {
                        var byteCount = sizeof(ELEMENT_T) * count;
                        if ((sizeof(ELEMENT_T) & (1 << 0)) != 0 || (byteCount & (1 << 0)) != 0 || ((Int32)sourcePointer & (1 << 0)) != 0 || ((Int32)destinationPointer & (1 << 0)) != 0)
                            InternalCopyMemoryUnmanagedByUInt32((Byte*)sourcePointer, (Byte*)destinationPointer, count * sizeof(ELEMENT_T) / sizeof(Byte));
                        else if ((sizeof(ELEMENT_T) & (1 << 1)) != 0 || (byteCount & (1 << 1)) != 0 || ((Int32)sourcePointer & (1 << 1)) != 0 || ((Int32)destinationPointer & (1 << 1)) != 0)
                            InternalCopyMemoryUnmanagedByUInt32((UInt16*)sourcePointer, (UInt16*)destinationPointer, count * sizeof(ELEMENT_T) / sizeof(UInt16));
                        else
                            InternalCopyMemoryUnmanagedByUInt32((UInt32*)sourcePointer, (UInt32*)destinationPointer, count * sizeof(ELEMENT_T) / sizeof(UInt32));
                    }
                }
                else
                {
                    // When (Byte*)sourcePointer < (Byte*)destinationPointer < (Byte*)sourcePointer + (sizeof(UInt64) or sizeof(Uint32))

                    // Undesirable overwrites may occur here when copying memory.
                    var difference = (Int32)((Byte*)destinationPointer - (Byte*)sourcePointer);
                    if (difference >= sizeof(UInt32) && (((sizeof(ELEMENT_T)) & (sizeof(UInt32) - 1)) == 0 || ((count * sizeof(ELEMENT_T)) & (sizeof(UInt32) - 1)) == 0))
                    {
                        // Since undesired overwriting does not occur here, copy it for each UInt32.
                        InternalCopyMemoryUnmanagedByUInt32((UInt32*)sourcePointer, (UInt32*)destinationPointer, count * sizeof(ELEMENT_T) / sizeof(UInt32));
                    }
                    else if (difference >= sizeof(UInt16) && (((sizeof(ELEMENT_T)) & (sizeof(UInt16) - 1)) == 0 || ((count * sizeof(ELEMENT_T)) & (sizeof(UInt16) - 1)) == 0))
                    {
                        // Since undesired overwriting does not occur here, copy it for each UInt16.
                        InternalCopyMemoryUnmanagedByUInt16((UInt16*)sourcePointer, (UInt16*)destinationPointer, count * sizeof(ELEMENT_T) / sizeof(UInt16));
                    }
                    else
                    {
                        // Here, copying every UInt64 or UInt32 causes an unfavorable overwrite, so copy every byte.
                        InternalCopyMemoryUnmanagedByByte((Byte*)sourcePointer, (Byte*)destinationPointer, count * sizeof(ELEMENT_T) / sizeof(Byte));
                    }
                }
            }
        }

        #region InternalCopyMemoryUnmanagedByUInt64

        private static unsafe void InternalCopyMemoryUnmanagedByUInt64(UInt64* sourcePointer, UInt64* destinationPointer, Int32 count)
        {
            if (((Int32)sourcePointer & (sizeof(UInt64) - 1)) != 0 || ((Int32)destinationPointer & (sizeof(UInt64) - 1)) != 0)
            {
                // If the sourcePointer or destinationPointer alignment is incorrect (usually not possible)
                InternalCopyMemoryUnmanagedByUInt64((UInt32*)sourcePointer, (UInt32*)destinationPointer, count * sizeof(UInt64) / sizeof(UInt32));
            }
            else
            {
                while (count >= 8)
                {
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    count -= 8;
                }

                if ((count & (1 << 2)) != 0)
                {
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
#if DEBUG
                    count &= ~(1 << 2);
#endif
                }

                if ((count & (1 << 1)) != 0)
                {
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
#if DEBUG
                    count &= ~(1 << 1);
#endif
                }

                if ((count & (1 << 0)) != 0)
                {
                    *destinationPointer++ = *sourcePointer++;
#if DEBUG
                    count &= ~(1 << 0);
#endif
                }
#if DEBUG
                Assert(count == 0);
#endif
            }
        }

        private static unsafe void InternalCopyMemoryUnmanagedByUInt64(UInt32* sourcePointer, UInt32* destinationPointer, Int32 count)
        {
            if (((Int32)sourcePointer & (sizeof(UInt32) - 1)) != 0 || ((Int32)destinationPointer & (sizeof(UInt32) - 1)) != 0)
            {
                // If the sourcePointer or destinationPointer alignment is incorrect (usually not possible)
                InternalCopyMemoryUnmanagedByUInt64((UInt16*)sourcePointer, (UInt16*)destinationPointer, count * sizeof(UInt32) / sizeof(UInt16));
            }
            else
            {
                switch (((-(Int32)destinationPointer & (sizeof(UInt64) - 1)) / sizeof(UInt32)).Minimum(count))
                {
                    case 1:
                        *destinationPointer++ = *sourcePointer++;
                        --count;
                        break;
                    default:
                        break;
                }
#if DEBUG
                Assert((Int32)destinationPointer % sizeof(UInt64) == 0 || count == 0);
#endif
                {
                    var longSourcePointer = (UInt64*)sourcePointer;
                    var longDestinationPointer = (UInt64*)destinationPointer;
                    while (count >= 8 * sizeof(UInt64) / sizeof(UInt32))
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        count -= 8 * sizeof(UInt64) / sizeof(UInt32);
                    }

                    if ((count & (1 << 3)) != 0)
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                        count &= ~(1 << 3);
#endif
                    }

                    if ((count & (1 << 2)) != 0)
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                        count &= ~(1 << 2);
#endif
                    }

                    if ((count & (1 << 1)) != 0)
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                        count &= ~(1 << 1);
#endif
                    }

                    sourcePointer = (UInt32*)longSourcePointer;
                    destinationPointer = (UInt32*)longDestinationPointer;
                }

                if ((count & (1 << 0)) != 0)
                {
                    *destinationPointer++ = *sourcePointer++;
#if DEBUG
                    count &= ~(1 << 0);
#endif
                }
#if DEBUG
                Assert(count == 0);
#endif
            }
        }

        private static unsafe void InternalCopyMemoryUnmanagedByUInt64(UInt16* sourcePointer, UInt16* destinationPointer, Int32 count)
        {
            if (((Int32)sourcePointer & (sizeof(UInt16) - 1)) != 0 || ((Int32)destinationPointer & (sizeof(UInt16) - 1)) != 0)
            {
                // If the sourcePointer or destinationPointer alignment is incorrect (usually not possible)
                InternalCopyMemoryUnmanagedByUInt64((Byte*)sourcePointer, (Byte*)destinationPointer, count * sizeof(UInt16) / sizeof(Byte));
            }
            else
            {
                switch (((-(Int32)destinationPointer & (sizeof(UInt64) - 1)) / sizeof(UInt16)).Minimum(count))
                {
                    case 1:
                        *destinationPointer++ = *sourcePointer++;
                        --count;
                        break;
                    case 2:
                        *(UInt32*)destinationPointer = *(UInt32*)sourcePointer;
                        sourcePointer += sizeof(UInt32) / sizeof(UInt16);
                        destinationPointer += sizeof(UInt32) / sizeof(UInt16);
                        count -= 2;
                        break;
                    case 3:
                        *destinationPointer++ = *sourcePointer++;
                        *(UInt32*)destinationPointer = *(UInt32*)sourcePointer;
                        sourcePointer += sizeof(UInt32) / sizeof(UInt16);
                        destinationPointer += sizeof(UInt32) / sizeof(UInt16);
                        count -= 3;
                        break;
                    default:
                        break;
                }
#if DEBUG
                Assert((Int32)destinationPointer % sizeof(UInt64) == 0 || count == 0);
#endif
                {
                    var longSourcePointer = (UInt64*)sourcePointer;
                    var longDestinationPointer = (UInt64*)destinationPointer;
                    while (count >= 8 * sizeof(UInt64) / sizeof(UInt16))
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        count -= 8 * sizeof(UInt64) / sizeof(UInt16);
                    }

                    if ((count & (1 << 4)) != 0)
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                        count &= ~(1 << 4);
#endif
                    }

                    if ((count & (1 << 3)) != 0)
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                        count &= ~(1 << 3);
#endif
                    }

                    if ((count & (1 << 2)) != 0)
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                        count &= ~(1 << 2);
#endif
                    }

                    sourcePointer = (UInt16*)longSourcePointer;
                    destinationPointer = (UInt16*)longDestinationPointer;
                }

                if ((count & (1 << 1)) != 0)
                {
                    *(UInt32*)destinationPointer = *(UInt32*)sourcePointer;
                    destinationPointer += 1 << 1;
                    sourcePointer += 1 << 1;
#if DEBUG
                    count &= ~(1 << 1);
#endif
                }

                if ((count & (1 << 0)) != 0)
                {
                    *destinationPointer++ = *sourcePointer++;
#if DEBUG
                    count &= ~(1 << 0);
#endif
                }
#if DEBUG
                Assert(count == 0);
#endif
            }
        }

        private static unsafe void InternalCopyMemoryUnmanagedByUInt64(Byte* sourcePointer, Byte* destinationPointer, Int32 count)
        {
            switch (((-(Int32)destinationPointer & (sizeof(UInt64) - 1)) / sizeof(Byte)).Minimum(count))
            {
                case 1:
                    *destinationPointer++ = *sourcePointer++;
                    --count;
                    break;
                case 2:
                    *(UInt16*)destinationPointer = *(UInt16*)sourcePointer;
                    sourcePointer += sizeof(UInt16) / sizeof(Byte);
                    destinationPointer += sizeof(UInt16) / sizeof(Byte);
                    count -= 2;
                    break;
                case 3:
                    *destinationPointer++ = *sourcePointer++;
                    *(UInt16*)destinationPointer = *(UInt16*)sourcePointer;
                    sourcePointer += sizeof(UInt16) / sizeof(Byte);
                    destinationPointer += sizeof(UInt16) / sizeof(Byte);
                    count -= 3;
                    break;
                case 4:
                    *(UInt32*)destinationPointer = *(UInt32*)sourcePointer;
                    sourcePointer += sizeof(UInt32) / sizeof(Byte);
                    destinationPointer += sizeof(UInt32) / sizeof(Byte);
                    count -= 4;
                    break;
                case 5:
                    *destinationPointer++ = *sourcePointer++;
                    *(UInt32*)destinationPointer = *(UInt32*)sourcePointer;
                    sourcePointer += sizeof(UInt32) / sizeof(Byte);
                    destinationPointer += sizeof(UInt32) / sizeof(Byte);
                    count -= 5;
                    break;
                case 6:
                    *(UInt16*)destinationPointer = *(UInt16*)sourcePointer;
                    sourcePointer += sizeof(UInt16) / sizeof(Byte);
                    destinationPointer += sizeof(UInt16) / sizeof(Byte);
                    *(UInt32*)destinationPointer = *(UInt32*)sourcePointer;
                    sourcePointer += sizeof(UInt32) / sizeof(Byte);
                    destinationPointer += sizeof(UInt32) / sizeof(Byte);
                    count -= 6;
                    break;
                case 7:
                    *destinationPointer++ = *sourcePointer++;
                    *(UInt16*)destinationPointer = *(UInt16*)sourcePointer;
                    sourcePointer += sizeof(UInt16) / sizeof(Byte);
                    destinationPointer += sizeof(UInt16) / sizeof(Byte);
                    *(UInt32*)destinationPointer = *(UInt32*)sourcePointer;
                    sourcePointer += sizeof(UInt32) / sizeof(Byte);
                    destinationPointer += sizeof(UInt32) / sizeof(Byte);
                    count -= 7;
                    break;
                default:
                    break;
            }
#if DEBUG
            Assert((Int32)destinationPointer % sizeof(UInt64) == 0 || count == 0);
#endif
            {
                var longSourcePointer = (UInt64*)sourcePointer;
                var longDestinationPointer = (UInt64*)destinationPointer;
                while (count >= 8 * sizeof(UInt64) / sizeof(Byte))
                {
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    count -= 8 * sizeof(UInt64) / sizeof(Byte);
                }

                if ((count & (1 << 5)) != 0)
                {
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                    count &= ~(1 << 5);
#endif
                }

                if ((count & (1 << 4)) != 0)
                {
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                    count &= ~(1 << 4);
#endif
                }

                if ((count & (1 << 3)) != 0)
                {
                    *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                    count &= ~(1 << 3);
#endif
                }

                sourcePointer = (Byte*)longSourcePointer;
                destinationPointer = (Byte*)longDestinationPointer;
            }

            if ((count & (1 << 2)) != 0)
            {
                *(UInt32*)destinationPointer = *(UInt32*)sourcePointer;
                destinationPointer += 1 << 2;
                sourcePointer += 1 << 2;
#if DEBUG
                count &= ~(1 << 2);
#endif
            }

            if ((count & (1 << 1)) != 0)
            {
                *(UInt16*)destinationPointer = *(UInt16*)sourcePointer;
                destinationPointer += 1 << 1;
                sourcePointer += 1 << 1;
#if DEBUG
                count &= ~(1 << 1);
#endif
            }

            if ((count & (1 << 0)) != 0)
            {
                *destinationPointer++ = *sourcePointer++;
#if DEBUG
                count &= ~(1 << 0);
#endif
            }
#if DEBUG
            Assert(count == 0);
#endif
        }

        #endregion

        #region InternalCopyMemoryUnmanagedByUInt32

        private static unsafe void InternalCopyMemoryUnmanagedByUInt32(UInt32* sourcePointer, UInt32* destinationPointer, Int32 count)
        {
            if (((Int32)sourcePointer & (sizeof(UInt32) - 1)) != 0 || ((Int32)destinationPointer & (sizeof(UInt32) - 1)) != 0)
            {
                // If the sourcePointer or destinationPointer alignment is incorrect (usually not possible)
                InternalCopyMemoryUnmanagedByUInt32((UInt16*)sourcePointer, (UInt16*)destinationPointer, count * sizeof(UInt32) / sizeof(UInt16));
            }
            else
            {
                while (count >= 8)
                {
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    count -= 8;
                }

                if ((count & (1 << 2)) != 0)
                {
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
#if DEBUG
                    count &= ~(1 << 2);
#endif
                }

                if ((count & (1 << 1)) != 0)
                {
                    *destinationPointer++ = *sourcePointer++;
                    *destinationPointer++ = *sourcePointer++;
#if DEBUG
                    count &= ~(1 << 1);
#endif
                }

                if ((count & (1 << 0)) != 0)
                {
                    *destinationPointer++ = *sourcePointer++;
#if DEBUG
                    count &= ~(1 << 0);
#endif
                }
#if DEBUG
                Assert(count == 0);
#endif
            }
        }

        private static unsafe void InternalCopyMemoryUnmanagedByUInt32(UInt16* sourcePointer, UInt16* destinationPointer, Int32 count)
        {
            if (((Int32)sourcePointer & (sizeof(UInt16) - 1)) != 0 || ((Int32)destinationPointer & (sizeof(UInt16) - 1)) != 0)
            {
                // If the sourcePointer or destinationPointer alignment is incorrect (usually not possible)
                InternalCopyMemoryUnmanagedByUInt32((Byte*)sourcePointer, (Byte*)destinationPointer, count * sizeof(UInt16) / sizeof(Byte));
            }
            else
            {
                switch (((-(Int32)destinationPointer & (sizeof(UInt32) - 1)) / sizeof(UInt16)).Minimum(count))
                {
                    case 1:
                        *destinationPointer++ = *sourcePointer++;
                        --count;
                        break;
                    default:
                        break;
                }
#if DEBUG
                Assert((Int32)destinationPointer % sizeof(UInt32) == 0 || count == 0);
#endif
                {
                    var longSourcePointer = (UInt32*)sourcePointer;
                    var longDestinationPointer = (UInt32*)destinationPointer;
                    while (count >= 8 * sizeof(UInt32) / sizeof(UInt16))
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        count -= 8 * sizeof(UInt32) / sizeof(UInt16);
                    }

                    if ((count & (1 << 3)) != 0)
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                        count &= ~(1 << 3);
#endif
                    }

                    if ((count & (1 << 2)) != 0)
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
                        *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                        count &= ~(1 << 2);
#endif
                    }

                    if ((count & (1 << 1)) != 0)
                    {
                        *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                        count &= ~(1 << 1);
#endif
                    }

                    sourcePointer = (UInt16*)longSourcePointer;
                    destinationPointer = (UInt16*)longDestinationPointer;
                }

                if ((count & (1 << 0)) != 0)
                {
                    *destinationPointer++ = *sourcePointer++;
#if DEBUG
                    count &= ~(1 << 0);
#endif
                }
#if DEBUG
                Assert(count == 0);
#endif
            }
        }

        private static unsafe void InternalCopyMemoryUnmanagedByUInt32(Byte* sourcePointer, Byte* destinationPointer, Int32 byteCount)
        {
            switch (((-(Int32)destinationPointer & (sizeof(UInt32) - 1)) / sizeof(Byte)).Minimum(byteCount))
            {
                case 1:
                    *destinationPointer++ = *sourcePointer++;
                    byteCount -= 1;
                    break;
                case 2:
                    *(UInt16*)destinationPointer = *(UInt16*)sourcePointer;
                    sourcePointer += sizeof(UInt16) / sizeof(Byte);
                    destinationPointer += sizeof(UInt16) / sizeof(Byte);
                    byteCount -= 2;
                    break;
                case 3:
                    *destinationPointer++ = *sourcePointer++;
                    *(UInt16*)destinationPointer = *(UInt16*)sourcePointer;
                    sourcePointer += sizeof(UInt16) / sizeof(Byte);
                    destinationPointer += sizeof(UInt16) / sizeof(Byte);
                    byteCount -= 3;
                    break;
                default:
                    break;
            }
#if DEBUG
            Assert((Int32)destinationPointer % sizeof(UInt32) == 0 || byteCount == 0);
#endif
            {
                var longSourcePointer = (UInt32*)sourcePointer;
                var longDestinationPointer = (UInt32*)destinationPointer;
                while (byteCount >= 8 * sizeof(UInt32) / sizeof(Byte))
                {
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    byteCount -= 8 * sizeof(UInt32) / sizeof(Byte);
                }

                if ((byteCount & (1 << 4)) != 0)
                {
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                    byteCount &= ~(1 << 4);
#endif
                }

                if ((byteCount & (1 << 3)) != 0)
                {
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                    byteCount &= ~(1 << 3);
#endif
                }

                if ((byteCount & (1 << 2)) != 0)
                {
                    *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                    byteCount &= ~(1 << 2);
#endif
                }

                sourcePointer = (Byte*)longSourcePointer;
                destinationPointer = (Byte*)longDestinationPointer;
            }

            if ((byteCount & (1 << 1)) != 0)
            {
                *(UInt16*)destinationPointer = *(UInt16*)sourcePointer;
                destinationPointer += sizeof(UInt16);
                sourcePointer += sizeof(UInt16);
#if DEBUG
                byteCount &= ~(1 << 1);
#endif
            }

            if ((byteCount & (1 << 0)) != 0)
            {
                *destinationPointer++ = *sourcePointer++;
#if DEBUG
                byteCount &= ~(1 << 0);
#endif
            }
#if DEBUG
            Assert(byteCount == 0);
#endif
        }

        #endregion

        #region InternalCopyMemoryUnmanagedByUInt16

        private static unsafe void InternalCopyMemoryUnmanagedByUInt16(UInt16* sourcePointer, UInt16* destinationPointer, Int32 count)
        {
            while (count >= 8)
            {
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                count -= 8;
            }

            if ((count & (1 << 2)) != 0)
            {
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
#if DEBUG
                count &= ~(1 << 2);
#endif
            }

            if ((count & (1 << 1)) != 0)
            {
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
#if DEBUG
                count &= ~(1 << 1);
#endif
            }

            if ((count & (1 << 0)) != 0)
            {
                *destinationPointer++ = *sourcePointer++;
#if DEBUG
                count &= ~(1 << 0);
#endif
            }
#if DEBUG
            Assert(count == 0);
#endif
        }

        private static unsafe void InternalCopyMemoryUnmanagedByUInt16(Byte* sourcePointer, Byte* destinationPointer, Int32 count)
        {
            switch ((((Int32)destinationPointer & (sizeof(UInt16) - 1)) / sizeof(Byte)).Minimum(count))
            {
                case 1:
                    *destinationPointer++ = *sourcePointer++;
                    --count;
                    break;
                default:
                    break;
            }
#if DEBUG
            Assert((Int32)destinationPointer % sizeof(UInt16) == 0 || count == 0);
#endif
            {
                var longSourcePointer = (UInt16*)sourcePointer;
                var longDestinationPointer = (UInt16*)destinationPointer;
                while (count >= 8 * sizeof(UInt16) / sizeof(Byte))
                {
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    count -= 8 * sizeof(UInt16) / sizeof(Byte);
                }

                if ((count & (1 << 3)) != 0)
                {
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                    count &= ~(1 << 3);
#endif
                }

                if ((count & (1 << 2)) != 0)
                {
                    *longDestinationPointer++ = *longSourcePointer++;
                    *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                    count &= ~(1 << 2);
#endif
                }

                if ((count & (1 << 1)) != 0)
                {
                    *longDestinationPointer++ = *longSourcePointer++;
#if DEBUG
                    count &= ~(1 << 1);
#endif
                }

                sourcePointer = (Byte*)longSourcePointer;
                destinationPointer = (Byte*)longDestinationPointer;
            }

            if ((count & (1 << 0)) != 0)
            {
                *destinationPointer++ = *sourcePointer++;
#if DEBUG
                count &= ~(1 << 0);
#endif
            }
#if DEBUG
            Assert(count == 0);
#endif
        }

        #endregion

        private static unsafe void InternalCopyMemoryUnmanagedByByte(Byte* sourcePointer, Byte* destinationPointer, Int32 count)
        {
            while (count >= 8)
            {
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                count -= 8;
            }

            if ((count & (1 << 2)) != 0)
            {
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
#if DEBUG
                count &= ~(1 << 2);
#endif
            }

            if ((count & (1 << 1)) != 0)
            {
                *destinationPointer++ = *sourcePointer++;
                *destinationPointer++ = *sourcePointer++;
#if DEBUG
                count &= ~(1 << 1);
#endif
            }

            if ((count & (1 << 0)) != 0)
            {
                *destinationPointer++ = *sourcePointer++;
#if DEBUG
                count &= ~(1 << 0);
#endif
            }
#if DEBUG
            Assert(count == 0);
#endif
        }

        #region InternalReverseArrayManaged

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalReverseArrayManaged<ELEMENT_T>(ELEMENT_T[] source, Int32 offset, Int32 count)
        {
            var index1 = offset;
            var index2 = offset + count - 1;
            while (index2 > index1)
            {
                (source[index2], source[index1]) = (source[index1], source[index2]);
                ++index1;
                --index2;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalReverseArrayManaged<ELEMENT_T>(Span<ELEMENT_T> source)
        {
            var index1 = 0;
            var index2 = source.Length - 1;
            while (index2 > index1)
            {
                (source[index2], source[index1]) = (source[index1], source[index2]);
                ++index1;
                --index2;
            }
        }

        #endregion

        private static unsafe void InternalReverseArrayUnmanaged<ELEMENT_T>(ref ELEMENT_T source, Int32 count)
            where ELEMENT_T : unmanaged
        {
            // サイズが 0 の配列が渡されると source は null 参照になるので、count の場合は source を参照してはならない。
            if (count <= 1)
                return;

            fixed (ELEMENT_T* buffer = &source)
            {
                var pointer1 = buffer;
                var pointer2 = buffer + count - 1;
                while (pointer2 > pointer1)
                {
                    (*pointer1, *pointer2) = (*pointer2, *pointer1);
                    ++pointer1;
                    --pointer2;
                }
            }
        }

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
