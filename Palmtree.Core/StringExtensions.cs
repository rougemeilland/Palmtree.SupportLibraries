using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;

namespace Palmtree
{
    public static partial class StringExtensions
    {
        private static readonly Char[] _delimiterOfCommandParameters = [' ', '\t'];

        static StringExtensions()
        {
#if DEBUG
            Validation.Assert('\u0007' == '\a');
            Validation.Assert('\u0008' == '\b');
            Validation.Assert('\u0009' == '\t');
            Validation.Assert('\u000a' == '\n');
            Validation.Assert('\u000b' == '\v');
            Validation.Assert('\u000c' == '\f');
            Validation.Assert('\u000c' == '\f');
            Validation.Assert('\u000d' == '\r');
#endif
        }

        /// <summary>
        /// 指定した文字列から、指定した文字に一致しない文字を検索します。
        /// </summary>
        /// <param name="s">検索対象の文字列です。</param>
        /// <param name="c">検索時に一致しない文字です。</param>
        /// <param name="startIndex">検索の開始位置です。</param>
        /// <returns>
        /// 以下の条件を満たす文字の、文字列の先頭からの位置を返します。もしそのような文字が見つからなかった場合は負の整数を返します。
        /// <list type="bullet">
        /// <item>文字列 <paramref name="s"/> の位置 <paramref name="startIndex"/> 以降の文字であり、かつ</item>
        /// <item>文字が <paramref name="c"/> と一致せず、かつ</item>
        /// <item>文字列 <paramref name="s"/> の先頭方向から最初に見つかった文字</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> の値が負かまたは文字列 <paramref name="s"/> の長さを超えています。
        /// </exception>
        public static Int32 IndexOfNot(this String s, Char c, Int32 startIndex = 0)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, s.Length);

            for (var index = startIndex; index < s.Length; ++index)
            {
                if (s[index] != c)
                    return index;
            }

