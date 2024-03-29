﻿using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Palmtree.IO.Compression.Archive.Zip
{
    internal readonly struct ZipStreamPosition
        : IEquatable<ZipStreamPosition>, IComparable<ZipStreamPosition>, IAdditionOperators<ZipStreamPosition, UInt64, ZipStreamPosition>, ISubtractionOperators<ZipStreamPosition, UInt64, ZipStreamPosition>, ISubtractionOperators<ZipStreamPosition, ZipStreamPosition, UInt64>
    {
        internal ZipStreamPosition(UInt32 diskNumber, UInt64 offsetOnTheDisk, IVirtualZipFile hostVirtualDisk)
        {
            if (hostVirtualDisk is null)
                throw new ArgumentNullException(nameof(hostVirtualDisk));

            DiskNumber = diskNumber;
            OffsetOnTheDisk = offsetOnTheDisk;
            Host = hostVirtualDisk;
        }

        public UInt32 DiskNumber { get; }
        public UInt64 OffsetOnTheDisk { get; }

        #region operator +

        public static ZipStreamPosition operator +(ZipStreamPosition x, UInt64 y) => x.Add(y);
        public static ZipStreamPosition operator +(UInt64 x, ZipStreamPosition y) => y.Add(x);

        #endregion

        #region operator -

        public static UInt64 operator -(ZipStreamPosition x, ZipStreamPosition y) => x.Subtract(y);
        public static ZipStreamPosition operator -(ZipStreamPosition x, UInt64 y) => x.Subtract(y);

        #endregion

        #region other operator

        public static Boolean operator ==(ZipStreamPosition x, ZipStreamPosition y) => x.Equals(y);
        public static Boolean operator !=(ZipStreamPosition x, ZipStreamPosition y) => !x.Equals(y);
        public static Boolean operator >(ZipStreamPosition x, ZipStreamPosition y) => x.CompareTo(y) > 0;
        public static Boolean operator >=(ZipStreamPosition x, ZipStreamPosition y) => x.CompareTo(y) >= 0;
        public static Boolean operator <(ZipStreamPosition x, ZipStreamPosition y) => x.CompareTo(y) < 0;
        public static Boolean operator <=(ZipStreamPosition x, ZipStreamPosition y) => x.CompareTo(y) <= 0;

        #endregion

        #region Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ZipStreamPosition Add(UInt64 x)
        {
            Validation.Assert(Host is not null, "Host is not null");
            return Host.Add(this, x);
        }

        #endregion

        #region Subtract

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt64 Subtract(ZipStreamPosition x)
        {
            Validation.Assert(Host is not null, "Host is not null");
            return Host.Subtract(this, x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ZipStreamPosition Subtract(UInt64 x)
        {
            Validation.Assert(Host is not null, "Host is not null");
            return Host.Subtract(this, x);
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 CompareTo(ZipStreamPosition other)
        {
            Validation.Assert(Host is not null, "Host is not null");
            return Host.Compare(this, other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Equals(ZipStreamPosition other)
        {
            Validation.Assert(Host is not null, "Host is not null");
            return Host.Equal(this, other);
        }

        public override Boolean Equals(Object? other)
            => other is not null
                && GetType() == other.GetType()
                && Equals((ZipStreamPosition)other);

        public override Int32 GetHashCode()
        {
            Validation.Assert(Host is not null, "Host is not null");
            return Host.GetHashCode(this);
        }

        public override String ToString() => $"0x{DiskNumber:x8}:0x{OffsetOnTheDisk:x16}";

        internal IVirtualZipFile? Host { get; }
        internal Boolean IsDefault => Host is null;
    }
}
