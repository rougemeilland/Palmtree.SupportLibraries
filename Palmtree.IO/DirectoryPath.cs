using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace Palmtree.IO
{
    public class DirectoryPath
        : FileSystemPath
    {
        private readonly DirectoryInfo _directory;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DirectoryPath(String path)
            : this(GetDirectoryInfo(path))
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DirectoryPath(DirectoryInfo directory)
            : base(directory)
        {
            _directory = directory;
        }

        public DirectoryPath? Parent
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                _directory.Refresh();
                var parent = _directory.Parent;
                return
                    parent is null
                    ? null
                    : new DirectoryPath(parent);
            }
        }

        public DirectoryPath Root
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                _directory.Refresh();
                return new DirectoryPath(_directory.Root);
            }
        }

        public DirectoryPath Create()
        {
            _directory.Refresh();
            try
            {
                if (!_directory.Exists)
                    _directory.Create();
                return this;
            }
            finally
            {
                _directory.Refresh();
#if DEBUG
                ValidationPath();
#endif
            }
        }

        public void Delete(Boolean recursive = false)
        {
            _directory.Refresh();
            try
            {
                _directory.Delete(recursive);
            }
            finally
            {
                _directory.Refresh();
#if DEBUG
                ValidationPath();
#endif
            }
        }

        public IEnumerable<DirectoryPath> EnumerateDirectories(Boolean recursive = false)
        {
            _directory.Refresh();
            try
            {
                var subDirectories =
                    _directory.EnumerateDirectories(
                        "*",
                        recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                foreach (var directory in subDirectories)
                    yield return new DirectoryPath(directory);
            }
            finally
            {
                _directory.Refresh();
            }
        }

        public IEnumerable<FilePath> EnumerateFiles(Boolean recursive = false)
        {
            _directory.Refresh();
            try
            {
                var subFiles =
                    _directory.EnumerateFiles(
                        "*",
                        recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                foreach (var file in subFiles)
                    yield return FilePath.CreateInstance(file);
            }
            finally
            {
                _directory.Refresh();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FilePath GetFile(String fileName)
        {
            if (fileName is null)
                throw new ArgumentNullException(nameof(fileName));

            _directory.Refresh();
            try
            {
                return new FilePath(Path.Combine(_directory.FullName, fileName));
            }
            finally
            {
                _directory.Refresh();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DirectoryPath GetSubDirectory(String subDirectoryName)
        {
            if (subDirectoryName is null)
                throw new ArgumentNullException(nameof(subDirectoryName));

            _directory.Refresh();
            try
            {
                return new DirectoryPath(Path.Combine(_directory.FullName, subDirectoryName));
            }
            finally
            {
                _directory.Refresh();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DirectoryPath GetSubDirectory(String subDirectoryName1, String subDirectoryName2)
        {
            if (String.IsNullOrEmpty(subDirectoryName1))
                throw new ArgumentException($"'{nameof(subDirectoryName1)}' must not be null or empty.", nameof(subDirectoryName1));
            if (String.IsNullOrEmpty(subDirectoryName2))
                throw new ArgumentException($"'{nameof(subDirectoryName2)}' must not be null or empty.", nameof(subDirectoryName2));

            _directory.Refresh();
            try
            {
                return new DirectoryPath(Path.Combine(_directory.FullName, subDirectoryName1, subDirectoryName2));
            }
            finally
            {
                _directory.Refresh();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DirectoryPath GetSubDirectory(String subDirectoryName1, String subDirectoryName2, String subDirectoryName3)
        {
            if (String.IsNullOrEmpty(subDirectoryName1))
                throw new ArgumentException($"'{nameof(subDirectoryName1)}' must not be null or empty.", nameof(subDirectoryName1));
            if (String.IsNullOrEmpty(subDirectoryName2))
                throw new ArgumentException($"'{nameof(subDirectoryName2)}' must not be null or empty.", nameof(subDirectoryName2));
            if (String.IsNullOrEmpty(subDirectoryName3))
                throw new ArgumentException($"'{nameof(subDirectoryName3)}' must not be null or empty.", nameof(subDirectoryName3));

            _directory.Refresh();
            try
            {
                return new DirectoryPath(Path.Combine(_directory.FullName, subDirectoryName1, subDirectoryName2, subDirectoryName3));
            }
            finally
            {
                _directory.Refresh();
            }
        }

        public DirectoryPath GetSubDirectory(params String[] subDirectoryNames)
        {
            if (subDirectoryNames is null)
                throw new ArgumentNullException(nameof(subDirectoryNames));

            var pathElements = new String[subDirectoryNames.Length + 1];
            pathElements[0] = _directory.FullName;
            for (var index = 0; index < subDirectoryNames.Length; ++index)
            {
                if (String.IsNullOrEmpty(subDirectoryNames[index]))
                    throw new ArgumentException($"'{nameof(subDirectoryNames)}[{index}]' must not be null or empty.", nameof(subDirectoryNames));
                pathElements[index + 1] = subDirectoryNames[index];
            }

            _directory.Refresh();
            try
            {
                return new DirectoryPath(Path.Combine(pathElements));
            }
            finally
            {
                _directory.Refresh();
            }
        }

        public void MoveTo(DirectoryPath destinationDirectory)
        {
            if (destinationDirectory is null)
                throw new ArgumentNullException(nameof(destinationDirectory));

            _directory.Refresh();
            destinationDirectory.Refresh();
            try
            {
                Directory.Move(_directory.FullName, destinationDirectory.FullName);
            }
            finally
            {
                _directory.Refresh();
                destinationDirectory.Refresh();
#if DEBUG
                ValidationPath();
                destinationDirectory.ValidationPath();
#endif
            }
        }

        public static DirectoryPath? UserHomeDirectory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                return String.IsNullOrEmpty(path) ? null : new DirectoryPath(path);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator DirectoryInfo(DirectoryPath path)
        {
            if (path is null)
                throw new ArgumentNullException(nameof(path));

            return new(path._directory.FullName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator DirectoryPath(DirectoryInfo directory)
        {
            if (directory is null)
                throw new ArgumentNullException(nameof(directory));

            return new(new DirectoryInfo(directory.FullName));
        }

        /// <remarks>
        /// The same instance as the object indicated by parameter <paramref name="directory"/> must not be used elsewhere.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static DirectoryPath CreateInstance(DirectoryInfo directory) => new(directory);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static DirectoryInfo GetDirectoryInfo(String path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentException($"'{nameof(path)}' must not be null or empty.", nameof(path));

            try
            {
                if (path.EndsWith(Path.AltDirectorySeparatorChar) || path.EndsWith(Path.DirectorySeparatorChar))
                    path = path[..^1];
                return new DirectoryInfo(path);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"A character string that cannot be used as a directory path name was specified. : \"{path}\"", nameof(path), ex);
            }
        }
    }
}
