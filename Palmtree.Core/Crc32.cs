using System;

namespace Palmtree
{
    public static class Crc32
    {
        public static ICrcCalculationState<UInt32> CreateCalculationState() => ByteArrayExtensions.CreateCrc32CalculationState();
    }
}
