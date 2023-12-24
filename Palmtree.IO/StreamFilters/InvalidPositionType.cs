using System;
using System.Numerics;

namespace Palmtree.IO.StreamFilters
{
    internal readonly struct InvalidPositionType
        : IComparable<InvalidPositionType>, IAdditionOperators<InvalidPositionType, UInt64, InvalidPositionType>, ISubtractionOperators<InvalidPositionType, UInt64, InvalidPositionType>, ISubtractionOperators<InvalidPositionType, InvalidPositionType, UInt64>
    {
        public readonly Int32 CompareTo(InvalidPositionType other) => throw Validation.GetFailErrorException("A method was called that should not have been called.");
        public static InvalidPositionType operator +(InvalidPositionType left, UInt64 right) => throw Validation.GetFailErrorException("A method was called that should not have been called.");
        public static InvalidPositionType operator -(InvalidPositionType left, UInt64 right) => throw Validation.GetFailErrorException("A method was called that should not have been called.");
        public static UInt64 operator -(InvalidPositionType left, InvalidPositionType right) => throw Validation.GetFailErrorException("A method was called that should not have been called.");
    }
}
