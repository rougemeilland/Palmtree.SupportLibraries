using System;

namespace Palmtree.IO.Console.StringExpansion
{
    internal sealed class ExpansionBadArgumentExceptionException
        : Exception
    {
        public ExpansionBadArgumentExceptionException(String message)
            : base(message)
        {
        }

        public ExpansionBadArgumentExceptionException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
