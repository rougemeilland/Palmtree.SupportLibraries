using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Palmtree
{
    /// <summary>
    /// コードを検証するクラスです。
    /// </summary>
    public static class Validation
    {
#if DEBUG || TRACE
        private abstract class ValidationLogger
            : IValidationLogger
        {
            private const Int32 _indentSize = 4;
            private Int32 _indentLevel;

            public void Indent()
                => _ = Interlocked.Increment(ref _indentLevel);

            public void Unindent()
            {
                var result = Interlocked.Decrement(ref _indentLevel);
                if (result < 0)
                {
                    _ = Interlocked.Exchange(ref _indentLevel, 0);
                    throw new InvalidOperationException("The UnIndent() method is called more times than the Indent() method.");
                }
            }

            public void WriteLog() => WriteLine("");

            public void WriteLog(String? prefix, String message)
            {
                if (prefix is null)
                {
                    WriteLine(message);
                }
                else
                {
                    Write(prefix);
                    var spaces = _indentSize * _indentLevel;
                    while (spaces >= 4)
                    {
                        Write("    ");
                        spaces -= 4;
                    }

                    while (spaces > 0)
                    {
                        Write(" ");
                        --spaces;
                    }

                    WriteLine(message);
                }
            }

            protected abstract void Write(String message);
            protected abstract void WriteLine(String message);
        }
#endif

#if DEBUG
        private sealed class DebugValidationLogger
            : ValidationLogger
        {
            protected override void Write(String message) => System.Diagnostics.Debug.Write(message);

            protected override void WriteLine(String message) => System.Diagnostics.Debug.WriteLine(message);

        }
#endif

#if TRACE
        private sealed class TraceValidationLogger
            : ValidationLogger
        {
            protected override void Write(String message) => System.Diagnostics.Trace.Write(message);

            protected override void WriteLine(String message) => System.Diagnostics.Trace.WriteLine(message);

        }
#endif

        static Validation()
        {
            var processPath = GetProcessPath();
            DefaultApplicationName =
                processPath is null
                ? null
                : OperatingSystem.IsWindows()
                ? Path.GetFileNameWithoutExtension(processPath)
                : Path.GetFileName(processPath);
#if DEBUG
            Debug = new DebugValidationLogger();
#endif
#if TRACE
            Trace = new TraceValidationLogger();
#endif

            static String? GetProcessPath()
            {
                if (Environment.ProcessPath is not null)
                    return Environment.ProcessPath;
                var startOfCommandLine = Environment.CommandLine.SplitCommandLineArguments().Take(1).ToArray();
                return
                    startOfCommandLine.Length == 1
                    ? startOfCommandLine[0].element
                    : null;
            }
        }

        public static String? DefaultApplicationName { get; }

#if DEBUG
        public static IValidationLogger Debug { get; }
#endif
#if TRACE
        public static IValidationLogger Trace { get; }
#endif

        /// <summary>
        /// 与えられた条件を検証し、条件が満たされていない場合に <see cref="AssertionException"/> 例外を発生させます。
        /// </summary>
        /// <param name="condition">
        /// 検証する条件の評価結果を示す <see cref="Boolean"/> です。
        /// </param>
        /// <param name="conditionalExpression">
        /// <para>
        /// 検証する条件式を示す <see cref="String"/> です。
        /// 既定の条件式を指定する場合は <see langword="null"/> です。
        /// </para>
        /// <para>
        /// パラメタ <paramref name="conditionalExpression"/> を省略することを推奨します。
        /// </para>
        /// </param>
        /// <param name="sourceFileName">
        /// <para>
        /// 致命的なエラーを検出したコードのソースファイルのパス名を示す <see cref="String"/> です。
        /// 既定のパス名を指定する場合は <see langword="null"/> です。
        /// </para>
        /// <para>
        /// パラメタ <paramref name="sourceFileName"/> を省略することを推奨します。
        /// </para>
        /// </param>
        /// <param name="lineNumber">
        /// <para>
        /// 致命的なエラーを検出したコードのソースファイル上の行番号を示す <see cref="Int32"/> です。
        /// 既定の行番号を指定する場合は 0 です。
        /// </para>
        /// <para>
        /// パラメタ <paramref name="lineNumber"/> を省略することを推奨します。
        /// </para>
        /// </param>
        /// <param name="memberName">
        /// <para>
        /// 致命的なエラーを検出したコードのメソッド名を示す <see cref="String"/> です。
        /// 既定のメソッド名を指定する場合は <see langword="null"/> です。
        /// </para>
        /// <para>
        /// パラメタ <paramref name="memberName"/> を省略することを推奨します。
        /// </para>
        /// </param>
        /// <remarks>
        /// 通常はあってはならない状況 (内部エラーなど) を検証するために使用します。
        /// </remarks>
        /// <exception cref="AssertionException">
        /// <paramref name="condition"/> が <see langword="false"/> です。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Conditional("DEBUG")]
        [System.Diagnostics.Conditional("TRACE")]
        public static void Assert(
            [DoesNotReturnIf(false)] Boolean condition,
            [CallerArgumentExpression(nameof(condition))] String? conditionalExpression = null,
            [CallerFilePath] String? sourceFileName = null,
            [CallerLineNumber] Int32 lineNumber = 0,
            [CallerMemberName] String? memberName = null)
        {
            if (!condition)
                throw CauseFatalError(conditionalExpression, sourceFileName, lineNumber, memberName);
        }

        /// <summary>
        /// 致命的エラーの例外オブジェクトを取得します。
        /// </summary>
        /// <param name="sourceFileName">
        /// <para>
        /// 致命的なエラーを検出したコードのソースファイルのパス名を示す <see cref="String"/> です。
        /// 既定のパス名を指定する場合は <see langword="null"/> です。
        /// </para>
        /// <para>
        /// パラメタ <paramref name="sourceFileName"/> を省略することを推奨します。
        /// </para>
        /// </param>
        /// <param name="lineNumber">
        /// <para>
        /// 致命的なエラーを検出したコードのソースファイル上の行番号を示す <see cref="Int32"/> です。
        /// 既定の行番号を指定する場合は 0 です。
        /// </para>
        /// <para>
        /// パラメタ <paramref name="lineNumber"/> を省略することを推奨します。
        /// </para>
        /// </param>
        /// <param name="memberName">
        /// <para>
        /// 致命的なエラーを検出したコードのメソッド名を示す <see cref="String"/> です。
        /// 既定のメソッド名を指定する場合は <see langword="null"/> です。
        /// </para>
        /// <para>
        /// パラメタ <paramref name="memberName"/> を省略することを推奨します。
        /// </para>
        /// </param>
        /// <remarks>
        /// 通常はあってはならない状況 (内部エラーなど) が発生してプログラムの続行ができない場合に使用します。
        /// </remarks>
        public static Exception GetFailErrorException(
            [CallerFilePath] String? sourceFileName = null,
            [CallerLineNumber] Int32 lineNumber = 0,
            [CallerMemberName] String? memberName = null)
            => CauseFatalError(null, sourceFileName, lineNumber, memberName);

        /// <summary>
        /// 致命的エラーの例外オブジェクトを取得します。
        /// </summary>
        /// <param name="innerException">
        /// 致命的エラーの原因となった例外のオブジェクトを示す <see cref="Exception"/> です。
        /// </param>
        /// <param name="sourceFileName">
        /// <para>
        /// 致命的なエラーを検出したコードのソースファイルのパス名を示す <see cref="String"/> です。
        /// 既定のパス名を指定する場合は <see langword="null"/> です。
        /// </para>
        /// <para>
        /// パラメタ <paramref name="sourceFileName"/> を省略することを推奨します。
        /// </para>
        /// </param>
        /// <param name="lineNumber">
        /// <para>
        /// 致命的なエラーを検出したコードのソースファイル上の行番号を示す <see cref="Int32"/> です。
        /// 既定の行番号を指定する場合は 0 です。
        /// </para>
        /// <para>
        /// パラメタ <paramref name="lineNumber"/> を省略することを推奨します。
        /// </para>
        /// </param>
        /// <param name="memberName">
        /// <para>
        /// 致命的なエラーを検出したコードのメソッド名を示す <see cref="String"/> です。
        /// 既定のメソッド名を指定する場合は <see langword="null"/> です。
        /// </para>
        /// <para>
        /// パラメタ <paramref name="memberName"/> を省略することを推奨します。
        /// </para>
        /// </param>
        /// <remarks>
        /// 通常はあってはならない状況 (内部エラーなど) が発生してプログラムの続行ができない場合に使用します。
        /// </remarks>
        public static Exception GetFailErrorException(
            Exception? innerException,
            [CallerFilePath] String? sourceFileName = null,
            [CallerLineNumber] Int32 lineNumber = 0,
            [CallerMemberName] String? memberName = null)
            => CauseFatalError(null, sourceFileName, lineNumber, memberName, innerException);

        /// <summary>
        /// 致命的なエラーを検出した場合の強制終了処理を実行します。
        /// </summary>
        /// <param name="conditionalExpression">
        /// 致命的なエラーを検出したときの条件式を示す <see cref="String"/> です。
        /// 条件式を省略する場合は <see langword="null"/> です。
        /// </param>
        /// <param name="sourceFileName">
        /// 致命的なエラーを検出したコードのソースファイルのパス名を示す <see cref="String"/> です。
        /// パス名を省略する場合は <see langword="null"/> です。
        /// </param>
        /// <param name="lineNumber">
        /// 致命的なエラーを検出したコードのソースファイル上の行番号を示す <see cref="Int32"/> です。
        /// 行番号を省略する場合は 0 です。
        /// </param>
        /// <param name="memberName">
        /// 致命的なエラーを検出したコードのメソッド名を示す <see cref="String"/> です。
        /// メソッド名を省略する場合は <see langword="null"/> です。
        /// </param>
        /// <param name="ex">
        /// エラーの原因となった例外オブジェクトを示す <see cref="Exception"/> です。
        /// 例外オブジェクトを省略する場合は <see langword="null"/> です。
        /// </param>
        /// <returns>
        /// 例外オブジェクト <see cref="AssertionException"/> を返します。
        /// </returns>
        /// <remarks>
        /// このメソッドは以下の順で評価されます。
        /// <list type="number">
        /// <item>シンボル "DEBUG " が定義されている場合、<see cref="System.Diagnostics.Debug.Fail(String?)"/> が実行される。</item>
        /// <item>シンボル "TRACE " が定義されている場合、<see cref="System.Diagnostics.Trace.Fail(String?)"/> が実行される。</item>
        /// <item> <see cref="AssertionException"/> オブジェクトを返す。</item>
        /// </list>
        /// </remarks>
        private static AssertionException CauseFatalError(String? conditionalExpression, String? sourceFileName, Int32 lineNumber, String? memberName, Exception? ex = null)
        {
            var message = BuildAssertionMessage(conditionalExpression, sourceFileName, lineNumber, memberName);
#if DEBUG
            System.Diagnostics.Debug.Fail(message);
#elif TRACE
            System.Diagnostics.Trace.Fail(message);
#else
#endif
            return
                ex is not null
                ? new AssertionException(message, ex)
                : new AssertionException(message);
        }

        private static String BuildAssertionMessage(String? conditionalExpression, String? sourceFileName, Int32 lineNumber, String? memberName)
        {
            var elements = new List<String>();
            if (conditionalExpression is not null)
                elements.Add($"conditional-expression=\"{conditionalExpression}\"");
            if (sourceFileName is not null)
                elements.Add($"source-file=\"{sourceFileName}\"");
            if (lineNumber > 0)
                elements.Add($"line={lineNumber}");
            if (memberName is not null)
                elements.Add($"member={memberName}");
            return $"Failed to assert.{(elements.Count > 0 ? ": " : "")}{String.Join(", ", elements)}";
        }
    }
}
