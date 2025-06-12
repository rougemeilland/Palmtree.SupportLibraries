using System;
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
        /// 与えられた条件を検証し、条件が満たされていない場合に与えられたメッセージの例外を発生させます。
        /// </summary>
        /// <param name="condition">
        /// 検証する条件です。
        /// </param>
        /// <param name="conditionText">
        /// 検証する条件を示すテキストです。
        /// </param>
        /// <remarks>
        /// 通常はあってはならない状況 (内部エラーなど) を検証するために使用します。
        /// </remarks>
        /// <exception cref="AssertionException">
        /// 検証条件が満たされませんでした。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [System.Diagnostics.Conditional("DEBUG")]
        [System.Diagnostics.Conditional("TRACE")]
        public static void Assert([DoesNotReturnIf(false)] Boolean condition, [CallerArgumentExpression(nameof(condition))] String? conditionText = null)
        {
            if (!condition)
                FailedToAssert(conditionText ?? "???");
        }

        /// <summary>
        /// 致命的エラーの例外オブジェクトを取得します。
        /// </summary>
        /// <param name="message">
        /// 例外のメッセージです。
        /// </param>
        /// <remarks>
        /// 通常はあってはならない状況 (内部エラーなど) が発生してプログラムの続行ができない場合に使用します。
        /// </remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception GetFailErrorException(String message)
        {
            System.Diagnostics.Debug.Fail(message);
            return new AssertionException(message);
        }

        /// <summary>
        /// 致命的エラーの例外オブジェクトを取得します。
        /// </summary>
        /// <param name="message">
        /// 例外のメッセージです。
        /// </param>
        /// <param name="innerException">
        /// 内部例外のオブジェクトです。
        /// </param>
        /// <remarks>
        /// 通常はあってはならない状況 (内部エラーなど) が発生してプログラムの続行ができない場合に使用します。
        /// </remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception GetFailErrorException(String message, Exception? innerException)
        {
            System.Diagnostics.Debug.Fail(message);
            return new AssertionException(message, innerException);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [DoesNotReturn]
        private static void FailedToAssert(String conditionText)
        {
            System.Diagnostics.Debug.Fail(conditionText);
            throw new AssertionException($"Failed to assert.; condition=\"{conditionText}\"");
        }
    }
}
