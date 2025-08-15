using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Palmtree;

namespace Palmtree.IO
{
    public static partial class StreamExtensions
    {
        private const Int32 _SIZE_OF_INT128 = 16;
        private const Int32 _SIZE_OF_UINT128 = 16;
        private const Int32 _SIZE_OF_HALF = 2;
        private const Int32 _COPY_TO_DEFAULT_BYTE_BUFFER_SIZE = 81920;
        private const Int32 _COPY_TO_DEFAULT_CHAR_BUFFER_SIZE = 1024;

        static StreamExtensions()
        {
#if DEBUG
            unsafe
            {
                Validation.Assert(_SIZE_OF_INT128 == sizeof(Int128));
                Validation.Assert(_SIZE_OF_UINT128 == sizeof(UInt128));
                Validation.Assert(_SIZE_OF_HALF == sizeof(Half));
            }
#endif
        }

        #region GetByteSequence

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetByteSequenceCore(null, null, leaveOpen);
        }

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetByteSequenceCore(null, progress, leaveOpen);
        }

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, UInt64 offset, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanSeek)
                throw new NotSupportedException();
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetByteSequenceCore(offset, checked((UInt64)sourceStream.Length - offset), null, leaveOpen);
        }

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanSeek)
                throw new NotSupportedException();
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetByteSequenceCore(offset, checked((UInt64)sourceStream.Length - offset), progress, leaveOpen);
        }

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanSeek)
                throw new NotSupportedException();
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetByteSequenceCore(offset, count, null, leaveOpen);
        }

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanSeek)
                throw new NotSupportedException();
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetByteSequenceCore(offset, count, progress, leaveOpen);
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return sourceStream.GetByteSequenceCore(null, null, leaveOpen);
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return sourceStream.GetByteSequenceCore(null, progress, leaveOpen);
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (sourceStream is not IRandomInputByteStream<UInt64> ramdomAccessStream)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, ramdomAccessStream.Length);

            return ramdomAccessStream.GetByteSequenceCore(offset, checked(ramdomAccessStream.Length - offset), null, leaveOpen);
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (sourceStream is not IRandomInputByteStream<UInt64> ramdomAccessStream)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, ramdomAccessStream.Length);

            return ramdomAccessStream.GetByteSequenceCore(offset, checked(ramdomAccessStream.Length - offset), progress, leaveOpen);
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (sourceStream is not IRandomInputByteStream<UInt64> ramdomAccessStream)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, ramdomAccessStream.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, ramdomAccessStream.Length - offset);

            return ramdomAccessStream.GetByteSequenceCore(offset, count, null, leaveOpen);
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (sourceStream is not IRandomInputByteStream<UInt64> ramdomAccessStream)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, ramdomAccessStream.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, ramdomAccessStream.Length - offset);

            return ramdomAccessStream.GetByteSequenceCore(offset, count, progress, leaveOpen);
        }

        #endregion

        #region GetReverseByteSequence

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanSeek)
                throw new NotSupportedException();
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetReverseByteSequenceCore(0, checked((UInt64)sourceStream.Length), null, leaveOpen);
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanSeek)
                throw new NotSupportedException();
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetReverseByteSequenceCore(0, checked((UInt64)sourceStream.Length), progress, leaveOpen);
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, UInt64 offset, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanSeek)
                throw new NotSupportedException();
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetReverseByteSequenceCore(offset, checked((UInt64)sourceStream.Length - offset), null, leaveOpen);
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanSeek)
                throw new NotSupportedException();
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetReverseByteSequenceCore(offset, checked((UInt64)sourceStream.Length - offset), progress, leaveOpen);
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanSeek)
                throw new NotSupportedException();
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetReverseByteSequenceCore(offset, count, null, leaveOpen);
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanSeek)
                throw new NotSupportedException();
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.GetReverseByteSequenceCore(offset, count, progress, leaveOpen);
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                throw new ArgumentException($"The stream specified by parameter {nameof(sourceStream)} must be a random access stream.", nameof(sourceStream));

            return baseRamdomAccessStream.GetReverseByteSequenceCore(0UL, baseRamdomAccessStream.Length, null, leaveOpen);
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                throw new NotSupportedException();

            return baseRamdomAccessStream.GetReverseByteSequenceCore(0UL, baseRamdomAccessStream.Length, progress, leaveOpen);
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, baseRamdomAccessStream.Length);

            return baseRamdomAccessStream.GetReverseByteSequenceCore(offset, baseRamdomAccessStream.Length - offset, null, leaveOpen);
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, baseRamdomAccessStream.Length);

            return baseRamdomAccessStream.GetReverseByteSequenceCore(offset, baseRamdomAccessStream.Length - offset, progress, leaveOpen);
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, baseRamdomAccessStream.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, baseRamdomAccessStream.Length - offset);

            return baseRamdomAccessStream.GetReverseByteSequenceCore(offset, count, null, leaveOpen);
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, baseRamdomAccessStream.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, baseRamdomAccessStream.Length - offset);

            return baseRamdomAccessStream.GetReverseByteSequenceCore(offset, count, progress, leaveOpen);
        }

        #endregion

        #region StreamBytesEqual

        public static Boolean StreamBytesEqual(this Stream stream1, Stream stream2, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(stream1);
            if (!stream1.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(stream2);
            if (!stream2.CanRead)
                throw new NotSupportedException();

            try
            {
                return stream1.StreamBytesEqual(stream2, null, leaveOpen);
            }
            finally
            {
                if (!leaveOpen)
                {
                    stream1?.Dispose();
                    stream2?.Dispose();
                }
            }
        }

        public static Boolean StreamBytesEqual(this Stream stream1, Stream stream2, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(stream1);
            if (!stream1.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(stream2);
            if (!stream2.CanRead)
                throw new NotSupportedException();

            try
            {
                return stream1.StreamBytesEqual(stream2, progress, leaveOpen);
            }
            finally
            {
                if (!leaveOpen)
                {
                    stream1?.Dispose();
                    stream2?.Dispose();
                }
            }
        }

        public static Boolean StreamBytesEqual(this ISequentialInputByteStream stream1, ISequentialInputByteStream stream2, Boolean leaveOpen = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(stream1);
                ArgumentNullException.ThrowIfNull(stream2);

                return
                    stream1 is IDirectDotNetStreamWrapper wrapper1 && stream2 is IDirectDotNetStreamWrapper wrapper2
                    ? StreamBytesEqualCore(wrapper1.RawStream, wrapper2.RawStream, null)
                    : StreamBytesEqualCore(stream1, stream2, null);
            }
            finally
            {
                if (!leaveOpen)
                {
                    stream1?.Dispose();
                    stream2?.Dispose();
                }
            }
        }

        public static Boolean StreamBytesEqual(this ISequentialInputByteStream stream1, ISequentialInputByteStream stream2, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(stream1);
                ArgumentNullException.ThrowIfNull(stream2);

                return
                    stream1 is IDirectDotNetStreamWrapper wrapper1 && stream2 is IDirectDotNetStreamWrapper wrapper2
                    ? StreamBytesEqualCore(wrapper1.RawStream, wrapper2.RawStream, progress)
                    : StreamBytesEqualCore(stream1, stream2, progress);
            }
            finally
            {
                if (!leaveOpen)
                {
                    stream1?.Dispose();
                    stream2?.Dispose();
                }
            }
        }

        #endregion

        #region CopyTo

        public static void CopyTo(this Stream sourceStream, Stream destinationStream, IProgress<UInt64>? progress)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            sourceStream.CopyToCore(destinationStream, _COPY_TO_DEFAULT_BYTE_BUFFER_SIZE, progress);
        }

        public static void CopyTo(this Stream sourceStream, Stream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bufferSize);

            sourceStream.CopyToCore(destinationStream, bufferSize, progress);
        }

        public static void CopyTo(this ISequentialInputByteStream sourceStream, ISequentialOutputByteStream destinationStream, IProgress<UInt64>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(destinationStream);

            sourceStream.CopyToCore(destinationStream, _COPY_TO_DEFAULT_BYTE_BUFFER_SIZE, progress);
        }

        public static void CopyTo(this ISequentialInputByteStream sourceStream, ISequentialOutputByteStream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bufferSize);

            sourceStream.CopyToCore(destinationStream, bufferSize, progress);
        }

        public static void CopyTo(this TextReader source, TextWriter destination, IProgress<UInt64>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);

            if (source is StreamReader streamReader && destination is StreamWriter streamWriter && streamReader.CurrentEncoding.EqualsStrictly(streamWriter.Encoding))
                streamReader.BaseStream.CopyToCore(streamWriter.BaseStream, _COPY_TO_DEFAULT_BYTE_BUFFER_SIZE, progress);
            else
                source.CopyToCore(destination, _COPY_TO_DEFAULT_CHAR_BUFFER_SIZE, progress);
        }

        public static void CopyTo(this TextReader source, TextWriter destination, Int32 bufferSize, IProgress<UInt64>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bufferSize);

            if (source is StreamReader streamReader && destination is StreamWriter streamWriter && streamReader.CurrentEncoding.EqualsStrictly(streamWriter.Encoding))
                streamReader.BaseStream.CopyToCore(streamWriter.BaseStream, bufferSize, progress);
            else
                source.CopyToCore(destination, bufferSize, progress);
        }

        #endregion

        #region GetByteSequenceCore

        private static IEnumerable<Byte> GetByteSequenceCore(this Stream sourceStream, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen)
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

            var processedCounter = new ProgressCounterUInt64(progress);
            var buffer = ArrayPool<Byte>.Shared.Rent(BUFFER_SIZE);
            try
            {
                processedCounter.Report();
                while (true)
                {
                    var readCount = BUFFER_SIZE;
                    if (count is not null)
                        readCount = (Int32)((UInt64)readCount).Minimum(count.Value - processedCounter.Value);
                    if (readCount <= 0)
                        break;
                    var length = sourceStream.Read(buffer.AsSpan(0, readCount));
                    if (length <= 0)
                        break;
                    for (var index = 0; index < length; ++index)
                    {
                        yield return buffer[index];
                        processedCounter.Increment();
                    }
                }
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream.Dispose();
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static IEnumerable<Byte> GetByteSequenceCore(this Stream sourceStream, UInt64 offset, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen)
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

            var processedCounter = new ProgressCounterUInt64(progress);
            var buffer = ArrayPool<Byte>.Shared.Rent(BUFFER_SIZE);
            try
            {
                if (!sourceStream.CanSeek)
                    throw new ArgumentException($"If stream {nameof(sourceStream)} is sequential, parameter {nameof(offset)} must not be specified.", nameof(offset));

                _ = sourceStream.Seek(checked((Int64)offset), SeekOrigin.Begin);
                processedCounter.Report();
                while (true)
                {
                    var readCount = BUFFER_SIZE;
                    if (count is not null)
                        readCount = (Int32)((UInt64)readCount).Minimum(count.Value - processedCounter.Value);
                    if (readCount <= 0)
                        break;
                    var length = sourceStream.Read(buffer.AsSpan(0, readCount));
                    if (length <= 0)
                        break;
                    for (var index = 0; index < length; ++index)
                    {
                        yield return buffer[index];
                        processedCounter.Increment();
                    }
                }
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream.Dispose();
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static IEnumerable<Byte> GetByteSequenceCore(this ISequentialInputByteStream sourceStream, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen)
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

            var processedCounter = new ProgressCounterUInt64(progress);
            var buffer = ArrayPool<Byte>.Shared.Rent(BUFFER_SIZE);
            try
            {
                processedCounter.Report();
                while (true)
                {
                    var readCount = BUFFER_SIZE;
                    if (count is not null)
                        readCount = (Int32)((UInt64)readCount).Minimum(count.Value - processedCounter.Value);
                    if (readCount <= 0)
                        break;
                    var length = sourceStream.Read(buffer.AsSpan(0, readCount));
                    if (length <= 0)
                        break;
                    for (var index = 0; index < length; ++index)
                    {
                        yield return buffer[index];
                        processedCounter.Increment();
                    }
                }
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream.Dispose();
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static IEnumerable<Byte> GetByteSequenceCore<POSITION_T>(this ISequentialInputByteStream sourceStream, POSITION_T offset, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen)
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

            var processedCounter = new ProgressCounterUInt64(progress);
            var buffer = ArrayPool<Byte>.Shared.Rent(BUFFER_SIZE);
            try
            {
                if (sourceStream is not IRandomInputByteStream<POSITION_T> randomAccessStream)
                    throw new ArgumentException($"If stream {nameof(sourceStream)} is sequential, parameter {nameof(offset)} must not be specified.", nameof(offset));

                randomAccessStream.Seek(offset);
                processedCounter.Report();
                while (true)
                {
                    var readCount = BUFFER_SIZE;
                    if (count is not null)
                        readCount = (Int32)((UInt64)readCount).Minimum(count.Value - processedCounter.Value);
                    if (readCount <= 0)
                        break;
                    var length = randomAccessStream.Read(buffer.AsSpan(0, readCount));
                    if (length <= 0)
                        break;
                    for (var index = 0; index < length; ++index)
                    {
                        yield return buffer[index];
                        processedCounter.Increment();
                    }
                }
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream.Dispose();
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region GetReverseByteSequenceCore

        private static IEnumerable<Byte> GetReverseByteSequenceCore(this Stream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

            var progressCounter = new ProgressCounterUInt64(progress);
            var buffer = ArrayPool<Byte>.Shared.Rent(BUFFER_SIZE);
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                progressCounter.Report();
                var pos = checked(offset + count);
                while (pos.CompareTo(offset + BUFFER_SIZE) > 0)
                {
                    pos -= BUFFER_SIZE;
                    _ = sourceStream.Seek(checked((Int64)pos), SeekOrigin.Begin);
                    var length = sourceStream.ReadBytesCore(buffer.AsSpan(0, BUFFER_SIZE));
                    Validation.Assert(length == BUFFER_SIZE);
                    for (var index = BUFFER_SIZE - 1; index >= 0; --index)
                    {
                        yield return buffer[index];
                        progressCounter.Increment();
                    }
                }

                if (pos.CompareTo(offset) > 0)
                {
                    var remain = checked((Int32)(pos - offset));
                    var length = sourceStream.ReadBytesCore(buffer.AsSpan(0, remain));
                    Validation.Assert(length == remain);
                    for (var index = remain - 1; index >= 0; --index)
                    {
                        yield return buffer[index];
                        progressCounter.Increment();
                    }
                }
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
                if (!leaveOpen)
                    sourceStream.Dispose();
            }
        }

        private static IEnumerable<Byte> GetReverseByteSequenceCore<POSITION_T>(this IRandomInputByteStream<POSITION_T> sourceStream, POSITION_T offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

            var progressCounter = new ProgressCounterUInt64(progress);
            var buffer = ArrayPool<Byte>.Shared.Rent(BUFFER_SIZE);
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                progressCounter.Report();
                var pos = checked(offset + count);
                while (pos.CompareTo(offset + BUFFER_SIZE) > 0)
                {
                    pos -= BUFFER_SIZE;
                    sourceStream.Seek(pos);
                    var length = sourceStream.ReadBytesCore(buffer.AsSpan(0, BUFFER_SIZE));
                    Validation.Assert(length == BUFFER_SIZE);
                    for (var index = BUFFER_SIZE - 1; index >= 0; --index)
                    {
                        yield return buffer[index];
                        progressCounter.Increment();
                    }
                }

                if (pos.CompareTo(offset) > 0)
                {
                    var remain = checked((Int32)(pos - offset));
                    var length = sourceStream.ReadBytesCore(buffer.AsSpan(0, remain));
                    Validation.Assert(length == remain);
                    for (var index = remain - 1; index >= 0; --index)
                    {
                        yield return buffer[index];
                        progressCounter.Increment();
                    }
                }
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
                if (!leaveOpen)
                    sourceStream.Dispose();
            }
        }

        #endregion

        #region StreamBytesEqualCore

        private static Boolean StreamBytesEqualCore(this Stream stream1, Stream stream2, IProgress<UInt64>? progress)
        {
            const Int32 bufferSize = 81920;

            Validation.Assert(bufferSize % sizeof(UInt64) == 0);
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer1 = (Byte[]?)null;
            var buffer2 = (Byte[]?)null;
            try
            {
                buffer1 = ArrayPool<Byte>.Shared.Rent(bufferSize);
                buffer2 = ArrayPool<Byte>.Shared.Rent(bufferSize);
                while (true)
                {
                    // まず両方のストリームから bufferSize バイトだけ読み込みを試みる
                    var bufferCount1 = stream1.ReadBytesCore(buffer1.AsSpan(0, bufferSize));
                    var bufferCount2 = stream2.ReadBytesCore(buffer2.AsSpan(0, bufferSize));
                    processedCounter.AddValue((UInt32)bufferCount1);

                    if (bufferCount1 != bufferCount2)
                    {
                        // 実際に読み込めたサイズが異なっている場合はどちらかだけがEOFに達したということなので、ストリームの内容が異なると判断しfalseを返す。
                        return false;
                    }

                    // この時点で bufferCount1 == bufferCount2 (どちらのストリームも読み込めたサイズは同じ)

                    if (!buffer1.AsSpan(0, bufferCount1).SequenceEqual(buffer2.AsSpan(0, bufferCount2)))
                    {
                        // バッファの内容が一致しなかった場合は false を返す。
                        return false;
                    }

                    if (bufferCount1 < bufferSize)
                    {
                        // どちらのストリームも同時にEOFに達したがそれまでに読み込めたデータはすべて一致していた場合
                        // 全てのデータが一致したと判断して true を返す。
                        return true;
                    }
                }
            }
            finally
            {
                processedCounter.Report();
                if (buffer1 is not null)
                    ArrayPool<Byte>.Shared.Return(buffer1);
                if (buffer2 is not null)
                    ArrayPool<Byte>.Shared.Return(buffer2);
            }
        }

        private static Boolean StreamBytesEqualCore(this ISequentialInputByteStream stream1, ISequentialInputByteStream stream2, IProgress<UInt64>? progress)
        {
            const Int32 bufferSize = 81920;

            Validation.Assert(bufferSize % sizeof(UInt64) == 0);
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer1 = (Byte[]?)null;
            var buffer2 = (Byte[]?)null;
            try
            {
                buffer1 = ArrayPool<Byte>.Shared.Rent(bufferSize);
                buffer2 = ArrayPool<Byte>.Shared.Rent(bufferSize);
                while (true)
                {
                    // まず両方のストリームから bufferSize バイトだけ読み込みを試みる
                    var bufferCount1 = stream1.ReadBytesCore(buffer1.AsSpan(0, bufferSize));
                    var bufferCount2 = stream2.ReadBytesCore(buffer2.AsSpan(0, bufferSize));
                    processedCounter.AddValue((UInt32)bufferCount1);

                    if (bufferCount1 != bufferCount2)
                    {
                        // 実際に読み込めたサイズが異なっている場合はどちらかだけがEOFに達したということなので、ストリームの内容が異なると判断しfalseを返す。
                        return false;
                    }

                    // この時点で bufferCount1 == bufferCount2 (どちらのストリームも読み込めたサイズは同じ)

                    if (!buffer1.AsSpan(0, bufferCount1).SequenceEqual(buffer2.AsSpan(0, bufferCount2)))
                    {
                        // バッファの内容が一致しなかった場合は false を返す。
                        return false;
                    }

                    if (bufferCount1 < bufferSize)
                    {
                        // どちらのストリームも同時にEOFに達したがそれまでに読み込めたデータはすべて一致していた場合
                        // 全てのデータが一致したと判断して true を返す。
                        return true;
                    }
                }
            }
            finally
            {
                processedCounter.Report();
                if (buffer1 is not null)
                    ArrayPool<Byte>.Shared.Return(buffer1);
                if (buffer2 is not null)
                    ArrayPool<Byte>.Shared.Return(buffer2);
            }
        }

        #endregion

        #region CopyToCore

        private static void CopyToCore(this Stream sourceStream, Stream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = ArrayPool<Byte>.Shared.Rent(bufferSize);
            try
            {
                while (true)
                {
                    var length = sourceStream.ReadBytesCore(buffer.AsSpan(0, bufferSize));
                    if (length <= 0)
                        break;
                    destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(0, length));
                    processedCounter.AddValue(checked((UInt64)length));
                }

                destinationStream.Flush();
            }
            finally
            {
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static void CopyToCore(this ISequentialInputByteStream sourceStream, ISequentialOutputByteStream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = ArrayPool<Byte>.Shared.Rent(bufferSize);
            try
            {
                while (true)
                {
                    var length = sourceStream.ReadBytesCore(buffer.AsSpan(0, bufferSize));
                    if (length <= 0)
                        break;
                    destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(0, length));
                    processedCounter.AddValue(checked((UInt64)length));
                }

                destinationStream.Flush();
            }
            finally
            {
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static void CopyToCore(this TextReader sourceStream, TextWriter destinationStream, Int32 bufferSize, IProgress<UInt64>? progress)
        {
            var processedCounter = progress is not null ? new ProgressCounterUInt64(progress) : null;
            var buffer = ArrayPool<Char>.Shared.Rent(bufferSize);
            try
            {
                processedCounter?.Report();
                while (true)
                {
                    var length = sourceStream.Read(buffer.AsSpan(0, bufferSize));
                    if (length <= 0)
                        break;
                    destinationStream.Write(buffer.AsReadOnlySpan(0, length));
                    processedCounter?.AddValue(checked((UInt32)length));
                }

                destinationStream.Flush();
            }
            finally
            {
                processedCounter?.Report();
                ArrayPool<Char>.Shared.Return(buffer);
            }
        }

        #endregion
    }
}
