﻿using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Palmtree.Threading;

namespace Palmtree.IO
{
    public static class TrashBox
    {
        private const String trashBoxEnvironmentVariableName = "TRASH_BOX_PATH";

        private class WindowsTrashBox
            : ITrashBox
        {
            private WindowsTrashBox()
            {
            }

            public static ITrashBox? Open()
                => OperatingSystem.IsWindows()
                    ? new WindowsTrashBox()
                    : null as ITrashBox;

            Boolean ITrashBox.DisposeFile(FilePath file)
            {
                try
                {
                    FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            async Task<Boolean> ITrashBox.DisposeFileAsync(FilePath file)
            {
                try
                {
                    await Task.Run(() => FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin)).ConfigureAwait(false);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        private class GenericTrashBox
            : ITrashBox
        {
            private static readonly Guid _thisClassId;
            private readonly DirectoryPath _trashBoxDirectory;
            private readonly String _lockObjectName;

            static GenericTrashBox()
            {
                _thisClassId = new Guid("B70EB9E2-150A-4FC9-A274-07658AEA0C16");
            }

            private GenericTrashBox(DirectoryPath trashBoxDirectory)
            {
                _trashBoxDirectory = new DirectoryPath(trashBoxDirectory.FullName);
                var hashValue = MD5.HashData(Encoding.UTF8.GetBytes(_trashBoxDirectory.FullName.ToUpperInvariant()));
                _lockObjectName = $"{_thisClassId}-{String.Concat(hashValue.Select(byteValue => byteValue.ToString("x2")))}";
            }

            public static ITrashBox? Open(String environmentVariableName)
            {
                var trashBoxDirector = TryGetTrashBoxDirectory(environmentVariableName);
                if (trashBoxDirector is null)
                    return null;

                return new GenericTrashBox(trashBoxDirector);
            }

            Boolean ITrashBox.DisposeFile(FilePath file)
            {
                try
                {
                    var count = 0;
                    using var semaphore = new Semaphore(1, 1, _lockObjectName, out var createdNew);
                    for (count = 0; ; ++count)
                    {
                        var destinationFile = _trashBoxDirectory.GetFile($"{file.Name}.{count}");

                        using var lockObject = semaphore.Lock();
                        if (!destinationFile.Exists)
                        {
                            file.MoveTo(destinationFile);
                            return true;
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            async Task<Boolean> ITrashBox.DisposeFileAsync(FilePath sourceFile)
            {
                try
                {
                    var count = 0;
                    using var semaphore = new Semaphore(0, 1, _lockObjectName);
                    for (count = 0; ; ++count)
                    {
                        var destinationFile = _trashBoxDirectory.GetFile($"{sourceFile.FullName}.{count}");

                        using var lockObject = await semaphore.LockAsync().ConfigureAwait(false);
                        if (!destinationFile.Exists)
                        {
                            await Task.Run(() => sourceFile.MoveTo(destinationFile, false)).ConfigureAwait(false);
                            return true;
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            private static DirectoryPath? TryGetTrashBoxDirectory(String environmentVariableName)
            {
                var trashBoxPath = Environment.GetEnvironmentVariable(environmentVariableName);
                if (trashBoxPath is null)
                    return null;
                try
                {
                    var trashBoxDirectory = new DirectoryPath(trashBoxPath);
                    if (!trashBoxDirectory.Exists)
                        trashBoxDirectory.Create().Refresh();

                    var temporaryFile = trashBoxDirectory.GetFile($".temporary.{Guid.NewGuid()}");
                    try
                    {
                        temporaryFile.WriteAllText("temporary");
                        _ = temporaryFile.ReadAllLines();

                        trashBoxDirectory.Refresh();
                        return trashBoxDirectory;
                    }
                    finally
                    {
                        temporaryFile.SafetyDelete();
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static ITrashBox OpenTrashBox()
            => GenericTrashBox.Open(trashBoxEnvironmentVariableName)
                ?? WindowsTrashBox.Open()
                ?? throw new IOException("ごみ箱を開けません。");
    }
}
