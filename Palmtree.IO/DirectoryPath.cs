using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Palmtree.IO
{
    public partial class DirectoryPath
        : FileSystemPath
    {
        private const String _SERVER_NAME_LOCAL_HOST = "localhost";

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

        public static DirectoryPath CurrentDirectory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new(Environment.CurrentDirectory);
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

        public static DirectoryPath? UserHomeDirectory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                return String.IsNullOrEmpty(path) ? null : new DirectoryPath(path);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<DirectoryPath> EnumerateDirectories(Boolean recursive = false)
            => EnumerateDirectories("*", recursive);

        public IEnumerable<DirectoryPath> EnumerateDirectories(String namePattern, Boolean recursive = false)
        {
            _directory.Refresh();
            try
            {
                var subDirectories =
                    _directory.EnumerateDirectories(
                        namePattern,
                        recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                foreach (var directory in subDirectories)
                    yield return new DirectoryPath(directory);
            }
            finally
            {
                _directory.Refresh();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<FilePath> EnumerateFiles(Boolean recursive = false)
            => EnumerateFiles("*", recursive);

        public IEnumerable<FilePath> EnumerateFiles(String namePattern, Boolean recursive = false)
        {
            _directory.Refresh();
            try
            {
                var subFiles =
                    _directory.EnumerateFiles(
                        namePattern,
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

        public DirectoryPath GetCasePreservedPath()
        {
            Match m;
            if (OperatingSystem.IsWindows() && (m = GetDosRootPathPattern().Match(FullName)).Success)
            {
                return new DirectoryInfo($"{m.Groups["driveLetter"].Value.ToUpperInvariant()}:\\");
            }
            else if (OperatingSystem.IsWindows() && (m = GetDosDeviceRootPathPattern().Match(FullName)).Success)
            {
                var prefix = m.Groups["prefix"].Value;
                var driveLetter = m.Groups["driveLetter"];
                var guid = m.Groups["guid"];
                var bootPartition = m.Groups["bootPartition"];
                Validation.Assert(driveLetter.Success || guid.Success || bootPartition.Success, "driveLetter.Success || guid.Success || bootPartition.Success");
                if (driveLetter.Success)
                    return new DirectoryInfo($"\\\\{prefix}\\{driveLetter.Value.ToUpperInvariant()}:\\");
                else if (guid.Success)
                    return new DirectoryInfo($"\\\\{prefix}\\Volume{guid.Value.ToLowerInvariant()}\\");
                else
                    return new DirectoryInfo($"\\\\{prefix}\\BootPartition\\");
            }
            else if (OperatingSystem.IsWindows() && (m = GetUncLinkPattern().Match(FullName)).Success)
            {
                var prefix = m.Groups["prefix"].Value;
                var serverName = m.Groups["serverName"].Value;
                var casePreservedServerName = GetCasePreservedServerName(serverName);
                var sharedResourceName = m.Groups["sharedResourceName"].Value.ToLowerInvariant();
                var casePreservedSharedResourceName = EnumerateSharedResourceNames(casePreservedServerName).Where(s => s.Equals(sharedResourceName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() ?? sharedResourceName;
                return new DirectoryInfo($"\\\\{prefix}\\UNC\\{casePreservedServerName}\\{casePreservedSharedResourceName}");
            }
            else if (OperatingSystem.IsWindows() && (m = GetUncRootPathPattern().Match(FullName)).Success)
            {
                var serverName = m.Groups["serverName"].Value.ToLowerInvariant();
                var casePreservedServerName = GetCasePreservedServerName(serverName);
                var sharedResourceName = m.Groups["sharedResourceName"].Value;
                var casePreservedSharedResourceName =
                    EnumerateSharedResourceNames(casePreservedServerName)
                    .Where(s => s.Equals(sharedResourceName, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault()
                    ?? sharedResourceName;
                return new DirectoryInfo($"\\\\{casePreservedServerName}\\{casePreservedSharedResourceName}");
            }
            else
            {
                var parent = Parent;
                if (parent is null)
                    return this;
                var found = parent.GetCasePreservedPath().EnumerateDirectories(Name).Take(2).ToArray();
                if (found.Length != 1)
                    return this;
                return found[0];
            }

            static String GetCasePreservedServerName(String serverName)
            {
                try
                {
                    if (IPAddress.TryParse(serverName, out var ipAddress))
                    {
                        return
                            IPAddress.IsLoopback(ipAddress)
                            ? _SERVER_NAME_LOCAL_HOST
                            : Dns.GetHostEntry(serverName).HostName;
                    }
                    else if (serverName.Equals(_SERVER_NAME_LOCAL_HOST, StringComparison.OrdinalIgnoreCase))
                    {
                        return _SERVER_NAME_LOCAL_HOST;
                    }
                    else
                    {
                        var dnsHostName = Dns.GetHostEntry(serverName).HostName;
                        return
                            dnsHostName.Equals(serverName, StringComparison.OrdinalIgnoreCase)
                            ? dnsHostName
                            : serverName.Contains('.')
                            ? serverName.ToLowerInvariant()
                            : serverName.ToUpperInvariant();
                    }
                }
                catch (Exception)
                {
                    return
                        serverName.Contains('.')
                        ? serverName.ToLowerInvariant()
                        : serverName.ToUpperInvariant();
                }
            }

            static IEnumerable<String> EnumerateSharedResourceNames(String serverName)
            {
                var resume_handle = 0;
                var sizeOf_SHARE_INFO_0 = Marshal.SizeOf<InterOpForWindows.SHARE_INFO_0>();

                var ret =
                    InterOpForWindows.NetShareEnum(
                        serverName,
                        InterOpForWindows.SharedDataInformationLevel.Level0,
                        out var bufPtr,
                        InterOpForWindows.MAX_PREFERRED_LENGTH,
                        out var entriesread,
                        out _,
                        ref resume_handle);
                if (ret != InterOpForWindows.NERR.NERR_Success)
                {
                    if (ret == InterOpForWindows.NERR.ERROR_MORE_DATA)
                        _ = InterOpForWindows.NetApiBufferFree(bufPtr);
                    throw new Win32Exception((Int32)ret);
                }

                var ptr = bufPtr;
                for (var index = 0; index < entriesread; ++index)
                {
                    yield return Marshal.PtrToStructure<InterOpForWindows.SHARE_INFO_0>(ptr).NetName;
                    ptr += sizeOf_SHARE_INFO_0;
                }

                _ = InterOpForWindows.NetApiBufferFree(bufPtr);
            }
        }

        public String? GetRelativePath(DirectoryPath baseDirectory)
        {
            var relativePathElements1 = new List<String>(); // this と baseDirectory の共通のディレクトリに baseDirectory から 遡るための ".." のリスト
            var relativePathElements2 = new List<String>(); // this と baseDirectory の共通のディレクトリから this に至るまでの要素のリスト
            var directoryPath = (DirectoryPath?)GetCasePreservedPath();
            var baseDirectoryPath = (DirectoryPath?)baseDirectory.GetCasePreservedPath();
            while (directoryPath is not null && baseDirectoryPath is not null)
            {
                if (directoryPath.FullName.Equals(baseDirectoryPath.FullName, StringComparison.Ordinal))
                {
                    // directoryPath と baseDirectoryPath が完全一致した場合

                    // relativePathElements1 と relativePathElements2 を結合したパス名を返す
                    var concatinatedPathElementList = relativePathElements1.Concat(relativePathElements2).ToArray();
                    return
                        concatinatedPathElementList.Length <= 0
                        ? "."
                        : Path.Combine(concatinatedPathElementList);
                }
                else if (directoryPath.FullName.Length > baseDirectoryPath.FullName.Length)
                {
                    // baseDirectoryPath のフルパス名よりもdirectoryPath のフルパス名の方が長い場合

                    var dir = directoryPath.Parent;
                    if (dir is not null)
                        relativePathElements2.Add(directoryPath.Name);
                    directoryPath = dir;
                }
                else
                {
                    // baseDirectoryPath のフルパス名よりもdirectoryPath のフルパス名の方が長くはない場合

                    var dir = baseDirectoryPath.Parent;
                    if (dir is not null)
                        relativePathElements1.Add("..");
                    baseDirectoryPath = dir;
                }
            }

            return null;
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

        [GeneratedRegex(@"^(?<driveLetter>[a-zA-Z]):\\$", RegexOptions.Compiled | RegexOptions.ExplicitCapture)]
        private static partial Regex GetDosRootPathPattern();

        [GeneratedRegex(@"^\\\\(?<serverName>[^\\]+)\\(?<sharedResourceName>[^\\]+)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture)]
        private static partial Regex GetUncRootPathPattern();

        [GeneratedRegex(@"^\\\\(?<prefix>[\.\?])\\(((?<driveLetter>[a-z]):)|(Volume(?<guid>{[\da-f]+-[\da-f]+-[\da-f]+-[\da-f]+-[\da-f]+}))|(?<bootPartition>BootPartition))\\$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.ExplicitCapture)]
        private static partial Regex GetDosDeviceRootPathPattern();

        [GeneratedRegex(@"^\\\\(?<prefix>[\.\?])\\UNC\\(?<serverName>[^\\]+)\\(?<sharedResourceName>[^\\]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.ExplicitCapture)]
        private static partial Regex GetUncLinkPattern();
    }
}
