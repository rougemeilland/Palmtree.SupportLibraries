using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Palmtree.IO.Serialization
{
    /// <summary>
    /// CSV形式のデータのシリアライズ/デシリアライズを行うクラスです。
    /// </summary>
    public class CsvSerializer
    {
        private const Char _doubleQuoteChar = '\"';
        private const Char _carriageReturnChar = '\r';
        private const Char _lineFeedChar = '\r';
        private const String _twoDoubleQuotesChars = "\"\"";
        private const String _carriageReturnAndLineFeedChars = "\r\n";

        #region シリアライザ

        /// <summary>
        /// CSVデータを文字列へシリアライズしてます。
        /// </summary>
        /// <param name="data">
        /// CSVデータを表す列挙子です。
        /// </param>
        /// <returns>
        /// シリアライズされた文字列です。
        /// </returns>
        public static String Serialize(IEnumerable<IEnumerable<String>> data)
            => Serialize(data, new CsvSerializerOption { });

        /// <summary>
        /// CSVデータを文字列へシリアライズします。
        /// </summary>
        /// <param name="data">
        /// CSVデータを表す列挙子です。
        /// </param>
        /// <param name="option">
        /// シリアライズ方法をカスタマイズするためのオプションです。
        /// </param>
        /// <returns>
        /// シリアライズされた文字列です。
        /// </returns>
        public static String Serialize(IEnumerable<IEnumerable<String>> data, CsvSerializerOption option)
        {
            var stringBuffer = new StringBuilder();
            using (var writer = new StringWriter(stringBuffer, CultureInfo.InvariantCulture))
            {
                Serialize(writer, data, option);
            }

            return stringBuffer.ToString();
        }

        /// <summary>
        /// CSVデータをシリアライズしてストリームに書き込みます。
        /// </summary>
        /// <param name="writer">
        /// シリアライズ結果の出力先となる <see cref="TextWriter"/> オブジェクトです。
        /// </param>
        /// <param name="data">
        /// CSVデータを表す列挙子です。
        /// </param>
        public static void Serialize(TextWriter writer, IEnumerable<IEnumerable<String>> data)
            => Serialize(writer, data, new CsvSerializerOption { });

        /// <summary>
        /// CSVデータをシリアライズしてストリームに書き込みます。
        /// </summary>
        /// <param name="writer">
        /// シリアライズ結果の出力先となる <see cref="TextWriter"/> オブジェクトです。
        /// </param>
        /// <param name="data">
        /// CSVデータを表す列挙子です。
        /// </param>
        /// <param name="option">
        /// シリアライズ方法をカスタマイズするためのオプションです。
        /// </param>
        public static void Serialize(TextWriter writer, IEnumerable<IEnumerable<String>> data, CsvSerializerOption option)
        {
            foreach (var row in data)
            {
                writer.Write(SerializeRow(row, option));
                writer.Write(option.RowDelimiterString);
            }

            writer.Flush();
        }

        private static String SerializeRow(IEnumerable<String> row, CsvSerializerOption option)
            => String.Join(
                option.ColumnDelimiterChar,
                row.Select(column => SerializeColumn(column, option)));

        private static String SerializeColumn(String column, CsvSerializerOption option)
            => column.IndexOfAny(new[] { option.ColumnDelimiterChar, _doubleQuoteChar, _carriageReturnChar, _lineFeedChar }) < 0
                ? column
                : $"{_doubleQuoteChar}{column.Replace(_doubleQuoteChar.ToString(), _twoDoubleQuotesChars)}{_doubleQuoteChar}";

        #endregion

        #region デシリアライザ

        /// <summary>
        /// 文字列からCSVデータへデシリアライズします。
        /// </summary>
        /// <param name="csvText">
        /// CSVデータを表す文字列です。
        /// </param>
        /// <returns>
        /// CSVデータを表す列挙子です。
        /// </returns>
        public static IEnumerable<IEnumerable<String>> Deserialize(String csvText)
            => Deserialize(csvText, new CsvSerializerOption { }).ToArray();

        /// <summary>
        /// 文字列からCSVデータへデシリアライズします。
        /// </summary>
        /// <param name="csvText">
        /// CSVデータを表す文字列です。
        /// </param>
        /// <param name="option">
        /// デシリアライズ方法をカスタマイズするためのオプションです。
        /// </param>
        /// <returns>
        /// CSVデータを表す列挙子です。
        /// </returns>
        public static IEnumerable<IEnumerable<String>> Deserialize(String csvText, CsvSerializerOption option)
            => Deserialize(new StringReader(csvText), option).ToArray();

        /// <summary>
        /// ストリームからテキストを読み込んでCSVデータへデシリアライズします。
        /// </summary>
        /// <param name="reader">
        /// CSVテキストを読み込むための <see cref="TextReader"/> です。
        /// </param>
        /// <returns>
        /// CSVデータを表す列挙子です。
        /// </returns>
        public static IEnumerable<IEnumerable<String>> Deserialize(TextReader reader)
            => Deserialize(reader, new CsvSerializerOption { }).ToArray();

        /// <summary>
        /// ストリームからテキストを読み込んでCSVデータへデシリアライズします。
        /// </summary>
        /// <param name="reader">
        /// CSVテキストを読み込むための <see cref="TextReader"/> です。
        /// </param>
        /// <param name="option">
        /// デシリアライズ方法をカスタマイズするためのオプションです。
        /// </param>
        /// <returns>
        /// CSVデータを表す列挙子です。
        /// </returns>
        public static IEnumerable<IEnumerable<String>> Deserialize(TextReader reader, CsvSerializerOption option)
            => DeserializeRows(new BuffetredTextReader(reader, 2), option).ToArray();

        private static IEnumerable<IEnumerable<String>> DeserializeRows(BuffetredTextReader reader, CsvSerializerOption option)
        {
            try
            {
                while (!reader.IsEndOfReader)
                {
                    var row = DeserializeRow(reader, option).ToArray();
                    if (row.Length > 0)
                        yield return row;
                }
            }
            finally
            {
                reader.Dispose();
            }
        }

        private static IEnumerable<String> DeserializeRow(BuffetredTextReader reader, CsvSerializerOption option)
        {
            while (!reader.IsEndOfReader)
            {
                if (reader.StartsWith(_carriageReturnAndLineFeedChars))
                {
                    _ = reader.Read();
                    _ = reader.Read();
                    break;
                }
                else if (reader.StartsWith(_carriageReturnChar) || reader.StartsWith(_lineFeedChar))
                {
                    _ = reader.Read();
                    break;
                }
                else
                {
                    yield return
                        reader.StartsWith(_doubleQuoteChar)
                        ? DeserializeQuotedColumn(reader, option)
                        : DeserializeUnquotedColumn(reader, option);
                }
            }
        }

        private static String DeserializeQuotedColumn(BuffetredTextReader reader, CsvSerializerOption option)
        {
            Validation.Assert(reader.StartsWith(_doubleQuoteChar), "reader.StartsWith(_doubleQuoteChar)");

            // 先頭のダブルクォートを読み捨てる
            _ = reader.Read();

            var columnString = new StringBuilder();
            var quotedMode = true;

            while (!reader.IsEndOfReader)
            {
                if (quotedMode)
                {
                    // ダブルクォート区間内の場合

                    if (reader.StartsWith(_twoDoubleQuotesChars))
                    {
                        // 先頭が連続したダブルクォートである場合

                        // 2つのダブルクォートを読み捨てて、1つのダブルクォートをカラム文字列に追加する。
                        _ = reader.Read();
                        _ = reader.Read();
                        _ = columnString.Append(_doubleQuoteChar);
                    }
                    else if (reader.StartsWith(_doubleQuoteChar))
                    {
                        // 先頭が単独のダブルクォートである場合

                        // ダブルクォートを読み捨てて、ダブルクォート区間を終える。
                        _ = reader.Read();
                        quotedMode = false;
                    }
                    else
                    {
                        // 先頭がダブルクォートではない場合

                        // 1文字を読み込む
                        var c =
                            reader.Read()
                            ?? throw new EndOfStreamException(); // ダブルクォートで閉じられずに EOF に達したため、エラーとする

                        // 先頭の1文字をカラム文字列に追加する
                        _ = columnString.Append(c);
                    }
                }
                else
                {
                    // ダブルクォート区間外の場合
                    // 再びダブルクォートが見つかっても通常の文字として扱う

                    if (reader.StartsWith(option.ColumnDelimiterChar))
                    {
                        // 先頭がカラムの区切り文字である場合

                        // 区切り文字を読み捨ててループを脱出する
                        _ = reader.Read();
                        break;
                    }
                    else if (reader.StartsWith(_carriageReturnChar) || reader.StartsWith(_lineFeedChar))
                    {
                        // 先頭が改行である場合

                        // ループを脱出する
                        break;
                    }
                    else
                    {
                        // 先頭がカラムの区切り文字でも改行でもない場合

                        // 1文字を読み込む
                        var c = reader.Read();
                        if (c is null)
                        {
                            // EOF を検出した場合

                            // カラムの終わりとみなす。
                            break;
                        }

                        // 先頭の1文字をカラム文字列に追加する
                        _ = columnString.Append(c.Value);
                    }
                }
            }

            var columnValue = columnString.ToString();
            return columnValue;
        }

        private static String DeserializeUnquotedColumn(BuffetredTextReader reader, CsvSerializerOption option)
        {
            var columnString = new StringBuilder();

            while (!reader.IsEndOfReader)
            {
                if (reader.StartsWith(option.ColumnDelimiterChar))
                {
                    // 先頭がカラムの区切り文字である場合

                    // 区切り文字を読み捨ててループを脱出する
                    _ = reader.Read();
                    break;
                }
                else if (reader.StartsWith(_carriageReturnChar) || reader.StartsWith(_lineFeedChar))
                {
                    // 先頭が改行である場合

                    // ループを脱出する
                    break;
                }
                else
                {
                    // 先頭がカラムの区切り文字でも改行でもない場合

                    // 1文字を読み込む
                    var c = reader.Read();
                    if (c is null)
                    {
                        // EOF を検出した場合

                        // カラムの終わりとみなす。
                        break;
                    }

                    // 先頭の1文字をカラム文字列に追加する
                    _ = columnString.Append(c.Value);
                }
            }

            var columnValue = columnString.ToString();
            return columnValue;
        }

        #endregion
    }
}
