using System;
using System.Globalization;
using System.Linq;

namespace Experiment.CSharp.Library
{
    public readonly struct Color256
    {
        private readonly Byte _colorNumber;

        private Color256(Int32 colorCode)
        {
            _colorNumber = checked((Byte)colorCode);
        }

        public static readonly ReadOnlyMemory<Color256> GrayScales = Enumerable.Repeat(232, 24).Select(code => new Color256(code)).ToArray();

        public override String ToString() => _colorNumber.ToString(CultureInfo.InvariantCulture);
    }
}
