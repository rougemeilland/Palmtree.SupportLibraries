using System;

namespace Palmtree.IO.Console.StringExpansion
{
    internal class ExpansionStringSyntaxErrorExceptionException
        : Exception
    {
        public ExpansionStringSyntaxErrorExceptionException(String message)
            : base(message)
        {
        }

        public ExpansionStringSyntaxErrorExceptionException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
