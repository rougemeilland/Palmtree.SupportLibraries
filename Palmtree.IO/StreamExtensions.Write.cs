using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Palmtree.IO
{
    public static partial class StreamExtensions
    {
        private const Int32 _WRITE_BYTE_SEQUENCE_DEFAULT_BUFFER_SIZE = 81920;

        #region Write

#if false
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Write(this Stream destinationStream, Byte[] buffer)
        {
            throw new NotImplementedException(); // equivalent to System.IO.Stream.Write(ReadOnlyMemory<Byte>)
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Write(this Stream destinationStream, Byte[] buffer, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            var bufferSpan = buffer.AsReadOnlySpan(offset);
            destinationStream.Write(bufferSpan);
            return bufferSpan.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Write(this Stream destinationStream, Byte[] buffer, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
#if DEBUG
            Validation.Assert(offset <= Int32.MaxValue);
#endif

            var bufferSpan = buffer.AsReadOnlySpan(offset);
            destinationStream.Write(bufferSpan);
            return checked((UInt32)bufferSpan.Length);
        }

#if false

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Write(this Stream destinationStream, Byte[] buffer, Int32 offset, Int32 count)
        {
            throw new NotImplementedException(); // defined in System.IO.Stream.Write(Byte[], Int32, Int32)
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Write(this Stream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);
#if DEBUG
            Validation.Assert(offset <= Int32.MaxValue);
            Validation.Assert(count <= Int32.MaxValue);
#endif

            destinationStream.Write(buffer.AsReadOnlySpan(offset, count));
            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Write(this Stream destinationStream, ReadOnlyMemory<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            destinationStream.Write(buffer.Span);
            return buffer.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Write(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            return destinationStream.Write(buffer.AsReadOnlySpan(offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Write(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);

            var length = destinationStream.Write(buffer.AsReadOnlySpan(offset));
            return checked((UInt32)length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Write(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            return destinationStream.Write(buffer.AsReadOnlySpan(offset, count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static UInt32 Write(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);

            var length = destinationStream.Write(buffer.AsReadOnlySpan(offset, count));
            return checked((UInt32)length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 Write(this ISequentialOutputByteStream destinationStream, ReadOnlyMemory<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            return destinationStream.Write(buffer.Span);
        }

        #endregion

        #region WriteByte

#if false
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteByte(this Stream destinationStream, Byte value)
        {
            throw new NotImplementedException(); // defined in System.IO.Stream.WriteByte(Byte)
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteByte(this ISequentialOutputByteStream destinationStream, Byte value)
        {
            Span<Byte> buffer = [value];
            var length = destinationStream.Write(buffer);
            Validation.Assert(length > 0);
        }

        #endregion

        #region WriteBytes

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this Stream destinationStream, Byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);

            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this Stream destinationStream, Byte[] buffer, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this Stream destinationStream, Byte[] buffer, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
#if DEBUG
            Validation.Assert(offset <= Int32.MaxValue);
#endif

            destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this Stream destinationStream, Byte[] buffer, Range range)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(offset, count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this Stream destinationStream, Byte[] buffer, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(offset, count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this Stream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);
#if DEBUG
            Validation.Assert(offset <= Int32.MaxValue);
            Validation.Assert(count <= Int32.MaxValue);
#endif

            destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(offset, count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this Stream destinationStream, ReadOnlyMemory<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            destinationStream.WriteBytesCore(buffer.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this Stream destinationStream, ReadOnlySpan<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();

            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this Stream destinationStream, IEnumerable<ReadOnlyMemory<Byte>> buffers)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            if (!destinationStream.CanWrite)
                throw new NotSupportedException();
            ArgumentNullException.ThrowIfNull(buffers);

            foreach (var buffer in buffers)
                destinationStream.Write(buffer.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);

            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);

            destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
#if DEBUG
            Validation.Assert(offset <= Int32.MaxValue);
#endif

            destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Range range)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var (offset, count) = buffer.GetOffsetAndLength(range);
            destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(offset, count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, buffer.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, buffer.Length - offset);

            destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(offset, count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, Byte[] buffer, UInt32 offset, UInt32 count)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffer);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)buffer.Length);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, (UInt32)buffer.Length - offset);
#if DEBUG
            Validation.Assert(offset <= Int32.MaxValue);
            Validation.Assert(count <= Int32.MaxValue);
#endif

            destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(offset, count));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, ReadOnlyMemory<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            var spanOfBuffer = buffer.Span;
            while (!spanOfBuffer.IsEmpty)
            {
                var length = destinationStream.Write(spanOfBuffer);
                if (length <= 0)
                    throw new IOException("Can not write any more");
                spanOfBuffer = spanOfBuffer[length..];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, ReadOnlySpan<Byte> buffer)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);

            destinationStream.WriteBytesCore(buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void WriteBytes(this ISequentialOutputByteStream destinationStream, IEnumerable<ReadOnlyMemory<Byte>> buffers)
        {
            ArgumentNullException.ThrowIfNull(destinationStream);
            ArgumentNullException.ThrowIfNull(buffers);

            foreach (var buffer in buffers)
                destinationStream.WriteBytesCore(buffer.Span);
        }

        #endregion

        #region WriteByteSequence

        public static void WriteByteSequence(this Stream destinationStream, IEnumerable<Byte> sequence)
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
                        destinationStream.Write(buffer, 0, index);
                }
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        public static void WriteByteSequence(this ISequentialOutputByteStream destinationStream, IEnumerable<Byte> sequence)
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
                        destinationStream.WriteBytesCore(buffer.AsReadOnlySpan(0, index));
                }
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(buffer);
            }
        }

        #endregion

        #region WriteBytesCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void WriteBytesCore(this Stream destinationStream, ReadOnlySpan<Byte> buffer)
            => destinationStream.Write(buffer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteBytesCore(this ISequentialOutputByteStream destinationStream, ReadOnlySpan<Byte> buffer)
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
    }
}
