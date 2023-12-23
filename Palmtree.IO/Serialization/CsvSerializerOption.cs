﻿using System;
using System.Linq;

namespace Palmtree.IO.Serialization
{
    /// <summary>
    /// CSV シリアライザのオプションのクラスです。
    /// </summary>
    public class CsvSerializerOption
    {
        private Char _columnDelimiterChar;
        private String _rowDelimiterString;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public CsvSerializerOption()
        {
            _columnDelimiterChar = ',';
            _rowDelimiterString = "\r\n";
        }

        /// <summary>
        /// CSVの列の区切り文字です。既定値は ','(カンマ) です。一般的には ','(カンマ) または '\t'(TAB) を設定します。
        /// </summary>
        public Char ColumnDelimiterChar
        {
            get => _columnDelimiterChar;

            set
            {
                if (_columnDelimiterChar.IsAnyOf('\"', '\r', '\n', '\u001a'))
                    throw new Exception($"The character '\\u{(Int32)value:x4}' cannot be used as a CSV column delimiter.");
                _columnDelimiterChar = value;
            }
        }

        /// <summary>
        /// CSVの改行コードです。既定値は "\r\n"(CRLF) です。"\r\n"(CRLF) / "\r"(CR) / "\n"(LF) の何れかが設定可能です。
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>このプロティの値はデシリアライズ時には参照されません。デシリアライズ時には自動的に改行コードが判別されます。</item>
        /// </list>
        /// </remarks>
        public String RowDelimiterString
        {
            get => _rowDelimiterString;

            set
            {
                if (_rowDelimiterString.IsNoneOf("\r\n", "\n", "\r"))
                    throw new Exception($"The string \"{(String.Concat(value.Select(c => $"\\u{(Int32)c:x4}")))}\" cannot be used as a CSV row delimiter.");
                _rowDelimiterString = value;
            }
        }
    }
}
