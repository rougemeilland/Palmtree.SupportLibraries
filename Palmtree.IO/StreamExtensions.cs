using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using Palmtree;
using Palmtree.IO.StreamFilters;

namespace Palmtree.IO
{
    public static class StreamExtensions
    {
        private const Int32 _COPY_TO_DEFAULT_BUFFER_SIZE = 81920;
        private const Int32 _WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE = 81920;

        #region AsInputByteStream

        public static ISequentialInputByteStream AsInputByteStream(this Stream sourceStream, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new ArgumentException($"The stream specified by parameter {nameof(sourceStream)} is not readable.", nameof(sourceStream));

                return
                    sourceStream.CanSeek
                    ? new RandomInputByteStreamByDotNetStream(sourceStream, leaveOpen)
                    : new SequentialInputByteStreamByDotNetStream(sourceStream, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region AsOutputByteStream

        public static ISequentialOutputByteStream AsOutputByteStream(this Stream destinationStream, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));
                if (!destinationStream.CanWrite)
                    throw new ArgumentException($"The stream specified by parameter {nameof(destinationStream)} is not writable.", nameof(destinationStream));

                return destinationStream.CanSeek
                    ? new RandomOutputByteStreamByDotNetStream(destinationStream, leaveOpen)
                    : new SequentialOutputByteStreamByDotNetStream(destinationStream, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region AsDotNetStream

        public static Stream AsDotNetStream(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return new DotNetStreamBySequentialInputByteStream(sourceStream, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static Stream AsDotNetStream(this ISequentialOutputByteStream destinationStream, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return new DotNetStreamBySequentialOutputByteStream(destinationStream, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region AsByteStream

        public static ISequentialInputByteStream AsByteStream(this IEnumerable<Byte> baseSequence)
        {
            if (baseSequence is null)
                throw new ArgumentNullException(nameof(baseSequence));

            return new SequentialInputByteStreamBySequence(baseSequence);
        }

        public static ISequentialInputByteStream AsByteStream(this IInputBitStream sourceStream, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return new SequentialInputByteStreamByBitStream(sourceStream, BitPackingDirection.MsbToLsb, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static ISequentialInputByteStream AsByteStream(this IInputBitStream sourceStream, BitPackingDirection bitPackingDirection, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return new SequentialInputByteStreamByBitStream(sourceStream, bitPackingDirection, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static ISequentialOutputByteStream AsByteStream(this IOutputBitStream destinationStream, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return new SequentialOutputByteStreamByBitStream(destinationStream, BitPackingDirection.MsbToLsb, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        public static ISequentialOutputByteStream AsByteStream(this IOutputBitStream destinationStream, BitPackingDirection bitPackingDirection, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return new SequentialOutputByteStreamByBitStream(destinationStream, bitPackingDirection, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region AsBitStream

        public static IInputBitStream AsBitStream(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return new SequentialInputBitStreamByByteStream(sourceStream, BitPackingDirection.Default, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IInputBitStream AsBitStream(this ISequentialInputByteStream sourceStream, BitPackingDirection bitPackingDirection, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return new SequentialInputBitStreamByByteStream(sourceStream, bitPackingDirection, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IInputBitStream AsBitStream(this IEnumerable<Byte> baseSequence, BitPackingDirection bitPackingDirection = BitPackingDirection.Default)
        {
            if (baseSequence is null)
                throw new ArgumentNullException(nameof(baseSequence));

            return new SequentialInputBitStreamBySequence(baseSequence, bitPackingDirection);
        }

        public static IOutputBitStream AsBitStream(this ISequentialOutputByteStream destinationStream, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return new SequentialOutputBitStreamByByteStream(destinationStream, BitPackingDirection.Default, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        public static IOutputBitStream AsBitStream(this ISequentialOutputByteStream destinationStream, BitPackingDirection bitPackingDirection, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return new SequentialOutputBitStreamByByteStream(destinationStream, bitPackingDirection, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region AsSequentialAccess

        public static ISequentialInputByteStream AsSequentialAccess<POSITION_T>(this IRandomInputByteStream<POSITION_T> sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            return sourceStream;
        }

        public static ISequentialOutputByteStream AsSequentialAccess<POSITION_T>(this IRandomOutputByteStream<POSITION_T> destinationStream)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            return destinationStream;
        }

        #endregion

        #region AsRandomAccess

        public static IRandomInputByteStream<POSITION_T> AsRandomAccess<POSITION_T>(this ISequentialInputByteStream sourceStream)
            where POSITION_T : struct
        {
            if (sourceStream is not IRandomInputByteStream<POSITION_T>)
                throw new ArgumentException($"Stream object {nameof(sourceStream)} does not support interface {nameof(IRandomInputByteStream<POSITION_T>)}.", nameof(sourceStream));

            return (IRandomInputByteStream<POSITION_T>)sourceStream;
        }

        public static IRandomOutputByteStream<POSITION_T> AsRandomAccess<POSITION_T>(this ISequentialOutputByteStream destinationStream)
            where POSITION_T : struct
        {
            if (destinationStream is not IRandomOutputByteStream<POSITION_T>)
                throw new ArgumentException($"Stream object {nameof(destinationStream)} does not support interface {nameof(IRandomOutputByteStream<POSITION_T>)}.", nameof(destinationStream));

            return (IRandomOutputByteStream<POSITION_T>)destinationStream;
        }

        #endregion

        #region AsTextReader

        public static TextReader AsTextReader(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return new StreamReader(sourceStream);
        }

        public static TextReader AsTextReader(this Stream sourceStream, Encoding encoding)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return new StreamReader(sourceStream, encoding);
        }

        public static TextReader AsTextReader(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            return sourceStream.AsDotNetStream().AsTextReader();
        }

        public static TextReader AsTextReader(this ISequentialInputByteStream sourceStream, Encoding encoding)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return sourceStream.AsDotNetStream().AsTextReader(encoding);
        }

        #endregion

        #region AsTextWriter

        public static TextWriter AsTextWriter(this Stream destinationStream)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            return new StreamWriter(destinationStream);
        }

        public static TextWriter AsTextWriter(this Stream destinationStream, Encoding encoding)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return new StreamWriter(destinationStream, encoding);
        }

        public static TextWriter AsTextWriter(this ISequentialOutputByteStream destinationStream)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            return destinationStream.AsDotNetStream().AsTextWriter();
        }

        public static TextWriter AsTextWriter(this ISequentialOutputByteStream destinationStream, Encoding encoding)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return destinationStream.AsDotNetStream().AsTextWriter(encoding);
        }

        #endregion

        #region WithPartial

        public static ISequentialInputByteStream WithPartial(this ISequentialInputByteStream sourceStream, UInt64 size, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return
                    sourceStream switch
                    {
                        IRandomInputByteStream<UInt64> randomAccessStream
                            => new PartialRandomInputStream<UInt64, UInt64>(randomAccessStream, size, 0UL, leaveOpen),
                        _
                            => new PartialSequentialInputStream(sourceStream, size, leaveOpen),
                    };
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static ISequentialInputByteStream WithPartial(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64? size, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return
                    sourceStream switch
                    {
                        IRandomInputByteStream<UInt64> randomAccessStream
                            => new PartialRandomInputStream<UInt64, UInt64>(randomAccessStream, offset, size, 0UL, leaveOpen),
                        _
                            => throw new ArgumentException($"Stream object {nameof(sourceStream)} does not support interface {nameof(IRandomInputByteStream<UInt64>)}.", nameof(sourceStream))
                    };
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IRandomInputByteStream<UInt64> WithPartial<BASE_POSITION_T>(this IRandomInputByteStream<BASE_POSITION_T> sourceStream, UInt64? size, Boolean leaveOpen = false)
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return new PartialRandomInputStream<UInt64, BASE_POSITION_T>(sourceStream, size, 0UL, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IRandomInputByteStream<UInt64> WithPartial<BASE_POSITION_T>(this IRandomInputByteStream<BASE_POSITION_T> sourceStream, BASE_POSITION_T offset, UInt64? size, Boolean leaveOpen = false)
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return new PartialRandomInputStream<UInt64, BASE_POSITION_T>(sourceStream, offset, size, 0UL, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IRandomInputByteStream<POSITION_T> WithPartial<POSITION_T, BASE_POSITION_T>(this IRandomInputByteStream<BASE_POSITION_T> sourceStream, UInt64? size, POSITION_T zeroPositionValue, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return new PartialRandomInputStream<POSITION_T, BASE_POSITION_T>(sourceStream, size, zeroPositionValue, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IRandomInputByteStream<POSITION_T> WithPartial<POSITION_T, BASE_POSITION_T>(this IRandomInputByteStream<BASE_POSITION_T> sourceStream, BASE_POSITION_T offset, UInt64? size, POSITION_T zeroPositionValue, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return new PartialRandomInputStream<POSITION_T, BASE_POSITION_T>(sourceStream, offset, size, zeroPositionValue, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static ISequentialOutputByteStream WithPartial(this ISequentialOutputByteStream destinationStream, UInt64 size, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return
                    destinationStream switch
                    {
                        IRandomOutputByteStream<UInt64> randomAccessStream
                            => new PartialRandomOutputStream<UInt64, UInt64>(randomAccessStream, size, 0UL, leaveOpen),
                        _
                            => new PartialSequentialOutputStream(destinationStream, size, leaveOpen),
                    };
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        public static ISequentialOutputByteStream WithPartial(this ISequentialOutputByteStream destinationStream, UInt64 offset, UInt64? size, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return
                    destinationStream switch
                    {
                        IRandomOutputByteStream<UInt64> randomAccessStream
                            => new PartialRandomOutputStream<UInt64, UInt64>(randomAccessStream, offset, size, 0UL, leaveOpen),
                        _
                            => throw new ArgumentException($"Stream object {nameof(destinationStream)} does not support interface {nameof(IRandomOutputByteStream<UInt64>)}.", nameof(destinationStream))
                    };
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        public static IRandomOutputByteStream<UInt64> WithPartial<BASE_POSITION_T>(this IRandomOutputByteStream<BASE_POSITION_T> destinationStream, UInt64 size, Boolean leaveOpen = false)
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return new PartialRandomOutputStream<UInt64, BASE_POSITION_T>(destinationStream, size, 0UL, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        public static IRandomOutputByteStream<UInt64> WithPartial<BASE_POSITION_T>(this IRandomOutputByteStream<BASE_POSITION_T> destinationStream, BASE_POSITION_T offset, UInt64? size, Boolean leaveOpen = false)
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return new PartialRandomOutputStream<UInt64, BASE_POSITION_T>(destinationStream, offset, size, 0UL, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        public static IRandomOutputByteStream<POSITION_T> WithPartial<POSITION_T, BASE_POSITION_T>(this IRandomOutputByteStream<BASE_POSITION_T> destinationStream, UInt64 size, POSITION_T zeroPositionValue, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return new PartialRandomOutputStream<POSITION_T, BASE_POSITION_T>(destinationStream, size, zeroPositionValue, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        public static IRandomOutputByteStream<POSITION_T> WithPartial<POSITION_T, BASE_POSITION_T>(this IRandomOutputByteStream<BASE_POSITION_T> destinationStream, BASE_POSITION_T offset, UInt64? size, POSITION_T zeroPositionValue, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return new PartialRandomOutputStream<POSITION_T, BASE_POSITION_T>(destinationStream, offset, size, zeroPositionValue, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region WithCache

        public static ISequentialInputByteStream WithCache(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return
                    sourceStream switch
                    {
                        IRandomInputByteStream<UInt64> baseRandomAccessStream
                            => new BufferedRandomInputStream<UInt64>(baseRandomAccessStream, leaveOpen),
                        _
                            => new BufferedSequentialInputStream(sourceStream, leaveOpen),
                    };
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static ISequentialInputByteStream WithCache(this ISequentialInputByteStream sourceStream, Int32 cacheSize, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (cacheSize <= 0)
                    throw new ArgumentOutOfRangeException(nameof(cacheSize));

                return
                    sourceStream switch
                    {
                        IRandomInputByteStream<UInt64> baseRandomAccessStream
                            => new BufferedRandomInputStream<UInt64>(baseRandomAccessStream, cacheSize, leaveOpen),
                        _
                            => new BufferedSequentialInputStream(sourceStream, cacheSize, leaveOpen),
                    };
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IRandomInputByteStream<POSITION_T> WithCache<POSITION_T>(this IRandomInputByteStream<POSITION_T> sourceStream, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return new BufferedRandomInputStream<POSITION_T>(sourceStream, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IRandomInputByteStream<POSITION_T> WithCache<POSITION_T>(this IRandomInputByteStream<POSITION_T> sourceStream, Int32 cacheSize, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (cacheSize <= 0)
                    throw new ArgumentOutOfRangeException(nameof(cacheSize));

                return new BufferedRandomInputStream<POSITION_T>(sourceStream, cacheSize, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static ISequentialOutputByteStream WithCache(this ISequentialOutputByteStream destinationStream, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return
                    destinationStream switch
                    {
                        IRandomOutputByteStream<UInt64> baseRandomAccessStream
                            => new BufferedRandomOutputStream<UInt64>(baseRandomAccessStream, leaveOpen),
                        _
                            => new BufferedSequentialOutputStream(destinationStream, leaveOpen)
                    };
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        public static ISequentialOutputByteStream WithCache(this ISequentialOutputByteStream destinationStream, Int32 cacheSize, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));
                if (cacheSize <= 0)
                    throw new ArgumentOutOfRangeException(nameof(cacheSize));

                return
                    destinationStream switch
                    {
                        IRandomOutputByteStream<UInt64> baseRandomAccessStream
                            => new BufferedRandomOutputStream<UInt64>(baseRandomAccessStream, cacheSize, leaveOpen),
                        _
                            => new BufferedSequentialOutputStream(destinationStream, cacheSize, leaveOpen)
                    };
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        public static IRandomOutputByteStream<POSITION_T> WithCache<POSITION_T>(this IRandomOutputByteStream<POSITION_T> destinationStream, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));

                return new BufferedRandomOutputStream<POSITION_T>(destinationStream, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        public static IRandomOutputByteStream<POSITION_T> WithCache<POSITION_T>(this IRandomOutputByteStream<POSITION_T> destinationStream, Int32 cacheSize, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));
                if (cacheSize <= 0)
                    throw new ArgumentOutOfRangeException(nameof(cacheSize));

                return new BufferedRandomOutputStream<POSITION_T>(destinationStream, cacheSize, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region WithProgression

        public static ISequentialInputByteStream WithProgression(this ISequentialInputByteStream sourceStream, IProgress<UInt64> progress, Boolean leaveOpen = false)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (progress is null)
                throw new ArgumentNullException(nameof(progress));

            return new SequentialInputByteStreamWithProgression(sourceStream, progress, leaveOpen);
        }

        public static ISequentialOutputByteStream WithProgression(this ISequentialOutputByteStream destinationStream, IProgress<UInt64> progress, Boolean leaveOpen = false)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (progress is null)
                throw new ArgumentNullException(nameof(progress));

            return new SequentialOutputByteStreamWithProgression(destinationStream, progress, leaveOpen);
        }

        #endregion

        #region WithEndAction

        public static ISequentialInputByteStream WithEndAction(this ISequentialInputByteStream sourceStream, Action<UInt64> endAction, Boolean leaveOpen = false)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (endAction is null)
                throw new ArgumentNullException(nameof(endAction));

            return new SequentialInputByteStreamWithEndAction(sourceStream, endAction, leaveOpen);
        }

        public static ISequentialOutputByteStream WithEndAction(this ISequentialOutputByteStream destinationStream, Action<UInt64> endAction, Boolean leaveOpen = false)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (endAction is null)
                throw new ArgumentNullException(nameof(endAction));

            return new SequentialOutputByteStreamWithEndAction(destinationStream, endAction, leaveOpen);
        }

        #endregion

        #region WithCrc32Calculation

        public static ISequentialInputByteStream WithCrc32Calculation(this ISequentialInputByteStream sourceStream, ValueHolder<(UInt32 Crc, UInt64 Length)> resultValueHolder, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (resultValueHolder is null)
                    throw new ArgumentNullException(nameof(resultValueHolder));

                return new SequentialInputByteStreamWithCrc32Calculation(sourceStream, Crc32.CreateCalculationState(), resultValue => resultValueHolder.Value = resultValue, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static ISequentialInputByteStream WithCrc32Calculation(this ISequentialInputByteStream sourceStream, Action<UInt32, UInt64> onCompleted, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (onCompleted is null)
                    throw new ArgumentNullException(nameof(onCompleted));

                return
                    new SequentialInputByteStreamWithCrc32Calculation(
                        sourceStream,
                        Crc32.CreateCalculationState(),
                        resultValue =>
                        {
                            try
                            {
                                onCompleted(resultValue.Crc, resultValue.Length);
                            }
                            catch (Exception)
                            {
                            }
                        },
                        leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static ISequentialOutputByteStream WithCrc32Calculation(this ISequentialOutputByteStream destinationStream, ValueHolder<(UInt32 Crc, UInt64 Length)> resultValueHolder, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));
                if (resultValueHolder is null)
                    throw new ArgumentNullException(nameof(resultValueHolder));

                return new SequentialOutputByteStreamWithCrc32Calculation(destinationStream, Crc32.CreateCalculationState(), resultValue => resultValueHolder.Value = resultValue, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        public static ISequentialOutputByteStream WithCrc32Calculation(this ISequentialOutputByteStream destinationStream, Action<UInt32, UInt64> onCompleted, Boolean leaveOpen = false)
        {
            try
            {
                if (destinationStream is null)
                    throw new ArgumentNullException(nameof(destinationStream));
                if (onCompleted is null)
                    throw new ArgumentNullException(nameof(onCompleted));

                return
                    new SequentialOutputByteStreamWithCrc32Calculation(
                        destinationStream,
                        Crc32.CreateCalculationState(),
                        resultValue =>
                        {
                            try
                            {
                                onCompleted(resultValue.Crc, resultValue.Length);
                            }
                            catch (Exception)
                            {
                            }
                        },
                        leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    destinationStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region Branch

        public static ISequentialOutputByteStream Branch(this ISequentialOutputByteStream baseStream1, ISequentialOutputByteStream baseStream2, Boolean leaveOpen = false)
        {
            if (baseStream1 is null)
                throw new ArgumentNullException(nameof(baseStream1));
            if (baseStream2 is null)
                throw new ArgumentNullException(nameof(baseStream2));

            return new BranchOutputStream(baseStream1, baseStream2, leaveOpen);
        }

        #endregion

        #region GetByteSequence

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetByteSequence(null, null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetByteSequence(null, progress, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, UInt64 offset, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetByteSequence(offset, checked((UInt64)sourceStream.Length - offset), null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetByteSequence(offset, checked((UInt64)sourceStream.Length - offset), progress, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetByteSequence(offset, count, null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetByteSequence(this Stream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetByteSequence(offset, count, progress, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalGetByteSequence(null, null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalGetByteSequence(null, progress, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> ramdomAccessStream)
                    throw new NotSupportedException();
                if (offset > ramdomAccessStream.Length)
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return ramdomAccessStream.InternalGetByteSequence(offset, checked(ramdomAccessStream.Length - offset), null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> ramdomAccessStream)
                    throw new NotSupportedException();
                if (offset > ramdomAccessStream.Length)
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return ramdomAccessStream.InternalGetByteSequence(offset, checked(ramdomAccessStream.Length - offset), progress, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> ramdomAccessStream)
                    throw new NotSupportedException();
                if (checked(offset + count) > ramdomAccessStream.Length)
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return ramdomAccessStream.InternalGetByteSequence(offset, count, null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> ramdomAccessStream)
                    throw new NotSupportedException();
                if (checked(offset + count) > ramdomAccessStream.Length)
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return ramdomAccessStream.InternalGetByteSequence(offset, count, progress, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region GetReverseByteSequence

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetReverseByteSequence(0, checked((UInt64)sourceStream.Length), null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetReverseByteSequence(0, checked((UInt64)sourceStream.Length), progress, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, UInt64 offset, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetReverseByteSequence(offset, checked((UInt64)sourceStream.Length - offset), null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetReverseByteSequence(offset, checked((UInt64)sourceStream.Length - offset), progress, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetReverseByteSequence(offset, count, null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this Stream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanSeek)
                    throw new NotSupportedException();
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalGetReverseByteSequence(offset, count, progress);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                    throw new ArgumentException($"The stream specified by parameter {nameof(sourceStream)} must be a random access stream.", nameof(sourceStream));

                return baseRamdomAccessStream.InternalGetReverseByteSequence(0UL, baseRamdomAccessStream.Length, null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                    throw new NotSupportedException();

                return baseRamdomAccessStream.InternalGetReverseByteSequence(0UL, baseRamdomAccessStream.Length, progress, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                    throw new NotSupportedException();
                if (offset > baseRamdomAccessStream.Length)
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return baseRamdomAccessStream.InternalGetReverseByteSequence(offset, baseRamdomAccessStream.Length - offset, null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                    throw new NotSupportedException();
                if (offset > baseRamdomAccessStream.Length)
                    throw new ArgumentOutOfRangeException(nameof(offset));

                return baseRamdomAccessStream.InternalGetReverseByteSequence(offset, baseRamdomAccessStream.Length - offset, progress, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                    throw new NotSupportedException();
                if (offset < 0)
                    throw new ArgumentOutOfRangeException(nameof(offset));
                if (count < 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (checked(offset + count) > baseRamdomAccessStream.Length)
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return baseRamdomAccessStream.InternalGetReverseByteSequence(offset, count, null, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        public static IEnumerable<Byte> GetReverseByteSequence(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (sourceStream is not IRandomInputByteStream<UInt64> baseRamdomAccessStream)
                    throw new NotSupportedException();
                if (offset < 0)
                    throw new ArgumentOutOfRangeException(nameof(offset));
                if (count < 0)
                    throw new ArgumentOutOfRangeException(nameof(count));
                if (checked(offset + count) > baseRamdomAccessStream.Length)
                    throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceStream)}.");

                return baseRamdomAccessStream.InternalGetReverseByteSequence(offset, count, progress, leaveOpen);
            }
            catch (Exception)
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
                throw;
            }
        }

        #endregion

        #region StreamBytesEqual

        public static Boolean StreamBytesEqual(this Stream stream1, Stream stream2, Boolean leaveOpen = false)
        {
            if (stream1 is null)
                throw new ArgumentNullException(nameof(stream1));
            if (!stream1.CanRead)
                throw new NotSupportedException();
            if (stream2 is null)
                throw new ArgumentNullException(nameof(stream2));
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
            if (stream1 is null)
                throw new ArgumentNullException(nameof(stream1));
            if (!stream1.CanRead)
                throw new NotSupportedException();
            if (stream2 is null)
                throw new ArgumentNullException(nameof(stream2));
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
                if (stream1 is null)
                    throw new ArgumentNullException(nameof(stream1));
                if (stream2 is null)
                    throw new ArgumentNullException(nameof(stream2));

                return InternalStreamBytesEqual(stream1, stream2, null);
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
                if (stream1 is null)
                    throw new ArgumentNullException(nameof(stream1));
                if (stream2 is null)
                    throw new ArgumentNullException(nameof(stream2));

                return InternalStreamBytesEqual(stream1, stream2, progress);
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

        public static void CopyTo(this Stream sourceStream, Stream destinationStream, IProgress<UInt64>? progress = null)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            sourceStream.InternalCopyTo(destinationStream, _COPY_TO_DEFAULT_BUFFER_SIZE, progress);
        }

        public static void CopyTo(this ISequentialInputByteStream sourceStream, ISequentialOutputByteStream destinationStream, IProgress<UInt64>? progress = null)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            sourceStream.InternalCopyTo(destinationStream, _COPY_TO_DEFAULT_BUFFER_SIZE, progress);
        }

        public static void CopyTo(this Stream sourceStream, Stream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress = null)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            sourceStream.InternalCopyTo(destinationStream, bufferSize, progress);
        }

        public static void CopyTo(this ISequentialInputByteStream sourceStream, ISequentialOutputByteStream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress = null)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (bufferSize < 1)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            sourceStream.InternalCopyTo(destinationStream, bufferSize, progress);
        }

        #endregion

        #region Read

        public static Int32 Read(this Stream sourceStream, Byte[] buffer)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            return sourceStream.Read(buffer.AsSpan());
        }

        public static Int32 Read(this Stream sourceStream, Byte[] buffer, Int32 offset)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return sourceStream.Read(buffer.AsSpan(offset));
        }

        public static UInt32 Read(this Stream sourceStream, Byte[] buffer, UInt32 offset)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > (UInt32)buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
#if DEBUG
            if (offset > Int32.MaxValue)
                throw new Exception();
#endif

            var length = sourceStream.Read(buffer.AsSpan(offset));
            return checked((UInt32)length);
        }

        public static Int32 Read(this Stream sourceStream, Byte[] buffer, Range range)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            return sourceStream.Read(buffer.AsSpan(offset, count));
        }

#if false
        public static Int32 Read(this Stream sourceStream, Byte[] buffer, Int32 offset, Int32 count)
        {
            throw new NotImplementedException(); // defined in System.IO.Stream.Read(Byte[], Int32, Int32)
        }
#endif

        public static UInt32 Read(this Stream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count) > (UInt32)buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            var length = sourceStream.Read(buffer.AsSpan(offset, count));
            return checked((UInt32)length);
        }

        public static Int32 Read(this Stream sourceStream, Memory<Byte> buffer)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.Read(buffer.Span);
        }

        public static Int32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            return sourceStream.Read(buffer.AsSpan());
        }

        public static Int32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return sourceStream.Read(buffer.AsSpan(offset));
        }

        public static UInt32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var length = sourceStream.Read(buffer.AsSpan(offset));
            return checked((UInt32)length);
        }

        public static Int32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer, Range range)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            return sourceStream.Read(buffer.AsSpan(offset, count));
        }

        public static Int32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, Int32 count)
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

            return sourceStream.Read(buffer.AsSpan(offset, count));
        }

        public static UInt32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            var length = sourceStream.Read(buffer.AsSpan(offset, count));
            return checked((UInt32)length);
        }

        public static Int32 Read(this ISequentialInputByteStream sourceStream, Memory<Byte> buffer)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            return sourceStream.Read(buffer.Span);
        }

        #endregion

        #region ReadByteOrNull

        public static Byte? ReadByteOrNull(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[1];
            return
                sourceStream.Read(buffer) > 0
                ? buffer[0]
                : null;
        }

        public static Byte? ReadByteOrNull(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[1];
            return
                sourceStream.Read(buffer) > 0
                ? buffer[0]
                : null;
        }

        #endregion

        #region ReadByte

        public static Byte ReadByte(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[1];
            if (sourceStream.Read(buffer) <= 0)
                throw new UnexpectedEndOfStreamException();

            return buffer[0];
        }

        #endregion

        #region ReadBytes

        public static ReadOnlyMemory<Byte> ReadBytes(this Stream sourceStream, Int32 count)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            return sourceStream.InternalReadBytes(checked((Int32)count));
        }

        public static ReadOnlyMemory<Byte> ReadBytes(this Stream sourceStream, UInt32 count)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.InternalReadBytes(checked((Int32)count));
        }

        public static Int32 ReadBytes(this Stream sourceStream, Byte[] buffer)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            return sourceStream.InternalReadBytes(buffer);
        }

        public static Int32 ReadBytes(this Stream sourceStream, Byte[] buffer, Int32 offset)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return sourceStream.InternalReadBytes(buffer.AsSpan(offset));
        }

        public static UInt32 ReadBytes(this Stream sourceStream, Byte[] buffer, UInt32 offset)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > (UInt32)buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var length = sourceStream.InternalReadBytes(buffer.AsSpan(offset));
            return checked((UInt32)length);
        }

        public static Int32 ReadBytes(this Stream sourceStream, Byte[] buffer, Range range)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            return sourceStream.InternalReadBytes(buffer.AsSpan(offset, count));
        }

        public static Int32 ReadBytes(this Stream sourceStream, Byte[] buffer, Int32 offset, Int32 count)
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
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            return sourceStream.InternalReadBytes(buffer.AsSpan(offset, count));
        }

        public static UInt32 ReadBytes(this Stream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset) + count > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            var length = sourceStream.InternalReadBytes(buffer.AsSpan(offset, count));
            return checked((UInt32)length);
        }

        public static Int32 ReadBytes(this Stream sourceStream, Memory<Byte> buffer)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.InternalReadBytes(buffer.Span);
        }

        public static Int32 ReadBytes(this Stream sourceStream, Span<Byte> buffer)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.InternalReadBytes(buffer);
        }

        public static ReadOnlyMemory<Byte> ReadBytes(this ISequentialInputByteStream sourceStream, Int32 count)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            return sourceStream.InternalReadBytes(count);
        }

        public static ReadOnlyMemory<Byte> ReadBytes(this ISequentialInputByteStream sourceStream, UInt32 count)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            return sourceStream.InternalReadBytes(checked((Int32)count));
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            return sourceStream.InternalReadBytes(buffer);
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return sourceStream.InternalReadBytes(buffer.AsSpan(offset));
        }

        public static UInt32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > (UInt32)buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var length = sourceStream.InternalReadBytes(buffer.AsSpan(offset));
            return checked((UInt32)length);
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer, Range range)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            return sourceStream.InternalReadBytes(buffer.AsSpan(offset, count));
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, Int32 count)
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

            return sourceStream.InternalReadBytes(buffer.AsSpan(offset, count));
        }

        public static UInt32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count) > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            var length = sourceStream.InternalReadBytes(buffer.AsSpan(offset, count));
            return checked((UInt32)length);
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Memory<Byte> buffer)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            return sourceStream.InternalReadBytes(buffer.Span);
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Span<Byte> buffer)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            return sourceStream.InternalReadBytes(buffer);
        }

        #endregion

        #region ReadAllBytes

        public static ReadOnlyMemory<Byte> ReadAllBytes(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.InternalReadAllBytes();
        }

        public static ReadOnlyMemory<Byte> ReadAllBytes(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            return sourceStream.InternalReadAllBytes();
        }

        #endregion

        #region ReadInt16LE

        public static Int16 ReadInt16LE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt16LE();
        }

        public static Int16 ReadInt16LE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt16LE();
        }

        #endregion

        #region ReadUInt16LE

        public static UInt16 ReadUInt16LE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt16LE();
        }

        public static UInt16 ReadUInt16LE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt16LE();
        }

        #endregion

        #region ReadInt32LE

        public static Int32 ReadInt32LE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt32LE();
        }

        public static Int32 ReadInt32LE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt32LE();
        }

        #endregion

        #region ReadUInt32LE

        public static UInt32 ReadUInt32LE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return
                buffer.ToUInt32LE();
        }

        public static UInt32 ReadUInt32LE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt32LE();
        }

        #endregion

        #region ReadInt64LE

        public static Int64 ReadInt64LE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt64LE();
        }

        public static Int64 ReadInt64LE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt64LE();
        }

        #endregion

        #region ReadUInt64LE

        public static UInt64 ReadUInt64LE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt64LE();
        }

        public static UInt64 ReadUInt64LE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt64LE();
        }

        #endregion

        #region ReadSingleLE

        public static Single ReadSingleLE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToSingleLE();
        }

        public static Single ReadSingleLE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToSingleLE();
        }

        #endregion

        #region ReadDoubleLE

        public static Double ReadDoubleLE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDoubleLE();
        }

        public static Double ReadDoubleLE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDoubleLE();
        }

        #endregion

        #region ReadDecimalLE

        public static Decimal ReadDecimalLE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDecimalLE();
        }

        public static Decimal ReadDecimalLE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDecimalLE();
        }

