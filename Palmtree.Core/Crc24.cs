using System;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public static class Crc24
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ICrcCalculationState<UInt32> CreateCalculationState()
            => ByteArrayExtensions.CreateCrc24CalculationState();
    }
}
