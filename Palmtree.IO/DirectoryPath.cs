using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Palmtree.IO
{
    public partial class DirectoryPath
        : FileSystemPath
    {
        private const String _SERVER_NAME_LOCAL_HOST = "localhost";

        static DirectoryPath()
        {
            var homeDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            UserHomeDirectory = String.IsNullOrEmpty(homeDirectoryPath) ? null : new DirectoryPath(homeDirectoryPath);
        }

        public DirectoryPath(String path)
            : this(GetDirectoryInfo(path))
        {
        }

        private DirectoryPath(DirectoryInfo directory)
            : base(directory)
        {
            var parent = directory.Parent;
            if (parent is null)
            {
                Parent = null;
                Root = this;
            }
            else
            {
                Parent = new DirectoryPath(parent);
                Root = new DirectoryPath(directory.Root);
            }
        }

        public static DirectoryPath CurrentDirectory => new(Environment.CurrentDirectory);
        public override Boolean Exists => Directory.Exists(FullName);
        public DirectoryPath? Parent { get; }
        public DirectoryPath Root { get; }
        public static DirectoryPath? UserHomeDirectory { get; }

        public DirectoryPath Create()
        {
            if (!Directory.Exists(FullName))
                _ = Directory.CreateDirectory(FullName);
            return this;
        }

        public static DirectoryPath CreateTemporaryDirectory() => new(Directory.CreateTempSubdirectory());

        public void Delete(Boolean recursive = false) => Directory.Delete(FullName, recursive);

        public IEnumerable<DirectoryPath> EnumerateDirectories(Boolean recursive = false) => EnumerateDirectories("*", recursive);

        public IEnumerable<DirectoryPath> EnumerateDirectories(String namePattern, Boolean recursive = false)
        {
            ArgumentException.ThrowIfNullOrEmpty(namePattern);

            var directoryPathNames =
                Directory.EnumerateDirectories(
                    FullName,
                    namePattern,
                    recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            foreach (var directoryPathName in directoryPathNames)
                yield return new DirectoryPath(directoryPathName);
        }

        public IEnumerable<FilePath> EnumerateFiles(Boolean recursive = false) => EnumerateFiles("*", recursive);

        public IEnumerable<FilePath> EnumerateFiles(String namePattern, Boolean recursive = false)
        {
            ArgumentException.ThrowIfNullOrEmpty(namePattern);

            var filePathNames =
                    Directory.EnumerateFiles(
                        FullName,
                        namePattern,
                        recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            foreach (var filePathName in filePathNames)
                yield return new FilePath(filePathName);
        }

        public FilePath GetFile(String fileName)
        {
            ArgumentException.ThrowIfNullOrEmpty(fileName);

            return new FilePath(Path.Combine(FullName, fileName));
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
                Validation.Assert(driveLetter.Success || guid.Success || bootPartition.Success);
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

        public DirectoryPath GetSubDirectory(String subDirectoryName)
        {
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName);

            return new DirectoryPath(Path.Combine(FullName, subDirectoryName));
        }

        public DirectoryPath GetSubDirectory(String subDirectoryName1, String subDirectoryName2)
        {
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName1);
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName2);

            return new DirectoryPath(Path.Combine(FullName, subDirectoryName1, subDirectoryName2));
        }

        public DirectoryPath GetSubDirectory(String subDirectoryName1, String subDirectoryName2, String subDirectoryName3)
        {
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName1);
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName2);
            ArgumentException.ThrowIfNullOrEmpty(subDirectoryName3);
            return new DirectoryPath(Path.Combine(FullName, subDirectoryName1, subDirectoryName2, subDirectoryName3));
        }

        public DirectoryPath GetSubDirectory(params String[] subDirectoryNames)
        {
            ArgumentNullException.ThrowIfNull(subDirectoryNames);

            var pathElements = new String[subDirectoryNames.Length + 1];
            pathElements[0] = FullName;
            for (var index = 0; index < subDirectoryNames.Length; ++index)
            {
                ArgumentException.ThrowIfNullOrEmpty(subDirectoryNames[index], $"subDirectoryNames[{index}]");
                pathElements[index + 1] = subDirectoryNames[index];
            }

            return new DirectoryPath(Path.Combine(pathElements));
        }

        public void MoveTo(DirectoryPath destinationDirectory)
        {
            ArgumentNullException.ThrowIfNull(destinationDirectory);

            Directory.Move(FullName, destinationDirectory.FullName);
        }

        public static implicit operator DirectoryInfo(DirectoryPath path)
        {
            ArgumentNullException.ThrowIfNull(path);

            return new(path.FullName);
        }

        public static implicit operator DirectoryPath(DirectoryInfo directory)
        {
            ArgumentNullException.ThrowIfNull(directory);

            return new(new DirectoryInfo(directory.FullName));
        }

        protected override DateTime InternalCreationTimeUtc
        {
            get => Directory.GetCreationTimeUtc(FullName);
            set => Directory.SetCreationTimeUtc(FullName, value);
        }

        protected override DateTime InternalLastAccessTimeUtc
        {
            get => Directory.GetLastAccessTimeUtc(FullName);
            set => Directory.SetLastAccessTimeUtc(FullName, value);
        }

        protected override DateTime InternalLastWriteTimeUtc
        {
            get => Directory.GetLastWriteTimeUtc(FullName);
            set => Directory.SetLastWriteTime(FullName, value);
        }

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
                throw new ArgumentException($"A string that cannot be used as a directory path name is specified. : {nameof(path)}=\"{path}\"", nameof(path), ex);
            }
        }

        [GeneratedRegex(@"^(?<driveLetter>[a-zA-Z]):\\$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
        private static partial Regex GetDosRootPathPattern();

        [GeneratedRegex(@"^\\\\(?<serverName>[^\\]+)\\(?<sharedResourceName>[^\\]+)$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
        private static partial Regex GetUncRootPathPattern();

        [GeneratedRegex(@"^\\\\(?<prefix>[\.\?])\\(((?<driveLetter>[a-z]):)|(Volume(?<guid>{[\da-f]+-[\da-f]+-[\da-f]+-[\da-f]+-[\da-f]+}))|(?<bootPartition>BootPartition))\\$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
        private static partial Regex GetDosDeviceRootPathPattern();

        [GeneratedRegex(@"^\\\\(?<prefix>[\.\?])\\UNC\\(?<serverName>[^\\]+)\\(?<sharedResourceName>[^\\]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
        private static partial Regex GetUncLinkPattern();
    }
}
