using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Experiment.CSharp.Library
{
    public static partial class VectorizedCalculation
    {
        #region NonVectorizedMax

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T NonVectorizedMax<ELEMENT_T>(ref ELEMENT_T array, UIntPtr byteLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(byteLength > 0);
            System.Diagnostics.Debug.Assert(byteLength % (UInt32)Unsafe.SizeOf<ELEMENT_T>() == 0);

            var max = array;
            var byteOffset = (UIntPtr)Unsafe.SizeOf<ELEMENT_T>();
            var byteCount = byteLength - (UInt32)Unsafe.SizeOf<ELEMENT_T>();
            while (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u)
            {
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 17u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 18u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 19u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 20u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 21u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 22u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 23u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 24u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 25u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 26u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 27u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 28u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 29u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 30u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 31u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 32u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u)
            {
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u)
            {
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u)
            {
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u)
            {
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u;
            }

            if (byteCount > 0)
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset));

            return max;
        }

        #endregion

        #region CalculateMaxByVector512

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMaxByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Max" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MaxNumber" method).

            System.Diagnostics.Debug.Assert(Vector512.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Half));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Single));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Double));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
#endif

            var maxv = Vector512.LoadUnsafe(ref array, (UInt32)Vector512<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector512<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector512<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count * 32u)
            {
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 16u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 17u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 18u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 19u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 20u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 21u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 22u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 23u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 24u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 25u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 26u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 27u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 28u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 29u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 30u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 16u)
            {
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 8u)
            {
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 2u)
            {
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 1u)
            {
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                maxv = Vector512.Max(maxv, Vector512.LoadUnsafe(ref array, elementLength - (UInt32)Vector512<ELEMENT_T>.Count));

            return maxv.Max();
        }

        #endregion

        #region CalculateMaxByVector256

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMaxByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Max" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MaxNumber" method).

            System.Diagnostics.Debug.Assert(Vector256.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Half));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Single));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Double));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
#endif

            var maxv = Vector256.LoadUnsafe(ref array, (UInt32)Vector256<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector256<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector256<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count * 32u)
            {
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 16u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 17u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 18u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 19u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 20u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 21u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 22u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 23u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 24u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 25u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 26u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 27u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 28u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 29u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 30u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 16u)
            {
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 8u)
            {
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 2u)
            {
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 1u)
            {
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                maxv = Vector256.Max(maxv, Vector256.LoadUnsafe(ref array, elementLength - (UInt32)Vector256<ELEMENT_T>.Count));

            return maxv.Max();
        }

        #endregion

        #region CalculateMaxByVector128

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMaxByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Max" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MaxNumber" method).

            System.Diagnostics.Debug.Assert(Vector128.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Half));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Single));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Double));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
#endif

            var maxv = Vector128.LoadUnsafe(ref array, (UInt32)Vector128<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector128<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector128<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count * 32u)
            {
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 16u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 17u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 18u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 19u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 20u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 21u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 22u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 23u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 24u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 25u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 26u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 27u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 28u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 29u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 30u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 16u)
            {
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 8u)
            {
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 2u)
            {
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 1u)
            {
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                maxv = Vector128.Max(maxv, Vector128.LoadUnsafe(ref array, elementLength - (UInt32)Vector128<ELEMENT_T>.Count));

            return maxv.Max();
        }

        #endregion

        #region CalculateMaxByNonVector

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMaxByNonVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(elementLength > 0);

            var max = Unsafe.AddByteOffset(ref array, UIntPtr.Zero);
            var byteOffset = (UIntPtr)Unsafe.SizeOf<ELEMENT_T>();
            var byteCount = (elementLength - 1u) * (UInt32)Unsafe.SizeOf<ELEMENT_T>();

            while (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u)
            {
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 17u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 18u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 19u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 20u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 21u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 22u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 23u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 24u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 25u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 26u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 27u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 28u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 29u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 30u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 31u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 32u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u)
            {
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u)
            {
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u)
            {
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u)
            {
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u;
            }

            if (byteCount > 0)
                max = ELEMENT_T.Max(max, Unsafe.AddByteOffset(ref array, byteOffset));

            return max;
        }

        #endregion

        #region NonVectorizedMin

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T NonVectorizedMin<ELEMENT_T>(ref ELEMENT_T array, UIntPtr byteLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(byteLength > 0);
            System.Diagnostics.Debug.Assert(byteLength % (UInt32)Unsafe.SizeOf<ELEMENT_T>() == 0);

            var min = array;
            var byteOffset = (UIntPtr)Unsafe.SizeOf<ELEMENT_T>();
            var byteCount = byteLength - (UInt32)Unsafe.SizeOf<ELEMENT_T>();
            while (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u)
            {
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 17u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 18u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 19u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 20u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 21u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 22u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 23u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 24u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 25u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 26u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 27u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 28u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 29u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 30u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 31u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 32u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u)
            {
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u)
            {
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u)
            {
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u)
            {
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u;
            }

            if (byteCount > 0)
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset));

            return min;
        }

        #endregion

        #region CalculateMinByVector512

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMinByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Min" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MinNumber" method).

            System.Diagnostics.Debug.Assert(Vector512.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Half));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Single));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Double));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
