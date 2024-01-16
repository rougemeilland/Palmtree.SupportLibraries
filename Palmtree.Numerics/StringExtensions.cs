using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Palmtree.Numerics
{
    public static class StringExtensions
    {
        private static readonly Regex _rationalNumberPattern;

        static StringExtensions()
        {
            _rationalNumberPattern = new Regex(@"^(?<numerator>-?\d+)/(?<denominator>\d+)$", RegexOptions.Compiled);
        }

        #region ParseAs

        /// <summary>
        /// 文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="Int32"/> 型に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="numberStyles">変換方法のオプションである <see cref="NumberStyles"/> 列挙体です。</param>
        /// <returns>変換された <see cref="Int32"/> 値です。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ParseAsInt32(this String s, NumberStyles numberStyles = NumberStyles.None)
            => Int32.Parse(s, numberStyles | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture.NumberFormat);

        /// <summary>
        /// 文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="UInt32"/> 型に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="numberStyles">変換方法のオプションである <see cref="NumberStyles"/> 列挙体です。</param>
        /// <returns>変換された <see cref="UInt32"/> 値です。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ParseAsUint32(this String s, NumberStyles numberStyles = NumberStyles.None)
            => UInt32.Parse(s, numberStyles, CultureInfo.InvariantCulture.NumberFormat);

        /// <summary>
        /// 文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="Int64"/> 型に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="numberStyles">変換方法のオプションである <see cref="NumberStyles"/> 列挙体です。</param>
        /// <returns>変換された <see cref="Int64"/> 値です。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ParseAsInt64(this String s, NumberStyles numberStyles = NumberStyles.None)
            => Int64.Parse(s, numberStyles | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture.NumberFormat);

        /// <summary>
        /// 文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="UInt64"/> 型に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="numberStyles">変換方法のオプションである <see cref="NumberStyles"/> 列挙体です。</param>
        /// <returns>変換された <see cref="UInt64"/> 値です。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ParseAsUint64(this String s, NumberStyles numberStyles = NumberStyles.None)
            => UInt64.Parse(s, numberStyles, CultureInfo.InvariantCulture.NumberFormat);

        /// <summary>
        /// 固定小数点形式の文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="Double"/> 型に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="numberStyles">変換方法のオプションである <see cref="NumberStyles"/> 列挙体です。</param>
        /// <returns>変換された <see cref="Double"/> 値です。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ParseAsDouble(this String s, NumberStyles numberStyles = NumberStyles.None)
            => Double.Parse(s, numberStyles | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat);

        /// <summary>
        /// 分数形式の文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="Int32"/> 型の分子と分母に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term>numerator</term><description>変換された分子である<see cref="Int32"/>値です。</description></item>
        /// <item><term>denominator</term><description>変換された分母である<see cref="Int32"/>値です。</description></item>
        /// </list>
        /// </returns>
        public static (Int32 numerator, Int32 denominator) ParseAsInt32Fraction(this String s)
        {
            var match = _rationalNumberPattern.Match(s);
            if (!match.Success)
                throw new FormatException($"Expected a string in fraction format.: \"{s}\"");

            return (match.Groups["numerator"].Value.ParseAsInt32(), match.Groups["denominator"].Value.ParseAsInt32());
        }

        /// <summary>
        /// 分数形式の文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="UInt32"/> 型の分子と分母に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term>numerator</term><description>変換された分子である<see cref="UInt32"/>値です。</description></item>
        /// <item><term>denominator</term><description>変換された分母である<see cref="UInt32"/>値です。</description></item>
        /// </list>
        /// </returns>
        public static (UInt32 numerator, UInt32 denominator) ParseAsUInt32Fraction(this String s)
        {
            var match = _rationalNumberPattern.Match(s);
            if (!match.Success)
                throw new FormatException($"Expected a string in fraction format.: \"{s}\"");

            return (match.Groups["numerator"].Value.ParseAsUint32(), match.Groups["denominator"].Value.ParseAsUint32());
        }

        /// <summary>
        /// 分数形式の文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="Int64"/> 型の分子と分母に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term>numerator</term><description>変換された分子である<see cref="Int64"/>値です。</description></item>
        /// <item><term>denominator</term><description>変換された分母である<see cref="Int64"/>値です。</description></item>
        /// </list>
        /// </returns>
        public static (Int64 numerator, Int64 denominator) ParseAsInt64Fraction(this String s)
        {
            var match = _rationalNumberPattern.Match(s);
            if (!match.Success)
                throw new FormatException($"Expected a string in fraction format.: \"{s}\"");

            return (match.Groups["numerator"].Value.ParseAsInt64(), match.Groups["denominator"].Value.ParseAsInt64());
        }

        /// <summary>
        /// 分数形式の文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="UInt64"/> 型の分子と分母に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <returns>
        /// <list type="bullet">
        /// <item><term>numerator</term><description>変換された分子である<see cref="UInt64"/>値です。</description></item>
        /// <item><term>denominator</term><description>変換された分母である<see cref="UInt64"/>値です。</description></item>
        /// </list>
        /// </returns>
        public static (UInt64 numerator, UInt64 denominator) ParseAsUInt64Fraction(this String s)
        {
            var match = _rationalNumberPattern.Match(s);
            if (!match.Success)
                throw new FormatException($"Expected a string in fraction format.: \"{s}\"");

            return (match.Groups["numerator"].Value.ParseAsUint64(), match.Groups["denominator"].Value.ParseAsUint64());
        }

        #endregion

        #region TryParse

        /// <summary>
        /// 文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="Int32"/> 値に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="value">変換結果の数値です。</param>
        /// <returns>true である場合は変換に成功したことを意味し、更に <paramref name="value"/> に変換結果が格納されます。
        /// false は変換に失敗したことを意味します。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(this String s, out Int32 value)
            => Int32.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign, CultureInfo.InvariantCulture.NumberFormat, out value);

        /// <summary>
        /// 文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="UInt32"/> 値に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="value">変換結果の数値です。</param>
        /// <returns>true である場合は変換に成功したことを意味し、更に <paramref name="value"/> に変換結果が格納されます。
        /// false は変換に失敗したことを意味します。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(this String s, out UInt32 value)
            => UInt32.TryParse(s, NumberStyles.None, CultureInfo.InvariantCulture.NumberFormat, out value);

        /// <summary>
        /// 文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="Int64"/> 値に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="value">変換結果の数値です。</param>
        /// <returns>true である場合は変換に成功したことを意味し、更に <paramref name="value"/> に変換結果が格納されます。
        /// false は変換に失敗したことを意味します。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(this String s, out Int64 value)
            => Int64.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign, CultureInfo.InvariantCulture.NumberFormat, out value);

        /// <summary>
        /// 文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="UInt64"/> 値に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="value">変換結果の数値です。</param>
        /// <returns>true である場合は変換に成功したことを意味し、更に <paramref name="value"/> に変換結果が格納されます。
        /// false は変換に失敗したことを意味します。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(this String s, out UInt64 value)
            => UInt64.TryParse(s, NumberStyles.None, CultureInfo.InvariantCulture.NumberFormat, out value);

        /// <summary>
        /// 固定小数点形式の文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="Double"/> 値に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="value">変換結果の数値です。</param>
        /// <returns>true である場合は変換に成功したことを意味し、更に <paramref name="value"/> に変換結果が格納されます。
        /// false は変換に失敗したことを意味します。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryParse(this String s, out Double value)
            => Double.TryParse(s, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out value);

        /// <summary>
        /// 分数形式の文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="Int32"/> 型の分子と分母に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="numerator">変換結果の分子です。</param>
        /// <param name="denominator">変換結果の分母です。</param>
        /// <returns>true である場合は変換に成功したことを意味し、更に <paramref name="numerator"/>および <paramref name="denominator"/> に変換結果が格納されます。
        /// false は変換に失敗したことを意味します。
        /// </returns>
        public static Boolean TryParse(this String s, out Int32 numerator, out Int32 denominator)
        {
            var match = _rationalNumberPattern.Match(s);
            if (!match.Success)
            {
                numerator = default;
                denominator = default;
                return false;
            }

            numerator = match.Groups["numerator"].Value.ParseAsInt32();
            denominator = match.Groups["denominator"].Value.ParseAsInt32();
            return true;
        }

        /// <summary>
        /// 分数形式の文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="UInt32"/> 型の分子と分母に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="numerator">変換結果の分子です。</param>
        /// <param name="denominator">変換結果の分母です。</param>
        /// <returns>true である場合は変換に成功したことを意味し、更に <paramref name="numerator"/>および <paramref name="denominator"/> に変換結果が格納されます。
        /// false は変換に失敗したことを意味します。
        /// </returns>
        public static Boolean TryParse(this String s, out UInt32 numerator, out UInt32 denominator)
        {
            var match = _rationalNumberPattern.Match(s);
            if (!match.Success)
            {
                numerator = default;
                denominator = default;
                return false;
            }

            numerator = UInt32.Parse(match.Groups["numerator"].Value, NumberStyles.None, CultureInfo.InvariantCulture.NumberFormat);
            denominator = UInt32.Parse(match.Groups["denominator"].Value, NumberStyles.None, CultureInfo.InvariantCulture.NumberFormat);
            return true;
        }

        /// <summary>
        /// 分数形式の文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="Int64"/> 型の分子と分母に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="numerator">変換結果の分子です。</param>
        /// <param name="denominator">変換結果の分母です。</param>
        /// <returns>true である場合は変換に成功したことを意味し、更に <paramref name="numerator"/>および <paramref name="denominator"/> に変換結果が格納されます。
        /// false は変換に失敗したことを意味します。
        /// </returns>
        public static Boolean TryParse(this String s, out Int64 numerator, out Int64 denominator)
        {
            var match = _rationalNumberPattern.Match(s);
            if (!match.Success)
            {
                numerator = default;
                denominator = default;
                return false;
            }

            numerator = match.Groups["numerator"].Value.ParseAsInt64();
            denominator = match.Groups["denominator"].Value.ParseAsInt64();
            return true;
        }

        /// <summary>
        /// 分数形式の文字列で表現された数値を <see cref="CultureInfo.InvariantCulture"/> の書式情報に基づいて <see cref="UInt64"/> 型の分子と分母に変換します。
        /// </summary>
        /// <param name="s">変換元の文字列です。</param>
        /// <param name="numerator">変換結果の分子です。</param>
        /// <param name="denominator">変換結果の分母です。</param>
        /// <returns>true である場合は変換に成功したことを意味し、更に <paramref name="numerator"/>および <paramref name="denominator"/> に変換結果が格納されます。
        /// false は変換に失敗したことを意味します。
        /// </returns>
        public static Boolean TryParse(this String s, out UInt64 numerator, out UInt64 denominator)
        {
            var match = _rationalNumberPattern.Match(s);
            if (!match.Success)
            {
                numerator = default;
                denominator = default;
                return false;
            }

            numerator = UInt64.Parse(match.Groups["numerator"].Value, NumberStyles.None, CultureInfo.InvariantCulture.NumberFormat);
            denominator = UInt64.Parse(match.Groups["denominator"].Value, NumberStyles.None, CultureInfo.InvariantCulture.NumberFormat);
            return true;
        }

        #endregion
    }
}
