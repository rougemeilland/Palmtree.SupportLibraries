﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Palmtree.Collections;
using Palmtree.IO.Compression.Archive.Zip.Headers.Builder;

namespace Palmtree.IO.Compression.Archive.Zip
{
    internal class GenericStyleZipOutputStream
          : ZipOutputStream
    {
        private static readonly UInt64 _minimumAtomicDataSize;

        private readonly FilePath _baseZipArchiveFile;
        private readonly DirectoryPath _baseDirectory;
        private readonly UInt64 _maximumVolumeSize;
        private readonly BigList<(FilePath volumeDiskFile, UInt64 volumeDiskSize)> _volumeDisks;
        private Boolean _isDisposed;
        private IRandomOutputByteStream<UInt64> _currentBaseStream;
        private Boolean _isLocked;

        static GenericStyleZipOutputStream()
        {
            var maximumHeaderSize =
                ZipFileEOCDR.MinimumHeaderSize
                .Maximum(ZipFileZip64EOCDL.FixedHeaderSize)
                .Maximum(ZipFileZip64EOCDR_Ver1.FixedHeaderSize)
                .Maximum(ZipEntryCentralDirectoryHeader.MinimumHeaderSize)
                .Maximum(ZipEntryLocalHeader.MinimumHeaderSize)
                .Maximum(ZipEntryDataDescriptor.FixedHeaderSize)
                .Maximum(ZipEntryDataDescriptor.FixedHeaderSizeForZip64);
            _minimumAtomicDataSize = checked((UInt64)maximumHeaderSize);
        }

        private GenericStyleZipOutputStream(FilePath zipArchiveFile, DirectoryPath baseDirectory, UInt64 maximumVolumeSize, IRandomOutputByteStream<UInt64> firstVolumeStream)
        {
            _baseZipArchiveFile = zipArchiveFile;
            _baseDirectory = baseDirectory;
            _maximumVolumeSize = maximumVolumeSize;
            _volumeDisks = new BigList<(FilePath volumeDiskFile, UInt64 volumeDiskSize)>();
            _isDisposed = false;
            _currentBaseStream = firstVolumeStream.WithCache();
            _isLocked = false;
        }

        public override String ToString() => $"GenericStyle:BasePath=\"{_baseZipArchiveFile.FullName}\"";

        public static IZipOutputStream CreateInstance(FilePath zipArchiveFile, UInt64 maximumVolumeSize)
        {
            if (zipArchiveFile is null)
                throw new ArgumentNullException(nameof(zipArchiveFile));
            if (maximumVolumeSize < _minimumAtomicDataSize)
                throw new ArgumentOutOfRangeException($"Maximum volume size is too short. To create a ZIP archive, the maximum volume size must be at least {maximumVolumeSize:N0} bytes.");

            var baseDirectory =
                zipArchiveFile.Directory
                ?? throw new ArgumentException($"Unable to get the directory path name of the zip file archive file.: {nameof(zipArchiveFile)}=\"{zipArchiveFile.FullName}\"", nameof(zipArchiveFile));
            var success = false;
            var stream = zipArchiveFile.Create();
            try
            {
                var instance =
                    new GenericStyleZipOutputStream(
                        zipArchiveFile,
                        baseDirectory,
                        maximumVolumeSize,
                        stream);
                var volumeDiskFile = GetVolumeFilePath(zipArchiveFile, baseDirectory, 0);
                if (volumeDiskFile.Exists)
                {
                    try
                    {
                        volumeDiskFile.Delete();
                    }
                    catch (Exception)
                    {
                    }
                }

                success = true;
                return instance;

            }
            finally
            {
                if (!success)
                    stream.Dispose();
            }
        }

        protected override ZipStreamPosition PositionCore => new(_volumeDisks.Count, _currentBaseStream.Position, this);

        protected override UInt64 MaximumDiskSizeCore => _maximumVolumeSize;

        protected override void ReserveAtomicSpaceCore(UInt64 atomicSpaceSize)
        {
            if (atomicSpaceSize > _maximumVolumeSize)
                throw new InvalidOperationException($"Maximum volume size is too short. To create this ZIP archive, the maximum volume size must be at least {atomicSpaceSize:N0} bytes.");

            if ((_currentBaseStream.Position + atomicSpaceSize) > _maximumVolumeSize)
                MoveToNextVolumeDisk();
        }

        protected override void LockVolumeDiskCore()
        {
            Validation.Assert(!_isLocked, "!_isLocked");
            _isLocked = true;
        }

        protected override void UnlockVolumeDiskCore()
        {
            Validation.Assert(_isLocked, "_isLocked");
            _isLocked = false;
        }

        protected override IRandomOutputByteStream<UInt64> GetCurrentStreamCore()
        {
            if (_currentBaseStream.Position >= _maximumVolumeSize)
                MoveToNextVolumeDisk();
            return _currentBaseStream;
        }

        protected override void CleanUpCore()
        {
            try
            {
                if (_baseZipArchiveFile.Exists)
                    _baseZipArchiveFile.Delete();
            }
            catch (Exception)
            {
            }

            for (var index = 0U; index < _volumeDisks.Count; ++index)
            {
                var volumeDiskFile = _volumeDisks[index].volumeDiskFile;
                try
                {
                    if (volumeDiskFile.Exists)
                        volumeDiskFile.Delete();
                }
                catch (Exception)
                {
                }
            }
        }

        protected override ZipStreamPosition AddCore(UInt32 diskNumber, UInt64 offsetOnTheDisk, UInt64 offset)
        {
            Validation.Assert(diskNumber <= _volumeDisks.Count && (diskNumber < _volumeDisks.Count || offsetOnTheDisk <= _currentBaseStream.Length), "diskNumber <= _volumeDisks.Count && (diskNumber < _volumeDisks.Count || offsetOnTheDisk <= _currentBaseStream.Length)");
            checked
            {
                offsetOnTheDisk += offset;
            }

            while (diskNumber <= _volumeDisks.Count)
            {
                var currentVolumeDiskSize = GetVolumeDiskSize(diskNumber);
                if (offsetOnTheDisk <= currentVolumeDiskSize)
                    return new ZipStreamPosition(diskNumber, offsetOnTheDisk, this);
                checked
                {
                    offsetOnTheDisk -= currentVolumeDiskSize;
                }

                ++diskNumber;
            }

            throw new OverflowException();
        }

        protected override ZipStreamPosition SubtractCore(UInt32 diskNumber, UInt64 offsetOnTheDisk, UInt64 offset)
        {
            Validation.Assert(diskNumber <= _volumeDisks.Count && (diskNumber < _volumeDisks.Count || offsetOnTheDisk <= _currentBaseStream.Length), "diskNumber <= _volumeDisks.Count && (diskNumber < _volumeDisks.Count || offsetOnTheDisk <= _currentBaseStream.Length)");
            while (true)
            {
                if (offsetOnTheDisk >= offset)
                    return new ZipStreamPosition(diskNumber, checked(offsetOnTheDisk - offset), this);
                if (diskNumber <= 0)
                    break;
                checked
                {
                    offsetOnTheDisk += GetVolumeDiskSize(diskNumber);
                }

                --diskNumber;
            }

            throw new OverflowException();
        }

        protected override UInt64 SubtractCore(UInt32 diskNumber1, UInt64 offsetOnTheDisk1, UInt32 diskNumber2, UInt64 offsetOnTheDisk2)
        {
            Validation.Assert(diskNumber1 <= _volumeDisks.Count && (diskNumber1 < _volumeDisks.Count || offsetOnTheDisk1 <= _currentBaseStream.Length), "diskNumber1 <= _volumeDisks.Count && (diskNumber1 < _volumeDisks.Count || offsetOnTheDisk1 <= _currentBaseStream.Length)");
            Validation.Assert(diskNumber2 <= _volumeDisks.Count && (diskNumber2 < _volumeDisks.Count || offsetOnTheDisk2 <= _currentBaseStream.Length), "diskNumber2 <= _volumeDisks.Count && (diskNumber2 < _volumeDisks.Count || offsetOnTheDisk2 <= _currentBaseStream.Length)");
            var minimumDiskNumber = diskNumber1.Minimum(diskNumber2);
            while (diskNumber1 > minimumDiskNumber)
            {
                checked
                {
                    --diskNumber1;
                    offsetOnTheDisk1 += _volumeDisks[diskNumber1].volumeDiskSize;
                }
            }

            while (diskNumber2 > minimumDiskNumber)
            {
                checked
                {
                    --diskNumber2;
                    offsetOnTheDisk2 += _volumeDisks[diskNumber2].volumeDiskSize;
                }
            }

            return checked(offsetOnTheDisk1 - offsetOnTheDisk2);
        }

        protected override (UInt32 diskNumber, UInt64 offsetOnTheDisk) NormalizeCore(UInt32 diskNumber, UInt64 offsetOnTheDisk)
        {
            if (diskNumber < _volumeDisks.Count)
            {
                var volumeSize = _volumeDisks[diskNumber].volumeDiskSize;
                Validation.Assert(offsetOnTheDisk <= volumeSize, "offsetOnTheDisk <= volumeSize");
                return
                    offsetOnTheDisk == volumeSize
                    ? (checked(diskNumber + 1), 0)
                    : (diskNumber, offsetOnTheDisk);
            }
            else
            {
                Validation.Assert(diskNumber == _volumeDisks.Count, "diskNumber == _volumeDisks.Count");
                Validation.Assert(offsetOnTheDisk <= _currentBaseStream.Length, "offsetOnTheDisk <= _currentBaseStream.Length");
                return (diskNumber, offsetOnTheDisk);
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                    _currentBaseStream.Dispose();
                _isDisposed = true;
            }

            base.Dispose(disposing);
        }

        protected override async Task DisposeAsyncCore()
        {
            if (!_isDisposed)
            {
                await _currentBaseStream.DisposeAsync().ConfigureAwait(false);
                _isDisposed = true;
            }

            await base.DisposeAsyncCore().ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void MoveToNextVolumeDisk()
        {
            ThrowExceptionIfLocked();
            var currentVolumeSize = _currentBaseStream.Length;
            _currentBaseStream.Flush();
            _currentBaseStream.Dispose();
            var newVolumeFile = GetVolumeFilePath(_volumeDisks.Count);
            _baseZipArchiveFile.MoveTo(newVolumeFile);
            _volumeDisks.Add((newVolumeFile, currentVolumeSize));
            try
            {
                var nextVolumeFile = GetVolumeFilePath(_volumeDisks.Count);
                if (nextVolumeFile.Exists)
                    nextVolumeFile.Delete();
            }
            catch (Exception)
            {
            }

            _currentBaseStream = _baseZipArchiveFile.Create().WithCache();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ThrowExceptionIfLocked()
        {
            if (_isLocked)
                throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private FilePath GetVolumeFilePath(UInt32 diskNumber)
            => GetVolumeFilePath(_baseZipArchiveFile, _baseDirectory, diskNumber);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static FilePath GetVolumeFilePath(FilePath baseZipArchiveFile, DirectoryPath baseDirectory, UInt32 diskNumber)
            => baseDirectory.GetFile($"{Path.GetFileNameWithoutExtension(baseZipArchiveFile.Name)}.z{diskNumber + 1:d2}");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private UInt64 GetVolumeDiskSize(UInt32 diskNumber)
        {
            Validation.Assert(diskNumber <= _volumeDisks.Count, "diskNumber <= _volumeDisks.Count");
            return
                diskNumber < _volumeDisks.Count
                ? _volumeDisks[diskNumber].volumeDiskSize
                : _currentBaseStream.Length;
        }
    }
}
