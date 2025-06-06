using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    /// <summary>
    /// コードを検証するクラスです。
    /// </summary>
    public static class Validation
    {
#if DEBUG
        private sealed class DebugValidationLogger
            : IValidationLogger
        {
            void IValidationLogger.Indent()
            {
                lock (this)
                {
                    System.Diagnostics.Debug.Indent();
                }
            }

            void IValidationLogger.Unindent()
            {
                lock (this)
                {
                    System.Diagnostics.Debug.Unindent();
                }
            }

            void IValidationLogger.Write(String message)
            {
                lock (this)
                {
                    System.Diagnostics.Debug.Write(message);
                }
            }

            void IValidationLogger.WriteLine()
            {
                lock (this)
                {
                    System.Diagnostics.Debug.WriteLine("");
                }
            }

            void IValidationLogger.WriteLine(String message)
            {
                lock (this)
                {
                    System.Diagnostics.Debug.WriteLine(message);
                }
            }
        }
#endif

#if TRACE
        private sealed class TraceValidationLogger
            : IValidationLogger
        {
            void IValidationLogger.Indent()
            {
                lock (this)
                {
                    System.Diagnostics.Trace.Indent();
                }
            }

            void IValidationLogger.Unindent()
            {
                lock (this)
                {
                    System.Diagnostics.Trace.Unindent();
                }
            }

            void IValidationLogger.Write(String message)
            {
                lock (this)
                {
                    System.Diagnostics.Trace.Write(message);
                }
            }

            void IValidationLogger.WriteLine()
            {
                lock (this)
                {
                    System.Diagnostics.Trace.WriteLine("");
                }
            }

            void IValidationLogger.WriteLine(String message)
            {
                lock (this)
                {
                    System.Diagnostics.Trace.WriteLine(message);
                }
            }
        }
#endif

        static Validation()
        {
#if DEBUG
            Debug = new DebugValidationLogger();
#endif
#if TRACE
            Trace = new TraceValidationLogger();
#endif
        }

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
