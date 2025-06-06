using System;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static class Signature
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 MakeUInt32LESignature(Byte byte0, Byte byte1, Byte byte2, Byte byte3)
            => ((UInt32)byte0 << (8 * 0))
                | (UInt32)byte1 << (8 * 1)
                | (UInt32)byte2 << (8 * 2)
                | (UInt32)byte3 << (8 * 3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 MakeUInt32LESignature(Char c0, Char c1, Char c2, Char c3)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(c0, '\x00');
            ArgumentOutOfRangeException.ThrowIfGreaterThan(c0, '\xff');
            ArgumentOutOfRangeException.ThrowIfLessThan(c1, '\x00');
            ArgumentOutOfRangeException.ThrowIfGreaterThan(c1, '\xff');
            ArgumentOutOfRangeException.ThrowIfLessThan(c2, '\x00');
            ArgumentOutOfRangeException.ThrowIfGreaterThan(c2, '\xff');
            ArgumentOutOfRangeException.ThrowIfLessThan(c3, '\x00');
            ArgumentOutOfRangeException.ThrowIfGreaterThan(c3, '\xff');

            return
                (UInt32)(Byte)c0 << (8 * 0)
                | (UInt32)(Byte)c1 << (8 * 1)
                | (UInt32)(Byte)c2 << (8 * 2)
                | (UInt32)(Byte)c3 << (8 * 3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 MakeUInt32BESignature(Byte byte0, Byte byte1, Byte byte2, Byte byte3)
            => ((UInt32)byte0 << (8 * 3))
                | (UInt32)byte1 << (8 * 2)
                | (UInt32)byte2 << (8 * 1)
                | (UInt32)byte3 << (8 * 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 MakeUInt32BESignature(Char c0, Char c1, Char c2, Char c3)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(c0, '\x00');
            ArgumentOutOfRangeException.ThrowIfGreaterThan(c0, '\xff');
            ArgumentOutOfRangeException.ThrowIfLessThan(c1, '\x00');
            ArgumentOutOfRangeException.ThrowIfGreaterThan(c1, '\xff');
            ArgumentOutOfRangeException.ThrowIfLessThan(c2, '\x00');
            ArgumentOutOfRangeException.ThrowIfGreaterThan(c2, '\xff');
            ArgumentOutOfRangeException.ThrowIfLessThan(c3, '\x00');
            ArgumentOutOfRangeException.ThrowIfGreaterThan(c3, '\xff');

            return
                (UInt32)(Byte)c0 << (8 * 3)
                | (UInt32)(Byte)c1 << (8 * 2)
                | (UInt32)(Byte)c2 << (8 * 1)
                | (UInt32)(Byte)c3 << (8 * 0);
        }
    }
}
