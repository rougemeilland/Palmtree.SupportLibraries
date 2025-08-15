using System;
using System.Buffers.Binary;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Palmtree;

namespace Palmtree
{
    public static partial class ByteArrayExtensions
    {
        #region ToSByte

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte ToSByte(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(SByte))
                throw new ArgumentException("Too short array", nameof(array));

            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(SByte));

            return unchecked((SByte)array[startIndex]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte ToSByte(this Memory<Byte> array)
        {
            if (array.Length < sizeof(SByte))
                throw new ArgumentException("Too short array", nameof(array));

            return unchecked((SByte)array.Span[0]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte ToSByte(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(SByte))
                throw new ArgumentException("Too short array", nameof(array));

            return unchecked((SByte)array.Span[0]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte ToSByte(this Span<Byte> array)
        {
            if (array.Length < sizeof(SByte))
                throw new ArgumentException("Too short array", nameof(array));

            return unchecked((SByte)array[0]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static SByte ToSByte(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(SByte))
                throw new ArgumentException("Too short array", nameof(array));

            return unchecked((SByte)array[0]);
        }

        #endregion

        #region ToByte

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte ToByte(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Byte))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Byte));

            return array[startIndex];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte ToByte(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Byte))
                throw new ArgumentException("Too short array", nameof(array));

            return array.Span[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte ToByte(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Byte))
                throw new ArgumentException("Too short array", nameof(array));

            return array.Span[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte ToByte(this Span<Byte> array)
        {
            if (array.Length < sizeof(Byte))
                throw new ArgumentException("Too short array", nameof(array));

            return array[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Byte ToByte(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Byte))
                throw new ArgumentException("Too short array", nameof(array));

            return array[0];
        }

        #endregion

        #region ToFriendlyString

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static String ToFriendlyString(this Byte[] value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, value.Length);

            return value.AsSpan(startIndex).AsReadOnly().ToFriendlyString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static String ToFriendlyString(this Byte[] value, Int32 startIndex, Int32 length)
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, value.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(length, value.Length - startIndex);

            return value.AsSpan(startIndex, length).AsReadOnly().ToFriendlyString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static String ToFriendlyString(this Memory<Byte> value)
            => ((ReadOnlySpan<Byte>)value.Span).ToFriendlyString();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static String ToFriendlyString(this ReadOnlyMemory<Byte> value)
            => value.Span.ToFriendlyString();

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static String ToFriendlyString(this Span<Byte> value)
            => ((ReadOnlySpan<Byte>)value).ToFriendlyString();

        public static String ToFriendlyString(this ReadOnlySpan<Byte> array)
        {
            var sb = new StringBuilder();
            var isFirst = true;
            for (var index = 0; index < array.Length; ++index)
            {
                if (!isFirst)
                    _ = sb.Append('-');
                _ = sb.Append(array[index].ToString("x2", CultureInfo.InvariantCulture.NumberFormat));
                isFirst = false;
            }

            return sb.ToString();
        }

        #endregion

        #region ToDecimalLECore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Decimal ToDecimalLECore(ReadOnlySpan<Byte> span)
        {
            UnionOf128bitNumber value = default;
            value.UInt128Value = BinaryPrimitives.ReadUInt128LittleEndian(span);
            return value.DecimalValue;
        }

        #endregion

        #region ToDecimalBECore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Decimal ToDecimalBECore(ReadOnlySpan<Byte> span)
        {
            UnionOf128bitNumber value = default;
            value.UInt128Value = BinaryPrimitives.ReadUInt128BigEndian(span);
            return value.DecimalValue;
        }

        #endregion
    }
}
