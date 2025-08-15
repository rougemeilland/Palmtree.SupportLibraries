using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Palmtree
{
    public static partial class ByteArrayExtensions
    {
        [StructLayout(LayoutKind.Explicit)]
        private struct UnionOf128bitNumber
        {
            [FieldOffset(0)]
            public Int128 Int128Value;

            [FieldOffset(0)]
            public UInt128 UInt128Value;

            [FieldOffset(0)]
            public Decimal DecimalValue;
        }

        private const Int32 _SIZE_OF_INT128 = 16;
        private const Int32 _SIZE_OF_UINT128 = 16;
        private const Int32 _SIZE_OF_HALF = 2;
        private const Byte _BYTE_ESCAPE_CHAR = 0x1b;
        private const Byte _BYTE_AT_MARK_CHAR = 0x40;
        private const Byte _BYTE_DOLLAR_CHAR = 0x24;
        private const Byte _BYTE_AMPERSAND_CHAR = 0x26;
        private const Byte _BYTE_OPEN_PARENTHESIS_CHAR = 0x28;
        private const Byte _BYTE_B_CHAR = 0x42;
        private const Byte _BYTE_D_CHAR = 0x44;
        private const Byte _BYTE_J_CHAR = 0x4a;
        private const Byte _BYTE_I_CHAR = 0x49;

        static ByteArrayExtensions()
        {
#if DEBUG
            unsafe
            {
                Validation.Assert(_SIZE_OF_INT128 == sizeof(Int128));
                Validation.Assert(_SIZE_OF_UINT128 == sizeof(UInt128));
                Validation.Assert(_SIZE_OF_HALF == sizeof(Half));
                Validation.Assert(sizeof(UnionOf128bitNumber) == sizeof(Int128));
                Validation.Assert(sizeof(UnionOf128bitNumber) == sizeof(UInt128));
                Validation.Assert(sizeof(UnionOf128bitNumber) == sizeof(Decimal));
            }
#endif
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        #region GetBitArraySequence

        public static IEnumerable<TinyBitArray> GetBitArraySequence(this IEnumerable<Byte> source, Int32 bitCount, BitPackingDirection bitPackingDirection = BitPackingDirection.Default)
        {
            ArgumentNullException.ThrowIfNull(source);

            var bitQueue = new BitQueue();
            foreach (var data in source)
            {
                bitQueue.Enqueue(data, bitPackingDirection);
                while (bitQueue.Count >= bitCount)
                    yield return bitQueue.DequeueBitArray(bitCount);
            }

            if (bitQueue.Count > 0)
                yield return bitQueue.DequeueBitArray(bitCount);
        }

        public static IEnumerable<TinyBitArray> GetBitArraySequence(this Memory<Byte> source, Int32 bitCount, BitPackingDirection bitPackingDirection = BitPackingDirection.Default)
        {
            var bitQueue = new BitQueue();
            for (var index = 0; index < source.Length; ++index)
            {
                bitQueue.Enqueue(source.Span[index], bitPackingDirection);
                while (bitQueue.Count >= bitCount)
                    yield return bitQueue.DequeueBitArray(bitCount);
            }

            if (bitQueue.Count > 0)
                yield return bitQueue.DequeueBitArray(bitCount);
        }

        public static IEnumerable<TinyBitArray> GetBitArraySequence(this ReadOnlyMemory<Byte> source, Int32 bitCount, BitPackingDirection bitPackingDirection = BitPackingDirection.Default)
        {
            var bitQueue = new BitQueue();
            for (var index = 0; index < source.Length; ++index)
            {
                bitQueue.Enqueue(source.Span[index], bitPackingDirection);
                while (bitQueue.Count >= bitCount)
                    yield return bitQueue.DequeueBitArray(bitCount);
            }

            if (bitQueue.Count > 0)
                yield return bitQueue.DequeueBitArray(bitCount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TinyBitArray> GetBitArraySequence(this IEnumerable<Byte[]> source, Int32 bitCount, BitPackingDirection bitPackingDirection = BitPackingDirection.Default)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SelectMany(bytes => bytes).GetBitArraySequence(bitCount, bitPackingDirection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TinyBitArray> GetBitArraySequence(this IEnumerable<Memory<Byte>> source, Int32 bitCount, BitPackingDirection bitPackingDirection = BitPackingDirection.Default)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SelectMany(bytes => bytes.GetSequence()).GetBitArraySequence(bitCount, bitPackingDirection);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TinyBitArray> GetBitArraySequence(this IEnumerable<ReadOnlyMemory<Byte>> source, Int32 bitCount, BitPackingDirection bitPackingDirection = BitPackingDirection.Default)
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.SelectMany(bytes => bytes.GetSequence()).GetBitArraySequence(bitCount, bitPackingDirection);
        }

        #endregion

        #region GetByteSequence

        public static IEnumerable<Byte> GetByteSequence(this IEnumerable<TinyBitArray> source, BitPackingDirection bitPackingDirection = BitPackingDirection.Default)
        {
            ArgumentNullException.ThrowIfNull(source);

            var bitQueue = new BitQueue();
            foreach (var bitArray in source)
            {
                bitQueue.Enqueue(bitArray);
                while (bitQueue.Count > 8)
                    yield return bitQueue.DequeueByte(bitPackingDirection);
            }

            if (bitQueue.Count > 0)
                yield return bitQueue.DequeueByte(bitPackingDirection);
        }

        #endregion

        #region GuessWhichEncoding

        public static Encoding? GuessWhichEncoding(this Byte[] bytes)
        {
            ArgumentNullException.ThrowIfNull(bytes);

            return ((ReadOnlySpan<Byte>)bytes).GuessWhichEncoding();
        }

        public static Encoding? GuessWhichEncoding(this Memory<Byte> bytes)
            => ((ReadOnlySpan<Byte>)bytes.Span).GuessWhichEncoding();

        public static Encoding? GuessWhichEncoding(this ReadOnlyMemory<Byte> bytes)
            => bytes.Span.GuessWhichEncoding();

        public static Encoding? GuessWhichEncoding(this Span<Byte> bytes)
            => ((ReadOnlySpan<Byte>)bytes).GuessWhichEncoding();

        public static Encoding? GuessWhichEncoding(this ReadOnlySpan<Byte> bytes)
        {
            var len = bytes.Length;

            // UTF-16かどうかのチェック
            var isBinary = false;
            for (var index = 0; index < len; index++)
            {
                var b1 = bytes[index];
                if (b1 <= 0x06 || b1.IsAnyOf((Byte)0x7f, (Byte)0xff))
                {
                    isBinary = true;
                    if (b1 == 0x00 && index < len - 1 && bytes[index + 1] <= 0x7f)
                        return Encoding.Unicode;
                }
            }

            if (isBinary)
                return null;

            // ASCIIかどうかのチェック
            var notJapanese = true;
            for (var index = 0; index < len; index++)
            {
                if (bytes[index] is _BYTE_ESCAPE_CHAR or >= 0x80)
                {
                    notJapanese = false;
                    break;
                }
            }

            if (notJapanese)
                return Encoding.ASCII;

            // JISコードかどうかのチェック
            for (var index = 0; index < len - 2; index++)
            {
                var b1 = bytes[index];
                var b2 = bytes[index + 1];
                var b3 = bytes[index + 2];

                if (b1 == _BYTE_ESCAPE_CHAR)
                {
                    if (b2 == _BYTE_DOLLAR_CHAR && b3 == _BYTE_AT_MARK_CHAR)
                        return Encoding.GetEncoding("iso-2022-jp");//JIS_0208 1978
                    else if (b2 == _BYTE_DOLLAR_CHAR && b3 == _BYTE_B_CHAR)
                        return Encoding.GetEncoding("iso-2022-jp");//JIS_0208 1983
                    else if (b2 == _BYTE_OPEN_PARENTHESIS_CHAR && b3.IsAnyOf(_BYTE_B_CHAR, _BYTE_J_CHAR))
                        return Encoding.GetEncoding("iso-2022-jp");//JIS_ASC
                    else if (b2 == _BYTE_OPEN_PARENTHESIS_CHAR && b3 == _BYTE_I_CHAR)
                        return Encoding.GetEncoding("iso-2022-jp");//JIS_KANA
                    if (index < len - 3)
                    {
                        var b4 = bytes[index + 3];
                        if (b2 == _BYTE_DOLLAR_CHAR &&
                            b3 == _BYTE_OPEN_PARENTHESIS_CHAR &&
                            b4 == _BYTE_D_CHAR)
                        {
                            return Encoding.GetEncoding("iso-2022-jp");//JIS_0212
                        }

                        if (index < len - 5 &&
                            b2 == _BYTE_AMPERSAND_CHAR &&
                            b3 == _BYTE_AT_MARK_CHAR &&
                            b4 == _BYTE_ESCAPE_CHAR &&
                            bytes[index + 4] == _BYTE_DOLLAR_CHAR &&
                            bytes[index + 5] == _BYTE_B_CHAR)
                        {
                            return Encoding.GetEncoding("iso-2022-jp");//JIS_0208 1990
                        }
                    }
                }
            }

            // この時点で euc/shif-jis/utf-8 のいずれかしかない。
            var count_shift_jis = 0;
            var count_euc = 0;
            var count_utf8 = 0;
            for (var index = 0; index < len - 1; index++)
            {
                var b1 = bytes[index];
                var b2 = bytes[index + 1];
                if ((b1.IsBetween((Byte)0x81, (Byte)0x9f) || b1.IsBetween((Byte)0xe0, (Byte)0xfc)) &&
                    (b2.IsBetween((Byte)0x40, (Byte)0x7e) || b2.IsBetween((Byte)0x80, (Byte)0xfc)))
                {
                    //SJIS_C
                    count_shift_jis += 2;
                    ++index;
                }
            }

            for (var index = 0; index < len - 1; index++)
            {
                var b1 = bytes[index];
                var b2 = bytes[index + 1];
                if (b1.IsBetween((Byte)0xa1, (Byte)0xfe) && b2.IsBetween((Byte)0xa1, (Byte)0xfe) ||
                    b1 == 0x8e && b2.IsBetween((Byte)0xa1, (Byte)0xdf))
                {
                    //EUC_C
                    //EUC_KANA
                    count_euc += 2;
                    ++index;
                }
                else if (index < len - 2)
                {
                    if (b1 == 0x8f &&
                        b2.IsBetween((Byte)0xa1, (Byte)0xfe) &&
                        bytes[index + 2].IsBetween((Byte)0xa1, (Byte)0xfe))
                    {
                        //EUC_0212
                        count_euc += 3;
                        index += 2;
                    }
                }
            }

            for (var index = 0; index < len - 1; index++)
            {
                var b1 = bytes[index];
                var b2 = bytes[index + 1];
                if (b1.IsBetween((Byte)0xc0, (Byte)0xdf) &&
                    b2.IsBetween((Byte)0x80, (Byte)0xbf))
                {
                    //UTF8
                    count_utf8 += 2;
                    ++index;
                }
                else if (index < len - 2)
                {
                    if (b1.IsBetween((Byte)0xe0, (Byte)0xef) &&
                        b2.IsBetween((Byte)0x80, (Byte)0xbf) &&
                        bytes[index + 2].IsBetween((Byte)0x80, (Byte)0xbf))
                    {
                        //UTF8
                        count_utf8 += 3;
                        index += 2;
                    }
                }
            }

            if (count_euc > count_shift_jis && count_euc > count_utf8)
                return Encoding.GetEncoding("euc-jp"); // euc
            else if (count_shift_jis > count_euc && count_shift_jis > count_utf8)
                return Encoding.GetEncoding("shift_jis");// shift_jis
            else if (count_utf8 > count_euc && count_utf8 > count_shift_jis)
                return Encoding.UTF8; // utf8
            else
                return null;
        }

        #endregion
    }
}
