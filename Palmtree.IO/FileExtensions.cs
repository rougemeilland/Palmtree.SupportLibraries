using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static FileInfo GetFile(this DirectoryInfo directory, String fileName)
        {
            ArgumentNullException.ThrowIfNull(directory);
            ArgumentException.ThrowIfNullOrEmpty(fileName);

            return new FileInfo(Path.Combine(directory.FullName, fileName));

        }

        #region GetSubDirectory

        public static DirectoryInfo GetSubDirectory(this DirectoryInfo directory, String subDirectoryName)
        {
            ArgumentNullException.ThrowIfNull(directory);
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName);

            return new DirectoryInfo(Path.Combine(directory.FullName, subDirectoryName));
        }

        public static DirectoryInfo GetSubDirectory(this DirectoryInfo directory, String subDirectoryName1, String subDirectoryName2)
        {
            ArgumentNullException.ThrowIfNull(directory);
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName1);
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName2);

            return new DirectoryInfo(Path.Combine(directory.FullName, subDirectoryName1, subDirectoryName2));
        }

        public static DirectoryInfo GetSubDirectory(this DirectoryInfo directory, String subDirectoryName1, String subDirectoryName2, String subDirectoryName3)
        {
            ArgumentNullException.ThrowIfNull(directory);
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName1);
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName2);
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName3);

            return new DirectoryInfo(Path.Combine(directory.FullName, subDirectoryName1, subDirectoryName2, subDirectoryName3));
        }

        public static DirectoryInfo GetSubDirectory(this DirectoryInfo directory, params String[] subDirectoryNames)
        {
            ArgumentNullException.ThrowIfNull(directory);
            ArgumentNullException.ThrowIfNull(subDirectoryNames);

            var pathElements = new String[subDirectoryNames.Length + 1];
            pathElements[0] = directory.FullName;
            for (var index = 0; index < subDirectoryNames.Length; ++index)
            {
                ArgumentException.ThrowIfNullOrEmpty(subDirectoryNames[index], $"subDirectoryNames[{index}]");

                pathElements[index + 1] = subDirectoryNames[index];
            }

            return new DirectoryInfo(Path.Combine(pathElements));
        }

        #endregion

        public static String GetNameWithoutExtension(this FileSystemInfo info)
        {
            ArgumentNullException.ThrowIfNull(info);

            return Path.GetFileNameWithoutExtension(info.Name);
        }

        #region ReadAllBytes

        public static Byte[] ReadAllBytes(this FileInfo file)
        {
            ArgumentNullException.ThrowIfNull(file);

            return File.ReadAllBytes(file.FullName);
        }

        public static Byte[] ReadAllBytes(this FilePath file)
        {
            ArgumentNullException.ThrowIfNull(file);

            return File.ReadAllBytes(file.FullName);
        }

        #endregion

        #region ReadAllLines

        public static String[] ReadAllLines(this FileInfo file)
        {
            ArgumentNullException.ThrowIfNull(file);

            return File.ReadAllLines(file.FullName);
        }

        public static String[] ReadAllLines(this FilePath file)
        {
            ArgumentNullException.ThrowIfNull(file);

            return File.ReadAllLines(file.FullName);
        }

        #endregion

        #region ReadLines

        public static IEnumerable<String> ReadLines(this FileInfo file)
        {
            ArgumentNullException.ThrowIfNull(file);

            return File.ReadLines(file.FullName);
        }

        public static IEnumerable<String> ReadLines(this FilePath file)
        {
            ArgumentNullException.ThrowIfNull(file);

            return File.ReadLines(file.FullName);
        }

        #endregion

        #region ReadAllText

        public static String ReadAllText(this FileInfo file)
        {
            ArgumentNullException.ThrowIfNull(file);

            return File.ReadAllText(file.FullName);
        }

        public static String ReadAllText(this FileInfo file, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(encoding);

            return File.ReadAllText(file.FullName, encoding);
        }

        public static String ReadAllText(this FilePath file)
        {
            ArgumentNullException.ThrowIfNull(file);

            return File.ReadAllText(file.FullName);
        }

        public static String ReadAllText(this FilePath file, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(encoding);

            return File.ReadAllText(file.FullName, encoding);
        }

        #endregion

        #region WriteAllBytes

        public static void WriteAllBytes(this FileInfo file, IEnumerable<Byte> data)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(data);

            using var stream = file.OpenWrite();
            stream.WriteByteSequence(data);
        }

        public static void WriteAllBytes(this FilePath file, IEnumerable<Byte> data)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(data);

            using var stream = file.OpenWrite();
            stream.WriteByteSequence(data);
        }

        public static void WriteAllBytes(this FileInfo file, Byte[] data)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(data);

            File.WriteAllBytes(file.FullName, data);
        }

        public static void WriteAllBytes(this FilePath file, Byte[] data)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(data);

            File.WriteAllBytes(file.FullName, data);
        }

        public static void WriteAllBytes(this FileInfo file, ReadOnlyMemory<Byte> data)
        {
            ArgumentNullException.ThrowIfNull(file);

            using var stream = file.OpenWrite();
            stream.WriteBytes(data.Span);
        }

        public static void WriteAllBytes(this FilePath file, ReadOnlyMemory<Byte> data)
        {
            ArgumentNullException.ThrowIfNull(file);

            using var stream = file.OpenWrite();
            stream.WriteBytes(data.Span);
        }

        public static void WriteAllBytes(this FileInfo file, ReadOnlySpan<Byte> data)
        {
            ArgumentNullException.ThrowIfNull(file);

            using var stream = file.OpenWrite();
            stream.WriteBytes(data);
        }

        public static void WriteAllBytes(this FilePath file, ReadOnlySpan<Byte> data)
        {
            ArgumentNullException.ThrowIfNull(file);

            using var stream = file.OpenWrite();
            stream.WriteBytes(data);
        }

        #endregion

        #region WriteAllLines

        public static void WriteAllLines(this FileInfo file, IEnumerable<String> lines)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);

            File.WriteAllLines(file.FullName, lines);
        }

        public static void WriteAllLines(this FileInfo file, String[] lines)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);

            File.WriteAllLines(file.FullName, lines);
        }

        public static void WriteAllLines(this FileInfo file, IEnumerable<String> lines, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);
            ArgumentNullException.ThrowIfNull(encoding);

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        public static void WriteAllLines(this FileInfo file, String[] lines, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);
            ArgumentNullException.ThrowIfNull(encoding);

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        public static void WriteAllLines(this FilePath file, IEnumerable<String> lines)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);

            File.WriteAllLines(file.FullName, lines);
        }

        public static void WriteAllLines(this FilePath file, String[] lines)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);

            File.WriteAllLines(file.FullName, lines);
        }

        public static void WriteAllLines(this FilePath file, IEnumerable<String> lines, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);
            ArgumentNullException.ThrowIfNull(encoding);

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        public static void WriteAllLines(this FilePath file, String[] lines, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);
            ArgumentNullException.ThrowIfNull(encoding);

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        #endregion

        #region WriteAllText

        public static void WriteAllText(this FileInfo file, String text)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentException.ThrowIfNullOrEmpty(text);

            File.WriteAllText(file.FullName, text);
        }

        public static void WriteAllText(this FilePath file, String text)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentException.ThrowIfNullOrEmpty(text);

            File.WriteAllText(file.FullName, text);
        }

        public static void WriteAllText(this FileInfo file, String text, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentException.ThrowIfNullOrEmpty(text);
            ArgumentNullException.ThrowIfNull(encoding);

            File.WriteAllText(file.FullName, text, encoding);
        }

        public static void WriteAllText(this FilePath file, String text, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentException.ThrowIfNullOrEmpty(text);
            ArgumentNullException.ThrowIfNull(encoding);

            File.WriteAllText(file.FullName, text, encoding);
        }

        public static void WriteAllText(this FileInfo file, String[] lines)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);

            File.WriteAllLines(file.FullName, lines);
        }

        public static void WriteAllText(this FilePath file, String[] lines)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);

            File.WriteAllLines(file.FullName, lines);
        }

        public static void WriteAllText(this FileInfo file, IEnumerable<String> lines)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);

            File.WriteAllLines(file.FullName, lines);
        }

        public static void WriteAllText(this FilePath file, IEnumerable<String> lines)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);

            File.WriteAllLines(file.FullName, lines);
        }

        public static void WriteAllText(this FileInfo file, String[] lines, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);
            ArgumentNullException.ThrowIfNull(encoding);

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        public static void WriteAllText(this FilePath file, String[] lines, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);
            ArgumentNullException.ThrowIfNull(encoding);

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        public static void WriteAllText(this FileInfo file, IEnumerable<String> lines, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);
            ArgumentNullException.ThrowIfNull(encoding);

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        public static void WriteAllText(this FilePath file, IEnumerable<String> lines, Encoding encoding)
        {
            ArgumentNullException.ThrowIfNull(file);
            ArgumentNullException.ThrowIfNull(lines);
            ArgumentNullException.ThrowIfNull(encoding);

            File.WriteAllLines(file.FullName, lines, encoding);
        }

        #endregion

        #region RenameFile

        public static (FilePath File, Boolean AlreadyExists) RenameFile(this FileInfo sourceFile, String newFileName)
        {
            ArgumentNullException.ThrowIfNull(sourceFile);
            ArgumentException.ThrowIfNullOrEmpty(newFileName);

            return ((FilePath)sourceFile).RenameFile(newFileName);
        }

        public static (FilePath File, Boolean AlreadyExists) RenameFile(this FilePath sourceFile, String newFileName)
        {
            ArgumentNullException.ThrowIfNull(sourceFile);
            ArgumentException.ThrowIfNullOrEmpty(newFileName);

            var sourceFileDirectory = sourceFile.Directory ?? throw new ArgumentException($"{nameof(sourceFile)} is the relative path.", nameof(sourceFile));
            var sourceFileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);
            var fileNameMatch = GetAlternateFileNamePattern().Match(sourceFileNameWithoutExtension);
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

        public static void SafetyDelete(this DirectoryInfo directory, Boolean recursive = false)
        {
            ArgumentNullException.ThrowIfNull(directory);

            try
            {
                if (Directory.Exists(directory.FullName))
                    Directory.Delete(directory.FullName, recursive);
            }
            catch (Exception)
            {
            }
        }

        public static void SafetyDelete(this DirectoryPath directory, Boolean recursive = false)
        {
            ArgumentNullException.ThrowIfNull(directory);

            try
            {
                if (directory.Exists)
                    directory.Delete(recursive);
            }
            catch (Exception)
            {
            }
        }

        public static void SafetyDelete(this FileInfo file)
        {
            ArgumentNullException.ThrowIfNull(file);

            try
            {
                if (File.Exists(file.FullName))
                    File.Delete(file.FullName);
            }
            catch (Exception)
            {
            }
        }

        public static void SafetyDelete(this FilePath file)
        {
            ArgumentNullException.ThrowIfNull(file);

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

        public static (UInt32 Crc, UInt64 Length) CalculateCrc24(this FileInfo sourceFile, IProgress<UInt64>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(sourceFile);

            return sourceFile.OpenRead().CalculateCrc24(progress);
        }

        public static (UInt32 Crc, UInt64 Length) CalculateCrc32(this FileInfo sourceFile, IProgress<UInt64>? progress = null)
        {
            ArgumentNullException.ThrowIfNull(sourceFile);

            return sourceFile.OpenRead().CalculateCrc32(progress);
        }

        public static IEnumerable<FilePath> EnumerateFilesFromArgument(this IEnumerable<String> args, Boolean recursive = true)
        {
            ArgumentNullException.ThrowIfNull(args);

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
                        : [];
                });
        }

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

        [GeneratedRegex(@"^(?<path>.*?)(\s*\([0-9]+\))+$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
        private static partial Regex GetAlternateFileNamePattern();
    }
}
