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
    public static class AsyncStreamExtensions
    {
        private const Int32 _COPY_TO_DEFAULT_BUFFER_SIZE = 81920;
        private const Int32 _WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE = 81920;

        #region GetAsyncByteSequence

        public static IAsyncEnumerable<Byte> GetAsyncByteSequenceAsync(this Stream sourceStream, Boolean leaveOpen = false, CancellationToken cancellationToken = default)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetByteSequenceAsync(null, null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetByteSequenceAsync(null, progress, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                if (offset > checked((UInt64)sourceStream.Length))
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return sourceStream.InternalGetByteSequenceAsync(offset, checked((UInt64)sourceStream.Length - offset), null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                if (offset > checked((UInt64)sourceStream.Length))
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return sourceStream.InternalGetByteSequenceAsync(offset, checked((UInt64)sourceStream.Length - offset), progress, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                if (checked(offset + count) > checked((UInt64)sourceStream.Length))
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return sourceStream.InternalGetByteSequenceAsync(offset, count, null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                if (checked(offset + count) > checked((UInt64)sourceStream.Length))
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return sourceStream.InternalGetByteSequenceAsync(offset, count, progress, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalGetByteSequenceAsync(null, null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalGetByteSequenceAsync(null, progress, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                if (offset > randomAccessStream.Length)
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return randomAccessStream.InternalGetByteSequenceAsync(offset, checked(randomAccessStream.Length - offset), null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                if (offset > randomAccessStream.Length)
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return randomAccessStream.InternalGetByteSequenceAsync(offset, checked(randomAccessStream.Length - offset), progress, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                if (checked(offset + count) > randomAccessStream.Length)
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return randomAccessStream.InternalGetByteSequenceAsync(offset, count, null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                if (checked(offset + count) > randomAccessStream.Length)
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return randomAccessStream.InternalGetByteSequenceAsync<UInt64>(offset, count, progress, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return InternalGetReverseByteSequenceAsync(sourceStream, 0, checked((UInt64)sourceStream.Length), null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return InternalGetReverseByteSequenceAsync(sourceStream, 0, checked((UInt64)sourceStream.Length), progress, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                if (offset > checked((UInt64)sourceStream.Length))
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return InternalGetReverseByteSequenceAsync(sourceStream, offset, checked((UInt64)sourceStream.Length - offset), null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                if (offset > checked((UInt64)sourceStream.Length))
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return InternalGetReverseByteSequenceAsync(sourceStream, offset, checked((UInt64)sourceStream.Length - offset), progress, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                if (checked(offset + count) > checked((UInt64)sourceStream.Length))
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return InternalGetReverseByteSequenceAsync(sourceStream, offset, count, null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();
                if (checked(offset + count) > checked((UInt64)sourceStream.Length))
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return InternalGetReverseByteSequenceAsync(sourceStream, offset, count, progress, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();

                return InternalGetReverseByteSequenceAsync(randomAccessStream, randomAccessStream.StartOfThisStream, randomAccessStream.Length, null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();

                return InternalGetReverseByteSequenceAsync(randomAccessStream, randomAccessStream.StartOfThisStream, randomAccessStream.Length, progress, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                if (offset > randomAccessStream.Length)
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return InternalGetReverseByteSequenceAsync(randomAccessStream, offset, checked(randomAccessStream.Length - offset), null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                if (offset > randomAccessStream.Length)
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return InternalGetReverseByteSequenceAsync(randomAccessStream, offset, checked(randomAccessStream.Length - offset), progress, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                if (checked(offset + count) > randomAccessStream.Length)
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return InternalGetReverseByteSequenceAsync(randomAccessStream, offset, count, null, leaveOpen, cancellationToken);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> randomAccessStream)
                    throw new NotSupportedException();
                if (checked(offset + count) > randomAccessStream.Length)
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return InternalGetReverseByteSequenceAsync(randomAccessStream, offset, count, progress, leaveOpen, cancellationToken);
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
                if (stream1 is null)
                    throw new ArgumentNullException(nameof(stream1));
                if (!stream1.CanRead)
                    throw new NotSupportedException();
                if (stream2 is null)
                    throw new ArgumentNullException(nameof(stream2));
                if (!stream2.CanRead)
                    throw new NotSupportedException();

                return await stream1.InternalStreamBytesEqualAsync(stream2, null, cancellationToken).ConfigureAwait(false);
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
                if (stream1 is null)
                    throw new ArgumentNullException(nameof(stream1));
                if (!stream1.CanRead)
                    throw new NotSupportedException();
                if (stream2 is null)
                    throw new ArgumentNullException(nameof(stream2));
                if (!stream2.CanRead)
                    throw new NotSupportedException();

                return await stream1.InternalStreamBytesEqualAsync(stream2, null, cancellationToken).ConfigureAwait(false);
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
                if (stream1 is null)
                    throw new ArgumentNullException(nameof(stream1));
                if (!stream1.CanRead)
                    throw new NotSupportedException();
                if (stream2 is null)
                    throw new ArgumentNullException(nameof(stream2));
                if (!stream2.CanRead)
                    throw new NotSupportedException();

                return await stream1.InternalStreamBytesEqualAsync(stream2, progress, cancellationToken).ConfigureAwait(false);
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
                if (stream1 is null)
                    throw new ArgumentNullException(nameof(stream1));
                if (!stream1.CanRead)
                    throw new NotSupportedException();
                if (stream2 is null)
                    throw new ArgumentNullException(nameof(stream2));
                if (!stream2.CanRead)
                    throw new NotSupportedException();

                return await stream1.InternalStreamBytesEqualAsync(stream2, progress, cancellationToken).ConfigureAwait(false);
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
                if (stream1 is null)
                    throw new ArgumentNullException(nameof(stream1));
                if (stream2 is null)
                    throw new ArgumentNullException(nameof(stream2));

                return await stream1.InternalStreamBytesEqualAsync(stream2, null, cancellationToken).ConfigureAwait(false);
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
                if (stream1 is null)
                    throw new ArgumentNullException(nameof(stream1));
                if (stream2 is null)
                    throw new ArgumentNullException(nameof(stream2));

                return await stream1.InternalStreamBytesEqualAsync(stream2, null, cancellationToken).ConfigureAwait(false);
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
                if (stream1 is null)
                    throw new ArgumentNullException(nameof(stream1));
                if (stream2 is null)
                    throw new ArgumentNullException(nameof(stream2));

                return await stream1.InternalStreamBytesEqualAsync(stream2, progress, cancellationToken).ConfigureAwait(false);
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
                if (stream1 is null)
                    throw new ArgumentNullException(nameof(stream1));
                if (stream2 is null)
                    throw new ArgumentNullException(nameof(stream2));

                return await stream1.InternalStreamBytesEqualAsync(stream2, progress, cancellationToken).ConfigureAwait(false);
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

        public static async Task CopyToAsync(this Stream source, Stream destination, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                if (source is null)
                    throw new ArgumentNullException(nameof(source));
                if (!source.CanRead)
                    throw new NotSupportedException();
                if (destination is null)
                    throw new ArgumentNullException(nameof(destination));
                if (!destination.CanWrite)
                    throw new NotSupportedException();

                await source.InternalCopyToAsync(destination, _COPY_TO_DEFAULT_BUFFER_SIZE, null, leaveOpen, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (source is not null)
                        await source.DisposeAsync().ConfigureAwait(false);
                    if (destination is not null)
                        await destination.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task CopyToAsync(this Stream source, Stream destination, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            try
            {
                if (source is null)
                    throw new ArgumentNullException(nameof(source));
                if (!source.CanRead)
                    throw new NotSupportedException();
                if (destination is null)
                    throw new ArgumentNullException(nameof(destination));
                if (!destination.CanWrite)
                    throw new NotSupportedException();

                await source.InternalCopyToAsync(destination, _COPY_TO_DEFAULT_BUFFER_SIZE, progress, false, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (source is not null)
                    await source.DisposeAsync().ConfigureAwait(false);
                if (destination is not null)
                    await destination.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task CopyToAsync(this Stream source, Stream destination, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                if (source is null)
                    throw new ArgumentNullException(nameof(source));
                if (!source.CanRead)
                    throw new NotSupportedException();
                if (destination is null)
                    throw new ArgumentNullException(nameof(destination));
                if (!destination.CanWrite)
                    throw new NotSupportedException();

                await source.InternalCopyToAsync(destination, _COPY_TO_DEFAULT_BUFFER_SIZE, progress, leaveOpen, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (source is not null)
                        await source.DisposeAsync().ConfigureAwait(false);
                    if (destination is not null)
                        await destination.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task CopyToAsync(this Stream source, Stream destination, Int32 bufferSize, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                if (source is null)
                    throw new ArgumentNullException(nameof(source));
                if (!source.CanRead)
                    throw new NotSupportedException();
                if (destination is null)
                    throw new ArgumentNullException(nameof(destination));
                if (!destination.CanWrite)
                    throw new NotSupportedException();

                await source.InternalCopyToAsync(destination, bufferSize, null, leaveOpen, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (source is not null)
                        await source.DisposeAsync().ConfigureAwait(false);
                    if (destination is not null)
                        await destination.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static async Task CopyToAsync(this Stream source, Stream destination, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            try
            {
                if (source is null)
                    throw new ArgumentNullException(nameof(source));
                if (!source.CanRead)
                    throw new NotSupportedException();
                if (destination is null)
                    throw new ArgumentNullException(nameof(destination));
                if (!destination.CanWrite)
                    throw new NotSupportedException();

                await source.InternalCopyToAsync(destination, bufferSize, progress, false, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (source is not null)
                    await source.DisposeAsync().ConfigureAwait(false);
                if (destination is not null)
                    await destination.DisposeAsync().ConfigureAwait(false);
            }
        }

        public static async Task CopyToAsync(this Stream source, Stream destination, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            try
            {
                if (source is null)
                    throw new ArgumentNullException(nameof(source));
                if (!source.CanRead)
                    throw new NotSupportedException();
                if (destination is null)
                    throw new ArgumentNullException(nameof(destination));
                if (!destination.CanWrite)
                    throw new NotSupportedException();

                await source.InternalCopyToAsync(destination, bufferSize, progress, leaveOpen, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (source is not null)
                        await source.DisposeAsync().ConfigureAwait(false);
                    if (destination is not null)
                        await destination.DisposeAsync().ConfigureAwait(false);
                }
            }
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (destination is null)
                throw new ArgumentNullException(nameof(destination));

            return source.InternalCopyToAsync(destination, _COPY_TO_DEFAULT_BUFFER_SIZE, null, false, cancellationToken);
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (destination is null)
                throw new ArgumentNullException(nameof(destination));

            return source.InternalCopyToAsync(destination, _COPY_TO_DEFAULT_BUFFER_SIZE, null, leaveOpen, cancellationToken);
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (destination is null)
                throw new ArgumentNullException(nameof(destination));

            return source.InternalCopyToAsync(destination, _COPY_TO_DEFAULT_BUFFER_SIZE, progress, false, cancellationToken);
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (destination is null)
                throw new ArgumentNullException(nameof(destination));

            return source.InternalCopyToAsync(destination, _COPY_TO_DEFAULT_BUFFER_SIZE, progress, leaveOpen, cancellationToken);
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, Int32 bufferSize, CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (destination is null)
                throw new ArgumentNullException(nameof(destination));
            if (bufferSize < 1)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            return source.InternalCopyToAsync(destination, bufferSize, null, false, cancellationToken);
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, Int32 bufferSize, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (destination is null)
                throw new ArgumentNullException(nameof(destination));
            if (bufferSize < 1)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            return source.InternalCopyToAsync(destination, bufferSize, null, leaveOpen, cancellationToken);
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (destination is null)
                throw new ArgumentNullException(nameof(destination));
            if (bufferSize < 1)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            return source.InternalCopyToAsync(destination, bufferSize, progress, false, cancellationToken);
        }

        public static Task CopyToAsync(this ISequentialInputByteStream source, ISequentialOutputByteStream destination, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (destination is null)
                throw new ArgumentNullException(nameof(destination));
            if (bufferSize < 1)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            return source.InternalCopyToAsync(destination, bufferSize, progress, leaveOpen, cancellationToken);
        }

        #endregion

        #region ReadAsync

#if false
        public static Task<Int32> ReadAsync(this Stream sourceStream, Byte[] buffer, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException(); // defined in System.IO.Stream.ReadAsync(Memory<Byte>, CancellationToken)
        }
#endif

        public static Task<Int32> ReadAsync(this Stream sourceStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return sourceStream.ReadAsync(buffer.AsMemory(offset), cancellationToken).AsTask();
        }

        public static async Task<UInt32> ReadAsync(this Stream sourceStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > (UInt32)buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var length =
                await sourceStream.ReadAsync(
                    buffer.AsMemory(offset),
                    cancellationToken)
                .ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadAsync(this Stream sourceStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            return sourceStream.ReadAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();
        }

        public static async Task<UInt32> ReadAsync(this Stream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count > (UInt32)buffer.Length))
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            var length =
                await sourceStream.ReadAsync(
                    buffer.AsMemory(offset, count),
                    cancellationToken)
                .ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return sourceStream.ReadAsync(buffer.AsMemory(offset), cancellationToken);
        }

        public static async Task<UInt32> ReadAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > (UInt32)buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var length =
                await sourceStream.ReadAsync(
                    buffer.AsMemory(offset),
                    cancellationToken)
                .ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            return sourceStream.ReadAsync(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static Task<Int32> ReadAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            return sourceStream.ReadAsync(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static async Task<UInt32> ReadAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count > (UInt32)buffer.Length))
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

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
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[1];
            return
                await sourceStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false) > 0
                ? buffer[0]
                : null;
        }

        public static async Task<Byte?> ReadByteOrNullAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[1];
            return
                await sourceStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false) > 0
                ? buffer[0]
                : null;
        }

        #endregion

        #region ReadByteAsync

        public static async Task<Byte> ReadByteAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[1];
            if (await sourceStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false) <= 0)
                throw new UnexpectedEndOfStreamException();

            return buffer[0];
        }

        public static async Task<Byte> ReadByteAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[1];
            if (await sourceStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false) <= 0)
                throw new UnexpectedEndOfStreamException();

            return buffer[0];
        }

        #endregion

        #region ReadBytesAsync

        public static Task<ReadOnlyMemory<Byte>> ReadBytesAsync(this Stream sourceStream, Int32 count, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            return sourceStream.InternalReadBytesAsync(count, cancellationToken);
        }

        public static Task<ReadOnlyMemory<Byte>> ReadBytesAsync(this Stream sourceStream, UInt32 count, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.InternalReadBytesAsync(checked((Int32)count), cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            return sourceStream.InternalReadBytesAsync(buffer, cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return sourceStream.InternalReadBytesAsync(buffer.AsMemory(offset), cancellationToken);
        }

        public static async Task<UInt32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > (UInt32)buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var length = await sourceStream.InternalReadBytesAsync(buffer.AsMemory(offset), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            return sourceStream.InternalReadBytesAsync(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            return sourceStream.InternalReadBytesAsync(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static async Task<UInt32> ReadBytesAsync(this Stream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            var length = await sourceStream.InternalReadBytesAsync(buffer.AsMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadBytesAsync(this Stream sourceStream, Memory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.InternalReadBytesAsync(buffer, cancellationToken);
        }

        public static Task<ReadOnlyMemory<Byte>> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Int32 count, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            return sourceStream.InternalReadBytesAsync(count, cancellationToken);
        }

        public static Task<ReadOnlyMemory<Byte>> ReadBytesAsync(this ISequentialInputByteStream sourceStream, UInt32 count, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            return sourceStream.InternalReadBytesAsync(checked((Int32)count), cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            return sourceStream.InternalReadBytesAsync(buffer, cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return sourceStream.InternalReadBytesAsync(buffer.AsMemory(offset), cancellationToken);
        }

        public static async Task<UInt32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > (UInt32)buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var length = await sourceStream.InternalReadBytesAsync(buffer.AsMemory(offset), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            return sourceStream.InternalReadBytesAsync(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static Task<Int32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            return sourceStream.InternalReadBytesAsync(buffer.AsMemory(offset, count), cancellationToken);
        }

        public static async Task<UInt32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            var length = await sourceStream.InternalReadBytesAsync(buffer.AsMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> ReadBytesAsync(this ISequentialInputByteStream sourceStream, Memory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            return sourceStream.InternalReadBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region ReadAllBytesAsync

        public static Task<ReadOnlyMemory<Byte>> ReadAllBytesAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.InternalReadAllBytesAsync(cancellationToken);
        }

        public static Task<ReadOnlyMemory<Byte>> ReadAllBytesAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            return sourceStream.InternalReadAllBytesAsync(cancellationToken);
        }

        #endregion

        #region ReadInt16LEAsync

        public static async Task<Int16> ReadInt16LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int16)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt16LE();
        }

        public static async Task<Int16> ReadInt16LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Int16)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt16LE();
        }

        #endregion

        #region ReadUInt16LEAsync

        public static async Task<UInt16> ReadUInt16LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt16)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt16LE();
        }

        public static async Task<UInt16> ReadUInt16LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(UInt16)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt16LE();
        }

        #endregion

        #region ReadInt32LEAsync

        public static async Task<Int32> ReadInt32LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int32)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt32LE();
        }

        public static async Task<Int32> ReadInt32LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Int32)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt32LE();
        }

        #endregion

        #region ReadUInt32LEAsync

        public static async Task<UInt32> ReadUInt32LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt32)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt32LE();
        }

        public static async Task<UInt32> ReadUInt32LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(UInt32)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt32LE();
        }

        #endregion

        #region ReadInt64LEAsync

        public static async Task<Int64> ReadInt64LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int64)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt64LE();
        }

        public static async Task<Int64> ReadInt64LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Int64)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt64LE();
        }

        #endregion

        #region ReadUInt64LEAsync

        public static async Task<UInt64> ReadUInt64LEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt64)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt64LE();
        }

        public static async Task<UInt64> ReadUInt64LEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(UInt64)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt64LE();
        }

        #endregion

        #region ReadSingleLEAsync

        public static async Task<Single> ReadSingleLEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Single)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToSingleLE();
        }

        public static async Task<Single> ReadSingleLEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Single)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToSingleLE();
        }

        #endregion

        #region ReadDoubleLEAsync

        public static async Task<Double> ReadDoubleLEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Double)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDoubleLE();
        }

        public static async Task<Double> ReadDoubleLEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Double)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDoubleLE();
        }

        #endregion

        #region ReadDecimalLEAsync

        public static async Task<Decimal> ReadDecimalLEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Decimal)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDecimalLE();
        }

        public static async Task<Decimal> ReadDecimalLEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Decimal)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDecimalLE();
        }

        #endregion

        #region ReadInt16BEAsync

        public static async Task<Int16> ReadInt16BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int16)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt16BE();
        }

        public static async Task<Int16> ReadInt16BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Int16)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt16BE();
        }

        #endregion

        #region ReadUInt16BEAsync

        public static async Task<UInt16> ReadUInt16BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt16)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt16BE();
        }

        public static async Task<UInt16> ReadUInt16BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(UInt16)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt16BE();
        }

        #endregion

        #region ReadInt32BEAsync

        public static async Task<Int32> ReadInt32BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int32)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt32BE();
        }

        public static async Task<Int32> ReadInt32BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Int32)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt32BE();
        }

        #endregion

        #region ReadUInt32BEAsync

        public static async Task<UInt32> ReadUInt32BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt32)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt32BE();
        }

        public static async Task<UInt32> ReadUInt32BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(UInt32)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt32BE();
        }

        #endregion

        #region ReadInt64BEAsync

        public static async Task<Int64> ReadInt64BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int64)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt64BE();
        }

        public static async Task<Int64> ReadInt64BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Int64)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt64BE();
        }

        #endregion

        #region ReadUInt64BEAsync

        public static async Task<UInt64> ReadUInt64BEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt64)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt64BE();
        }

        public static async Task<UInt64> ReadUInt64BEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(UInt64)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt64BE();
        }

        #endregion

        #region ReadSingleBEAsync

        public static async Task<Single> ReadSingleBEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Single)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToSingleBE();
        }

        public static async Task<Single> ReadSingleBEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Single)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToSingleBE();
        }

        #endregion

        #region ReadDoubleBEAsync

        public static async Task<Double> ReadDoubleBEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Double)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDoubleBE();
        }

        public static async Task<Double> ReadDoubleBEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Double)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDoubleBE();
        }

        #endregion

        #region ReadDecimalBEAsync

        public static async Task<Decimal> ReadDecimalBEAsync(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Decimal)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDecimalBE();
        }

        public static async Task<Decimal> ReadDecimalBEAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellationToken = default)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            var buffer = new Byte[sizeof(Decimal)];
            if (await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDecimalBE();
        }

        #endregion

        #region WriteAsync

        public static async Task<Int32> WriteAsync(this Stream destinationStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            var count = buffer.Length - offset;
            await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return count;
        }

        public static async Task<UInt32> WriteAsync(this Stream destinationStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var count = checked((UInt32)buffer.Length - offset);
            await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return count;
        }

        public static async Task<UInt32> WriteAsync(this Stream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count > (UInt32)buffer.Length))
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return count;
        }

        public static async Task<Int32> WriteAsync(this Stream destinationStream, ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            await destinationStream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
            return buffer.Length;
        }

        public static Task<Int32> WriteAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset), cancellationToken);
        }

        public static async Task<UInt32> WriteAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var length = await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        public static Task<Int32> WriteAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            return destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static async Task<UInt32> WriteAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count > (UInt32)buffer.Length))
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            var length = await destinationStream.WriteAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken).ConfigureAwait(false);
            return checked((UInt32)length);
        }

        #endregion

        #region WriteByteAsync

        public static async Task WriteByteAsync(this ISequentialOutputByteStream destinationStream, Byte value, CancellationToken cancellationToken = default)
        {
            var length = await destinationStream.WriteAsync(new[] { value }, cancellationToken).ConfigureAwait(false);
            Validation.Assert(length > 0, "length > 0");
        }

        #endregion

        #region WriteBytesAsync

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(offset), cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(offset), cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            return destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            return destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count > (UInt32)buffer.Length))
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            return destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this Stream destinationStream, ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(offset), cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > (UInt32)buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(offset), cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Range range, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            return destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            return destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count > (UInt32)buffer.Length))
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            return destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(offset, count), cancellationToken);
        }

        public static Task WriteBytesAsync(this ISequentialOutputByteStream destinationStream, ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteByteSequenceAsync

        public static async Task WriteByteSequenceAsync(this Stream destinationStream, IEnumerable<Byte> sequence, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (sequence is null)
                throw new ArgumentNullException(nameof(sequence));

            using var enumerator = sequence.GetEnumerator();
            var buffer = new Byte[_WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE];
            var isEndOfSequence = false;
            while (!isEndOfSequence)
            {
                var index = 0;
                while (index < buffer.Length)
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

        public static async Task WriteByteSequenceAsync(this Stream destinationStream, IAsyncEnumerable<Byte> sequence, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (sequence is null)
                throw new ArgumentNullException(nameof(sequence));

            var enumerator = sequence.GetAsyncEnumerator(cancellationToken);
            await using (enumerator.ConfigureAwait(false))
            {
                var buffer = new Byte[_WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE];
                var isEndOfSequence = false;
                while (!isEndOfSequence)
                {
                    var index = 0;
                    while (index < buffer.Length)
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

        public static async Task WriteByteSequenceAsync(this ISequentialOutputByteStream destinationStream, IEnumerable<Byte> sequence, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (sequence is null)
                throw new ArgumentNullException(nameof(sequence));

            using var enumerator = sequence.GetEnumerator();
            var buffer = new Byte[_WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE];
            var isEndOfSequence = false;
            while (!isEndOfSequence)
            {
                var index = 0;
                while (index < buffer.Length)
                {
                    if (!enumerator.MoveNext())
                    {
                        isEndOfSequence = true;
                        break;
                    }

                    buffer[index++] = enumerator.Current;
                }

                if (index > 0)
                    await destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(0, index), cancellationToken).ConfigureAwait(false);
            }
        }

        public static async Task WriteByteSequenceAsync(this ISequentialOutputByteStream destinationStream, IAsyncEnumerable<Byte> sequence, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (sequence is null)
                throw new ArgumentNullException(nameof(sequence));

            var enumerator = sequence.GetAsyncEnumerator(cancellationToken);
            await using (enumerator.ConfigureAwait(false))
            {
                var buffer = new Byte[_WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE];
                var isEndOfSequence = false;
                while (!isEndOfSequence)
                {
                    var index = 0;
                    while (index < buffer.Length)
                    {
                        if (!await enumerator.MoveNextAsync().ConfigureAwait(false))
                        {
                            isEndOfSequence = true;
                            break;
                        }

                        buffer[index++] = enumerator.Current;
                    }

                    if (index > 0)
                        await destinationStream.InternalWriteBytesAsync(buffer.AsReadOnlyMemory(0, index), cancellationToken).ConfigureAwait(false);
                }
            }
        }

        #endregion

        #region WriteInt16LEAsync

        public static Task WriteInt16LEAsync(this Stream destinationStream, Int16 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int16)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteInt16LEAsync(this ISequentialOutputByteStream destinationStream, Int16 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Int16)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteUInt16LEAsync

        public static Task WriteUInt16LEAsync(this Stream destinationStream, UInt16 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt16)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteUInt16LEAsync(this ISequentialOutputByteStream destinationStream, UInt16 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(UInt16)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteInt32LEAsync

        public static Task WriteInt32LEAsync(this Stream destinationStream, Int32 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int32)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteInt32LEAsync(this ISequentialOutputByteStream destinationStream, Int32 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Int32)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteUInt32LEAsync

        public static Task WriteUInt32LEAsync(this Stream destinationStream, UInt32 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt32)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteUInt32LEAsync(this ISequentialOutputByteStream destinationStream, UInt32 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(UInt32)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteInt64LEAsync

        public static Task WriteInt64LEAsync(this Stream destinationStream, Int64 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int64)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteInt64LEAsync(this ISequentialOutputByteStream destinationStream, Int64 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Int64)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteUInt64LEAsync

        public static Task WriteUInt64LEAsync(this Stream destinationStream, UInt64 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt64)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteUInt64LEAsync(this ISequentialOutputByteStream destinationStream, UInt64 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(UInt64)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteSingleLEAsync

        public static Task WriteSingleLEAsync(this Stream destinationStream, Single value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Single)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteSingleLEAsync(this ISequentialOutputByteStream destinationStream, Single value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Single)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteDoubleLEAsync

        public static Task WriteDoubleLEAsync(this Stream destinationStream, Double value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Double)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteDoubleLEAsync(this ISequentialOutputByteStream destinationStream, Double value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Double)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteDecimalLEAsync

        public static Task WriteDecimalLEAsync(this Stream destinationStream, Decimal value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Decimal)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteDecimalLEAsync(this ISequentialOutputByteStream destinationStream, Decimal value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Decimal)];
            buffer.SetValueLE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteInt16BEAsync

        public static Task WriteInt16BEAsync(this Stream destinationStream, Int16 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int16)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteInt16BEAsync(this ISequentialOutputByteStream destinationStream, Int16 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Int16)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteUInt16BEAsync

        public static Task WriteUInt16BEAsync(this Stream destinationStream, UInt16 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt16)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteUInt16BEAsync(this ISequentialOutputByteStream destinationStream, UInt16 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(UInt16)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteInt32BEAsync

        public static Task WriteInt32BEAsync(this Stream destinationStream, Int32 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int32)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteInt32BEAsync(this ISequentialOutputByteStream destinationStream, Int32 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Int32)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteUInt32BEAsync

        public static Task WriteUInt32BEAsync(this Stream destinationStream, UInt32 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt32)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteUInt32BEAsync(this ISequentialOutputByteStream destinationStream, UInt32 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(UInt32)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteInt64BEAsync

        public static Task WriteInt64BEAsync(this Stream destinationStream, Int64 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Int64)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteInt64BEAsync(this ISequentialOutputByteStream destinationStream, Int64 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Int64)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteUInt64BEAsync

        public static Task WriteUInt64BEAsync(this Stream destinationStream, UInt64 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(UInt64)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteUInt64BEAsync(this ISequentialOutputByteStream destinationStream, UInt64 value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(UInt64)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteSingleBEAsync

        public static Task WriteSingleBEAsync(this Stream destinationStream, Single value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Single)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteSingleBEAsync(this ISequentialOutputByteStream destinationStream, Single value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Single)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteDoubleBEAsync

        public static Task WriteDoubleBEAsync(this Stream destinationStream, Double value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Double)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteDoubleBEAsync(this ISequentialOutputByteStream destinationStream, Double value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Double)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region WriteDecimalBEAsync

        public static Task WriteDecimalBEAsync(this Stream destinationStream, Decimal value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            var buffer = new Byte[sizeof(Decimal)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        public static Task WriteDecimalBEAsync(this ISequentialOutputByteStream destinationStream, Decimal value, CancellationToken cancellationToken = default)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var buffer = new Byte[sizeof(Decimal)];
            buffer.SetValueBE(0, value);
            return destinationStream.InternalWriteBytesAsync(buffer, cancellationToken);
        }

        #endregion

        #region CalculateCrc24Async

        public static async Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this Stream sourceStream, CancellationToken cancellationToken = default)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc24Async(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc24Async(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc24Async(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc24Async(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc24Async(bufferSize, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc24Async(bufferSize, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc24Async(bufferSize, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc24Async(bufferSize, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc24Async(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc24Async(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc24Async(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc24Async(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc24Async(bufferSize, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc24Async(bufferSize, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc24Async(bufferSize, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc24Async(bufferSize, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc32Async(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc32Async(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc32Async(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc32Async(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc32Async(bufferSize, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc32Async(bufferSize, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc32Async(bufferSize, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return await sourceStream.InternalCalculateCrc32Async(bufferSize, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc32Async(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc32Async(MAX_BUFFER_SIZE, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc32Async(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc32Async(MAX_BUFFER_SIZE, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc32Async(bufferSize, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc32Async(bufferSize, null, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc32Async(bufferSize, progress, cancellationToken).ConfigureAwait(false);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return await sourceStream.InternalCalculateCrc32Async(bufferSize, progress, cancellationToken).ConfigureAwait(false);
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

        #region InternalGetByteSequenceAsync

        private static async IAsyncEnumerable<Byte> InternalGetByteSequenceAsync(this Stream sourceStream, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

            var processedCounter = new ProgressCounterUInt64(progress);
            try
            {
                processedCounter.Report();
                var buffer = new Byte[BUFFER_SIZE];
                while (true)
                {
                    var readCount = buffer.Length;
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
            }
        }

        private static async IAsyncEnumerable<Byte> InternalGetByteSequenceAsync(this ISequentialInputByteStream sourceStream, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

            var processedCounter = new ProgressCounterUInt64(progress);
            try
            {
                processedCounter.Report();
                var buffer = new Byte[BUFFER_SIZE];
                while (true)
                {
                    var readCount = buffer.Length;
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
            }
        }

        private static async IAsyncEnumerable<Byte> InternalGetByteSequenceAsync(this Stream sourceStream, UInt64 offset, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const Int32 BUFFER_SIZE = 80 * 1024;

            var processedCounter = new ProgressCounterUInt64(progress);
            try
            {
                if (!sourceStream.CanSeek)
                    throw new ArgumentException($"If stream {nameof(sourceStream)} is sequential, parameter {nameof(offset)} must not be specified.", nameof(offset));

                _ = sourceStream.Seek(checked((Int64)offset), SeekOrigin.Begin);
                processedCounter.Report();
                var buffer = new Byte[BUFFER_SIZE];
                while (true)
                {
                    var readCount = buffer.Length;
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
            }
        }

        private static async IAsyncEnumerable<Byte> InternalGetByteSequenceAsync<POSITION_T>(this ISequentialInputByteStream sourceStream, POSITION_T offset, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const Int32 BUFFER_SIZE = 80 * 1024;

            var processedCounter = new ProgressCounterUInt64(progress);
            try
            {
                if (sourceStream is not IRandomInputByteStream<POSITION_T> randomAccessStream)
                    throw new ArgumentException($"If stream {nameof(sourceStream)} is sequential, parameter {nameof(offset)} must not be specified.", nameof(offset));

                randomAccessStream.Seek(offset);
                processedCounter.Report();
                var buffer = new Byte[BUFFER_SIZE];
                while (true)
                {
                    var readCount = buffer.Length;
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
            }
        }

        #endregion

        #region InternalGetReverseByteSequenceAsync

        private static async IAsyncEnumerable<Byte> InternalGetReverseByteSequenceAsync(this Stream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const Int32 BUFFER_SIZE = 80 * 1024;

            var progressCounter = new ProgressCounterUInt64(progress);
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                progressCounter.Report();
                var buffer = new Byte[BUFFER_SIZE];
                var pos = checked(offset + count);
                while (pos.CompareTo(offset + BUFFER_SIZE) > 0)
                {
                    pos -= BUFFER_SIZE;
                    _ = sourceStream.Seek(checked((Int64)pos), SeekOrigin.Begin);
                    var length = await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false);
                    Validation.Assert(length == buffer.Length, "length == buffer.Length");
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
                    Validation.Assert(length == remain, "length == remain");
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
            }
        }

        private static async IAsyncEnumerable<Byte> InternalGetReverseByteSequenceAsync<POSITION_T>(this IRandomInputByteStream<POSITION_T> sourceStream, POSITION_T offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen, [EnumeratorCancellation] CancellationToken cancellationToken)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            const Int32 BUFFER_SIZE = 80 * 1024;

            var progressCounter = new ProgressCounterUInt64(progress);
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                progressCounter.Report();
                var buffer = new Byte[BUFFER_SIZE];
                var pos = checked(offset + count);
                while (pos.CompareTo(offset + BUFFER_SIZE) > 0)
                {
                    pos -= BUFFER_SIZE;
                    sourceStream.Seek(pos);
                    var length = await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false);
                    Validation.Assert(length == buffer.Length, "length == buffer.Length");
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
                    Validation.Assert(length == remain, "length == remain");
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
            }
        }

        #endregion

        #region InternalStreamBytesEqualAsync

        private static async Task<Boolean> InternalStreamBytesEqualAsync(this Stream stream1, Stream stream2, IProgress<UInt64>? progress, CancellationToken cancellationToken)
        {
            const Int32 bufferSize = 81920;

            Validation.Assert(bufferSize % sizeof(UInt64) == 0, "bufferSize % sizeof(UInt64) == 0");
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer1 = new Byte[bufferSize];
            var buffer2 = new Byte[bufferSize];
            try
            {
                while (true)
                {
                    // まず両方のストリームから bufferSize バイトだけ読み込みを試みる
                    var bufferCount1 = await stream1.InternalReadBytesAsync(buffer1, cancellationToken).ConfigureAwait(false);
                    var bufferCount2 = await stream2.InternalReadBytesAsync(buffer2, cancellationToken).ConfigureAwait(false);
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

                    if (bufferCount1 < buffer1.Length)
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
            }
        }

        private static async Task<Boolean> InternalStreamBytesEqualAsync(this ISequentialInputByteStream stream1, ISequentialInputByteStream stream2, IProgress<UInt64>? progress, CancellationToken cancellationToken)
        {
            const Int32 bufferSize = 81920;

            Validation.Assert(bufferSize % sizeof(UInt64) == 0, "bufferSize % sizeof(UInt64) == 0");
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer1 = new Byte[bufferSize];
            var buffer2 = new Byte[bufferSize];
            try
            {
                while (true)
                {
                    // まず両方のストリームから bufferSize バイトだけ読み込みを試みる
                    var bufferCount1 = await stream1.InternalReadBytesAsync(buffer1, cancellationToken).ConfigureAwait(false);
                    var bufferCount2 = await stream2.InternalReadBytesAsync(buffer2, cancellationToken).ConfigureAwait(false);
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

                    if (bufferCount1 < buffer1.Length)
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
            }
        }

        #endregion

        #region InternalCopyToAsync

        private static async Task InternalCopyToAsync(this Stream sourceStream, Stream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            try
            {
                processedCounter.Report();
                var buffer = new Byte[bufferSize];
                while (true)
                {
                    var length = await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false);
                    if (length <= 0)
                        break;
                    await destinationStream.InternalWriteBytesAsync(buffer, cancellationToken).ConfigureAwait(false);
                    processedCounter.AddValue(checked((UInt32)length));
                }

                destinationStream.Flush();
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                    if (destinationStream is not null)
                        await destinationStream.DisposeAsync().ConfigureAwait(false);
                }

                processedCounter.Report();
            }
        }

        private static async Task InternalCopyToAsync(this ISequentialInputByteStream sourceStream, ISequentialOutputByteStream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress, Boolean leaveOpen, CancellationToken cancellationToken)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            try
            {
                processedCounter.Report();
                var buffer = new Byte[bufferSize];
                while (true)
                {
                    var length = await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false);
                    if (length <= 0)
                        break;
                    await destinationStream.InternalWriteBytesAsync(buffer, cancellationToken).ConfigureAwait(false);
                    processedCounter.AddValue(checked((UInt32)length));
                }

                destinationStream.Flush();
            }
            finally
            {
                if (!leaveOpen)
                {
                    if (sourceStream is not null)
                        await sourceStream.DisposeAsync().ConfigureAwait(false);
                    if (destinationStream is not null)
                        await destinationStream.DisposeAsync().ConfigureAwait(false);
                }

                processedCounter.Report();
            }
        }

        #endregion

        #region InternalReadBytesAsync

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<ReadOnlyMemory<Byte>> InternalReadBytesAsync(this Stream sourceStream, Int32 count, CancellationToken cancellationToken)
        {
            var buffer = new Byte[count];
            var length = await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false);
            if (length < buffer.Length)
                Array.Resize(ref buffer, length);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<ReadOnlyMemory<Byte>> InternalReadBytesAsync(this ISequentialInputByteStream sourceStream, Int32 count, CancellationToken cancellationToken)
        {
            var buffer = new Byte[count];
            var length = await sourceStream.InternalReadBytesAsync(buffer, cancellationToken).ConfigureAwait(false);
            if (length < buffer.Length)
                Array.Resize(ref buffer, length);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<Int32> InternalReadBytesAsync(this Stream sourceStream, Memory<Byte> buffer, CancellationToken cancellationToken)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<Int32> InternalReadBytesAsync(this ISequentialInputByteStream sourceStream, Memory<Byte> buffer, CancellationToken cancellationToken)
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

        #region InternalReadAllBytesAsync

        private static async Task<ReadOnlyMemory<Byte>> InternalReadAllBytesAsync(this Stream sourceStream, CancellationToken cancellation)
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

        private static async Task<ReadOnlyMemory<Byte>> InternalReadAllBytesAsync(this ISequentialInputByteStream sourceStream, CancellationToken cancellation)
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
            if (!destinationWindow.IsEmpty)
                throw new Exception();
#endif
            return buffer;
        }

        #endregion

        #region InternalWriteBytesAsync

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task InternalWriteBytesAsync(this Stream destinationStream, ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken)
            => destinationStream.WriteAsync(buffer, cancellationToken).AsTask();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task InternalWriteBytesAsync(this ISequentialOutputByteStream destinationStream, ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken)
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

        #region InternalCalculateCrc24Async

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<(UInt32 Crc, UInt64 Length)> InternalCalculateCrc24Async(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken)
            => InternalCalculateCrcAsync(sourceStream, bufferSize, progress, Crc24.CreateCalculationState(), cancellationToken);

        private static Task<(UInt32 Crc, UInt64 Length)> InternalCalculateCrc24Async(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken)
            => InternalCalculateCrcAsync(sourceStream, bufferSize, progress, Crc24.CreateCalculationState(), cancellationToken);

        #endregion

        #region InternalCalculateCrc32Async

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<(UInt32 Crc, UInt64 Length)> InternalCalculateCrc32Async(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken)
            => InternalCalculateCrcAsync(sourceStream, bufferSize, progress, Crc32.CreateCalculationState(), cancellationToken);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<(UInt32 Crc, UInt64 Length)> InternalCalculateCrc32Async(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, CancellationToken cancellationToken)
            => InternalCalculateCrcAsync(sourceStream, bufferSize, progress, Crc32.CreateCalculationState(), cancellationToken);

        #endregion

        #region InternalCalculateCrcAsync

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<(UInt32 Crc, UInt64 Length)> InternalCalculateCrcAsync(Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, ICrcCalculationState<UInt32> session, CancellationToken cancellationToken)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = new Byte[bufferSize];
            try
            {
                while (true)
                {
                    var length = await sourceStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
                    if (length <= 0)
                        break;
                    session.Put(buffer, 0, length);
                    processedCounter.AddValue(checked((UInt64)length));
                }

                return session.GetResultValue();
            }
            finally
            {
                processedCounter.Report();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<(UInt32 Crc, UInt64 Length)> InternalCalculateCrcAsync(ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, ICrcCalculationState<UInt32> session, CancellationToken cancellationToken)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = new Byte[bufferSize];
            try
            {
                while (true)
                {
                    var length = await sourceStream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
                    if (length <= 0)
                        break;
                    session.Put(buffer, 0, length);
                    processedCounter.AddValue(checked((UInt64)length));
                }

                return session.GetResultValue();
            }
            finally
            {
                processedCounter.Report();
            }
        }

        #endregion
    }
}
