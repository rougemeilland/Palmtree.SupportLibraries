using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Palmtree.IO.Console
{
    public static partial class TinyConsole
    {
        /// <summary>
        /// 指定した <see cref="Object"/> のテキスト形式をコンソールに書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Object? value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(value);
        }

        /// <summary>
        /// 指定した <see cref="Boolean"/> 値のテキスト形式をコンソールに書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Boolean value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(value);
        }

        /// <summary>
        /// 指定した UNICODE 文字値をコンソールに書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Char value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(value);
        }

        /// <summary>
        /// 指定した <see cref="Int32"/> 値のテキスト形式をコンソールに書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Int32 value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(value);
        }

        /// <summary>
        /// 指定した <see cref="UInt32"/> 値のテキスト形式をコンソールに書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(UInt32 value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(value);
        }

        /// <summary>
        /// 指定した <see cref="Int64"/> 値のテキスト形式をコンソールに書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Int64 value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(value);
        }

        /// <summary>
        /// 指定した <see cref="UInt64"/> 値のテキスト形式をコンソールに書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(UInt64 value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(value);
        }

        /// <summary>
        /// 指定した <see cref="Single"/> 値のテキスト形式をコンソールに書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Single value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(value);
        }

        /// <summary>
        /// 指定した <see cref="Double"/> 値のテキスト形式をコンソールに書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Double value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(value);
        }

        /// <summary>
        /// 指定した <see cref="Decimal"/> 値のテキスト形式をコンソールに書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Decimal value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(value);
        }

        /// <summary>
        /// 指定した <see cref="String"/> 値のテキスト形式をコンソールに書き込みます。
        /// </summary>
        /// <param name="value">書き込む値です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(String? value)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(value);
        }

        /// <summary>
        /// 指定した UNICODE 文字配列をコンソールに書き込みます。
        /// </summary>
        /// <param name="buffer">Unicode 文字配列です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Char[]? buffer)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(buffer);
        }

        /// <summary>
        /// 指定した UNICODE 文字の部分配列をコンソールに書き込みます。
        /// </summary>
        /// <param name="buffer">UNICODE 文字の配列です。</param>
        /// <param name="index"><paramref name="buffer"/> 内の開始位置です。</param>
        /// <param name="count">書き込む文字数です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Char[] buffer, Int32 index, Int32 count)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(buffer, index, count);
        }

        /// <summary>
        /// 指定した UNICODE 文字配列をコンソールに書き込みます。
        /// </summary>
        /// <param name="buffer">Unicode 文字配列です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(ReadOnlySpan<Char> buffer)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(buffer);
        }

        /// <summary>
        /// 指定した書式情報を使用して、指定した <see cref="Object"/> 値のテキスト表現をコンソールに書き込みます。
        /// </summary>
        /// <param name="format">複合書式設定文字列です。</param>
        /// <param name="arg0"><paramref name="format"/> を使用して書き込むオブジェクトです。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write([StringSyntax(StringSyntaxAttribute.CompositeFormat)] String format, Object? arg0)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(format, arg0);
        }

        /// <summary>
        /// 指定した書式情報を使用して、指定した <see cref="Object"/> 値のテキスト表現をコンソールに書き込みます。
        /// </summary>
        /// <param name="format">複合書式設定文字列です。</param>
        /// <param name="arg0"><paramref name="format"/> を使用して書き込む最初のオブジェクトです。</param>
        /// <param name="arg1"><paramref name="format"/> を使用して書き込む 2 番目のオブジェクトです。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write([StringSyntax(StringSyntaxAttribute.CompositeFormat)] String format, Object? arg0, Object? arg1)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(format, arg0, arg1);
        }

        /// <summary>
        /// 指定した書式情報を使用して、指定した <see cref="Object"/> 値のテキスト表現をコンソールに書き込みます。
        /// </summary>
        /// <param name="format">複合書式設定文字列です。</param>
        /// <param name="arg0"><paramref name="format"/> を使用して書き込む最初のオブジェクトです。</param>
        /// <param name="arg1"><paramref name="format"/> を使用して書き込む 2 番目のオブジェクトです。</param>
        /// <param name="arg2"><paramref name="format"/> を使用して書き込む 3 番目のオブジェクトです。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write([StringSyntax(StringSyntaxAttribute.CompositeFormat)] String format, Object? arg0, Object? arg1, Object? arg2)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// 指定された書式情報を使用して、指定した <see cref="Object"/> 配列のテキスト表現をコンソールに書き込みます。
        /// </summary>
        /// <param name="format">複合書式設定文字列です。</param>
        /// <param name="arg"><paramref name="format"/> を使用して書き込むオブジェクトの配列です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write([StringSyntax(StringSyntaxAttribute.CompositeFormat)] String format, params Object?[] arg)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(format, arg);

        }

#if NET9_0_OR_GREATER
        /// <summary>
        /// 指定された書式情報を使用して、指定した <see cref="Object"/> 配列のテキスト表現をコンソールに書き込みます。
        /// </summary>
        /// <param name="format">複合書式設定文字列です。</param>
        /// <param name="arg"><paramref name="format"/> を使用して書き込むオブジェクトの配列です。</param>
        /// <remarks>
        /// 実際の出力先は以下の優先順位で決定します。
        /// <list type="number">
        /// <item>標準出力がリダイレクトされていない場合は、標準出力に出力する。</item>
        /// <item>標準エラー出力がリダイレクトされていない場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardOutput"/> である場合は、標準出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.StandardError"/> である場合は、標準エラー出力に出力する。</item>
        /// <item>プロパティ <see cref="DefaultTextWriter"/> の値が <see cref="ConsoleTextWriterType.None"/> である場合は、出力しない</item>
        /// </list>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write([StringSyntax(StringSyntaxAttribute.CompositeFormat)] String format, params ReadOnlySpan<Object?> arg)
        {
            SetCharacterSet(CharacterSet.Primary);
            _consoleOutputState.Value.ConsoleTextWriter.Write(format, arg);
        }
#endif

        /// <summary>
        /// 代替文字 (グラフィックス文字) をコンソールに書き込みます。
        /// </summary>
        /// <param name="altChar">
        /// 代替文字である <see cref="AlternativeChar"/> 値です。
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準エラー出力の両方がリダイレクトされています。</item>
        /// </list>
        /// </exception>
        /// <remarks>
        /// <list type="bullet">
        /// ターミナルによってはすべての代替文字がサポートされているとは限りません。ターミナルがサポートしていない代替文字は'?'として表示されます。
        /// </list>
        /// </remarks>
        public static void Write(AlternativeChar altChar)
        {
            var escapeCodeWriter =
                _consoleOutputState.Value.EscapeCodeWriter
                ?? throw new InvalidOperationException("Since both standard error output is redirected, the alternate characters cannot be displayed.");

            var key = (Char)altChar;
            var alternativeCharacterSetMap = _alternativeCharacterSetMap.Value;
            var c =
                key.InRange(_alternativeCharacterSetMapMinimumKey, (Char)(_alternativeCharacterSetMapMinimumKey + alternativeCharacterSetMap.Length))
                ? alternativeCharacterSetMap[key - _alternativeCharacterSetMapMinimumKey]
                : '\0';
            if (c == '\0')
            {
                SetCharacterSet(CharacterSet.Primary);
                escapeCodeWriter.Write('?');
            }
            else
            {
                SetCharacterSet(CharacterSet.Alternative);
                escapeCodeWriter.Write(c);
            }
        }
    }
}
