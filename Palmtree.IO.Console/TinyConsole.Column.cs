using System;
using System.Globalization;

namespace Palmtree.IO.Console
{
    partial class TinyConsole
    {
        /// <summary>
        /// 現在のカルチャにおいて、指定した文字列をコンソールに表示した場合にコンソール上に占める最大桁数を取得します。
        /// </summary>
        /// <param name="s">
        /// 桁数を求める対象である <see cref="String"/> オブジェクトです。
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
        public static Int32 GetWidth(String s)
            => GetWidth(s, CultureInfo.CurrentCulture);

        /// <summary>
        /// 指定したカルチャにおいて、指定した文字列をコンソールに表示した場合にコンソール上に占める最大桁数を取得します。
        /// </summary>
        /// <param name="s">
        /// 桁数を求める対象である <see cref="String"/> オブジェクトです。
        /// </param>
        /// <param name="culture">
        /// 
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
        public static Int32 GetWidth(String s, CultureInfo culture)
        {
            if (String.IsNullOrEmpty(s))
                throw new ArgumentException($"'{nameof(s)}' must not be null or empty.", nameof(s));
            if (culture is null)
                throw new ArgumentNullException(nameof(culture));

            return EastAsianWidth.GetWidth(s, culture);
        }
    }
}
