using System;
using System.Runtime.CompilerServices;

namespace Palmtree.IO.Console
{
    partial class TinyConsole
    {
        /// <summary>
        /// 現在の行終端記号をコンソールに書き込みます。
        /// </summary>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine()
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine();
        }

        /// <summary>
        /// 指定した <see cref="Object"/> のテキスト形式をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(Object? value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(value);
        }

        /// <summary>
        /// 指定した <see cref="Boolean"/> 値のテキスト形式をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(Boolean value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(value);
        }

        /// <summary>
        /// 指定した UNICODE 文字をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(Char value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(value);
        }

        /// <summary>
        /// 指定した <see cref="Int32"/> 値のテキスト形式をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(Int32 value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(value);
        }

        /// <summary>
        /// 指定した <see cref="UInt32"/> 値のテキスト形式をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(UInt32 value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(value);
        }

        /// <summary>
        /// 指定した <see cref="Int64"/> 値のテキスト形式をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(Int64 value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(value);
        }

        /// <summary>
        /// 指定した <see cref="UInt64"/> 値のテキスト形式をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(UInt64 value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(value);
        }

        /// <summary>
        /// 指定した <see cref="Single"/> 値のテキスト形式をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(Single value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(value);
        }

        /// <summary>
        /// 指定した <see cref="Double"/>値のテキスト形式をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(Double value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(value);
        }

        /// <summary>
        /// 指定した <see cref="Decimal"/> 値のテキスト形式をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(Decimal value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(value);
        }

        /// <summary>
        /// 指定した<see cref="String"/> 値をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(String? value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(value);
        }

        /// <summary>
        /// 指定した UNICODE 文字配列をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="buffer">書き込む UNICODE 文字配列です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(Char[]? buffer)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(buffer);
        }

        /// <summary>
        /// 指定した UNICODE 文字の部分配列をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="buffer">UNICODE 文字の配列です。</param>
        /// <param name="index"><paramref name="buffer"/> 内の開始位置です。</param>
        /// <param name="count">書き込む文字数です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(Char[] buffer, Int32 index, Int32 count)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(buffer, index, count);
        }

        /// <summary>
        /// 指定した書式情報を使用して、指定した <see cref="Object"/> 値のテキスト表現をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="format">複合書式設定文字列です。</param>
        /// <param name="arg0"><paramref name="format"/> を使用して書き込むオブジェクトです。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(String format, Object? arg0)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(format, arg0);
        }

        /// <summary>
        /// 指定した書式情報を使用して、指定した <see cref="Object"/> 値のテキスト表現をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="format">複合書式設定文字列です。</param>
        /// <param name="arg0"><paramref name="format"/> を使用して書き込む最初のオブジェクトです。</param>
        /// <param name="arg1"><paramref name="format"/> を使用して書き込む 2 番目のオブジェクト。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(String format, Object? arg0, Object? arg1)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(format, arg0, arg1);
        }

        /// <summary>
        /// 指定した書式情報を使用して、指定した <see cref="Object"/> 値のテキスト表現をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="format">複合書式設定文字列です。</param>
        /// <param name="arg0"><paramref name="format"/> を使用して書き込む最初のオブジェクトです。</param>
        /// <param name="arg1"><paramref name="format"/> を使用して書き込む 2 番目のオブジェクト。</param>
        /// <param name="arg2"><paramref name="format"/> を使用して書き込む 3 番目のオブジェクトです。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(String format, Object? arg0, Object? arg1, Object? arg2)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// 指定した書式情報を使用して、指定した <see cref="Object"/> 配列のテキスト表現をコンソールに書き込み、続けて現在の行終端記号を書き込みます。
        /// </summary>
        /// <param name="format">複合書式設定文字列です。</param>
        /// <param name="arg"><paramref name="format"/> を使用して書き込むオブジェクトの配列です。</param>
        /// <remarks>
        /// 実際の出力先は以下の通りです。
        /// <list type="bullet">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力ストリーム</item>
        /// <item>標準出力がリダイレクトされている場合は、標準エラー出力ストリーム</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(String format, params Object?[] arg)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleTextWriter.WriteLine(format, arg);
        }
    }
}
