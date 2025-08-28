using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Palmtree;

namespace Test.Palmtree.Core
{
    public sealed partial class TestArrayExtensions
    {
        private const String _charMap = " !#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        private static readonly Int32[] _listOfLength = [0, 1, 6, 7, 8, 9, 14, 15, 16, 17, 30, 31, 32, 33, 62, 63, 64, 65];
        private static readonly RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

        private static void FillRandomData<ELEMENT_T>(Span<ELEMENT_T> buffer)
            where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            if (typeof(ELEMENT_T) == typeof(Single))
            {
                FillSingleRandomData(buffer);
            }
            else if (typeof(ELEMENT_T) == typeof(Double))
            {
                FillDoubleRandomData(buffer);
            }
            else if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                if (Unsafe.SizeOf<NFloat>() == sizeof(Single))
                    FillSingleRandomData(buffer);
                else if (Unsafe.SizeOf<NFloat>() == sizeof(Double))
                    FillDoubleRandomData(buffer);
                else
                    throw Validation.GetFatalErrorException();
            }
            else if (typeof(ELEMENT_T) == typeof(Decimal))
            {
                FillDecimalRandomData(buffer);
            }
            else if (typeof(ELEMENT_T) == typeof(BigInteger))
            {
                // 256bit の BigInteger 乱数配列を生成する

                Span<Byte> tempBuffer = stackalloc Byte[256 / 8 + 1];
                for (var index = 0; index < buffer.Length; ++index)
                {
                    _randomNumberGenerator.GetBytes(tempBuffer[..^1]);
                    tempBuffer[^1] = 0;
                    buffer[index] = ELEMENT_T.CreateChecked(new BigInteger(tempBuffer));
                }
            }
            else if (IsBitwiseNumber<ELEMENT_T>())
            {
                FillRandomDataCore(buffer);
            }
            else
            {
                throw Validation.GetFatalErrorException();
            }

            static void FillSingleRandomData(Span<ELEMENT_T> buffer)
            {
                const Single denominator = 1U << 31;
                var source = new UInt32[buffer.Length];
                FillRandomDataCore(source.AsSpan());
                for (var index = 0; index < buffer.Length; ++index)
                    buffer[index] = ELEMENT_T.CreateTruncating((source[index] >> 1) / denominator);
            }

            static void FillDoubleRandomData(Span<ELEMENT_T> buffer)
            {
                const Double denominator = 1UL << 63;
                var source = new UInt64[buffer.Length];
                FillRandomDataCore(source.AsSpan());
                for (var index = 0; index < buffer.Length; ++index)
                    buffer[index] = ELEMENT_T.CreateTruncating((source[index] >> 1) / denominator);
            }

            static void FillDecimalRandomData(Span<ELEMENT_T> buffer)
            {
                // 10,000,000,000,000,000,000,000,000,000.000 == 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 10 == 10^28 == 0x204fce5e3e25026110000000 < 2^94
                const Decimal decimalDenominator = 1000m * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 10;
                var uint128Denominator = (UInt128)1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 1000 * 10;
                Validation.Assert(decimalDenominator > (Decimal)(UInt128.One << 93));
                Validation.Assert(decimalDenominator < (Decimal)(UInt128.One << 94));

                checked
                {
                    Validation.Assert(decimalDenominator == (Decimal)uint128Denominator);
                }

                var source = new UInt32[3]; // 96 bits
                for (var index = 0; index < buffer.Length;)
                {
                    FillRandomDataCore(source.AsSpan());
                    var element = ((UInt128)source[0] << (0 * 32)) | ((UInt128)source[1] << (1 * 32)) | ((UInt128)source[2] << (2 * 32));
                    element >>= 2;

                    Validation.Assert(element < (UInt128.One << 94));

                    if (element < uint128Denominator)
                    {
                        buffer[index] = ELEMENT_T.CreateTruncating((Decimal)element / decimalDenominator);
                        ++index;
                    }
                }
            }
        }

        private static void FillRandomString(Span<String> buffer)
        {
            Span<Byte> tempBuffer = stackalloc Byte[14 + 1];
            Span<Char> charBuffer = stackalloc Char[16];
            for (var index = 0; index < buffer.Length; ++index)
            {
                _randomNumberGenerator.GetBytes(tempBuffer[..^1]);
                tempBuffer[^1] = 0;
                var n = new BigInteger(tempBuffer);
                for (var charIndex = 0; charIndex < charBuffer.Length; ++charIndex)
                {
                    var (q, r) = BigInteger.DivRem(n, _charMap.Length);
                    charBuffer[charIndex] = _charMap[Int32.CreateChecked(r)];
                    n = q;
                }

                buffer[index] = new String(charBuffer);
            }
        }

        private static void FillRandomDataCore<ELEMENT_T>(Span<ELEMENT_T> buffer)
            where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            Validation.Assert(IsBitwiseNumber<ELEMENT_T>() == true);
            var byteBuffer = new Byte[buffer.Length * Unsafe.SizeOf<ELEMENT_T>()];
            _randomNumberGenerator.GetBytes(byteBuffer);
            for (var index = 0; index < buffer.Length; ++index)
            {
                if (Unsafe.SizeOf<ELEMENT_T>() == 1)
                    buffer[index] = ELEMENT_T.CreateTruncating(byteBuffer[index]);
                else if (Unsafe.SizeOf<ELEMENT_T>() == 2)
                    buffer[index] = ELEMENT_T.CreateTruncating(BinaryPrimitives.ReadUInt16LittleEndian(byteBuffer.AsSpan(index * 2)));
                else if (Unsafe.SizeOf<ELEMENT_T>() == 4)
                    buffer[index] = ELEMENT_T.CreateTruncating(BinaryPrimitives.ReadUInt32LittleEndian(byteBuffer.AsSpan(index * 4)));
                else if (Unsafe.SizeOf<ELEMENT_T>() == 8)
                    buffer[index] = ELEMENT_T.CreateTruncating(BinaryPrimitives.ReadUInt64LittleEndian(byteBuffer.AsSpan(index * 8)));
                else if (Unsafe.SizeOf<ELEMENT_T>() == 16)
                    buffer[index] = ELEMENT_T.CreateTruncating(BinaryPrimitives.ReadUInt128LittleEndian(byteBuffer.AsSpan(index * 16)));
                else
                    throw new Exception("Unexpected.");
            }
        }

        private static String FormatArray<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> buffer)
        {
            var result = new List<String>();
            foreach (var value in buffer)
                result.Add($"{value}");

            return $"[{String.Join(", ", result)}]";
        }

        private static Boolean IsBitwiseNumber<VALUE_T>()
            => typeof(VALUE_T) == typeof(SByte)
                || typeof(VALUE_T) == typeof(Byte)
                || typeof(VALUE_T) == typeof(Int16)
                || typeof(VALUE_T) == typeof(UInt16)
                || typeof(VALUE_T) == typeof(Char)
                || typeof(VALUE_T) == typeof(Int32)
                || typeof(VALUE_T) == typeof(UInt32)
                || typeof(VALUE_T) == typeof(Int64)
                || typeof(VALUE_T) == typeof(UInt64)
                || typeof(VALUE_T) == typeof(Int128)
                || typeof(VALUE_T) == typeof(UInt128)
                || typeof(VALUE_T) == typeof(IntPtr)
                || typeof(VALUE_T) == typeof(UIntPtr);
    }
}