#endif

            var minv = Vector512.LoadUnsafe(ref array, (UInt32)Vector512<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector512<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector512<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count * 32u)
            {
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 16u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 17u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 18u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 19u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 20u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 21u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 22u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 23u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 24u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 25u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 26u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 27u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 28u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 29u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 30u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 16u)
            {
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 8u)
            {
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 2u)
            {
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 1u)
            {
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                minv = Vector512.Min(minv, Vector512.LoadUnsafe(ref array, elementLength - (UInt32)Vector512<ELEMENT_T>.Count));

            return minv.Min();
        }

        #endregion

        #region CalculateMinByVector256

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMinByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Min" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MinNumber" method).

            System.Diagnostics.Debug.Assert(Vector256.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Half));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Single));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Double));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
#endif

            var minv = Vector256.LoadUnsafe(ref array, (UInt32)Vector256<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector256<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector256<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count * 32u)
            {
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 16u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 17u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 18u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 19u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 20u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 21u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 22u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 23u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 24u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 25u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 26u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 27u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 28u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 29u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 30u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 16u)
            {
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 8u)
            {
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 2u)
            {
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 1u)
            {
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                minv = Vector256.Min(minv, Vector256.LoadUnsafe(ref array, elementLength - (UInt32)Vector256<ELEMENT_T>.Count));

            return minv.Min();
        }

        #endregion

        #region CalculateMinByVector128

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMinByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Min" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MinNumber" method).

            System.Diagnostics.Debug.Assert(Vector128.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Half));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Single));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Double));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
#endif

            var minv = Vector128.LoadUnsafe(ref array, (UInt32)Vector128<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector128<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector128<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count * 32u)
            {
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 16u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 17u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 18u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 19u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 20u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 21u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 22u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 23u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 24u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 25u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 26u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 27u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 28u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 29u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 30u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 16u)
            {
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 8u)
            {
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 2u)
            {
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 1u)
            {
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                minv = Vector128.Min(minv, Vector128.LoadUnsafe(ref array, elementLength - (UInt32)Vector128<ELEMENT_T>.Count));

            return minv.Min();
        }

        #endregion

        #region CalculateMinByNonVector

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMinByNonVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(elementLength > 0);

            var min = Unsafe.AddByteOffset(ref array, UIntPtr.Zero);
            var byteOffset = (UIntPtr)Unsafe.SizeOf<ELEMENT_T>();
            var byteCount = (elementLength - 1u) * (UInt32)Unsafe.SizeOf<ELEMENT_T>();

            while (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u)
            {
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 17u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 18u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 19u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 20u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 21u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 22u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 23u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 24u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 25u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 26u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 27u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 28u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 29u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 30u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 31u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 32u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u)
            {
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u)
            {
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u)
            {
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u)
            {
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u;
            }

            if (byteCount > 0)
                min = ELEMENT_T.Min(min, Unsafe.AddByteOffset(ref array, byteOffset));

            return min;
        }

        #endregion

        #region NonVectorizedMaxNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T NonVectorizedMaxNumber<ELEMENT_T>(ref ELEMENT_T array, UIntPtr byteLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(byteLength > 0);
            System.Diagnostics.Debug.Assert(byteLength % (UInt32)Unsafe.SizeOf<ELEMENT_T>() == 0);

            var max = array;
            var byteOffset = (UIntPtr)Unsafe.SizeOf<ELEMENT_T>();
            var byteCount = byteLength - (UInt32)Unsafe.SizeOf<ELEMENT_T>();
            while (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u)
            {
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 17u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 18u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 19u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 20u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 21u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 22u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 23u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 24u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 25u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 26u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 27u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 28u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 29u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 30u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 31u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 32u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u)
            {
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u)
            {
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u)
            {
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u)
            {
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u;
            }

            if (byteCount > 0)
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset));

            return max;
        }

        #endregion

        #region CalculateMaxNumberByVector512

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMaxNumberByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            System.Diagnostics.Debug.Assert(Vector512.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count);

            var maxv = Vector512.LoadUnsafe(ref array, (UInt32)Vector512<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector512<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector512<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count * 32u)
            {
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 16u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 17u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 18u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 19u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 20u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 21u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 22u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 23u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 24u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 25u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 26u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 27u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 28u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 29u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 30u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 16u)
            {
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 8u)
            {
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 2u)
            {
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 1u)
            {
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                maxv = Vector512.MaxNumber(maxv, Vector512.LoadUnsafe(ref array, elementLength - (UInt32)Vector512<ELEMENT_T>.Count));

            return maxv.MaxNumber();
        }