        #endregion

        #region ReadInt16BE

        public static Int16 ReadInt16BE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt16BE();
        }

        public static Int16 ReadInt16BE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt16BE();
        }

        #endregion

        #region ReadUInt16BE

        public static UInt16 ReadUInt16BE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt16BE();
        }

        public static UInt16 ReadUInt16BE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt16BE();
        }

        #endregion

        #region ReadInt32BE

        public static Int32 ReadInt32BE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt32BE();
        }

        public static Int32 ReadInt32BE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt32BE();
        }

        #endregion

        #region ReadUInt32BE

        public static UInt32 ReadUInt32BE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt32BE();
        }

        public static UInt32 ReadUInt32BE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt32BE();
        }

        #endregion

        #region ReadInt64BE

        public static Int64 ReadInt64BE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt64BE();
        }

        public static Int64 ReadInt64BE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToInt64BE();
        }

        #endregion

        #region ReadUInt64BE

        public static UInt64 ReadUInt64BE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt64BE();
        }

        public static UInt64 ReadUInt64BE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToUInt64BE();
        }

        #endregion

        #region ReadSingleBE

        public static Single ReadSingleBE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToSingleBE();
        }

        public static Single ReadSingleBE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToSingleBE();
        }

        #endregion

        #region ReadDoubleBE

        public static Double ReadDoubleBE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDoubleBE();
        }

        public static Double ReadDoubleBE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDoubleBE();
        }

        #endregion

        #region ReadDecimalBE

        public static Decimal ReadDecimalBE(this Stream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDecimalBE();
        }

        public static Decimal ReadDecimalBE(this ISequentialInputByteStream sourceStream)
        {
            if (sourceStream is null)
                throw new ArgumentNullException(nameof(sourceStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            if (sourceStream.InternalReadBytes(buffer) != buffer.Length)
                throw new UnexpectedEndOfStreamException();

            return buffer.ToDecimalBE();
        }

        #endregion

        #region Write

#if false
        public static Int32 Write(this Stream destinationStream, Byte[] buffer)
        {
            throw new NotImplementedException(); // equivalent to System.IO.Stream.Write(ReadOnlyMemory<Byte>)
        }
#endif

        public static Int32 Write(this Stream destinationStream, Byte[] buffer, Int32 offset)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            var bufferSpan = buffer.AsReadOnlySpan(offset);
            destinationStream.Write(bufferSpan);
            return bufferSpan.Length;
        }

        public static UInt32 Write(this Stream destinationStream, Byte[] buffer, UInt32 offset)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
#if DEBUG
            if (offset > Int32.MaxValue)
                throw new Exception();
#endif

            var bufferSpan = buffer.AsReadOnlySpan(offset);
            destinationStream.Write(bufferSpan);
            return checked((UInt32)bufferSpan.Length);
        }

