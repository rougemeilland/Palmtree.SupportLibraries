using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Palmtree.Numerics
{
    public readonly struct UBigInt
        : ISpanFormattable, IComparable, IComparable<UBigInt>, IEquatable<UBigInt>, IBinaryInteger<UBigInt>, ISignedNumber<UBigInt>
    {
        public static readonly UBigInt One;
        public static readonly UBigInt Zero;

        private static readonly UBigInt AdditiveIdentity;
        private static readonly UBigInt MultiplicativeIdentity;
        private static readonly Int32 Radix;

        internal readonly UnsignedIntegerCapsule<BigInteger> Value;

        #region constructor

        static UBigInt()
        {
            Zero = new UBigInt(UnsignedIntegerCapsule<BigInteger>.ZeroValue);
            One = new UBigInt(UnsignedIntegerCapsule<BigInteger>.OneValue);
            MultiplicativeIdentity = new UBigInt(UnsignedIntegerCapsule<BigInteger>.MultiplicativeIdentityValue);
            AdditiveIdentity = new UBigInt(UnsignedIntegerCapsule<BigInteger>.AdditiveIdentityValue);
            Radix = UnsignedIntegerCapsule<BigInteger>.RadixValue;
        }

        public UBigInt()
            : this(new UnsignedIntegerCapsule<BigInteger>(BigInteger.Zero))
        {
        }

        public UBigInt(Int32 value)
            : this(new UnsignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public UBigInt(UInt32 value)
            : this(new UnsignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public UBigInt(Int64 value)
            : this(new UnsignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public UBigInt(UInt64 value)
            : this(new UnsignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

#if NET7_0_OR_GREATER
        public UBigInt(Int128 value)
            : this(new UnsignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public UBigInt(UInt128 value)
            : this(new UnsignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }
#endif // NET7_0_OR_GREATER

        public UBigInt(Single value)
            : this(new UnsignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public UBigInt(Double value)
            : this(new UnsignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public UBigInt(Decimal value)
            : this(new UnsignedIntegerCapsule<BigInteger>((BigInteger)value))
        {
        }

        public UBigInt(BigInteger value)
            : this(new UnsignedIntegerCapsule<BigInteger>(value))
        {
        }

        public UBigInt(ReadOnlyMemory<Byte> value, Boolean isBigEndian = false)
            : this(CreateInstance(value.Span, false, isBigEndian) ?? throw new ArgumentException("Invalid binary format.", nameof(value)))
        {
        }

        public UBigInt(ReadOnlySpan<Byte> value, Boolean isBigEndian = false)
            : this(CreateInstance(value, true, isBigEndian) ?? throw new ArgumentException("Invalid binary format.", nameof(value)))
        {
        }

        private UBigInt(UnsignedIntegerCapsule<BigInteger> value)
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

        #endregion

        #region methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UBigInt Add(UBigInt other) => new(Value.NativeValue + other.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Object? other)
        {
            if (other is null)
                return 1;
            if (other.GetType() == typeof(BigInt))
                return CompareTo((BigInt)other);
            else if (other.GetType() == typeof(UBigInt))
                return CompareTo((UBigInt)other);
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
        public Byte[] ToByteArray(Boolean isBigEndian = false) => Value.NativeValue.ToByteArray(true, isBigEndian);

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
        public static UBigInt Abs(UBigInt value) => new(BigInteger.Abs(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Clamp(UBigInt value, UBigInt min, UBigInt max) => new(BigInteger.Clamp(value.Value.NativeValue, min.Value.NativeValue, max.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Compare(UBigInt left, UBigInt right) => BigInteger.Compare(left.Value.NativeValue, right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt CreateChecked<TOther>(TOther value)
            where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(BigInt))
                return new UBigInt(((BigInt)(Object)value).Value.NativeValue);
            else if (typeof(TOther) == typeof(UBigInt))
                return (UBigInt)(Object)value;
            else
                return new UBigInt(BigInteger.CreateChecked(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt CreateSaturating<TOther>(TOther value)
            where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(BigInt))
                return new UBigInt(((BigInt)(Object)value).Value.NativeValue);
            else if (typeof(TOther) == typeof(UBigInt))
                return (UBigInt)(Object)value;
            else
                return new UBigInt(BigInteger.CreateSaturating(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt CreateTruncating<TOther>(TOther value)
            where TOther : INumberBase<TOther>
        {
            if (typeof(TOther) == typeof(BigInt))
                return new UBigInt(((BigInt)(Object)value).Value.NativeValue);
            else if (typeof(TOther) == typeof(UBigInt))
                return (UBigInt)(Object)value;
            else
                return new UBigInt(BigInteger.CreateTruncating(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Divide(UBigInt dividend, UBigInt divisor) => new(BigInteger.Divide(dividend.Value.NativeValue, divisor.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (UBigInt Quotient, UBigInt Remainder) DivRem(UBigInt left, UBigInt right)
        {
            var (quotient, remainder) = BigInteger.DivRem(left.Value.NativeValue, right.Value.NativeValue);
            return (new UBigInt(quotient), new UBigInt(remainder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt DivRem(UBigInt dividend, UBigInt divisor, out UBigInt remainder)
        {
            var quotient = BigInteger.DivRem(dividend.Value.NativeValue, divisor.Value.NativeValue, out var r);
            remainder = new UBigInt(r);
            return new UBigInt(quotient);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt GreatestCommonDivisor(UBigInt left, UBigInt right) => new(BigInteger.GreatestCommonDivisor(left.Value.NativeValue, right.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEvenInteger(UBigInt value) => BigInteger.IsEvenInteger(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(UBigInt value) => BigInteger.IsNegative(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOddInteger(UBigInt value) => BigInteger.IsOddInteger(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(UBigInt value) => BigInteger.IsPositive(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPow2(UBigInt value) => BigInteger.IsPow2(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt LeadingZeroCount(UBigInt value) => new(BigInteger.LeadingZeroCount(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(UBigInt value) => BigInteger.Log(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(UBigInt value) => BigInteger.Log10(value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Log2(UBigInt value) => new(BigInteger.Log2(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Max(UBigInt left, UBigInt right) => new(BigInteger.Max(left.Value.NativeValue, right.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt MaxMagnitude(UBigInt x, UBigInt y) => new(BigInteger.MaxMagnitude(x.Value.NativeValue, y.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Min(UBigInt left, UBigInt right) => new(BigInteger.Min(left.Value.NativeValue, right.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt MinMagnitude(UBigInt x, UBigInt y) => new(BigInteger.MinMagnitude(x.Value.NativeValue, y.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt ModPow(UBigInt value, UBigInt exponent, UBigInt modulus) => new(BigInteger.ModPow(value.Value.NativeValue, exponent.Value.NativeValue, modulus.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Multiply(UBigInt left, UBigInt right) => new(BigInteger.Multiply(left.Value.NativeValue, right.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Negate(UBigInt value) => new(BigInteger.Negate(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Parse(String value) => new(BigInteger.Parse(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Parse(ReadOnlySpan<Char> s, IFormatProvider? provider) => new(BigInteger.Parse(s, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Parse(String value, NumberStyles style) => new(BigInteger.Parse(value, style));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Parse(String value, IFormatProvider? provider) => new(BigInteger.Parse(value, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Parse(ReadOnlySpan<Char> value, NumberStyles style = NumberStyles.Integer, IFormatProvider? provider = default) => new(BigInteger.Parse(value, style, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Parse(String value, NumberStyles style, IFormatProvider? provider) => new(BigInteger.Parse(value, style, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt PopCount(UBigInt value) => new(BigInteger.PopCount(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Pow(UBigInt value, Int32 exponent) => new(BigInteger.Pow(value.Value.NativeValue, exponent));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Remainder(UBigInt dividend, UBigInt divisor) => new(BigInteger.Remainder(dividend.Value.NativeValue, divisor.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt RotateLeft(UBigInt value, Int32 rotateAmount) => new(BigInteger.RotateLeft(value.Value.NativeValue, rotateAmount));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt RotateRight(UBigInt value, Int32 rotateAmount) => new(BigInteger.RotateRight(value.Value.NativeValue, rotateAmount));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt Subtract(UBigInt left, UBigInt right) => new(BigInteger.Subtract(left.Value.NativeValue, right.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt TrailingZeroCount(UBigInt value) => new(BigInteger.TrailingZeroCount(value.Value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> value, NumberStyles style, IFormatProvider? provider, out UBigInt result)
        {
            if (!BigInteger.TryParse(value, style, provider, out var r))
            {
                result = default;
                return false;
            }

            result = new UBigInt(r);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String? s, IFormatProvider? provider, out UBigInt result)
        {
            if (!BigInteger.TryParse(s, provider, out var r))
            {
                result = default;
                return false;
            }

            result = new UBigInt(r);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> s, IFormatProvider? provider, out UBigInt result)
        {
            if (!BigInteger.TryParse(s, provider, out var r))
            {
                result = default;
                return false;
            }

            result = new UBigInt(r);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String? value, out UBigInt result)
        {
            if (!BigInteger.TryParse(value, out var r))
            {
                result = default;
                return false;
            }

            result = new UBigInt(r);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> value, out UBigInt result)
        {
            if (!BigInteger.TryParse(value, out var r))
            {
                result = default;
                return false;
            }

            result = new UBigInt(r);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String? value, NumberStyles style, IFormatProvider? provider, out UBigInt result)
        {
            if (!BigInteger.TryParse(value, style, provider, out var r))
            {
                result = default;
                return false;
            }

            result = new UBigInt(r);
            return true;
        }

        #endregion

        #region explicit interface members

        static UBigInt INumberBase<UBigInt>.One => One;
        static Int32 INumberBase<UBigInt>.Radix => Radix;
        static UBigInt INumberBase<UBigInt>.Zero => Zero;
        static UBigInt IAdditiveIdentity<UBigInt, UBigInt>.AdditiveIdentity => AdditiveIdentity;
        static UBigInt IMultiplicativeIdentity<UBigInt, UBigInt>.MultiplicativeIdentity => MultiplicativeIdentity;
        static UBigInt ISignedNumber<UBigInt>.NegativeOne => throw new NotSupportedException();
        Int32 IBinaryInteger<UBigInt>.GetByteCount() => Value.GetByteCount();
        Int32 IBinaryInteger<UBigInt>.GetShortestBitLength() => Value.GetShortestBitLength();
        Boolean IBinaryInteger<UBigInt>.TryWriteBigEndian(Span<Byte> destination, out Int32 bytesWritten) => Value.TryWriteBigEndian(destination, out bytesWritten);
        Boolean IBinaryInteger<UBigInt>.TryWriteLittleEndian(Span<Byte> destination, out Int32 bytesWritten) => Value.TryWriteLittleEndian(destination, out bytesWritten);
        static Boolean IBinaryInteger<UBigInt>.TryReadBigEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out UBigInt value)
        {
            if (!UnsignedIntegerCapsule<BigInteger>.TryReadBigEndian(source, isUnsigned, out var x))
            {
                value = default;
                return false;
            }

            value = new UBigInt(x);
            return true;
        }

        static Boolean IBinaryInteger<UBigInt>.TryReadLittleEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out UBigInt value)
        {
            if (!UnsignedIntegerCapsule<BigInteger>.TryReadLittleEndian(source, isUnsigned, out var x))
            {
                value = default;
                return false;
            }

            value = new UBigInt(x);
            return true;
        }

        static Boolean INumberBase<UBigInt>.IsCanonical(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsCanonical(value.Value);
        static Boolean INumberBase<UBigInt>.IsComplexNumber(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsComplexNumber(value.Value);
        static Boolean INumberBase<UBigInt>.IsFinite(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsFinite(value.Value);
        static Boolean INumberBase<UBigInt>.IsImaginaryNumber(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsImaginaryNumber(value.Value);
        static Boolean INumberBase<UBigInt>.IsInfinity(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsInfinity(value.Value);
        static Boolean INumberBase<UBigInt>.IsInteger(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsInteger(value.Value);
        static Boolean INumberBase<UBigInt>.IsNaN(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsNaN(value.Value);
        static Boolean INumberBase<UBigInt>.IsNegativeInfinity(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsNegativeInfinity(value.Value);
        static Boolean INumberBase<UBigInt>.IsNormal(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsNormal(value.Value);
        static Boolean INumberBase<UBigInt>.IsPositiveInfinity(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsPositiveInfinity(value.Value);
        static Boolean INumberBase<UBigInt>.IsRealNumber(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsRealNumber(value.Value);
        static Boolean INumberBase<UBigInt>.IsSubnormal(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsSubnormal(value.Value);
        static Boolean INumberBase<UBigInt>.IsZero(UBigInt value) => UnsignedIntegerCapsule<BigInteger>.IsZero(value.Value);
        static UBigInt INumberBase<UBigInt>.MaxMagnitudeNumber(UBigInt x, UBigInt y) => new(UnsignedIntegerCapsule<BigInteger>.MaxMagnitudeNumber(x.Value, y.Value));
        static UBigInt INumberBase<UBigInt>.MinMagnitudeNumber(UBigInt x, UBigInt y) => new(UnsignedIntegerCapsule<BigInteger>.MinMagnitudeNumber(x.Value, y.Value));

        static Boolean INumberBase<UBigInt>.TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out UBigInt result)
        {
            if (!UnsignedIntegerCapsule<BigInteger>.TryConvertFromChecked(value, out var r))
            {
                result = default;
                return false;
            }

            result = new UBigInt(r);
            return true;
        }

        static Boolean INumberBase<UBigInt>.TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out UBigInt result)
        {
            if (!UnsignedIntegerCapsule<BigInteger>.TryConvertFromSaturating(value, out var r))
            {
                result = default;
                return false;
            }

            result = new UBigInt(r);
            return true;
        }

        static Boolean INumberBase<UBigInt>.TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out UBigInt result)
        {
            if (!UnsignedIntegerCapsule<BigInteger>.TryConvertFromTruncating(value, out var r))
            {
                result = default;
                return false;
            }

            result = new UBigInt(r);
            return true;
        }

        static Boolean INumberBase<UBigInt>.TryConvertToChecked<TOther>(UBigInt value, [MaybeNullWhen(false)] out TOther result) => UnsignedIntegerCapsule<BigInteger>.TryConvertToChecked(value.Value, out result);
        static Boolean INumberBase<UBigInt>.TryConvertToSaturating<TOther>(UBigInt value, [MaybeNullWhen(false)] out TOther result) => UnsignedIntegerCapsule<BigInteger>.TryConvertToSaturating(value.Value, out result);
        static Boolean INumberBase<UBigInt>.TryConvertToTruncating<TOther>(UBigInt value, [MaybeNullWhen(false)] out TOther result) => UnsignedIntegerCapsule<BigInteger>.TryConvertToTruncating(value.Value, out result);

        #endregion

        #region operators

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator +(UBigInt left, UBigInt right) => new(left.Value.NativeValue + right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator -(UBigInt left, UBigInt right) => new(left.Value.NativeValue - right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator *(UBigInt left, UBigInt right) => new(left.Value.NativeValue * right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator /(UBigInt dividend, UBigInt divisor) => new(dividend.Value.NativeValue / divisor.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator %(UBigInt dividend, UBigInt divisor) => new(dividend.Value.NativeValue % divisor.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator +(UBigInt value) => new(+value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator -(UBigInt value) => new(-value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator ++(UBigInt value)
        {
            var x = value.Value.NativeValue;
            return new UBigInt(++x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator --(UBigInt value)
        {
            var x = value.Value.NativeValue;
            return new UBigInt(--x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator &(UBigInt left, UBigInt right) => new(left.Value.NativeValue & right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator |(UBigInt left, UBigInt right) => new(left.Value.NativeValue | right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator ^(UBigInt left, UBigInt right) => new(left.Value.NativeValue ^ right.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator ~(UBigInt value) => new(~value.Value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator <<(UBigInt value, Int32 shift) => new(value.Value.NativeValue << shift);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator >>(UBigInt value, Int32 shift) => new(value.Value.NativeValue >> shift);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UBigInt operator >>>(UBigInt value, Int32 shift) => new(value.Value.NativeValue >>> shift);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(UBigInt left, UBigInt right) => left.Value.NativeValue == right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(UBigInt left, BigInteger right) => left.Value.NativeValue == right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(UBigInt left, UInt64 right) => left.Value.NativeValue == right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(UBigInt left, Int64 right) => left.Value.NativeValue == right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(BigInteger left, UBigInt right) => left == right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(UInt64 left, UBigInt right) => left == right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(Int64 left, UBigInt right) => left == right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(UBigInt left, UBigInt right) => left.Value.NativeValue != right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(UBigInt left, BigInteger right) => left.Value.NativeValue != right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(UBigInt left, UInt64 right) => left.Value.NativeValue != right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(UBigInt left, Int64 right) => left.Value.NativeValue != right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(BigInteger left, UBigInt right) => left != right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(UInt64 left, UBigInt right) => left != right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(Int64 left, UBigInt right) => left != right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(UBigInt left, UBigInt right) => left.Value.NativeValue > right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(UBigInt left, BigInteger right) => left.Value.NativeValue > right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(UBigInt left, UInt64 right) => left.Value.NativeValue > right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(UBigInt left, Int64 right) => left.Value.NativeValue > right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(BigInteger left, UBigInt right) => left > right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(UInt64 left, UBigInt right) => left > right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(Int64 left, UBigInt right) => left > right.Value.NativeValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(UBigInt left, UBigInt right) => left.Value.NativeValue < right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(UBigInt left, BigInteger right) => left.Value.NativeValue < right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(UBigInt left, UInt64 right) => left.Value.NativeValue < right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(UBigInt left, Int64 right) => left.Value.NativeValue < right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(BigInteger left, UBigInt right) => left < right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(UInt64 left, UBigInt right) => left < right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(Int64 left, UBigInt right) => left < right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(UBigInt left, UBigInt right) => left.Value.NativeValue >= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(UBigInt left, BigInteger right) => left.Value.NativeValue >= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(UBigInt left, UInt64 right) => left.Value.NativeValue >= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(UBigInt left, Int64 right) => left.Value.NativeValue >= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(BigInteger left, UBigInt right) => left >= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(UInt64 left, UBigInt right) => left >= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(Int64 left, UBigInt right) => left >= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(UBigInt left, UBigInt right) => left.Value.NativeValue <= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(UBigInt left, BigInteger right) => left.Value.NativeValue <= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(UBigInt left, UInt64 right) => left.Value.NativeValue <= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(UBigInt left, Int64 right) => left.Value.NativeValue <= right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(BigInteger left, UBigInt right) => left <= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(UInt64 left, UBigInt right) => left <= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(Int64 left, UBigInt right) => left <= right.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Char(UBigInt value) => (Char)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator SByte(UBigInt value) => (SByte)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Byte(UBigInt value) => (Byte)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int16(UBigInt value) => (Int16)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UInt16(UBigInt value) => (UInt16)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int32(UBigInt value) => (Int32)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UInt32(UBigInt value) => (UInt32)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int64(UBigInt value) => (Int64)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UInt64(UBigInt value) => (UInt64)value.Value.NativeValue;

#if NET7_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Int128(UBigInt value) => (Int128)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UInt128(UBigInt value) => (UInt128)value.Value.NativeValue;
#endif // NET7_0_OR_GREATER

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Single(UBigInt value) => (Single)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Double(UBigInt value) => (Double)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Decimal(UBigInt value) => (Decimal)value.Value.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UBigInt(Half value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UBigInt(Single value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UBigInt(Double value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator UBigInt(Decimal value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(Char value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(SByte value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(Byte value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(Int16 value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(UInt16 value) => new((BigInteger)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(Int32 value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(UInt32 value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(Int64 value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(UInt64 value) => new(value);

#if NET7_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(Int128 value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(UInt128 value) => new(value);
#endif // NET7_0_OR_GREATER

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UBigInt(BigInteger value) => new(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BigInteger(UBigInt value) => value.Value.NativeValue;

        #endregion

        #region private methods

        private static UnsignedIntegerCapsule<BigInteger>? CreateInstance(ReadOnlySpan<Byte> value, Boolean isUnsigned, Boolean isBigEndian)
        {
            if (isBigEndian)
                return UnsignedIntegerCapsule<BigInteger>.TryReadBigEndian(value, isUnsigned, out var result) ? result : null;
            else
                return UnsignedIntegerCapsule<BigInteger>.TryReadLittleEndian(value, isUnsigned, out var result) ? result : null;
        }

        #endregion
    }
}
