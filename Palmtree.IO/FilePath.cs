//#define _LOG_FILE_ACCESS
#define _RETRY_TO_CREAT_FILE_STREAM_AND_DELETE_FILE
#define _SLEEP_BETWEEN_RETRIES
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Palmtree.IO
{
    public class FilePath
        : FileSystemPath
    {
        /// <summary>
        /// パス名の最大長を示す整数。
        /// </summary>
        /// <remarks>
        /// この値は、.NET 9.0 (2025年5月現在) の native ソースコードにてハードコーディングされている。
        /// (<see href="https://github.com/dotnet/runtime/blob/main/src/coreclr/pal/inc/pal.h"/> など、複数個所)
        /// </remarks>
        private static readonly Int32 MAX_LONGPATH = 1024;

        private const Int32 _E_ERROR_SHARING_VIOLATION = unchecked((Int32)0x80070020u);
#if _RETRY_TO_CREAT_FILE_STREAM_AND_DELETE_FILE
        private const Int32 _COUNT_VALUE_FOR_RETRY_TO_AVOID_ACCEES_VIOLATION_ERROR = 10;
#else
        private const Int32 _COUNT_VALUE_FOR_RETRY_TO_AVOID_ACCEES_VIOLATION_ERROR = 0;
#endif
#if _SLEEP_BETWEEN_RETRIES
        private static readonly TimeSpan _INTERVAL_TIME_VALUE_FOR_RETRY_TO_AVOID_ACCEES_VIOLATION_ERROR = TimeSpan.FromMilliseconds(1);
#endif
        private static readonly Random _randomNumberGeneratorForUniqueFileName;
        private static readonly Char[] _uniqueFileNameMap;

        static FilePath()
        {
            _randomNumberGeneratorForUniqueFileName = new Random(Environment.TickCount);
            _uniqueFileNameMap = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v' };
#if DEBUG
            // _uniqueFileNameMap の要素が数字あるいは英小文字のみであることの確認
            Validation.Assert(_uniqueFileNameMap.All(c => c is >= '0' and <= '9' or >= 'a' and <= 'z'));

            // _uniqueFileNameMap の配列の長さが 32 であることの確認
            Validation.Assert(_uniqueFileNameMap.Length == 32);

            // _uniqueFileNameMap の要素が重複していないことの確認
            Validation.Assert(_uniqueFileNameMap.Distinct().Count() == 32);
#endif
        }

        public FilePath(String path)
            : this(GetFileInfo(path))
        {
        }

        private FilePath(FileInfo file)
            : base(file)
        {
            var directoryPath = Path.GetDirectoryName(FullName);
            Validation.Assert(directoryPath is not null);
            Directory = new DirectoryPath(directoryPath);
        }

        public DirectoryPath Directory { get; }
        public override Boolean Exists => File.Exists(FullName);
        public UInt64 Length => checked((UInt64)GetFileInfo(FullName).Length);

        public ISequentialOutputByteStream Append(FileShare share = FileShare.None) => Append(FullName, share);

        #region AppendText

        public TextWriter AppendText(FileShare share = FileShare.None) => AppendText(share, Encoding.UTF8);

        public TextWriter AppendText(Encoding encoding) => AppendText(FileShare.None, encoding);

        public TextWriter AppendText(FileShare share, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            return Append(FullName, share).AsTextWriter(encoding);
        }

        #endregion

        public void CopyTo(FilePath destinationFile, Boolean overwrite = false)
        {
            ArgumentNullException.ThrowIfNull(destinationFile);

            File.Copy(FullName, destinationFile.FullName, overwrite);
        }

        public IRandomOutputByteStream<UInt64> Create(FileShare share = FileShare.None) => OpenWrite(FullName, FileMode.Create, share);

        public IRandomOutputByteStream<UInt64> CreateNew(FileShare share = FileShare.None) => OpenWrite(FullName, FileMode.CreateNew, share);

        #region CreateNewText

        public TextWriter CreateNewText(FileShare share = FileShare.None) => CreateNewText(share, Encoding.UTF8);

        public TextWriter CreateNewText(Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            return CreateNewText(FileShare.None, encoding);
        }

        public TextWriter CreateNewText(FileShare share, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            return OpenWrite(FullName, FileMode.CreateNew, share).AsTextWriter(encoding);
        }

        #endregion

        public static FilePath CreateTemporaryFile(String prefix = "tmp", String suffix = ".tmp")
        {
            ArgumentNullException.ThrowIfNull(prefix);
            ArgumentNullException.ThrowIfNull(suffix);

            var temporaryDirectoryPath = Path.GetTempPath();
            try
            {
                return CreateUniqueFile(temporaryDirectoryPath, prefix, suffix);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"A temporary file could not be created.: directory=\"{temporaryDirectoryPath}\", {nameof(prefix)}=\"{prefix}\", {nameof(suffix)}=\"{suffix}\"", ex);
            }
            catch (IOException ex)
            {
                throw new IOException($"A temporary file could not be created.: directory=\"{temporaryDirectoryPath}\", {nameof(prefix)}=\"{prefix}\", {nameof(suffix)}=\"{suffix}\"", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"A unitemporaryque file could not be created.: directory=\"{temporaryDirectoryPath}\", {nameof(prefix)}=\"{prefix}\", {nameof(suffix)}=\"{suffix}\"", ex);
            }
        }

        #region CreateText

        public TextWriter CreateText(FileShare share = FileShare.None) => CreateText(share, Encoding.UTF8);

        public TextWriter CreateText(Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            return CreateText(FileShare.None, encoding);
        }

        public TextWriter CreateText(FileShare share, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            return OpenWrite(FullName, FileMode.Create, share).AsTextWriter(encoding);
        }

        #endregion

        public void Delete() => DeleteFile(FullName);

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

            System.IO.File.Move(FullName, destinationFile.FullName, overwrite);
        }

        public IRandomInputByteStream<UInt64> OpenRead(FileShare share = FileShare.None) => OpenRead(FullName, share);

        #region OpenText

        public TextReader OpenText(FileShare share = FileShare.None) => OpenText(share, Encoding.UTF8);

        public TextReader OpenText(Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            return OpenText(FileShare.None, encoding);
        }

        public TextReader OpenText(FileShare share, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(encoding);

            return OpenRead(FullName, share).AsTextReader(encoding);
        }

        #endregion

        public IRandomOutputByteStream<UInt64> OpenWrite(FileShare share = FileShare.None) => OpenWrite(FullName, FileMode.OpenOrCreate, share);

        public FilePath Replace(FilePath destinationFilePath, FilePath? destinationBackupFilePath)
        {
            ArgumentNullException.ThrowIfNull(destinationFilePath);

            return GetFileInfo(FullName).Replace(destinationFilePath.FullName, destinationBackupFilePath?.FullName);
        }

        public FilePath Replace(FilePath destinationFilePath, FilePath? destinationBackupFilePath, Boolean ignoreMetadataErrors)
        {
            ArgumentNullException.ThrowIfNull(destinationFilePath);

            return GetFileInfo(FullName).Replace(destinationFilePath.FullName, destinationBackupFilePath?.FullName, ignoreMetadataErrors);
        }

        public static implicit operator FileInfo(FilePath path)
        {
            ArgumentNullException.ThrowIfNull(path);

            return new(path.FullName);
        }

        public static implicit operator FilePath(FileInfo file)
        {
            ArgumentNullException.ThrowIfNull(file);

            return new(file.FullName);
        }

        internal static FilePath CreateUniqueFile(String baseDirectoryPath, String prefix, String suffix)
        {
            // 乱数を生成してパス名を決定する。
            // 乱数の範囲は [0..2^31-1] で 31 ビットで表現できる。
            // そのうちそれぞれ 5 ビットずつを 1 文字に変換し、合計 30 ビットを6文字に変換する。

            var uniqueFileNameBuilder = new StringBuilder(prefix.Length + 6 + suffix.Length);
            var retryCount = 0;
            while (true)
            {
                var randomNumber = _randomNumberGeneratorForUniqueFileName.Next();
                _ = uniqueFileNameBuilder.Clear();
                _ = uniqueFileNameBuilder.Append(prefix);
                _ = uniqueFileNameBuilder.Append(_uniqueFileNameMap[(randomNumber >> 25) & 0x1f]);
                _ = uniqueFileNameBuilder.Append(_uniqueFileNameMap[(randomNumber >> 20) & 0x1f]);
                _ = uniqueFileNameBuilder.Append(_uniqueFileNameMap[(randomNumber >> 15) & 0x1f]);
                _ = uniqueFileNameBuilder.Append(_uniqueFileNameMap[(randomNumber >> 10) & 0x1f]);
                _ = uniqueFileNameBuilder.Append(_uniqueFileNameMap[(randomNumber >> 5) & 0x1f]);
                _ = uniqueFileNameBuilder.Append(_uniqueFileNameMap[(randomNumber >> 0) & 0x1f]);
                _ = uniqueFileNameBuilder.Append(suffix);

                var path = Path.Combine(baseDirectoryPath, uniqueFileNameBuilder.ToString());
                var uniqueFilePath =
                    TryToCreateFilePath(path)
                    ?? throw new ArgumentException($"Can't create unique file. Probably parameter 'prefix' or parameter 'suffix' contains invalid characters.: {nameof(prefix)}=\"{prefix}\", {nameof(suffix)}=\"{suffix}\"");
                if (TryToCreateUniqueFile(uniqueFilePath.FullName, retryCount))
                    return uniqueFilePath;
                ++retryCount;
            }
        }

        protected override DateTime InternalCreationTimeUtc
        {
            get => File.GetCreationTimeUtc(FullName);
            set => File.SetCreationTimeUtc(FullName, value);
        }

        protected override DateTime InternalLastAccessTimeUtc
        {
            get => File.GetLastAccessTimeUtc(FullName);
            set => File.SetLastAccessTimeUtc(FullName, value);
        }

        protected override DateTime InternalLastWriteTimeUtc
        {
            get => File.GetLastWriteTimeUtc(FullName);
            set => File.SetLastWriteTime(FullName, value);
        }

        private static FileInfo GetFileInfo(String path)
        {
            ArgumentException.ThrowIfNullOrEmpty(path);

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
                outStream = CreateFileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write, share);
                _ = outStream.Seek(0, SeekOrigin.End);
                sequentialOutStream = outStream.AsOutputByteStream();
                success = true;
                return sequentialOutStream;
            }
            catch (Exception ex)
            {
#if DEBUG
                Validation.Debug.WriteLine(ex);
#elif TRACE
                Validation.Trace.WriteLine(ex);
#endif
                throw;
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
                inStream = CreateFileStream(fullPath, FileMode.Open, FileAccess.Read, share);
                sequentialInStream = inStream.AsInputByteStream();
                randomInStream = sequentialInStream.AsRandomAccess<UInt64>();
                success = true;
                return randomInStream;
            }
            catch (Exception ex)
            {
#if DEBUG
                Validation.Debug.WriteLine(ex);
#elif TRACE
                Validation.Trace.WriteLine(ex);
#endif
                throw;
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
                outStream = CreateFileStream(fullPath, mode, FileAccess.Write, share);
                sequentialOutStream = outStream.AsOutputByteStream();
                randomOutStream = sequentialOutStream.AsRandomAccess<UInt64>();
                success = true;
                return randomOutStream;
            }
            catch (Exception ex)
            {
#if DEBUG
                Validation.Debug.WriteLine(ex);
#elif TRACE
                Validation.Trace.WriteLine(ex);
#endif
                throw;
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1859:可能な場合は具象型を使用してパフォーマンスを向上させる", Justification = "Release ビルドでは復帰値の型を FileStream にすることが可能だが、Debug ビルドでは Stream にせざるを得ないため。")]
        private static Stream CreateFileStream(String fullPath, FileMode mode, FileAccess access, FileShare share)
        {
            for (var count = 0; ; ++count)
            {
                try
                {
                    var stream = new FileStream(fullPath, mode, access, share);
#if DEBUG
                    if (count > 0)
                        System.Diagnostics.Debug.WriteLine($"FilePath.CreateFileStream(String, FileMode, FileAccess, FileShare): Tried {count} times.");
#endif
#if DEBUG && _LOG_FILE_ACCESS
                    return stream.WithLogger();
#else
                    return stream;
#endif
                }
                catch (IOException ex)
                {
                    if (!OperatingSystem.IsWindows() || ex.HResult != _E_ERROR_SHARING_VIOLATION || count >= _COUNT_VALUE_FOR_RETRY_TO_AVOID_ACCEES_VIOLATION_ERROR)
                    {
#if DEBUG
                        Validation.Debug.WriteLine(ex);
#elif TRACE
                        Validation.Trace.WriteLine(ex);
#endif
                        throw;
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    Validation.Debug.WriteLine(ex);
#elif TRACE
                    Validation.Trace.WriteLine(ex);
#endif
                    throw;
                }
#if _SLEEP_BETWEEN_RETRIES
                Thread.Sleep(_INTERVAL_TIME_VALUE_FOR_RETRY_TO_AVOID_ACCEES_VIOLATION_ERROR);
#endif
            }
        }

        private static void DeleteFile(String fullPath)
        {
            for (var count = 0; ; ++count)
            {
                try
                {
                    File.Delete(fullPath);
#if DEBUG
                    if (count > 0)
                        System.Diagnostics.Debug.WriteLine($"FilePath.CreateFileStream(String, FileMode, FileAccess, FileShare): Tried {count} times.");
#endif
                    return;
                }
                catch (IOException ex)
                {
                    if (!OperatingSystem.IsWindows() || ex.HResult != _E_ERROR_SHARING_VIOLATION || count >= _COUNT_VALUE_FOR_RETRY_TO_AVOID_ACCEES_VIOLATION_ERROR)
                    {
#if DEBUG
                        Validation.Debug.WriteLine(ex);
#elif TRACE
                        Validation.Trace.WriteLine(ex);
#endif
                        throw;
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    Validation.Debug.WriteLine(ex);
#elif TRACE
                    Validation.Trace.WriteLine(ex);
#endif
                    throw;
                }
#if _SLEEP_BETWEEN_RETRIES
                Thread.Sleep(_INTERVAL_TIME_VALUE_FOR_RETRY_TO_AVOID_ACCEES_VIOLATION_ERROR);
#endif
            }
        }

        private static Boolean TryToCreateUniqueFile(String tempFilePath, Int32 retryCount)
        {
            if (retryCount < 100)
            {
                // 総試行回数が 100 回以下の場合

                // ユニークな名前のファイルの作成を試みる
                return TryToCreateUniqueFile(tempFilePath);
            }

            // 総試行回数が 100 を超えた場合
            // この先のルートは new FileStream() で例外が発生することがほぼ確定している。

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
        }

        private static Boolean TryToCreateUniqueFile(String tempFilePath)
        {
            var normalizedPath = NormalizePath(tempFilePath);
            if (OperatingSystem.IsWindows())
            {
                // Windows 
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

        /// <remarks>
        /// See NormalizePath method in <see hcref="https://github.com/dotnet/runtime/blob/main/src/coreclr/utilcode/longfilepathwrappers.cpp"/>
        /// </remarks>
        private static String NormalizePath(String path)
        {
            if (OperatingSystem.IsWindows())
            {
                if (path.Length <= 0
                    || path.StartsWith(@"\\.\", StringComparison.Ordinal)
                    || path.StartsWith(@"\\?\", StringComparison.Ordinal)
                    || path.StartsWith(@"\\?\UNC\", StringComparison.Ordinal)
                    || Path.IsPathFullyQualified(path) && path.Length < MAX_LONGPATH)
                {
                    return path;
                }

                var fullPath = Path.GetFullPath(path);
                return
                    fullPath.StartsWith(@"\\", StringComparison.Ordinal)
                    ? $@"\\?\UNC\{fullPath[2..]}"
                    : $@"\\?\{fullPath}";
            }
            else
            {
                return Path.GetFullPath(path);
            }
        }

        private static FilePath? TryToCreateFilePath(String path)
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
}