            return -1;
        }

        #region IndexOfNotAny

        /// <summary>
        /// 指定した文字列から、指定した文字の何れにも一致しない文字を検索します。
        /// </summary>
        /// <param name="s">検索対象の文字列です。</param>
        /// <param name="characters">検索時に一致しない文字です。</param>
        /// <returns>
        /// 以下の条件を満たす文字の、文字列の先頭からの位置を返します。もしそのような文字が見つからなかった場合は負の整数を返します。
        /// <list type="bullet">
        /// <item>文字が文字配列 <paramref name="characters"/> の要素の何れとも一致せず、かつ</item>
        /// <item>文字列 <paramref name="s"/> の先頭から最初に見つかった文字</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> の値が負かまたは文字列 <paramref name="s"/> の長さを超えています。
        /// </exception>
        public static Int32 IndexOfNotAny(this String s, params Char[] characters) => s.IndexOfNotAny(characters, 0);

        /// <summary>
        /// 指定した文字列から、指定した文字の何れにも一致しない文字を検索します。
        /// </summary>
        /// <param name="s">検索対象の文字列です。</param>
        /// <param name="characters">検索時に一致しない文字です。</param>
        /// <param name="startIndex">検索の開始位置です。</param>
        /// <returns>
        /// 以下の条件を満たす文字の、文字列の先頭からの位置を返します。もしそのような文字が見つからなかった場合は負の整数を返します。
        /// <list type="bullet">
        /// <item>文字列 <paramref name="s"/> の位置 <paramref name="startIndex"/> 以降の文字であり、かつ</item>
        /// <item>文字が文字配列 <paramref name="characters"/> の要素の何れとも一致せず、かつ</item>
        /// <item>文字列 <paramref name="s"/> の先頭方向から最初に見つかった文字</item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex"/> の値が負かまたは文字列 <paramref name="s"/> の長さを超えています。
        /// </exception>
        public static Int32 IndexOfNotAny(this String s, Char[] characters, Int32 startIndex = 0)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(startIndex);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startIndex, s.Length);

            for (var index = startIndex; index < s.Length; ++index)
            {
                if (!characters.Contains(s[index]))
                    return index;
            }

            return -1;
        }

        #endregion

        #region ChunkAsString

        public static IEnumerable<String> ChunkAsString(this IEnumerable<Char> source, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

            var sb = new StringBuilder();
            foreach (var c in source)
            {
                _ = sb.Append(c);
                if (sb.Length >= count)
                {
                    yield return sb.ToString();
                    _ = sb.Clear();
                }
            }
        }

        #endregion

        #region Slice

        public static ReadOnlyMemory<Char> Slice(this String sourceString, Int32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceString);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceString.Length);

            return (ReadOnlyMemory<Char>)sourceString[offset..].ToCharArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<Char> Slice(this String sourceString, UInt32 offset)
        {
            ArgumentNullException.ThrowIfNull(sourceString);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, (UInt32)sourceString.Length);

            return sourceString.Slice(checked((Int32)offset));
        }

        public static ReadOnlyMemory<Char> Slice(this String sourceString, Range range)
        {
            ArgumentNullException.ThrowIfNull(sourceString);
            var sourceArray = sourceString.ToCharArray();

            var (offset, count) = sourceArray.GetOffsetAndLength(range);
            return sourceString.Substring(offset, count).ToCharArray();
        }

        public static ReadOnlyMemory<Char> Slice(this String sourceString, Int32 offset, Int32 count)
        {
            ArgumentNullException.ThrowIfNull(sourceString);
            ArgumentOutOfRangeException.ThrowIfNegative(offset);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, sourceString.Length);
            ArgumentOutOfRangeException.ThrowIfNegative(count);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(count, sourceString.Length - offset);

            return (ReadOnlyMemory<Char>)sourceString.Substring(offset, count).ToCharArray();
        }

        public static ReadOnlyMemory<Char> Slice(this String sourceString, UInt32 offset, UInt32 count)
            => sourceString.Slice(checked((Int32)offset), checked((Int32)count));

        #endregion

        /// <summary>
        /// 指定された文字列を JSON 形式でエンコードします。
        /// </summary>
        /// <param name="s">エンコード対象の文字列です。</param>
        /// <returns>エンコードされた文字列です。</returns>
        public static String JsonEncode(this String s)
        {
            ArgumentNullException.ThrowIfNull(s);

            return
                String.Concat(
                    s.Select(c =>
                        c switch
                        {
                            '\u0000' or '\u0001' or '\u0002' or '\u0003' or '\u0004' or '\u0005' or '\u0006' or '\u0007' or '\u000b' or '\u000e' or '\u000f' or '\u0010' or '\u0011' or '\u0012' or '\u0013' or '\u0014' or '\u0015' or '\u0016' or '\u0017' or '\u0018' or '\u0019' or '\u001a' or '\u001b' or '\u001c' or '\u001d' or '\u001e' or '\u001f' or '\u007f'
                                => $"\\u{(Int32)c:x4}",
                            '\u0008' => "\\b",
                            '\u0009' => "\\t",
                            '\u000a' => "\\n",
                            '\u000c' => "\\f",
                            '\u000d' => "\\r",
                            '\"' => "\\\"",
                            '\\' => "\\\\",
                            '/' => "\\/",
                            _ => c.ToString(),
                        }));
        }

        /// <summary>
        /// 指定された文字列を C# の文字列リテラル形式でエンコードします。
        /// </summary>
        /// <param name="s">エンコード対象の文字列です。</param>
        /// <returns>エンコードされた文字列です。</returns>
        public static String CSharpEncode(this String s)
        {
            ArgumentNullException.ThrowIfNull(s);

            return
                String.Concat(
                    s.Select(c =>
                        c switch
                        {
                            '\u0000' or '\u0001' or '\u0002' or '\u0003' or '\u0004' or '\u0005' or '\u0006' or '\u000e' or '\u000f' or '\u0010' or '\u0011' or '\u0012' or '\u0013' or '\u0014' or '\u0015' or '\u0016' or '\u0017' or '\u0018' or '\u0019' or '\u001a' or '\u001b' or '\u001c' or '\u001d' or '\u001e' or '\u001f' or '\u007f'
                                => $"\\u{(Int32)c:x4}",
                            '\u0007' => "\\a",
                            '\u0008' => "\\b",
                            '\u0009' => "\\t",
                            '\u000a' => "\\n",
                            '\u000b' => "\\v",
                            '\u000c' => "\\f",
                            '\u000d' => "\\r",
                            '\"' => "\\\"",
                            '\\' => "\\\\",
                            _ => c.ToString(),
                        }));
        }

        /// <summary>
        /// 指定された文字列をコマンドラインの引数の形式でエンコードします。
        /// </summary>
        /// <param name="arg">エンコード対象の文字列です。</param>
        /// <returns>エンコードされた文字列です。</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>
        /// <term>注意事項</term>
        /// <description>
        /// <para>
        /// このメソッドは、以下のメカニズムで行われるコマンドラインのエンコーディングの実装に準拠しており、コマンドプロンプトやPowerShellを含めたシェル固有のエンコーディングには対応していません。
        /// <list type="bullet">
        /// <item><see cref="System.Diagnostics.Process.Start()"/></item>
        /// <item><see cref="Environment.GetCommandLineArgs()"/></item>
        /// </list>
        /// </para>
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        public static String EncodeCommandLineArgument(this String arg)
        {
            ArgumentNullException.ThrowIfNull(arg);

            arg = GetBackSlashAndDoubleQuotePattern().Replace(arg, @"\${backslash}$&");
            if (arg.Length > 0 && arg.IndexOfAny(_delimiterOfCommandParameters) < 0)
                return arg;
            return $"\"{GetEndsWithBackSlashPattern().Replace(arg, "${backslash}${backslash}")}\"";
        }

        /// <summary>
        /// 指定された文字列をコマンドラインの引数の形式でデコードします。
        /// </summary>
        /// <param name="arg">デコード対象の文字列です。</param>
        /// <returns>デコードされた文字列です。</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>
        /// <term>注意事項</term>
        /// <description>
        /// <para>
        /// このメソッドは、以下のメカニズムで行われるコマンドラインのエンコーディングの実装に準拠しており、コマンドプロンプトやPowerShellを含めたシェル固有のエンコーディングには対応していません。
        /// <list type="bullet">
        /// <item><see cref="System.Diagnostics.Process.Start()"/></item>
        /// <item><see cref="Environment.GetCommandLineArgs()"/></item>
        /// </list>
        /// </para>
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        public static String DecodeCommandLineArgument(this String arg)
        {
            ArgumentNullException.ThrowIfNull(arg);

            try
            {
                var nextPos = ParseCommandLine(arg, 0, out var decodedArg);
                if (nextPos != arg.Length)
                    throw new ArgumentException("Command parameter delimiters (spaces, tabs, newlines, etc.) are not quoted.", nameof(arg));
                return decodedArg;
            }
            catch (FormatException ex)
            {
                throw new ArgumentException($"The command line syntax is incorrect.: \"{arg}\"", nameof(arg), ex);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to parse command line.: \"{arg}\"", ex);
            }
        }

        /// <summary>
        /// コマンドラインの文字列をコマンドとパラメタに切り分けます。
        /// </summary>
        /// <param name="commandLine">コマンドラインの文字列です。</param>
        /// <returns>
        /// <para>
        /// 切り分けられたコマンドラインの要素を表す構造体の列挙子です。
        /// 最初の要素はコマンドのパス名を表し、2番目以降の要素は各コマンドパラメタを表します。
        /// </para>
        /// <para>
        /// 列挙子の要素である構造体の各メンバーは以下の意味を持ちます。
        /// <list type="bullet">
        /// <item>
        /// <term>element</term>
        /// <description>切り分けられた <paramref name="commandLine"/> の一部です。</description>
        /// </item>
        /// <item>
        /// <term>start</term>
        /// <description> element に対応する <paramref name="commandLine"/> 上の開始位置です。 </description>
        /// </item>
        /// <item>
        /// <term>end</term>
        /// <description> element の次の文字に対応する <paramref name="commandLine"/> 上の位置です。 </description>
        /// </item>
        /// </list>
        /// </para>
        /// </returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>
        /// <term>注意事項</term>
        /// <description>
        /// <para>
        /// このメソッドは、以下のメカニズムで行われるコマンドラインのエンコーディングの実装に準拠しており、コマンドプロンプトやPowerShellを含めたシェル固有のエンコーディングには対応していません。
        /// <list type="bullet">
        /// <item><see cref="System.Diagnostics.Process.Start()"/></item>
        /// <item><see cref="Environment.GetCommandLineArgs()"/></item>
        /// </list>
        /// </para>
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        public static IEnumerable<(String arg, Int32 start, Int32 end)> SplitCommandLineArguments(this String commandLine)
        {
            ArgumentNullException.ThrowIfNull(commandLine);

            var first = true;
            for (var index = 0; index < commandLine.Length;)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    index = commandLine.SkipCommandLineDelimiter(index);
                    if (index >= commandLine.Length)
                        break;
                }

                var start = index;
                index = GetToken(commandLine, index, out var element);
                if (index < 0)
                    break;
                yield return (element, start, index);
            }

            static Int32 GetToken(String commandLine, Int32 startAt, out String token)
            {
                try
                {
                    return commandLine.ParseCommandLine(startAt, out token);
                }
                catch (FormatException ex)
                {
                    throw new ArgumentException($"The command line syntax is incorrect.: \"{commandLine}\"", nameof(commandLine), ex);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Failed to parse command line.: \"{commandLine}\"", ex);
                }
            }
        }

        /// <summary>
        /// 指定された文字列をコマンドプロンプトのコマンドラインの引数の形式でエンコードします。
        /// </summary>
        /// <param name="arg">エンコード対象の文字列です。</param>
        /// <returns>エンコードされた文字列です。</returns>
        /// <remarks>
        /// <para>
        /// このメソッドは、コマンドプロンプトの "/c" あるいは "/k" オプションに続くコマンド文字列に適用される特殊なエスケープ規則に従って、文字列をエンコードします。
        /// </para>
        /// <para>
        /// このメソッドは 以下の条件を満たす場合にのみ使用してください。
        /// </para>
        /// <list type="number">
        /// <item>対象オペレーティングシステムが Windows のみであり、かつ</item>
        /// <item>エンコード対象文字列がコマンドプロンプト (cmd.exe) の "/c" または "/k" オプションに続くコマンド文字列の一部であり、かつ</item>
        /// <item>エンコード対象文字列に含まれる文字が、コマンドプロンプトの構文上の特殊文字として扱われることが望ましくない場合。</item>
        /// </list>
        /// <para>
        /// 例えば、以下のようなプロセスを実行したい場合にはこのメソッドを利用する必要があります。
        /// </para>
        /// <code>
        ///     cmd.exe /c "chcp 65001&amp;&amp;&lt;command name&gt; &lt;command argument 1&gt; &lt;command argument 2&gt; ... "
        /// </code>
        /// <para>
        /// 上記の例では、コマンドプロンプト上で、コンソールのコードページを UTF-8 に変更した上で、&lt;command name&gt; を実行します。
        /// この例では、実行したいコマンドの名前 &lt;command name&gt; およびそのパラメタ &lt;command argument 1&gt;,  &lt;command argument 2&gt;, ... の何れかに以下の何れかの文字が含まれている可能性がある場合、それらをこのメソッドでエンコードする必要があります。
        /// </para>
        /// <list type="bullet">
        /// <item>コマンドプロンプトの構文上の特殊文字 ('&amp;', '&lt;', '&gt;', '^', '|') の何れか</item>
        /// <item>通常のコマンド引数でエスケープされなければならない文字 (空白、TAB、ダブルクォート) の何れか</item> 
        /// </list>
        /// </remarks>
        [SupportedOSPlatform("windows")]
        public static String EncodeCommandPromptCommandLineArgument(this String arg)
        {
            ArgumentNullException.ThrowIfNull(arg);

            arg = GetCharacterEscapedAtCaretPattern().Replace(arg, @"^${specialCharacter}");
            arg = GetBackSlashAndDoubleQuotePattern().Replace(arg, @"\${backslash}$&");
            if (arg.Length > 0 && arg.IndexOfAny(_delimiterOfCommandParameters) < 0)
                return arg;
            arg = GetEndsWithBackSlashPattern().Replace(arg, "${backslash}${backslash}");
            return $"^\"{arg}^\"";
        }

        /// <summary>
        /// 指定した文字列の英数字記号を半角文字に置換します。
        /// </summary>
        /// <param name="s">
        /// 置換する文字列を示す <see cref="String"/> オブジェクトです。
        /// </param>
        /// <returns>
        /// 置換された文字列を示す <see cref="String"/> オブジェクトです。
        /// </returns>
        public static String ToNarrow(this String s)
        {
            ArgumentNullException.ThrowIfNull(s);

            var sb = new StringBuilder();
            foreach (var c in s)
            {
                _ = sb.Append(
                    c switch
                    {
                        '　' => ' ',
                        '！' => '!',
                        '”' => '"',
                        '＃' => '#',
                        '＄' => '$',
                        '％' => '%',
                        '＆' => '&',
                        '’' => '\'',
                        '（' => '(',
                        '）' => ')',
                        '＊' => '*',
                        '＋' => '+',
                        '，' => ',',
                        '‐' => '-',
                        '．' => '.',
                        '／' => '/',
                        >= '０' and <= '９' => (Char)(c - '０' + '0'),
                        '：' => ':',
                        '；' => ';',
                        '＜' => '<',
                        '＝' => '=',
                        '＞' => '>',
                        '？' => '?',
                        '＠' => '@',
                        >= 'Ａ' and <= 'Ｚ' => (Char)(c - 'Ａ' + 'A'),
                        '［' => '[',
                        '＼' => '\\',
                        '］' => ']',
                        '＾' => '^',
                        '＿' => '_',
                        '‘' => '`',
                        >= 'ａ' and <= 'ｚ' => (Char)(c - 'ａ' + 'a'),
                        '｛' => '{',
                        '｜' => '|',
                        '｝' => '}',
                        '～' => '~',
                        _ => c,
                    });
            }

            return sb.ToString();
        }

        /// <summary>
        /// 指定された文字列を Windows のファイルシステムで使用可能な形式でエンコードします。
        /// </summary>
        /// <param name="s">エンコード対象の文字列です。</param>
        /// <returns>エンコードされた文字列です。</returns>
        public static String WindowsFileNameEncoding(this String s)
        {
            ArgumentNullException.ThrowIfNull(s);

            var pathName =
                String.Concat(
                    // 少なくとも '?' を 1 つを含む連続した '!' または '?' のシーケンスは、すべて全角文字に変換する。
                    GetQuestionMarksAndExclamationMarksSequencePattern().Replace(
                        s,
                        m =>
                            String.Concat(
                                m.Value
                                .Select(c =>
                                    c switch
                                    {
                                        '?' => '？',
                                        '!' => '！',
                                        _ => c,
                                    })))
                    .Select(c =>
                        c switch
                        {
                            '\\' => '＼',
                            '/' => '／',
                            ':' => '：',
                            '*' => '＊',
                            '?' => '？',
                            '"' => '”',
                            '<' => '＜',
                            '>' => '＞',
                            '|' => '｜',
                            _ => c,
                        }));

            if (pathName.EndsWith('.'))
                pathName = pathName[..^1];
            return pathName.Trim();
        }

        public static String? GetLeadingCommonPart(this String? s1, String? s2, Boolean ignoreCase = false)
        {
            if (s1 is null)
                return s2;
            if (s2 is null)
                return s1;
            if (s1.Length == 0 || s2.Length == 0)
                return "";
            if (s1.Length > s2.Length)
                (s2, s1) = (s1, s2);
#if DEBUG
            Validation.Assert(s1.Length <= s2.Length);
#endif
            var found =
                s1
                .Zip(s2, (c1, c2) => new { c1, c2 })
                .Select((item, index) => new { item.c1, item.c2, index })
                .FirstOrDefault(item => !CharacterEqual(item.c1, item.c2, ignoreCase));
            return
                found is not null
                ? s1[..found.index]
                : s1;
        }

        public static String? GetTrailingCommonPart(this String? s1, String? s2, Boolean ignoreCase = false)
        {
            if (s1 is null)
                return s2;
            if (s2 is null)
                return s1;
            if (s1.Length == 0 || s2.Length == 0)
                return "";
            if (s1.Length > s2.Length)
                (s2, s1) = (s1, s2);
#if DEBUG
            Validation.Assert(s1.Length <= s2.Length);
#endif
            var found =
                s1.Reverse()
                .Zip(s2.Reverse(), (c1, c2) => new { c1, c2 })
                .Select((item, index) => new { item.c1, item.c2, index })
                .FirstOrDefault(item => !CharacterEqual(item.c1, item.c2, ignoreCase));
            return
                found is not null
                ? s1.Substring(s1.Length - found.index, found.index)
                : s1;
        }

        #region IsNoneOf

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf(this String s, String s1, String s2, StringComparison stringComparison = StringComparison.Ordinal)
            => !s.IsAnyOf(s1, s2, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf(this String s, String s1, String s2, String s3, StringComparison stringComparison = StringComparison.Ordinal)
            => !s.IsAnyOf(s1, s2, s3, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf(this String s, String s1, String s2, String s3, String s4, StringComparison stringComparison = StringComparison.Ordinal)
            => !s.IsAnyOf(s1, s2, s3, s4, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf(this String s, String s1, String s2, String s3, String s4, String s5, StringComparison stringComparison = StringComparison.Ordinal)
            => !s.IsAnyOf(s1, s2, s3, s4, s5, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf(this String s, String s1, String s2, String s3, String s4, String s5, String s6, StringComparison stringComparison = StringComparison.Ordinal)
            => !s.IsAnyOf(s1, s2, s3, s4, s5, s6, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf(this String s, String s1, String s2, String s3, String s4, String s5, String s6, String s7, StringComparison stringComparison = StringComparison.Ordinal)
            => !s.IsAnyOf(s1, s2, s3, s4, s5, s6, s7, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNoneOf(this String s, String s1, String s2, String s3, String s4, String s5, String s6, String s7, String s8, StringComparison stringComparison = StringComparison.Ordinal)
            => !s.IsAnyOf(s1, s2, s3, s4, s5, s6, s7, s8, stringComparison);

        #endregion

        #region IsAnyOf

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf(this String s, String s1, String s2, StringComparison stringComparison = StringComparison.Ordinal)
            => String.Equals(s, s1, stringComparison)
                || String.Equals(s, s2, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf(this String s, String s1, String s2, String s3, StringComparison stringComparison = StringComparison.Ordinal)
            => String.Equals(s, s1, stringComparison)
                || String.Equals(s, s2, stringComparison)
                || String.Equals(s, s3, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf(this String s, String s1, String s2, String s3, String s4, StringComparison stringComparison = StringComparison.Ordinal)
            => String.Equals(s, s1, stringComparison)
                || String.Equals(s, s2, stringComparison)
                || String.Equals(s, s3, stringComparison)
                || String.Equals(s, s4, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf(this String s, String s1, String s2, String s3, String s4, String s5, StringComparison stringComparison = StringComparison.Ordinal)
            => String.Equals(s, s1, stringComparison)
                || String.Equals(s, s2, stringComparison)
                || String.Equals(s, s3, stringComparison)
                || String.Equals(s, s4, stringComparison)
                || String.Equals(s, s5, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf(this String s, String s1, String s2, String s3, String s4, String s5, String s6, StringComparison stringComparison = StringComparison.Ordinal)
            => String.Equals(s, s1, stringComparison)
                || String.Equals(s, s2, stringComparison)
                || String.Equals(s, s3, stringComparison)
                || String.Equals(s, s4, stringComparison)
                || String.Equals(s, s5, stringComparison)
                || String.Equals(s, s6, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf(this String s, String s1, String s2, String s3, String s4, String s5, String s6, String s7, StringComparison stringComparison = StringComparison.Ordinal)
            => String.Equals(s, s1, stringComparison)
                || String.Equals(s, s2, stringComparison)
                || String.Equals(s, s3, stringComparison)
                || String.Equals(s, s4, stringComparison)
                || String.Equals(s, s5, stringComparison)
                || String.Equals(s, s6, stringComparison)
                || String.Equals(s, s7, stringComparison);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsAnyOf(this String s, String s1, String s2, String s3, String s4, String s5, String s6, String s7, String s8, StringComparison stringComparison = StringComparison.Ordinal)
            => String.Equals(s, s1, stringComparison)
                || String.Equals(s, s2, stringComparison)
                || String.Equals(s, s3, stringComparison)
                || String.Equals(s, s4, stringComparison)
                || String.Equals(s, s5, stringComparison)
                || String.Equals(s, s6, stringComparison)
                || String.Equals(s, s7, stringComparison)
                || String.Equals(s, s8, stringComparison);

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String GetString(this Encoding encoding, ReadOnlyMemory<Byte> bytes)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            return encoding.GetString(bytes.Span);
        }

        public static ReadOnlyMemory<Byte> GetReadOnlyBytes(this Encoding encoding, String s)
        {
            ArgumentNullException.ThrowIfNull(encoding);
            ArgumentNullException.ThrowIfNull(s);

            return encoding.GetBytes(s);
        }

        private static Int32 SkipCommandLineDelimiter(this String commandLine, Int32 startAt)
        {
            var nextPos = commandLine.IndexOfNotAny(_delimiterOfCommandParameters, startAt);
            return nextPos >= 0 ? nextPos : commandLine.Length;
        }

        /// <summary>
        /// コマンドラインを表す単一文字列からコマンドまたはパラメタをひとつだけ切り出します。
        /// </summary>
        /// <param name="commandLine">
        /// コマンドライン文字列を表す <see cref="String"/> オブジェクトです。
        /// </param>
        /// <param name="startAt">
        /// <paramref name="commandLine"/> 上の解析開始位置を表す <see cref="Int32"/> 値です。
        /// </param>
        /// <param name="decodedToken">
        /// 切り出されたコマンドまたはパラメタを表す <see cref="String"/> オブジェクトです。
        /// </param>
        /// <returns>
        /// <para>
        /// 0 以上ならばそれは <paramref name="commandLine"/> 上のコマンドまたはパラメタの解析終了位置です。この値が指し示す位置は以下の何れかです。
        /// <list type="bullet">
        /// <item><paramref name="commandLine"/>上の空白またはTAB文字の位置</item>
        /// <item><paramref name="commandLine"/> の終端</item>
        /// </list>
        /// </para>
        /// <para>
        /// 負数ならばそれはコマンドまたはパラメタの何れもそれ以上ないことを意味します。
        /// </para>
        /// </returns>
        /// <exception cref="FormatException">
        /// 引用符 (ダブルクォート) が閉じられていません。
        /// </exception>
        /// <remarks>
        /// <list type="bullet">
        /// <item>
        /// このメソッドは、 <see cref="System.Diagnostics.Process.Start()"/> および Main(string[]) メソッドで実装されているコマンドラインのデコード方法の実装を模倣しています。
        /// 詳細については以下のソースコードを参照してください。
        /// <list type="bullet">
        /// <item>GetNextArgument() in https://github.com/dotnet/runtime/blob/main/src/libraries/System.Diagnostics.Process/src/System/Diagnostics/Process.Unix.cs for Linux</item>
        /// <item>SegmentCommandLine() in https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Environment.Windows.cs for Windows</item>
        /// </list>
        /// </item>
        /// <item>このメソッドでは、必要最低限のデコードのみをサポートしています。各種シェル (コマンドプロンプトやPowerShellを含む) 固有のデコードが必要な場合には別途行ってください。</item>
        /// </list>
        /// </remarks>
        private static Int32 ParseCommandLine(this String commandLine, Int32 startAt, out String decodedToken)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(startAt);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(startAt, commandLine.Length);
            if (startAt < commandLine.Length && (commandLine[startAt] == ' ' || commandLine[startAt] == '\t'))
                throw new InvalidOperationException("The character at the start of parsing is a space or a TAB.");

            var index = startAt;
            var quoted = false;
            var token = new StringBuilder();
            var tokenIsValid = false;
            while (index < commandLine.Length)
            {
                var c = commandLine[index];
                switch (c)
                {
                    case '"':
                    {
                        if (quoted && index + 1 < commandLine.Length && commandLine[index + 1] == '"')
                        {
                            // 2個の連続したダブルクォートの場合
                            // 1個のダブルクォートを出力する
                            _ = token.Append('"');
                            index += 2;
                        }
                        else
                        {
                            // 1個のダブルクォートである場合
                            quoted = !quoted;
                            ++index;
                        }

                        tokenIsValid = true;
                        break;
                    }
                    case '\\':
                    {
                        // 連続するバックスラッシュの数を数える
                        var countOfBackslashes = CountSequentialBackslash(commandLine, index + 1) + 1;
                        if (index + countOfBackslashes >= commandLine.Length || commandLine[index + countOfBackslashes] != '"')
                        {
                            // 連続するバックスラッシュの後が文字列の終端あるいはダブルクォート以外の文字である場合
                            // 連続するバックスラッシュをそのまま出力する
                            _ = token.Append('\\', countOfBackslashes);
                            index += countOfBackslashes;
                        }
                        else if ((countOfBackslashes & 1) == 0)
                        {
                            // 偶数個のバックスラッシュの後がダブルクォートである場合
                            _ = token.Append('\\', countOfBackslashes >> 1);
                            index += countOfBackslashes;
                        }
                        else
                        {
                            // 奇数個のバックスラッシュの後がダブルクォートである場合
                            _ = token.Append('\\', countOfBackslashes >> 1);
                            _ = token.Append('\"');
                            index += countOfBackslashes + 1;
                        }

                        tokenIsValid = true;
                        break;
                    }
                    case ' ':
                    case '\t':
                    {
                        if (quoted)
                        {
                            // ダブルクォート区間内である場合
                            _ = token.Append(c);
                            ++index;
                        }
                        else
                        {
                            // ダブルクォート区間外である場合
                            decodedToken = token.ToString();
                            return tokenIsValid ? index : -1;
                        }

                        tokenIsValid = true;
                        break;
                    }
                    default:
                    {
                        _ = token.Append(c);
                        ++index;
                        tokenIsValid = true;
                        break;
                    }
                }
            }

            if (quoted)
                throw new FormatException("The double quote section of the string is not closed.");

            decodedToken = token.ToString();
            return tokenIsValid ? index : -1;

            static Int32 CountSequentialBackslash(String s, Int32 offset)
            {
                for (var index = offset; index < s.Length; ++index)
                {
                    if (s[index] != '\\')
                        return index - offset;
                }

                return s.Length - offset;
            }
        }

        private static Boolean CharacterEqual(Char c1, Char c2, Boolean ignoreCase)
            => ignoreCase ?
                Char.ToUpperInvariant(c1) == Char.ToUpperInvariant(c2)
                : c1 == c2;

        [GeneratedRegex(@"([\?!]*\?[\?!]*)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
        private static partial Regex GetQuestionMarksAndExclamationMarksSequencePattern();

        [GeneratedRegex(@"(?<specialCharacter>&|<|>|\^|\|)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
        private static partial Regex GetCharacterEscapedAtCaretPattern();

        [GeneratedRegex(@"(?<backslash>\\*)""", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
        private static partial Regex GetBackSlashAndDoubleQuotePattern();

        [GeneratedRegex(@"(?<backslash>\\+)$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
        private static partial Regex GetEndsWithBackSlashPattern();
    }
}
