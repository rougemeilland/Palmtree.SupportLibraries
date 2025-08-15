using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Palmtree;

namespace Palmtree.IO
{
    public static partial class StreamExtensions
    {
        #region Read

        public static Int32 Read(this Stream sourceStream, Byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);

            return sourceStream.Read(buffer.AsSpan());
        }

        public static Int32 Read(this Stream sourceStream, Byte[] buffer, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return sourceStream.Read(buffer.AsSpan(offset));
        }

        public static UInt32 Read(this Stream sourceStream, Byte[] buffer, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
#if DEBUG
            Validation.Assert(offset <= Int32.MaxValue);
#endif

            var length = sourceStream.Read(buffer.AsSpan(offset));
            return checked((UInt32)length);
        }

        public static Int32 Read(this Stream sourceStream, Byte[] buffer, Range range)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
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
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);

            var length = sourceStream.Read(buffer.AsSpan(offset, count));
            return checked((UInt32)length);
        }

        public static Int32 Read(this Stream sourceStream, Memory<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.Read(buffer.Span);
        }

        public static Int32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);

            return sourceStream.Read(buffer.AsSpan());
        }

        public static Int32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return sourceStream.Read(buffer.AsSpan(offset));
        }

        public static UInt32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            var length = sourceStream.Read(buffer.AsSpan(offset));
            return checked((UInt32)length);
        }

        public static Int32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer, Range range)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            return sourceStream.Read(buffer.AsSpan(offset, count));
        }

        public static Int32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return sourceStream.Read(buffer.AsSpan(offset, count));
        }

        public static UInt32 Read(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            var length = sourceStream.Read(buffer.AsSpan(offset, count));
            return checked((UInt32)length);
        }

        public static Int32 Read(this ISequentialInputByteStream sourceStream, Memory<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return sourceStream.Read(buffer.Span);
        }

        #endregion

        #region ReadByteOrNull

        public static Byte? ReadByteOrNull(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
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
            ArgumentNullException.ThrowIfNull(sourceStream);

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
            ArgumentNullException.ThrowIfNull(sourceStream);

            Span<Byte> buffer = stackalloc Byte[1];
            if (sourceStream.Read(buffer) <= 0)
                throw new EndOfStreamException();

            return buffer[0];
        }

        #endregion

        #region ReadBytes

        public static ReadOnlyMemory<Byte> ReadBytes(this Stream sourceStream, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            return sourceStream.ReadBytesCore(checked((Int32)count));
        }

        public static ReadOnlyMemory<Byte> ReadBytes(this Stream sourceStream, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.ReadBytesCore(checked((Int32)count));
        }

        public static Int32 ReadBytes(this Stream sourceStream, Byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);

            return sourceStream.ReadBytesCore(buffer);
        }

        public static Int32 ReadBytes(this Stream sourceStream, Byte[] buffer, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return sourceStream.ReadBytesCore(buffer.AsSpan(offset));
        }

        public static UInt32 ReadBytes(this Stream sourceStream, Byte[] buffer, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            var length = sourceStream.ReadBytesCore(buffer.AsSpan(offset));
            return checked((UInt32)length);
        }

        public static Int32 ReadBytes(this Stream sourceStream, Byte[] buffer, Range range)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            return sourceStream.ReadBytesCore(buffer.AsSpan(offset, count));
        }

        public static Int32 ReadBytes(this Stream sourceStream, Byte[] buffer, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return sourceStream.ReadBytesCore(buffer.AsSpan(offset, count));
        }

        public static UInt32 ReadBytes(this Stream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            if (checked(offset) + count > buffer.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(buffer)}.");

            var length = sourceStream.ReadBytesCore(buffer.AsSpan(offset, count));
            return checked((UInt32)length);
        }

        public static Int32 ReadBytes(this Stream sourceStream, Memory<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.ReadBytesCore(buffer.Span);
        }

        public static Int32 ReadBytes(this Stream sourceStream, Span<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.ReadBytesCore(buffer);
        }

        public static ReadOnlyMemory<Byte> ReadBytes(this ISequentialInputByteStream sourceStream, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentOutOfRangeException.ThrowIfNegative(count);

            return sourceStream.ReadBytesCore(count);
        }

        public static ReadOnlyMemory<Byte> ReadBytes(this ISequentialInputByteStream sourceStream, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return sourceStream.ReadBytesCore(checked((Int32)count));
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);

            return sourceStream.ReadBytesCore(buffer);
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return sourceStream.ReadBytesCore(buffer.AsSpan(offset));
        }

        public static UInt32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            var length = sourceStream.ReadBytesCore(buffer.AsSpan(offset));
            return checked((UInt32)length);
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer, Range range)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            return sourceStream.ReadBytesCore(buffer.AsSpan(offset, count));
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return sourceStream.ReadBytesCore(buffer.AsSpan(offset, count));
        }

        public static UInt32 ReadBytes(this ISequentialInputByteStream sourceStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);

            var length = sourceStream.ReadBytesCore(buffer.AsSpan(offset, count));
            return checked((UInt32)length);
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Memory<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return sourceStream.ReadBytesCore(buffer.Span);
        }

        public static Int32 ReadBytes(this ISequentialInputByteStream sourceStream, Span<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return sourceStream.ReadBytesCore(buffer);
        }

        #endregion

        #region ReadAllBytes

        public static ReadOnlyMemory<Byte> ReadAllBytes(this Stream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);
            if (!sourceStream.CanRead)
                throw new NotSupportedException();

            return sourceStream.ReadAllBytesCore();
        }

        public static ReadOnlyMemory<Byte> ReadAllBytes(this ISequentialInputByteStream sourceStream)
        {
            ArgumentNullException.ThrowIfNull(sourceStream);

            return sourceStream.ReadAllBytesCore();
        }

        #endregion

        #region ReadBytesCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ReadOnlyMemory<Byte> ReadBytesCore(this Stream sourceStream, Int32 count)
        {
            var buffer = new Byte[count];
            var length = sourceStream.ReadBytesCore(buffer);
            if (length < buffer.Length)
                Array.Resize(ref buffer, length);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ReadOnlyMemory<Byte> ReadBytesCore(this ISequentialInputByteStream sourceStream, Int32 count)
        {
            var buffer = new Byte[count];
            var length = sourceStream.ReadBytesCore(buffer);
            if (length < buffer.Length)
                Array.Resize(ref buffer, length);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Int32 ReadBytesCore(this Stream sourceStream, Span<Byte> buffer)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Int32 ReadBytesCore(this ISequentialInputByteStream sourceStream, Span<Byte> buffer)
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

        #region ReadAllBytesCore

        private static ReadOnlyMemory<Byte> ReadAllBytesCore(this Stream sourceStream)
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

        private static ReadOnlyMemory<Byte> ReadAllBytesCore(this ISequentialInputByteStream sourceStream)
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
                return ReadOnlyMemory<Byte>.Empty;
            if (buffers.Count == 1)
                return buffers.Dequeue();
            var buffer = new Byte[totalLength];
            var destinationWindow = buffer.AsMemory();
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
