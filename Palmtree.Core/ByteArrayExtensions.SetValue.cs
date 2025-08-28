using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static partial class ByteArrayExtensions
    {
        #region SetValue

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValue(this Byte[] buffer, Int32 startIndex, SByte value)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Byte))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Byte));

            buffer[startIndex] = unchecked((Byte)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValue(this Memory<Byte> buffer, SByte value)
        {
            if (buffer.Length < sizeof(Byte))
                throw new ArgumentException("Too short array", nameof(buffer));

            buffer.Span[0] = unchecked((Byte)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValue(this Span<Byte> buffer, SByte value)
        {
            if (buffer.Length < sizeof(Byte))
                throw new ArgumentException("Too short array", nameof(buffer));

            buffer[0] = unchecked((Byte)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValue(this Byte[] buffer, Int32 startIndex, Byte value)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Byte))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Byte));

            buffer[startIndex] = unchecked((Byte)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValue(this Memory<Byte> buffer, Byte value)
        {
            if (buffer.Length < sizeof(Byte))
                throw new ArgumentException("Too short array", nameof(buffer));

            buffer.Span[0] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValue(this Span<Byte> buffer, Byte value)
        {
            if (buffer.Length < sizeof(Byte))
                throw new ArgumentException("Too short array", nameof(buffer));

            buffer[0] = value;
        }

        #endregion

        #region SetValueLECore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void SetValueLECore(Span<Byte> span, Decimal value)
        {
            UnionOf128bitNumber buffer = default;
            buffer.DecimalValue = value;
            BinaryPrimitives.WriteUInt128LittleEndian(span, buffer.UInt128Value);
        }

        #endregion

        #region SetValueBECore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void SetValueBECore(Span<Byte> span, Decimal value)
        {
            UnionOf128bitNumber buffer = default;
            buffer.DecimalValue = value;
            BinaryPrimitives.WriteUInt128BigEndian(span, buffer.UInt128Value);
        }

        #endregion
    }
}
