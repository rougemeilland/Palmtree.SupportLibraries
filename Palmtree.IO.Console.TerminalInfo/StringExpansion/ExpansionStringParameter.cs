using System;
using Palmtree.Numerics;

namespace Palmtree.IO.Console.StringExpansion
{
    internal class ExpansionStringParameter
        : ExpansionParameter
    {
        private readonly String _value;

        public ExpansionStringParameter(String value)
        {
            _value = value;
        }

        public override Int32 AsNumber() => throw new ExpansionBadArgumentExceptionException("Cannot call \"AsNumber()\" on numeric type values.");
        public override Boolean AsBool() => throw new ExpansionBadArgumentExceptionException("Cannot call \"AsBool()\" on numeric type values.");
        public override String AsString() => _value;

        protected override String Format(String width, String precision, String typeSpec)
        {
            if (typeSpec != "s")
                throw new ExpansionBadArgumentExceptionException($"Not supported type spec.: \"{typeSpec}\"");

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

            var valueText = _value;
            if (precisionValue >= 0)
                valueText = valueText[..precisionValue.Minimum(valueText.Length)];
            if (widthValue >= 0)
                valueText = $"{new String(width[0] == '0' ? '0' : ' ', (widthValue - valueText.Length).Maximum(0))}{valueText}";
            return valueText;
        }
    }
}