#if false

        public static Int32 Write(this Stream destinationStream, Byte[] buffer, Int32 offset, Int32 count)
        {
            throw new NotImplementedException(); // defined in System.IO.Stream.Write(Byte[], Int32, Int32)
        }
#endif

        public static UInt32 Write(this Stream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count) > (UInt32)buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");
#if DEBUG
            if (offset > Int32.MaxValue)
                throw new Exception();
            if (count > Int32.MaxValue)
                throw new Exception();
#endif

            destinationStream.Write(buffer.AsReadOnlySpan(offset, count));
            return count;
        }

        public static Int32 Write(this Stream destinationStream, ReadOnlyMemory<Byte> buffer)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            destinationStream.Write(buffer.Span);
            return buffer.Length;
        }

        public static Int32 Write(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return destinationStream.Write(buffer.AsReadOnlySpan(offset));
        }

        public static UInt32 Write(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            var length = destinationStream.Write(buffer.AsReadOnlySpan(offset));
            return checked((UInt32)length);
        }

        public static Int32 Write(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, Int32 count)
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

            return destinationStream.Write(buffer.AsReadOnlySpan(offset, count));
        }

        public static UInt32 Write(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count) > (UInt32)buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            var length = destinationStream.Write(buffer.AsReadOnlySpan(offset, count));
            return checked((UInt32)length);
        }

        public static Int32 Write(this ISequentialOutputByteStream destinationStream, ReadOnlyMemory<Byte> buffer)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            return destinationStream.Write(buffer.Span);
        }

        #endregion

        #region WriteByte

