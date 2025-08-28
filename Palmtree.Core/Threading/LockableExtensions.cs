using System;
using System.Threading;
using System.Threading.Tasks;
using Palmtree.Threading;

// public な拡張メソッドのクラスであるため、アセンブリの既定の名前空間に配置した。
#pragma warning disable IDE0130 // Namespace がフォルダー構造と一致しません
namespace Palmtree
#pragma warning restore IDE0130 // Namespace がフォルダー構造と一致しません
{
    public static class LockableExtensions
    {
        private sealed class LocalSemaphore
            : ILockable, IAsyncLockable
        {
            private readonly SemaphoreSlim _semaphore;
            private Boolean _isDisposed;

            public LocalSemaphore(SemaphoreSlim semaphore)
            {
                _semaphore = semaphore;
                _isDisposed = false;
            }

            ~LocalSemaphore()
            {
                Dispose(disposing: false);
            }

            public void Lock() => _semaphore.Wait();

            public Task LockAsync() => _semaphore.WaitAsync();

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(Boolean disposing)
            {
                if (!_isDisposed)
                {
                    if (disposing)
                    {
                    }

                    _ = _semaphore.Release();

                    _isDisposed = true;
                }
            }
        }

        private sealed class GlobalSemaphore
            : ILockable, IAsyncLockable
        {
            private readonly Semaphore _semaphore;
            private Boolean _isDisposed;

            public GlobalSemaphore(Semaphore semaphore)
            {
                _semaphore = semaphore;
                _isDisposed = false;
            }

            ~GlobalSemaphore()
            {
                Dispose(disposing: false);
            }

            public void Lock() => _ = _semaphore.WaitOne();

            public Task LockAsync() => Task.Run(_semaphore.WaitOne);

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(Boolean disposing)
            {
                if (!_isDisposed)
                {
                    if (disposing)
                    {
                    }

                    _ = _semaphore.Release();

                    _isDisposed = true;
                }
            }
        }

        private sealed class GlobalMutex
            : ILockable
        {
            private readonly Mutex _mutex;
            private Boolean _isDisposed;

            public GlobalMutex(Mutex mutex)
            {
                _mutex = mutex;
                _isDisposed = false;
            }

            ~GlobalMutex()
            {
                Dispose(disposing: false);
            }

            public void Lock() => _ = _mutex.WaitOne();

            public Task LockAsync()
                => Task.Run(
                    () =>
                    {
                        _ = _mutex.WaitOne();
                    });

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(Boolean disposing)
            {
                if (!_isDisposed)
                {
                    if (disposing)
                    {
                    }

                    _mutex.ReleaseMutex();

                    _isDisposed = true;
                }
            }
        }

        public static IDisposable Lock(this SemaphoreSlim semaphore)
        {
            var lockObject = new LocalSemaphore(semaphore);
            lockObject.Lock();
            return lockObject;
        }

        public static async Task<IDisposable> LockAsync(this SemaphoreSlim semaphore)
        {
            var lockObject = new LocalSemaphore(semaphore);
            await lockObject.LockAsync().ConfigureAwait(false);
            return lockObject;
        }

        public static IDisposable Lock(this Semaphore semaphore)
        {
            var lockObject = new GlobalSemaphore(semaphore);
            lockObject.Lock();
            return lockObject;
        }

        public static async Task<IDisposable> LockAsync(this Semaphore semaphore)
        {
            var lockObject = new GlobalSemaphore(semaphore);
            await lockObject.LockAsync().ConfigureAwait(false);
            return lockObject;
        }

        public static IDisposable Lock(this Mutex mutex)
        {
            var lockObject = new GlobalMutex(mutex);
            lockObject.Lock();
            return lockObject;
        }
    }
}
