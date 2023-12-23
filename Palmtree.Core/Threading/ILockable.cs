using System;

namespace Palmtree.Threading
{
    internal interface ILockable
        : IDisposable
    {
        void Lock();
    }
}
