using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Palmtree.IO.Console
{
    internal static partial class EastAsianWidth
    {
        [StructLayout(LayoutKind.Sequential)]
        private readonly struct EastAsianWidthRange
        {
            public readonly Int32 StartCodePoint;
            public readonly Int32 Length;
            public readonly EastAsianWidthType Type;

            public EastAsianWidthRange(Int32 startCodePoint, Int32 length, EastAsianWidthType type)
            {
                StartCodePoint = startCodePoint;
                Length = length;
                Type = type;
            }
        }

        public static Int32 GetWidth(String s, CultureInfo culture)
        {
            var bytes = Encoding.UTF32.GetBytes(s);
            Validation.Assert(bytes.Length % sizeof(Int32) == 0, "bytes.Length % sizeof(Int32) == 0");
            var isEastAsia = IsEastAsianCulture(culture);
            var totalWidth = 0;
            for (var index = 0; index < bytes.Length; index += sizeof(Int32))
            {
                var codePoint = bytes.ToInt32LE(index);
                totalWidth +=
                    GetWidthType(codePoint) switch
                    {
                        EastAsianWidthType.Fullwidth or EastAsianWidthType.Wide => 2,
                        EastAsianWidthType.Ambiguous => isEastAsia ? 2 : 1,
                        _ => 1,
                    };
            }

            return totalWidth;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsEastAsianCulture(CultureInfo culture)
            => _eastAsianCultureNames.Contains(culture.Name);

        private static EastAsianWidthType GetWidthType(Int32 codePoint)
        {
            var offset = 0;
            var count = _eastAsianWidthRanges.Length;
            while (count > 1)
            {
                var halfOfCount = count / 2;
                var middleCodePoint = offset + halfOfCount;
                if (codePoint < _eastAsianWidthRanges[middleCodePoint].StartCodePoint)
                {
                    count = halfOfCount;
                }
                else
                {
                    offset = middleCodePoint;
                    count -= halfOfCount;
                }
            }

            Validation.Assert(count == 1, "count == 1");
            var foundElement = _eastAsianWidthRanges[offset];
#if DEBUG
            Validation.Assert(codePoint >= foundElement.StartCodePoint && codePoint < foundElement.StartCodePoint + foundElement.Length, "codePoint >= foundElement.StartCodePoint && codePoint < foundElement.StartCodePoint + foundElement.Length");
#endif
            return foundElement.Type;
        }

#if DEBUG
        private static void DoTest()
        {
            for (var index = 0; index < _eastAsianWidthRanges.Length - 1; ++index)
                Validation.Assert(_eastAsianWidthRanges[index].StartCodePoint + _eastAsianWidthRanges[index].Length == _eastAsianWidthRanges[index + 1].StartCodePoint, "_eastAsianWidthRanges[index].StartCodePoint + _eastAsianWidthRanges[index].Length == _eastAsianWidthRanges[index+1].StartCodePoint");
            for (var codePoint = 0; codePoint <= 0x10ffff; ++codePoint)
                _ = GetWidthType(codePoint);
        }
#endif
    }
}
