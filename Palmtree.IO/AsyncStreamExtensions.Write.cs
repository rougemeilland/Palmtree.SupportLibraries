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
        private const Int32 _WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE = 81920;

        #region WriteAsync

        public static async Task<Int32> WriteAsync(this Stream destinationStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            var count = buffer.Length - offset;
            await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return count;
        }

        public static async Task<UInt32> WriteAsync(this Stream destinationStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            var count = checked((UInt32)buffer.Length - offset);
            await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return count;
        }

        public static async Task<UInt32> WriteAsync(this Stream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);

            await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return count;
        }

        public static async Task<Int32> WriteAsync(this Stream destinationStream, ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            await destinationStream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
            return buffer.Length;
        }

        public static Task<Int32> WriteAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset), cancellationToken);
        }

        public static async Task<UInt32> WriteAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            var length = await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> WriteAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static async Task<UInt32> WriteAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);

            var length = await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        #endregion

        #region WriteByteAsync

        public static async Task WriteByteAsync(this ISequentialOutputByteStream destinationStream, Byte value, CancellationToken cancellationToken = default)
        {
            var length = await destinationStream.WriteAsync(new[] { value }, cancellationToken).ConfigureAwait(false);
            Validation.Assert(length > 0);
        }

        #endregion

        #region WriteBytesAsync

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);

            return destinationStream.WriteBytesAsyncCore(buffer, cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(offset), cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            return destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(offset), cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            return destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);

            return destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            return destinationStream.WriteBytesAsyncCore(buffer, cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);

            return destinationStream.WriteBytesAsyncCore(buffer, cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(offset), cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            return destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(offset), cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            return destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);

            return destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return destinationStream.WriteBytesAsyncCore(buffer, cancellationToken);
        }

        #endregion

        #region WriteByteSequenceAsync

        public static async Task WriteByteSequenceAsync(this Stream destinationStream, IEnumerable<Byte> sequence, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(sequence);

            var buffer = ArrayPool<Byte>.Shared.Rent(_WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE);
            try
            {
                using var enumerator = sequence.GetEnumerator();
                var isEndOfSequence = false;
                while (!isEndOfSequence)
                {
                    var index = 0;
                    while (index < _WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE)
                    {
                        if (!enumerator.MoveNext())
                        {
                            isEndOfSequence = true;
                            break;
                        }

                        buffer[index++] = enumerator.Current;
                    }

                    if (index > 0)
                        await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(0, index), cancellationToken).ConfigureAwait(false);
                }
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task WriteByteSequenceAsync(this Stream destinationStream, IAsyncEnumerable<Byte> sequence, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(sequence);

            var buffer = ArrayPool<Byte>.Shared.Rent(_WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE);
            try
            {
                var enumerator = sequence.GetAsyncEnumerator(cancellationToken);
                await using (enumerator.ConfigureAwait(false))
                {
                    var isEndOfSequence = false;
                    while (!isEndOfSequence)
                    {
                        var index = 0;
                        while (index < _WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE)
                        {
                            if (!await enumerator.MoveNextAsync().ConfigureAwait(false))
                            {
                                isEndOfSequence = true;
                                break;
                            }

                            buffer[index++] = enumerator.Current;
                        }

                        if (index > 0)
                            await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(0, index), cancellationToken).ConfigureAwait(false);
                    }
                }
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task WriteByteSequenceAsync(this ISequentialOutputByteStream destinationStream, IEnumerable<Byte> sequence, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(sequence);

            var buffer = ArrayPool<Byte>.Shared.Rent(_WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE);
            try
            {
                using var enumerator = sequence.GetEnumerator();
                var isEndOfSequence = false;
                while (!isEndOfSequence)
                {
                    var index = 0;
                    while (index < _WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE)
                    {
                        if (!enumerator.MoveNext())
                        {
                            isEndOfSequence = true;
                            break;
                        }

                        buffer[index++] = enumerator.Current;
                    }

                    if (index > 0)
                        await destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(0, index), cancellationToken).ConfigureAwait(false);
                }
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static async Task WriteByteSequenceAsync(this ISequentialOutputByteStream destinationStream, IAsyncEnumerable<Byte> sequence, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(sequence);

            var buffer = ArrayPool<Byte>.Shared.Rent(_WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE);
            try
            {
                var enumerator = sequence.GetAsyncEnumerator(cancellationToken);
                await using (enumerator.ConfigureAwait(false))
                {
                    var isEndOfSequence = false;
                    while (!isEndOfSequence)
                    {
                        var index = 0;
                        while (index < _WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE)
                        {
                            if (!await enumerator.MoveNextAsync().ConfigureAwait(false))
                            {
                                isEndOfSequence = true;
                                break;
                            }

                            buffer[index++] = enumerator.Current;
                        }

                        if (index > 0)
                            await destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(0, index), cancellationToken).ConfigureAwait(false);
                    }
                }
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteBytesAsyncCore

        private static Task WriteBytesAsyncCore(this Stream destinationStream, ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken)
            => destinationStream.WriteAsync(buffer, cancellationToken).AsTask();

        private static async Task WriteBytesAsyncCore(this ISequentialOutputByteStream destinationStream, ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken)
        {
            while (!buffer.IsEmpty)
            {
                var length = await destinationStream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
                if (length <= 0)
                    throw new IOException("Can not write any more");
                buffer = buffer[length..];
            }
        }

        #endregion
    }
}
