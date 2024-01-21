using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Palmtree.Numerics
{
    internal readonly struct UnsignedIntegerCapsule<NUMBER_T>
        : ISpanFormattable, IComparable, IComparable<UnsignedIntegerCapsule<NUMBER_T>>, IEquatable<UnsignedIntegerCapsule<NUMBER_T>>, IBinaryInteger<UnsignedIntegerCapsule<NUMBER_T>>, IUnsignedNumber<UnsignedIntegerCapsule<NUMBER_T>>
        where NUMBER_T : struct, ISpanFormattable, IComparable, IComparable<NUMBER_T>, IEquatable<NUMBER_T>, IBinaryInteger<NUMBER_T>, ISignedNumber<NUMBER_T>
    {
        public static readonly Int32 RadixValue;
        public static readonly UnsignedIntegerCapsule<NUMBER_T> AdditiveIdentityValue;
        public static readonly UnsignedIntegerCapsule<NUMBER_T> MultiplicativeIdentityValue;
        public static readonly UnsignedIntegerCapsule<NUMBER_T> OneValue;
        public static readonly UnsignedIntegerCapsule<NUMBER_T> ZeroValue;

        public readonly NUMBER_T NativeValue;

        static UnsignedIntegerCapsule()
        {
            RadixValue = NUMBER_T.Radix;
            AdditiveIdentityValue = new UnsignedIntegerCapsule<NUMBER_T>(NUMBER_T.AdditiveIdentity);
            MultiplicativeIdentityValue = new UnsignedIntegerCapsule<NUMBER_T>(NUMBER_T.MultiplicativeIdentity);
            OneValue = new UnsignedIntegerCapsule<NUMBER_T>(NUMBER_T.One);
            ZeroValue = new UnsignedIntegerCapsule<NUMBER_T>(NUMBER_T.Zero);
        }

        public UnsignedIntegerCapsule(NUMBER_T number)
        {
            if (NUMBER_T.IsNegative(number))
                throw new OverflowException();
            NativeValue = number;
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
        public Int32 CompareTo(UnsignedIntegerCapsule<NUMBER_T> other)
            => NativeValue.CompareTo(other.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(UnsignedIntegerCapsule<NUMBER_T> other)
            => NativeValue.Equals(other.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String ToString(String? format, IFormatProvider? formatProvider)
            => NativeValue.ToString(format, formatProvider);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(Object? other)
        {
            if (other is null)
                return 1;
            if (other is not UnsignedIntegerCapsule<NUMBER_T> obj2)
                throw new ArgumentException("Bad object type.", nameof(other));
            return NativeValue.CompareTo(obj2.NativeValue);
        }

        public static UnsignedIntegerCapsule<NUMBER_T> One
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new(NUMBER_T.One);
        }

        public static Int32 Radix
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => NUMBER_T.Radix;
        }

        public static UnsignedIntegerCapsule<NUMBER_T> Zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new(NUMBER_T.Zero);
        }

        public static UnsignedIntegerCapsule<NUMBER_T> AdditiveIdentity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new(NUMBER_T.AdditiveIdentity);
        }

        public static UnsignedIntegerCapsule<NUMBER_T> MultiplicativeIdentity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new(NUMBER_T.MultiplicativeIdentity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Boolean Equals([NotNullWhen(true)] Object? obj)
            => obj is not null && GetType() == obj.GetType() && Equals((UnsignedIntegerCapsule<NUMBER_T>)obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Int32 GetHashCode()
            => NativeValue.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override String ToString()
            => NativeValue.ToString() ?? "";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> Abs(UnsignedIntegerCapsule<NUMBER_T> value)
            => new(NUMBER_T.Abs(value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsCanonical(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsCanonical(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsComplexNumber(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsComplexNumber(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEvenInteger(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsEvenInteger(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFinite(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsFinite(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsImaginaryNumber(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsImaginaryNumber(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInfinity(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsInfinity(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInteger(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsInteger(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNaN(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsNaN(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsNegative(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegativeInfinity(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsNegativeInfinity(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNormal(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsNormal(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOddInteger(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsOddInteger(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsPositive(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositiveInfinity(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsPositiveInfinity(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPow2(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsPow2(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsRealNumber(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsRealNumber(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSubnormal(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsSubnormal(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsZero(UnsignedIntegerCapsule<NUMBER_T> value)
            => NUMBER_T.IsZero(value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> Log2(UnsignedIntegerCapsule<NUMBER_T> value)
            => new(NUMBER_T.Log2(value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> MaxMagnitude(UnsignedIntegerCapsule<NUMBER_T> x, UnsignedIntegerCapsule<NUMBER_T> y)
            => new(NUMBER_T.MaxMagnitude(x.NativeValue, y.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> MaxMagnitudeNumber(UnsignedIntegerCapsule<NUMBER_T> x, UnsignedIntegerCapsule<NUMBER_T> y)
            => new(NUMBER_T.MaxMagnitudeNumber(x.NativeValue, y.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> MinMagnitude(UnsignedIntegerCapsule<NUMBER_T> x, UnsignedIntegerCapsule<NUMBER_T> y)
            => new(NUMBER_T.MinMagnitude(x.NativeValue, y.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> MinMagnitudeNumber(UnsignedIntegerCapsule<NUMBER_T> x, UnsignedIntegerCapsule<NUMBER_T> y)
            => new(NUMBER_T.MinMagnitudeNumber(x.NativeValue, y.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> Parse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider)
            => new(NUMBER_T.Parse(s, style, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> Parse(String s, NumberStyles style, IFormatProvider? provider)
            => new(NUMBER_T.Parse(s, style, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> Parse(ReadOnlySpan<Char> s, IFormatProvider? provider)
            => new(NUMBER_T.Parse(s, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> Parse(String s, IFormatProvider? provider)
            => new(NUMBER_T.Parse(s, provider));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> PopCount(UnsignedIntegerCapsule<NUMBER_T> value)
            => new(NUMBER_T.PopCount(value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> TrailingZeroCount(UnsignedIntegerCapsule<NUMBER_T> value)
            => new(NUMBER_T.TrailingZeroCount(value.NativeValue));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out UnsignedIntegerCapsule<NUMBER_T> result)
        {
            if (!NUMBER_T.TryParse(s, style, provider, out var x))
            {
                result = default;
                return false;
            }

            result = new UnsignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse([NotNullWhen(true)] String? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out UnsignedIntegerCapsule<NUMBER_T> result)
        {
            if (!NUMBER_T.TryParse(s, style, provider, out var x))
            {
                result = default;
                return false;
            }

            result = new UnsignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(ReadOnlySpan<Char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out UnsignedIntegerCapsule<NUMBER_T> result)
        {
            if (!NUMBER_T.TryParse(s, provider, out var x))
            {
                result = default;
                return false;
            }

            result = new UnsignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse([NotNullWhen(true)] String? s, IFormatProvider? provider, [MaybeNullWhen(false)] out UnsignedIntegerCapsule<NUMBER_T> result)
        {
            if (!NUMBER_T.TryParse(s, provider, out var x))
            {
                result = default;
                return false;
            }

            result = new UnsignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryReadBigEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out UnsignedIntegerCapsule<NUMBER_T> value)
        {
            if (!NUMBER_T.TryReadBigEndian(source, isUnsigned, out var x))
            {
                value = default;
                return false;
            }

            value = new UnsignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryReadLittleEndian(ReadOnlySpan<Byte> source, Boolean isUnsigned, out UnsignedIntegerCapsule<NUMBER_T> value)
        {
            if (!NUMBER_T.TryReadLittleEndian(source, isUnsigned, out var x))
            {
                value = default;
                return false;
            }

            value = new UnsignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out UnsignedIntegerCapsule<NUMBER_T> result)
            where TOther : INumberBase<TOther>
        {
            if (!NUMBER_T.TryConvertFromChecked(value, out var x))
            {
                result = default;
                return false;
            }

            result = new UnsignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out UnsignedIntegerCapsule<NUMBER_T> result)
            where TOther : INumberBase<TOther>
        {
            if (!NUMBER_T.TryConvertFromSaturating(value, out var x))
            {
                result = default;
                return false;
            }

            result = new UnsignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out UnsignedIntegerCapsule<NUMBER_T> result)
            where TOther : INumberBase<TOther>
        {
            if (!NUMBER_T.TryConvertFromTruncating(value, out var x))
            {
                result = default;
                return false;
            }

            result = new UnsignedIntegerCapsule<NUMBER_T>(x);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertToChecked<TOther>(UnsignedIntegerCapsule<NUMBER_T> value, [MaybeNullWhen(false)] out TOther result)
            where TOther : INumberBase<TOther>
            => NUMBER_T.TryConvertToChecked(value.NativeValue, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertToSaturating<TOther>(UnsignedIntegerCapsule<NUMBER_T> value, [MaybeNullWhen(false)] out TOther result)
            where TOther : INumberBase<TOther>
            => NUMBER_T.TryConvertToSaturating(value.NativeValue, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryConvertToTruncating<TOther>(UnsignedIntegerCapsule<NUMBER_T> value, [MaybeNullWhen(false)] out TOther result)
            where TOther : INumberBase<TOther>
            => NUMBER_T.TryConvertToTruncating(value.NativeValue, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator +(UnsignedIntegerCapsule<NUMBER_T> value)
            => new(+value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator +(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue + right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator -(UnsignedIntegerCapsule<NUMBER_T> value)
            => new(-value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator -(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue - right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator ~(UnsignedIntegerCapsule<NUMBER_T> value)
            => new(~value.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator ++(UnsignedIntegerCapsule<NUMBER_T> value)
        {
            var x = value.NativeValue;
            return new UnsignedIntegerCapsule<NUMBER_T>(++x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator --(UnsignedIntegerCapsule<NUMBER_T> value)
        {
            var x = value.NativeValue;
            return new UnsignedIntegerCapsule<NUMBER_T>(--x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator *(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue * right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator /(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue / right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator %(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue % right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator &(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue & right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator |(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue | right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator ^(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => new(left.NativeValue ^ right.NativeValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator <<(UnsignedIntegerCapsule<NUMBER_T> value, Int32 shiftAmount)
            => new(value.NativeValue << shiftAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator >>(UnsignedIntegerCapsule<NUMBER_T> value, Int32 shiftAmount)
            => new(value.NativeValue >> shiftAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnsignedIntegerCapsule<NUMBER_T> operator >>>(UnsignedIntegerCapsule<NUMBER_T> value, Int32 shiftAmount)
            => new(value.NativeValue >>> shiftAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator ==(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue == right.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator !=(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue != right.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue < right.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue > right.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator <=(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue <= right.NativeValue;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean operator >=(UnsignedIntegerCapsule<NUMBER_T> left, UnsignedIntegerCapsule<NUMBER_T> right)
            => left.NativeValue >= right.NativeValue;
    }
}
