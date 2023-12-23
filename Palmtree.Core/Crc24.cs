using System;

namespace Palmtree
{
    public static class Crc24
    {
        public static ICrcCalculationState<UInt32> CreateCalculationState()
            => ByteArrayExtensions.CreateCrc24CalculationState();
    }
}
