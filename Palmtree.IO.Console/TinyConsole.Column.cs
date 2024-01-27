using System;
using System.Globalization;

namespace Palmtree.IO.Console
{
    partial class TinyConsole
    {
        /// <summary>
        /// 指定したカルチャにおいて、指定した文字列をコンソールに表示した場合にコンソール上に占める最大桁数を取得します。
        /// </summary>
        /// <param name="s">
        /// 桁数を求める対象である <see cref="String"/> オブジェクトです。
        /// </param>
        /// <param name="culture">
        /// 文字の桁数を求めるために使用されるカルチャを示す <see cref="CultureInfo"/> オブジェクトです。
        /// null の場合は、既定のカルチャが使用されます。
        /// </param>
        /// <returns>
        /// 文字列 <paramref name="s"/> をコンソールに表示した場合にコンソール上に占める最大の桁数を示す <see cref="Int32"/> 値です。
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>
        /// 文字列 <paramref name="s"/> に制御文字が含まれる場合、このメソッドは正しい値を返さないことがあります。
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="s"/> が null です。
        /// </exception>
        public static Int32 GetWidth(String s, CultureInfo? culture = null)
        {
            if (s is null)
                throw new ArgumentNullException(nameof(s));

            return EastAsianWidth.GetWidth(s, culture ?? CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 指定された文字列の一部を省略することによって指定された長さに縮めます。
        /// </summary>
        /// <param name="s">
        /// 縮める文字列を示す <see cref="String"/> オブジェクトです。
        /// </param>
        /// <param name="width">
        /// 縮める長さを示す <see cref="Int32"/> 値です。
        /// </param>
        /// <param name="altStr">
        /// <paramref name="s"/> で示される文字列を縮める際に代わりに使用される文字列を示す <see cref="String"/> オブジェクトです。
        /// </param>
        /// <param name="style">
        /// 文字列を縮める際に省略する箇所を示す <see cref="TextShrinkingStyle"/> 列挙体です。
        /// </param>
        /// <param name="culture">
        /// 文字列を縮める際に使用されるカルチャを示す <see cref="CultureInfo"/> オブジェクトです。
        /// null の場合は、既定のカルチャが使用されます。
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="s"/> が null です。
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="width"/> が範囲外の値です。
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="altStr"/> が null または 空文字列です。
        /// </exception>
        public static String ShrinkText(String s, Int32 width, String altStr, TextShrinkingStyle style, CultureInfo? culture = null)
        {
            if (s is null)
                throw new ArgumentNullException(nameof(s));
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width));
            if (String.IsNullOrEmpty(altStr))
                throw new ArgumentException($"'{nameof(altStr)}' must not be null or empty.", nameof(altStr));

            return EastAsianWidth.ShrinkText(s, width, altStr, style, culture ?? CultureInfo.CurrentCulture);
        }
    }
}
