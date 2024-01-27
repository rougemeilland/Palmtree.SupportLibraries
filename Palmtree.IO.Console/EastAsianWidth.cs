using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public static Int32 GetWidth(String text, CultureInfo culture)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));
            if (culture is null)
                throw new ArgumentNullException(nameof(culture));

            return GetWidth(GetCodePoints(text, IsEastAsianCulture(culture)));
        }

        public static String ShrinkText(String text, Int32 width, String altStr, TextShrinkingStyle style, CultureInfo culture)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));
            if (String.IsNullOrEmpty(altStr))
                throw new ArgumentException($"'{nameof(altStr)}' must not be null or empty.", nameof(altStr));
            if (culture is null)
                throw new ArgumentNullException(nameof(culture));

            var isEastAsia = IsEastAsianCulture(culture);
            var textCodePoints = GetCodePoints(text, isEastAsia);
            var textWidth = GetWidth(textCodePoints);
            if (textWidth <= width)
                return text.Normalize(NormalizationForm.FormC);
            var altStrCodePoints = GetCodePoints(altStr, isEastAsia);
            var altStrWidth = GetWidth(altStrCodePoints);
            if (width < altStrWidth)
                throw new ArgumentOutOfRangeException(nameof(width));
            var extraCharactersWidth = textWidth + altStrWidth - width;
            switch (style)
            {
                case TextShrinkingStyle.OmitCenter:
                {
                    var outputCodePoints = new List<Int32>();
                    var totalWidth = 0;

                    // 左側のコードポイントを出力する
                    var leftWidthLimit = (width - altStrWidth + 1) / 2;
                    var leftOffset = 0;
                    while (true)
                    {
                        var (codePoint, codePointWidth) = textCodePoints[leftOffset];

                        // 次の文字を埋めようとして幅の上限を超えた場合は、ループを打ち切る
                        if (totalWidth + codePointWidth > leftWidthLimit)
                            break;
                        ++leftOffset;
                        totalWidth += codePointWidth;
                        outputCodePoints.Add(codePoint);
                    }

                    // 左側の最後のコードポイントの位置が分割してはならない位置であった場合は、分割位置をずらす。
                    while (true)
                    {
                        if (leftOffset <= 0)
                            break;
                        var (codePoint, codePointWidth) = textCodePoints[leftOffset - 1];
                        if (!MustNotBeDivided(codePoint, textCodePoints[leftOffset].CodePoint))
                            break;

                        totalWidth -= codePointWidth;
                        --leftOffset;
                        outputCodePoints.RemoveAt(outputCodePoints.Count - 1);
                    }

                    // 省略文字列を出力する
                    for (var index = 0; index < altStrCodePoints.Length; ++index)
                    {
                        var (codePoint, codePointWidth) = altStrCodePoints[index];
                        totalWidth += codePointWidth;
                        outputCodePoints.Add(codePoint);
                    }

                    // 右側の開始位置を求める
                    var rightWidthLimit = width - totalWidth;
                    var rightWidth = 0;
                    var rightOffset = 0;
                    while (true)
                    {
                        var codePointWidth = textCodePoints[^(rightOffset+1)].Width;

                        // 次の文字に進めようとして幅の上限を超えた場合は、ループを打ち切る
                        if (rightWidth + codePointWidth > rightWidthLimit)
                            break;
                        ++rightOffset;
                        rightWidth += codePointWidth;
                    }

                    // 右側の最初のコードポイントの位置が分割してはならない位置であった場合は、分割位置をずらす。
                    while (true)
                    {
                        if (rightOffset <= 0)
                            break;
                        var (codePoint, codePointWidth) = textCodePoints[^rightOffset];
                        if (!MustNotBeDivided(textCodePoints[^(rightOffset + 1)].CodePoint, codePoint))
                            break;
                        --rightOffset;
                    }

                    // 省略文字列に置換される部分のうち、フォーマット指定コードポイントのみを出力する
                    for (var index = leftOffset; index < textCodePoints.Length - rightOffset; ++index)
                    {
                        var (codePoint, codePointWidth) = textCodePoints[index];
                        if (CharUnicodeInfo.GetUnicodeCategory(codePoint) == UnicodeCategory.Format)
                        {
                            totalWidth += codePointWidth;
                            outputCodePoints.Add(codePoint);
                        }
                    }

                    // 右側のコードポイントを出力する
                    for (var index = textCodePoints.Length - rightOffset; index < textCodePoints.Length; ++index)
                    {
                        var (codePoint, codePointWidth) = textCodePoints[index];
                        totalWidth += codePointWidth;
                        outputCodePoints.Add(codePoint);
                    }

#if DEBUG
                    Validation.Assert(outputCodePoints.Select(codePoint => GetWidth(codePoint, isEastAsia)).Sum() == totalWidth, "codePoints.Select(codePoint => GetWidth(codePoint, isEastAsia)).Sum() == totalWidth");
                    Validation.Assert(totalWidth <= width, "totalWidth <= width");
#endif
                    var outBytes = new Byte[outputCodePoints.Count * sizeof(Int32)];
                    for (var index = 0; index < outputCodePoints.Count; ++index)
                        outBytes.SetValueLE(index * sizeof(Int32), outputCodePoints[index]);

                    return Encoding.UTF32.GetString(outBytes);
                }
                case TextShrinkingStyle.OmitBeginning:
                {
                    var outputCodePoints = new List<Int32>();
                    var totalWidth = 0;

                    // 省略文字列を出力する
                    for (var index = 0; index < altStrCodePoints.Length; ++index)
                    {
                        var (codePoint, codePointWidth) = altStrCodePoints[index];
                        totalWidth += codePointWidth;
                        outputCodePoints.Add(codePoint);
                    }

                    // 右側の開始位置を求める
                    var rightWidthLimit = width - totalWidth;
                    var rightWidth = 0;
                    var rightOffset = 0;
                    while (true)
                    {
                        var codePointWidth = textCodePoints[^(rightOffset + 1)].Width;

                        // 次の文字に進めようとして幅の上限を超えた場合は、ループを打ち切る
                        if (rightWidth + codePointWidth > rightWidthLimit)
                            break;
                        ++rightOffset;
                        rightWidth += codePointWidth;
                    }

                    // 右側の最初のコードポイントの位置が分割してはならない位置であった場合は、分割位置をずらす。
                    while (true)
                    {
                        if (rightOffset <= 0)
                            break;
                        var (codePoint, codePointWidth) = textCodePoints[^rightOffset];
                        if (!MustNotBeDivided(textCodePoints[^(rightOffset + 1)].CodePoint, codePoint))
                            break;
                        --rightOffset;
                    }

                    // 省略文字列に置換される部分のうち、フォーマット指定コードポイントのみを出力する
                    for (var index = 0; index < textCodePoints.Length - rightOffset; ++index)
                    {
                        var (codePoint, codePointWidth) = textCodePoints[index];
                        if (CharUnicodeInfo.GetUnicodeCategory(codePoint) == UnicodeCategory.Format)
                        {
                            totalWidth += codePointWidth;
                            outputCodePoints.Add(codePoint);
                        }
                    }

                    // 右側のコードポイントを出力する
                    for (var index = textCodePoints.Length - rightOffset; index < textCodePoints.Length; ++index)
                    {
                        var (codePoint, codePointWidth) = textCodePoints[index];
                        totalWidth += codePointWidth;
                        outputCodePoints.Add(codePoint);
                    }

#if DEBUG
                    Validation.Assert(outputCodePoints.Select(codePoint => GetWidth(codePoint, isEastAsia)).Sum() == totalWidth, "codePoints.Select(codePoint => GetWidth(codePoint, isEastAsia)).Sum() == totalWidth");
                    Validation.Assert(totalWidth <= width, "totalWidth <= width");
#endif
                    var outBytes = new Byte[outputCodePoints.Count * sizeof(Int32)];
                    for (var index = 0; index < outputCodePoints.Count; ++index)
                        outBytes.SetValueLE(index * sizeof(Int32), outputCodePoints[index]);

                    return Encoding.UTF32.GetString(outBytes);
                }
                case TextShrinkingStyle.OmitEnd:
                {
                    var outputCodePoints = new List<Int32>();
                    var totalWidth = 0;

                    // 左側のコードポイントを出力する
                    var leftWidthLimit = width - altStrWidth;
                    var leftOffset = 0;
                    while (true)
                    {
                        var (codePoint, codePointWidth) = textCodePoints[leftOffset];

                        // 次の文字を埋めようとして幅の上限を超えた場合は、ループを打ち切る
                        if (totalWidth + codePointWidth > leftWidthLimit)
                            break;
                        ++leftOffset;
                        totalWidth += codePointWidth;
                        outputCodePoints.Add(codePoint);
                    }

                    // 左側の最後のコードポイントの位置が分割してはならない位置であった場合は、分割位置をずらす。
                    while (true)
                    {
                        if (leftOffset <= 0)
                            break;
                        var (codePoint, codePointWidth) = textCodePoints[leftOffset - 1];
                        if (!MustNotBeDivided(codePoint, textCodePoints[leftOffset].CodePoint))
                            break;

                        totalWidth -= codePointWidth;
                        --leftOffset;
                        outputCodePoints.RemoveAt(outputCodePoints.Count - 1);
                    }

                    // 省略文字列を出力する
                    for (var index = 0; index < altStrCodePoints.Length; ++index)
                    {
                        var (codePoint, codePointWidth) = altStrCodePoints[index];
                        totalWidth += codePointWidth;
                        outputCodePoints.Add(codePoint);
                    }

                    // 省略文字列に置換される部分のうち、フォーマット指定コードポイントのみを出力する
                    for (var index = leftOffset; index < textCodePoints.Length; ++index)
                    {
                        var (codePoint, codePointWidth) = textCodePoints[index];
                        if (CharUnicodeInfo.GetUnicodeCategory(codePoint) == UnicodeCategory.Format)
                        {
                            totalWidth += codePointWidth;
                            outputCodePoints.Add(codePoint);
                        }
                    }

#if DEBUG
                    Validation.Assert(outputCodePoints.Select(codePoint => GetWidth(codePoint, isEastAsia)).Sum() == totalWidth, "codePoints.Select(codePoint => GetWidth(codePoint, isEastAsia)).Sum() == totalWidth");
                    Validation.Assert(totalWidth <= width, "totalWidth <= width");
#endif
                    var outBytes = new Byte[outputCodePoints.Count * sizeof(Int32)];
                    for (var index = 0; index < outputCodePoints.Count; ++index)
                        outBytes.SetValueLE(index * sizeof(Int32), outputCodePoints[index]);

                    return Encoding.UTF32.GetString(outBytes);
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(style));
            }
        }

        private static (Int32 CodePoint, Int32 Width)[] GetCodePoints(String text, Boolean isEastAsia)
        {
            var utf32Bytes = Encoding.UTF32.GetBytes(text.Normalize(NormalizationForm.FormC));
            Validation.Assert(utf32Bytes.Length % sizeof(Int32) == 0, "utf32Bytes.Length % sizeof(Int32) == 0");
            var textLength = utf32Bytes.Length / sizeof(Int32);
            var outBytes = new (Int32 CodePoint, Int32 Width)[textLength];
            for (var index = 0; index < outBytes.Length; ++index)
            {
                var codePoint = utf32Bytes.ToInt32LE(index * sizeof(Int32));
                var width = GetWidth(codePoint, isEastAsia);
                outBytes[index] = (codePoint, width);
            }

            return outBytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 GetWidth((Int32 CodePoint, Int32 Width)[] codePoints)
        {
            var totalWidth = 0;
            for (var index = 0; index < codePoints.Length; ++index)
                totalWidth += codePoints[index].Width;
            return totalWidth;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 GetWidth(Int32 codePoint, Boolean isEastAsia)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(codePoint);
            if (category == UnicodeCategory.Control)
            {
                // 制御文字のコードポイントである場合

                // 幅の計算上は無視する。
                return 0;
            }
            else
            {
                // 上記以外のコードポイントである場合

                // 表示幅を求める。
                return GetWidthType(codePoint) switch
                {
                    EastAsianWidthType.Fullwidth or EastAsianWidthType.Wide => 2,
                    EastAsianWidthType.Ambiguous => isEastAsia ? 2 : 1,
                    _ => 1,
                };
            }
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

        /// <summary>
        /// 2つのコードポイントの間で文字列を分割してよいかどうかを調べます。
        /// </summary>
        /// <param name="firstCodePoint">
        /// 最初のコードポイントを示す <see cref="Int32"/>値です。
        /// </param>
        /// <param name="secondCodePoint">
        /// 2 番目のコードポイントを示す <see cref="Int32"/>値です。
        /// </param>
        /// <returns>
        /// <paramref name="firstCodePoint"/> で示されるコードポイントと <paramref name="secondCodePoint"/> で示されるコードポイントとの間で文字列を分割可能してはならないなら true、そうではない場合は false を返します。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:未使用のパラメーターを削除します", Justification = "<保留中>")]
        private static Boolean MustNotBeDivided(Int32 firstCodePoint, Int32 secondCodePoint)
            => CharUnicodeInfo.GetUnicodeCategory(secondCodePoint) == UnicodeCategory.NonSpacingMark; // 2 番目のコードポイントが結合文字指定である場合

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
