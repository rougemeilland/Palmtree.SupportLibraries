using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Palmtree
{
    public static partial class ArrayExtensions
    {
        #region SequenceCompareTo

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(array2);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(comparer);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> selecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(selecter);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, selecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array1);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, array2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(comparer);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, array2, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(keySelecter);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ELEMENT_T[] array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(array1);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array2);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(comparer);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(keySelecter);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
            => SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, array2);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(comparer);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, array2, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this Span<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            return SequenceCompareToCore((ReadOnlySpan<ELEMENT_T>)array1, array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
        {
            ArgumentNullException.ThrowIfNull(array2);

            return SequenceCompareToCore(array1, (ReadOnlySpan<ELEMENT_T>)array2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(comparer);

            return SequenceCompareToCore(array1, (ReadOnlySpan<ELEMENT_T>)array2, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(keySelecter);

            return SequenceCompareToCore(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ELEMENT_T[] array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(array2);
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            return SequenceCompareToCore(array1, (ReadOnlySpan<ELEMENT_T>)array2, keySelecter, keyComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>
            => SequenceCompareToCore(array1, array2);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> comparer)
        {
            ArgumentNullException.ThrowIfNull(comparer);

            return SequenceCompareToCore(array1, array2, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>
        {
            ArgumentNullException.ThrowIfNull(keySelecter);

            return SequenceCompareToCore(array1, array2, keySelecter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 SequenceCompareTo<ELEMENT_T, KEY_T>(this ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            return SequenceCompareToCore(array1, array2, keySelecter, keyComparer);
        }

        #endregion

        #region SequenceCompareToCore

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Int32 SequenceCompareToCore<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2)
            where ELEMENT_T : IComparable<ELEMENT_T>?
        {
            if (typeof(ELEMENT_T) == typeof(SByte))
            {
                return
                    SequenceCompareToCore(
                        ref Unsafe.As<ELEMENT_T, SByte>(ref MemoryMarshal.GetReference(array1)),
                        array1.Length,
                        ref Unsafe.As<ELEMENT_T, SByte>(ref MemoryMarshal.GetReference(array2)),
                        array2.Length);
            }
            else if (typeof(ELEMENT_T) == typeof(Byte))
            {
                return
                    SequenceCompareToCore(
                        ref Unsafe.As<ELEMENT_T, Byte>(ref MemoryMarshal.GetReference(array1)),
                        array1.Length,
                        ref Unsafe.As<ELEMENT_T, Byte>(ref MemoryMarshal.GetReference(array2)),
                        array2.Length);
            }
            else if (typeof(ELEMENT_T) == typeof(Int16))
            {
                return
                    SequenceCompareToCore(
                        ref Unsafe.As<ELEMENT_T, Int16>(ref MemoryMarshal.GetReference(array1)),
                        array1.Length,
                        ref Unsafe.As<ELEMENT_T, Int16>(ref MemoryMarshal.GetReference(array2)),
                        array2.Length);
            }
            else if (typeof(ELEMENT_T) == typeof(UInt16))
            {
                return
                    SequenceCompareToCore(
                        ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array1)),
                        array1.Length,
                        ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array2)),
                        array2.Length);
            }
            else if (typeof(ELEMENT_T) == typeof(Char))
            {
                return
                    SequenceCompareToCore(
                        ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array1)),
                        array1.Length,
                        ref Unsafe.As<ELEMENT_T, UInt16>(ref MemoryMarshal.GetReference(array2)),
                        array2.Length);
            }
            else if (typeof(ELEMENT_T) == typeof(Int32))
            {
                return
                    SequenceCompareToCore(
                        ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array1)),
                        array1.Length,
                        ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array2)),
                        array2.Length);
            }
            else if (typeof(ELEMENT_T) == typeof(UInt32))
            {
                return
                    SequenceCompareToCore(
                        ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array1)),
                        array1.Length,
                        ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array2)),
                        array2.Length);
            }
            else if (typeof(ELEMENT_T) == typeof(Int64))
            {
                return
                    SequenceCompareToCore(
                        ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array1)),
                        array1.Length,
                        ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array2)),
                        array2.Length);
            }
            else if (typeof(ELEMENT_T) == typeof(UInt64))
            {
                return
                    SequenceCompareToCore(
                        ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array1)),
                        array1.Length,
                        ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array2)),
                        array2.Length);
            }
            else if (typeof(ELEMENT_T) == typeof(IntPtr))
            {
                return
                    Unsafe.SizeOf<IntPtr>() switch
                    {
                        sizeof(Int32) =>
                            SequenceCompareToCore(
                                ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array1)),
                                array1.Length,
                                ref Unsafe.As<ELEMENT_T, Int32>(ref MemoryMarshal.GetReference(array2)),
                                array2.Length),
                        sizeof(Int64) =>
                            SequenceCompareToCore(
                                ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array1)),
                                array1.Length,
                                ref Unsafe.As<ELEMENT_T, Int64>(ref MemoryMarshal.GetReference(array2)),
                                array2.Length),
                        _ => throw Validation.GetFatalErrorException()
                    };
            }
            else if (typeof(ELEMENT_T) == typeof(UIntPtr))
            {
                return
                    Unsafe.SizeOf<UIntPtr>() switch
                    {
                        sizeof(UInt32) =>
                            SequenceCompareToCore(
                                ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array1)),
                                array1.Length,
                                ref Unsafe.As<ELEMENT_T, UInt32>(ref MemoryMarshal.GetReference(array2)),
                                array2.Length),
                        sizeof(UInt64) =>
                            SequenceCompareToCore(
                                ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array1)),
                                array1.Length,
                                ref Unsafe.As<ELEMENT_T, UInt64>(ref MemoryMarshal.GetReference(array2)),
                                array2.Length),
                        _ => throw Validation.GetFatalErrorException()
                    };
            }
            else if (typeof(ELEMENT_T) == typeof(Single))
            {
                return
                    SequenceCompareToCore(
                        ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array1)),
                        array1.Length,
                        ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array2)),
                        array2.Length);
            }
            else if (typeof(ELEMENT_T) == typeof(Double))
            {
                return
                    SequenceCompareToCore(
                        ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array1)),
                        array1.Length,
                        ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array2)),
                        array2.Length);
            }
            else if (typeof(ELEMENT_T) == typeof(NFloat))
            {
                return
                    Unsafe.SizeOf<NFloat>() switch
                    {
                        sizeof(Double) =>
                            SequenceCompareToCore(
                                ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array1)),
                                array1.Length,
                                ref Unsafe.As<ELEMENT_T, Double>(ref MemoryMarshal.GetReference(array2)),
                                array2.Length),
                        sizeof(Single) =>
                            SequenceCompareToCore(
                                ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array1)),
                                array1.Length,
                                ref Unsafe.As<ELEMENT_T, Single>(ref MemoryMarshal.GetReference(array2)),
                                array2.Length),
                        _ => throw Validation.GetFatalErrorException()
                    };
            }
            else
            {
                var count = Int32.Min(array1.Length, array2.Length);
                for (var index = 0; index < count; index++)
                {
                    var c = Compare(array1[index], array2[index]);
                    if (c != 0)
                        return c;
                }

                return array1.Length - array2.Length;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Int32 Compare(ELEMENT_T value1, ELEMENT_T value2)
            {
                if (value1 is null)
                    return value2 is not null ? 0 : -1;
                else
                    return value2 is null ? 1 : value1.CompareTo(value2);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Int32 SequenceCompareToCore<ELEMENT_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, IComparer<ELEMENT_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(keyComparer);

            var count = array1.Length.Minimum(array2.Length);
            for (var index = 0; index < count; index++)
            {
                var c = keyComparer.Compare(array1[index], array2[index]);
                if (c != 0)
                    return c;
            }

            return array1.Length.CompareTo(array2.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Int32 SequenceCompareToCore<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter)
            where KEY_T : IComparable<KEY_T>?
        {
            ArgumentNullException.ThrowIfNull(keySelecter);

            var count = array1.Length.Minimum(array2.Length);
            for (var index = 0; index < count; index++)
            {
                var c = Compare(keySelecter(array1[index]), keySelecter(array2[index]));
                if (c != 0)
                    return c;
            }

            return array1.Length.CompareTo(array2.Length);

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Int32 Compare([AllowNull] KEY_T key1, [AllowNull] KEY_T key2)
            {
                if (key1 is null)
                    return key2 is null ? 0 : -1;
                else
                    return key2 is null ? 1 : key1.CompareTo(key2);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Int32 SequenceCompareToCore<ELEMENT_T, KEY_T>(ReadOnlySpan<ELEMENT_T> array1, ReadOnlySpan<ELEMENT_T> array2, Func<ELEMENT_T, KEY_T> keySelecter, IComparer<KEY_T> keyComparer)
        {
            ArgumentNullException.ThrowIfNull(keySelecter);
            ArgumentNullException.ThrowIfNull(keyComparer);

            var count = array1.Length.Minimum(array2.Length);
            for (var index = 0; index < count; index++)
            {
                var c = keyComparer.Compare(keySelecter(array1[index]), keySelecter(array2[index]));
                if (c != 0)
                    return c;
            }

            return array1.Length.CompareTo(array2.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe Int32 SequenceCompareToCore<ELEMENT_T>(ref ELEMENT_T left, Int32 leftLength, ref ELEMENT_T right, Int32 rightLength)
            where ELEMENT_T : IComparable<ELEMENT_T>?
        {
            Validation.Assert(leftLength >= 0);
            Validation.Assert(rightLength >= 0);

            if (Unsafe.AreSame(ref left, ref right))
                return leftLength - rightLength;

            var minimumLength = (UIntPtr)(((UInt32)leftLength < (UInt32)rightLength) ? (UInt32)leftLength : (UInt32)rightLength);

            if (Vector512.IsHardwareAccelerated && Vector512<ELEMENT_T>.IsSupported && minimumLength >= (UIntPtr)Vector512<ELEMENT_T>.Count)
            {
                var result = CompareToByVector512(ref left, ref right, minimumLength);
                return result != 0 ? result : leftLength - rightLength;
            }
            else if (Vector256.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && minimumLength >= (UIntPtr)Vector256<ELEMENT_T>.Count)
            {
                var result = CompareToByVector256(ref left, ref right, minimumLength);
                return result != 0 ? result : leftLength - rightLength;
            }
            else if (Vector128.IsHardwareAccelerated && Vector256<ELEMENT_T>.IsSupported && minimumLength >= (UIntPtr)Vector128<ELEMENT_T>.Count)
            {
                var result = CompareToByVector128(ref left, ref right, minimumLength);
                return result != 0 ? result : leftLength - rightLength;
            }
            else if (IsBitwiseEquatableQuickly<ELEMENT_T>())
            {
                var result = CompareToByUintPtr(ref left, ref right, minimumLength);
                return result != 0 ? result : leftLength - rightLength;
            }
            else
            {
                var result = CompareToByDefault(ref left, ref right, minimumLength);
                return result != 0 ? result : leftLength - rightLength;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Int32 CompareToByVector512(ref ELEMENT_T left, ref ELEMENT_T right, UIntPtr length)
            {
                Validation.Assert(Vector512.IsHardwareAccelerated == true);
                Validation.Assert(Vector512<ELEMENT_T>.IsSupported == true);
                Validation.Assert(length >= (UIntPtr)Vector512<ELEMENT_T>.Count);

                var offset = (UIntPtr)0;
                var lengthToCompare = length - (UIntPtr)Vector512<ELEMENT_T>.Count;
                while (offset < lengthToCompare)
                {
                    var matches = Vector512.Equals(Vector512.LoadUnsafe(ref left, offset), Vector512.LoadUnsafe(ref right, offset)).ExtractMostSignificantBits();
                    if (!IsMatched(matches))
                        return Final(ref left, ref right, offset, ~matches);

                    offset += (UIntPtr)Vector512<ELEMENT_T>.Count;
                }

                {
                    var matches = Vector512.Equals(Vector512.LoadUnsafe(ref left, lengthToCompare), Vector512.LoadUnsafe(ref right, lengthToCompare)).ExtractMostSignificantBits();
                    if (!IsMatched(matches))
                        return Final(ref left, ref right, lengthToCompare, ~matches);
                }

                return 0;

                [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
                static Int32 Final(ref ELEMENT_T left, ref ELEMENT_T right, UIntPtr offset, UInt64 differences)
                {
                    Validation.Assert(IsMatched(differences) == false);

                    offset += (UInt32)BitOperations.TrailingZeroCount(differences);
                    var leftElement = Unsafe.AddByteOffset(ref left, offset * (UInt32)Unsafe.SizeOf<ELEMENT_T>());
                    var rightElement = Unsafe.AddByteOffset(ref right, offset * (UInt32)Unsafe.SizeOf<ELEMENT_T>());

                    Validation.Assert(leftElement is not null); // Since "Vector512<ELEMENT_T>.IsSupported == true", "ELEMENT_T" cannot be a nullable type.
                    Validation.Assert(rightElement is not null); // Since "Vector512<ELEMENT_T>.IsSupported == true", "ELEMENT_T" cannot be a nullable type.

                    var result = leftElement.CompareTo(rightElement);

                    Validation.Assert(result != 0);

                    return result;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
                static Boolean IsMatched(UInt64 comparisonResult)
                {

                    var allMatched = UInt64.MaxValue >> (64 - Vector512<ELEMENT_T>.Count); // An integer consisting of "1" bits for "Vector512<ELEMENT_T>.Count"
                    return comparisonResult == allMatched;

                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Int32 CompareToByVector256(ref ELEMENT_T left, ref ELEMENT_T right, UIntPtr length)
            {
                Validation.Assert(Vector256.IsHardwareAccelerated == true);
                Validation.Assert(Vector256<ELEMENT_T>.IsSupported == true);
                Validation.Assert(length >= (UIntPtr)Vector256<ELEMENT_T>.Count);

                var offset = (UIntPtr)0;
                var lengthToCompare = length - (UIntPtr)Vector256<ELEMENT_T>.Count;
                while (offset < lengthToCompare)
                {
                    var matches = Vector256.Equals(Vector256.LoadUnsafe(ref left, offset), Vector256.LoadUnsafe(ref right, offset)).ExtractMostSignificantBits();
                    if (!IsMatched(matches))
                        return Final(ref left, ref right, offset, ~matches);

                    offset += (UIntPtr)Vector256<ELEMENT_T>.Count;
                }

                {
                    var matches = Vector256.Equals(Vector256.LoadUnsafe(ref left, lengthToCompare), Vector256.LoadUnsafe(ref right, lengthToCompare)).ExtractMostSignificantBits();
                    if (!IsMatched(matches))
                        return Final(ref left, ref right, lengthToCompare, ~matches);
                }

                return 0;

                [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
                static Int32 Final(ref ELEMENT_T left, ref ELEMENT_T right, UIntPtr offset, UInt32 differences)
                {
                    Validation.Assert(IsMatched(differences) == false);

                    offset += (UInt32)BitOperations.TrailingZeroCount(differences);
                    var leftElement = Unsafe.AddByteOffset(ref left, offset * (UInt32)Unsafe.SizeOf<ELEMENT_T>());
                    var rightElement = Unsafe.AddByteOffset(ref right, offset * (UInt32)Unsafe.SizeOf<ELEMENT_T>());

                    Validation.Assert(leftElement is not null); // Since "Vector256<ELEMENT_T>.IsSupported == true", "ELEMENT_T" cannot be a nullable type.
                    Validation.Assert(rightElement is not null); // Since "Vector256<ELEMENT_T>.IsSupported == true", "ELEMENT_T" cannot be a nullable type.

                    var result = leftElement.CompareTo(rightElement);

                    Validation.Assert(result != 0);

                    return result;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
                static Boolean IsMatched(UInt32 comparisonResult)
                {

                    var allMatched = UInt32.MaxValue >> (32 - Vector256<ELEMENT_T>.Count); // An integer consisting of "1" bits for "Vector256<ELEMENT_T>.Count"
                    return comparisonResult == allMatched;

                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Int32 CompareToByVector128(ref ELEMENT_T left, ref ELEMENT_T right, UIntPtr length)
            {
                Validation.Assert(Vector128.IsHardwareAccelerated == true);
                Validation.Assert(Vector128<ELEMENT_T>.IsSupported == true);
                Validation.Assert(length >= (UIntPtr)Vector128<ELEMENT_T>.Count);

                var offset = (UIntPtr)0;
                var lengthToCompare = length - (UIntPtr)Vector128<ELEMENT_T>.Count;
                while (offset < lengthToCompare)
                {
                    var matches = Vector128.Equals(Vector128.LoadUnsafe(ref left, offset), Vector128.LoadUnsafe(ref right, offset)).ExtractMostSignificantBits();
                    if (!IsMatched(matches))
                        return Final(ref left, ref right, offset, ~matches);

                    offset += (UIntPtr)Vector128<ELEMENT_T>.Count;
                }

                {
                    var matches = Vector128.Equals(Vector128.LoadUnsafe(ref left, lengthToCompare), Vector128.LoadUnsafe(ref right, lengthToCompare)).ExtractMostSignificantBits();
                    if (!IsMatched(matches))
                        return Final(ref left, ref right, lengthToCompare, ~matches);
                }

                return 0;

                [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
                static Int32 Final(ref ELEMENT_T left, ref ELEMENT_T right, UIntPtr offset, UInt32 differences)
                {
                    Validation.Assert(IsMatched(differences) == false);

                    offset += (UInt32)BitOperations.TrailingZeroCount(differences);
                    var leftElement = Unsafe.AddByteOffset(ref left, offset * (UInt32)Unsafe.SizeOf<ELEMENT_T>());
                    var rightElement = Unsafe.AddByteOffset(ref right, offset * (UInt32)Unsafe.SizeOf<ELEMENT_T>());

                    Validation.Assert(leftElement is not null); // Since "Vector128<ELEMENT_T>.IsSupported == true", "ELEMENT_T" cannot be a nullable type.
                    Validation.Assert(rightElement is not null); // Since "Vector128<ELEMENT_T>.IsSupported == true", "ELEMENT_T" cannot be a nullable type.

                    var result = leftElement.CompareTo(rightElement);

                    Validation.Assert(result != 0);

                    return result;
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
                static Boolean IsMatched(UInt32 comparisonResult)
                {

                    var allMatched = UInt16.MaxValue >> (16 - Vector128<ELEMENT_T>.Count); // An integer consisting of "1" bits for "Vector128<ELEMENT_T>.Count"
                    return comparisonResult == allMatched;

                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Int32 CompareToByUintPtr(ref ELEMENT_T left, ref ELEMENT_T right, UIntPtr length)
            {
                Validation.Assert(IsBitwiseEquatableQuickly<ELEMENT_T>() == true);

                var offset = GetCommonPartLength(ref left, ref right, length);

                while (offset < length)
                {
                    var leftValue = Unsafe.AddByteOffset(ref left, offset * (UInt32)Unsafe.SizeOf<ELEMENT_T>());
                    var rightValue = Unsafe.AddByteOffset(ref right, offset * (UInt32)Unsafe.SizeOf<ELEMENT_T>());

                    Validation.Assert(leftValue is not null); // Since "typeof(ELEMENT_T).IsBitwiseEquitable() == true", "ELEMENT_T" cannot be a nullable type.
                    Validation.Assert(rightValue is not null); // Since "typeof(ELEMENT_T).IsBitwiseEquitable() == true", "ELEMENT_T" cannot be a nullable type.

                    var c = leftValue.CompareTo(rightValue);
                    if (c != 0)
                        return c;
                    ++offset;
                }

                return 0;

                [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
                static UIntPtr GetCommonPartLength(ref ELEMENT_T left, ref ELEMENT_T right, UIntPtr length)
                {
                    // "comparisonLength" is not a "const" because sizeof(UIntPtr) is not a constant, but "comparisonLength" is treated as a constant by the JIT at runtime,
                    // and the comparison of "comparisonLength" to a constant is optimized away, removing the code for the false condition.
                    var comparisonLength = (UIntPtr)(Unsafe.SizeOf<ELEMENT_T>() > sizeof(UIntPtr) ? Unsafe.SizeOf<ELEMENT_T>() : sizeof(UIntPtr));

                    var byteOffset = UIntPtr.Zero;
                    var byteLength = length * (UInt32)Unsafe.SizeOf<ELEMENT_T>();
                    while ((byteLength - byteOffset) >= comparisonLength)
                    {
                        if (comparisonLength == (UInt32)sizeof(UIntPtr))
                        {
                            if (GetUIntPtrValue(ref Unsafe.As<ELEMENT_T, Byte>(ref left), byteOffset) != GetUIntPtrValue(ref Unsafe.As<ELEMENT_T, Byte>(ref right), byteOffset))
                                break;
                            byteOffset += comparisonLength;
                        }
                        else if (comparisonLength == (UInt32)sizeof(UIntPtr) * 2)
                        {
                            if (GetUIntPtrValue(ref Unsafe.As<ELEMENT_T, Byte>(ref left), byteOffset + (UInt32)sizeof(UIntPtr) * 0) != GetUIntPtrValue(ref Unsafe.As<ELEMENT_T, Byte>(ref right), byteOffset + (UInt32)sizeof(UIntPtr) * 0))
                                break;
                            if (GetUIntPtrValue(ref Unsafe.As<ELEMENT_T, Byte>(ref left), byteOffset + (UInt32)sizeof(UIntPtr) * 1) != GetUIntPtrValue(ref Unsafe.As<ELEMENT_T, Byte>(ref right), byteOffset + (UInt32)sizeof(UIntPtr) * 1))
                                break;
                            byteOffset += comparisonLength;
                        }
                        else
                        {
                            throw Validation.GetFatalErrorException();
                        }
                    }

                    var result = byteOffset / (UInt32)Unsafe.SizeOf<ELEMENT_T>();

                    Validation.Assert(result * (UInt32)Unsafe.SizeOf<ELEMENT_T>() == byteOffset);

                    return result;

                    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
                    static UIntPtr GetUIntPtrValue(ref Byte start, UIntPtr byteOffset)
                    {
                        return Unsafe.ReadUnaligned<UIntPtr>(ref Unsafe.AddByteOffset(ref start, byteOffset));
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            static Int32 CompareToByDefault(ref ELEMENT_T left, ref ELEMENT_T right, UIntPtr length)
            {
                var offset = (UIntPtr)0;

                while (offset < length)
                {
                    var c =
                            Compare(
                                Unsafe.AddByteOffset(ref left, offset * (UInt32)Unsafe.SizeOf<ELEMENT_T>()),
                                Unsafe.AddByteOffset(ref right, offset * (UInt32)Unsafe.SizeOf<ELEMENT_T>()));
                    if (c != 0)
                        return c;
                    ++offset;
                }

                return 0;

                [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
                static Int32 Compare(ELEMENT_T leftValue, ELEMENT_T rightValue)
                {
                    if (leftValue is null)
                        return rightValue is null ? 0 : -1;
                    else
                        return rightValue is null ? 1 : leftValue.CompareTo(rightValue);
                }
            }
        }

        #endregion
    }
}
