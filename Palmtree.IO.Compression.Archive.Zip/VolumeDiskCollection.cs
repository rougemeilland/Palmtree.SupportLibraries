using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace Palmtree.IO.Compression.Archive.Zip
{
    /// <summary>
    /// シングルボリューム/マルチボリューム ZIP アーカイブのボリュームディスク情報を保持するクラスです。
    /// </summary>
    internal class VolumeDiskCollection
        : IDisposable
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct VolumeInfo
        {
            public static readonly Int32 Size;

            static VolumeInfo()
            {
                unsafe
                {
                    Size = sizeof(VolumeInfo);
                }
            }

            public UInt64 TotalOffset;
            public UInt64 VolumeDiskSize;
        }

        private interface IVirtualArray<ELEMENT_T>
            : IDisposable
        {
            Boolean OnMemory { get; }
            ELEMENT_T this[UInt32 index] { get; }
            UInt32 Length { get; }
            void Add(ELEMENT_T element);
            ELEMENT_T[] GetRawArray();
        }

        private class ArrayOfVolumeDisksOnMemory
            : IVirtualArray<VolumeInfo>
        {
            private readonly List<VolumeInfo> _volumeDisks;

            private Boolean _isDisposed;

            public ArrayOfVolumeDisksOnMemory()
            {
                _volumeDisks = new List<VolumeInfo>();
                _isDisposed = false;

            }

            public Boolean OnMemory => true;
            public VolumeInfo this[UInt32 index]
            {
                get
                {
                    if (_isDisposed)
                        throw new ObjectDisposedException(GetType().FullName);

                    return _volumeDisks[checked((Int32)index)];
                }
            }

            public UInt32 Length
            {
                get
                {
                    if (_isDisposed)
                        throw new ObjectDisposedException(GetType().FullName);

                    return checked((UInt32)_volumeDisks.Count);
                }
            }

            public void Add(VolumeInfo element)
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().FullName);

                _volumeDisks.Add(element);
            }

            public VolumeInfo[] GetRawArray()
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().FullName);

                return _volumeDisks.ToArray();
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(Boolean disposing)
            {
                if (!_isDisposed)
                {
                    if (disposing)
                    {
                    }

                    _isDisposed = true;
                }
            }
        }

        private class ArrayOfVolumeDisksOnSharedMemory
            : IVirtualArray<VolumeInfo>
        {
            private readonly FilePath _baseFile;
            private readonly System.IO.Stream _baseFileStream;

            private Boolean _isDisposed;
            private UInt32 _length;

            public ArrayOfVolumeDisksOnSharedMemory(VolumeInfo[] sourceArray)
            {
                _baseFile = new FilePath(Path.GetTempFileName());
                _baseFileStream = new FileStream(_baseFile.FullName, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                _isDisposed = false;
                _length = checked((UInt32)sourceArray.Length);
                unsafe
                {
                    fixed (VolumeInfo* p = sourceArray)
                    {
                        Validation.Assert(VolumeInfo.Size == 16, "VolumeInfo.Size == 16");
                        Validation.Assert(sizeof(VolumeInfo) == 16, "sizeof(VolumeInfo) == 16");
                        _baseFileStream.Write(new ReadOnlySpan<Byte>((Byte*)p, sourceArray.Length * sizeof(VolumeInfo)));
                    }
                }
            }

            ~ArrayOfVolumeDisksOnSharedMemory()
            {
                Dispose(disposing: false);
            }

            public Boolean OnMemory => false;

            public VolumeInfo this[UInt32 index]
            {
                get
                {
                    if (_isDisposed)
                        throw new ObjectDisposedException(GetType().FullName);
                    if (index >= _length)
                        throw new ArgumentOutOfRangeException(nameof(index));

#if DEBUG
                    Validation.Assert(checked(_length * VolumeInfo.Size) == _baseFileStream.Length, "checked(_length * VolumeInfo.Size) == _baseFileStream.Length");
#endif
                    _ = _baseFileStream.Seek(checked(index * VolumeInfo.Size), SeekOrigin.Begin);
                    VolumeInfo value;
                    unsafe
                    {
                        var length = _baseFileStream.ReadBytes(new Span<Byte>((Byte*)&value, sizeof(VolumeInfo)));
                        if (length < sizeof(VolumeInfo))
                            throw new EndOfStreamException();
                    }

                    return value;
                }
            }

            public UInt32 Length
            {
                get
                {
                    if (_isDisposed)
                        throw new ObjectDisposedException(GetType().FullName);

                    return _length;
                }
            }

            public void Add(VolumeInfo value)
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().FullName);
                if (_length >= UInt32.MaxValue)
                    throw new OutOfMemoryException();

#if DEBUG
                Validation.Assert(checked(_length * VolumeInfo.Size) == _baseFileStream.Length, "checked(_length * VolumeInfo.Size) == _baseFileStream.Length");
#endif
                _ = _baseFileStream.Seek(checked(_length * VolumeInfo.Size), SeekOrigin.Begin);
                unsafe
                {
                    _baseFileStream.Write(new ReadOnlySpan<Byte>((Byte*)&value, sizeof(VolumeInfo)));
                }

                checked
                {
                    ++_length;
                }
            }

            public VolumeInfo[] GetRawArray()
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(GetType().FullName);

                throw new NotSupportedException();
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(Boolean disposing)
            {
                if (!_isDisposed)
                {
                    if (disposing)
                    {
                        _baseFileStream.Dispose();
                    }

                    try
                    {
                        if (_baseFile.Exists)
                            _baseFile.Delete();
                    }
                    catch (Exception)
                    {
                    }

                    _isDisposed = true;
                }
            }

            private static MemoryMappedViewAccessor CreateAccesser(MemoryMappedFile memoryMappedFile, Int64 offset, Int64 count)
                => memoryMappedFile.CreateViewAccessor(offset, count, MemoryMappedFileAccess.ReadWrite);
        }

        private const UInt32 _MAX_MEMORY_ARRAY_LENGTH = 1024 * 1024 / 16;

        private readonly IVirtualArray<VolumeInfo> _volumeDisks;

        private Boolean _isDisposed;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="volumeDiskSizes">
        /// ボリュームディスクのサイズ (バイト数) を示す <see cref="UInt64"/> 値 の列挙子です。
        /// </param>
        public VolumeDiskCollection(IEnumerable<UInt64> volumeDiskSizes)
        {
            _volumeDisks = new ArrayOfVolumeDisksOnMemory();
            _isDisposed = false;
            try
            {
                var totalOffset = 0UL;
                var lastVolumeDiskSize = 0UL;
                foreach (var volumeDiskSize in volumeDiskSizes)
                {
                    if (_volumeDisks.OnMemory && _volumeDisks.Length >= _MAX_MEMORY_ARRAY_LENGTH)
                    {
                        var newVolmeDisk = new ArrayOfVolumeDisksOnSharedMemory(_volumeDisks.GetRawArray());
                        _volumeDisks.Dispose();
                        _volumeDisks = newVolmeDisk;
                    }

                    _volumeDisks.Add(new VolumeInfo { TotalOffset = totalOffset, VolumeDiskSize = volumeDiskSize });
                    lastVolumeDiskSize = volumeDiskSize;
                    checked
                    {
                        totalOffset += volumeDiskSize;
                    }
                }

                if (_volumeDisks.Length <= 0)
                    throw new ArgumentException($"{nameof(volumeDiskSizes)} is an empty sequence.");

                TotalVolumeDiskSize = totalOffset;
                LastVolumeDiskNumber = _volumeDisks.Length - 1;
                LastVolumeDiskSize = lastVolumeDiskSize;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Duplicate disk number.", nameof(volumeDiskSizes), ex);
            }
        }

        /// <summary>
        /// 最後のボリュームディスクのディスク番号を取得します。
        /// </summary>
        /// <value>
        /// 最後のボリュームディスクのディスク番号を示す <see cref="UInt32"/> 値です。
        /// </value>
        public UInt32 LastVolumeDiskNumber { get; }

        /// <summary>
        /// 最後のボリュームディスクのサイズを取得します。
        /// </summary>
        /// <value>
        /// 最後のボリュームディスクのサイズ (バイト数) を示す <see cref="UInt64"/> 値です。
        /// </value>
        public UInt64 LastVolumeDiskSize { get; }

        /// <summary>
        /// 全ボリュームディスクの合計サイズを取得します。
        /// </summary>
        /// <value>
        /// 全ボリュームディスクの合計サイズ (バイト数) を示す <see cref="UInt64"/> 値です。
        /// </value>
        public UInt64 TotalVolumeDiskSize { get; }

        /// <summary>
        /// 最初のボリュームの先頭からのオフセットから、それと同じ位置を示すディスク番号とそのディスクの先頭からのオフセットを求めます。
        /// </summary>
        /// <param name="offsetFromStart">
        /// 最初のボリュームの先頭からのオフセット (バイト数) を示す <see cref="UInt64"/> 値です。
        /// </param>
        /// <param name="diskNumber">
        /// <paramref name="offsetFromStart"/> で示された位置のディスク番号を示す <see cref="UInt32"/> 値です。
        /// </param>
        /// <param name="offsetOnTheDisk">
        /// <paramref name="diskNumber"/> で示された位置の、ボリュームディスク <paramref name="diskNumber"/> の先頭からのオフセットです。
        /// </param>
        /// <returns>
        /// <paramref name="diskNumber"/> および <paramref name="offsetOnTheDisk"/> を求めることが出来た場合は true、そうではない場合は false が返ります。
        /// </returns>
        public Boolean TryGetVolumeDiskPosition(UInt64 offsetFromStart, out UInt32 diskNumber, out UInt64 offsetOnTheDisk)
        {
            if (offsetFromStart > TotalVolumeDiskSize)
            {
                diskNumber = 0;
                offsetOnTheDisk = 0;
                return false;
            }

            GetVolumeDiskPosition(offsetFromStart, 0, LastVolumeDiskNumber, out diskNumber, out var totalOffset);
            offsetOnTheDisk = checked(offsetFromStart - totalOffset);
            return true;
        }

        /// <summary>
        /// ボリュームディスクの詳細情報を取得します。
        /// </summary>
        /// <param name="diskNumber">
        /// ボリュームディスクの番号を示す <see cref="UInt32"/> です。
        /// </param>
        /// <param name="volumeDiskSize">
        /// <paramref name="diskNumber"/> で示されるボリュームディスクのサイズ (バイト数) を示す <see cref="UInt64"/> オブジェクトです。
        /// </param>
        /// 詳細情報の取得に成功した場合は true、そうではない場合は false が返ります。
        /// <returns>
        /// </returns>
        public Boolean TryGetVolumeDiskSize(UInt32 diskNumber, out UInt64 volumeDiskSize)
        {
            if (diskNumber >= _volumeDisks.Length)
            {
                volumeDiskSize = 0;
                return false;
            }

            volumeDiskSize = _volumeDisks[diskNumber].VolumeDiskSize;
            return true;
        }

        /// <summary>
        /// ボリュームディスクの番号とそのディスクの先頭からのオフセットから、それと同じ位置を示す最初のボリュームの先頭からのオフセットを求めます。
        /// </summary>
        /// <param name="diskNumber">
        /// ボリュームディスクの番号を示す <see cref="UInt32"/> 値です。
        /// </param>
        /// <param name="offsetOnTheDisk">
        /// <paramref name="diskNumber"/> で示されるボリュームディスクの先頭からのオフセット (バイト数) を示す <see cref="UInt64"/> 値です。
        /// </param>
        /// <param name="offsetFromStart">
        /// <paramref name="diskNumber"/> および <paramref name="offsetOnTheDisk"/> で示されるボリュームディスク上の位置と同じ位置を示す、最初のボリュームの先頭からのオフセット (バイト数) です。
        /// </param>
        /// <returns>
        /// <paramref name="offsetFromStart"/> を求めるのに成功した場合は true、そうではない場合は false が返ります。
        /// </returns>
        public Boolean TryGetOffsetFromStart(UInt32 diskNumber, UInt64 offsetOnTheDisk, out UInt64 offsetFromStart)
        {
            if (diskNumber >= _volumeDisks.Length)
            {
                offsetFromStart = 0;
                return false;
            }

            var element = _volumeDisks[diskNumber];
            if (offsetOnTheDisk > element.VolumeDiskSize)
            {
                offsetFromStart = 0;
                return false;
            }

            offsetFromStart = checked(element.TotalOffset + offsetOnTheDisk);
            return true;
        }

        /// <summary>
        /// 全ボリュームディスクの情報を列挙します。
        /// </summary>
        /// <returns>
        /// 以下のタプルの列挙子を返します。
        /// <list type="bullet">
        /// <item>ボリュームディスクのディスク番号を示す <see cref="UInt32"/> 値</item>
        /// <item>ボリュームディスクのサイズ (バイト数) を示す <see cref="UInt64"/> 値</item>
        /// </list>
        /// </returns>
        public IEnumerable<(UInt32 diskNumber, UInt64 volumeDiskSize)> EnumerateVolumeDisks()
        {
            for (var diskNumber = 0U; diskNumber <= LastVolumeDiskNumber; ++diskNumber)
            {
                var element = _volumeDisks[diskNumber];
                yield return (diskNumber, element.VolumeDiskSize);
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _volumeDisks.Dispose();
                }

                _isDisposed = true;
            }
        }

        private void GetVolumeDiskPosition(UInt64 offsetFromStart, UInt32 startOfDiskNumber, UInt32 endOfDiskNumber, out UInt32 diskNumber, out UInt64 totalOffset)
        {
            Validation.Assert(startOfDiskNumber <= endOfDiskNumber, "startOfDiskNumber <= endOfDiskNumber");
            if (startOfDiskNumber == endOfDiskNumber)
            {
                var element = _volumeDisks[startOfDiskNumber];
                diskNumber = startOfDiskNumber;
                totalOffset = element.TotalOffset;
            }
            else
            {
                var halfLength = (endOfDiskNumber - startOfDiskNumber + 1) / 2;
                var middleDiskNumber = startOfDiskNumber + halfLength;
                var middleElement = _volumeDisks[middleDiskNumber];
                if (offsetFromStart < middleElement.TotalOffset)
                    GetVolumeDiskPosition(offsetFromStart, startOfDiskNumber, middleDiskNumber - 1, out diskNumber, out totalOffset);
                else
                    GetVolumeDiskPosition(offsetFromStart, middleDiskNumber, endOfDiskNumber, out diskNumber, out totalOffset);
            }
        }
    }
}
