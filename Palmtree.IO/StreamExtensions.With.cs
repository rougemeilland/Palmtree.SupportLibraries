using System;
using System.IO;
using System.Numerics;
using Palmtree.IO.StreamFilters;

namespace Palmtree.IO
{
    public static partial class StreamExtensions
    {
        #region WithPartial

        public static ISequentialInputByteStream WithPartial(this ISequentialInputByteStream sourceStream, UInt64 size, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return
                sourceStream switch
                {
                    IRandomInputByteStream<UInt64> randomAccessStream
                        => new PartialRandomInputStream<UInt64, UInt64>(randomAccessStream, size, 0UL, leaveOpen),
                    _
                        => new PartialSequentialInputStream(sourceStream, size, leaveOpen),
                };
        }

        public static ISequentialInputByteStream WithPartial(this ISequentialInputByteStream sourceStream, UInt64 offset, UInt64? size, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return
                sourceStream switch
                {
                    IRandomInputByteStream<UInt64> randomAccessStream
                        => new PartialRandomInputStream<UInt64, UInt64>(randomAccessStream, offset, size, 0UL, leaveOpen),
                    _
                        => throw new ArgumentException($"Stream object {nameof(sourceStream)} does not support interface {nameof(IRandomInputByteStream<UInt64>)}.", nameof(sourceStream))
                };
        }

        public static IRandomInputByteStream<UInt64> WithPartial<BASE_POSITION_T>(this IRandomInputByteStream<BASE_POSITION_T> sourceStream, UInt64? size, Boolean leaveOpen = false)
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return new PartialRandomInputStream<UInt64, BASE_POSITION_T>(sourceStream, size, 0UL, leaveOpen);
        }

