using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Palmtree
{
    public static partial class StringExtensions
    {
        private static readonly Char[] _anyOfTabOrSpace = new[] { '\t', ' ' };

        static StringExtensions()
        {
#if DEBUG
            Validation.Assert('\u0007' == '\a', "'\u0007' == '\a'");
            Validation.Assert('\u0008' == '\b', "'\u0008' == '\b'");
            Validation.Assert('\u0009' == '\t', "'\u0009' == '\t'");
            Validation.Assert('\u000a' == '\n', "'\u000a' == '\n'");
            Validation.Assert('\u000b' == '\v', "'\u000b' == '\v'");
            Validation.Assert('\u000c' == '\f', "'\u000c' == '\f'");
            Validation.Assert('\u000c' == '\f', "'\u000c' == '\f'");
            Validation.Assert('\u000d' == '\r', "'\u000d' == '\r'");
#endif
        }

        #region ChunkAsString

        public static IEnumerable<String> ChunkAsString(this IEnumerable<Char> source, Int32 count)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

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
            if (sourceString is null)
                throw new ArgumentNullException(nameof(sourceString));
            if (!offset.IsBetween(0, sourceString.Length))
                throw new ArgumentOutOfRangeException(nameof(offset));

            return (ReadOnlyMemory<Char>)sourceString[offset..].ToCharArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<Char> Slice(this String sourceString, UInt32 offset)
        {
            if (sourceString is null)
                throw new ArgumentNullException(nameof(sourceString));
            if (checked((Int32)offset) > sourceString.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            return sourceString.Slice(checked((Int32)offset));
        }

        public static ReadOnlyMemory<Char> Slice(this String sourceString, Range range)
        {
            if (sourceString is null)
                throw new ArgumentNullException(nameof(sourceString));
            var sourceArray = sourceString.ToCharArray();

            var (offset, count) = sourceArray.GetOffsetAndLength(range, nameof(range));
            return sourceString.Substring(offset, count).ToCharArray();
        }

        public static ReadOnlyMemory<Char> Slice(this String sourceString, Int32 offset, Int32 count)
        {
            if (sourceString is null)
                throw new ArgumentNullException(nameof(sourceString));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (checked(count + offset) > sourceString.Length)
                throw new ArgumentException($"The specified range ({nameof(offset)} and {nameof(count)}) is not within the {nameof(sourceString)}.");

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
            if (s is null)
                throw new ArgumentNullException(nameof(s));

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
            if (s is null)
                throw new ArgumentNullException(nameof(s));

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
        /// <param name="forShell">シェル用のエンコードを行うならば true、そうではないのなら false です。</param>
        /// <returns>エンコードされた文字列です。</returns>
        /// <remarks>
        /// <list type="bullet">
        /// <item>エンコードの方法は実行環境のプラットフォームによって異なります。</item>
        /// <item> Windows 以外のオペレーティングシステムでは <paramref name="forShell"/> を true に指定することはできません。</item>
        /// </list>
        /// </remarks>
        public static String CommandLineArgumentEncode(this String arg, Boolean forShell = false)
        {
            if (arg is null)
                throw new ArgumentNullException(nameof(arg));

            return
                OperatingSystem.IsWindows()
                ? EncodeForWindows(arg, forShell)
                : EncodeForUnix(arg, forShell);

            static String EncodeForUnix(String arg, Boolean forShell)
            {
                if (forShell)
                    throw new NotSupportedException();

                arg = GetDoubleQuoteOrBackSlashPattern().Replace(arg, "\\$1");
                if (arg.IndexOfAny(_anyOfTabOrSpace) < 0)
                    return arg;
                return $"\"{arg}\"";
            }

            static String EncodeForWindows(String arg, Boolean forShell)
            {
                if (forShell)
                    arg = GetCharacterEscapedAtCaretPattern().Replace(arg, @"^$1");
                arg = GetBackSlashAndDoubleQuotePattern().Replace(arg, @"\$1$&");
                if (arg.IndexOfAny(_anyOfTabOrSpace) < 0)
                    return arg;
                arg = GetEndsWithBaskSlashPattern().Replace(arg, "$1$1");
                return
                    forShell
                    ? $"\"{arg}\""
                    : $"^\"{arg}^\"";
            }
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
            if (s is null)
                throw new ArgumentNullException(nameof(s));

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
            if (s is null)
                throw new ArgumentNullException(nameof(s));

            var pathName =
                String.Concat(
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
            return pathName;
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
            if (s1.Length > s2.Length)
                throw new Exception();
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
            if (s1.Length > s2.Length)
                throw new Exception();
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
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return encoding.GetString(bytes.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<Byte> GetReadOnlyBytes(this Encoding encoding, String s)
        {
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));
            if (s is null)
                throw new ArgumentNullException(nameof(s));

            return encoding.GetBytes(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean CharacterEqual(Char c1, Char c2, Boolean ignoreCase)
            => ignoreCase ?
                Char.ToUpperInvariant(c1) == Char.ToUpperInvariant(c2)
                : c1 == c2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [GeneratedRegex("([\\?!]*\\?[\\?!]*)", RegexOptions.Compiled)]
        private static partial Regex GetQuestionMarksAndExclamationMarksSequencePattern();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [GeneratedRegex("(&|<|>|\\^|\\|)", RegexOptions.Compiled)]
        private static partial Regex GetCharacterEscapedAtCaretPattern();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [GeneratedRegex("(\"|\\\\)", RegexOptions.Compiled)]
        private static partial Regex GetDoubleQuoteOrBackSlashPattern();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [GeneratedRegex("(\\\\*)\"", RegexOptions.Compiled)]
        private static partial Regex GetBackSlashAndDoubleQuotePattern();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [GeneratedRegex("(\\\\+)$", RegexOptions.Compiled)]
        private static partial Regex GetEndsWithBaskSlashPattern();
    }
}
