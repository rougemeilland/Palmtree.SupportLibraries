#define USE_SYSTEM_BIT_OPERATION
using System;
using System.Runtime.CompilerServices;
#if USE_SYSTEM_BIT_OPERATION
using System.Numerics;
#endif

namespace Palmtree.Numerics
{
    public static class IntegerExtensions
    {
        #region GreatestCommonDivisor

        /// <summary>
        /// 2つの整数の最大公約数を計算します。
        /// </summary>
        /// <param name="u">最初の整数です。</param>
        /// <param name="v">2 番目の整数です。</param>
        /// <returns><paramref name="u"/>と<paramref name="v"/>の最大公約数です。この値は常に正の整数です。 </returns>
        /// <exception cref="OverflowException">計算中にオーバーフローが発生しました。</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 GreatestCommonDivisor(this Int32 u, Int32 v)
        {
            // checked / unchecked を行っている理由:
            //   checked コンテキスト内部で int.MinValue (0x80000000) の値を符号反転しようとするとオーバーフロー例外が発生するため、明示的に unchecked をしている。
            //   符号なし GCD が 0x80000000 であった場合、int 型に安全に変換することはできないので、checked を指定してオーバーフローチェックを行っている。
            //     ※ 符号なし u と 符号なし v は共に 0x80000000 以下であるので、GCDが 0x80000000 を超えることはない。
            //        GCDが 0x80000000 になるのは u と v が共に int.MinValue (0x80000000) である場合だけ。

            var unsignedGcd = GreatestCommonDivisor(u.AbsoluteWithoutCheck(), v.AbsoluteWithoutCheck());
            return checked((Int32)unsignedGcd);
        }

        /// <summary>
        /// 2つの整数の最大公約数を計算します。
        /// </summary>
        /// <param name="u">最初の整数です。</param>
        /// <param name="v">2 番目の整数です。</param>
        /// <returns><paramref name="u"/>と<paramref name="v"/>の最大公約数です。この値は常に正の整数です。 </returns>
        /// <exception cref="OverflowException">計算中にオーバーフローが発生しました。</exception>
        public static UInt32 GreatestCommonDivisor(this UInt32 u, UInt32 v)
        {
            if (u <= 0)
            {
                if (v <= 0)
                    throw new ArgumentException($"Both {nameof(u)} and {nameof(v)} are 0. The greatest common divisor of 0 and 0 is undefined.");

                return v;
            }

            if (v <= 0)
                return u;

            // この時点で u と v は共に正数
            System.Diagnostics.Debug.Assert(u >= 0 && v >= 0);

            // 「互減法」アルゴリズムを使用して最大公約数を求める。
            // 乗算/除算/剰余算を一切使わなくて済むので互除法よりも極めて高速に計算できる。
            // [出典] 「準数値算法 算術演算」 著)Donald Ervin Knuth , 出版)サイエンス社
            //
            // GCDの以下の性質を利用して、u および v をだんだん小さくしていくアルゴリズムである。
            // 1) w が u と v の公約数であるとき、GCD(u / w, v / w) * w == GCD(u, v)
            // 2) 素数 w が u の因数であるが v の因数ではないとき、 GCD(u / w, v) == GCD(u, v)
            // 3) u > v のとき、 GCD(u - v, v) == GCD(u, v)
            // 4) u > 0 && v > 0 && u == v のとき、 GCD(u, v) == u

            var k = 0;

            // u と v のどちらかが奇数になるまで続ける
#if USE_SYSTEM_BIT_OPERATION
            {
                var zeroBitCount = BitOperations.TrailingZeroCount(u).Minimum(BitOperations.TrailingZeroCount(v));
                u >>= zeroBitCount;
                v >>= zeroBitCount;
                k += zeroBitCount;
            }
#else
            while ((u & 1) == 0 && (v & 1) == 0)
            {
                u >>= 1;
                v >>= 1;
                ++k;
            }
#endif

            // この時点で、u と v は共に正で、かつ少なくとも u と v のどちらかが奇数で、かつ、 u << k が元の u に等しく、v << k が元の v に等しい
            System.Diagnostics.Debug.Assert(u > 0 && v > 0 && ((u & 1) != 0 || (v & 1) != 0));

            // u が偶数であれば奇数になるまで右シフトする。

#if USE_SYSTEM_BIT_OPERATION
            u >>= BitOperations.TrailingZeroCount(u);
#else
            while ((u & 1) == 0)
                u >>= 1;
#endif

            // v が偶数であれば奇数になるまで右シフトする。
#if USE_SYSTEM_BIT_OPERATION
            v >>= BitOperations.TrailingZeroCount(v);
#else
            while ((v & 1) == 0)
                v >>= 1;
#endif

            // 最大公約数が求まるまで繰り返す。
            while (true)
            {
                // この時点で、u と v は共に正の奇数である。
                System.Diagnostics.Debug.Assert(u > 0 && v > 0 && (u & 1) != 0 && (v & 1) != 0);

                if (u == v)
                {
                    // u == v の場合

                    // u を k ビットだけ左シフトした値を最終的な GCD として復帰
                    return u << k;
                }

                if (u < v)
                {
                    // u < v なら u と v を入れ替える。
                    (v, u) = (u, v);
                }

                // この時点で、u と v は共に正で、かつ u > v かつ 少なくとも u と v はともに奇数である。
                System.Diagnostics.Debug.Assert(u > 0 && v > 0 && u > v && (u & 1) != 0 && (v & 1) != 0);

                u -= v;

                // この時点で u は正の偶数
                System.Diagnostics.Debug.Assert(u > 0 && (u & 1) == 0);

                // u が奇数になるまで u を右シフトする
#if USE_SYSTEM_BIT_OPERATION
                u >>= BitOperations.TrailingZeroCount(u);
#else
                while ((u & 1) == 0)
                    u >>= 1;
#endif
            }
        }

