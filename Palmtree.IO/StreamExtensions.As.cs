using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Palmtree.IO.StreamFilters;

namespace Palmtree.IO
{
    public static partial class StreamExtensions
    {
        private const Int32 _DEFAULT_TEXT_STREAM_BUFFER_SIZE = 1024;

        private static readonly Encoding _defaultTextStreamEncoding = new UTF8Encoding(false);

        #region AsInputByteStream

        public static ISequentialInputByteStream AsInputByteStream(this Stream sourceStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new ArgumentException($"The stream specified by parameter {nameof(sourceStream)} is not readable.", nameof(sourceStream));

            return
                sourceStream.CanSeek
                ? new RandomInputByteStreamByDotNetStream(sourceStream, leaveOpen)
                : new SequentialInputByteStreamByDotNetStream(sourceStream, leaveOpen);
        }

        #endregion

        #region AsOutputByteStream

        public static ISequentialOutputByteStream AsOutputByteStream(this Stream destinationStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new ArgumentException($"The stream specified by parameter {nameof(destinationStream)} is not writable.", nameof(destinationStream));

            return destinationStream.CanSeek
                ? new RandomOutputByteStreamByDotNetStream(destinationStream, leaveOpen)
                : new SequentialOutputByteStreamByDotNetStream(destinationStream, leaveOpen);
        }

        #endregion

        #region AsDotNetStream

        public static Stream AsDotNetStream(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return
                sourceStream is IDirectDotNetStreamWrapper wrapper
                ? new DotNetStreamByPassThroughSequentialInputOutputByteStream(sourceStream, wrapper.RawStream, leaveOpen)
                : new DotNetStreamBySequentialInputByteStream(sourceStream, leaveOpen);
        }

        public static Stream AsDotNetStream(this ISequentialOutputByteStream destinationStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return
                destinationStream is IDirectDotNetStreamWrapper wrapper
                ? new DotNetStreamByPassThroughSequentialInputOutputByteStream(destinationStream, wrapper.RawStream, leaveOpen)
                : new DotNetStreamBySequentialOutputByteStream(destinationStream, leaveOpen);
        }

        public static Stream AsDotNetStream(this TextReader reader, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(reader);

            return
                reader is StreamReader streamReader && _defaultTextStreamEncoding.EqualsStrictly(streamReader.CurrentEncoding)
                ? new DotNetStreamByStreamReader(streamReader, leaveOpen)
                : new DotNetStreamByTextReader(reader, _defaultTextStreamEncoding, leaveOpen);
        }

        public static Stream AsDotNetStream(this TextReader reader, Encoding encoding, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(reader);
            ArgumentNullException.ThrowIfNull(encoding);

            return
                reader is StreamReader streamReader && encoding.EqualsStrictly(streamReader.CurrentEncoding)
                ? new DotNetStreamByStreamReader(streamReader, leaveOpen)
                : new DotNetStreamByTextReader(reader, encoding, leaveOpen);
        }

        public static Stream AsDotNetStream(this TextWriter writer, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(writer);

            return
                writer is StreamWriter streamWriter && _defaultTextStreamEncoding.EqualsStrictly(streamWriter.Encoding)
                ? new DotNetStreamByStreamWriter(streamWriter, leaveOpen)
                : new DotNetStreamByTextWriter(writer, _defaultTextStreamEncoding, leaveOpen);
        }

        public static Stream AsDotNetStream(this TextWriter writer, Encoding encoding, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(encoding);

            return
                writer is StreamWriter streamWriter && encoding.EqualsStrictly(streamWriter.Encoding)
                ? new DotNetStreamByStreamWriter(streamWriter, leaveOpen)
                : new DotNetStreamByTextWriter(writer, encoding, leaveOpen);
        }

        #endregion

        #region AsByteStream

        public static ISequentialInputByteStream AsByteStream(this IEnumerable<Byte> baseSequence)
        {
            ArgumentNullException.ThrowIfNull(baseSequence);

            return new SequentialInputByteStreamBySequence(baseSequence);
        }

        public static ISequentialInputByteStream AsByteStream(this IInputBitStream sourceStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return new SequentialInputByteStreamByBitStream(sourceStream, BitPackingDirection.MsbToLsb, leaveOpen);
        }

        public static ISequentialInputByteStream AsByteStream(this IInputBitStream sourceStream, BitPackingDirection bitPackingDirection, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return new SequentialInputByteStreamByBitStream(sourceStream, bitPackingDirection, leaveOpen);
        }

