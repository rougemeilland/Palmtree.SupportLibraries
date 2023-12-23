using System;
using System.Threading.Tasks;

namespace Palmtree.Threading
{
    internal interface IAsyncLockable
        : IDisposable
    {
        Task LockAsync();
    }
}
