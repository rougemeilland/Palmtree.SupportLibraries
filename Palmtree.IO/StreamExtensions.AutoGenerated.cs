using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Palmtree.IO
{
    public static partial class StreamExtensions
    {
        #region ReadInt16LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ReadInt16LE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int16))
                throw new EndOfStreamException();

            return buffer.ToInt16LE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ReadInt16LE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int16))
                throw new EndOfStreamException();

            return buffer.ToInt16LE();
        }

        #endregion

        #region ReadUInt16LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ReadUInt16LE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt16))
                throw new EndOfStreamException();

            return buffer.ToUInt16LE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ReadUInt16LE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt16))
                throw new EndOfStreamException();

            return buffer.ToUInt16LE();
        }

        #endregion

        #region ReadInt32LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ReadInt32LE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int32))
                throw new EndOfStreamException();

            return buffer.ToInt32LE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ReadInt32LE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int32))
                throw new EndOfStreamException();

            return buffer.ToInt32LE();
        }

        #endregion

        #region ReadUInt32LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ReadUInt32LE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt32))
                throw new EndOfStreamException();

            return buffer.ToUInt32LE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ReadUInt32LE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt32))
                throw new EndOfStreamException();

            return buffer.ToUInt32LE();
        }

        #endregion

        #region ReadInt64LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ReadInt64LE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int64))
                throw new EndOfStreamException();

            return buffer.ToInt64LE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ReadInt64LE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int64))
                throw new EndOfStreamException();

            return buffer.ToInt64LE();
        }

        #endregion

        #region ReadUInt64LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ReadUInt64LE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt64))
                throw new EndOfStreamException();

            return buffer.ToUInt64LE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ReadUInt64LE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt64))
                throw new EndOfStreamException();

            return buffer.ToUInt64LE();
        }

        #endregion

        #region ReadInt128LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ReadInt128LE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_INT128];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_INT128)
                throw new EndOfStreamException();

            return buffer.ToInt128LE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ReadInt128LE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_INT128];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_INT128)
                throw new EndOfStreamException();

            return buffer.ToInt128LE();
        }

        #endregion

        #region ReadUInt128LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ReadUInt128LE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_UINT128];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_UINT128)
                throw new EndOfStreamException();

            return buffer.ToUInt128LE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ReadUInt128LE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_UINT128];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_UINT128)
                throw new EndOfStreamException();

            return buffer.ToUInt128LE();
        }

        #endregion

        #region ReadHalfLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ReadHalfLE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_HALF];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_HALF)
                throw new EndOfStreamException();

            return buffer.ToHalfLE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ReadHalfLE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_HALF];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_HALF)
                throw new EndOfStreamException();

            return buffer.ToHalfLE();
        }

        #endregion

        #region ReadSingleLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ReadSingleLE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Single))
                throw new EndOfStreamException();

            return buffer.ToSingleLE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ReadSingleLE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Single))
                throw new EndOfStreamException();

            return buffer.ToSingleLE();
        }

        #endregion

        #region ReadDoubleLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ReadDoubleLE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Double))
                throw new EndOfStreamException();

            return buffer.ToDoubleLE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ReadDoubleLE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Double))
                throw new EndOfStreamException();

            return buffer.ToDoubleLE();
        }

        #endregion

        #region ReadDecimalLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ReadDecimalLE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Decimal))
                throw new EndOfStreamException();

            return buffer.ToDecimalLE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ReadDecimalLE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Decimal))
                throw new EndOfStreamException();

            return buffer.ToDecimalLE();
        }

        #endregion

        #region ReadInt16BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ReadInt16BE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int16))
                throw new EndOfStreamException();

            return buffer.ToInt16BE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int16 ReadInt16BE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int16))
                throw new EndOfStreamException();

            return buffer.ToInt16BE();
        }

        #endregion

        #region ReadUInt16BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ReadUInt16BE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt16))
                throw new EndOfStreamException();

            return buffer.ToUInt16BE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt16 ReadUInt16BE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt16))
                throw new EndOfStreamException();

            return buffer.ToUInt16BE();
        }

        #endregion

        #region ReadInt32BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ReadInt32BE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int32))
                throw new EndOfStreamException();

            return buffer.ToInt32BE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 ReadInt32BE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int32))
                throw new EndOfStreamException();

            return buffer.ToInt32BE();
        }

        #endregion

        #region ReadUInt32BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ReadUInt32BE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt32))
                throw new EndOfStreamException();

            return buffer.ToUInt32BE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 ReadUInt32BE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt32))
                throw new EndOfStreamException();

            return buffer.ToUInt32BE();
        }

        #endregion

        #region ReadInt64BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ReadInt64BE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int64))
                throw new EndOfStreamException();

            return buffer.ToInt64BE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int64 ReadInt64BE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Int64))
                throw new EndOfStreamException();

            return buffer.ToInt64BE();
        }

        #endregion

        #region ReadUInt64BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ReadUInt64BE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt64))
                throw new EndOfStreamException();

            return buffer.ToUInt64BE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt64 ReadUInt64BE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(UInt64))
                throw new EndOfStreamException();

            return buffer.ToUInt64BE();
        }

        #endregion

        #region ReadInt128BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ReadInt128BE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_INT128];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_INT128)
                throw new EndOfStreamException();

            return buffer.ToInt128BE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int128 ReadInt128BE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_INT128];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_INT128)
                throw new EndOfStreamException();

            return buffer.ToInt128BE();
        }

        #endregion

        #region ReadUInt128BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ReadUInt128BE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_UINT128];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_UINT128)
                throw new EndOfStreamException();

            return buffer.ToUInt128BE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt128 ReadUInt128BE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_UINT128];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_UINT128)
                throw new EndOfStreamException();

            return buffer.ToUInt128BE();
        }

        #endregion

        #region ReadHalfBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ReadHalfBE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_HALF];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_HALF)
                throw new EndOfStreamException();

            return buffer.ToHalfBE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Half ReadHalfBE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_HALF];
            if (sourceStream.ReadBytesCore(buffer) != _SIZE_OF_HALF)
                throw new EndOfStreamException();

            return buffer.ToHalfBE();
        }

        #endregion

        #region ReadSingleBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ReadSingleBE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Single))
                throw new EndOfStreamException();

            return buffer.ToSingleBE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Single ReadSingleBE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Single))
                throw new EndOfStreamException();

            return buffer.ToSingleBE();
        }

        #endregion

        #region ReadDoubleBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ReadDoubleBE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Double))
                throw new EndOfStreamException();

            return buffer.ToDoubleBE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Double ReadDoubleBE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Double))
                throw new EndOfStreamException();

            return buffer.ToDoubleBE();
        }

        #endregion

        #region ReadDecimalBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ReadDecimalBE(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Decimal))
                throw new EndOfStreamException();

            return buffer.ToDecimalBE();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Decimal ReadDecimalBE(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            if (sourceStream.ReadBytesCore(buffer) != sizeof(Decimal))
                throw new EndOfStreamException();

            return buffer.ToDecimalBE();
        }

        #endregion

        #region WriteInt16LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt16LE(this Stream destinationStream, Int16 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt16LE(this ISequentialOutputByteStream destinationStream, Int16 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteUInt16LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt16LE(this Stream destinationStream, UInt16 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt16LE(this ISequentialOutputByteStream destinationStream, UInt16 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteInt32LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt32LE(this Stream destinationStream, Int32 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt32LE(this ISequentialOutputByteStream destinationStream, Int32 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteUInt32LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt32LE(this Stream destinationStream, UInt32 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt32LE(this ISequentialOutputByteStream destinationStream, UInt32 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteInt64LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt64LE(this Stream destinationStream, Int64 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt64LE(this ISequentialOutputByteStream destinationStream, Int64 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteUInt64LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt64LE(this Stream destinationStream, UInt64 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt64LE(this ISequentialOutputByteStream destinationStream, UInt64 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteInt128LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt128LE(this Stream destinationStream, Int128 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_INT128];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt128LE(this ISequentialOutputByteStream destinationStream, Int128 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_INT128];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteUInt128LE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt128LE(this Stream destinationStream, UInt128 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_UINT128];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt128LE(this ISequentialOutputByteStream destinationStream, UInt128 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_UINT128];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteHalfLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteHalfLE(this Stream destinationStream, Half value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_HALF];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteHalfLE(this ISequentialOutputByteStream destinationStream, Half value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_HALF];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteSingleLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteSingleLE(this Stream destinationStream, Single value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteSingleLE(this ISequentialOutputByteStream destinationStream, Single value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteDoubleLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteDoubleLE(this Stream destinationStream, Double value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteDoubleLE(this ISequentialOutputByteStream destinationStream, Double value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteDecimalLE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteDecimalLE(this Stream destinationStream, Decimal value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteDecimalLE(this ISequentialOutputByteStream destinationStream, Decimal value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            buffer.SetValueLE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteInt16BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt16BE(this Stream destinationStream, Int16 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt16BE(this ISequentialOutputByteStream destinationStream, Int16 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteUInt16BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt16BE(this Stream destinationStream, UInt16 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt16BE(this ISequentialOutputByteStream destinationStream, UInt16 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteInt32BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt32BE(this Stream destinationStream, Int32 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt32BE(this ISequentialOutputByteStream destinationStream, Int32 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteUInt32BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt32BE(this Stream destinationStream, UInt32 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt32BE(this ISequentialOutputByteStream destinationStream, UInt32 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteInt64BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt64BE(this Stream destinationStream, Int64 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt64BE(this ISequentialOutputByteStream destinationStream, Int64 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteUInt64BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt64BE(this Stream destinationStream, UInt64 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt64BE(this ISequentialOutputByteStream destinationStream, UInt64 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteInt128BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt128BE(this Stream destinationStream, Int128 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_INT128];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteInt128BE(this ISequentialOutputByteStream destinationStream, Int128 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_INT128];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteUInt128BE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt128BE(this Stream destinationStream, UInt128 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_UINT128];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteUInt128BE(this ISequentialOutputByteStream destinationStream, UInt128 value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_UINT128];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteHalfBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteHalfBE(this Stream destinationStream, Half value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_HALF];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteHalfBE(this ISequentialOutputByteStream destinationStream, Half value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[_SIZE_OF_HALF];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteSingleBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteSingleBE(this Stream destinationStream, Single value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteSingleBE(this ISequentialOutputByteStream destinationStream, Single value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteDoubleBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteDoubleBE(this Stream destinationStream, Double value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteDoubleBE(this ISequentialOutputByteStream destinationStream, Double value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion

        #region WriteDecimalBE

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteDecimalBE(this Stream destinationStream, Decimal value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteDecimalBE(this ISequentialOutputByteStream destinationStream, Decimal value)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            buffer.SetValueBE(value);
            destinationStream.WriteBytesCore(buffer);
        }

        #endregion
    }
}
