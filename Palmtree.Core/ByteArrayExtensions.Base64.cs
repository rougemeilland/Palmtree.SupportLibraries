using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Palmtree.Text;

namespace Palmtree
{
    public static partial class ByteArrayExtensions
    {
        #region GetBase64EncodedSequence

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Char> GetBase64EncodedSequence(this IEnumerable<Byte> source, Char char62 = '+', Char char63 = '/')
        {
            ArgumentNullException.ThrowIfNull(source);
            if (!char62.IsValidBase64OptionalCharacter())
                throw new ArgumentException("Invalid character", nameof(char62));
            if (!char63.IsValidBase64OptionalCharacter())
                throw new ArgumentException("Invalid character", nameof(char63));
            if (char62 == char63)
                throw new ArgumentException($"Invalid character ({nameof(char62)}=={nameof(char63)})");

            return GetBase64EncodedSequenceCore(source, char62, char63);
        }

        #endregion

        #region GetBase64DecodedSequence

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Byte> GetBase64DecodedSequence(this IEnumerable<Char> source, Boolean ignoreSpace = false, Boolean ignoreInvalidCharacter = false, Char char62 = '+', Char char63 = '/')
        {
            ArgumentNullException.ThrowIfNull(source);
            if (!char62.IsValidBase64OptionalCharacter())
                throw new ArgumentException("Invalid character", nameof(char62));
            if (!char63.IsValidBase64OptionalCharacter())
                throw new ArgumentException("Invalid character", nameof(char63));
            if (char62 == char63)
                throw new ArgumentException($"Invalid character ({nameof(char62)}=={nameof(char63)})");

            return GetBase64DecodedSequenceCore(source, ignoreSpace, ignoreInvalidCharacter, char62, char63);
        }

        #endregion

