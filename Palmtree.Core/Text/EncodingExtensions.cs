using System;
using System.Text;

// public な拡張メソッドのクラスであるため、アセンブリの既定の名前空間に配置した。
#pragma warning disable IDE0130 // Namespace がフォルダー構造と一致しません
namespace Palmtree
#pragma warning restore IDE0130 // Namespace がフォルダー構造と一致しません
{
    public static class EncodingExtensions
    {
        static EncodingExtensions()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static Encoding WithoutPreamble(this Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            if (encoding.Preamble.Length <= 0)
                return encoding;

            switch (encoding.CodePage)
            {
                case 1200: // utf-16
                {
                    var newEncoding = new UnicodeEncoding(false, false)
                    {
                        EncoderFallback = encoding.EncoderFallback,
                        DecoderFallback = encoding.DecoderFallback
                    };
                    return newEncoding;
                }
                case 1201: // utf-16BE
                {
                    var newEncoding = new UnicodeEncoding(true, false)
                    {
                        EncoderFallback = encoding.EncoderFallback,
                        DecoderFallback = encoding.DecoderFallback
                    };
                    return newEncoding;
                }
                case 12000: // utf-32
                {
                    var newEncoding = new UTF32Encoding(false, false)
                    {
                        EncoderFallback = encoding.EncoderFallback,
                        DecoderFallback = encoding.DecoderFallback
                    };
                    return newEncoding;
                }
                case 12001: // utf-32BE
                {
                    var newEncoding = new UTF32Encoding(true, false)
                    {
                        EncoderFallback = encoding.EncoderFallback,
                        DecoderFallback = encoding.DecoderFallback
                    };
                    return newEncoding;
                }
                case 65001: // utf-8
                {
                    var newEncoding = new UTF8Encoding(false)
                    {
                        EncoderFallback = encoding.EncoderFallback,
                        DecoderFallback = encoding.DecoderFallback
                    };
                    return newEncoding;
                }
                default:
                {
                    // UTF-XX 以外のエンコーディングは Preamble を持たないはずなので、このルートに到達することはないはず。
                    throw Validation.GetFatalErrorException();
                }
            }
        }

        public static Encoding WithFallback(this Encoding encoding, String? encoderReplacement, String? decoderReplacement)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            var newEncoderFallback = encoderReplacement is null ? (EncoderFallback)new EncoderExceptionFallback() : new EncoderReplacementFallback(encoderReplacement);
            var newDecoderFallback = decoderReplacement is null ? (DecoderFallback)new DecoderExceptionFallback() : new DecoderReplacementFallback(decoderReplacement);

            if (encoding.EncoderFallback.Equals(newEncoderFallback) && encoding.DecoderFallback.Equals(newDecoderFallback))
                return encoding;

            switch (encoding.CodePage)
            {
                case 1200: // utf-16
                {
                    var newEncoding = new UnicodeEncoding(false, encoding.Preamble.Length > 0)
                    {
                        EncoderFallback = newEncoderFallback,
                        DecoderFallback = newDecoderFallback,
                    };
                    return newEncoding;
                }
                case 1201: // utf-16BE
                {
                    var newEncoding = new UnicodeEncoding(true, encoding.Preamble.Length > 0)
                    {
                        EncoderFallback = newEncoderFallback,
                        DecoderFallback = newDecoderFallback,
                    };
                    return newEncoding;
                }
                case 12000: // utf-32
                {
                    var newEncoding = new UTF32Encoding(false, encoding.Preamble.Length > 0)
                    {
                        EncoderFallback = newEncoderFallback,
                        DecoderFallback = newDecoderFallback,
                    };
                    return newEncoding;
                }
                case 12001: // utf-32BE
                {
                    var newEncoding = new UTF32Encoding(true, encoding.Preamble.Length > 0)
                    {
                        EncoderFallback = newEncoderFallback,
                        DecoderFallback = newDecoderFallback,
                    };
                    return newEncoding;
                }
                case 65001: // utf-8
                {
                    var newEncoding = new UTF8Encoding(encoding.Preamble.Length > 0)
                    {
                        EncoderFallback = newEncoderFallback,
                        DecoderFallback = newDecoderFallback,
                    };
                    return newEncoding;
                }
                default:
                {
                    return
                    Encoding.GetEncoding(
                        encoding.CodePage,
                        newEncoderFallback,
                        newDecoderFallback);
                }
            }
        }

        public static Boolean EqualsStrictly(this Encoding encoding1, Encoding encoding2)
            => encoding1 is null
                ? encoding2 is null
                : encoding2 is not null
                    && encoding1.CodePage.Equals(encoding2.CodePage)
                    && encoding1.Preamble.SequenceEqual(encoding2.Preamble)
                    && encoding1.EncoderFallback.Equals(encoding2.EncoderFallback)
                    && encoding1.DecoderFallback.Equals(encoding2.DecoderFallback);
    }
}