#endif

        #endregion

        #region CalculateMaxNumberByVector256

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMaxNumberByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            System.Diagnostics.Debug.Assert(Vector256.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count);

            var maxv = Vector256.LoadUnsafe(ref array, (UInt32)Vector256<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector256<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector256<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count * 32u)
            {
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 16u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 17u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 18u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 19u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 20u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 21u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 22u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 23u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 24u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 25u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 26u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 27u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 28u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 29u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 30u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 16u)
            {
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 8u)
            {
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 2u)
            {
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 1u)
            {
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                maxv = Vector256.MaxNumber(maxv, Vector256.LoadUnsafe(ref array, elementLength - (UInt32)Vector256<ELEMENT_T>.Count));

            return maxv.MaxNumber();
        }
#endif

        #endregion

        #region CalculateMaxNumberByVector128

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMaxNumberByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            System.Diagnostics.Debug.Assert(Vector128.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count);

            var maxv = Vector128.LoadUnsafe(ref array, (UInt32)Vector128<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector128<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector128<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count * 32u)
            {
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 16u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 17u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 18u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 19u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 20u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 21u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 22u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 23u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 24u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 25u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 26u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 27u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 28u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 29u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 30u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 16u)
            {
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 8u)
            {
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 2u)
            {
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 1u)
            {
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                maxv = Vector128.MaxNumber(maxv, Vector128.LoadUnsafe(ref array, elementLength - (UInt32)Vector128<ELEMENT_T>.Count));

            return maxv.MaxNumber();
        }
#endif

        #endregion

        #region CalculateMaxNumberByNonVector

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMaxNumberByNonVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(elementLength > 0);

            var max = Unsafe.AddByteOffset(ref array, UIntPtr.Zero);
            var byteOffset = (UIntPtr)Unsafe.SizeOf<ELEMENT_T>();
            var byteCount = (elementLength - 1u) * (UInt32)Unsafe.SizeOf<ELEMENT_T>();

            while (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u)
            {
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 17u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 18u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 19u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 20u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 21u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 22u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 23u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 24u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 25u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 26u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 27u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 28u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 29u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 30u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 31u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 32u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u)
            {
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u)
            {
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u)
            {
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u)
            {
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u;
            }

            if (byteCount > 0)
                max = ELEMENT_T.MaxNumber(max, Unsafe.AddByteOffset(ref array, byteOffset));

            return max;
        }
