using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;

namespace Palmtree.IO
{
    public static partial class StreamExtensions
    {
        #region CalculateCrc24

        public static (UInt32 Crc, UInt64 Length) CalculateCrc24(this Stream sourceStream, Boolean leaveOpen = false)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.CalculateCrc24Core(MAX_BUFFER_SIZE, null);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc24(this Stream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.CalculateCrc24Core(MAX_BUFFER_SIZE, progress);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc24(this Stream sourceStream, Int32 bufferSize, Boolean leaveOpen = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.CalculateCrc24Core(bufferSize, null);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc24(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.CalculateCrc24Core(bufferSize, progress);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc24(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return sourceStream.CalculateCrc24Core(MAX_BUFFER_SIZE, null);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc24(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return sourceStream.CalculateCrc24Core(MAX_BUFFER_SIZE, progress);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc24(this ISequentialInputByteStream sourceStream, Int32 bufferSize, Boolean leaveOpen = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return sourceStream.CalculateCrc24Core(bufferSize, null);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc24(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return sourceStream.CalculateCrc24Core(bufferSize, progress);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        #endregion

        #region CalculateCrc32

        public static (UInt32 Crc, UInt64 Length) CalculateCrc32(this Stream sourceStream, Boolean leaveOpen = false)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.CalculateCrc32Core(MAX_BUFFER_SIZE, null);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc32(this Stream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.CalculateCrc32Core(MAX_BUFFER_SIZE, progress);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc32(this Stream sourceStream, Int32 bufferSize, Boolean leaveOpen = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.CalculateCrc32Core(bufferSize, null);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc32(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.CalculateCrc32Core(bufferSize, progress);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc32(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return sourceStream.CalculateCrc32Core(MAX_BUFFER_SIZE, null);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc32(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return sourceStream.CalculateCrc32Core(MAX_BUFFER_SIZE, progress);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc32(this ISequentialInputByteStream sourceStream, Int32 bufferSize, Boolean leaveOpen = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return sourceStream.CalculateCrc32Core(bufferSize, null);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc32(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return sourceStream.CalculateCrc32Core(bufferSize, progress);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        #endregion

        #region CalculateCrc24Core

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static (UInt32 Crc, UInt64 Length) CalculateCrc24Core(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress)
            => CalculateCrcCore(sourceStream, bufferSize, progress, Crc24.CreateCalculationState());

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static (UInt32 Crc, UInt64 Length) CalculateCrc24Core(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress)
            => CalculateCrcCore(sourceStream, bufferSize, progress, Crc24.CreateCalculationState());

        #endregion

        #region CalculateCrc32Core

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static (UInt32 Crc, UInt64 Length) CalculateCrc32Core(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress)
            => CalculateCrcCore(sourceStream, bufferSize, progress, Crc32.CreateCalculationState());

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static (UInt32 Crc, UInt64 Length) CalculateCrc32Core(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress)
            => CalculateCrcCore(sourceStream, bufferSize, progress, Crc32.CreateCalculationState());

        #endregion

        #region CalculateCrcCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static (UInt32 Crc, UInt64 Length) CalculateCrcCore(Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, ICrcCalculationState<UInt32> session)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = ArrayPool<Byte>.Shared.Rent(bufferSize);
            try
            {
                while (true)
                {
                    var length = sourceStream.Read(buffer.AsSpan(0, bufferSize));
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

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static (UInt32 Crc, UInt64 Length) CalculateCrcCore(ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, ICrcCalculationState<UInt32> session)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = ArrayPool<Byte>.Shared.Rent(bufferSize);
            try
            {
                while (true)
                {
                    var length = sourceStream.Read(buffer.AsSpan(0, bufferSize));
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
