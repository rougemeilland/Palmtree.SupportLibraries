using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Palmtree.IO
{
    public class FilePath
        : FileSystemPath
    {
        private readonly FileInfo _file;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FilePath(String path)
            : this(GetFineInfo(path))
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FilePath(FileInfo file)
            : base(file)
        {
            _file = file;
        }

        public DirectoryPath Directory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                _file.Refresh();
                var directory = _file.Directory;
                Validation.Assert(directory is not null, $" _file.Directory is not null (_file == \"{_file.FullName}\")");
                return DirectoryPath.CreateInstance(directory);
            }
        }

        public UInt64 Length
        {
            get
            {
                _file.Refresh();
                return checked((UInt64)_file.Length);
            }
        }

        public TextWriter AppendText()
        {
            try
            {
                _file.Refresh();
                return _file.AppendText();
            }
            finally
            {
                _file.Refresh();
            }
        }

        public TextWriter AppendText(Encoding encoding)
        {
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            try
            {
                _file.Refresh();
                var outStream = _file.OpenWrite();
                _ = outStream.Seek(0, SeekOrigin.End);
                return outStream.AsTextWriter(encoding);
            }
            finally
            {
                _file.Refresh();
            }
        }

        public void CopyTo(FilePath destinationFile, Boolean overwrite = false)
        {
            if (destinationFile is null)
                throw new ArgumentNullException(nameof(destinationFile));

            _file.Refresh();
            destinationFile.Refresh();
            try
            {
                _ = _file.CopyTo(destinationFile.FullName, overwrite);
            }
            finally
            {
                _file.Refresh();
                destinationFile.Refresh();
#if DEBUG
                ValidationPath();
                destinationFile.ValidationPath();
#endif
            }
        }

        public IRandomOutputByteStream<UInt64> Create()
        {
            _file.Refresh();
            try
            {
                return _file.Create().AsOutputByteStream().AsRandomAccess<UInt64>();
            }
            finally
            {
                _file.Refresh();
            }
        }

        public IRandomOutputByteStream<UInt64> CreateNew()
        {
            _file.Refresh();
            try
            {
                return new FileStream(_file.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.None).AsOutputByteStream().AsRandomAccess<UInt64>();
            }
            finally
            {
                _file.Refresh();
            }
        }

        public TextWriter CreateNewText()
        {
            _file.Refresh();
            try
            {
                return new StreamWriter(new FileStream(_file.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.None));
            }
            finally
            {
                _file.Refresh();
            }
        }

        public TextWriter CreateNewText(Encoding encoding)
        {
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            _file.Refresh();
            try
            {
                return new StreamWriter(new FileStream(_file.FullName, FileMode.CreateNew, FileAccess.Write, FileShare.None), encoding);
            }
            finally
            {
                _file.Refresh();
            }
        }

        public TextWriter CreateText()
        {
            _file.Refresh();
            try
            {
                return _file.CreateText();
            }
            finally
            {
                _file.Refresh();
            }
        }

        public TextWriter CreateText(Encoding encoding)
        {
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            _file.Refresh();
            try
            {
                return _file.Create().AsTextWriter(encoding);
            }
            finally
            {
                _file.Refresh();
            }
        }

        public void MoveTo(FilePath destinationFile, Boolean overwrite = false)
        {
            if (destinationFile is null)
                throw new ArgumentNullException(nameof(destinationFile));

            _file.Refresh();
            destinationFile.Refresh();
            try
            {
                File.Move(_file.FullName, destinationFile._file.FullName, overwrite);
            }
            finally
            {
                _file.Refresh();
                destinationFile.Refresh();
#if DEBUG
                ValidationPath();
                destinationFile.ValidationPath();
#endif
            }
        }

        public IRandomInputByteStream<UInt64> Open(FileMode mode, FileAccess access, FileShare share)
        {
            _file.Refresh();
            try
            {
                return _file.Open(mode, access, share).AsInputByteStream().AsRandomAccess<UInt64>();
            }
            finally
            {
                _file.Refresh();
            }
        }

        public IRandomInputByteStream<UInt64> OpenRead()
        {
            _file.Refresh();
            try
            {
                return _file.OpenRead().AsInputByteStream().AsRandomAccess<UInt64>();
            }
            finally
            {
                _file.Refresh();
            }
        }

        public TextReader OpenText()
        {
            _file.Refresh();
            try
            {
                return _file.OpenText();
            }
            finally
            {
                _file.Refresh();
            }
        }

        public TextReader OpenText(Encoding encoding)
        {
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            _file.Refresh();
            try
            {
                return _file.OpenRead().AsTextReader(encoding);
            }
            finally
            {
                _file.Refresh();
            }
        }

        public IRandomOutputByteStream<UInt64> OpenWrite()
        {
            _file.Refresh();
            try
            {
                return _file.OpenWrite().AsOutputByteStream().AsRandomAccess<UInt64>();
            }
            finally
            {
                _file.Refresh();
            }
        }

        public void Replace(FilePath destination, FilePath destinatonBackupFile)
        {
            if (destination is null)
                throw new ArgumentNullException(nameof(destination));
            if (destinatonBackupFile is null)
                throw new ArgumentNullException(nameof(destinatonBackupFile));

            _file.Refresh();
            destination.Refresh();
            destinatonBackupFile.Refresh();
            try
            {
                _ = _file.Replace(destination._file.FullName, destinatonBackupFile._file.FullName);
            }
            finally
            {
                _file.Refresh();
                destination.Refresh();
                destinatonBackupFile.Refresh();
#if DEBUG
                ValidationPath();
                destination.ValidationPath();
                destinatonBackupFile.ValidationPath();
#endif
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator FileInfo(FilePath path)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            return new(path._file.FullName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator FilePath(FileInfo directory)
        {
            if (directory is null)
                throw new ArgumentNullException(nameof(directory));

            return new(new FileInfo(directory.FullName));
        }

        /// <remarks>
        /// The same instance as the object indicated by parameter <paramref name="file"/> must not be used elsewhere.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static FilePath CreateInstance(FileInfo file) => new(file);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FileInfo GetFineInfo(String path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentException($"'{nameof(path)}' must not be null or empty.", nameof(path));

            try
            {
                return new FileInfo(path);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"A character string that cannot be used as a file path name was specified. : \"{path}\"", nameof(path), ex);
            }
        }
    }
}
