using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO
{
    public static partial class AsyncStreamExtensions
    {
        #region ReadAsync

#if false
        public static Task<Int32> ReadAsync(this Stream sourceStream, Byte[] buffer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException(); // defined in System.IO.Stream.ReadAsync(Memory<Byte>, CancellationToken)
        }
#endif

        public static Task<Int32> ReadAsync(this Stream sourceStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return sourceStream.ReadAsync(buffer.AsMemory(offset), cancellationToken).AsTask();
        }

        public static async Task<UInt32> ReadAsync(this Stream sourceStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            var length =
                await sourceStream.ReadAsync(
                    buffer.AsMemory(offset),
                    cancellationToken)
                .ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadAsync(this Stream sourceStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            return sourceStream.ReadAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();
        }

        public static async Task<UInt32> ReadAsync(this Stream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);

            var length =
                await sourceStream.ReadAsync(
                    buffer.AsMemory(offset, count),
                    cancellationToken)
                .ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return sourceStream.ReadAsync(buffer.AsMemory(offset), cancellationToken);
        }

        public static async Task<UInt32> ReadAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            var length =
                await sourceStream.ReadAsync(
                    buffer.AsMemory(offset),
                    cancellationToken)
                .ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            return sourceStream.ReadAsync(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static Task<Int32> ReadAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return sourceStream.ReadAsync(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static async Task<UInt32> ReadAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);

            var length =
                await sourceStream.ReadAsync(
                    buffer.AsMemory(offset, count),
                    cancellationToken)
                .ConfigureAwait(false);
            return checked((UInt32)length);
        }

        #endregion

        #region ReadByteOrNullAsync

        public static async Task<Byte?> ReadByteOrNullAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(1);
            try
            {
                return
                    await sourceStream.ReadAsync(buffer.AsMemory(0, 1), cancellationToken).ConfigureAwait(false) > 0
                    ? buffer[0]
                    : null;
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Byte?> ReadByteOrNullAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(1);
            try
            {
                return
                    await sourceStream.ReadAsync(buffer.AsMemory(0, 1), cancellationToken).ConfigureAwait(false) > 0
                ? buffer[0]
                : null;
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadByteAsync

        public static async Task<Byte> ReadByteAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = ArrayPool<Byte>.Shared.Rent(1);
            try
            {
                if (await sourceStream.ReadAsync(buffer.AsMemory(0, 1), cancellationToken).ConfigureAwait(false) <= 0)
                    throw new EndOfStreamException();

                return buffer[0];
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task<Byte> ReadByteAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            var buffer = ArrayPool<Byte>.Shared.Rent(1);
            try
            {
                if (await sourceStream.ReadAsync(buffer.AsMemory(0, 1), cancellationToken).ConfigureAwait(false) <= 0)
                    throw new EndOfStreamException();

                return buffer[0];
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region ReadBytesAsync

        public static Task<ReadOnlyMemory<Byte>> ReadBytesAsync(this Stream sourceStream, Int32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            return sourceStream.ReadBytesAsyncCore(count, cancellationToken);
        }

        public static Task<ReadOnlyMemory<Byte>> ReadBytesAsync(this Stream sourceStream, UInt32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.ReadBytesAsyncCore(checked((Int32)count), cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);

            return sourceStream.ReadBytesAsyncCore(buffer, cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return sourceStream.ReadBytesAsyncCore(buffer.AsMemory(offset), cancellationToken);
        }

        public static async Task<UInt32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            var length = await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(offset), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            return sourceStream.ReadBytesAsyncCore(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return sourceStream.ReadBytesAsyncCore(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static async Task<UInt32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            var length = await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadBytesAsync(this Stream sourceStream, Memory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.ReadBytesAsyncCore(buffer, cancellationToken);
        }

        public static Task<ReadOnlyMemory<Byte>> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Int32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            return sourceStream.ReadBytesAsyncCore(count, cancellationToken);
        }

        public static Task<ReadOnlyMemory<Byte>> ReadBytesAsync(this ISequentialInputByteStream sourceStream, UInt32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return sourceStream.ReadBytesAsyncCore(checked((Int32)count), cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);

            return sourceStream.ReadBytesAsyncCore(buffer, cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return sourceStream.ReadBytesAsyncCore(buffer.AsMemory(offset), cancellationToken);
        }

        public static async Task<UInt32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            var length = await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(offset), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            return sourceStream.ReadBytesAsyncCore(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return sourceStream.ReadBytesAsyncCore(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static async Task<UInt32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            var length = await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Memory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return sourceStream.ReadBytesAsyncCore(buffer, cancellationToken);
        }

        #endregion

        #region ReadAllBytesAsync

        public static Task<ReadOnlyMemory<Byte>> ReadAllBytesAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.ReadAllBytesAsyncCore(cancellationToken);
        }

        public static Task<ReadOnlyMemory<Byte>> ReadAllBytesAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return sourceStream.ReadAllBytesAsyncCore(cancellationToken);
        }

        #endregion

        #region ReadBytesAsyncCore

        private static async Task<ReadOnlyMemory<Byte>> ReadBytesAsyncCore(this Stream sourceStream, Int32 count, CancellationToken cancellationToken)
        {
            var buffer = new Byte[count];
            var length = await sourceStream.ReadBytesAsyncCore(buffer, cancellationToken).ConfigureAwait(false);
            if (length < buffer.Length)
                Array.Resize(ref buffer, length);
            return buffer;
        }

        private static async Task<ReadOnlyMemory<Byte>> ReadBytesAsyncCore(this ISequentialInputByteStream sourceStream, Int32 count, CancellationToken cancellationToken)
        {
            var buffer = new Byte[count];
            var length = await sourceStream.ReadBytesAsyncCore(buffer, cancellationToken).ConfigureAwait(false);
            if (length < buffer.Length)
                Array.Resize(ref buffer, length);
            return buffer;
        }

        private static async Task<Int32> ReadBytesAsyncCore(this Stream sourceStream, Memory<Byte> buffer, CancellationToken cancellationToken)
        {
            var totalLength = 0;
            while (!buffer.IsEmpty)
            {
                var length = await sourceStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
                if (length <= 0)
                    break;
                buffer = buffer[length..];
                totalLength += totalLength;
            }

            return totalLength;
        }

        private static async Task<Int32> ReadBytesAsyncCore(this ISequentialInputByteStream sourceStream, Memory<Byte> buffer, CancellationToken cancellationToken)
        {
            var totalLength = 0;
            while (!buffer.IsEmpty)
            {
                var length = await sourceStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
                if (length <= 0)
                    break;
                buffer = buffer[length..];
                totalLength += totalLength;
            }

            return totalLength;
        }

        #endregion

        #region ReadAllBytesAsyncCore

        private static async Task<ReadOnlyMemory<Byte>> ReadAllBytesAsyncCore(this Stream sourceStream, CancellationToken cancellation)
        {
            const Int32 BUFFER_SIZE = 80 * 1024;
            var buffers = new Queue<Byte[]>();
            var totalLength = 0;
            while (true)
            {
                var partialBuffer = new Byte[BUFFER_SIZE];
                var length = await sourceStream.ReadAsync(partialBuffer, cancellation).ConfigureAwait(false);
                if (length <= 0)
                    break;
                if (length < partialBuffer.Length)
                    Array.Resize(ref partialBuffer, length);
                buffers.Enqueue(partialBuffer);
                totalLength += length;
            }

            return ConcatBuffer(buffers, totalLength);
        }

        private static async Task<ReadOnlyMemory<Byte>> ReadAllBytesAsyncCore(this ISequentialInputByteStream sourceStream, CancellationToken cancellation)
        {
            const Int32 BUFFER_SIZE = 80 * 1024;
            var buffers = new Queue<Byte[]>();
            var totalLength = 0;
            while (true)
            {
                var partialBuffer = new Byte[BUFFER_SIZE];
                var length = await sourceStream.ReadAsync(partialBuffer, cancellation).ConfigureAwait(false);
                if (length <= 0)
                    break;
                if (length < partialBuffer.Length)
                    Array.Resize(ref partialBuffer, length);
                buffers.Enqueue(partialBuffer);
                totalLength += length;
            }

            return ConcatBuffer(buffers, totalLength);
        }

        private static ReadOnlyMemory<Byte> ConcatBuffer(Queue<Byte[]> buffers, Int32 totalLength)
        {
            if (buffers.Count <= 0)
                return ReadOnlyMemory<Byte>.Empty;
            if (buffers.Count == 1)
                return buffers.Dequeue();
            var buffer = new Byte[totalLength].AsMemory();
            var destinationWindow = buffer;
            while (buffers.Count > 0)
            {
                var partialBuffer = buffers.Dequeue();
                partialBuffer.CopyTo(destinationWindow);
                destinationWindow = destinationWindow[partialBuffer.Length..];
            }
#if DEBUG
            Validation.Assert(destinationWindow.IsEmpty);
#endif
            return buffer;
        }

        #endregion
    }
}
