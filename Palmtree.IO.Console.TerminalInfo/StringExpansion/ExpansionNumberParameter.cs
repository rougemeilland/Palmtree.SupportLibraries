using System;
using System.Globalization;
using System.Text;
using Palmtree.Numerics;

namespace Palmtree.IO.Console.StringExpansion
{
    internal class ExpansionNumberParameter
        : ExpansionParameter
    {
        private readonly Int32 _value;

        public ExpansionNumberParameter(Int32 value)
        {
            _value = value;
        }

        public ExpansionNumberParameter(Char value)
        {
            _value = value;
        }

        public ExpansionNumberParameter(Boolean value)
        {
            _value = value ? 1 : 0;
        }

        public override Int32 AsNumber() => _value;
        public override Boolean AsBool() => _value != 0;
        public override String AsString() => throw new ExpansionBadArgumentExceptionException("Cannot call \"AsString()\" on numeric type values.");

        protected override String Format(String width, String precision, String typeSpec)
        {
            if (!width.TryParse(out Int32 widthValue))
            {
                if (!String.IsNullOrEmpty(width))
                    throw new ExpansionBadArgumentExceptionException($"Invalid format of {nameof(width)}.: \"{width}\"");

                widthValue = -1;
            }

            if (!precision.TryParse(out Int32 precisionValue))
            {
                if (!String.IsNullOrEmpty(precision))
                    throw new ExpansionBadArgumentExceptionException($"Invalid format of {nameof(precision)}.: \"{precision}\"");

                precisionValue = -1;
            }

            return
                typeSpec switch
                {
                    "c" =>
                        PadText(
                            new String((Char)(Byte)_value, 1),
                            widthValue,
                            width.Length > 0 && width[0] == '0' ? '0' : ' '),
                    "d" =>
                        Format(
                            _value < 0 ? "-" : "",
                            (_value < 0 ? checked(-_value) : _value).ToString("D", CultureInfo.InvariantCulture.NumberFormat),
                            widthValue,
                            precisionValue,
                            width.Length > 0 && width[0] == '0'),
                    "o" =>
                        Format(
                            "",
                            Convert.ToString(_value, 8),
                            widthValue,
                            precisionValue,
                            width.Length > 0 && width[0] == '0'),
                    "u" =>
                        Format(
                            "",
                            unchecked((UInt32)_value).ToString("D", CultureInfo.InvariantCulture.NumberFormat),
                            widthValue,
                            precisionValue,
                            width.Length > 0 && width[0] == '0'),
                    "x" or "X" =>
                        Format(
                            "",
                            _value.ToString(typeSpec, CultureInfo.InvariantCulture.NumberFormat),
                            widthValue,
                            precisionValue,
                            width.Length > 0 && width[0] == '0'),
                    _ => throw new ExpansionBadArgumentExceptionException($"Not supported type spec.: \"{typeSpec}\""),
                };
        }

        private static String FromByteToString(Byte data)
        {
            Span<Byte> buffer = stackalloc Byte[1];
            buffer[0] = data;
            return Encoding.ASCII.GetString(buffer);
        }

        private static String Format(String sign, String valueText, Int32 width, Int32 precision, Boolean zeroPadding)
            => precision >= 0
                ? PadText($"{sign}{PadText(valueText, precision, '0')}", width, ' ')
                : width < 0
                ? $"{sign}{valueText}"
                : zeroPadding
                ? $"{sign}{PadText(valueText, (width - sign.Length).Maximum(0), '0')}"
                : PadText($"{sign}{valueText}", width, ' ');

        private static String PadText(String text, Int32 length, Char paddingChar)
            => length > text.Length ? $"{new String(paddingChar, length - text.Length)}{text}" : text;
    }
}
