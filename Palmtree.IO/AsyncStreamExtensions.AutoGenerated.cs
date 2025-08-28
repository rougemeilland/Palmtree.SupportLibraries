using System;
using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO
{
    public static partial class AsyncStreamExtensions
    {
        #region ReadInt16LEAsync

        public static async Task<Int16> ReadInt16LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int16));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int16)), cancellationToken).ConfigureAwait(false) != sizeof(Int16))
                    throw new EndOfStreamException();
                return buffer.ToInt16LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Int16> ReadInt16LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int16));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int16)), cancellationToken).ConfigureAwait(false) != sizeof(Int16))
                    throw new EndOfStreamException();

                return buffer.ToInt16LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadUInt16LEAsync

        public static async Task<UInt16> ReadUInt16LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt16));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt16)), cancellationToken).ConfigureAwait(false) != sizeof(UInt16))
                    throw new EndOfStreamException();
                return buffer.ToUInt16LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<UInt16> ReadUInt16LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt16));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt16)), cancellationToken).ConfigureAwait(false) != sizeof(UInt16))
                    throw new EndOfStreamException();

                return buffer.ToUInt16LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadInt32LEAsync

        public static async Task<Int32> ReadInt32LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int32));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int32)), cancellationToken).ConfigureAwait(false) != sizeof(Int32))
                    throw new EndOfStreamException();
                return buffer.ToInt32LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Int32> ReadInt32LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int32));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int32)), cancellationToken).ConfigureAwait(false) != sizeof(Int32))
                    throw new EndOfStreamException();

                return buffer.ToInt32LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadUInt32LEAsync

        public static async Task<UInt32> ReadUInt32LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt32));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt32)), cancellationToken).ConfigureAwait(false) != sizeof(UInt32))
                    throw new EndOfStreamException();
                return buffer.ToUInt32LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<UInt32> ReadUInt32LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt32));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt32)), cancellationToken).ConfigureAwait(false) != sizeof(UInt32))
                    throw new EndOfStreamException();

                return buffer.ToUInt32LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadInt64LEAsync

        public static async Task<Int64> ReadInt64LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int64));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int64)), cancellationToken).ConfigureAwait(false) != sizeof(Int64))
                    throw new EndOfStreamException();
                return buffer.ToInt64LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Int64> ReadInt64LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int64));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int64)), cancellationToken).ConfigureAwait(false) != sizeof(Int64))
                    throw new EndOfStreamException();

                return buffer.ToInt64LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadUInt64LEAsync

        public static async Task<UInt64> ReadUInt64LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt64));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt64)), cancellationToken).ConfigureAwait(false) != sizeof(UInt64))
                    throw new EndOfStreamException();
                return buffer.ToUInt64LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<UInt64> ReadUInt64LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt64));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt64)), cancellationToken).ConfigureAwait(false) != sizeof(UInt64))
                    throw new EndOfStreamException();

                return buffer.ToUInt64LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadInt128LEAsync

        public static async Task<Int128> ReadInt128LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_INT128);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_INT128), cancellationToken).ConfigureAwait(false) != _SIZE_OF_INT128)
                    throw new EndOfStreamException();
                return buffer.ToInt128LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Int128> ReadInt128LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_INT128);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_INT128), cancellationToken).ConfigureAwait(false) != _SIZE_OF_INT128)
                    throw new EndOfStreamException();

                return buffer.ToInt128LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadUInt128LEAsync

        public static async Task<UInt128> ReadUInt128LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_UINT128);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_UINT128), cancellationToken).ConfigureAwait(false) != _SIZE_OF_UINT128)
                    throw new EndOfStreamException();
                return buffer.ToUInt128LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<UInt128> ReadUInt128LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_UINT128);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_UINT128), cancellationToken).ConfigureAwait(false) != _SIZE_OF_UINT128)
                    throw new EndOfStreamException();

                return buffer.ToUInt128LE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadHalfLEAsync

        public static async Task<Half> ReadHalfLEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_HALF);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_HALF), cancellationToken).ConfigureAwait(false) != _SIZE_OF_HALF)
                    throw new EndOfStreamException();
                return buffer.ToHalfLE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Half> ReadHalfLEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_HALF);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_HALF), cancellationToken).ConfigureAwait(false) != _SIZE_OF_HALF)
                    throw new EndOfStreamException();

                return buffer.ToHalfLE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadSingleLEAsync

        public static async Task<Single> ReadSingleLEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Single));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Single)), cancellationToken).ConfigureAwait(false) != sizeof(Single))
                    throw new EndOfStreamException();
                return buffer.ToSingleLE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Single> ReadSingleLEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Single));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Single)), cancellationToken).ConfigureAwait(false) != sizeof(Single))
                    throw new EndOfStreamException();

                return buffer.ToSingleLE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadDoubleLEAsync

        public static async Task<Double> ReadDoubleLEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Double));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Double)), cancellationToken).ConfigureAwait(false) != sizeof(Double))
                    throw new EndOfStreamException();
                return buffer.ToDoubleLE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Double> ReadDoubleLEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Double));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Double)), cancellationToken).ConfigureAwait(false) != sizeof(Double))
                    throw new EndOfStreamException();

                return buffer.ToDoubleLE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadDecimalLEAsync

        public static async Task<Decimal> ReadDecimalLEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Decimal));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Decimal)), cancellationToken).ConfigureAwait(false) != sizeof(Decimal))
                    throw new EndOfStreamException();
                return buffer.ToDecimalLE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Decimal> ReadDecimalLEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Decimal));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Decimal)), cancellationToken).ConfigureAwait(false) != sizeof(Decimal))
                    throw new EndOfStreamException();

                return buffer.ToDecimalLE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadInt16BEAsync

        public static async Task<Int16> ReadInt16BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int16));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int16)), cancellationToken).ConfigureAwait(false) != sizeof(Int16))
                    throw new EndOfStreamException();
                return buffer.ToInt16BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Int16> ReadInt16BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int16));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int16)), cancellationToken).ConfigureAwait(false) != sizeof(Int16))
                    throw new EndOfStreamException();

                return buffer.ToInt16BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadUInt16BEAsync

        public static async Task<UInt16> ReadUInt16BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt16));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt16)), cancellationToken).ConfigureAwait(false) != sizeof(UInt16))
                    throw new EndOfStreamException();
                return buffer.ToUInt16BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<UInt16> ReadUInt16BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt16));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt16)), cancellationToken).ConfigureAwait(false) != sizeof(UInt16))
                    throw new EndOfStreamException();

                return buffer.ToUInt16BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadInt32BEAsync

        public static async Task<Int32> ReadInt32BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int32));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int32)), cancellationToken).ConfigureAwait(false) != sizeof(Int32))
                    throw new EndOfStreamException();
                return buffer.ToInt32BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Int32> ReadInt32BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int32));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int32)), cancellationToken).ConfigureAwait(false) != sizeof(Int32))
                    throw new EndOfStreamException();

                return buffer.ToInt32BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadUInt32BEAsync

        public static async Task<UInt32> ReadUInt32BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt32));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt32)), cancellationToken).ConfigureAwait(false) != sizeof(UInt32))
                    throw new EndOfStreamException();
                return buffer.ToUInt32BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<UInt32> ReadUInt32BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt32));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt32)), cancellationToken).ConfigureAwait(false) != sizeof(UInt32))
                    throw new EndOfStreamException();

                return buffer.ToUInt32BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadInt64BEAsync

        public static async Task<Int64> ReadInt64BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int64));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int64)), cancellationToken).ConfigureAwait(false) != sizeof(Int64))
                    throw new EndOfStreamException();
                return buffer.ToInt64BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Int64> ReadInt64BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int64));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Int64)), cancellationToken).ConfigureAwait(false) != sizeof(Int64))
                    throw new EndOfStreamException();

                return buffer.ToInt64BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadUInt64BEAsync

        public static async Task<UInt64> ReadUInt64BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt64));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt64)), cancellationToken).ConfigureAwait(false) != sizeof(UInt64))
                    throw new EndOfStreamException();
                return buffer.ToUInt64BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<UInt64> ReadUInt64BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt64));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt64)), cancellationToken).ConfigureAwait(false) != sizeof(UInt64))
                    throw new EndOfStreamException();

                return buffer.ToUInt64BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadInt128BEAsync

        public static async Task<Int128> ReadInt128BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_INT128);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_INT128), cancellationToken).ConfigureAwait(false) != _SIZE_OF_INT128)
                    throw new EndOfStreamException();
                return buffer.ToInt128BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Int128> ReadInt128BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_INT128);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_INT128), cancellationToken).ConfigureAwait(false) != _SIZE_OF_INT128)
                    throw new EndOfStreamException();

                return buffer.ToInt128BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadUInt128BEAsync

        public static async Task<UInt128> ReadUInt128BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_UINT128);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_UINT128), cancellationToken).ConfigureAwait(false) != _SIZE_OF_UINT128)
                    throw new EndOfStreamException();
                return buffer.ToUInt128BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<UInt128> ReadUInt128BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_UINT128);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_UINT128), cancellationToken).ConfigureAwait(false) != _SIZE_OF_UINT128)
                    throw new EndOfStreamException();

                return buffer.ToUInt128BE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadHalfBEAsync

        public static async Task<Half> ReadHalfBEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_HALF);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_HALF), cancellationToken).ConfigureAwait(false) != _SIZE_OF_HALF)
                    throw new EndOfStreamException();
                return buffer.ToHalfBE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Half> ReadHalfBEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_HALF);
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_HALF), cancellationToken).ConfigureAwait(false) != _SIZE_OF_HALF)
                    throw new EndOfStreamException();

                return buffer.ToHalfBE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadSingleBEAsync

        public static async Task<Single> ReadSingleBEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Single));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Single)), cancellationToken).ConfigureAwait(false) != sizeof(Single))
                    throw new EndOfStreamException();
                return buffer.ToSingleBE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Single> ReadSingleBEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Single));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Single)), cancellationToken).ConfigureAwait(false) != sizeof(Single))
                    throw new EndOfStreamException();

                return buffer.ToSingleBE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadDoubleBEAsync

        public static async Task<Double> ReadDoubleBEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Double));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Double)), cancellationToken).ConfigureAwait(false) != sizeof(Double))
                    throw new EndOfStreamException();
                return buffer.ToDoubleBE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Double> ReadDoubleBEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Double));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Double)), cancellationToken).ConfigureAwait(false) != sizeof(Double))
                    throw new EndOfStreamException();

                return buffer.ToDoubleBE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadDecimalBEAsync

        public static async Task<Decimal> ReadDecimalBEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Decimal));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Decimal)), cancellationToken).ConfigureAwait(false) != sizeof(Decimal))
                    throw new EndOfStreamException();
                return buffer.ToDecimalBE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Decimal> ReadDecimalBEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Decimal));
            try
            {
                if (await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, sizeof(Decimal)), cancellationToken).ConfigureAwait(false) != sizeof(Decimal))
                    throw new EndOfStreamException();

                return buffer.ToDecimalBE();
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteInt16LEAsync

        public static Task WriteInt16LEAsync(this Stream destinationStream, Int16 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int16));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int16)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteInt16LEAsync(this ISequentialOutputByteStream destinationStream, Int16 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int16));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int16)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteUInt16LEAsync

        public static Task WriteUInt16LEAsync(this Stream destinationStream, UInt16 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt16));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt16)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteUInt16LEAsync(this ISequentialOutputByteStream destinationStream, UInt16 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt16));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt16)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteInt32LEAsync

        public static Task WriteInt32LEAsync(this Stream destinationStream, Int32 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int32));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int32)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteInt32LEAsync(this ISequentialOutputByteStream destinationStream, Int32 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int32));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int32)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteUInt32LEAsync

        public static Task WriteUInt32LEAsync(this Stream destinationStream, UInt32 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt32));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt32)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteUInt32LEAsync(this ISequentialOutputByteStream destinationStream, UInt32 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt32));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt32)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteInt64LEAsync

        public static Task WriteInt64LEAsync(this Stream destinationStream, Int64 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int64));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int64)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteInt64LEAsync(this ISequentialOutputByteStream destinationStream, Int64 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int64));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int64)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteUInt64LEAsync

        public static Task WriteUInt64LEAsync(this Stream destinationStream, UInt64 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt64));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt64)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteUInt64LEAsync(this ISequentialOutputByteStream destinationStream, UInt64 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt64));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt64)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteInt128LEAsync

        public static Task WriteInt128LEAsync(this Stream destinationStream, Int128 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_INT128);
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_INT128), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteInt128LEAsync(this ISequentialOutputByteStream destinationStream, Int128 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_INT128);
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_INT128), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteUInt128LEAsync

        public static Task WriteUInt128LEAsync(this Stream destinationStream, UInt128 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_UINT128);
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_UINT128), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteUInt128LEAsync(this ISequentialOutputByteStream destinationStream, UInt128 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_UINT128);
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_UINT128), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteHalfLEAsync

        public static Task WriteHalfLEAsync(this Stream destinationStream, Half value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_HALF);
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_HALF), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteHalfLEAsync(this ISequentialOutputByteStream destinationStream, Half value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_HALF);
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_HALF), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteSingleLEAsync

        public static Task WriteSingleLEAsync(this Stream destinationStream, Single value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Single));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Single)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteSingleLEAsync(this ISequentialOutputByteStream destinationStream, Single value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Single));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Single)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteDoubleLEAsync

        public static Task WriteDoubleLEAsync(this Stream destinationStream, Double value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Double));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Double)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteDoubleLEAsync(this ISequentialOutputByteStream destinationStream, Double value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Double));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Double)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteDecimalLEAsync

        public static Task WriteDecimalLEAsync(this Stream destinationStream, Decimal value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Decimal));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Decimal)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteDecimalLEAsync(this ISequentialOutputByteStream destinationStream, Decimal value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Decimal));
            try
            {
                buffer.SetValueLE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Decimal)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteInt16BEAsync

        public static Task WriteInt16BEAsync(this Stream destinationStream, Int16 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int16));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int16)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteInt16BEAsync(this ISequentialOutputByteStream destinationStream, Int16 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int16));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int16)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteUInt16BEAsync

        public static Task WriteUInt16BEAsync(this Stream destinationStream, UInt16 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt16));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt16)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteUInt16BEAsync(this ISequentialOutputByteStream destinationStream, UInt16 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt16));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt16)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteInt32BEAsync

        public static Task WriteInt32BEAsync(this Stream destinationStream, Int32 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int32));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int32)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteInt32BEAsync(this ISequentialOutputByteStream destinationStream, Int32 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int32));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int32)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteUInt32BEAsync

        public static Task WriteUInt32BEAsync(this Stream destinationStream, UInt32 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt32));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt32)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteUInt32BEAsync(this ISequentialOutputByteStream destinationStream, UInt32 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt32));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt32)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteInt64BEAsync

        public static Task WriteInt64BEAsync(this Stream destinationStream, Int64 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int64));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int64)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteInt64BEAsync(this ISequentialOutputByteStream destinationStream, Int64 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Int64));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Int64)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteUInt64BEAsync

        public static Task WriteUInt64BEAsync(this Stream destinationStream, UInt64 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt64));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt64)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteUInt64BEAsync(this ISequentialOutputByteStream destinationStream, UInt64 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(UInt64));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(UInt64)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteInt128BEAsync

        public static Task WriteInt128BEAsync(this Stream destinationStream, Int128 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_INT128);
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_INT128), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteInt128BEAsync(this ISequentialOutputByteStream destinationStream, Int128 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_INT128);
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_INT128), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteUInt128BEAsync

        public static Task WriteUInt128BEAsync(this Stream destinationStream, UInt128 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_UINT128);
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_UINT128), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteUInt128BEAsync(this ISequentialOutputByteStream destinationStream, UInt128 value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_UINT128);
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_UINT128), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteHalfBEAsync

        public static Task WriteHalfBEAsync(this Stream destinationStream, Half value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_HALF);
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_HALF), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteHalfBEAsync(this ISequentialOutputByteStream destinationStream, Half value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(_SIZE_OF_HALF);
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, _SIZE_OF_HALF), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteSingleBEAsync

        public static Task WriteSingleBEAsync(this Stream destinationStream, Single value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Single));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Single)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteSingleBEAsync(this ISequentialOutputByteStream destinationStream, Single value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Single));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Single)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteDoubleBEAsync

        public static Task WriteDoubleBEAsync(this Stream destinationStream, Double value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Double));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Double)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteDoubleBEAsync(this ISequentialOutputByteStream destinationStream, Double value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Double));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Double)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteDecimalBEAsync

        public static Task WriteDecimalBEAsync(this Stream destinationStream, Decimal value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Decimal));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Decimal)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static Task WriteDecimalBEAsync(this ISequentialOutputByteStream destinationStream, Decimal value, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(sizeof(Decimal));
            try
            {
                buffer.SetValueBE(value);
                return destinationStream.WriteBytesAsyncCore(buffer.AsMemory(0, sizeof(Decimal)), cancellationToken);
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion
    }
}
