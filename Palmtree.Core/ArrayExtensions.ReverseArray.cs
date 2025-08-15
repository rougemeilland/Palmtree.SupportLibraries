using System;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static partial class ArrayExtensions
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
            ArgumentNullException.ThrowIfNull(source);

            ReverseArrayCore(source.AsSpan());
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
            ReverseArrayCore(source.Span);
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
            ReverseArrayCore(source);
            return source;
        }

        #endregion

        #region ReverseArrayCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void ReverseArrayCore<ELEMENT_T>(Span<ELEMENT_T> source)
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