        public static IRandomInputByteStream<UInt64> WithPartial<BASE_POSITION_T>(this IRandomInputByteStream<BASE_POSITION_T> sourceStream, BASE_POSITION_T offset, UInt64? size, Boolean leaveOpen = false)
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return new PartialRandomInputStream<UInt64, BASE_POSITION_T>(sourceStream, offset, size, 0UL, leaveOpen);
        }

        public static IRandomInputByteStream<POSITION_T> WithPartial<POSITION_T, BASE_POSITION_T>(this IRandomInputByteStream<BASE_POSITION_T> sourceStream, UInt64? size, POSITION_T zeroPositionValue, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return new PartialRandomInputStream<POSITION_T, BASE_POSITION_T>(sourceStream, size, zeroPositionValue, leaveOpen);
        }

        public static IRandomInputByteStream<POSITION_T> WithPartial<POSITION_T, BASE_POSITION_T>(this IRandomInputByteStream<BASE_POSITION_T> sourceStream, BASE_POSITION_T offset, UInt64? size, POSITION_T zeroPositionValue, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return new PartialRandomInputStream<POSITION_T, BASE_POSITION_T>(sourceStream, offset, size, zeroPositionValue, leaveOpen);
        }

        public static ISequentialOutputByteStream WithPartial(this ISequentialOutputByteStream destinationStream, UInt64 size, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return
                destinationStream switch
                {
                    IRandomOutputByteStream<UInt64> randomAccessStream
                        => new PartialRandomOutputStream<UInt64, UInt64>(randomAccessStream, size, 0UL, leaveOpen),
                    _
                        => new PartialSequentialOutputStream(destinationStream, size, leaveOpen),
                };
        }

        public static ISequentialOutputByteStream WithPartial(this ISequentialOutputByteStream destinationStream, UInt64 offset, UInt64? size, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return
                destinationStream switch
                {
                    IRandomOutputByteStream<UInt64> randomAccessStream
                        => new PartialRandomOutputStream<UInt64, UInt64>(randomAccessStream, offset, size, 0UL, leaveOpen),
                    _
                        => throw new ArgumentException($"Stream object {nameof(destinationStream)} does not support interface {nameof(IRandomOutputByteStream<UInt64>)}.", nameof(destinationStream))
                };
        }

        public static IRandomOutputByteStream<UInt64> WithPartial<BASE_POSITION_T>(this IRandomOutputByteStream<BASE_POSITION_T> destinationStream, UInt64 size, Boolean leaveOpen = false)
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return new PartialRandomOutputStream<UInt64, BASE_POSITION_T>(destinationStream, size, 0UL, leaveOpen);
        }

        public static IRandomOutputByteStream<UInt64> WithPartial<BASE_POSITION_T>(this IRandomOutputByteStream<BASE_POSITION_T> destinationStream, BASE_POSITION_T offset, UInt64? size, Boolean leaveOpen = false)
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return new PartialRandomOutputStream<UInt64, BASE_POSITION_T>(destinationStream, offset, size, 0UL, leaveOpen);
        }

        public static IRandomOutputByteStream<POSITION_T> WithPartial<POSITION_T, BASE_POSITION_T>(this IRandomOutputByteStream<BASE_POSITION_T> destinationStream, UInt64 size, POSITION_T zeroPositionValue, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return new PartialRandomOutputStream<POSITION_T, BASE_POSITION_T>(destinationStream, size, zeroPositionValue, leaveOpen);
        }

        public static IRandomOutputByteStream<POSITION_T> WithPartial<POSITION_T, BASE_POSITION_T>(this IRandomOutputByteStream<BASE_POSITION_T> destinationStream, BASE_POSITION_T offset, UInt64? size, POSITION_T zeroPositionValue, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
            where BASE_POSITION_T : struct, IComparable<BASE_POSITION_T>, IAdditionOperators<BASE_POSITION_T, UInt64, BASE_POSITION_T>, ISubtractionOperators<BASE_POSITION_T, BASE_POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return new PartialRandomOutputStream<POSITION_T, BASE_POSITION_T>(destinationStream, offset, size, zeroPositionValue, leaveOpen);
        }

        #endregion

        #region WithCache

        public static ISequentialInputByteStream WithCache(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return
                sourceStream switch
                {
                    IRandomInputByteStream<UInt64> baseRandomAccessStream
                        => new BufferedRandomInputStream<UInt64>(baseRandomAccessStream, leaveOpen),
                    _
                        => new BufferedSequentialInputStream(sourceStream, leaveOpen),
                };
        }

        public static ISequentialInputByteStream WithCache(this ISequentialInputByteStream sourceStream, Int32 cacheSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cacheSize);

            return
                sourceStream switch
                {
                    IRandomInputByteStream<UInt64> baseRandomAccessStream
                        => new BufferedRandomInputStream<UInt64>(baseRandomAccessStream, cacheSize, leaveOpen),
                    _
                        => new BufferedSequentialInputStream(sourceStream, cacheSize, leaveOpen),
                };
        }

        public static IRandomInputByteStream<POSITION_T> WithCache<POSITION_T>(this IRandomInputByteStream<POSITION_T> sourceStream, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return new BufferedRandomInputStream<POSITION_T>(sourceStream, leaveOpen);
        }

        public static IRandomInputByteStream<POSITION_T> WithCache<POSITION_T>(this IRandomInputByteStream<POSITION_T> sourceStream, Int32 cacheSize, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cacheSize);

            return new BufferedRandomInputStream<POSITION_T>(sourceStream, cacheSize, leaveOpen);
        }

        public static ISequentialOutputByteStream WithCache(this ISequentialOutputByteStream destinationStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return
                destinationStream switch
                {
                    IRandomOutputByteStream<UInt64> baseRandomAccessStream
                        => new BufferedRandomOutputStream<UInt64>(baseRandomAccessStream, leaveOpen),
                    _
                        => new BufferedSequentialOutputStream(destinationStream, leaveOpen)
                };
        }

        public static ISequentialOutputByteStream WithCache(this ISequentialOutputByteStream destinationStream, Int32 cacheSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cacheSize);

            return
                destinationStream switch
                {
                    IRandomOutputByteStream<UInt64> baseRandomAccessStream
                        => new BufferedRandomOutputStream<UInt64>(baseRandomAccessStream, cacheSize, leaveOpen),
                    _
                        => new BufferedSequentialOutputStream(destinationStream, cacheSize, leaveOpen)
                };
        }

        public static IRandomOutputByteStream<POSITION_T> WithCache<POSITION_T>(this IRandomOutputByteStream<POSITION_T> destinationStream, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return new BufferedRandomOutputStream<POSITION_T>(destinationStream, leaveOpen);
        }

        public static IRandomOutputByteStream<POSITION_T> WithCache<POSITION_T>(this IRandomOutputByteStream<POSITION_T> destinationStream, Int32 cacheSize, Boolean leaveOpen = false)
            where POSITION_T : struct, IComparable<POSITION_T>, IAdditionOperators<POSITION_T, UInt64, POSITION_T>, ISubtractionOperators<POSITION_T, POSITION_T, UInt64>
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(cacheSize);

            return new BufferedRandomOutputStream<POSITION_T>(destinationStream, cacheSize, leaveOpen);
        }

        #endregion

        #region WithProgression

        public static ISequentialInputByteStream WithProgression(this ISequentialInputByteStream sourceStream, IProgress<UInt64> progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(progress);

            return new SequentialInputByteStreamWithProgression(sourceStream, progress, leaveOpen);
        }

        public static ISequentialOutputByteStream WithProgression(this ISequentialOutputByteStream destinationStream, IProgress<UInt64> progress, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(progress);

            return new SequentialOutputByteStreamWithProgression(destinationStream, progress, leaveOpen);
        }

        #endregion

        #region WithEndAction

        public static ISequentialInputByteStream WithEndAction(this ISequentialInputByteStream sourceStream, Action<UInt64> endAction, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(endAction);

            return new SequentialInputByteStreamWithEndAction(sourceStream, endAction, leaveOpen);
        }

        public static ISequentialOutputByteStream WithEndAction(this ISequentialOutputByteStream destinationStream, Action<UInt64> endAction, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(endAction);

            return new SequentialOutputByteStreamWithEndAction(destinationStream, endAction, leaveOpen);
        }

        public static Stream WithEndAction(this Stream baseStream, Action endAction, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(baseStream);
            ArgumentNullException.ThrowIfNull(endAction);

            return new DotNetStreamWithEndAction(baseStream, endAction, leaveOpen);
        }

        #endregion

        #region WithLineFileter

        public static TextReader WithFilter(this TextReader reader, Func<String, Boolean> filter, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(reader);
            ArgumentNullException.ThrowIfNull(filter);

            return new TextReaderWithFilter(reader, filter, leaveOpen);
        }

        public static TextWriter WithFilter(this TextWriter writer, Func<String, Boolean> filter, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(filter);

            return new TextWriterWithFilter(writer, filter, leaveOpen);
        }

        #endregion

        #region WithLogger

        public static Stream WithLogger(this Stream baseStream, IValidationLogger? validationLogger = null)
        {
            ArgumentNullException.ThrowIfNull(baseStream);

            return new DotNetStreamWithLogger(baseStream, validationLogger);
        }

        #endregion

        #region WithCrc32Calculation

        public static ISequentialInputByteStream WithCrc32Calculation(this ISequentialInputByteStream sourceStream, ValueHolder<(UInt32 Crc, UInt64 Length)> resultValueHolder, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(resultValueHolder);

            return new SequentialInputByteStreamWithCrc32Calculation(sourceStream, Crc32.CreateCalculationState(), resultValue => resultValueHolder.Value = resultValue, leaveOpen);
        }

        public static ISequentialInputByteStream WithCrc32Calculation(this ISequentialInputByteStream sourceStream, Action<UInt32, UInt64> onCompleted, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(onCompleted);

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

        public static ISequentialOutputByteStream WithCrc32Calculation(this ISequentialOutputByteStream destinationStream, ValueHolder<(UInt32 Crc, UInt64 Length)> resultValueHolder, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(resultValueHolder);

            return new SequentialOutputByteStreamWithCrc32Calculation(destinationStream, Crc32.CreateCalculationState(), resultValue => resultValueHolder.Value = resultValue, leaveOpen);
        }

        public static ISequentialOutputByteStream WithCrc32Calculation(this ISequentialOutputByteStream destinationStream, Action<UInt32, UInt64> onCompleted, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(onCompleted);

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

        #endregion

        #region WithBranch

        public static ISequentialOutputByteStream WithBranch(this ISequentialOutputByteStream baseStream1, ISequentialOutputByteStream baseStream2, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(baseStream1);
            ArgumentNullException.ThrowIfNull(baseStream2);

            return
                baseStream1 is IDirectDotNetStreamWrapper dotNetDirectWrapperStream1 && baseStream2 is IDirectDotNetStreamWrapper dotNetDirectWrapperStream2
                ? new DotNetStreamWithBranchBySequentialOutputByteStream(baseStream1, dotNetDirectWrapperStream1.RawStream, baseStream2, dotNetDirectWrapperStream2.RawStream, leaveOpen).AsOutputByteStream()
                : new SequentialOutputByteStreamWithBranch(baseStream1, baseStream2, leaveOpen);
        }

        public static Stream WithBranch(this Stream baseStream1, Stream baseStream2, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(baseStream1);
            ArgumentNullException.ThrowIfNull(baseStream2);
            if (!baseStream1.CanWrite)
                throw new ArgumentException("Non-writable stream is specified.", nameof(baseStream1));
            if (!baseStream2.CanWrite)
                throw new ArgumentException("Non-writable stream is specified.", nameof(baseStream2));

            return new DotNetStreamWithBranch(baseStream1, baseStream2, leaveOpen);
        }

        public static TextWriter WithBranch(this TextWriter baseStream1, TextWriter baseStream2, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(baseStream1);
            ArgumentNullException.ThrowIfNull(baseStream2);

            return new TextWriterWithBranch(baseStream1, baseStream2, leaveOpen);
        }

        #endregion
    }
}