#if false
        public static void WriteByte(this Stream destinationStream, Byte value)
        {
            throw new NotImplementedException(); // defined in System.IO.Stream.WriteByte(Byte)
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteByte(this ISequentialOutputByteStream destinationStream, Byte value)
        {
            Span<Byte> buffer = stackalloc Byte[1];
            buffer[0] = value;
            var length = destinationStream.Write(buffer);
            Validation.Assert(length > 0, "length > 0");
        }

        #endregion

        #region WriteBytes

        public static void WriteBytes(this Stream destinationStream, Byte[] buffer)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteBytes(this Stream destinationStream, Byte[] buffer, Int32 offset)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(offset));
        }

        public static void WriteBytes(this Stream destinationStream, Byte[] buffer, UInt32 offset)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
#if DEBUG
            if (offset > Int32.MaxValue)
                throw new Exception();
#endif

            destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(offset));
        }

        public static void WriteBytes(this Stream destinationStream, Byte[] buffer, Range range)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(offset, count));
        }

        public static void WriteBytes(this Stream destinationStream, Byte[] buffer, Int32 offset, Int32 count)
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

            destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(offset, count));
        }

        public static void WriteBytes(this Stream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count) > (UInt32)buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");
#if DEBUG
            if (offset > Int32.MaxValue)
                throw new Exception();
            if (count > Int32.MaxValue)
                throw new Exception();
