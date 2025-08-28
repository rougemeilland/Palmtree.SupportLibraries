using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static partial class ByteArrayExtensions
    {
        #region ToInt16LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16LE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Int16));

            return BinaryPrimitives.ReadInt16LittleEndian(array.AsReadOnlySpan(startIndex, sizeof(Int16)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16LE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt16LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16LE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt16LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16LE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt16LittleEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16LE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt16LittleEndian(array);
        }

        #endregion

        #region ToUInt16LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ToUInt16LE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(UInt16));

            return BinaryPrimitives.ReadUInt16LittleEndian(array.AsReadOnlySpan(startIndex, sizeof(UInt16)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ToUInt16LE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt16LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ToUInt16LE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt16LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ToUInt16LE(this Span<Byte> array)
        {
            if (array.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt16LittleEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ToUInt16LE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt16LittleEndian(array);
        }

        #endregion

        #region ToInt32LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32LE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Int32));

            return BinaryPrimitives.ReadInt32LittleEndian(array.AsReadOnlySpan(startIndex, sizeof(Int32)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32LE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt32LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32LE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt32LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32LE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt32LittleEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32LE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt32LittleEndian(array);
        }

        #endregion

        #region ToUInt32LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ToUInt32LE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(UInt32));

            return BinaryPrimitives.ReadUInt32LittleEndian(array.AsReadOnlySpan(startIndex, sizeof(UInt32)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ToUInt32LE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt32LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ToUInt32LE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt32LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ToUInt32LE(this Span<Byte> array)
        {
            if (array.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt32LittleEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ToUInt32LE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt32LittleEndian(array);
        }

        #endregion

        #region ToInt64LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64LE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Int64));

            return BinaryPrimitives.ReadInt64LittleEndian(array.AsReadOnlySpan(startIndex, sizeof(Int64)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64LE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt64LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64LE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt64LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64LE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt64LittleEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64LE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt64LittleEndian(array);
        }

        #endregion

        #region ToUInt64LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ToUInt64LE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(UInt64));

            return BinaryPrimitives.ReadUInt64LittleEndian(array.AsReadOnlySpan(startIndex, sizeof(UInt64)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ToUInt64LE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt64LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ToUInt64LE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt64LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ToUInt64LE(this Span<Byte> array)
        {
            if (array.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt64LittleEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ToUInt64LE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt64LittleEndian(array);
        }

        #endregion

        #region ToInt128LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ToInt128LE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - _SIZE_OF_INT128);

            return BinaryPrimitives.ReadInt128LittleEndian(array.AsReadOnlySpan(startIndex, _SIZE_OF_INT128));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ToInt128LE(this Memory<Byte> array)
        {
            if (array.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt128LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ToInt128LE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt128LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ToInt128LE(this Span<Byte> array)
        {
            if (array.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt128LittleEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ToInt128LE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt128LittleEndian(array);
        }

        #endregion

        #region ToUInt128LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ToUInt128LE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - _SIZE_OF_UINT128);

            return BinaryPrimitives.ReadUInt128LittleEndian(array.AsReadOnlySpan(startIndex, _SIZE_OF_UINT128));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ToUInt128LE(this Memory<Byte> array)
        {
            if (array.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt128LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ToUInt128LE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt128LittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ToUInt128LE(this Span<Byte> array)
        {
            if (array.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt128LittleEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ToUInt128LE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt128LittleEndian(array);
        }

        #endregion

        #region ToHalfLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ToHalfLE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - _SIZE_OF_HALF);

            return BinaryPrimitives.ReadHalfLittleEndian(array.AsReadOnlySpan(startIndex, _SIZE_OF_HALF));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ToHalfLE(this Memory<Byte> array)
        {
            if (array.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadHalfLittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ToHalfLE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadHalfLittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ToHalfLE(this Span<Byte> array)
        {
            if (array.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadHalfLittleEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ToHalfLE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadHalfLittleEndian(array);
        }

        #endregion

        #region ToSingleLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ToSingleLE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Single));

            return BinaryPrimitives.ReadSingleLittleEndian(array.AsReadOnlySpan(startIndex, sizeof(Single)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ToSingleLE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadSingleLittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ToSingleLE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadSingleLittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ToSingleLE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadSingleLittleEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ToSingleLE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadSingleLittleEndian(array);
        }

        #endregion

        #region ToDoubleLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ToDoubleLE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Double));

            return BinaryPrimitives.ReadDoubleLittleEndian(array.AsReadOnlySpan(startIndex, sizeof(Double)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ToDoubleLE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadDoubleLittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ToDoubleLE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadDoubleLittleEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ToDoubleLE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadDoubleLittleEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ToDoubleLE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadDoubleLittleEndian(array);
        }

        #endregion

        #region ToDecimalLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ToDecimalLE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Decimal));

            return ToDecimalLECore(array.AsReadOnlySpan(startIndex, sizeof(Decimal)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ToDecimalLE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(array));

            return ToDecimalLECore(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ToDecimalLE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(array));

            return ToDecimalLECore(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ToDecimalLE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(array));

            return ToDecimalLECore(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ToDecimalLE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(array));

            return ToDecimalLECore(array);
        }

        #endregion

        #region ToInt16BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16BE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Int16));

            return BinaryPrimitives.ReadInt16BigEndian(array.AsReadOnlySpan(startIndex, sizeof(Int16)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16BE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt16BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16BE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt16BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16BE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt16BigEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ToInt16BE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt16BigEndian(array);
        }

        #endregion

        #region ToUInt16BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ToUInt16BE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(UInt16));

            return BinaryPrimitives.ReadUInt16BigEndian(array.AsReadOnlySpan(startIndex, sizeof(UInt16)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ToUInt16BE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt16BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ToUInt16BE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt16BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ToUInt16BE(this Span<Byte> array)
        {
            if (array.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt16BigEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ToUInt16BE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt16BigEndian(array);
        }

        #endregion

        #region ToInt32BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32BE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Int32));

            return BinaryPrimitives.ReadInt32BigEndian(array.AsReadOnlySpan(startIndex, sizeof(Int32)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32BE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt32BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32BE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt32BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32BE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt32BigEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ToInt32BE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt32BigEndian(array);
        }

        #endregion

        #region ToUInt32BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ToUInt32BE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(UInt32));

            return BinaryPrimitives.ReadUInt32BigEndian(array.AsReadOnlySpan(startIndex, sizeof(UInt32)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ToUInt32BE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt32BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ToUInt32BE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt32BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ToUInt32BE(this Span<Byte> array)
        {
            if (array.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt32BigEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ToUInt32BE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt32BigEndian(array);
        }

        #endregion

        #region ToInt64BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64BE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Int64));

            return BinaryPrimitives.ReadInt64BigEndian(array.AsReadOnlySpan(startIndex, sizeof(Int64)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64BE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt64BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64BE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt64BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64BE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt64BigEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ToInt64BE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt64BigEndian(array);
        }

        #endregion

        #region ToUInt64BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ToUInt64BE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(UInt64));

            return BinaryPrimitives.ReadUInt64BigEndian(array.AsReadOnlySpan(startIndex, sizeof(UInt64)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ToUInt64BE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt64BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ToUInt64BE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt64BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ToUInt64BE(this Span<Byte> array)
        {
            if (array.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt64BigEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ToUInt64BE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt64BigEndian(array);
        }

        #endregion

        #region ToInt128BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ToInt128BE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - _SIZE_OF_INT128);

            return BinaryPrimitives.ReadInt128BigEndian(array.AsReadOnlySpan(startIndex, _SIZE_OF_INT128));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ToInt128BE(this Memory<Byte> array)
        {
            if (array.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt128BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ToInt128BE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt128BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ToInt128BE(this Span<Byte> array)
        {
            if (array.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt128BigEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ToInt128BE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadInt128BigEndian(array);
        }

        #endregion

        #region ToUInt128BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ToUInt128BE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - _SIZE_OF_UINT128);

            return BinaryPrimitives.ReadUInt128BigEndian(array.AsReadOnlySpan(startIndex, _SIZE_OF_UINT128));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ToUInt128BE(this Memory<Byte> array)
        {
            if (array.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt128BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ToUInt128BE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt128BigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ToUInt128BE(this Span<Byte> array)
        {
            if (array.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt128BigEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ToUInt128BE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadUInt128BigEndian(array);
        }

        #endregion

        #region ToHalfBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ToHalfBE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - _SIZE_OF_HALF);

            return BinaryPrimitives.ReadHalfBigEndian(array.AsReadOnlySpan(startIndex, _SIZE_OF_HALF));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ToHalfBE(this Memory<Byte> array)
        {
            if (array.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadHalfBigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ToHalfBE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadHalfBigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ToHalfBE(this Span<Byte> array)
        {
            if (array.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadHalfBigEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ToHalfBE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadHalfBigEndian(array);
        }

        #endregion

        #region ToSingleBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ToSingleBE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Single));

            return BinaryPrimitives.ReadSingleBigEndian(array.AsReadOnlySpan(startIndex, sizeof(Single)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ToSingleBE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadSingleBigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ToSingleBE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadSingleBigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ToSingleBE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadSingleBigEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ToSingleBE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadSingleBigEndian(array);
        }

        #endregion

        #region ToDoubleBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ToDoubleBE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Double));

            return BinaryPrimitives.ReadDoubleBigEndian(array.AsReadOnlySpan(startIndex, sizeof(Double)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ToDoubleBE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadDoubleBigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ToDoubleBE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadDoubleBigEndian(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ToDoubleBE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadDoubleBigEndian(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ToDoubleBE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(array));

            return BinaryPrimitives.ReadDoubleBigEndian(array);
        }

        #endregion

        #region ToDecimalBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ToDecimalBE(this Byte[] array, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(array);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (array.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(array));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, array.Length - sizeof(Decimal));

            return ToDecimalBECore(array.AsReadOnlySpan(startIndex, sizeof(Decimal)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ToDecimalBE(this Memory<Byte> array)
        {
            if (array.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(array));

            return ToDecimalBECore(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ToDecimalBE(this ReadOnlyMemory<Byte> array)
        {
            if (array.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(array));

            return ToDecimalBECore(array.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ToDecimalBE(this Span<Byte> array)
        {
            if (array.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(array));

            return ToDecimalBECore(array);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ToDecimalBE(this ReadOnlySpan<Byte> array)
        {
            if (array.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(array));

            return ToDecimalBECore(array);
        }

        #endregion

        #region SetValueLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, Int16 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Int16));

            BinaryPrimitives.WriteInt16LittleEndian(buffer.AsSpan(startIndex, sizeof(Int16)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, Int16 value)
        {
            if (buffer.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt16LittleEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, Int16 value)
        {
            if (buffer.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt16LittleEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, UInt16 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(UInt16));

            BinaryPrimitives.WriteUInt16LittleEndian(buffer.AsSpan(startIndex, sizeof(UInt16)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, UInt16 value)
        {
            if (buffer.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt16LittleEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, UInt16 value)
        {
            if (buffer.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt16LittleEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, Int32 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Int32));

            BinaryPrimitives.WriteInt32LittleEndian(buffer.AsSpan(startIndex, sizeof(Int32)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, Int32 value)
        {
            if (buffer.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt32LittleEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, Int32 value)
        {
            if (buffer.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt32LittleEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, UInt32 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(UInt32));

            BinaryPrimitives.WriteUInt32LittleEndian(buffer.AsSpan(startIndex, sizeof(UInt32)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, UInt32 value)
        {
            if (buffer.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt32LittleEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, UInt32 value)
        {
            if (buffer.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt32LittleEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, Int64 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Int64));

            BinaryPrimitives.WriteInt64LittleEndian(buffer.AsSpan(startIndex, sizeof(Int64)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, Int64 value)
        {
            if (buffer.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt64LittleEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, Int64 value)
        {
            if (buffer.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt64LittleEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, UInt64 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(UInt64));

            BinaryPrimitives.WriteUInt64LittleEndian(buffer.AsSpan(startIndex, sizeof(UInt64)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, UInt64 value)
        {
            if (buffer.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt64LittleEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, UInt64 value)
        {
            if (buffer.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt64LittleEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, Int128 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - _SIZE_OF_INT128);

            BinaryPrimitives.WriteInt128LittleEndian(buffer.AsSpan(startIndex, _SIZE_OF_INT128), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, Int128 value)
        {
            if (buffer.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt128LittleEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, Int128 value)
        {
            if (buffer.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt128LittleEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, UInt128 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - _SIZE_OF_UINT128);

            BinaryPrimitives.WriteUInt128LittleEndian(buffer.AsSpan(startIndex, _SIZE_OF_UINT128), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, UInt128 value)
        {
            if (buffer.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt128LittleEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, UInt128 value)
        {
            if (buffer.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt128LittleEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, Half value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - _SIZE_OF_HALF);

            BinaryPrimitives.WriteHalfLittleEndian(buffer.AsSpan(startIndex, _SIZE_OF_HALF), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, Half value)
        {
            if (buffer.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteHalfLittleEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, Half value)
        {
            if (buffer.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteHalfLittleEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, Single value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Single));

            BinaryPrimitives.WriteSingleLittleEndian(buffer.AsSpan(startIndex, sizeof(Single)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, Single value)
        {
            if (buffer.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteSingleLittleEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, Single value)
        {
            if (buffer.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteSingleLittleEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, Double value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Double));

            BinaryPrimitives.WriteDoubleLittleEndian(buffer.AsSpan(startIndex, sizeof(Double)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, Double value)
        {
            if (buffer.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteDoubleLittleEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, Double value)
        {
            if (buffer.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteDoubleLittleEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Byte[] buffer, Decimal value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Decimal));

            SetValueLECore(buffer.AsSpan(startIndex, sizeof(Decimal)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Memory<Byte> buffer, Decimal value)
        {
            if (buffer.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(buffer));

            SetValueLECore(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueLE(this Span<Byte> buffer, Decimal value)
        {
            if (buffer.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(buffer));

            SetValueLECore(buffer, value);
        }

        #endregion

        #region SetValueBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, Int16 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Int16));

            BinaryPrimitives.WriteInt16BigEndian(buffer.AsSpan(startIndex, sizeof(Int16)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, Int16 value)
        {
            if (buffer.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt16BigEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, Int16 value)
        {
            if (buffer.Length < sizeof(Int16))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt16BigEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, UInt16 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(UInt16));

            BinaryPrimitives.WriteUInt16BigEndian(buffer.AsSpan(startIndex, sizeof(UInt16)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, UInt16 value)
        {
            if (buffer.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt16BigEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, UInt16 value)
        {
            if (buffer.Length < sizeof(UInt16))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt16BigEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, Int32 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Int32));

            BinaryPrimitives.WriteInt32BigEndian(buffer.AsSpan(startIndex, sizeof(Int32)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, Int32 value)
        {
            if (buffer.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt32BigEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, Int32 value)
        {
            if (buffer.Length < sizeof(Int32))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt32BigEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, UInt32 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(UInt32));

            BinaryPrimitives.WriteUInt32BigEndian(buffer.AsSpan(startIndex, sizeof(UInt32)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, UInt32 value)
        {
            if (buffer.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt32BigEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, UInt32 value)
        {
            if (buffer.Length < sizeof(UInt32))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt32BigEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, Int64 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Int64));

            BinaryPrimitives.WriteInt64BigEndian(buffer.AsSpan(startIndex, sizeof(Int64)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, Int64 value)
        {
            if (buffer.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt64BigEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, Int64 value)
        {
            if (buffer.Length < sizeof(Int64))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt64BigEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, UInt64 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(UInt64));

            BinaryPrimitives.WriteUInt64BigEndian(buffer.AsSpan(startIndex, sizeof(UInt64)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, UInt64 value)
        {
            if (buffer.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt64BigEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, UInt64 value)
        {
            if (buffer.Length < sizeof(UInt64))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt64BigEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, Int128 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - _SIZE_OF_INT128);

            BinaryPrimitives.WriteInt128BigEndian(buffer.AsSpan(startIndex, _SIZE_OF_INT128), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, Int128 value)
        {
            if (buffer.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt128BigEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, Int128 value)
        {
            if (buffer.Length < _SIZE_OF_INT128)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteInt128BigEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, UInt128 value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - _SIZE_OF_UINT128);

            BinaryPrimitives.WriteUInt128BigEndian(buffer.AsSpan(startIndex, _SIZE_OF_UINT128), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, UInt128 value)
        {
            if (buffer.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt128BigEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, UInt128 value)
        {
            if (buffer.Length < _SIZE_OF_UINT128)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteUInt128BigEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, Half value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - _SIZE_OF_HALF);

            BinaryPrimitives.WriteHalfBigEndian(buffer.AsSpan(startIndex, _SIZE_OF_HALF), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, Half value)
        {
            if (buffer.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteHalfBigEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, Half value)
        {
            if (buffer.Length < _SIZE_OF_HALF)
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteHalfBigEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, Single value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Single));

            BinaryPrimitives.WriteSingleBigEndian(buffer.AsSpan(startIndex, sizeof(Single)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, Single value)
        {
            if (buffer.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteSingleBigEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, Single value)
        {
            if (buffer.Length < sizeof(Single))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteSingleBigEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, Double value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Double));

            BinaryPrimitives.WriteDoubleBigEndian(buffer.AsSpan(startIndex, sizeof(Double)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, Double value)
        {
            if (buffer.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteDoubleBigEndian(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, Double value)
        {
            if (buffer.Length < sizeof(Double))
                throw new ArgumentException("Too short array", nameof(buffer));

            BinaryPrimitives.WriteDoubleBigEndian(buffer, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Byte[] buffer, Decimal value, Int32 startIndex = 0)
        {
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            if (buffer.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(buffer));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, buffer.Length - sizeof(Decimal));

            SetValueBECore(buffer.AsSpan(startIndex, sizeof(Decimal)), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Memory<Byte> buffer, Decimal value)
        {
            if (buffer.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(buffer));

            SetValueBECore(buffer.Span, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void SetValueBE(this Span<Byte> buffer, Decimal value)
        {
            if (buffer.Length < sizeof(Decimal))
                throw new ArgumentException("Too short array", nameof(buffer));

            SetValueBECore(buffer, value);
        }

        #endregion
    }
}
