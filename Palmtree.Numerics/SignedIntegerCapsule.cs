using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Palmtree.Numerics
{
    internal readonly struct SignedIntegerCapsule<NUMBER_T>
        : ISpanFormattable, IComparable, IComparable<SignedIntegerCapsule<NUMBER_T>>, IEquatable<SignedIntegerCapsule<NUMBER_T>>, IBinaryInteger<SignedIntegerCapsule<NUMBER_T>>, ISignedNumber<SignedIntegerCapsule<NUMBER_T>>
        where NUMBER_T : struct, ISpanFormattable, IComparable, IComparable<NUMBER_T>, IEquatable<NUMBER_T>, IBinaryInteger<NUMBER_T>, ISignedNumber<NUMBER_T>
    {
        public static readonly Int32 RadixValue;
        public static readonly SignedIntegerCapsule<NUMBER_T> AdditiveIdentityValue;
        public static readonly SignedIntegerCapsule<NUMBER_T> MultiplicativeIdentityValue;
        public static readonly SignedIntegerCapsule<NUMBER_T> NegativeOneValue;
        public static readonly SignedIntegerCapsule<NUMBER_T> OneValue;
        public static readonly SignedIntegerCapsule<NUMBER_T> ZeroValue;

        public readonly NUMBER_T NativeValue;

        static SignedIntegerCapsule()
        {
            RadixValue = NUMBER_T.Radix;
            AdditiveIdentityValue = new SignedIntegerCapsule<NUMBER_T>(NUMBER_T.AdditiveIdentity);
            MultiplicativeIdentityValue = new SignedIntegerCapsule<NUMBER_T>(NUMBER_T.MultiplicativeIdentity);
            NegativeOneValue = new SignedIntegerCapsule<NUMBER_T>(NUMBER_T.NegativeOne);
            OneValue = new SignedIntegerCapsule<NUMBER_T>(NUMBER_T.One);
            ZeroValue = new SignedIntegerCapsule<NUMBER_T>(NUMBER_T.Zero);
        }

        public SignedIntegerCapsule(NUMBER_T number)
        {
            NativeValue = number;
        }

        public static SignedIntegerCapsule<NUMBER_T> AdditiveIdentity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => AdditiveIdentityValue;
        }

        public static SignedIntegerCapsule<NUMBER_T> MultiplicativeIdentity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => MultiplicativeIdentityValue;
        }

        public static SignedIntegerCapsule<NUMBER_T> NegativeOne
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => NegativeOneValue;
        }

        public static SignedIntegerCapsule<NUMBER_T> One
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => OneValue;
        }

        public static Int32 Radix
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => RadixValue;
        }

        public static SignedIntegerCapsule<NUMBER_T> Zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ZeroValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 GetByteCount()
            => NativeValue.GetByteCount();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 GetShortestBitLength()
            => NativeValue.GetShortestBitLength();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryFormat(Span<Char> destination, out Int32 charsWritten, ReadOnlySpan<Char> format, IFormatProvider? provider)
            => NativeValue.TryFormat(destination, out charsWritten, format, provider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryWriteBigEndian(Span<Byte> destination, out Int32 bytesWritten)
            => NativeValue.TryWriteBigEndian(destination, out bytesWritten);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryWriteLittleEndian(Span<Byte> destination, out Int32 bytesWritten)
            => NativeValue.TryWriteLittleEndian(destination, out bytesWritten);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(SignedIntegerCapsule<NUMBER_T> other)
            => NativeValue.CompareTo(other.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(SignedIntegerCapsule<NUMBER_T> other)
            => NativeValue.Equals(other.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format, IFormatProvider? formatProvider)
            => NativeValue.ToString(format, formatProvider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Int32 IComparable.CompareTo(Object? other)
        {
            if (other is null)
                return 1;
            if (other is not SignedIntegerCapsule<NUMBER_T> obj2)
                throw new ArgumentException("Bad object type.", nameof(other));
            return NativeValue.CompareTo(obj2.NativeValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Boolean Equals(Object? other)
            => other is not null && GetType() == other.GetType() && Equals((SignedIntegerCapsule<NUMBER_T>)other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
            => NativeValue.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override String ToString()
            => NativeValue.ToString() ?? "";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> Abs(SignedIntegerCapsule<NUMBER_T> value)
            => new(NUMBER_T.Abs(value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsCanonical(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsCanonical(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsComplexNumber(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsComplexNumber(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEvenInteger(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsEvenInteger(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFinite(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsFinite(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsImaginaryNumber(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsImaginaryNumber(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInfinity(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsInfinity(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInteger(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsInteger(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNaN(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsNaN(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsNegative(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegativeInfinity(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsNegativeInfinity(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNormal(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsNormal(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOddInteger(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsOddInteger(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsPositive(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositiveInfinity(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsPositiveInfinity(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Boolean IBinaryNumber<SignedIntegerCapsule<NUMBER_T>>.IsPow2(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsPow2(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsRealNumber(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsRealNumber(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSubnormal(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsSubnormal(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsZero(SignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsZero(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> Log2(SignedIntegerCapsule<NUMBER_T> value)
            => new(NUMBER_T.Log2(value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> MaxMagnitude(SignedIntegerCapsule<NUMBER_T> x, SignedIntegerCapsule<NUMBER_T> y)
            => new(NUMBER_T.MaxMagnitude(x.NativeValue, y.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> MaxMagnitudeNumber(SignedIntegerCapsule<NUMBER_T> x, SignedIntegerCapsule<NUMBER_T> y)
            => new(NUMBER_T.MaxMagnitudeNumber(x.NativeValue, y.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> MinMagnitude(SignedIntegerCapsule<NUMBER_T> x, SignedIntegerCapsule<NUMBER_T> y)
            => new(NUMBER_T.MinMagnitude(x.NativeValue, y.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> MinMagnitudeNumber(SignedIntegerCapsule<NUMBER_T> x, SignedIntegerCapsule<NUMBER_T> y)
            => new(NUMBER_T.MinMagnitudeNumber(x.NativeValue, y.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> Clamp(SignedIntegerCapsule<NUMBER_T> value, SignedIntegerCapsule<NUMBER_T> min, SignedIntegerCapsule<NUMBER_T> max)
            => new(NUMBER_T.Clamp(value.NativeValue, min.NativeValue, max.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> Parse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider)
            => new(NUMBER_T.Parse(s, style, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> Parse(String s, NumberStyles style, IFormatProvider? provider)
            => new(NUMBER_T.Parse(s, style, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> Parse(ReadOnlySpan<Char> s, IFormatProvider? provider)
            => new(NUMBER_T.Parse(s, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> Parse(String s, IFormatProvider? provider)
            => new(NUMBER_T.Parse(s, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> PopCount(SignedIntegerCapsule<NUMBER_T> value)
            => new(NUMBER_T.PopCount(value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> TrailingZeroCount(SignedIntegerCapsule<NUMBER_T> value)
            => new(NUMBER_T.TrailingZeroCount(value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out SignedIntegerCapsule<NUMBER_T> result)
            where TOther : INumberBase<TOther>
        {
            if (!NUMBER_T.TryConvertFromChecked(value, out var x))
            {
                result = default;
                return false;
            }

            result = new SignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out SignedIntegerCapsule<NUMBER_T> result)
            where TOther : INumberBase<TOther>
        {
            if (!NUMBER_T.TryConvertFromSaturating(value, out var x))
            {
                result = default;
                return false;
            }

            result = new SignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out SignedIntegerCapsule<NUMBER_T> result)
            where TOther : INumberBase<TOther>
        {
            if (!NUMBER_T.TryConvertFromTruncating(value, out var x))
            {
                result = default;
                return false;
            }

            result = new SignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertToChecked<TOther>(SignedIntegerCapsule<NUMBER_T> value, [MaybeNullWhen(false)] out TOther result)
            where TOther : INumberBase<TOther>
            => NUMBER_T.TryConvertToChecked(value.NativeValue, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertToSaturating<TOther>(SignedIntegerCapsule<NUMBER_T> value, [MaybeNullWhen(false)] out TOther result)
            where TOther : INumberBase<TOther>
            => NUMBER_T.TryConvertToSaturating(value.NativeValue, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertToTruncating<TOther>(SignedIntegerCapsule<NUMBER_T> value, [MaybeNullWhen(false)] out TOther result)
            where TOther : INumberBase<TOther>
            => NUMBER_T.TryConvertToTruncating(value.NativeValue, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out SignedIntegerCapsule<NUMBER_T> result)
        {
            if (!NUMBER_T.TryParse(s, style, provider, out var x))
            {
                result = default;
                return false;
            }

            result = new SignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(String? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out SignedIntegerCapsule<NUMBER_T> result)
        {
            if (!NUMBER_T.TryParse(s, style, provider, out var x))
            {
                result = default;
                return false;
            }

            result = new SignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Boolean ISpanParsable<SignedIntegerCapsule<NUMBER_T>>.TryParse(ReadOnlySpan<Char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out SignedIntegerCapsule<NUMBER_T> result)
        {
            if (!NUMBER_T.TryParse(s, provider, out var x))
            {
                result = default;
                return false;
            }

            result = new SignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Boolean IParsable<SignedIntegerCapsule<NUMBER_T>>.TryParse(String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out SignedIntegerCapsule<NUMBER_T> result)
        {
            if (!NUMBER_T.TryParse(s, provider, out var x))
            {
                result = default;
                return false;
            }

            result = new SignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryReadBigEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, [MaybeNullWhen(false)] out SignedIntegerCapsule<NUMBER_T> value)
        {
            if (!NUMBER_T.TryReadBigEndian(source, isUnsigned, out var x))
            {
                value = default;
                return false;
            }

            value = new SignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryReadLittleEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, [MaybeNullWhen(false)] out SignedIntegerCapsule<NUMBER_T> value)
        {
            if (!NUMBER_T.TryReadLittleEndian(source, isUnsigned, out var x))
            {
                value = default;
                return false;
            }

            value = new SignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator +(SignedIntegerCapsule<NUMBER_T> value)
            => new(+value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator +(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue + right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator -(SignedIntegerCapsule<NUMBER_T> value)
            => new(-value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator -(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue - right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator ~(SignedIntegerCapsule<NUMBER_T> value)
            => new(~value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator ++(SignedIntegerCapsule<NUMBER_T> value)
        {
            var x = value.NativeValue;
            ++x;
            return new(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator --(SignedIntegerCapsule<NUMBER_T> value)
        {
            var x = value.NativeValue;
            --x;
            return new(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator *(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue * right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator /(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue / right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator %(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue % right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator &(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue & right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator |(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue | right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator ^(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue ^ right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator <<(SignedIntegerCapsule<NUMBER_T> value, Int32 shiftAmount)
            => new(value.NativeValue << shiftAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator >>(SignedIntegerCapsule<NUMBER_T> value, Int32 shiftAmount)
            => new(value.NativeValue >> shiftAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SignedIntegerCapsule<NUMBER_T> operator >>>(SignedIntegerCapsule<NUMBER_T> value, Int32 shiftAmount)
            => new(value.NativeValue >>> shiftAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue == right.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue != right.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue < right.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue > right.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue <= right.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(SignedIntegerCapsule<NUMBER_T> left, SignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue >= right.NativeValue;
    }
}
