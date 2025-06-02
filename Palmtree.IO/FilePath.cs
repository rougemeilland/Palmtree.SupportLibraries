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

        public ISequentialOutputByteStream Append(FileShare share = FileShare.None)
        {
            _file.Refresh();
            try
            {
                return Append(_file.FullName, share);
            }
            finally
            {
                _file.Refresh();
            }
        }

        #region AppendText

        public TextWriter AppendText(FileShare share = FileShare.None) => AppendText(share, Encoding.UTF8);

        public TextWriter AppendText(Encoding encoding) => AppendText(FileShare.None, encoding);

        public TextWriter AppendText(FileShare share, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            _file.Refresh();
            try
            {
                return Append(_file.FullName, share).AsTextWriter(encoding);
            }
            finally
            {
                _file.Refresh();
            }
        }

        #endregion

        public void CopyTo(FilePath destinationFile, Boolean overwrite = false)
        {
            ArgumentNullException.ThrowIfNull(destinationFile);

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

        public IRandomOutputByteStream<UInt64> Create(FileShare share = FileShare.None)
        {
            _file.Refresh();
            try
            {
                return OpenWrite(_file.FullName, FileMode.Create, share);
            }
            finally
            {
                _file.Refresh();
            }
        }

        public IRandomOutputByteStream<UInt64> CreateNew(FileShare share = FileShare.None)
        {
            _file.Refresh();
            try
            {
                return OpenWrite(_file.FullName, FileMode.CreateNew, share);
            }
            finally
            {
                _file.Refresh();
            }
        }

        #region CreateNewText

        public TextWriter CreateNewText(FileShare share = FileShare.None) => CreateNewText(share, Encoding.UTF8);

        public TextWriter CreateNewText(Encoding encoding) => CreateNewText(FileShare.None, encoding);

        public TextWriter CreateNewText(FileShare share, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            _file.Refresh();
            try
            {
                return OpenWrite(_file.FullName, FileMode.CreateNew, share).AsTextWriter(encoding);
            }
            finally
            {
                _file.Refresh();
            }
        }

        #endregion

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

        #region CreateText

        public TextWriter CreateText(FileShare share = FileShare.None) => CreateText(share, Encoding.UTF8);

        public TextWriter CreateText(Encoding encoding) => CreateText(FileShare.None, encoding);

        public TextWriter CreateText(FileShare share, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            _file.Refresh();
            try
            {
                return OpenWrite(_file.FullName, FileMode.Create, share).AsTextWriter(encoding);
            }
            finally
            {
                _file.Refresh();
            }
        }

        #endregion

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
            ArgumentNullException.ThrowIfNull(baseDirectory);

            var dir = Directory.GetRelativePath(baseDirectory);
            return dir is null
                ? null
                : dir.Equals(".", StringComparison.Ordinal)
                ? Name
                : Path.Combine(dir, Name);
        }

        public void MoveTo(FilePath destinationFile, Boolean overwrite = false)
        {
            ArgumentNullException.ThrowIfNull(destinationFile);

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

        public IRandomInputByteStream<UInt64> OpenRead(FileShare share = FileShare.None)
        {
            _file.Refresh();
            try
            {
                return OpenRead(_file.FullName, share);
            }
            finally
            {
                _file.Refresh();
            }
        }

        #region OpenText

        public TextReader OpenText(FileShare share = FileShare.None) => OpenText(share, Encoding.UTF8);

        public TextReader OpenText(Encoding encoding) => OpenText(FileShare.None, encoding);

        public TextReader OpenText(FileShare share, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            _file.Refresh();
            try
            {
                return OpenRead(_file.FullName, share).AsTextReader(encoding);
            }
            finally
            {
                _file.Refresh();
            }
        }

        #endregion

        public IRandomOutputByteStream<UInt64> OpenWrite(FileShare share = FileShare.None)
        {
            _file.Refresh();
            try
            {
                return OpenWrite(_file.FullName, FileMode.OpenOrCreate, share);
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

        private static ISequentialOutputByteStream Append(String fullPath, FileShare share)
        {
            var outStream = (Stream?)null;
            var sequentialOutStream = (ISequentialOutputByteStream?)null;
            var success = false;
            try
            {
                outStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write, share);
                _ = outStream.Seek(0, SeekOrigin.End);
                sequentialOutStream = outStream.AsOutputByteStream();
                success = true;
                return sequentialOutStream;
            }
            finally
            {
                if (!success)
                {
                    sequentialOutStream?.Dispose();
                    outStream?.Dispose();
                }
            }
        }

        private static IRandomInputByteStream<UInt64> OpenRead(String fullPath, FileShare share)
        {
            var inStream = (Stream?)null;
            var sequentialInStream = (ISequentialInputByteStream?)null;
            var randomInStream = (IRandomInputByteStream<UInt64>?)null;
            var success = false;
            try
            {
                inStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, share);
                sequentialInStream = inStream.AsInputByteStream();
                randomInStream = sequentialInStream.AsRandomAccess<UInt64>();
                success = true;
                return randomInStream;
            }
            finally
            {
                if (!success)
                {
                    randomInStream?.Dispose();
                    sequentialInStream?.Dispose();
                    inStream?.Dispose();
                }
            }
        }

        private static IRandomOutputByteStream<UInt64> OpenWrite(String fullPath, FileMode mode, FileShare share)
        {
            var outStream = (Stream?)null;
            var sequentialOutStream = (ISequentialOutputByteStream?)null;
            var randomOutStream = (IRandomOutputByteStream<UInt64>?)null;
            var success = false;
            try
            {
                outStream = new FileStream(fullPath, mode, FileAccess.Write, share);
                sequentialOutStream = outStream.AsOutputByteStream();
                randomOutStream = sequentialOutStream.AsRandomAccess<UInt64>();
                success = true;
                return randomOutStream;
            }
            finally
            {
                if (!success)
                {
                    randomOutStream?.Dispose();
                    sequentialOutStream?.Dispose();
                    outStream?.Dispose();
                }
            }
        }
    }
}
