using System;

namespace Palmtree
{
    public interface IDisposableValidationLogger
        : IDisposable, IValidationLogger
    {
    }
}
