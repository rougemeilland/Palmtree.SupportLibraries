using System;

namespace Palmtree
{
    [Obsolete("使用されなくなりました。代わりに Validation.Assert() または Validation.GetFailErrorException() を使用してください。")]
    public class InternalLogicalErrorException
        : Exception
    {
        public InternalLogicalErrorException()
            : base("Detected internal logical error.")
        {
        }

        public InternalLogicalErrorException(String message)
            : base(message)
        {
        }

        public InternalLogicalErrorException(String message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
