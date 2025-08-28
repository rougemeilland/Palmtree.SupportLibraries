using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Palmtree
{
    public static partial class ArrayExtensions
    {
        #region MaxCoreByVector512

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxCoreByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Max" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MaxNumber" method).

            Validation.Assert(Vector512.IsHardwareAccelerated == true);
            Validation.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector512<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            Validation.Assert(typeof(ELEMENT_T) != typeof(Half));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Single));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Double));
            Validation.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
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

        #region MaxCoreByVector256

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxCoreByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Max" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MaxNumber" method).

            Validation.Assert(Vector256.IsHardwareAccelerated == true);
            Validation.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector256<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            Validation.Assert(typeof(ELEMENT_T) != typeof(Half));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Single));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Double));
            Validation.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
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

        #region MaxCoreByVector128

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxCoreByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Max" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MaxNumber" method).

            Validation.Assert(Vector128.IsHardwareAccelerated == true);
            Validation.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector128<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            Validation.Assert(typeof(ELEMENT_T) != typeof(Half));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Single));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Double));
            Validation.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
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

        #region MaxCoreByNonVector

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxCoreByNonVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            Validation.Assert(elementLength > 0);

            var max = array;
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

        #region MinCoreByVector512

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinCoreByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Min" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MinNumber" method).

            Validation.Assert(Vector512.IsHardwareAccelerated == true);
            Validation.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector512<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            Validation.Assert(typeof(ELEMENT_T) != typeof(Half));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Single));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Double));
            Validation.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
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

        #region MinCoreByVector256

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinCoreByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Min" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MinNumber" method).

            Validation.Assert(Vector256.IsHardwareAccelerated == true);
            Validation.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector256<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            Validation.Assert(typeof(ELEMENT_T) != typeof(Half));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Single));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Double));
            Validation.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
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

        #region MinCoreByVector128

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinCoreByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {
            // This method must not be called if the following conditions are true:
            //   1) The .NET runtime version is 8.0 or earlier, and
            //   2) "ELEMENT_T" is an IEEE 754-compliant floating-point type.
            //
            // The reason is that the behavior of the "Min" method in the Vector Operation class in .NET 8.0 or earlier is different from that in .NET 9.0 or later,
            // in that it ignores NaNs (its behavior is rather similar to that of the "MinNumber" method).

            Validation.Assert(Vector128.IsHardwareAccelerated == true);
            Validation.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector128<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count);
#if !NET9_0_OR_GREATER
            Validation.Assert(typeof(ELEMENT_T) != typeof(Half));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Single));
            Validation.Assert(typeof(ELEMENT_T) != typeof(Double));
            Validation.Assert(typeof(ELEMENT_T) != typeof(System.Runtime.InteropServices.NFloat));
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

        #region MinCoreByNonVector

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinCoreByNonVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            Validation.Assert(elementLength > 0);

            var min = array;
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

        #region MaxNumberCoreByVector512

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxNumberCoreByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            Validation.Assert(Vector512.IsHardwareAccelerated == true);
            Validation.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector512<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count);

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

        #region MaxNumberCoreByVector256

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxNumberCoreByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            Validation.Assert(Vector256.IsHardwareAccelerated == true);
            Validation.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector256<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count);

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

        #region MaxNumberCoreByVector128

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxNumberCoreByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            Validation.Assert(Vector128.IsHardwareAccelerated == true);
            Validation.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector128<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count);

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

        #region MaxNumberCoreByNonVector

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MaxNumberCoreByNonVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            Validation.Assert(elementLength > 0);

            var max = array;
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

        #endregion

        #region MinNumberCoreByVector512

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinNumberCoreByVector512<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            Validation.Assert(Vector512.IsHardwareAccelerated == true);
            Validation.Assert(Vector512<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector512<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector512<ELEMENT_T>.Count);

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

        #region MinNumberCoreByVector256

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinNumberCoreByVector256<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            Validation.Assert(Vector256.IsHardwareAccelerated == true);
            Validation.Assert(Vector256<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector256<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector256<ELEMENT_T>.Count);

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

        #region MinNumberCoreByVector128

#if NET9_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinNumberCoreByVector128<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
             where ELEMENT_T : INumber<ELEMENT_T>
        {

            Validation.Assert(Vector128.IsHardwareAccelerated == true);
            Validation.Assert(Vector128<ELEMENT_T>.IsSupported == true);
            Validation.Assert(Vector128<ELEMENT_T>.Count >= 2);
            Validation.Assert(elementLength >= (UInt32)Vector128<ELEMENT_T>.Count);

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

        #region MinNumberCoreByNonVector

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static ELEMENT_T MinNumberCoreByNonVector<ELEMENT_T>(ref ELEMENT_T array, UIntPtr elementLength)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            Validation.Assert(elementLength > 0);

            var min = array;
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

        #endregion
    }
}
