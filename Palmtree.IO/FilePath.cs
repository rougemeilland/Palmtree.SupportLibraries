using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Palmtree.IO
{
    public class FilePath
        : FileSystemPath
    {
        private static readonly Random _randomNumberGeneratorForTemporaryFileName;
        private static readonly Char[] _temporaryFileNameMap;

        private readonly FileInfo _file;

        static FilePath()
        {
            _randomNumberGeneratorForTemporaryFileName = new Random(Environment.TickCount);
            _temporaryFileNameMap = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v' };
#if DEBUG
            // _temporaryFileNameMap の要素が数字あるいは英小文字のみであることの確認
            Validation.Assert(_temporaryFileNameMap.All(c => c is >= '0' and <= '9' or >= 'a' and <= 'z'), "_temporaryFileNameMap.All(c => c is >= '0' and <= '9' or >= 'a' and <= 'z')");

            // _temporaryFileNameMap の配列の長さが 32 であることの確認
            Validation.Assert(_temporaryFileNameMap.Length == 32, "_temporaryFileNameMap.Length == 32");

            // _temporaryFileNameMap の要素が重複していないことの確認
            Validation.Assert(_temporaryFileNameMap.Distinct().Count() == 32, "_temporaryFileNameMap.Distinct().Count() == 32");
#endif
        }

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

        public static FilePath CreateTemporaryFile(String prefix = "tmp", String suffix = ".tmp")
        {
            ArgumentNullException.ThrowIfNull(prefix);
            ArgumentNullException.ThrowIfNull(suffix);

            var tempFileNameBuilder = new StringBuilder(prefix.Length + 6 + suffix.Length);
            var getTempDir = Path.GetTempPath();
            var retryCount = 0;
            while (true)
            {
#if false
                var randomNumber = 0x12345678;
#else
                var randomNumber = _randomNumberGeneratorForTemporaryFileName.Next();
#endif
                _ = tempFileNameBuilder.Clear();
                _ = tempFileNameBuilder.Append(prefix);
                _ = tempFileNameBuilder.Append(_temporaryFileNameMap[(randomNumber >> 25) & 0x1f]);
                _ = tempFileNameBuilder.Append(_temporaryFileNameMap[(randomNumber >> 20) & 0x1f]);
                _ = tempFileNameBuilder.Append(_temporaryFileNameMap[(randomNumber >> 15) & 0x1f]);
                _ = tempFileNameBuilder.Append(_temporaryFileNameMap[(randomNumber >> 10) & 0x1f]);
                _ = tempFileNameBuilder.Append(_temporaryFileNameMap[(randomNumber >> 5) & 0x1f]);
                _ = tempFileNameBuilder.Append(_temporaryFileNameMap[(randomNumber >> 0) & 0x1f]);
                _ = tempFileNameBuilder.Append(suffix);

                var path = Path.Combine(getTempDir, tempFileNameBuilder.ToString());
                var tempFilePath =
                    CreateFilePathInstance(path)
                    ?? throw new ArgumentException($"Can't create temporary file. Probably parameter 'prefix' or parameter 'suffix' contains invalid characters.: {nameof(prefix)}=\"{prefix}\", {nameof(suffix)}=\"{suffix}\"");
                if (CheckTemporaryFile(tempFilePath.FullName, retryCount))
                    return tempFilePath;
                ++retryCount;
            }

            static Boolean CheckTemporaryFile(String tempFilePath, Int32 retryCount)
            {
                if (retryCount < 100)
                    return TryToCreateTemporaryFile(tempFilePath);
                var stream = (Stream?)null;
                try
                {
                    stream = new FileStream(tempFilePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None);
                    return true;
                }
                finally
                {
                    stream?.Dispose();
                }

                static Boolean TryToCreateTemporaryFile(String tempFilePath)
                {
                    if (OperatingSystem.IsWindows())
                    {
                        var normalizedPath = InterOpForWindows.NormalizePath(tempFilePath);
                        var handle =
                            InterOpForWindows.CreateFile(
                                normalizedPath,
                                InterOpForWindows.AccessMode.GENERIC_WRITE,
                                InterOpForWindows.FileShare.FILE_SHARE_NONE,
                                IntPtr.Zero,
                                InterOpForWindows.CreationMode.CREATE_NEW,
                                InterOpForWindows.FileAttribute.FILE_ATTRIBUTE_NORMAL,
                                IntPtr.Zero);
                        try
                        {
                            var errorCode = Marshal.GetLastWin32Error();
                            if (handle != InterOpForWindows.INVALID_HANDLE_VALUE)
                                return true;
                            else if (errorCode == InterOpForWindows.ERROR_FILE_EXISTS)
                                return false;
                            else
                                throw new Win32Exception(errorCode);
                        }
                        finally
                        {
                            if (handle != InterOpForWindows.INVALID_HANDLE_VALUE)
                                _ = InterOpForWindows.CloseHandle(handle);
                        }
                    }
                    else
                    {
                        var handle =
                            InterOpForLinux.Open(
                                tempFilePath,
                                InterOpForLinux.OpenMode.O_CREAT | InterOpForLinux.OpenMode.O_EXCL,
                                InterOpForLinux.Permission.S_IRWXU);
                        try
                        {
                            var errno = Marshal.GetLastSystemError();
                            if (handle >= 0)
                                return true;
                            else if (errno == InterOpForLinux.ERROR_CODE_EEXIST)
                                return false;
                            else
                                throw new IOException(InterOpForLinux.StrError(errno));
                        }
                        finally
                        {
                            if (handle >= 0)
                                _ = InterOpForLinux.Close(handle);
                        }
                    }
                }
            }

            static FilePath? CreateFilePathInstance(String path)
            {
                try
                {
                    return new FilePath(path);
                }
                catch (Exception)
                {
                    return null;
                }
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

        public FilePath GetCasePreservedPath()
        {
            var casePreservedPath = Directory.GetCasePreservedPath();
            if (!casePreservedPath.Exists)
                return casePreservedPath.GetFile(Name);
            var found = casePreservedPath.EnumerateFiles(Name).Take(2).ToArray();
            if (found.Length != 1)
                return casePreservedPath.GetFile(Name);
            return found[0];
        }

        public String? GetRelativePath(DirectoryPath baseDirectory)
        {
            var dir = Directory.GetRelativePath(baseDirectory);
            return dir is null
                ? null
                : dir.Equals(".", StringComparison.Ordinal)
                ? Name
                : Path.Combine(dir, Name);
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
