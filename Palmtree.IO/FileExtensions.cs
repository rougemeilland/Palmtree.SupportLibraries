using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Palmtree.IO
{
    public static partial class FileExtensions
    {
        private static readonly Object _lockObject;

        static FileExtensions()
        {
            _lockObject = new Object();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FileInfo GetFile(this DirectoryInfo directory, String fileName)
        {
            if (directory is null)
                throw new ArgumentNullException(nameof(directory));
            if (String.IsNullOrEmpty(fileName))
                throw new ArgumentException($"'{nameof(fileName)}' must not be null or empty.", nameof(fileName));

            return new FileInfo(Path.Combine(directory.FullName, fileName));

        }

        #region GetSubDirectory

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DirectoryInfo GetSubDirectory(this DirectoryInfo directory, String subDirectoryName)
        {
            if (directory is null)
                throw new ArgumentNullException(nameof(directory));
            if (String.IsNullOrEmpty(subDirectoryName))
                throw new ArgumentException($"'{nameof(subDirectoryName)}' must not be null or empty.", nameof(subDirectoryName));

            return new DirectoryInfo(Path.Combine(directory.FullName, subDirectoryName));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DirectoryInfo GetSubDirectory(this DirectoryInfo directory, String subDirectoryName1, String subDirectoryName2)
        {
            if (String.IsNullOrEmpty(subDirectoryName1))
                throw new ArgumentException($"'{nameof(subDirectoryName1)}' must not be null or empty.", nameof(subDirectoryName1));
            if (String.IsNullOrEmpty(subDirectoryName2))
                throw new ArgumentException($"'{nameof(subDirectoryName2)}' must not be null or empty.", nameof(subDirectoryName2));

            return new DirectoryInfo(Path.Combine(directory.FullName, subDirectoryName1, subDirectoryName2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DirectoryInfo GetSubDirectory(this DirectoryInfo directory, String subDirectoryName1, String subDirectoryName2, String subDirectoryName3)
        {
            if (String.IsNullOrEmpty(subDirectoryName1))
                throw new ArgumentException($"'{nameof(subDirectoryName1)}' must not be null or empty.", nameof(subDirectoryName1));
            if (String.IsNullOrEmpty(subDirectoryName2))
                throw new ArgumentException($"'{nameof(subDirectoryName2)}' must not be null or empty.", nameof(subDirectoryName2));
            if (String.IsNullOrEmpty(subDirectoryName3))
                throw new ArgumentException($"'{nameof(subDirectoryName3)}' must not be null or empty.", nameof(subDirectoryName3));

            return new DirectoryInfo(Path.Combine(directory.FullName, subDirectoryName1, subDirectoryName2, subDirectoryName3));
        }

        public static DirectoryInfo GetSubDirectory(this DirectoryInfo directory, params String[] subDirectoryNames)
        {
            if (subDirectoryNames is null)
                throw new ArgumentNullException(nameof(subDirectoryNames));

            var pathElements = new String[subDirectoryNames.Length + 1];
            pathElements[0] = directory.FullName;
            for (var index = 0; index < subDirectoryNames.Length; ++index)
            {
                if (String.IsNullOrEmpty(subDirectoryNames[index]))
                    throw new ArgumentException($"'{nameof(subDirectoryNames)}[{index}]' must not be null or empty.", nameof(subDirectoryNames));
                pathElements[index + 1] = subDirectoryNames[index];
            }

            return new DirectoryInfo(Path.Combine(pathElements));
        }

        #endregion

        public static String GetNameWithoutExtension(this FileInfo file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return Path.GetFileNameWithoutExtension(file.Name);
        }

        #region ReadAllBytes

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] ReadAllBytes(this FileInfo file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return File.ReadAllBytes(file.FullName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte[] ReadAllBytes(this FilePath file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return File.ReadAllBytes(file.FullName);
        }

        #endregion

        #region ReadAllLines

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[] ReadAllLines(this FileInfo file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return File.ReadAllLines(file.FullName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[] ReadAllLines(this FilePath file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return File.ReadAllLines(file.FullName);
        }

        #endregion

        #region ReadLines

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> ReadLines(this FileInfo file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return File.ReadLines(file.FullName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> ReadLines(this FilePath file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return File.ReadLines(file.FullName);
        }

        #endregion

        #region WriteAllBytes

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllBytes(this FileInfo file, IEnumerable<Byte> data)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            using var stream = file.OpenWrite();
            stream.WriteByteSequence(data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllBytes(this FilePath file, IEnumerable<Byte> data)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            using var stream = file.OpenWrite();
            stream.WriteByteSequence(data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllBytes(this FileInfo file, Byte[] data)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            File.WriteAllBytes(file.FullName, data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllBytes(this FilePath file, Byte[] data)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            File.WriteAllBytes(file.FullName, data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllBytes(this FileInfo file, ReadOnlyMemory<Byte> data)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            using var stream = file.OpenWrite();
            stream.WriteBytes(data.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllBytes(this FilePath file, ReadOnlyMemory<Byte> data)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            using var stream = file.OpenWrite();
            stream.WriteBytes(data.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllBytes(this FileInfo file, ReadOnlySpan<Byte> data)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            using var stream = file.OpenWrite();
            stream.WriteBytes(data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllBytes(this FilePath file, ReadOnlySpan<Byte> data)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            using var stream = file.OpenWrite();
            stream.WriteBytes(data);
        }

        #endregion

        #region WriteAllText

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FileInfo file, String text)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            File.WriteAllText(file.FullName, text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FilePath file, String text)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            File.WriteAllText(file.FullName, text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FileInfo file, String text, Encoding encoding)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (text is null)
                throw new ArgumentNullException(nameof(text));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            File.WriteAllText(file.FullName, text, encoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FilePath file, String text, Encoding encoding)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (text is null)
                throw new ArgumentNullException(nameof(text));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            File.WriteAllText(file.FullName, text, encoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FileInfo file, String[] lines)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));

            File.WriteAllLines(file.FullName, lines);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FilePath file, String[] lines)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));

            File.WriteAllLines(file.FullName, lines);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FileInfo file, IEnumerable<String> lines)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));

            File.WriteAllLines(file.FullName, lines);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FilePath file, IEnumerable<String> lines)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));

            File.WriteAllLines(file.FullName, lines);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FileInfo file, String[] lines, Encoding encoding)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FilePath file, String[] lines, Encoding encoding)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FileInfo file, IEnumerable<String> lines, Encoding encoding)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteAllText(this FilePath file, IEnumerable<String> lines, Encoding encoding)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        #endregion

        #region RenameFile

        public static (FilePath File, Boolean AlreadyExists) RenameFile(this FileInfo sourceFile, String newFileName)
            => ((FilePath)sourceFile).RenameFile(newFileName);

        public static (FilePath File, Boolean AlreadyExists) RenameFile(this FilePath sourceFile, String newFileName)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));
            if (String.IsNullOrEmpty(newFileName))
                throw new ArgumentException($"'{nameof(newFileName)}' must not be null or empty.", nameof(newFileName));

            var sourceFileDirectory = sourceFile.Directory ?? throw new ArgumentException($"{nameof(sourceFile)} is the relative path.", nameof(sourceFile));
            var sourceFileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);
            var fileNameMatch = GetSimpleFileNamePattern().Match(sourceFileNameWithoutExtension);
            if (fileNameMatch.Success)
                sourceFileNameWithoutExtension = fileNameMatch.Groups["path"].Value;
            var sourceFileExtension = Path.GetExtension(newFileName);
            lock (_lockObject)
            {
                var retryCount = 1;
                while (true)
                {
                    var newFile =
                        sourceFileDirectory.GetFile(
                            $"{sourceFileNameWithoutExtension}{(retryCount <= 1 ? "" : $" ({retryCount})")}{sourceFileExtension}");
                    if (String.Equals(newFile.FullName, sourceFile.FullName, StringComparison.OrdinalIgnoreCase))
                    {
                        return (newFile, false);
                    }
                    else if (!newFile.Exists)
                    {
                        File.Move(sourceFile.FullName, newFile.FullName);
                        return (newFile, false);
                    }
                    else if (newFile.Length == sourceFile.Length &&
                            newFile.OpenRead().StreamBytesEqual(sourceFile.OpenRead()))
                    {
                        sourceFile.Delete();
                        return (newFile, true);
                    }
                    else
                    {
                        ++retryCount;
                    }
                }
            }
        }

        #endregion

        #region SafetyDelete

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafetyDelete(this DirectoryInfo directory, Boolean recursive = false)
        {
            try
            {
                if (Directory.Exists(directory.FullName))
                    Directory.Delete(directory.FullName, recursive);
            }
            catch (Exception)
            {
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafetyDelete(this DirectoryPath directory, Boolean recursive = false)
        {
            try
            {
                if (directory.Exists)
                    directory.Delete(recursive);
            }
            catch (Exception)
            {
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafetyDelete(this FileInfo file)
        {
            try
            {
                if (File.Exists(file.FullName))
                    File.Delete(file.FullName);
            }
            catch (Exception)
            {
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafetyDelete(this FilePath file)
        {
            try
            {
                if (file.Exists)
                    file.Delete();
            }
            catch (Exception)
            {
            }
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (UInt32 Crc, UInt64 Length) CalculateCrc24(this FileInfo sourceFile, IProgress<UInt64>? progress = null)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));

            return sourceFile.OpenRead().CalculateCrc24(progress);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (UInt32 Crc, UInt64 Length) CalculateCrc32(this FileInfo sourceFile, IProgress<UInt64>? progress = null)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));

            return sourceFile.OpenRead().CalculateCrc32(progress);
        }

        public static IEnumerable<FilePath> EnumerateFilesFromArgument(this IEnumerable<String> args, Boolean recursive = true)
        {
            if (args is null)
                throw new ArgumentNullException(nameof(args));

            return
                args
                .SelectMany(arg =>
                {
                    var file = TryParseAsFilePath(arg);
                    if (file is not null)
                        return new[] { file };
                    var directory = TryParseAsDirectoryPath(arg);
                    return
                        directory is not null
                        ? directory.EnumerateFiles(recursive)
                        : Array.Empty<FilePath>();
                });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FilePath? TryParseAsFilePath(String path)
        {
            try
            {
                var file = new FilePath(path);
                return file.Exists ? file : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static DirectoryPath? TryParseAsDirectoryPath(String path)
        {
            try
            {
                var directory = new DirectoryPath(path);
                return directory.Exists ? directory : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [GeneratedRegex(@"^(?<path>.*?)(\s*\([0-9]+\))+$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant)]
        private static partial Regex GetSimpleFileNamePattern();
    }
}