        public static ISequentialOutputByteStream AsByteStream(this IOutputBitStream destinationStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return new SequentialOutputByteStreamByBitStream(destinationStream, BitPackingDirection.MsbToLsb, leaveOpen);
        }

        public static ISequentialOutputByteStream AsByteStream(this IOutputBitStream destinationStream, BitPackingDirection bitPackingDirection, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return new SequentialOutputByteStreamByBitStream(destinationStream, bitPackingDirection, leaveOpen);
        }

        #endregion

        #region AsBitStream

        public static IInputBitStream AsBitStream(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return new SequentialInputBitStreamByByteStream(sourceStream, BitPackingDirection.Default, leaveOpen);
        }

        public static IInputBitStream AsBitStream(this ISequentialInputByteStream sourceStream, BitPackingDirection bitPackingDirection, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return new SequentialInputBitStreamByByteStream(sourceStream, bitPackingDirection, leaveOpen);
        }

        public static IInputBitStream AsBitStream(this IEnumerable<Byte> baseSequence, BitPackingDirection bitPackingDirection = BitPackingDirection.Default)
        {
            ArgumentNullException.ThrowIfNull(baseSequence);

            return new SequentialInputBitStreamBySequence(baseSequence, bitPackingDirection);
        }

        public static IOutputBitStream AsBitStream(this ISequentialOutputByteStream destinationStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return new SequentialOutputBitStreamByByteStream(destinationStream, BitPackingDirection.Default, leaveOpen);
        }

        public static IOutputBitStream AsBitStream(this ISequentialOutputByteStream destinationStream, BitPackingDirection bitPackingDirection, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return new SequentialOutputBitStreamByByteStream(destinationStream, bitPackingDirection, leaveOpen);
        }

        #endregion

        #region AsSequentialAccess

        public static ISequentialInputByteStream AsSequentialAccess<POSITION_T>(this IRandomInputByteStream<POSITION_T> sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return sourceStream;
        }

        public static ISequentialOutputByteStream AsSequentialAccess<POSITION_T>(this IRandomOutputByteStream<POSITION_T> destinationStream)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

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

        public static TextReader AsTextReader(this Stream sourceStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return new StreamReader(sourceStream, _defaultTextStreamEncoding, true, _DEFAULT_TEXT_STREAM_BUFFER_SIZE, leaveOpen);
        }

        public static TextReader AsTextReader(this Stream sourceStream, Encoding encoding, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(encoding);

            return new StreamReader(sourceStream, encoding, true, _DEFAULT_TEXT_STREAM_BUFFER_SIZE, leaveOpen);
        }

        public static TextReader AsTextReader(this Stream sourceStream, Boolean detectEncodingFromByteOrderMarks, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return new StreamReader(sourceStream, _defaultTextStreamEncoding, detectEncodingFromByteOrderMarks, _DEFAULT_TEXT_STREAM_BUFFER_SIZE, leaveOpen);
        }

        public static TextReader AsTextReader(this Stream sourceStream, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(encoding);

            return new StreamReader(sourceStream, encoding, detectEncodingFromByteOrderMarks, _DEFAULT_TEXT_STREAM_BUFFER_SIZE, leaveOpen);
        }

        public static TextReader AsTextReader(this Stream sourceStream, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return new StreamReader(sourceStream, _defaultTextStreamEncoding, true, bufferSize, leaveOpen);
        }

        public static TextReader AsTextReader(this Stream sourceStream, Encoding encoding, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(encoding);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return new StreamReader(sourceStream, encoding, false, bufferSize, leaveOpen);
        }

