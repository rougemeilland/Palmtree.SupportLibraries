﻿using System;
using System.Collections.Generic;

namespace Palmtree
{
    public interface ICrcCalculationState<CRC_VALUE_T>
        where CRC_VALUE_T : struct
    {
        public void Put(Byte data);
        public void Put(Byte[] data, Int32 offset, Int32 count);
        public void Put(ReadOnlySpan<Byte> data);
        public void Put(IEnumerable<Byte> data);
        public void Reset();
        public (CRC_VALUE_T, UInt64) GetResultValue();
    }
}
