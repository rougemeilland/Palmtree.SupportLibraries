using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    partial class ArrayExtensions
    {
        #region InternalQuickSort
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalQuickSort<ELEMENT_T>(ELEMENT_T[] source, Int32 offset, Int32 count)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (source.Length < 2)
                return;
            else if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref source[offset]), count);
            else
                InternalQuickSortManaged(source, offset, offset + count - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalQuickSort<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelector)
            where KEY_T : IComparable<KEY_T>
        {
            if (source.Length < 2)
                return;
            else if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Boolean, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Char, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<SByte, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Byte, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int16, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt16, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int32, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt32, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int64, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt64, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Single, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Double, KEY_T>>(ref keySelector));
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Decimal, KEY_T>>(ref keySelector));
            else
                InternalQuickSortManaged(source, offset, offset + count - 1, keySelector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalQuickSort<ELEMENT_T>(ELEMENT_T[] source, Int32 offset, Int32 count, IComparer<ELEMENT_T> comparer)
        {
            if (source.Length < 2)
                return;
            else if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Boolean>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Char>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<SByte>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Byte>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int16>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt16>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int32>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt32>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int64>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt64>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Single>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Double>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref source[offset]), count, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Decimal>>(ref comparer));
            else
                InternalQuickSortManaged(source, offset, offset + count - 1, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalQuickSort<ELEMENT_T, KEY_T>(ELEMENT_T[] source, Int32 offset, Int32 count, Func<ELEMENT_T, KEY_T> keySelector, IComparer<KEY_T> keyComparer)
        {
            if (source.Length < 2)
                return;
            else if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Boolean, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Char, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<SByte, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Byte, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int16, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt16, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int32, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt32, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int64, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt64, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Single, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Double, KEY_T>>(ref keySelector), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref source[offset]), count, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Decimal, KEY_T>>(ref keySelector), keyComparer);
            else
                InternalQuickSortManaged(source, offset, offset + count - 1, keySelector, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalQuickSort<ELEMENT_T>(Span<ELEMENT_T> source)
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
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref source.GetPinnableReference()), source.Length);
            else
                InternalQuickSortManaged(source, 0, source.Length - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalQuickSort<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySekecter)
            where KEY_T : IComparable<KEY_T>
        {
            if (source.Length < 2)
                return;
            else if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Boolean, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Char, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<SByte, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Byte, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int16, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt16, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int32, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt32, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int64, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt64, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Single, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Double, KEY_T>>(ref keySekecter));
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Decimal, KEY_T>>(ref keySekecter));
            else
                InternalQuickSortManaged(source, 0, source.Length - 1, keySekecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalQuickSort<ELEMENT_T>(Span<ELEMENT_T> source, IComparer<ELEMENT_T> comparer)
        {
            if (source.Length < 2)
                return;
            else if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Boolean>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Char>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<SByte>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Byte>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int16>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt16>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int32>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt32>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int64>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt64>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Single>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Double>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref source.GetPinnableReference()), source.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Decimal>>(ref comparer));
            else
                InternalQuickSortManaged(source, 0, source.Length - 1, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalQuickSort<ELEMENT_T, KEY_T>(Span<ELEMENT_T> source, Func<ELEMENT_T, KEY_T> keySekecter, IComparer<KEY_T> keyComparer)
        {
            if (source.Length < 2)
                return;
            else if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Boolean, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Char, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<SByte, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Byte, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int16, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt16, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int32, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt32, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Int64, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<UInt64, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Single, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Double, KEY_T>>(ref keySekecter), keyComparer);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalQuickSortUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref source.GetPinnableReference()), source.Length, Unsafe.As<Func<ELEMENT_T, KEY_T>, Func<Decimal, KEY_T>>(ref keySekecter), keyComparer);
            else
                InternalQuickSortManaged(source, 0, source.Length - 1, keySekecter, keyComparer);
        }
        #endregion

        #region InternalSequenceEqual
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean InternalSequenceEqual<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Boolean>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Char))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Char>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, SByte>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Byte>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int16>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt16>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int32>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt32>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int64>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt64>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Single))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Single>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Double))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Double>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Decimal>(ref array2[array2Offset]), array2Length);
            else
                return InternalSequenceEqualManaged(array1, array1Offset, array1Length, array2, array2Offset, array2Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean InternalSequenceEqual<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Boolean>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Boolean>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Char))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Char>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Char>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(SByte))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, SByte>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<SByte>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Byte))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Byte>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Byte>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Int16))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int16>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Int16>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt16>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<UInt16>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Int32))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int32>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Int32>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt32>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<UInt32>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Int64))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int64>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Int64>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt64>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<UInt64>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Single))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Single>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Single>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Double))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Double>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Double>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Decimal>(ref array2[array2Offset]), array2Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Decimal>>(ref equalityComparer));
            else
                return InternalSequenceEqualManaged(array1, array1Offset, array1Length, array2, array2Offset, array2Length, equalityComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean InternalSequenceEqual<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IEquatable<ELEMENT_T>
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Boolean>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Char))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Char>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, SByte>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Byte>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int16>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt16>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int32>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt32>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int64>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt64>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Single))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Double))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else
                return InternalSequenceEqualManaged(array1, array2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean InternalSequenceEqual<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IEqualityComparer<ELEMENT_T> equalityComparer)
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Boolean>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Boolean>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Char))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Char>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Char>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(SByte))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, SByte>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<SByte>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Byte))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Byte>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Byte>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Int16))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int16>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Int16>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt16>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<UInt16>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Int32))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int32>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Int32>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt32>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<UInt32>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Int64))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int64>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Int64>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt64>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<UInt64>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Single))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Single>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Double))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Double>>(ref equalityComparer));
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                return InternalSequenceEqualUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IEqualityComparer<ELEMENT_T>, IEqualityComparer<Decimal>>(ref equalityComparer));
            else
                return InternalSequenceEqualManaged(array1, array2, equalityComparer);
        }
        #endregion

        #region InternalSequenceCompare
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 InternalSequenceCompare<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Boolean>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Char))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Char>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, SByte>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Byte>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int16>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt16>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int32>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt32>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int64>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt64>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Single))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Single>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Double))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Double>(ref array2[array2Offset]), array2Length);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Decimal>(ref array2[array2Offset]), array2Length);
            else
                return InternalSequenceCompareManaged(array1, array1Offset, array1Length, array2, array2Offset, array2Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 InternalSequenceCompare<ELEMENT_T>(ELEMENT_T[] array1, Int32 array1Offset, Int32 array1Length, ELEMENT_T[] array2, Int32 array2Offset, Int32 array2Length, IComparer<ELEMENT_T> comparer)
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Boolean>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Boolean>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Char))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Char>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Char>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(SByte))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, SByte>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<SByte>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Byte))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Byte>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Byte>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int16))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int16>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int16>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt16>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt16>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int32))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int32>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int32>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt32>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt32>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int64))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Int64>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int64>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, UInt64>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt64>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Single))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Single>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Single>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Double))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Double>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Double>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref array1[array1Offset]), array1Length, ref Unsafe.As<ELEMENT_T, Decimal>(ref array2[array2Offset]), array2Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Decimal>>(ref comparer));
            else
                return InternalSequenceCompareManaged(array1, array1Offset, array1Length, array2, array2Offset, array2Length, comparer);
        }

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
            else if (typeof(ELEMENT_T) == typeof(Single))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Double))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length);
            else
                return InternalSequenceCompareManaged(array1, array2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 InternalSequenceCompare<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Boolean>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Boolean>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Char))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Char>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Char>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(SByte))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, SByte>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<SByte>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Byte))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Byte>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Byte>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int16))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int16>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int16>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt16>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt16>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int32))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int32>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int32>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt32>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt32>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Int64))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Int64>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Int64>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, UInt64>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<UInt64>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Single))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Single>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Double))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Double>>(ref comparer));
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                return InternalSequenceCompareUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array1.GetPinnableReference())), array1.Length, ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in array2.GetPinnableReference())), array2.Length, Unsafe.As<IComparer<ELEMENT_T>, IComparer<Decimal>>(ref comparer));
            else
                return InternalSequenceCompareManaged(array1, array2, comparer);
        }
        #endregion

        #region InternalCopyMemory
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalCopyMemory<ELEMENT_T>(ELEMENT_T[] sourceArray, Int32 sourceArrayOffset, ELEMENT_T[] destinationArray, Int32 destinationArrayOffset, Int32 count)
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, Boolean>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, Char>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, SByte>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, Byte>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, Int16>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, UInt16>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, Int32>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, UInt32>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, Int64>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, UInt64>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, Single>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, Double>(ref destinationArray[destinationArrayOffset]), count);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref sourceArray[sourceArrayOffset]), ref Unsafe.As<ELEMENT_T, Decimal>(ref destinationArray[destinationArrayOffset]), count);
            else
                InternalCopyMemoryManaged(sourceArray, ref sourceArrayOffset, destinationArray, ref destinationArrayOffset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalCopyMemory<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> sourceArray, Span<ELEMENT_T> destinationArray)
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Boolean>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Char>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, SByte>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Byte>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Int16>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, UInt16>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Int32>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, UInt32>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Int64>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, UInt64>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Single>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Double>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalCopyMemoryUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref Unsafe.AsRef(in sourceArray.GetPinnableReference())), ref Unsafe.As<ELEMENT_T, Decimal>(ref destinationArray.GetPinnableReference()), sourceArray.Length);
            else
                InternalCopyMemoryManaged(sourceArray, destinationArray);
        }
        #endregion

        #region InternalReverseArray
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalReverseArray<ELEMENT_T>(this ELEMENT_T[] source, Int32 offset, Int32 count)
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref source[offset]), count);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref source[offset]), count);
            else
                InternalReverseArrayManaged(source, offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void InternalReverseArray<ELEMENT_T>(Span<ELEMENT_T> source)
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Boolean>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Char))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Char>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(SByte))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, SByte>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Byte))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Byte>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Int16))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Int16>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt16))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, UInt16>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Int32))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Int32>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt32))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, UInt32>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Int64))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Int64>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(UInt64))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, UInt64>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Single))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Single>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Double))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Double>(ref source.GetPinnableReference()), source.Length);
            else if (typeof(ELEMENT_T) == typeof(Decimal))
                InternalReverseArrayUnmanaged(ref Unsafe.As<ELEMENT_T, Decimal>(ref source.GetPinnableReference()), source.Length);
            else
                InternalReverseArrayManaged(source);
        }
        #endregion
    }
}