#endif

        #endregion

        #region NonVectorizedMinNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T NonVectorizedMinNumber<ELEMENT_T>(ref ELEMENT_T array, UIntPtr byteLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(byteLength > 0);
            System.Diagnostics.Debug.Assert(byteLength % (UInt32)Unsafe.SizeOf<ELEMENT_T>() == 0);

            var min = array;
            var byteOffset = (UIntPtr)Unsafe.SizeOf<ELEMENT_T>();
            var byteCount = byteLength - (UInt32)Unsafe.SizeOf<ELEMENT_T>();
            while (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u)
            {
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 17u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 18u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 19u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 20u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 21u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 22u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 23u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 24u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 25u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 26u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 27u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 28u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 29u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 30u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 31u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 32u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u)
            {
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u)
            {
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u)
            {
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u)
            {
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u;
            }

            if (byteCount > 0)
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset));

            return min;
        }

        #endregion

        #region CalculateMinNumberByVector512

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMinNumberByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            System.Diagnostics.Debug.Assert(Vector512.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count);

            var minv = Vector512.LoadUnsafe(ref array, (UInt32)Vector512<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector512<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector512<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count * 32u)
            {
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 16u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 17u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 18u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 19u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 20u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 21u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 22u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 23u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 24u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 25u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 26u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 27u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 28u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 29u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 30u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 16u)
            {
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 8u)
            {
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 2u)
            {
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector512<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 1u)
            {
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                minv = Vector512.MinNumber(minv, Vector512.LoadUnsafe(ref array, elementLength - (UInt32)Vector512<ELEMENT_T>.Count));

            return minv.MinNumber();
        }
#endif

        #endregion

        #region CalculateMinNumberByVector256

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMinNumberByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            System.Diagnostics.Debug.Assert(Vector256.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count);

            var minv = Vector256.LoadUnsafe(ref array, (UInt32)Vector256<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector256<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector256<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count * 32u)
            {
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 16u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 17u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 18u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 19u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 20u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 21u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 22u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 23u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 24u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 25u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 26u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 27u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 28u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 29u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 30u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 16u)
            {
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 8u)
            {
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 2u)
            {
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector256<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 1u)
            {
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                minv = Vector256.MinNumber(minv, Vector256.LoadUnsafe(ref array, elementLength - (UInt32)Vector256<ELEMENT_T>.Count));

            return minv.MinNumber();
        }
#endif

        #endregion

        #region CalculateMinNumberByVector128

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMinNumberByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            System.Diagnostics.Debug.Assert(Vector128.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count);

            var minv = Vector128.LoadUnsafe(ref array, (UInt32)Vector128<ELEMENT_T>.Count * 0u);
            var offset = (UIntPtr)Vector128<ELEMENT_T>.Count * 1u;
            var count = elementLength - (UInt32)Vector128<ELEMENT_T>.Count * 1u;

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count * 32u)
            {
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 16u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 17u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 18u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 19u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 20u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 21u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 22u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 23u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 24u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 25u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 26u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 27u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 28u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 29u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 30u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 31u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 16u)
            {
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 8u)
            {
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 4u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 2u)
            {
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u));
                offset += (UInt32)Vector128<ELEMENT_T>.Count * 2u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 2u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 1u)
            {
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u));
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 1u;
            }

            if (count > 0)
                minv = Vector128.MinNumber(minv, Vector128.LoadUnsafe(ref array, elementLength - (UInt32)Vector128<ELEMENT_T>.Count));

            return minv.MinNumber();
        }
#endif

        #endregion

        #region CalculateMinNumberByNonVector

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateMinNumberByNonVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(elementLength > 0);

            var min = Unsafe.AddByteOffset(ref array, UIntPtr.Zero);
            var byteOffset = (UIntPtr)Unsafe.SizeOf<ELEMENT_T>();
            var byteCount = (elementLength - 1u) * (UInt32)Unsafe.SizeOf<ELEMENT_T>();

            while (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u)
            {
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 17u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 18u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 19u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 20u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 21u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 22u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 23u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 24u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 25u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 26u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 27u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 28u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 29u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 30u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 31u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 32u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 32u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u)
            {
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 9u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 10u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 11u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 12u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 13u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 14u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 15u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 16u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 16u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u)
            {
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 5u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 6u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 7u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 8u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 8u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u)
            {
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 3u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 4u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 4u;
            }

            if (byteCount >= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u)
            {
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 0u));
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset + (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 1u));
                byteOffset += (UIntPtr)Unsafe.SizeOf<ELEMENT_T>() * 2u;
                byteCount -= (UInt32)Unsafe.SizeOf<ELEMENT_T>() * 2u;
            }

            if (byteCount > 0)
                min = ELEMENT_T.MinNumber(min, Unsafe.AddByteOffset(ref array, byteOffset));

            return min;
        }
