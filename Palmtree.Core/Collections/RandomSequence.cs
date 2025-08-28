using System;
using System.Buffers;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Palmtree.Collections
{
    public static class RandomSequence
    {
        private const Int32 _SIZE_OF_UINT128 = 16;
        private const Decimal _denominatorOfRandomDecimalValueAsDecimal = 1000m * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 10; // 10^28
        private static readonly UInt128 _denominatorOfRandomDecimalValueAsUInt128 = new(0x00000000204fce5eUL, 0x3e25026110000000UL); // c# の言語仕様上、UInt128 の const 定義は認められていない。

        static RandomSequence()
        {
            checked
            {
#if DEBUG
                Validation.Assert(_denominatorOfRandomDecimalValueAsUInt128 == (UInt128)1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 10);
                Validation.Assert(_denominatorOfRandomDecimalValueAsDecimal == (Decimal)_denominatorOfRandomDecimalValueAsUInt128);
                unsafe
                {
                    Validation.Assert(_SIZE_OF_UINT128 == sizeof(UInt128));
                }
#endif
            }
        }

        /// <summary>
        /// 与えられた長さのランダムなビット配列を要素とするシーケンスを取得します。
        /// </summary>
        /// <param name="bitCount">
        /// ビット配列の長さを示す <see cref="Int32"/> 値です。
        /// </param>
        /// <returns>
        /// ビット配列を要素とするシーケンスを示す <see cref="IEnumerable{ReadOnlyBitArray}">IEnumerable&lt;<see cref="ReadOnlyBitArray"/>&gt;</see> オブジェクトです。
        /// </returns>
        /// <remarks>
        /// このメソッドで取得したシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードは長さが5ビットのランダムなビット配列を100個だけ取得します。
        /// <code>
        ///    ReadOnlyBitArray[] randomBitArrays = RandomSequence.GetBitArraySequence(5).Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<TinyBitArray> GetBitArraySequence(Int32 bitCount)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bitCount);

            var bufferSize = (bitCount + 7) / 8;
            var buffer = ArrayPool<Byte>.Shared.Rent(bufferSize);
            try
            {
                using var generator = RandomNumberGenerator.Create();
                var bitQueue = new BitQueue();
                while (true)
                {
                    generator.GetBytes(buffer.AsSpan(0, bufferSize));
                    for (var index = 0; index < bufferSize; ++index)
                        bitQueue.Enqueue(buffer[index]);
                    while (bitQueue.Count >= bitCount)
                        yield return bitQueue.DequeueBitArray(bitCount);
                }
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// <see langword="true"/> または <see langword="false"/> のランダムな <see cref="Boolean"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="Boolean"/> を要素とするシーケンスを示す <see cref="IEnumerable{Boolean}">IEnumerable&lt;<see cref="Boolean"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="SByte"/> 値を100個だけ取得します。
        /// <code>
        ///    SByte[] randomValueArray = RandomSequence.GetBooleanSequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<Boolean> GetBooleanSequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
            {
                var value = generator.GenerateRandomByte();
                yield return (value & (1U << 0)) != 0;
                yield return (value & (1U << 1)) != 0;
                yield return (value & (1U << 2)) != 0;
                yield return (value & (1U << 3)) != 0;
                yield return (value & (1U << 4)) != 0;
                yield return (value & (1U << 5)) != 0;
                yield return (value & (1U << 6)) != 0;
                yield return (value & (1U << 7)) != 0;
            }
        }

        /// <summary>
        /// ランダムな表示可能 <see cref="Char"/> ('\u000a', '\u0020'-'\u007e') を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="Char"/> を要素とするシーケンスを示す <see cref="IEnumerable{Char}">IEnumerable&lt;<see cref="Char"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな文字を100個だけ取得します。
        /// <code>
        ///    Char[] randomCharArray = RandomSequence.GetAsciiCharSequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<Char> GetAsciiCharSequence()
        {
            // 0x02.Power(33)-1 == 0x1ffffffff
            // 0x60.Power( 5)-1 == 0x1e5ffffff

            const Int32 LENGTH_OF_BIT_SET = 33;

            using var generator = RandomNumberGenerator.Create();
            var bitQueue = new BitQueue();
            while (true)
            {
                bitQueue.Enqueue(generator.GenerateRandomUInt32());
                while (bitQueue.Count >= LENGTH_OF_BIT_SET)
                {
                    var value = bitQueue.DequeueBitArray(LENGTH_OF_BIT_SET).ToUInt64();
                    yield return ToAsciiChar(value % 0x60);
                    value /= 0x60;
                    yield return ToAsciiChar(value % 0x60);
                    value /= 0x60;
                    yield return ToAsciiChar(value % 0x60);
                    value /= 0x60;
                    yield return ToAsciiChar(value % 0x60);
                    value /= 0x60;
                    yield return ToAsciiChar(value % 0x60);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Char ToAsciiChar(UInt64 x)
            {
                return
                    x == 0x5f
                    ? '\n'
                    : (Char)(x + 0x20);
            }
        }

        /// <summary>
        /// <see cref="SByte.MinValue"/ >以上、 <see cref="SByte.MaxValue"/> 以下のランダムな <see cref="SByte"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="SByte"/> を要素とするシーケンスを示す <see cref="IEnumerable{SByte}">IEnumerable&lt;<see cref="SByte"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="SByte"/> 値を100個だけ取得します。
        /// <code>
        ///    SByte[] randomValueArray = RandomSequence.GetSByteSequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<SByte> GetSByteSequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
                yield return unchecked((SByte)generator.GenerateRandomByte());
        }

        /// <summary>
        /// <see cref="Byte.MinValue"/ >以上、 <see cref="Byte.MaxValue"/> 以下のランダムな <see cref="Byte"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="Byte"/> を要素とするシーケンスを示す <see cref="IEnumerable{Byte}">IEnumerable&lt;<see cref="Byte"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="Byte"/> 値を100個だけ取得します。
        /// <code>
        ///    Byte[] randomValueArray = RandomSequence.GetByteSequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<Byte> GetByteSequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
                yield return generator.GenerateRandomByte();
        }

        /// <summary>
        /// <see cref="Int16.MinValue"/ >以上、 <see cref="Int16.MaxValue"/> 以下のランダムな <see cref="Int16"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="Int16"/> を要素とするシーケンスを示す <see cref="IEnumerable{Int16}">IEnumerable&lt;<see cref="Int16"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="Int16"/> 値を100個だけ取得します。
        /// <code>
        ///    Int16[] randomValueArray = RandomSequence.GetInt16Sequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<Int16> GetInt16Sequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
                yield return unchecked((Int16)generator.GenerateRandomUInt16());
        }

        /// <summary>
        /// <see cref="UInt16.MinValue"/ >以上、 <see cref="UInt16.MaxValue"/> 以下のランダムな <see cref="UInt16"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="UInt16"/> を要素とするシーケンスを示す <see cref="IEnumerable{UInt16}">IEnumerable&lt;<see cref="UInt16"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="UInt16"/> 値を100個だけ取得します。
        /// <code>
        ///    UInt16[] randomValueArray = RandomSequence.GetUInt16Sequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<UInt16> GetUInt16Sequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
                yield return generator.GenerateRandomUInt16();
        }

        /// <summary>
        /// <see cref="Int32.MinValue"/ >以上、 <see cref="Int32.MaxValue"/> 以下のランダムな <see cref="Int32"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="Int32"/> を要素とするシーケンスを示す <see cref="IEnumerable{Int32}">IEnumerable&lt;<see cref="Int32"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="Int32"/> 値を100個だけ取得します。
        /// <code>
        ///    Int32[] randomValueArray = RandomSequence.GetInt32Sequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<Int32> GetInt32Sequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
                yield return unchecked((Int32)generator.GenerateRandomUInt32());
        }

        /// <summary>
        /// <see cref="UInt32.MinValue"/ >以上、 <see cref="UInt32.MaxValue"/> 以下のランダムな <see cref="UInt32"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="UInt32"/> を要素とするシーケンスを示す <see cref="IEnumerable{UInt32}">IEnumerable&lt;<see cref="UInt32"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="UInt32"/> 値を100個だけ取得します。
        /// <code>
        ///    UInt32[] randomValueArray = RandomSequence.GetUInt32Sequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<UInt32> GetUInt32Sequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
                yield return generator.GenerateRandomUInt32();
        }

        /// <summary>
        /// <see cref="Int64.MinValue"/ >以上、 <see cref="Int64.MaxValue"/> 以下のランダムな <see cref="Int64"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="Int64"/> を要素とするシーケンスを示す <see cref="IEnumerable{Int64}">IEnumerable&lt;<see cref="Int64"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="Int64"/> 値を100個だけ取得します。
        /// <code>
        ///    Int64[] randomValueArray = RandomSequence.GetInt64Sequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<Int64> GetInt64Sequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
                yield return unchecked((Int64)generator.GenerateRandomUInt64());
        }

        /// <summary>
        /// <see cref="UInt64.MinValue"/ >以上、 <see cref="UInt64.MaxValue"/> 以下のランダムな <see cref="UInt64"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="UInt64"/> を要素とするシーケンスを示す <see cref="IEnumerable{UInt64}">IEnumerable&lt;<see cref="UInt64"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="UInt64"/> 値を100個だけ取得します。
        /// <code>
        ///    UInt64[] randomValueArray = RandomSequence.GetUInt64Sequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<UInt64> GetUInt64Sequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
                yield return generator.GenerateRandomUInt64();
        }

        /// <summary>
        /// <see cref="Int128.MinValue"/ >以上、 <see cref="Int128.MaxValue"/> 以下のランダムな <see cref="Int128"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="Int128"/> を要素とするシーケンスを示す <see cref="IEnumerable{Int128}">IEnumerable&lt;<see cref="Int128"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="Int128"/> 値を100個だけ取得します。
        /// <code>
        ///    Int128[] randomValueArray = RandomSequence.GetInt128Sequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<Int128> GetInt128Sequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
                yield return unchecked((Int128)generator.GenerateRandomUInt128());
        }

        /// <summary>
        /// <see cref="UInt128.MinValue"/ >以上、 <see cref="UInt128.MaxValue"/> 以下のランダムな <see cref="UInt128"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="UInt128"/> を要素とするシーケンスを示す <see cref="IEnumerable{UInt128}">IEnumerable&lt;<see cref="UInt128"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="UInt128"/> 値を100個だけ取得します。
        /// <code>
        ///    UInt128[] randomValueArray = RandomSequence.GetUInt128Sequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<UInt128> GetUInt128Sequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
                yield return generator.GenerateRandomUInt128();
        }

        /// <summary>
        /// 0 以上かつ 1 未満のランダムな <see cref="Half"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="Half"/> を要素とするシーケンスを示す <see cref="IEnumerable{Half}">IEnumerable&lt;<see cref="Half"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="Half"/> 値を100個だけ取得します。
        /// <code>
        ///    Half[] randomValueArray = RandomSequence.GetHalfSequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<Half> GetHalfSequence()
        {
#if DEBUG
            const Int32 BIT_COUNT_NUMERATOR = 11;
            const Int32 BIT_COUNT_PER_BYTE = 8;
            const Int32 WORD_COUNT = 3; // buffer0, buffer1, buffer2 を使用する
#endif
            const Int32 RANDOM_BYTE_COUNT = 11;
#if DEBUG
            Validation.Assert(BIT_COUNT_NUMERATOR * BIT_COUNT_PER_BYTE / BigInteger.GreatestCommonDivisor(BIT_COUNT_NUMERATOR, BIT_COUNT_PER_BYTE) == RANDOM_BYTE_COUNT * BIT_COUNT_PER_BYTE);
            Validation.Assert(RANDOM_BYTE_COUNT + 1 == sizeof(UInt32) * WORD_COUNT);
#endif
            var denominator = (Half)(1U << 11);
            using var generator = RandomNumberGenerator.Create();
            while (true)
            {
                // 11 ビットの numerator を計算して denominator で割った値を yield return で返すのを繰り返す。

                Span<Byte> buffer = stackalloc Byte[RANDOM_BYTE_COUNT + 1];
                generator.GetBytes(buffer[..RANDOM_BYTE_COUNT]);
                buffer[RANDOM_BYTE_COUNT] = 0; // 最後尾のバイトをクリアする
                var buffer0 = buffer[..sizeof(UInt32)].ToUInt32LE();
                var buffer1 = buffer.Slice(sizeof(UInt32) * 1, sizeof(UInt32)).ToUInt32LE();
                var buffer2 = buffer.Slice(sizeof(UInt32) * 2, sizeof(UInt32)).ToUInt32LE();
                yield return (Half)((buffer0 << 0) & 0x07ff) / denominator;
                yield return (Half)((buffer0 >> 11) & 0x07ff) / denominator;
                yield return (Half)(((buffer0 >> 22) | (buffer1 << 10)) & 0x07ff) / denominator;
                yield return (Half)((buffer1 >> 1) & 0x07ff) / denominator;
                yield return (Half)((buffer1 >> 12) & 0x07ff) / denominator;
                yield return (Half)(((buffer1 >> 23) | (buffer2 << 9)) & 0x07ff) / denominator;
                yield return (Half)((buffer2 >> 2) & 0x07ff) / denominator;
                yield return (Half)((buffer2 >> 13) & 0x07ff) / denominator;
            }
        }

        /// <summary>
        /// 0 以上かつ 1 未満のランダムな <see cref="Single"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="Single"/> を要素とするシーケンスを示す <see cref="IEnumerable{Single}">IEnumerable&lt;<see cref="Single"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="Single"/> 値を100個だけ取得します。
        /// <code>
        ///    Single[] randomValueArray = RandomSequence.GetSingleSequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<Single> GetSingleSequence()
        {
            const Int32 SIZE_OF_NUMERATOR = 3;
            const Single denominator = 1U << (SIZE_OF_NUMERATOR * 8);

            using var generator = RandomNumberGenerator.Create();
            while (true)
            {
                var numerator = GenerateNumerator(generator, SIZE_OF_NUMERATOR);
                yield return numerator / denominator;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static UInt32 GenerateNumerator(RandomNumberGenerator generator, Int32 bufferSize)
            {
#if DEBUG
                Validation.Assert(bufferSize + 1 == sizeof(UInt32));
#endif
                Span<Byte> buffer = stackalloc Byte[bufferSize + 1];
                generator.GetBytes(buffer[..bufferSize]);
                buffer[bufferSize] = 0; // 最上位バイトをクリアする
                return buffer.ToUInt32LE();
            }
        }

        /// <summary>
        /// 0 以上かつ 1 未満のランダムな <see cref="Double"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="Double"/> を要素とするシーケンスを示す <see cref="IEnumerable{Double}">IEnumerable&lt;<see cref="Double"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="Double"/> 値を100個だけ取得します。
        /// <code>
        ///    Double[] randomValueArray = RandomSequence.GetDoubleSequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<Double> GetDoubleSequence()
        {
            const Int32 SIZE_OF_NUMERATOR = 7;
            const Double denominator = 1UL << (SIZE_OF_NUMERATOR * 8);

            using var generator = RandomNumberGenerator.Create();
            while (true)
            {
                var numerator = GenerateNumerator(generator, SIZE_OF_NUMERATOR);
                yield return numerator / denominator;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static UInt64 GenerateNumerator(RandomNumberGenerator generator, Int32 bufferSize)
            {
#if DEBUG
                Validation.Assert(bufferSize + 1 == sizeof(UInt64));
#endif
                Span<Byte> buffer = stackalloc Byte[bufferSize + 1];
                generator.GetBytes(buffer[..bufferSize]);
                buffer[bufferSize] = 0; // 最上位バイトをクリアする
                return buffer.ToUInt64LE();
            }
        }

        /// <summary>
        /// 0 以上かつ 1 未満のランダムな <see cref="Decimal"/> を要素とするシーケンスを取得します。
        /// </summary>
        /// <returns>
        /// ランダムな <see cref="Decimal"/> を要素とするシーケンスを示す <see cref="IEnumerable{Decimal}">IEnumerable&lt;<see cref="Decimal"/>&gt;</see> です。
        /// </returns>
        /// <remarks>
        /// このシーケンスは終了せず永遠に続きます。
        /// 必要な長さの要素が取得出来たらシーケンスの列挙を打ち切ってください。(例: Take 拡張メソッドを使用する)
        /// </remarks>
        /// <example>
        /// 以下のコードはランダムな <see cref="Decimal"/> 値を100個だけ取得します。
        /// <code>
        ///    Decimal[] randomValueArray = RandomSequence.GetDecimalSequence().Take(100).ToArray();
        /// </code>
        /// </example>
        public static IEnumerable<Decimal> GetDecimalSequence()
        {
            using var generator = RandomNumberGenerator.Create();
            while (true)
            {
                // 94 bit 乱数整数を生成して、それが 10^28 未満であれば、10^28 で割った値を返す。
                var numerator = GenerateNumerator(generator);
                if (numerator < _denominatorOfRandomDecimalValueAsUInt128)
                {
#if DEBUG
                    checked
#endif
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine($"GetDecimalSequence(): numerator < {_denominatorOfRandomDecimalValueAsDecimal:N0}: {numerator:N0}");
#endif
                        yield return (Decimal)numerator / _denominatorOfRandomDecimalValueAsDecimal;
                    }
                }
                else
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine($"GetDecimalSequence(): numerator skipped.: {numerator:N0}");
#endif
                }
            }

            static UInt128 GenerateNumerator(RandomNumberGenerator generator)
            {
                // 94 bit 乱数整数を生成して返す。

                Span<Byte> buffer = stackalloc Byte[sizeof(UInt32) * 3];
                generator.GetBytes(buffer);
                var numerator = (UInt128)(buffer.Slice(sizeof(UInt32) * 2, sizeof(UInt32)).ToUInt32LE() >> 2);
                numerator <<= 32;
                numerator |= buffer.Slice(sizeof(UInt32) * 1, sizeof(UInt32)).ToUInt32LE();
                numerator <<= 32;
                numerator |= buffer[..sizeof(UInt32)].ToUInt32LE();
                return numerator;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Byte GenerateRandomByte(this RandomNumberGenerator generator)
        {
            Span<Byte> buffer = stackalloc Byte[sizeof(Byte)];
            generator.GetBytes(buffer);
            return buffer[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static UInt16 GenerateRandomUInt16(this RandomNumberGenerator generator)
        {
            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            generator.GetBytes(buffer);
            return buffer.ToUInt16LE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static UInt32 GenerateRandomUInt32(this RandomNumberGenerator generator)
        {
            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            generator.GetBytes(buffer);
            return buffer.ToUInt32LE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static UInt64 GenerateRandomUInt64(this RandomNumberGenerator generator)
        {
            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            generator.GetBytes(buffer);
            return buffer.ToUInt64LE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static UInt128 GenerateRandomUInt128(this RandomNumberGenerator generator)
        {
            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_UINT128];
            generator.GetBytes(buffer);
            return buffer.ToUInt128LE();
        }
    }
}
