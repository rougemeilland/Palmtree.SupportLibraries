using System;
using System.Collections.Generic;

namespace Palmtree
{
    public interface ICrcCalculationState<CRC_VALUE_T>
        where CRC_VALUE_T : struct
    {
        void Put(Byte data);
        void Put(Byte[] data, Int32 offset, Int32 count);
        void Put(ReadOnlySpan<Byte> data);
        void Put(IEnumerable<Byte> data);
        void Reset();
        (CRC_VALUE_T, UInt64) GetResultValue();
    }
}
