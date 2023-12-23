﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Palmtree.IO.Console
{
    /// <summary>
    /// 88 種類の色を示す構造体です。
    /// </summary>
    public readonly struct Color88
        : IEquatable<Color88>
    {
        private static readonly Byte[] _rdbCubeTable = new[] { (Byte)0x0, (Byte)0x8b, (Byte)0xcd, (Byte)0xff };
        private static readonly UInt16[] _rdbCubeBoundaries;
        private static readonly Byte[] _grayScaleTable = new[] { (Byte)0x2e, (Byte)0x5c, (Byte)0x73, (Byte)0x8b, (Byte)0xa2, (Byte)0xb9, (Byte)0xd0, (Byte)0xe7 };
        private readonly Byte _colorNumber;

        static Color88()
        {
            _rdbCubeBoundaries = new UInt16[_rdbCubeTable.Length + 1];
            _rdbCubeBoundaries[0] = 0;
            _rdbCubeBoundaries[1] = GetBoundary(_rdbCubeTable[0], _rdbCubeTable[1]);
            _rdbCubeBoundaries[2] = GetBoundary(_rdbCubeTable[1], _rdbCubeTable[2]);
            _rdbCubeBoundaries[3] = GetBoundary(_rdbCubeTable[2], _rdbCubeTable[3]);
            _rdbCubeBoundaries[4] = 256;
        }

        private Color88(Int32 colorCode)
        {
            if (!colorCode.InRange(0, 88))
                throw new ArgumentOutOfRangeException(nameof(colorCode));

            _colorNumber = checked((Byte)colorCode);
        }

        /// <summary>
        /// 黒色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 Black = new(0);

        /// <summary>
        /// 赤色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public static readonly Color88 Red = new(1);

        /// <summary>
        /// 緑色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 Green = new(2);

        /// <summary>
        /// 黄色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 Yellow = new(3);

        /// <summary>
        /// 青色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 Blue = new(4);

        /// <summary>
        /// マジェンタを意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 Magenta = new(5);

        /// <summary>
        /// シアンを意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 Cyan = new(6);

        /// <summary>
        /// 白色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 White = new(7);

        /// <summary>
        /// 明るい黒色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 BrightBlack = new(8);

        /// <summary>
        /// 明るい赤色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 BrightRed = new(9);

        /// <summary>
        /// 明るい緑色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 BrightGreen = new(10);

        /// <summary>
        /// 明るい黄色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 BrightYellow = new(11);

        /// <summary>
        /// 明るい青色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 BrightBlue = new(12);

        /// <summary>
        /// 明るいマジェンタを意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 BrightMagenta = new(13);

        /// <summary>
        /// 明るいシアンを意味する <see cref="Color88"/> 値です。
        /// </summary>
        public readonly static Color88 BrightCyan = new(14);

        /// <summary>
        /// 明るい白色を意味する <see cref="Color88"/> 値です。
        /// </summary>
        public static readonly Color88 BrightWhite = new(15);

        /// <summary>
        /// 8 段階のグレースケールの配列です。要素番号が大きいほど明るくなります。
        /// </summary>
        public static readonly ReadOnlyMemory<Color88> GrayScales = Enumerable.Repeat(80, 8).Select(code => new Color88(code)).ToArray();

        /// <summary>
        /// 色の RGB 成分を取得します。
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item><term>r</term><description>赤成分の値です。 0 &lt;= r &lt;= 255</description></item>
        /// <item><term>g</term><description>緑成分の値です。 0 &lt;= g &lt;= 255</description></item>
        /// <item><term>b</term><description>青成分の値です。 0 &lt;= b &lt;= 255</description></item>
        /// </list>
        /// </returns>
        public (Byte r, Byte g, Byte b) ToRgb()
        {
            if (_colorNumber < 16)
            {
                return Color16.ToRgbCore(_colorNumber);
            }
            else if (_colorNumber < 80)
            {
                var colorCode = _colorNumber - 16;
                var rIndex = (colorCode >> 4) & 0x03;
                var gIndex = (colorCode >> 2) & 0x03;
                var bIndex = (colorCode >> 0) & 0x03;
                return (_rdbCubeTable[rIndex], _rdbCubeTable[gIndex], _rdbCubeTable[bIndex]);
            }
            else
            {
                var grayScale = _grayScaleTable[_colorNumber - 80];
                return (grayScale, grayScale, grayScale);
            }
        }

        /// <summary>
        /// 値を別の <see cref="Color88"/> 値と等しいかどうか調べます。
        /// </summary>
        /// <param name="other">
        /// 別の <see cref="Color88"/> 値です。
        /// </param>
        /// <returns>
        /// 値が <paramref name="other"/> と等しいなら true、そうではないなら false です。
        /// </returns>
        public Boolean Equals(Color88 other)
            => _colorNumber == other._colorNumber;

        /// <summary>
        /// 値が別のオブジェクトと等しいかどうか調べます。
        /// </summary>
        /// <param name="other">
        /// 別のオブジェクトです。
        /// </param>
        /// <returns>
        /// 値が <paramref name="other"/> と等しいなら true、そうではないなら false です。
        /// </returns>
        public override Boolean Equals(Object? other)
            => other != null
                && GetType() == other.GetType()
                && Equals((Color88)other);

        /// <summary>
        /// 値のハッシュコードを取得します。
        /// </summary>
        /// <returns>
        /// ハッシュコードである <see cref="Int32"/>値です。
        /// </returns>
        public override Int32 GetHashCode()
            => _colorNumber.GetHashCode();

        /// <summary>
        /// 値の文字列表現を取得します。
        /// </summary>
        /// <returns>
        /// 値の文字列表現の <see cref="String"/> オブジェクトです。
        /// </returns>
        public override String ToString()
        {
            if (_colorNumber < 16)
            {
                return Color16.ToStringCore(_colorNumber);
            }
            else if (_colorNumber < 80)
            {
                var colorCode = _colorNumber - 16;
                var rIndex = (colorCode >> 4) & 0x03;
                var gIndex = (colorCode >> 2) & 0x03;
                var bIndex = (colorCode >> 0) & 0x03;
                return $"#{_rdbCubeTable[rIndex]:x2}{_rdbCubeTable[gIndex]:x2}{_rdbCubeTable[bIndex]:x2}";
            }
            else
            {
                var grayScale = _grayScaleTable[_colorNumber - 80];
                return $"#{grayScale:x2}{grayScale:x2}{grayScale:x2}";
            }
        }

        /// <summary>
        /// 与えられた RGB 値に最も近い <see cref="Color88"/> 構造体を作成します。
        /// </summary>
        /// <param name="r">
        /// 赤要素の値です。
        /// 0 &lt;= <paramref name="r"/> &lt;= 255
        /// </param>
        /// <param name="g">
        /// 緑要素の値です。
        /// 0 &lt;= <paramref name="g"/> &lt;= 255
        /// </param>
        /// <param name="b">
        /// 青要素の値です。
        /// 0 &lt;= <paramref name="b"/> &lt;= 255
        /// </param>
        /// <returns>
        /// 作成された <see cref="Color88"/> 値です。
        /// </returns>
        public static Color88 FromRgb(Byte r, Byte g, Byte b)
            => new(
                EnumerateGrayScaleColors(r, g, b)
                .Concat(Color16.FindColor16CodeFromRgb(r, g, b))
                .Append(GetRgbCube(r, g, b))
                .MinBy(item => item.distance2)
                .colorNumber);

        /// <summary>
        /// <see cref="Color88"/> 型のすべての値を列挙します。
        /// </summary>
        /// <returns>
        /// <see cref="Color88"/> 値の列挙子です。
        /// </returns>
        public static IEnumerable<Color88> GetValues()
        {
            for (var index = 0; index < 88; ++index)
                yield return new Color88(index);
        }

        /// <summary>
        /// 2つの <see cref="Color88"/> 値が等しいかどうか調べます。
        /// </summary>
        /// <param name="left">
        /// 左側の <see cref="Color88"/> 値です。
        /// </param>
        /// <param name="right">
        /// 右側の <see cref="Color88"/> 値です。
        /// </param>
        /// <returns>
        /// <paramref name="left"/> と <paramref name="right"/> が等しいなら true、そうではないなら false です。
        /// </returns>
        public static Boolean operator ==(Color88 left, Color88 right) => left.Equals(right);

        /// <summary>
        /// 2つの <see cref="Color88"/> 値が等しくないかどうか調べます。
        /// </summary>
        /// <param name="left">
        /// 左側の <see cref="Color88"/> 値です。
        /// </param>
        /// <param name="right">
        /// 右側の <see cref="Color88"/> 値です。
        /// </param>
        /// <returns>
        /// <paramref name="left"/> と <paramref name="right"/> が等しくないなら true、そうではないなら false です。
        /// </returns>
        public static Boolean operator !=(Color88 left, Color88 right) => !left.Equals(right);

        /// <summary>
        /// <see cref="Color88"/> 値を <see cref="Int32"/> 値に変換します。
        /// </summary>
        /// <param name="value">
        /// 変換する <see cref="Color88"/> 値です。
        /// </param>
        public static explicit operator Int32(Color88 value) => value._colorNumber;

        private static UInt16 GetBoundary(Byte value1, Byte value2)
        {
            var sum = value1 + value2;
            if ((sum & 1) != 0)
                ++sum;
            return (UInt16)(sum >> 1);
        }

        private static (Int32 colorNumber, Int32 distance2) GetRgbCube(Byte r, Byte g, Byte b)
        {
            var rIndex = 0;
            while (rIndex < _rdbCubeTable.Length)
            {
                if (r >= _rdbCubeBoundaries[rIndex] && r < _rdbCubeBoundaries[rIndex + 1])
                    break;
                ++rIndex;
            }

            Validation.Assert(rIndex < _rdbCubeTable.Length, "rIndex < _rdbCubeTable.Length");

            var gIndex = 0;
            while (gIndex < _rdbCubeTable.Length)
            {
                if (g >= _rdbCubeBoundaries[gIndex] && g < _rdbCubeBoundaries[gIndex + 1])
                    break;
                ++gIndex;
            }

            Validation.Assert(gIndex < _rdbCubeTable.Length, "gIndex < _rdbCubeTable.Length");

            var bIndex = 0;
            while (bIndex < _rdbCubeTable.Length)
            {
                if (b >= _rdbCubeBoundaries[bIndex] && b < _rdbCubeBoundaries[bIndex + 1])
                    break;
                ++bIndex;
            }

            Validation.Assert(bIndex < _rdbCubeTable.Length, "bIndex < _rdbCubeTable.Length");

            var colorNumber = (rIndex << 4) + (gIndex << 2) + (bIndex << 0) + 16;
            var rDistance = _rdbCubeTable[rIndex] - r;
            var gDistance = _rdbCubeTable[gIndex] - g;
            var bDistance = _rdbCubeTable[bIndex] - b;
            return (colorNumber, rDistance * rDistance + gDistance * gDistance + bDistance + bDistance);
        }

        private static IEnumerable<(Int32 colorNumber, Int32 distance2)> EnumerateGrayScaleColors(Byte r, Byte g, Byte b)
        {
            for (var index = 0; index < _grayScaleTable.Length; ++index)
            {
                var colorNumber = index + 80;
                var rDistance = _grayScaleTable[index] - r;
                var gDistance = _grayScaleTable[index] - g;
                var bDistance = _grayScaleTable[index] - b;
                yield return (colorNumber, rDistance * rDistance + gDistance * gDistance + bDistance * bDistance);
            }
        }
    }
}
