using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO.Compression.Archive.Zip
{
    internal abstract class ZipOutputStream
        : SequentialOutputByteStream, IZipOutputStream, IVirtualZipFile
    {
        private readonly Guid _instanceId;
        private Boolean _isDisposed;
        private Boolean _isCompletedSuccessfully;

        protected ZipOutputStream()
        {
            _instanceId = Guid.NewGuid();
            _isDisposed = false;
            _isCompletedSuccessfully = false;
        }

        public ZipStreamPosition Position
        {
            get
            {
                ObjectDisposedException.ThrowIf(_isDisposed, this);

                return PositionCore;
            }
        }

        public void ReserveAtomicSpace(UInt64 atomicSpaceSize)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            ReserveAtomicSpaceCore(atomicSpaceSize);
        }

        public void LockVolumeDisk()
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            LockVolumeDiskCore();
        }

        public void UnlockVolumeDisk()
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            UnlockVolumeDiskCore();
        }

        public void CompletedSuccessfully()
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);

            _isCompletedSuccessfully = true;
        }

        public ZipStreamPosition Add(ZipStreamPosition position, UInt64 offset)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            Validation.Assert(Equals(position.Host));
            try
            {
                return AddCore(position.DiskNumber, position.OffsetOnTheDisk, offset);
            }
            catch (OverflowException ex)
            {
                throw new OverflowException($"Overflow occurred while calculating \"{position}\" + 0x{offset:x16}.", ex);
            }
        }

        public ZipStreamPosition Subtract(ZipStreamPosition position, UInt64 offset)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            Validation.Assert(Equals(position.Host));
            try
            {
                return SubtractCore(position.DiskNumber, position.OffsetOnTheDisk, offset);
            }
            catch (OverflowException ex)
            {
                throw new OverflowException($"Overflow occurred while calculating \"{position}\" - 0x{offset:x16}.", ex);
            }
        }

        public UInt64 Subtract(ZipStreamPosition position1, ZipStreamPosition position2)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            Validation.Assert(Equals(position1.Host));
            Validation.Assert(Equals(position2.Host));
            try
            {
                return SubtractCore(position1.DiskNumber, position1.OffsetOnTheDisk, position2.DiskNumber, position2.OffsetOnTheDisk);
            }
            catch (OverflowException ex)
            {
                throw new OverflowException($"Overflow occurred while calculating \"{position1}\" - \"{position2}\".", ex);
            }
        }

        public Int32 Compare(ZipStreamPosition position1, ZipStreamPosition position2)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            Validation.Assert(Equals(position1.Host));
            Validation.Assert(Equals(position2.Host));
            var (diskNumber1, offsetOnTheDisk1) = NormalizeCore(position1.DiskNumber, position1.OffsetOnTheDisk);
            var (diskNumber2, offsetOnTheDisk2) = NormalizeCore(position2.DiskNumber, position2.OffsetOnTheDisk);
            var c = diskNumber1.CompareTo(diskNumber2);
            if (c != 0)
                return c;
            return offsetOnTheDisk1.CompareTo(offsetOnTheDisk2);
        }

        public Boolean Equal(ZipStreamPosition position1, ZipStreamPosition position2)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            Validation.Assert(Equals(position1.Host));
            Validation.Assert(Equals(position2.Host));
            var (diskNumber1, offsetOnTheDisk1) = NormalizeCore(position1.DiskNumber, position1.OffsetOnTheDisk);
            var (diskNumber2, offsetOnTheDisk2) = NormalizeCore(position2.DiskNumber, position2.OffsetOnTheDisk);

            return
                diskNumber1 == diskNumber2
                && offsetOnTheDisk1 == offsetOnTheDisk2;
        }

        public Int32 GetHashCode(ZipStreamPosition position)
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
            Validation.Assert(Equals(position.Host));
            var (diskNumber, offsetOnTheDisk) = NormalizeCore(position.DiskNumber, position.OffsetOnTheDisk);
            return HashCode.Combine(diskNumber, offsetOnTheDisk);
        }

        public Boolean Equals(IVirtualZipFile? other)
           => other is not null
               && GetType() == other.GetType()
               && _instanceId == ((ZipOutputStream)other)._instanceId;

        protected abstract ZipStreamPosition PositionCore { get; }

        protected override Int32 WriteCore(ReadOnlySpan<Byte> buffer)
        {
            var currentStream = GetCurrentStreamCore();
            return currentStream.Write(buffer[..GetSizeToWrite(currentStream, buffer)]);
        }

        protected override Task<Int32> WriteAsyncCore(ReadOnlyMemory<Byte> buffer, CancellationToken cancellationToken)
        {
            var currentStream = GetCurrentStreamCore();
            return currentStream.WriteAsync(buffer[..GetSizeToWrite(currentStream, buffer.Span)], cancellationToken);
        }

        protected override void FlushCore() { }
        protected override Task FlushAsyncCore(CancellationToken cancellationToken = default) => Task.CompletedTask;
        protected virtual UInt64 MaximumDiskSizeCore => UInt64.MaxValue;
        protected virtual void ReserveAtomicSpaceCore(UInt64 atomicSpaceSize) { }
        protected virtual void LockVolumeDiskCore() { }
        protected virtual void UnlockVolumeDiskCore() { }
        protected abstract void CleanUpCore();
        protected abstract IRandomOutputByteStream<UInt64> GetCurrentStreamCore();
        protected abstract ZipStreamPosition AddCore(UInt32 diskNumber, UInt64 offsetOnTheDisk, UInt64 offset);
        protected abstract ZipStreamPosition SubtractCore(UInt32 diskNumber, UInt64 offsetOnTheDisk, UInt64 offset);
        protected abstract UInt64 SubtractCore(UInt32 diskNumber1, UInt64 offsetOnTheDisk1, UInt32 diskNumber2, UInt64 offsetOnTheDisk2);
        protected virtual (UInt32 diskNumber, UInt64 offsetOnTheDisk) NormalizeCore(UInt32 diskNumber, UInt64 offsetOnTheDisk) => (diskNumber, offsetOnTheDisk);

        protected override void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                }

                if (!_isCompletedSuccessfully)
                    CleanUpCore();

                _isDisposed = true;
            }

            base.Dispose(disposing);
        }

        protected override async Task DisposeAsyncCore()
        {
            if (!_isDisposed)
            {
                if (!_isCompletedSuccessfully)
                    CleanUpCore();
                _isDisposed = true;
            }

            await base.DisposeAsyncCore().ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Int32 GetSizeToWrite(IRandomOutputByteStream<UInt64> stream, ReadOnlySpan<Byte> buffer)
        {
            var sizeToWrite = checked((Int32)(MaximumDiskSizeCore - stream.Length).Minimum((UInt64)buffer.Length));
            Validation.Assert(sizeToWrite > 0);
            return sizeToWrite;
        }
    }
}