#endif

            destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(offset, count));
        }

        public static void WriteBytes(this Stream destinationStream, ReadOnlyMemory<Byte> buffer)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            destinationStream.InternalWriteBytes(buffer.Span);
        }

        public static void WriteBytes(this Stream destinationStream, ReadOnlySpan<Byte> buffer)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteBytes(this Stream destinationStream, IEnumerable<ReadOnlyMemory<Byte>> buffers)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            if (buffers is null)
                throw new ArgumentNullException(nameof(buffers));

            foreach (var buffer in buffers)
                destinationStream.Write(buffer.Span);
        }

        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));

            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (!offset.IsBetween(0, buffer.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(offset));
        }

        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset > (UInt32)buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
#if DEBUG
            if (offset > Int32.MaxValue)
                throw new Exception();
#endif

            destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(offset));
        }

        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Range range)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var (offset, count) = buffer.GetOffsetAndLength(range, nameof(range));
            destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(offset, count));
        }

        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, Int32 count)
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

            destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(offset, count));
        }

        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer));
            if (checked(offset + count) > (UInt32)buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");
#if DEBUG
            if (offset > Int32.MaxValue)
                throw new Exception();
            if (count > Int32.MaxValue)
                throw new Exception();
#endif

            destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(offset, count));
        }

        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, ReadOnlyMemory<Byte> buffer)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            var spanOfBuffer = buffer.Span;
            while (!spanOfBuffer.IsEmpty)
            {
                var length = destinationStream.Write(spanOfBuffer);
                if (length <= 0)
                    throw new IOException("Can not write any more");
                spanOfBuffer = spanOfBuffer[length..];
            }
        }

        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, ReadOnlySpan<Byte> buffer)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, IEnumerable<ReadOnlyMemory<Byte>> buffers)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (buffers is null)
                throw new ArgumentNullException(nameof(buffers));

            foreach (var buffer in buffers)
                destinationStream.InternalWriteBytes(buffer.Span);
        }

        #endregion

        #region WriteByteSequence

        public static void WriteByteSequence(this Stream destinationStream, IEnumerable<Byte> sequence)
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
                    destinationStream.Write(buffer, 0, index);
            }
        }

        public static void WriteByteSequence(this ISequentialOutputByteStream destinationStream, IEnumerable<Byte> sequence)
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
                    destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(0, index));
            }
        }

        #endregion

        #region WriteInt16LE

        public static void WriteInt16LE(this Stream destinationStream, Int16 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteInt16LE(this ISequentialOutputByteStream destinationStream, Int16 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteUInt16LE

        public static void WriteUInt16LE(this Stream destinationStream, UInt16 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteUInt16LE(this ISequentialOutputByteStream destinationStream, UInt16 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteInt32LE

        public static void WriteInt32LE(this Stream destinationStream, Int32 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteInt32LE(this ISequentialOutputByteStream destinationStream, Int32 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteUInt32LE

        public static void WriteUInt32LE(this Stream destinationStream, UInt32 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteUInt32LE(this ISequentialOutputByteStream destinationStream, UInt32 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteInt64LE

        public static void WriteInt64LE(this Stream destinationStream, Int64 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteInt64LE(this ISequentialOutputByteStream destinationStream, Int64 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteUInt64LE

        public static void WriteUInt64LE(this Stream destinationStream, UInt64 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteUInt64LE(this ISequentialOutputByteStream destinationStream, UInt64 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteSingleLE

        public static void WriteSingleLE(this Stream destinationStream, Single value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteSingleLE(this ISequentialOutputByteStream destinationStream, Single value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteDoubleLE

        public static void WriteDoubleLE(this Stream destinationStream, Double value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteDoubleLE(this ISequentialOutputByteStream destinationStream, Double value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteDecimalLE

        public static void WriteDecimalLE(this Stream destinationStream, Decimal value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteDecimalLE(this ISequentialOutputByteStream destinationStream, Decimal value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            buffer.SetValueLE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteInt16BE

        public static void WriteInt16BE(this Stream destinationStream, Int16 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteInt16BE(this ISequentialOutputByteStream destinationStream, Int16 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int16)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteUInt16BE

        public static void WriteUInt16BE(this Stream destinationStream, UInt16 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteUInt16BE(this ISequentialOutputByteStream destinationStream, UInt16 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt16)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteInt32BE

        public static void WriteInt32BE(this Stream destinationStream, Int32 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteInt32BE(this ISequentialOutputByteStream destinationStream, Int32 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int32)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteUInt32BE

        public static void WriteUInt32BE(this Stream destinationStream, UInt32 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteUInt32BE(this ISequentialOutputByteStream destinationStream, UInt32 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt32)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteInt64BE

        public static void WriteInt64BE(this Stream destinationStream, Int64 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteInt64BE(this ISequentialOutputByteStream destinationStream, Int64 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Int64)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteUInt64BE

        public static void WriteUInt64BE(this Stream destinationStream, UInt64 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteUInt64BE(this ISequentialOutputByteStream destinationStream, UInt64 value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(UInt64)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteSingleBE

        public static void WriteSingleBE(this Stream destinationStream, Single value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteSingleBE(this ISequentialOutputByteStream destinationStream, Single value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Single)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteDoubleBE

        public static void WriteDoubleBE(this Stream destinationStream, Double value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteDoubleBE(this ISequentialOutputByteStream destinationStream, Double value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Double)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region WriteDecimalBE

        public static void WriteDecimalBE(this Stream destinationStream, Decimal value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        public static void WriteDecimalBE(this ISequentialOutputByteStream destinationStream, Decimal value)
        {
            if (destinationStream is null)
                throw new ArgumentNullException(nameof(destinationStream));

            Span<Byte> buffer = stackalloc Byte[sizeof(Decimal)];
            buffer.SetValueBE(value);
            destinationStream.InternalWriteBytes(buffer);
        }

        #endregion

        #region CalculateCrc24

        public static (UInt32 Crc, UInt64 Length) CalculateCrc24(this Stream sourceStream, Boolean leaveOpen = false)
        {
            const Int32 MAX_BUFFER_SIZE = 80 * 1024;

            try
            {
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalCalculateCrc24(MAX_BUFFER_SIZE, null);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalCalculateCrc24(MAX_BUFFER_SIZE, progress);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalCalculateCrc24(bufferSize, null);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalCalculateCrc24(bufferSize, progress);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalCalculateCrc24(MAX_BUFFER_SIZE, null);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalCalculateCrc24(MAX_BUFFER_SIZE, progress);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalCalculateCrc24(bufferSize, null);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalCalculateCrc24(bufferSize, progress);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalCalculateCrc32(MAX_BUFFER_SIZE, null);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalCalculateCrc32(MAX_BUFFER_SIZE, progress);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalCalculateCrc32(bufferSize, null);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));
                if (!sourceStream.CanRead)
                    throw new NotSupportedException();

                return sourceStream.InternalCalculateCrc32(bufferSize, progress);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalCalculateCrc32(MAX_BUFFER_SIZE, null);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalCalculateCrc32(MAX_BUFFER_SIZE, progress);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalCalculateCrc32(bufferSize, null);
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
                if (sourceStream is null)
                    throw new ArgumentNullException(nameof(sourceStream));

                return sourceStream.InternalCalculateCrc32(bufferSize, progress);
            }
            finally
            {
                if (!leaveOpen)
                    sourceStream?.Dispose();
            }
        }

        #endregion

        #region InternalGetByteSequence

        private static IEnumerable<Byte> InternalGetByteSequence(this Stream sourceStream, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen)
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
                    var length = sourceStream.Read(buffer, 0, readCount);
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
            }
        }

        private static IEnumerable<Byte> InternalGetByteSequence(this Stream sourceStream, UInt64 offset, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen)
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

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
            }
        }

        private static IEnumerable<Byte> InternalGetByteSequence(this ISequentialInputByteStream sourceStream, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen)
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
            }
        }

        private static IEnumerable<Byte> InternalGetByteSequence<POSITION_T>(this ISequentialInputByteStream sourceStream, POSITION_T offset, UInt64? count, IProgress<UInt64>? progress, Boolean leaveOpen)
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

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
            }
        }

        #endregion

        #region InternalGetReverseByteSequence

        private static IEnumerable<Byte> InternalGetReverseByteSequence(this Stream sourceStream, UInt64 offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

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
                    var length = sourceStream.InternalReadBytes(buffer);
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
                    var length = sourceStream.InternalReadBytes(buffer.AsSpan(0, remain));
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

        private static IEnumerable<Byte> InternalGetReverseByteSequence<POSITION_T>(this IRandomInputByteStream<POSITION_T> sourceStream, POSITION_T offset, UInt64 count, IProgress<UInt64>? progress, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            const Int32 BUFFER_SIZE = 8 * 1024;

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
                    var length = sourceStream.InternalReadBytes(buffer);
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
                    var length = sourceStream.InternalReadBytes(buffer.AsSpan(0, remain));
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

        #region InternalStreamBytesEqual

        private static Boolean InternalStreamBytesEqual(this Stream stream1, Stream stream2, IProgress<UInt64>? progress)
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
                    var bufferCount1 = stream1.InternalReadBytes(buffer1);
                    var bufferCount2 = stream2.InternalReadBytes(buffer2);
                    processedCounter.AddValue((UInt32)bufferCount1);

                    if (bufferCount1 != bufferCount2)
                    {
                        // 実際に読み込めたサイズが異なっている場合はどちらかだけがEOFに達したということなので、ストリームの内容が異なると判断しfalseを返す。
                        return false;
                    }

                    // この時点で bufferCount1 == bufferCount2 (どちらのストリームも読み込めたサイズは同じ)

                    if (!buffer1.SequenceEqual(0, buffer2, 0, bufferCount1))
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

        private static Boolean InternalStreamBytesEqual(this ISequentialInputByteStream stream1, ISequentialInputByteStream stream2, IProgress<UInt64>? progress)
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
                    var bufferCount1 = stream1.InternalReadBytes(buffer1);
                    var bufferCount2 = stream2.InternalReadBytes(buffer2);
                    processedCounter.AddValue((UInt32)bufferCount1);

                    if (bufferCount1 != bufferCount2)
                    {
                        // 実際に読み込めたサイズが異なっている場合はどちらかだけがEOFに達したということなので、ストリームの内容が異なると判断しfalseを返す。
                        return false;
                    }

                    // この時点で bufferCount1 == bufferCount2 (どちらのストリームも読み込めたサイズは同じ)

                    if (!buffer1.SequenceEqual(0, buffer2, 0, bufferCount1))
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

        #region InternalCopyTo

        private static void InternalCopyTo(this Stream sourceStream, Stream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = new Byte[bufferSize];
            try
            {
                while (true)
                {
                    var length = sourceStream.InternalReadBytes(buffer);
                    if (length <= 0)
                        break;
                    destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(0, length));
                    processedCounter.AddValue(checked((UInt64)length));
                }

                destinationStream.Flush();
            }
            finally
            {
                processedCounter.Report();
            }
        }

        private static void InternalCopyTo(this ISequentialInputByteStream sourceStream, ISequentialOutputByteStream destinationStream, Int32 bufferSize, IProgress<UInt64>? progress)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = new Byte[bufferSize];
            try
            {
                while (true)
                {
                    var length = sourceStream.InternalReadBytes(buffer);
                    if (length <= 0)
                        break;
                    destinationStream.InternalWriteBytes(buffer.AsReadOnlySpan(0, length));
                    processedCounter.AddValue(checked((UInt64)length));
                }

                destinationStream.Flush();
            }
            finally
            {
                processedCounter.Report();
            }
        }

        #endregion

        #region InternalReadBytes

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ReadOnlyMemory<Byte> InternalReadBytes(this Stream sourceStream, Int32 count)
        {
            var buffer = new Byte[count];
            var length = sourceStream.InternalReadBytes(buffer);
            if (length < buffer.Length)
                Array.Resize(ref buffer, length);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ReadOnlyMemory<Byte> InternalReadBytes(this ISequentialInputByteStream sourceStream, Int32 count)
        {
            var buffer = new Byte[count];
            var length = sourceStream.InternalReadBytes(buffer);
            if (length < buffer.Length)
                Array.Resize(ref buffer, length);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 InternalReadBytes(this Stream sourceStream, Span<Byte> buffer)
        {
            var totalLength = 0;
            while (!buffer.IsEmpty)
            {
                var length = sourceStream.Read(buffer);
                if (length <= 0)
                    break;
                buffer = buffer[length..];
                totalLength += length;
            }

            return totalLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 InternalReadBytes(this ISequentialInputByteStream sourceStream, Span<Byte> buffer)
        {
            var totalLength = 0;
            while (!buffer.IsEmpty)
            {
                var length = sourceStream.Read(buffer);
                if (length <= 0)
                    break;
                buffer = buffer[length..];
                totalLength += length;
            }

            return totalLength;
        }

        #endregion

        #region InternalReadAllBytes

        private static ReadOnlyMemory<Byte> InternalReadAllBytes(this Stream sourceStream)
        {
            const Int32 BUFFER_SIZE = 80 * 1024;
            var buffers = new Queue<Byte[]>();
            var dataLength = 0;
            while (true)
            {
                var partialBuffer = new Byte[BUFFER_SIZE];
                var length = sourceStream.Read(partialBuffer);
                if (length <= 0)
                    break;
                if (length < partialBuffer.Length)
                    Array.Resize(ref partialBuffer, length);
                buffers.Enqueue(partialBuffer);
                dataLength += length;
            }

            return ConcatBuffer(buffers, dataLength);
        }

        private static ReadOnlyMemory<Byte> InternalReadAllBytes(this ISequentialInputByteStream sourceStream)
        {
            const Int32 BUFFER_SIZE = 80 * 1024;
            var buffers = new Queue<Byte[]>();
            var dataLength = 0;
            while (true)
            {
                var partialBuffer = new Byte[BUFFER_SIZE];
                var length = sourceStream.Read(partialBuffer);
                if (length <= 0)
                    break;
                if (length < partialBuffer.Length)
                    Array.Resize(ref partialBuffer, length);
                buffers.Enqueue(partialBuffer);
                dataLength += length;
            }

            return ConcatBuffer(buffers, dataLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ReadOnlyMemory<Byte> ConcatBuffer(Queue<Byte[]> buffers, Int32 totalLength)
        {
            if (buffers.Count <= 0)
            {
                return ReadOnlyMemory<Byte>.Empty;
            }
            else if (buffers.Count == 1)
            {
                return buffers.Dequeue();
            }
            else
            {
                var buffer = new Byte[totalLength];
                var destinationWindow = buffer.AsMemory();
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
        }

        #endregion

        #region InternalWriteBytes

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalWriteBytes(this Stream destinationStream, ReadOnlySpan<Byte> buffer)
            => destinationStream.Write(buffer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalWriteBytes(this ISequentialOutputByteStream destinationStream, ReadOnlySpan<Byte> buffer)
        {
            while (!buffer.IsEmpty)
            {
                var length = destinationStream.Write(buffer);
                if (length <= 0)
                    throw new IOException("Can not write any more");
                buffer = buffer[length..];
            }
        }

        #endregion

        #region InternalCalculateCrc24

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (UInt32 Crc, UInt64 Length) InternalCalculateCrc24(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress)
            => InternalCalculateCrc(sourceStream, bufferSize, progress, Crc24.CreateCalculationState());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (UInt32 Crc, UInt64 Length) InternalCalculateCrc24(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress)
            => InternalCalculateCrc(sourceStream, bufferSize, progress, Crc24.CreateCalculationState());

        #endregion

        #region InternalCalculateCrc32

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (UInt32 Crc, UInt64 Length) InternalCalculateCrc32(this Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress)
            => InternalCalculateCrc(sourceStream, bufferSize, progress, Crc32.CreateCalculationState());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (UInt32 Crc, UInt64 Length) InternalCalculateCrc32(this ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress)
            => InternalCalculateCrc(sourceStream, bufferSize, progress, Crc32.CreateCalculationState());

        #endregion

        #region InternalCalculateCrc

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (UInt32 Crc, UInt64 Length) InternalCalculateCrc(Stream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, ICrcCalculationState<UInt32> session)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = new Byte[bufferSize];
            try
            {
                while (true)
                {
                    var length = sourceStream.Read(buffer);
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
        private static (UInt32 Crc, UInt64 Length) InternalCalculateCrc(ISequentialInputByteStream sourceStream, Int32 bufferSize, IProgress<UInt64>? progress, ICrcCalculationState<UInt32> session)
        {
            var processedCounter = new ProgressCounterUInt64(progress);
            processedCounter.Report();
            var buffer = new Byte[bufferSize];
            try
            {
                while (true)
                {
                    var length = sourceStream.Read(buffer);
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
