using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    partial class ArrayExtensions
    {
        #region InternalQuickSort
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void InternalQuickSort<ELEMENT_T>(this Span<ELEMENT_T> source)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (source.Length < 2)
                return;
            else if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref source.GetPinnableReference()), source.Length);
#if NET7_0_OR_GREATER
            else if (typeof(ELEMENT_T) == typeof(Int128))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int128>(ref source.GetPinnableReference()), source.Length);
#endif // NET7_0_OR_GREATER
#if NET7_0_OR_GREATER
            else if (typeof(ELEMENT_T) == typeof(UInt128))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt128>(ref source.GetPinnableReference()), source.Length);
#endif // NET7_0_OR_GREATER
            else if (typeof(ELEMENT_T) == typeof(Half))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Half>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(IntPtr))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, IntPtr>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(UIntPtr))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UIntPtr>(ref source.GetPinnableReference()), source.Length);
            else
                InternalQuickSortManaged(source);
        }
        #endregion

        #region InternalSequenceEqual
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean InternalSequenceEqual<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Boolean>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(Char))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Char>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, SByte>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Byte>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Int16>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, UInt16>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Int32>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, UInt32>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Int64>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, UInt64>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
#if NET7_0_OR_GREATER
            else if (typeof(ELEMENT_T) == typeof(Int128))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int128>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Int128>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
#endif // NET7_0_OR_GREATER
#if NET7_0_OR_GREATER
            else if (typeof(ELEMENT_T) == typeof(UInt128))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt128>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, UInt128>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
#endif // NET7_0_OR_GREATER
            else if (typeof(ELEMENT_T) == typeof(Half))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Half>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Half>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(Single))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(Double))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(IntPtr))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, IntPtr>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, IntPtr>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else if (typeof(ELEMENT_T) == typeof(UIntPtr))
                return array1.Length == array2.Length && InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UIntPtr>(ref Unsafe.AsRef(in array1.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, UIntPtr>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array1.Length);
            else
                return InternalSequenceEqualManaged(array1, array2);
        }
        #endregion

        #region InternalSequenceCompare
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 InternalSequenceCompare<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Boolean>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Char))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Char>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, SByte>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Byte>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int16>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt16>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int32>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt32>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int64>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt64>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
#if NET7_0_OR_GREATER
            else if (typeof(ELEMENT_T) == typeof(Int128))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int128>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int128>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
#endif // NET7_0_OR_GREATER
#if NET7_0_OR_GREATER
            else if (typeof(ELEMENT_T) == typeof(UInt128))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt128>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt128>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
#endif // NET7_0_OR_GREATER
            else if (typeof(ELEMENT_T) == typeof(Half))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Half>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Half>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Single))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Double))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(IntPtr))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, IntPtr>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, IntPtr>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(UIntPtr))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UIntPtr>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UIntPtr>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else
                return InternalSequenceCompareManaged(array1, array2);
        }
        #endregion
    }
}