        /// <summary>
        /// 2つの整数の最大公約数を計算します。
        /// </summary>
        /// <param name="u">最初の整数です。</param>
        /// <param name="v">2 番目の整数です。</param>
        /// <returns><paramref name="u"/>と<paramref name="v"/>の最大公約数です。この値は常に正の整数です。 </returns>
        /// <exception cref="OverflowException">計算中にオーバーフローが発生しました。</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 GreatestCommonDivisor(this Int64 u, Int64 v)
        {
            // checked / unchecked を行っている理由:
            //   checked コンテキスト内部で long.MinValue (0x8000000000000000) の値を符号反転しようとするとオーバーフロー例外が発生するため、明示的に unchecked をしている。
            //   符号なし GCD が 0x8000000000000000 であった場合、long 型に安全に変換することはできないので、checked を指定してオーバーフローチェックを行っている。
            //     ※ 符号なし u と 符号なし v は共に 0x8000000000000000 以下であるので、GCDが 0x8000000000000000 を超えることはない。
            //        GCDが 0x8000000000000000 になるのは u と v が共に long.MinValue (0x8000000000000000) である場合だけ。
            var unsignedGcd = GreatestCommonDivisor(u.AbsoluteWithoutCheck(), v.AbsoluteWithoutCheck());
            return checked((Int64)unsignedGcd);
        }