        public static TextReader AsTextReader(this Stream sourceStream, Boolean detectEncodingFromByteOrderMarks, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return new StreamReader(sourceStream, _defaultTextStreamEncoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        public static TextReader AsTextReader(this Stream sourceStream, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(encoding);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return new StreamReader(sourceStream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        public static TextReader AsTextReader(this ISequentialInputByteStream sourceStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return
                sourceStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextReaderBySequentialInputByteStream(sourceStream, dotNetDirectWrapperStream.RawStream, leaveOpen: leaveOpen)
                : new StreamReader(sourceStream.AsDotNetStream(), leaveOpen: leaveOpen);
        }

        public static TextReader AsTextReader(this ISequentialInputByteStream sourceStream, Encoding encoding, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(encoding);

            return
                sourceStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextReaderBySequentialInputByteStream(sourceStream, dotNetDirectWrapperStream.RawStream, encoding, leaveOpen: leaveOpen)
                : new StreamReader(sourceStream.AsDotNetStream(), encoding, leaveOpen: leaveOpen);
        }

        public static TextReader AsTextReader(this ISequentialInputByteStream sourceStream, Boolean detectEncodingFromByteOrderMarks, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return
                sourceStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextReaderBySequentialInputByteStream(sourceStream, dotNetDirectWrapperStream.RawStream, detectEncodingFromByteOrderMarks: detectEncodingFromByteOrderMarks, leaveOpen: leaveOpen)
                : new StreamReader(sourceStream.AsDotNetStream(), detectEncodingFromByteOrderMarks: detectEncodingFromByteOrderMarks, leaveOpen: leaveOpen);
        }

        public static TextReader AsTextReader(this ISequentialInputByteStream sourceStream, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(encoding);

            return
                sourceStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextReaderBySequentialInputByteStream(sourceStream, dotNetDirectWrapperStream.RawStream, encoding, detectEncodingFromByteOrderMarks, leaveOpen: leaveOpen)
                : new StreamReader(sourceStream.AsDotNetStream(), encoding, detectEncodingFromByteOrderMarks, leaveOpen: leaveOpen);
        }

        public static TextReader AsTextReader(this ISequentialInputByteStream sourceStream, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return
                sourceStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextReaderBySequentialInputByteStream(sourceStream, dotNetDirectWrapperStream.RawStream, bufferSize: bufferSize, leaveOpen: leaveOpen)
                : new StreamReader(sourceStream.AsDotNetStream(), bufferSize: bufferSize, leaveOpen: leaveOpen);
        }

        public static TextReader AsTextReader(this ISequentialInputByteStream sourceStream, Encoding encoding, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(encoding);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return
                sourceStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextReaderBySequentialInputByteStream(sourceStream, dotNetDirectWrapperStream.RawStream, encoding, bufferSize: bufferSize, leaveOpen: leaveOpen)
                : new StreamReader(sourceStream.AsDotNetStream(), encoding, bufferSize: bufferSize, leaveOpen: leaveOpen);
        }

        public static TextReader AsTextReader(this ISequentialInputByteStream sourceStream, Boolean detectEncodingFromByteOrderMarks, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return
                sourceStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextReaderBySequentialInputByteStream(sourceStream, dotNetDirectWrapperStream.RawStream, detectEncodingFromByteOrderMarks: detectEncodingFromByteOrderMarks, bufferSize: bufferSize, leaveOpen: leaveOpen)
                : new StreamReader(sourceStream.AsDotNetStream(), detectEncodingFromByteOrderMarks: detectEncodingFromByteOrderMarks, bufferSize: bufferSize, leaveOpen: leaveOpen);
        }

        public static TextReader AsTextReader(this ISequentialInputByteStream sourceStream, Encoding encoding, Boolean detectEncodingFromByteOrderMarks, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(encoding);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return
                sourceStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextReaderBySequentialInputByteStream(sourceStream, dotNetDirectWrapperStream.RawStream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen)
                : new StreamReader(sourceStream.AsDotNetStream(), encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
        }

        #endregion

        #region AsTextWriter

        public static TextWriter AsTextWriter(this Stream destinationStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            return new StreamWriter(destinationStream, _defaultTextStreamEncoding, _DEFAULT_TEXT_STREAM_BUFFER_SIZE, leaveOpen);
        }

        public static TextWriter AsTextWriter(this Stream destinationStream, Encoding encoding, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(encoding);

            return new StreamWriter(destinationStream, encoding, _DEFAULT_TEXT_STREAM_BUFFER_SIZE, leaveOpen);
        }

        public static TextWriter AsTextWriter(this Stream destinationStream, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return new StreamWriter(destinationStream, _defaultTextStreamEncoding, bufferSize, leaveOpen);
        }

        public static TextWriter AsTextWriter(this Stream destinationStream, Encoding encoding, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(encoding);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return new StreamWriter(destinationStream, encoding, bufferSize, leaveOpen);
        }

        public static TextWriter AsTextWriter(this Stream destinationStream, Boolean autoFlush, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            return new StreamWriter(destinationStream, _defaultTextStreamEncoding, _DEFAULT_TEXT_STREAM_BUFFER_SIZE, leaveOpen) { AutoFlush = autoFlush };
        }

        public static TextWriter AsTextWriter(this Stream destinationStream, Encoding encoding, Boolean autoFlush, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(encoding);

            return new StreamWriter(destinationStream, encoding, _DEFAULT_TEXT_STREAM_BUFFER_SIZE, leaveOpen) { AutoFlush = autoFlush };
        }

        public static TextWriter AsTextWriter(this Stream destinationStream, Int32 bufferSize, Boolean autoFlush, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return new StreamWriter(destinationStream, _defaultTextStreamEncoding, bufferSize, leaveOpen) { AutoFlush = autoFlush };
        }

        public static TextWriter AsTextWriter(this Stream destinationStream, Encoding encoding, Int32 bufferSize, Boolean autoFlush, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(encoding);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return new StreamWriter(destinationStream, encoding, bufferSize, leaveOpen) { AutoFlush = autoFlush };
        }

        public static TextWriter AsTextWriter(this ISequentialOutputByteStream destinationStream, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return
                destinationStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextWriterBySequentialOutputByteStream(destinationStream, dotNetDirectWrapperStream.RawStream, leaveOpen: leaveOpen)
                : new StreamWriter(destinationStream.AsDotNetStream(), leaveOpen: leaveOpen);
        }

        public static TextWriter AsTextWriter(this ISequentialOutputByteStream destinationStream, Encoding encoding, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(encoding);

            return
                destinationStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextWriterBySequentialOutputByteStream(destinationStream, dotNetDirectWrapperStream.RawStream, encoding, leaveOpen: leaveOpen)
                : new StreamWriter(destinationStream.AsDotNetStream(), encoding, leaveOpen: leaveOpen);
        }

        public static TextWriter AsTextWriter(this ISequentialOutputByteStream destinationStream, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return
                destinationStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextWriterBySequentialOutputByteStream(destinationStream, dotNetDirectWrapperStream.RawStream, bufferSize: bufferSize, leaveOpen: leaveOpen)
                : new StreamWriter(destinationStream.AsDotNetStream(), bufferSize: bufferSize, leaveOpen: leaveOpen);
        }

        public static TextWriter AsTextWriter(this ISequentialOutputByteStream destinationStream, Encoding encoding, Int32 bufferSize, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(encoding);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return
                destinationStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextWriterBySequentialOutputByteStream(destinationStream, dotNetDirectWrapperStream.RawStream, encoding, bufferSize, leaveOpen: leaveOpen)
                : new StreamWriter(destinationStream.AsDotNetStream(), encoding, bufferSize, leaveOpen);
        }

        public static TextWriter AsTextWriter(this ISequentialOutputByteStream destinationStream, Boolean autoFlush, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return
                destinationStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextWriterBySequentialOutputByteStream(destinationStream, dotNetDirectWrapperStream.RawStream, autoFlush: autoFlush, leaveOpen: leaveOpen)
                : new StreamWriter(destinationStream.AsDotNetStream(), leaveOpen: leaveOpen) { AutoFlush = autoFlush };
        }

        public static TextWriter AsTextWriter(this ISequentialOutputByteStream destinationStream, Encoding encoding, Boolean autoFlush, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(encoding);

            return
                destinationStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextWriterBySequentialOutputByteStream(destinationStream, dotNetDirectWrapperStream.RawStream, encoding, autoFlush: autoFlush, leaveOpen: leaveOpen)
                : new StreamWriter(destinationStream.AsDotNetStream(), encoding, leaveOpen: leaveOpen) { AutoFlush = autoFlush };
        }

        public static TextWriter AsTextWriter(this ISequentialOutputByteStream destinationStream, Int32 bufferSize, Boolean autoFlush, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return
                destinationStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextWriterBySequentialOutputByteStream(destinationStream, dotNetDirectWrapperStream.RawStream, bufferSize: bufferSize, autoFlush: autoFlush, leaveOpen: leaveOpen)
                : new StreamWriter(destinationStream.AsDotNetStream(), bufferSize: bufferSize, leaveOpen: leaveOpen) { AutoFlush = autoFlush };
        }

        public static TextWriter AsTextWriter(this ISequentialOutputByteStream destinationStream, Encoding encoding, Int32 bufferSize, Boolean autoFlush, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(encoding);
            ArgumentOutOfRangeException.ThrowIfNegative(bufferSize);

            return
                destinationStream is IDirectDotNetStreamWrapper dotNetDirectWrapperStream
                ? new TextWriterBySequentialOutputByteStream(destinationStream, dotNetDirectWrapperStream.RawStream, encoding, bufferSize, autoFlush, leaveOpen)
                : new StreamWriter(destinationStream.AsDotNetStream(), encoding, bufferSize, leaveOpen) { AutoFlush = autoFlush };
        }

        #endregion

        #region AsValidationLogger

        public static IDisposableValidationLogger AsValidationLogger(this TextWriter writer, Boolean leaveOpen = false)
        {
            ArgumentNullException.ThrowIfNull(writer);

            return new TextWriterValidationLogger(writer, leaveOpen);
        }

        #endregion
    }
}
