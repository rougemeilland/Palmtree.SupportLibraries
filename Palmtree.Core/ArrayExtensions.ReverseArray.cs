using System;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    partial class ArrayExtensions
    {
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

            InternalReverseArray(source.AsSpan());
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

            InternalReverseArray(source.AsSpan(offset, count));
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

        #region InternalReverseArray

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalReverseArray<ELEMENT_T>(Span<ELEMENT_T> source)
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
    }
}