        #region EncodeBase64

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static String EncodeBase64(this IEnumerable<Byte> source, IProgress<UInt64>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(source);

            return EncodeBase64Core(source, Base64EncodingType.Default, progress);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static String EncodeBase64(this IEnumerable<Byte> source, Base64EncodingType encodingType, IProgress<UInt64>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(source);

            return EncodeBase64Core(source, encodingType, progress);
        }

        #endregion

        #region DecodeBase64

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Byte> DecodeBase64(this String source, IProgress<UInt64>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(source);

            return DecodeBase64Core(source, Base64EncodingType.Default, progress);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static IEnumerable<Byte> DecodeBase64(this String source, Base64EncodingType encodingType, IProgress<UInt64>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(source);

            return DecodeBase64Core(source, encodingType, progress);
        }

        #endregion

        private static Boolean IsValidBase64OptionalCharacter(this Char c)
            => c switch
            {
                '!' or '"' or '#' or '$' or '%' or '&' or '\'' or '(' or ')' or '*' or '+' or ',' or '-' or '.' or '/' or ':' or ';' or '<' or '>' or '?' or '@' or '[' or '\\' or ']' or '^' or '_' or '`' or '{' or '|' or '}' or '~' => true,
                _ => false,
            };

        private static String EncodeBase64Core(IEnumerable<Byte> source, Base64EncodingType encodingType, IProgress<UInt64>? progress)
        {
            switch (encodingType)
            {
                case Base64EncodingType.Rfc4648Encoding: // Default
                case Base64EncodingType.Rfc2045Encoding: // for MIME
                    return
                        String.Join(
                            "\r\n",
                            source.GetBase64EncodedSequence()
                            .ChunkAsString(64));
                case Base64EncodingType.Rfc4880Encoding: // for OpenPGP Radix-64
                    var crc24ValueHolder = new ValueHolder<(UInt32 Crc, UInt64 Length)>();
                    var bodyPart =
                        String.Join(
                            "\r\n",
                            source
                            .GetSequenceWithCrc24(crc24ValueHolder, progress)
                            .GetBase64EncodedSequence()
                            .ChunkAsString(76));
                    var crc24 = crc24ValueHolder.Value.Crc;
                    var crcPart =
                        new String(
                            [..
                                new[]
                                {
                                    (Byte)(crc24 >> 16),
                                    (Byte)(crc24 >> 8),
                                    (Byte)(crc24 >> 0),
                                }
                                .GetBase64EncodedSequence()
                            ]);
                    return bodyPart + "\r\n=" + crcPart;
                default:
                    throw new ArgumentException($"Unexpected {nameof(Base64EncodingType)} value", nameof(encodingType));
            }
        }

        private static IEnumerable<Byte> DecodeBase64Core(String source, Base64EncodingType encodingType, IProgress<UInt64>? progress)
        {
            switch (encodingType)
            {
                case Base64EncodingType.Rfc4648Encoding: // Default
                    return source.GetBase64DecodedSequence(false, false);
                case Base64EncodingType.Rfc2045Encoding: // for MIME
                    return source.GetBase64DecodedSequence(true, true);
                case Base64EncodingType.Rfc4880Encoding: // for OpenPGP Radix-64
                    var indexOfLastEqualSign = source.LastIndexOf('=');
                    var bodyPart = indexOfLastEqualSign >= 0 ? source[..indexOfLastEqualSign] : source;
                    var crcPart = indexOfLastEqualSign >= 0 ? source[(indexOfLastEqualSign + 1)..] : null;
                    var data = bodyPart.GetBase64DecodedSequence(true, false).ToArray();
                    if (crcPart is not null)
                    {
                        var crcByteArray = crcPart.GetBase64DecodedSequence(true, false).ToArray();
                        if (crcByteArray.Length != 3)
                            throw new FormatException();
                        var desiredCrc = ((UInt32)crcByteArray[0] << 16) | ((UInt32)crcByteArray[1] << 8) | ((UInt32)crcByteArray[2] << 0);
                        var actualCrc = data.CalculateCrc24(progress).Crc;
                        if (actualCrc != desiredCrc)
                            throw new FormatException();
                    }

                    return data;
                default:
                    throw new ArgumentException($"Unexpected {nameof(Base64EncodingType)} value", nameof(encodingType));
            }
        }

        private static IEnumerable<Char> GetBase64EncodedSequenceCore(IEnumerable<Byte> source, Char char62, Char char63)
        {
            const Int32 BUFFER_SIZE = 3;
            var bytes = ArrayPool<Byte>.Shared.Rent(BUFFER_SIZE);
            try
            {
                var index = 0;
                foreach (var data in source)
                {
                    bytes[index++] = data;
                    if (index >= BUFFER_SIZE)
                    {
                        yield return ToBase64Character(bytes[0] >> 2, char62, char63);
                        yield return ToBase64Character(((bytes[0] << 4) | (bytes[1] >> 4)) & 0x3f, char62, char63);
                        yield return ToBase64Character(((bytes[1] << 2) | (bytes[2] >> 6)) & 0x3f, char62, char63);
                        yield return ToBase64Character(bytes[2] & 0x3f, char62, char63);
                        index = 0;
                    }
                }

                switch (index)
                {
                    case 0:
                        break;
                    case 1:
                        yield return ToBase64Character(bytes[0] >> 2, char62, char63);
                        yield return ToBase64Character((bytes[0] << 4) & 0x3f, char62, char63);
                        yield return '=';
                        yield return '=';
                        break;
                    case 2:
                        yield return ToBase64Character(bytes[0] >> 2, char62, char63);
                        yield return ToBase64Character(((bytes[0] << 4) | (bytes[1] >> 4)) & 0x3f, char62, char63);
                        yield return ToBase64Character((bytes[1] << 2) & 0x3f, char62, char63);
                        yield return '=';
                        break;
                    default:
                        throw Validation.GetFatalErrorException();
                }
            }
            finally
            {
                ArrayPool<Byte>.Shared.Return(bytes);
            }
        }

        private static IEnumerable<Byte> GetBase64DecodedSequenceCore(IEnumerable<Char> source, Boolean ignoreSpace, Boolean ignoreInvalidCharacter, Char char62, Char char63)
        {
            var charSource =
                source
                .Where(c => !c.IsAnyOf('\r', '\n') && (!Char.IsWhiteSpace(c) || (ignoreSpace ? false : throw new FormatException())))
                .TakeWhile(c => c != '=')
                .Select(c => FromBase64Character(c, char62, char63))
                .Where(n => n >= 0 || (ignoreInvalidCharacter ? false : throw new FormatException()));
            var buffer = new Int32[4];
            var index = 0;
            foreach (var data in charSource)
            {
                buffer[index++] = data;
                if (index >= buffer.Length)
                {
                    yield return (Byte)((buffer[0] << 2) | (buffer[1] >> 4));
                    yield return (Byte)((buffer[1] << 4) | (buffer[2] >> 2));
                    yield return (Byte)((buffer[2] << 6) | (buffer[3] >> 0));
                }
            }

            switch (index)
            {
                case 1:
                    break;
                case 2:
                    yield return (Byte)((buffer[0] << 2) | (buffer[1] >> 4));
                    break;
                case 3:
                    yield return (Byte)((buffer[0] << 2) | (buffer[1] >> 4));
                    yield return (Byte)((buffer[1] << 4) | (buffer[2] >> 2));
                    break;
                default:
                    throw new FormatException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Char ToBase64Character(Int32 n, Char char62, Char char63)
        {
#if DEBUG
            Validation.Assert(n >= 0);
#endif
            if (n < 26)
                return (Char)('A' + n);
            if (n < 52)
                return (Char)('a' + n - 26);
            if (n < 62)
                return (Char)('0' + n - 52);
            if (n == 62)
                return char62;
#if DEBUG
            Validation.Assert(n == 63);
#endif
            return char63;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 FromBase64Character(Char c, Char char62, Char char63)
        {
#if DEBUG
            Validation.Assert('0' < 'A');
            Validation.Assert('A' < 'a');
#endif
            if (c == char62)
                return 62;
            else if (c == char63)
                return 63;
            else if (c < '0')
                return -1;
            else if (c <= '9')
                return c - '0' + 52;
            else if (c < 'A')
                return -1;
            else if (c <= 'Z')
                return c - 'A';
            else if (c < 'a')
                return -1;
            else if (c <= 'z')
                return c - 'a' + 26;
            else
                return -1;
        }
    }
}
