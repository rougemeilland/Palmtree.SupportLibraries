using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO
{
    public static partial class AsyncStreamExtensions
    {
        private const Int32 _SIZE_OF_INT128 = 16;
        private const Int32 _SIZE_OF_UINT128 = 16;
        private const Int32 _SIZE_OF_HALF = 2;
        private const Int32 _COPY_TO_DEFAULT_BYTE_BUFFER_SIZE = 81920;
        private const Int32 _COPY_TO_DEFAULT_CHAR_BUFFER_SIZE = 1024;

        static AsyncStreamExtensions()
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

        #region GetAsyncByteSequence

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this Stream sourceStream, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.GetByteSequenceAsyncCore(null, null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this Stream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.GetByteSequenceAsyncCore(null, progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this Stream sourceStream, UInt64 offset, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, checked((UInt64)sourceStream.Length));

                return sourceStream.GetByteSequenceAsyncCore(offset, checked((UInt64)sourceStream.Length - offset), null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this Stream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, checked((UInt64)sourceStream.Length));

                return sourceStream.GetByteSequenceAsyncCore(offset, checked((UInt64)sourceStream.Length - offset), progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsyncAsync(this Stream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, checked((UInt64)sourceStream.Length));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(count, checked((UInt64)sourceStream.Length) - offset);

                return sourceStream.GetByteSequenceAsyncCore(offset, count, null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this Stream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, checked((UInt64)sourceStream.Length));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(count, checked((UInt64)sourceStream.Length) - offset);

                return sourceStream.GetByteSequenceAsyncCore(offset, count, progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return sourceStream.GetByteSequenceAsyncCore(null, null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);

                return sourceStream.GetByteSequenceAsyncCore(null, progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this ISequentialInputByteStream sourceStream, UInt64 offset, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, randomAccessStream.Length);

                return randomAccessStream.GetByteSequenceAsyncCore(offset, checked(randomAccessStream.Length - offset), null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this ISequentialInputByteStream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, randomAccessStream.Length);

                return randomAccessStream.GetByteSequenceAsyncCore(offset, checked(randomAccessStream.Length - offset), progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, randomAccessStream.Length);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(count, randomAccessStream.Length - offset);

                return randomAccessStream.GetByteSequenceAsyncCore(offset, count, null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, randomAccessStream.Length);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(count, randomAccessStream.Length - offset);

                return randomAccessStream.GetByteSequenceAsyncCore<UInt64>(offset, count, progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                sourceStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region GetAsyncReverseByteSequence

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this Stream sourceStream, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return GetReverseByteSequenceAsyncCore(sourceStream, 0, checked((UInt64)sourceStream.Length), null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this Stream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return GetReverseByteSequenceAsyncCore(sourceStream, 0, checked((UInt64)sourceStream.Length), progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this Stream sourceStream, UInt64 offset, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, checked((UInt64)sourceStream.Length));

                return GetReverseByteSequenceAsyncCore(sourceStream, offset, checked((UInt64)sourceStream.Length - offset), null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this Stream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, checked((UInt64)sourceStream.Length));

                return GetReverseByteSequenceAsyncCore(sourceStream, offset, checked((UInt64)sourceStream.Length - offset), progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this Stream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, checked((UInt64)sourceStream.Length));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(count, checked((UInt64)sourceStream.Length) - offset);

                return GetReverseByteSequenceAsyncCore(sourceStream, offset, count, null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this Stream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, checked((UInt64)sourceStream.Length));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(count, checked((UInt64)sourceStream.Length) - offset);

                return GetReverseByteSequenceAsyncCore(sourceStream, offset, count, progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();

                return GetReverseByteSequenceAsyncCore(randomAccessStream, randomAccessStream.StartOfThisStream, randomAccessStream.Length, null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();

                return GetReverseByteSequenceAsyncCore(randomAccessStream, randomAccessStream.StartOfThisStream, randomAccessStream.Length, progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this ISequentialInputByteStream sourceStream, UInt64 offset, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, randomAccessStream.Length);

                return GetReverseByteSequenceAsyncCore(randomAccessStream, offset, checked(randomAccessStream.Length - offset), null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this ISequentialInputByteStream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, randomAccessStream.Length);

                return GetReverseByteSequenceAsyncCore(randomAccessStream, offset, checked(randomAccessStream.Length - offset), progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, randomAccessStream.Length);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(count, randomAccessStream.Length - offset);

                return GetReverseByteSequenceAsyncCore(randomAccessStream, offset, count, null, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IAsyncEnumerable<Byte> GetAsyncReverseByteSequenceAsync(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(sourceStream);
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, randomAccessStream.Length);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(count, randomAccessStream.Length - offset);

                return GetReverseByteSequenceAsyncCore(randomAccessStream, offset, count, progress, leaveOpen, cancellationToken);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region StreamBytesEqualAsync

        public static async Task<Boolean> StreamBytesEqualAsync(this Stream stream1, Stream stream2, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(stream1);
                if (!stream1.CanRead)
                    throw new NotSupportedException();
                ArgumentNullException.ThrowIfNull(stream2);
                if (!stream2.CanRead)
                    throw new NotSupportedException();

                return await stream1.StreamBytesEqualAsyncCore(stream2, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (stream1 is not null)
                    await stream1.DisposeAsync().ConfigureAwait(false);
                if (stream2 is not null)
                    await stream2.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<Boolean> StreamBytesEqualAsync(this Stream stream1, Stream stream2, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(stream1);
                if (!stream1.CanRead)
                    throw new NotSupportedException();
                ArgumentNullException.ThrowIfNull(stream2);
                if (!stream2.CanRead)
                    throw new NotSupportedException();

                return await stream1.StreamBytesEqualAsyncCore(stream2, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (stream1 is not null)
                        await stream1.DisposeAsync().ConfigureAwait(false);
                    if (stream2 is not null)
                        await stream2.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<Boolean> StreamBytesEqualAsync(this Stream stream1, Stream stream2, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(stream1);
                if (!stream1.CanRead)
                    throw new NotSupportedException();
                ArgumentNullException.ThrowIfNull(stream2);
                if (!stream2.CanRead)
                    throw new NotSupportedException();

                return await stream1.StreamBytesEqualAsyncCore(stream2, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (stream1 is not null)
                    await stream1.DisposeAsync().ConfigureAwait(false);
                if (stream2 is not null)
                    await stream2.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<Boolean> StreamBytesEqualAsync(this Stream stream1, Stream stream2, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(stream1);
                if (!stream1.CanRead)
                    throw new NotSupportedException();
                ArgumentNullException.ThrowIfNull(stream2);
                if (!stream2.CanRead)
                    throw new NotSupportedException();

                return await stream1.StreamBytesEqualAsyncCore(stream2, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (stream1 is not null)
                        await stream1.DisposeAsync().ConfigureAwait(false);
                    if (stream2 is not null)
                        await stream2.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<Boolean> StreamBytesEqualAsync(this ISequentialInputByteStream stream1, ISequentialInputByteStream stream2, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(stream1);
                ArgumentNullException.ThrowIfNull(stream2);

                return await stream1.StreamBytesEqualAsyncCore(stream2, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (stream1 is not null)
                    await stream1.DisposeAsync().ConfigureAwait(false);
                if (stream2 is not null)
                    await stream2.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<Boolean> StreamBytesEqualAsync(this ISequentialInputByteStream stream1, ISequentialInputByteStream stream2, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(stream1);
                ArgumentNullException.ThrowIfNull(stream2);

                return await stream1.StreamBytesEqualAsyncCore(stream2, null, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (stream1 is not null)
                        await stream1.DisposeAsync().ConfigureAwait(false);
                    if (stream2 is not null)
                        await stream2.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task<Boolean> StreamBytesEqualAsync(this ISequentialInputByteStream stream1, ISequentialInputByteStream stream2, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(stream1);
                ArgumentNullException.ThrowIfNull(stream2);

                return await stream1.StreamBytesEqualAsyncCore(stream2, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (stream1 is not null)
                    await stream1.DisposeAsync().ConfigureAwait(false);
                if (stream2 is not null)
                    await stream2.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task<Boolean> StreamBytesEqualAsync(this ISequentialInputByteStream stream1, ISequentialInputByteStream stream2, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(stream1);
                ArgumentNullException.ThrowIfNull(stream2);

                return await stream1.StreamBytesEqualAsyncCore(stream2, progress, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (stream1 is not null)
                        await stream1.DisposeAsync().ConfigureAwait(false);
                    if (stream2 is not null)
                        await stream2.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        #endregion

        #region CopyToAsync

#if false
        public static Task CopyToAsync(this Stream source, Stream destination, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException(); // defined in System.IO.Stream.CopyAsync(Stream, CancellationToken)
        }
#endif

#if false
        public static Task CopyToAsync(this Stream source, Stream destination, Int32 bufferSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException(); // defined in System.IO.Stream.CopyAsync(Stream, Int32, CancellationToken)
        }
#endif

        public static async Task CopyToAsync(this Stream source, Stream destination, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source);
            if (!source.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(destination);
            if (!destination.CanWrite)
                throw new NotSupportedException();

            await source.CopyToAsyncCore(destination, _COPY_TO_DEFAULT_BYTE_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
        }

        public static async Task CopyToAsync(this Stream source, Stream destination, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source);
            if (!source.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(destination);
            if (!destination.CanWrite)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bufferSize);

            await source.CopyToAsyncCore(destination, bufferSize, progress, cancellationToken).ConfigureAwait(false);
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);

            return source.CopyToAsyncCore(destination, _COPY_TO_DEFAULT_BYTE_BUFFER_SIZE, null, cancellationToken);
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);

            return source.CopyToAsyncCore(destination, _COPY_TO_DEFAULT_BYTE_BUFFER_SIZE, progress, cancellationToken);
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, Int32 bufferSize, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bufferSize);

            return source.CopyToAsyncCore(destination, bufferSize, null, cancellationToken);
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bufferSize);

            return source.CopyToAsyncCore(destination, bufferSize, progress, cancellationToken);
        }

        public static Task CopyToAsync(this TextReader source, TextWriter destination, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);

            return
                source is StreamReader streamReader && destination is StreamWriter streamWriter && streamReader.CurrentEncoding.EqualsStrictly(streamWriter.Encoding)
                ? streamReader.BaseStream.CopyToAsyncCore(streamWriter.BaseStream, _COPY_TO_DEFAULT_BYTE_BUFFER_SIZE, null, cancellationToken)
                : source.CopyToAsyncCore(destination, _COPY_TO_DEFAULT_CHAR_BUFFER_SIZE, null, cancellationToken);
        }

        public static Task CopyToAsync(this TextReader source, TextWriter destination, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);

            return
                source is StreamReader streamReader && destination is StreamWriter streamWriter && streamReader.CurrentEncoding.EqualsStrictly(streamWriter.Encoding)
                ? streamReader.BaseStream.CopyToAsyncCore(streamWriter.BaseStream, _COPY_TO_DEFAULT_BYTE_BUFFER_SIZE, progress, cancellationToken)
                : source.CopyToAsyncCore(destination, _COPY_TO_DEFAULT_CHAR_BUFFER_SIZE, progress, cancellationToken);
        }

        public static Task CopyToAsync(this TextReader source, TextWriter destination, Int32 bufferSize, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bufferSize);

            return
                source is StreamReader streamReader && destination is StreamWriter streamWriter && streamReader.CurrentEncoding.EqualsStrictly(streamWriter.Encoding)
                ? streamReader.BaseStream.CopyToAsyncCore(streamWriter.BaseStream, bufferSize, null, cancellationToken)
                : source.CopyToAsyncCore(destination, bufferSize, null, cancellationToken);
        }

        public static Task CopyToAsync(this TextReader source, TextWriter destination, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bufferSize);

            return
                source is StreamReader streamReader && destination is StreamWriter streamWriter && streamReader.CurrentEncoding.EqualsStrictly(streamWriter.Encoding)
                ? streamReader.BaseStream.CopyToAsyncCore(streamWriter.BaseStream, bufferSize, progress, cancellationToken)
                : source.CopyToAsyncCore(destination, bufferSize, progress, cancellationToken);
        }

        #endregion

        #region GetByteSequenceAsyncCore

        private static async IAsyncEnumerable<Byte> GetByteSequenceAsyncCore(this Stream sourceStream, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
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
                    var length = await sourceStream.ReadAsync(buffer.AsMemory(0, readCount), cancellationToken).ConfigureAwait(false);
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
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static async IAsyncEnumerable<Byte> GetByteSequenceAsyncCore(this ISequentialInputByteStream sourceStream, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
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
                    var length = await sourceStream.ReadAsync(buffer.AsMemory(0, readCount), cancellationToken).ConfigureAwait(false);
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
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static async IAsyncEnumerable<Byte> GetByteSequenceAsyncCore(this Stream sourceStream, UInt64 offset, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const Int32 BUFFER_SIZE = 80 * 1024;

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
                    var length = await sourceStream.ReadAsync(buffer.AsMemory(0, readCount), cancellationToken).ConfigureAwait(false);
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
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static async IAsyncEnumerable<Byte> GetByteSequenceAsyncCore<POSITION_T>(this ISequentialInputByteStream sourceStream, POSITION_T offset, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const Int32 BUFFER_SIZE = 80 * 1024;

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
                    var length = await randomAccessStream.ReadAsync(buffer.AsMemory(0, readCount), cancellationToken).ConfigureAwait(false);
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
                    await sourceStream.DisposeAsync().ConfigureAwait(false);
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region GetReverseByteSequenceAsyncCore

        private static async IAsyncEnumerable<Byte> GetReverseByteSequenceAsyncCore(this Stream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const Int32 BUFFER_SIZE = 80 * 1024;

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
                    var length = await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, BUFFER_SIZE), cancellationToken).ConfigureAwait(false);
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
                    var length = sourceStream.ReadBytes(buffer.AsMemory(0, remain));
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
                if (!leaveOpen)
                    sourceStream.Dispose();
                progressCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static async IAsyncEnumerable<Byte> GetReverseByteSequenceAsyncCore<POSITION_T>(this IRandomInputByteStream<POSITION_T> sourceStream, POSITION_T offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            const Int32 BUFFER_SIZE = 80 * 1024;

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
                    var length = await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, BUFFER_SIZE), cancellationToken).ConfigureAwait(false);
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
                    var length = sourceStream.ReadBytes(buffer.AsMemory(0, remain));
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
                if (!leaveOpen)
                    sourceStream.Dispose();
                progressCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region StreamBytesEqualAsyncCore

        private static async Task<Boolean> StreamBytesEqualAsyncCore(this Stream stream1, Stream stream2, IProgress<UInt64>? progress, CancellationToken cancellationToken)
        {
            const Int32 BUFFER_SIZE = 81920;

            Validation.Assert(BUFFER_SIZE % sizeof(UInt64) == 0);
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer1 = ArrayPool<Byte>.Shared.Rent(BUFFER_SIZE);
            var buffer2 = ArrayPool<Byte>.Shared.Rent(BUFFER_SIZE);
            try
            {
                while (true)
                {
                    // まず両方のストリームから BUFFER_SIZE バイトだけ読み込みを試みる
                    var bufferCount1 = await stream1.ReadBytesAsyncCore(buffer1.AsMemory(0, BUFFER_SIZE), cancellationToken).ConfigureAwait(false);
                    var bufferCount2 = await stream2.ReadBytesAsyncCore(buffer2.AsMemory(0, BUFFER_SIZE), cancellationToken).ConfigureAwait(false);
                    processedCounter.AddValue(checked((UInt32)bufferCount1));

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

                    if (bufferCount1 < BUFFER_SIZE)
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
                ArrayPool<Byte>.Shared.Return(buffer1);
                ArrayPool<Byte>.Shared.Return(buffer2);
            }
        }

        private static async Task<Boolean> StreamBytesEqualAsyncCore(this ISequentialInputByteStream stream1, ISequentialInputByteStream stream2, IProgress<UInt64>? progress, CancellationToken cancellationToken)
        {
            const Int32 BUFFER_SIZE = 81920;

            Validation.Assert(BUFFER_SIZE % sizeof(UInt64) == 0);
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer1 = ArrayPool<Byte>.Shared.Rent(BUFFER_SIZE);
            var buffer2 = ArrayPool<Byte>.Shared.Rent(BUFFER_SIZE);
            try
            {
                while (true)
                {
                    // まず両方のストリームから BUFFER_SIZE バイトだけ読み込みを試みる
                    var bufferCount1 = await stream1.ReadBytesAsyncCore(buffer1.AsMemory(0, BUFFER_SIZE), cancellationToken).ConfigureAwait(false);
                    var bufferCount2 = await stream2.ReadBytesAsyncCore(buffer2.AsMemory(0, BUFFER_SIZE), cancellationToken).ConfigureAwait(false);
                    processedCounter.AddValue(checked((UInt32)bufferCount1));

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

                    if (bufferCount1 < BUFFER_SIZE)
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
                ArrayPool<Byte>.Shared.Return(buffer1);
                ArrayPool<Byte>.Shared.Return(buffer2);
            }
        }

        #endregion

        #region CopyToAsyncCore

        private static async Task CopyToAsyncCore(this Stream sourceStream, Stream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken)
        {
            var processedCounter = progress is not null ? new ProgressCounterUInt64(progress) : null;
            var buffer = ArrayPool<Byte>.Shared.Rent(bufferSize);
            try
            {
                processedCounter?.Report();
                while (true)
                {
                    var length = await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, bufferSize), cancellationToken).ConfigureAwait(false);
                    if (length <= 0)
                        break;
                    await destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(0, length), cancellationToken).ConfigureAwait(false);
                    processedCounter?.AddValue(checked((UInt32)length));
                }

                destinationStream.Flush();
            }
            finally
            {
                processedCounter?.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static async Task CopyToAsyncCore(this ISequentialInputByteStream sourceStream, ISequentialOutputByteStream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            var buffer = ArrayPool<Byte>.Shared.Rent(bufferSize);
            try
            {
                processedCounter.Report();
                while (true)
                {
                    var length = await sourceStream.ReadBytesAsyncCore(buffer.AsMemory(0, bufferSize), cancellationToken).ConfigureAwait(false);
                    if (length <= 0)
                        break;
                    await destinationStream.WriteBytesAsyncCore(buffer.AsReadOnlyMemory(0, length), cancellationToken).ConfigureAwait(false);
                    processedCounter.AddValue(checked((UInt32)length));
                }

                destinationStream.Flush();
            }
            finally
            {
                processedCounter.Report();
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        private static async Task CopyToAsyncCore(this TextReader sourceStream, TextWriter destinationStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken)
        {
            var processedCounter = progress is not null ? new ProgressCounterUInt64(progress) : null;
            var buffer = ArrayPool<Char>.Shared.Rent(bufferSize);
            try
            {
                processedCounter?.Report();
                while (true)
                {
                    var length = await sourceStream.ReadAsync(buffer.AsMemory(0, bufferSize), cancellationToken).ConfigureAwait(false);
                    if (length <= 0)
                        break;
                    await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(0, length), cancellationToken).ConfigureAwait(false);
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
