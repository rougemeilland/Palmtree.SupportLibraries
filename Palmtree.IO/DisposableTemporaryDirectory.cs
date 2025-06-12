using System;

namespace Palmtree.IO
{
    public class DisposableTemporaryDirectory
        : IDisposable
    {
        private readonly DirectoryPath _tempraryDirectoryPath;

        private Boolean _isDisposed;

        private DisposableTemporaryDirectory(DirectoryPath tempraryDirectoryPath)
        {
            _tempraryDirectoryPath = tempraryDirectoryPath;
            _isDisposed = false;
        }

        ~DisposableTemporaryDirectory()
        {
            Dispose(disposing: false);
        }

        public DirectoryPath Directory
        {
            get
            {
                ObjectDisposedException.ThrowIf(_isDisposed, this);

                return _tempraryDirectoryPath;
            }
        }

        public static DisposableTemporaryDirectory Create()
            => new(DirectoryPath.CreateTemporaryDirectory());

        protected virtual void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                }

                // ディレクトリはアンマネージリソース扱い
                _tempraryDirectoryPath.SafetyDelete(true);
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
