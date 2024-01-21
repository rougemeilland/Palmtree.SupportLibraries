using System;
using System.IO;

namespace Palmtree.IO
{
    public class TemporaryDirectory
        : IDisposable
    {
        private readonly FilePath _lockFilePath;
        private readonly DirectoryPath _directoryPath;
        private readonly ISequentialOutputByteStream _lockFileStream;

        private Boolean _isDisposed;

        private TemporaryDirectory(FilePath lockFilePath, DirectoryPath directoryPath, ISequentialOutputByteStream lockFileStream)
        {
            _lockFilePath = lockFilePath;
            _directoryPath = directoryPath;
            _lockFileStream = lockFileStream;
            _isDisposed = false;
        }

        ~TemporaryDirectory()
        {
            Dispose(disposing: false);
        }

        public DirectoryPath Directory
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().FullName);

                return _directoryPath;
            }
        }

        public static TemporaryDirectory Create()
        {
            while (true)
            {
                var success = false;
                var lockFilePath = (FilePath?)null;
                var directoryPath = (DirectoryPath?)null;
                var lockFileStream = (ISequentialOutputByteStream?)null;
                try
                {
                    try
                    {
                        lockFilePath = new FilePath(Path.GetTempFileName());
                        directoryPath = new DirectoryPath($"{lockFilePath}.dir");
                        if (!directoryPath.Exists)
                        {
                            _ = directoryPath.Create();
                            lockFileStream = lockFilePath.OpenWrite();
                            success = true;
                            return new TemporaryDirectory(lockFilePath, directoryPath, lockFileStream);
                        }
                    }
                    catch (IOException)
                    {
                    }
                }
                finally
                {
                    if (!success)
                    {
                        lockFileStream?.Dispose();
                        directoryPath?.SafetyDelete(true);
                        lockFilePath?.SafetyDelete();
                    }
                }
            }
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _lockFileStream.Dispose();
                }

                // ファイルはアンマネージリソース扱い
                _directoryPath.SafetyDelete(true);
                _lockFilePath.SafetyDelete();
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
