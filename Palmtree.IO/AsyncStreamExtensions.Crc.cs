using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO
{
    public static partial class AsyncStreamExtensions
    {
        #region CalculateCrc24Async

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc24AsyncCore(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this Stream sourceStream, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc24AsyncCore(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this Stream sourceStream, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc24AsyncCore(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this Stream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc24AsyncCore(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this Stream sourceStream, Int32 bufferSize, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc24AsyncCore(bufferSize, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this Stream sourceStream, Int32 bufferSize, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc24AsyncCore(bufferSize, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc24AsyncCore(bufferSize, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc24AsyncCore(bufferSize, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc24AsyncCore(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this ISequentialInputByteStream sourceStream, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc24AsyncCore(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc24AsyncCore(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc24AsyncCore(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this ISequentialInputByteStream sourceStream, Int32 bufferSize, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc24AsyncCore(bufferSize, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this ISequentialInputByteStream sourceStream, Int32 bufferSize, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc24AsyncCore(bufferSize, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc24AsyncCore(bufferSize, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc24AsyncCore(bufferSize, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        #endregion

        #region CalculateCrc32Async

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc32AsyncCore(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this Stream sourceStream, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc32AsyncCore(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this Stream sourceStream, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc32AsyncCore(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this Stream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc32AsyncCore(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this Stream sourceStream, Int32 bufferSize, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc32AsyncCore(bufferSize, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this Stream sourceStream, Int32 bufferSize, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc32AsyncCore(bufferSize, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc32AsyncCore(bufferSize, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.CalculateCrc32AsyncCore(bufferSize, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc32AsyncCore(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this ISequentialInputByteStream sourceStream, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc32AsyncCore(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc32AsyncCore(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc32AsyncCore(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this ISequentialInputByteStream sourceStream, Int32 bufferSize, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc32AsyncCore(bufferSize, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this ISequentialInputByteStream sourceStream, Int32 bufferSize, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc32AsyncCore(bufferSize, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc32AsyncCore(bufferSize, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (sourceStream is not null)
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return await sourceStream.CalculateCrc32AsyncCore(bufferSize, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        #endregion

        #region CalculateCrc24AsyncCore

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24AsyncCore(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken)
            => CalculateCrcAsyncCore(sourceStream, bufferSize, progress, Crc24.CreateCalculationState(), cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24AsyncCore(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken)
            => CalculateCrcAsyncCore(sourceStream, bufferSize, progress, Crc24.CreateCalculationState(), cancellationToken);

        #endregion

        #region CalculateCrc32AsyncCore

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32AsyncCore(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken)
            => CalculateCrcAsyncCore(sourceStream, bufferSize, progress, Crc32.CreateCalculationState(), cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32AsyncCore(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken)
            => CalculateCrcAsyncCore(sourceStream, bufferSize, progress, Crc32.CreateCalculationState(), cancellationToken);

        #endregion

        #region CalculateCrcAsyncCore

        private static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrcAsyncCore(Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, ICrcCalculationState<UInt32> session, CancellationToken cancellationToken)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = ArrayPool<Byte>.Shared.Rent(bufferSize);
            try
            {
                while (true)
                {
                    var length = await sourceStream.ReadAsync(buffer.AsMemory(0, bufferSize), cancellationToken).ConfigureAwait(false);
                    if (length <= 0)
                        break;
                    session.Put(buffer.AsReadOnlySpan(0, length));
                    processedCounter.AddValue(checked((UInt64)length));
                }

                return session.GetResultValue();
            }
            finally
            {
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrcAsyncCore(ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, ICrcCalculationState<UInt32> session, CancellationToken cancellationToken)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = ArrayPool<Byte>.Shared.Rent(bufferSize);
            try
            {
                while (true)
                {
                    var length = await sourceStream.ReadAsync(buffer.AsMemory(0, bufferSize), cancellationToken).ConfigureAwait(false);
                    if (length <= 0)
                        break;
                    session.Put(buffer.AsReadOnlySpan(0, length));
                    processedCounter.AddValue(checked((UInt64)length));
                }

                return session.GetResultValue();
            }
            finally
            {
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion
    }
}
