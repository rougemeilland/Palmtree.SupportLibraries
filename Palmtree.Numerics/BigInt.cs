using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Palmtree.Numerics
{
    public readonly struct BigInt
        : ISpanFormattable, IComparable, IComparable<BigInt>, IEquatable<BigInt>, IBinaryInteger<BigInt>, ISignedNumber<BigInt>
    {
        public static readonly BigInt One;
        public static readonly BigInt MinusOne;
        public static readonly BigInt Zero;

        private static readonly BigInt AdditiveIdentity;
        private static readonly BigInt MultiplicativeIdentity;
        private static readonly Int32 Radix;

        internal readonly SignedIntegerCapsule<BigInteger> Value;

        #region constructor

        static BigInt()
        {
            Zero = new BigInt(SignedIntegerCapsule<BigInteger>.ZeroValue);
            One = new BigInt(SignedIntegerCapsule<BigInteger>.OneValue);
            MinusOne = new BigInt(SignedIntegerCapsule<BigInteger>.NegativeOneValue);
            MultiplicativeIdentity = new BigInt(SignedIntegerCapsule<BigInteger>.MultiplicativeIdentityValue);
            AdditiveIdentity = new BigInt(SignedIntegerCapsule<BigInteger>.AdditiveIdentityValue);
            Radix = SignedIntegerCapsule<BigInteger>.RadixValue;
        }

        public BigInt()
            : this(new SignedIntegerCapsule<BigInteger>(BigInteger.Zero))
        {
        }

        public BigInt(Int32 value)
            : this(new SignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public BigInt(UInt32 value)
            : this(new SignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public BigInt(Int64 value)
            : this(new SignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public BigInt(UInt64 value)
            : this(new SignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

#if NET7_0_OR_GREATER
        public BigInt(Int128 value)
            : this(new SignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public BigInt(UInt128 value)
            : this(new SignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }
#endif // NET7_0_OR_GREATER

        public BigInt(Single value)
            : this(new SignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public BigInt(Double value)
            : this(new SignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public BigInt(Decimal value)
            : this(new SignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public BigInt(BigInteger value)
            : this(new SignedIntegerCapsule<BigInteger>(value))
        {
        }

        public BigInt(ReadOnlyMemory<Byte> value, Boolean isBigEndian = false)
            : this(CreateInstance(value.Span, false, isBigEndian) ?? throw new ArgumentException("Invalid binary format.", nameof(value)))
        {
        }

        public BigInt(ReadOnlySpan<Byte> value, Boolean isBigEndian = false)
            : this(CreateInstance(value, false, isBigEndian) ?? throw new ArgumentException("Invalid binary format.", nameof(value)))
        {
        }

        private BigInt(SignedIntegerCapsule<BigInteger> value)
        {
            Value = value;
        }

        #endregion

        #region properties

        public Boolean IsEven
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Value.NativeValue.IsEven;
        }

        public Boolean IsOne
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Value.NativeValue.IsOne;
        }

        public Boolean IsZero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Value.NativeValue.IsZero;
        }

        public Boolean IsPowerOfTwo
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Value.NativeValue.IsPowerOfTwo;
        }

        public Int32 Sign
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Value.NativeValue.Sign;
        }

        #endregion

        #region methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BigInt Add(BigInt other) => new(Value.NativeValue + other.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Object? other)
        {
            if (other is null)
                return 1;
            else if (other.GetType() == typeof(UBigInt))
                return CompareTo((UBigInt)other);
            else if (other.GetType() == typeof(BigInt))
                return CompareTo((BigInt)other);
            else
                return Value.NativeValue.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(BigInt other) => Value.NativeValue.CompareTo(other.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(UBigInt other) => Value.NativeValue.CompareTo(other.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(BigInteger other) => Value.NativeValue.CompareTo(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(UInt64 other) => Value.NativeValue.CompareTo(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Int64 other) => Value.NativeValue.CompareTo(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Boolean Equals(Object? other)
        {
            if (other is null)
                return false;
            else if (other.GetType() == typeof(BigInt))
                return Equals((BigInt)other);
            else if (other.GetType() == typeof(UBigInt))
                return Equals((UBigInt)other);
            else
                return Value.NativeValue.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(BigInt other) => Value.NativeValue.Equals(other.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(UBigInt other) => Value.NativeValue.Equals(other.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(BigInteger other) => Value.NativeValue.Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(UInt64 other) => Value.NativeValue.Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(Int64 other) => Value.NativeValue.Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int64 GetBitLength() => Value.NativeValue.GetBitLength();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 GetByteCount(Boolean isUnsigned = false) => Value.NativeValue.GetByteCount(isUnsigned);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode() => Value.NativeValue.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Byte[] ToByteArray() => Value.NativeValue.ToByteArray();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Byte[] ToByteArray(Boolean isBigEndian = false) => Value.NativeValue.ToByteArray(false, isBigEndian);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format, IFormatProvider? provider) => Value.NativeValue.ToString(format, provider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format) => Value.NativeValue.ToString(format);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(IFormatProvider? provider) => Value.NativeValue.ToString(provider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override String ToString() => Value.NativeValue.ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryFormat(Span<Char> destination, out Int32 charsWritten, ReadOnlySpan<Char> format = default, IFormatProvider? provider = default) => Value.NativeValue.TryFormat(destination, out charsWritten, format, provider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryWriteBytes(Span<Byte> destination, out Int32 bytesWritten, Boolean isUnsigned = false, Boolean isBigEndian = false) => Value.NativeValue.TryWriteBytes(destination, out bytesWritten, isUnsigned, isBigEndian);

        #endregion

        #region static methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Abs(BigInt value) => new(BigInteger.Abs(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Clamp(BigInt value, BigInt min, BigInt max) => new(BigInteger.Clamp(value.Value.NativeValue, min.Value.NativeValue, max.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Compare(BigInt left, BigInt right) => BigInteger.Compare(left.Value.NativeValue, right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt CopySign(BigInt value, BigInt sign) => new(BigInteger.CopySign(value.Value.NativeValue, sign.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt CreateChecked<TOther>(TOther value)
            where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(BigInt))
                return (BigInt)(Object)value;
            else if (typeof(TOther) == typeof(UBigInt))
                return new BigInt(((UBigInt)(Object)value).Value.NativeValue);
            else
                return new BigInt(BigInteger.CreateChecked(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt CreateSaturating<TOther>(TOther value)
            where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(BigInt))
                return (BigInt)(Object)value;
            else if (typeof(TOther) == typeof(UBigInt))
                return new BigInt(((UBigInt)(Object)value).Value.NativeValue);
            else
                return new BigInt(BigInteger.CreateSaturating(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt CreateTruncating<TOther>(TOther value)
            where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(BigInt))
                return (BigInt)(Object)value;
            else if (typeof(TOther) == typeof(UBigInt))
                return new BigInt(((UBigInt)(Object)value).Value.NativeValue);
            else
                return new BigInt(BigInteger.CreateTruncating(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Divide(BigInt dividend, BigInt divisor) => new(BigInteger.Divide(dividend.Value.NativeValue, divisor.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (BigInt Quotient, BigInt Remainder) DivRem(BigInt left, BigInt right)
        {
            var (quotient, remainder) = BigInteger.DivRem(left.Value.NativeValue, right.Value.NativeValue);
            return (new BigInt(quotient), new BigInt(remainder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt DivRem(BigInt dividend, BigInt divisor, out BigInt remainder)
        {
            var quotient = BigInteger.DivRem(dividend.Value.NativeValue, divisor.Value.NativeValue, out var r);
            remainder = new BigInt(r);
            return new BigInt(quotient);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt GreatestCommonDivisor(BigInt left, BigInt right) => new(BigInteger.GreatestCommonDivisor(left.Value.NativeValue, right.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEvenInteger(BigInt value) => BigInteger.IsEvenInteger(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(BigInt value) => BigInteger.IsNegative(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOddInteger(BigInt value) => BigInteger.IsOddInteger(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(BigInt value) => BigInteger.IsPositive(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPow2(BigInt value) => BigInteger.IsPow2(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt LeadingZeroCount(BigInt value) => new(BigInteger.LeadingZeroCount(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(BigInt value) => BigInteger.Log(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(BigInt value) => BigInteger.Log10(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Log2(BigInt value) => new(BigInteger.Log2(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Max(BigInt left, BigInt right) => new(BigInteger.Max(left.Value.NativeValue, right.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt MaxMagnitude(BigInt x, BigInt y) => new(BigInteger.MaxMagnitude(x.Value.NativeValue, y.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Min(BigInt left, BigInt right) => new(BigInteger.Min(left.Value.NativeValue, right.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt MinMagnitude(BigInt x, BigInt y) => new(BigInteger.MinMagnitude(x.Value.NativeValue, y.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt ModPow(BigInt value, BigInt exponent, BigInt modulus) => new(BigInteger.ModPow(value.Value.NativeValue, exponent.Value.NativeValue, modulus.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Multiply(BigInt left, BigInt right) => new(BigInteger.Multiply(left.Value.NativeValue, right.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Negate(BigInt value) => new(BigInteger.Negate(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Parse(String value) => new(BigInteger.Parse(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Parse(ReadOnlySpan<Char> s, IFormatProvider? provider) => new(BigInteger.Parse(s, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Parse(String value, NumberStyles style) => new(BigInteger.Parse(value, style));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Parse(String value, IFormatProvider? provider) => new(BigInteger.Parse(value, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Parse(ReadOnlySpan<Char> value, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default) => new(BigInteger.Parse(value, style, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Parse(String value, NumberStyles style, IFormatProvider? provider) => new(BigInteger.Parse(value, style, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt PopCount(BigInt value) => new(BigInteger.PopCount(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Pow(BigInt value, Int32 exponent) => new(BigInteger.Pow(value.Value.NativeValue, exponent));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Remainder(BigInt dividend, BigInt divisor) => new(BigInteger.Remainder(dividend.Value.NativeValue, divisor.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt RotateLeft(BigInt value, Int32 rotateAmount) => new(BigInteger.RotateLeft(value.Value.NativeValue, rotateAmount));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt RotateRight(BigInt value, Int32 rotateAmount) => new(BigInteger.RotateRight(value.Value.NativeValue, rotateAmount));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt Subtract(BigInt left, BigInt right) => new(BigInteger.Subtract(left.Value.NativeValue, right.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt TrailingZeroCount(BigInt value) => new(BigInteger.TrailingZeroCount(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider, out BigInt result)
        {
            if (!BigInteger.TryParse(value, style, provider, out var r))
            {
                result = default;
                return false;
            }

            result = new BigInt(r);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String? s, IFormatProvider? provider, out BigInt result)
        {
            if (!BigInteger.TryParse(s, provider, out var r))
            {
                result = default;
                return false;
            }

            result = new BigInt(r);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> s, IFormatProvider? provider, out BigInt result)
        {
            if (!BigInteger.TryParse(s, provider, out var r))
            {
                result = default;
                return false;
            }

            result = new BigInt(r);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String? value, out BigInt result)
        {
            if (!BigInteger.TryParse(value, out var r))
            {
                result = default;
                return false;
            }

            result = new BigInt(r);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> value, out BigInt result)
        {
            if (!BigInteger.TryParse(value, out var r))
            {
                result = default;
                return false;
            }

            result = new BigInt(r);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String? value, NumberStyles style, IFormatProvider? provider, out BigInt result)
        {
            if (!BigInteger.TryParse(value, style, provider, out var r))
            {
                result = default;
                return false;
            }

            result = new BigInt(r);
            return true;
        }

        #endregion

        #region explicit interface members

        static BigInt INumberBase<BigInt>.One => One;
        static Int32 INumberBase<BigInt>.Radix => Radix;
        static BigInt INumberBase<BigInt>.Zero => Zero;
        static BigInt IAdditiveIdentity<BigInt, BigInt>.AdditiveIdentity => AdditiveIdentity;
        static BigInt IMultiplicativeIdentity<BigInt, BigInt>.MultiplicativeIdentity => MultiplicativeIdentity;
        static BigInt ISignedNumber<BigInt>.NegativeOne => MinusOne;
        Int32 IBinaryInteger<BigInt>.GetByteCount() => Value.GetByteCount();
        Int32 IBinaryInteger<BigInt>.GetShortestBitLength() => Value.GetShortestBitLength();
        Boolean IBinaryInteger<BigInt>.TryWriteBigEndian(Span<Byte> destination, out Int32 bytesWritten) => Value.TryWriteBigEndian(destination, out bytesWritten);
        Boolean IBinaryInteger<BigInt>.TryWriteLittleEndian(Span<Byte> destination, out Int32 bytesWritten) => Value.TryWriteLittleEndian(destination, out bytesWritten);
        static Boolean IBinaryInteger<BigInt>.TryReadBigEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out BigInt value)
        {
            if (!SignedIntegerCapsule<BigInteger>.TryReadBigEndian(source, isUnsigned, out var x))
            {
                value = default;
                return false;
            }

            value = new BigInt(x);
            return true;
        }

        static Boolean IBinaryInteger<BigInt>.TryReadLittleEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out BigInt value)
        {
            if (!SignedIntegerCapsule<BigInteger>.TryReadLittleEndian(source, isUnsigned, out var x))
            {
                value = default;
                return false;
            }

            value = new BigInt(x);
            return true;
        }

        static Boolean INumberBase<BigInt>.IsCanonical(BigInt value) => SignedIntegerCapsule<BigInteger>.IsCanonical(value.Value);
        static Boolean INumberBase<BigInt>.IsComplexNumber(BigInt value) => SignedIntegerCapsule<BigInteger>.IsComplexNumber(value.Value);
        static Boolean INumberBase<BigInt>.IsFinite(BigInt value) => SignedIntegerCapsule<BigInteger>.IsFinite(value.Value);
        static Boolean INumberBase<BigInt>.IsImaginaryNumber(BigInt value) => SignedIntegerCapsule<BigInteger>.IsImaginaryNumber(value.Value);
        static Boolean INumberBase<BigInt>.IsInfinity(BigInt value) => SignedIntegerCapsule<BigInteger>.IsInfinity(value.Value);
        static Boolean INumberBase<BigInt>.IsInteger(BigInt value) => SignedIntegerCapsule<BigInteger>.IsInteger(value.Value);
        static Boolean INumberBase<BigInt>.IsNaN(BigInt value) => SignedIntegerCapsule<BigInteger>.IsNaN(value.Value);
        static Boolean INumberBase<BigInt>.IsNegativeInfinity(BigInt value) => SignedIntegerCapsule<BigInteger>.IsNegativeInfinity(value.Value);
        static Boolean INumberBase<BigInt>.IsNormal(BigInt value) => SignedIntegerCapsule<BigInteger>.IsNormal(value.Value);
        static Boolean INumberBase<BigInt>.IsPositiveInfinity(BigInt value) => SignedIntegerCapsule<BigInteger>.IsPositiveInfinity(value.Value);
        static Boolean INumberBase<BigInt>.IsRealNumber(BigInt value) => SignedIntegerCapsule<BigInteger>.IsRealNumber(value.Value);
        static Boolean INumberBase<BigInt>.IsSubnormal(BigInt value) => SignedIntegerCapsule<BigInteger>.IsSubnormal(value.Value);
        static Boolean INumberBase<BigInt>.IsZero(BigInt value) => SignedIntegerCapsule<BigInteger>.IsZero(value.Value);
        static BigInt INumberBase<BigInt>.MaxMagnitudeNumber(BigInt x, BigInt y) => new(SignedIntegerCapsule<BigInteger>.MaxMagnitudeNumber(x.Value, y.Value));
        static BigInt INumberBase<BigInt>.MinMagnitudeNumber(BigInt x, BigInt y) => new(SignedIntegerCapsule<BigInteger>.MinMagnitudeNumber(x.Value, y.Value));

        static Boolean INumberBase<BigInt>.TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out BigInt result)
        {
            if (!SignedIntegerCapsule<BigInteger>.TryConvertFromChecked(value, out var r))
            {
                result = default;
                return false;
            }

            result = new BigInt(r);
            return true;
        }

        static Boolean INumberBase<BigInt>.TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out BigInt result)
        {
            if (!SignedIntegerCapsule<BigInteger>.TryConvertFromSaturating(value, out var r))
            {
                result = default;
                return false;
            }

            result = new BigInt(r);
            return true;
        }

        static Boolean INumberBase<BigInt>.TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out BigInt result)
        {
            if (!SignedIntegerCapsule<BigInteger>.TryConvertFromTruncating(value, out var r))
            {
                result = default;
                return false;
            }

            result = new BigInt(r);
            return true;
        }

        static Boolean INumberBase<BigInt>.TryConvertToChecked<TOther>(BigInt value, [MaybeNullWhen(false)] out TOther result) => SignedIntegerCapsule<BigInteger>.TryConvertToChecked(value.Value, out result);
        static Boolean INumberBase<BigInt>.TryConvertToSaturating<TOther>(BigInt value, [MaybeNullWhen(false)] out TOther result) => SignedIntegerCapsule<BigInteger>.TryConvertToSaturating(value.Value, out result);
        static Boolean INumberBase<BigInt>.TryConvertToTruncating<TOther>(BigInt value, [MaybeNullWhen(false)] out TOther result) => SignedIntegerCapsule<BigInteger>.TryConvertToTruncating(value.Value, out result);

        #endregion

        #region operators

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator +(BigInt left, BigInt right) => new(left.Value.NativeValue + right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator -(BigInt left, BigInt right) => new(left.Value.NativeValue - right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator *(BigInt left, BigInt right) => new(left.Value.NativeValue * right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator /(BigInt dividend, BigInt divisor) => new(dividend.Value.NativeValue / divisor.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator %(BigInt dividend, BigInt divisor) => new(dividend.Value.NativeValue % divisor.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator +(BigInt value) => new(+value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator -(BigInt value) => new(-value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator ++(BigInt value)
        {
            var x = value.Value.NativeValue;
            return new BigInt(++x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator --(BigInt value)
        {
            var x = value.Value.NativeValue;
            return new BigInt(--x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator &(BigInt left, BigInt right) => new(left.Value.NativeValue & right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator |(BigInt left, BigInt right) => new(left.Value.NativeValue | right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator ^(BigInt left, BigInt right) => new(left.Value.NativeValue ^ right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator ~(BigInt value) => new(~value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator <<(BigInt value, Int32 shift) => new(value.Value.NativeValue << shift);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator >>(BigInt value, Int32 shift) => new(value.Value.NativeValue >> shift);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInt operator >>>(BigInt value, Int32 shift) => new(value.Value.NativeValue >>> shift);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(BigInt left, BigInt right) => left.Value.NativeValue == right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(BigInt left, UBigInt right) => left.Value.NativeValue == right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(BigInt left, BigInteger right) => left.Value.NativeValue == right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(BigInt left, UInt64 right) => left.Value.NativeValue == right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(BigInt left, Int64 right) => left.Value.NativeValue == right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(UBigInt left, BigInt right) => left.Value.NativeValue == right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(BigInteger left, BigInt right) => left == right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(UInt64 left, BigInt right) => left == right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(Int64 left, BigInt right) => left == right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(BigInt left, BigInt right) => left.Value.NativeValue != right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(BigInt left, UBigInt right) => left.Value.NativeValue != right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(BigInt left, BigInteger right) => left.Value.NativeValue != right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(BigInt left, UInt64 right) => left.Value.NativeValue != right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(BigInt left, Int64 right) => left.Value.NativeValue != right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(UBigInt left, BigInt right) => left.Value.NativeValue != right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(BigInteger left, BigInt right) => left != right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(UInt64 left, BigInt right) => left != right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(Int64 left, BigInt right) => left != right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(BigInt left, BigInt right) => left.Value.NativeValue > right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(BigInt left, UBigInt right) => left.Value.NativeValue > right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(BigInt left, BigInteger right) => left.Value.NativeValue > right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(BigInt left, UInt64 right) => left.Value.NativeValue > right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(BigInt left, Int64 right) => left.Value.NativeValue > right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(UBigInt left, BigInt right) => left.Value.NativeValue > right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(BigInteger left, BigInt right) => left > right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(UInt64 left, BigInt right) => left > right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(Int64 left, BigInt right) => left > right.Value.NativeValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(BigInt left, BigInt right) => left.Value.NativeValue < right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(BigInt left, UBigInt right) => left.Value.NativeValue < right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(BigInt left, BigInteger right) => left.Value.NativeValue < right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(BigInt left, UInt64 right) => left.Value.NativeValue < right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(BigInt left, Int64 right) => left.Value.NativeValue < right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(UBigInt left, BigInt right) => left.Value.NativeValue < right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(BigInteger left, BigInt right) => left < right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(UInt64 left, BigInt right) => left < right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(Int64 left, BigInt right) => left < right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(BigInt left, BigInt right) => left.Value.NativeValue >= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(BigInt left, UBigInt right) => left.Value.NativeValue >= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(BigInt left, BigInteger right) => left.Value.NativeValue >= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(BigInt left, UInt64 right) => left.Value.NativeValue >= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(BigInt left, Int64 right) => left.Value.NativeValue >= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(UBigInt left, BigInt right) => left.Value.NativeValue >= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(BigInteger left, BigInt right) => left >= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(UInt64 left, BigInt right) => left >= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(Int64 left, BigInt right) => left >= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(BigInt left, BigInt right) => left.Value.NativeValue <= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(BigInt left, UBigInt right) => left.Value.NativeValue <= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(BigInt left, BigInteger right) => left.Value.NativeValue <= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(BigInt left, UInt64 right) => left.Value.NativeValue <= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(BigInt left, Int64 right) => left.Value.NativeValue <= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(UBigInt left, BigInt right) => left.Value.NativeValue <= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(BigInteger left, BigInt right) => left <= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(UInt64 left, BigInt right) => left <= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(Int64 left, BigInt right) => left <= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Char(BigInt value) => (Char)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator SByte(BigInt value) => (SByte)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Byte(BigInt value) => (Byte)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int16(BigInt value) => (Int16)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UInt16(BigInt value) => (UInt16)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int32(BigInt value) => (Int32)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UInt32(BigInt value) => (UInt32)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int64(BigInt value) => (Int64)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UInt64(BigInt value) => (UInt64)value.Value.NativeValue;

#if NET7_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int128(BigInt value) => (Int128)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UInt128(BigInt value) => (UInt128)value.Value.NativeValue;
#endif // NET7_0_OR_GREATER

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Single(BigInt value) => (Single)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Double(BigInt value) => (Double)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Decimal(BigInt value) => (Decimal)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UBigInt(BigInt value) => new(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator BigInt(Half value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator BigInt(Single value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator BigInt(Double value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator BigInt(Decimal value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(Char value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(SByte value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(Byte value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(Int16 value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(UInt16 value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(Int32 value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(UInt32 value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(Int64 value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(UInt64 value) => new(value);

#if NET7_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(Int128 value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(UInt128 value) => new(value);
#endif // NET7_0_OR_GREATER

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(UBigInt value) => new(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInt(BigInteger value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInteger(BigInt value) => value.Value.NativeValue;

        #endregion

        #region private methods

        private static SignedIntegerCapsule<BigInteger>? CreateInstance(ReadOnlySpan<Byte> value, Boolean isUnsigned, Boolean isBigEndian)
        {
            if (isBigEndian)
                return SignedIntegerCapsule<BigInteger>.TryReadBigEndian(value, isUnsigned, out var result) ? result : null;
            else
                return SignedIntegerCapsule<BigInteger>.TryReadLittleEndian(value, isUnsigned, out var result) ? result : null;
        }

        #endregion
    }
}
