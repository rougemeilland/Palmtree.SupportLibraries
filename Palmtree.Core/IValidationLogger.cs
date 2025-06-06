using System;

namespace Palmtree
{
    public interface IValidationLogger
    {
        void Indent();
        void Unindent();
        void Write(String message);
        void WriteLine();
        void WriteLine(String message);
    }
}
