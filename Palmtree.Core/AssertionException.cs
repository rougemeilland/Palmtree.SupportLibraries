using System;

namespace Palmtree
{
    /// <summary>
    /// 通常はあってはならない条件 (内部エラーなど) で通知される例外のクラスです。
    /// </summary>
    public sealed class AssertionException
        : Exception
    {
        internal AssertionException(String message)
            : base(message)
        {
            System.Diagnostics.Debug.Fail(message);
        }

        internal AssertionException(String message, Exception inner)
            : base(message, inner)
        {
            System.Diagnostics.Debug.Fail(message);
        }
    }
}