        /// <summary>
        /// 2つの整数の最大公約数を計算します。
        /// </summary>
        /// <param name="u">最初の整数です。</param>
        /// <param name="v">2 番目の整数です。</param>
        /// <returns><paramref name="u"/>と<paramref name="v"/>の最大公約数です。この値は常に正の整数です。 </returns>
        /// <exception cref="OverflowException">計算中にオーバーフローが発生しました。</exception>
        public static UInt64 GreatestCommonDivisor(this UInt64 u, UInt64 v)
        {
            if (u <= 0)
            {
                if (v <= 0)
                    throw new ArgumentException($"Both {nameof(u)} and {nameof(v)} are 0. The greatest common divisor of 0 and 0 is undefined.");

                return v;
            }

            if (v <= 0)
                return u;

            // この時点で u と v は共に正数
            System.Diagnostics.Debug.Assert(u >= 0 && v >= 0);

            // 「互減法」アルゴリズムを使用して最大公約数を求める。
            // 乗算/除算/剰余算を一切使わなくて済むので互除法よりも極めて高速に計算できる。
            // [出典] 「準数値算法 算術演算」 著)Donald Ervin Knuth , 出版)サイエンス社
            //
            // GCDの以下の性質を利用して、u および v をだんだん小さくしていくアルゴリズムである。
            // 1) w が u と v の公約数であるとき、GCD(u / w, v / w) * w == GCD(u, v)
            // 2) 素数 w が u の因数であるが v の因数ではないとき、 GCD(u / w, v) == GCD(u, v)
            // 3) u > v のとき、 GCD(u - v, v) == GCD(u, v)
            // 4) u > 0 && v > 0 && u == v のとき、 GCD(u, v) == u

            var k = 0;

            // u と v のどちらかが奇数になるまで続ける
#if USE_SYSTEM_BIT_OPERATION
            {
                var zeroBitCount = BitOperations.TrailingZeroCount(u).Minimum(BitOperations.TrailingZeroCount(v));
                u >>= zeroBitCount;
                v >>= zeroBitCount;
                k += zeroBitCount;
            }
#else
            while ((u & 1) == 0 && (v & 1) == 0)
            {
                u >>= 1;
                v >>= 1;
                ++k;
            }
#endif

            // この時点で、u と v は共に正で、かつ少なくとも u と v のどちらかが奇数で、かつ、 u << k が元の u に等しく、v << k が元の v に等しい
            System.Diagnostics.Debug.Assert(u > 0 && v > 0 && ((u & 1) != 0 || (v & 1) != 0));

            // u が偶数であれば奇数になるまで右シフトする。
#if USE_SYSTEM_BIT_OPERATION
            u >>= BitOperations.TrailingZeroCount(u);
#else
            while ((u & 1) == 0)
                u >>= 1;
#endif

            // v が偶数であれば奇数になるまで右シフトする。
#if USE_SYSTEM_BIT_OPERATION
            v >>= BitOperations.TrailingZeroCount(v);
#else
            while ((v & 1) == 0)
                v >>= 1;
#endif

            // 最大公約数が求まるまで繰り返す。
            while (true)
            {
                // この時点で、u と v は共に正の奇数である。
                System.Diagnostics.Debug.Assert(u > 0 && v > 0 && (u & 1) != 0 && (v & 1) != 0);

                if (u == v)
                {
                    // u == v の場合

                    // u を k ビットだけ左シフトした値を最終的な GCD として復帰
                    return u << k;
                }

                if (u < v)
                {
                    // u < v なら u と v を入れ替える。
                    (v, u) = (u, v);
                }

                // この時点で、u と v は共に正で、かつ u > v かつ 少なくとも u と v はともに奇数である。
                System.Diagnostics.Debug.Assert(u > 0 && v > 0 && u > v && (u & 1) != 0 && (v & 1) != 0);

                u -= v;

                // この時点で u は正の偶数
                System.Diagnostics.Debug.Assert(u > 0 && (u & 1) == 0);

                // u が奇数になるまで u を右シフトする
#if USE_SYSTEM_BIT_OPERATION
                u >>= BitOperations.TrailingZeroCount(u);
#else
                while ((u & 1) == 0)
                    u >>= 1;
#endif
            }
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// 2つの整数の最大公約数を計算します。
        /// </summary>
        /// <param name="u">最初の整数です。</param>
        /// <param name="v">2 番目の整数です。</param>
        /// <returns><paramref name="u"/>と<paramref name="v"/>の最大公約数です。この値は常に正の整数です。 </returns>
        /// <exception cref="OverflowException">計算中にオーバーフローが発生しました。</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int128 GreatestCommonDivisor(this Int128 u, Int128 v)
        {
            // checked / unchecked を行っている理由:
            //   checked コンテキスト内部で Int128.MinValue (0x80000000000000000000000000000000) の値を符号反転しようとするとオーバーフロー例外が発生するため、明示的に unchecked をしている。
            //   符号なし GCD が 0x80000000000000000000000000000000 であった場合、long 型に安全に変換することはできないので、checked を指定してオーバーフローチェックを行っている。
            //     ※ 符号なし u と 符号なし v は共に 0x80000000000000000000000000000000 以下であるので、GCDが 0x80000000000000000000000000000000 を超えることはない。
            //        GCDが 0x80000000000000000000000000000000 になるのは u と v が共に long.MinValue (0x80000000000000000000000000000000) である場合だけ。
            var unsignedGcd = GreatestCommonDivisor(u.AbsoluteWithoutCheck(), v.AbsoluteWithoutCheck());
            return checked((Int128)unsignedGcd);
        }

        /// <summary>
        /// 2つの整数の最大公約数を計算します。
        /// </summary>
        /// <param name="u">最初の整数です。</param>
        /// <param name="v">2 番目の整数です。</param>
        /// <returns><paramref name="u"/>と<paramref name="v"/>の最大公約数です。この値は常に正の整数です。 </returns>
        /// <exception cref="OverflowException">計算中にオーバーフローが発生しました。</exception>
        public static UInt128 GreatestCommonDivisor(this UInt128 u, UInt128 v)
        {
            if (u <= 0)
            {
                if (v <= 0)
                    throw new ArgumentException($"Both {nameof(u)} and {nameof(v)} are 0. The greatest common divisor of 0 and 0 is undefined.");

                return v;
            }

            if (v <= 0)
                return u;

            // この時点で u と v は共に正数
            System.Diagnostics.Debug.Assert(u >= 0 && v >= 0);

            // 「互減法」アルゴリズムを使用して最大公約数を求める。
            // 乗算/除算/剰余算を一切使わなくて済むので互除法よりも極めて高速に計算できる。
            // [出典] 「準数値算法 算術演算」 著)Donald Ervin Knuth , 出版)サイエンス社
            //
            // GCDの以下の性質を利用して、u および v をだんだん小さくしていくアルゴリズムである。
            // 1) w が u と v の公約数であるとき、GCD(u / w, v / w) * w == GCD(u, v)
            // 2) 素数 w が u の因数であるが v の因数ではないとき、 GCD(u / w, v) == GCD(u, v)
            // 3) u > v のとき、 GCD(u - v, v) == GCD(u, v)
            // 4) u > 0 && v > 0 && u == v のとき、 GCD(u, v) == u

            var k = 0;

            // u と v のどちらかが奇数になるまで続ける
#if USE_SYSTEM_BIT_OPERATION
            {
                var zeroBitCount = (Int32)UInt128.TrailingZeroCount(u).Minimum(UInt128.TrailingZeroCount(v));
                u >>= zeroBitCount;
                v >>= zeroBitCount;
                k += zeroBitCount;
            }
#else
            while ((u & 1) == 0 && (v & 1) == 0)
            {
                u >>= 1;
                v >>= 1;
                ++k;
            }
#endif

            // この時点で、u と v は共に正で、かつ少なくとも u と v のどちらかが奇数で、かつ、 u << k が元の u に等しく、v << k が元の v に等しい
            System.Diagnostics.Debug.Assert(u > 0 && v > 0 && ((u & 1) != 0 || (v & 1) != 0));

            // u が偶数であれば奇数になるまで右シフトする。
#if USE_SYSTEM_BIT_OPERATION
            u >>= (Int32)UInt128.TrailingZeroCount(u);
#else
            while ((u & 1) == 0)
                u >>= 1;
#endif

            // v が偶数であれば奇数になるまで右シフトする。
#if USE_SYSTEM_BIT_OPERATION
            v >>= (Int32)UInt128.TrailingZeroCount(v);
#else
            while ((v & 1) == 0)
                v >>= 1;
#endif

            // 最大公約数が求まるまで繰り返す。
            while (true)
            {
                // この時点で、u と v は共に正の奇数である。
                System.Diagnostics.Debug.Assert(u > 0 && v > 0 && (u & 1) != 0 && (v & 1) != 0);

                if (u == v)
                {
                    // u == v の場合

                    // u を k ビットだけ左シフトした値を最終的な GCD として復帰
                    return u << k;
                }

                if (u < v)
                {
                    // u < v なら u と v を入れ替える。
                    (v, u) = (u, v);
                }

                // この時点で、u と v は共に正で、かつ u > v かつ 少なくとも u と v はともに奇数である。
                System.Diagnostics.Debug.Assert(u > 0 && v > 0 && u > v && (u & 1) != 0 && (v & 1) != 0);

                u -= v;

                // この時点で u は正の偶数
                System.Diagnostics.Debug.Assert(u > 0 && (u & 1) == 0);

                // u が奇数になるまで u を右シフトする
#if USE_SYSTEM_BIT_OPERATION
                u >>= (Int32)UInt128.TrailingZeroCount(u);
#else
                while ((u & 1) == 0)
                    u >>= 1;
#endif
            }
        }
#endif // NET7_0_OR_GREATER

        #endregion

        #region Reduce

        /// <summary>
        /// 2つの整数を約分します。
        /// </summary>
        /// <param name="u">最初の整数です。</param>
        /// <param name="v">2 番目の整数です。</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term>newU</term><description>約分された <paramref name="u"/>です。</description></item>
        /// <item><term>newV</term><description>約分された <paramref name="v"/>です。</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="OverflowException">計算中にオーバーフローが発生しました。</exception>
        public static (Int32 newU, Int32 newV) Reduce(this Int32 u, Int32 v)
        {
            var gcd = GreatestCommonDivisor(u, v);
            return (u / gcd, v / gcd);
        }

        /// <summary>
        /// 2つの整数を約分します。
        /// </summary>
        /// <param name="u">最初の整数です。</param>
        /// <param name="v">2 番目の整数です。</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term>newU</term><description>約分された <paramref name="u"/>です。</description></item>
        /// <item><term>newV</term><description>約分された <paramref name="v"/>です。</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="OverflowException">計算中にオーバーフローが発生しました。</exception>
        public static (UInt32 newU, UInt32 newV) Reduce(this UInt32 u, UInt32 v)
        {
            var gcd = GreatestCommonDivisor(u, v);
            return (u / gcd, v / gcd);
        }

        /// <summary>
        /// 2つの整数を約分します。
        /// </summary>
        /// <param name="u">最初の整数です。</param>
        /// <param name="v">2 番目の整数です。</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term>newU</term><description>約分された <paramref name="u"/>です。</description></item>
        /// <item><term>newV</term><description>約分された <paramref name="v"/>です。</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="OverflowException">計算中にオーバーフローが発生しました。</exception>
        public static (Int64 newU, Int64 newV) Reduce(this Int64 u, Int64 v)
        {
            var gcd = GreatestCommonDivisor(u, v);
            return (u / gcd, v / gcd);
        }

        /// <summary>
        /// 2つの整数を約分します。
        /// </summary>
        /// <param name="u">最初の整数です。</param>
        /// <param name="v">2 番目の整数です。</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term>newU</term><description>約分された <paramref name="u"/>です。</description></item>
        /// <item><term>newV</term><description>約分された <paramref name="v"/>です。</description></item>
        /// </list>
        /// </returns>
        /// <exception cref="OverflowException">計算中にオーバーフローが発生しました。</exception>
        public static (UInt64 newU, UInt64 newV) Reduce(this UInt64 u, UInt64 v)
        {
            var gcd = GreatestCommonDivisor(u, v);
            return (u / gcd, v / gcd);
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt32 AbsoluteWithoutCheck(this Int32 value) => unchecked(value >= 0 ? (UInt32)value : (UInt32)(-value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt64 AbsoluteWithoutCheck(this Int64 value) => unchecked(value >= 0 ? (UInt64)value : (UInt64)(-value));

#if NET7_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt128 AbsoluteWithoutCheck(this Int128 value) => unchecked(value >= 0 ? (UInt128)value : (UInt128)(-value));
#endif // NET7_0_OR_GREATER
    }
}