#endif

        #endregion

        #region CalculateSumOfSignedIntegerByVector512

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumOfSignedIntegerByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength, ELEMENT_T overflowTestMask)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector512.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 4u);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(Int32) || typeof(ELEMENT_T) == typeof(Int64));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Int32) || (Int32)(Object)overflowTestMask == unchecked((Int32)~(~0u >> 1)));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Int64) || (Int64)(Object)overflowTestMask == unchecked((Int64)~(~0ul >> 1)));

            var sumv = Vector512<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var overflowTestVector = Vector512.Create(overflowTestMask);
            var count = elementLength;

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count * 32u)
            {
                var v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 16u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 17u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 18u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 19u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 20u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 21u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 22u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 23u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 24u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 25u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 26u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 27u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 28u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 29u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 30u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 31u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector512<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 16u)
            {
                var v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector512<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 8u)
            {
                var v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector512<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                var v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector512<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count)
            {
                var v = Vector512.LoadUnsafe(ref array, offset);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                if ((overflowTracking & overflowTestVector) != Vector512<ELEMENT_T>.Zero)
                    throw new OverflowException();

                sumv = sumv2;
                offset += (UInt32)Vector512<ELEMENT_T>.Count;
                count -= (UInt32)Vector512<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector512<ELEMENT_T>.Count; index++)
            {
                checked
                {
                    sum += sumv[index];
                }
            }

            return sum;
        }

        #endregion

        #region CalculateSumOfSignedIntegerByVector256

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumOfSignedIntegerByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength, ELEMENT_T overflowTestMask)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector256.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 4u);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(Int32) || typeof(ELEMENT_T) == typeof(Int64));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Int32) || (Int32)(Object)overflowTestMask == unchecked((Int32)~(~0u >> 1)));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Int64) || (Int64)(Object)overflowTestMask == unchecked((Int64)~(~0ul >> 1)));

            var sumv = Vector256<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var overflowTestVector = Vector256.Create(overflowTestMask);
            var count = elementLength;

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count * 32u)
            {
                var v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 16u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 17u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 18u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 19u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 20u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 21u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 22u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 23u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 24u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 25u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 26u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 27u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 28u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 29u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 30u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 31u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector256<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 16u)
            {
                var v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector256<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 8u)
            {
                var v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector256<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                var v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector256<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count)
            {
                var v = Vector256.LoadUnsafe(ref array, offset);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                if ((overflowTracking & overflowTestVector) != Vector256<ELEMENT_T>.Zero)
                    throw new OverflowException();

                sumv = sumv2;
                offset += (UInt32)Vector256<ELEMENT_T>.Count;
                count -= (UInt32)Vector256<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector256<ELEMENT_T>.Count; index++)
            {
                checked
                {
                    sum += sumv[index];
                }
            }

            return sum;
        }

        #endregion

        #region CalculateSumOfSignedIntegerByVector128

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumOfSignedIntegerByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength, ELEMENT_T overflowTestMask)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector128.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 4u);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(Int32) || typeof(ELEMENT_T) == typeof(Int64));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Int32) || (Int32)(Object)overflowTestMask == unchecked((Int32)~(~0u >> 1)));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(Int64) || (Int64)(Object)overflowTestMask == unchecked((Int64)~(~0ul >> 1)));

            var sumv = Vector128<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var overflowTestVector = Vector128.Create(overflowTestMask);
            var count = elementLength;

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count * 32u)
            {
                var v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 16u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 17u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 18u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 19u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 20u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 21u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 22u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 23u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 24u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 25u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 26u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 27u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 28u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 29u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 30u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 31u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector128<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 16u)
            {
                var v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector128<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 8u)
            {
                var v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector128<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                var v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv2 ^ sumv) & (sumv2 ^ v);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv ^ sumv2) & (sumv ^ v);

                if ((overflowTracking & overflowTestVector) != Vector128<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count)
            {
                var v = Vector128.LoadUnsafe(ref array, offset);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv2 ^ sumv) & (sumv2 ^ v);

                if ((overflowTracking & overflowTestVector) != Vector128<ELEMENT_T>.Zero)
                    throw new OverflowException();

                sumv = sumv2;
                offset += (UInt32)Vector128<ELEMENT_T>.Count;
                count -= (UInt32)Vector128<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector128<ELEMENT_T>.Count; index++)
            {
                checked
                {
                    sum += sumv[index];
                }
            }

            return sum;
        }

        #endregion

        #region CalculateSumOfUnsignedIntegerByVector512

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumOfUnsignedIntegerByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength, ELEMENT_T overflowTestMask)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector512.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 4u);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(UInt32) || typeof(ELEMENT_T) == typeof(UInt64));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(UInt32) || (UInt32)(Object)overflowTestMask == ~(~0u >> 1));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(UInt64) || (UInt64)(Object)overflowTestMask == ~(~0ul >> 1));

            var sumv = Vector512<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var overflowTestVector = Vector512.Create(overflowTestMask);
            var count = elementLength;

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count * 32u)
            {
                var v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 16u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 17u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 18u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 19u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 20u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 21u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 22u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 23u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 24u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 25u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 26u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 27u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 28u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 29u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 30u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 31u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector512<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 16u)
            {
                var v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector512<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 8u)
            {
                var v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector512<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                var v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                v = Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector512.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector512<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count)
            {
                var v = Vector512.LoadUnsafe(ref array, offset);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector512.AndNot(sumv | v, sumv2);

                if ((overflowTracking & overflowTestVector) != Vector512<ELEMENT_T>.Zero)
                    throw new OverflowException();

                sumv = sumv2;
                offset += (UInt32)Vector512<ELEMENT_T>.Count;
                count -= (UInt32)Vector512<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector512<ELEMENT_T>.Count; index++)
            {
                checked
                {
                    sum += sumv[index];
                }
            }

            return sum;
        }

        #endregion

        #region CalculateSumOfUnsignedIntegerByVector256

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumOfUnsignedIntegerByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength, ELEMENT_T overflowTestMask)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector256.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 4u);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(UInt32) || typeof(ELEMENT_T) == typeof(UInt64));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(UInt32) || (UInt32)(Object)overflowTestMask == ~(~0u >> 1));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(UInt64) || (UInt64)(Object)overflowTestMask == ~(~0ul >> 1));

            var sumv = Vector256<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var overflowTestVector = Vector256.Create(overflowTestMask);
            var count = elementLength;

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count * 32u)
            {
                var v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 16u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 17u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 18u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 19u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 20u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 21u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 22u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 23u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 24u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 25u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 26u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 27u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 28u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 29u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 30u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 31u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector256<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 16u)
            {
                var v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector256<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 8u)
            {
                var v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector256<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                var v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                v = Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector256.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector256<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count)
            {
                var v = Vector256.LoadUnsafe(ref array, offset);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector256.AndNot(sumv | v, sumv2);

                if ((overflowTracking & overflowTestVector) != Vector256<ELEMENT_T>.Zero)
                    throw new OverflowException();

                sumv = sumv2;
                offset += (UInt32)Vector256<ELEMENT_T>.Count;
                count -= (UInt32)Vector256<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector256<ELEMENT_T>.Count; index++)
            {
                checked
                {
                    sum += sumv[index];
                }
            }

            return sum;
        }

        #endregion

        #region CalculateSumOfUnsignedIntegerByVector128

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumOfUnsignedIntegerByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength, ELEMENT_T overflowTestMask)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector128.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 4u);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(UInt32) || typeof(ELEMENT_T) == typeof(UInt64));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(UInt32) || (UInt32)(Object)overflowTestMask == ~(~0u >> 1));
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) != typeof(UInt64) || (UInt64)(Object)overflowTestMask == ~(~0ul >> 1));

            var sumv = Vector128<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var overflowTestVector = Vector128.Create(overflowTestMask);
            var count = elementLength;

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count * 32u)
            {
                var v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 16u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 17u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 18u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 19u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 20u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 21u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 22u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 23u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 24u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 25u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 26u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 27u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 28u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 29u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 30u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 31u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector128<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 16u)
            {
                var v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector128<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 8u)
            {
                var v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector128<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                var v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0u);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2u);
                sumv2 = sumv + v;
                overflowTracking |= (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                v = Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3u);
                sumv = sumv2 + v;
                overflowTracking |= (sumv2 & v) | Vector128.AndNot(sumv2 | v, sumv);

                if ((overflowTracking & overflowTestVector) != Vector128<ELEMENT_T>.Zero)
                    throw new OverflowException();

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count)
            {
                var v = Vector128.LoadUnsafe(ref array, offset);
                var sumv2 = sumv + v;
                var overflowTracking = (sumv & v) | Vector128.AndNot(sumv | v, sumv2);

                if ((overflowTracking & overflowTestVector) != Vector128<ELEMENT_T>.Zero)
                    throw new OverflowException();

                sumv = sumv2;
                offset += (UInt32)Vector128<ELEMENT_T>.Count;
                count -= (UInt32)Vector128<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector128<ELEMENT_T>.Count; index++)
            {
                checked
                {
                    sum += sumv[index];
                }
            }

            return sum;
        }

        #endregion

        #region CalculateSumOfIeee754FloatingNumberByVector512

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumOfIeee754FloatingNumberByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector512.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(Half) || typeof(ELEMENT_T) == typeof(Single) || typeof(ELEMENT_T) == typeof(Double) || typeof(ELEMENT_T) == typeof(System.Runtime.InteropServices.NFloat);

            var sumv = Vector512<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var count = elementLength;

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count * 32u)
            {
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 16);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 17);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 18);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 19);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 20);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 21);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 22);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 23);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 24);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 25);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 26);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 27);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 28);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 29);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 30);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 31);

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 16u)
            {
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15);

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 8u)
            {
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7);

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3);

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count)
            {
                sumv += Vector512.LoadUnsafe(ref array, offset);
                offset += (UInt32)Vector512<ELEMENT_T>.Count;
                count -= (UInt32)Vector512<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector512<ELEMENT_T>.Count; index++)
                sum += sumv[index];

            return sum;
        }

        #endregion

        #region CalculateSumOfIeee754FloatingNumberByVector256

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumOfIeee754FloatingNumberByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector256.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(Half) || typeof(ELEMENT_T) == typeof(Single) || typeof(ELEMENT_T) == typeof(Double) || typeof(ELEMENT_T) == typeof(System.Runtime.InteropServices.NFloat);

            var sumv = Vector256<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var count = elementLength;

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count * 32u)
            {
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 16);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 17);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 18);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 19);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 20);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 21);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 22);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 23);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 24);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 25);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 26);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 27);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 28);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 29);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 30);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 31);

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 16u)
            {
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15);

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 8u)
            {
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7);

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3);

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count)
            {
                sumv += Vector256.LoadUnsafe(ref array, offset);
                offset += (UInt32)Vector256<ELEMENT_T>.Count;
                count -= (UInt32)Vector256<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector256<ELEMENT_T>.Count; index++)
                sum += sumv[index];

            return sum;
        }

        #endregion

        #region CalculateSumOfIeee754FloatingNumberByVector128

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateSumOfIeee754FloatingNumberByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector128.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(Half) || typeof(ELEMENT_T) == typeof(Single) || typeof(ELEMENT_T) == typeof(Double) || typeof(ELEMENT_T) == typeof(System.Runtime.InteropServices.NFloat);

            var sumv = Vector128<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var count = elementLength;

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count * 32u)
            {
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 16);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 17);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 18);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 19);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 20);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 21);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 22);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 23);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 24);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 25);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 26);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 27);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 28);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 29);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 30);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 31);

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 16u)
            {
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15);

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 8u)
            {
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7);

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3);

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count)
            {
                sumv += Vector128.LoadUnsafe(ref array, offset);
                offset += (UInt32)Vector128<ELEMENT_T>.Count;
                count -= (UInt32)Vector128<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector128<ELEMENT_T>.Count; index++)
                sum += sumv[index];

            return sum;
        }

        #endregion

        #region CalculateUncheckedSumByVector512

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateUncheckedSumByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector512.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector512<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count * 4u);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(Int32) || typeof(ELEMENT_T) == typeof(Int64) || typeof(ELEMENT_T) == typeof(UInt32) || typeof(ELEMENT_T) == typeof(UInt64));

            var sumv = Vector512<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var count = elementLength;

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count * 32u)
            {
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 16);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 17);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 18);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 19);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 20);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 21);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 22);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 23);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 24);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 25);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 26);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 27);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 28);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 29);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 30);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 31);

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 16u)
            {
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 8);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 9);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 10);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 11);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 12);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 13);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 14);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 15);

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 8u)
            {
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 4);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 5);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 6);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 7);

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector512<ELEMENT_T>.Count * 4u)
            {
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 0);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 1);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 2);
                sumv += Vector512.LoadUnsafe(ref array, offset + (UInt32)Vector512<ELEMENT_T>.Count * 3);

                offset += (UInt32)Vector512<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector512<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector512<ELEMENT_T>.Count)
            {
                sumv += Vector512.LoadUnsafe(ref array, offset);
                offset += (UInt32)Vector512<ELEMENT_T>.Count;
                count -= (UInt32)Vector512<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector512<ELEMENT_T>.Count; index++)
                sum += sumv[index];

            return sum;
        }

        #endregion

        #region CalculateUncheckedSumByVector256

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateUncheckedSumByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector256.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector256<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count * 4u);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(Int32) || typeof(ELEMENT_T) == typeof(Int64) || typeof(ELEMENT_T) == typeof(UInt32) || typeof(ELEMENT_T) == typeof(UInt64));

            var sumv = Vector256<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var count = elementLength;

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count * 32u)
            {
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 16);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 17);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 18);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 19);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 20);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 21);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 22);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 23);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 24);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 25);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 26);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 27);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 28);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 29);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 30);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 31);

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 16u)
            {
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 8);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 9);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 10);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 11);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 12);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 13);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 14);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 15);

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 8u)
            {
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 4);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 5);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 6);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 7);

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector256<ELEMENT_T>.Count * 4u)
            {
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 0);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 1);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 2);
                sumv += Vector256.LoadUnsafe(ref array, offset + (UInt32)Vector256<ELEMENT_T>.Count * 3);

                offset += (UInt32)Vector256<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector256<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector256<ELEMENT_T>.Count)
            {
                sumv += Vector256.LoadUnsafe(ref array, offset);
                offset += (UInt32)Vector256<ELEMENT_T>.Count;
                count -= (UInt32)Vector256<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector256<ELEMENT_T>.Count; index++)
                sum += sumv[index];

            return sum;
        }

        #endregion

        #region CalculateUncheckedSumByVector128

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T CalculateUncheckedSumByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumberBase<ELEMENT_T>
        {
            System.Diagnostics.Debug.Assert(Vector128.IsHardwareAccelerated == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            System.Diagnostics.Debug.Assert(Vector128<ELEMENT_T>.Count >= 2);
            System.Diagnostics.Debug.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count * 4u);
            System.Diagnostics.Debug.Assert(typeof(ELEMENT_T) == typeof(Int32) || typeof(ELEMENT_T) == typeof(Int64) || typeof(ELEMENT_T) == typeof(UInt32) || typeof(ELEMENT_T) == typeof(UInt64));

            var sumv = Vector128<ELEMENT_T>.Zero;
            var offset = UIntPtr.Zero;
            var count = elementLength;

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count * 32u)
            {
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 16);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 17);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 18);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 19);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 20);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 21);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 22);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 23);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 24);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 25);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 26);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 27);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 28);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 29);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 30);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 31);

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 32u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 32u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 16u)
            {
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 8);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 9);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 10);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 11);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 12);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 13);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 14);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 15);

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 16u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 16u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 8u)
            {
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 4);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 5);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 6);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 7);

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 8u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 8u;
            }

            if (count >= (UInt32)Vector128<ELEMENT_T>.Count * 4u)
            {
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 0);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 1);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 2);
                sumv += Vector128.LoadUnsafe(ref array, offset + (UInt32)Vector128<ELEMENT_T>.Count * 3);

                offset += (UInt32)Vector128<ELEMENT_T>.Count * 4u;
                count -= (UInt32)Vector128<ELEMENT_T>.Count * 4u;
            }

            while (count >= (UInt32)Vector128<ELEMENT_T>.Count)
            {
                sumv += Vector128.LoadUnsafe(ref array, offset);
                offset += (UInt32)Vector128<ELEMENT_T>.Count;
                count -= (UInt32)Vector128<ELEMENT_T>.Count;
            }

            var sum = ELEMENT_T.Zero;
            for (var index = 0; index < Vector128<ELEMENT_T>.Count; index++)
                sum += sumv[index];

            return sum;
        }

        #endregion
    }
}
