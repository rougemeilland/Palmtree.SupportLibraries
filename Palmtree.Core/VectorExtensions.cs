using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Palmtree
{
    internal static class VectorExtensions
    {
        #region Max

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Max<ELEMENT_T>(this Vector512<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector512<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector512<ELEMENT_T>.Count == 2)
                return ELEMENT_T.Max(vector[0], vector[1]);
#if NET9_0_OR_GREATER
            return
                Vector256.IsHardwareAccelerated
                ? Vector256.Max(vector.GetLower(), vector.GetUpper()).Max()
                : ELEMENT_T.Max(vector.GetLower().Max(), vector.GetUpper().Max());
#else
            return
                Vector256.IsHardwareAccelerated && !IsIeee754FloatingNumberType<ELEMENT_T>()
                ? Vector256.Max(vector.GetLower(), vector.GetUpper()).Max()
                : ELEMENT_T.Max(vector.GetLower().Max(), vector.GetUpper().Max());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Max<ELEMENT_T>(this Vector256<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector256<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector256<ELEMENT_T>.Count == 2)
                return ELEMENT_T.Max(vector[0], vector[1]);
#if NET9_0_OR_GREATER
            return
                Vector128.IsHardwareAccelerated
                ? Vector128.Max(vector.GetLower(), vector.GetUpper()).Max()
                : ELEMENT_T.Max(vector.GetLower().Max(), vector.GetUpper().Max());
#else
            return
                Vector128.IsHardwareAccelerated && !IsIeee754FloatingNumberType<ELEMENT_T>()
                ? Vector128.Max(vector.GetLower(), vector.GetUpper()).Max()
                : ELEMENT_T.Max(vector.GetLower().Max(), vector.GetUpper().Max());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Max<ELEMENT_T>(this Vector128<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector128<ELEMENT_T>.Count == 1)
                return vector[0];

            Validation.Assert(Vector128<ELEMENT_T>.Count > 1);
            Validation.Assert(Vector128<ELEMENT_T>.Count <= 16);

            var max = ELEMENT_T.Max(vector[0], vector[1]);
            if (Vector128<ELEMENT_T>.Count > 2)
                max = ELEMENT_T.Max(max, vector[2]);
            if (Vector128<ELEMENT_T>.Count > 2)
                max = ELEMENT_T.Max(max, vector[2]);
            if (Vector128<ELEMENT_T>.Count > 3)
                max = ELEMENT_T.Max(max, vector[3]);
            if (Vector128<ELEMENT_T>.Count > 4)
                max = ELEMENT_T.Max(max, vector[4]);
            if (Vector128<ELEMENT_T>.Count > 5)
                max = ELEMENT_T.Max(max, vector[5]);
            if (Vector128<ELEMENT_T>.Count > 6)
                max = ELEMENT_T.Max(max, vector[6]);
            if (Vector128<ELEMENT_T>.Count > 7)
                max = ELEMENT_T.Max(max, vector[7]);
            if (Vector128<ELEMENT_T>.Count > 8)
                max = ELEMENT_T.Max(max, vector[8]);
            if (Vector128<ELEMENT_T>.Count > 9)
                max = ELEMENT_T.Max(max, vector[9]);
            if (Vector128<ELEMENT_T>.Count > 10)
                max = ELEMENT_T.Max(max, vector[10]);
            if (Vector128<ELEMENT_T>.Count > 11)
                max = ELEMENT_T.Max(max, vector[11]);
            if (Vector128<ELEMENT_T>.Count > 12)
                max = ELEMENT_T.Max(max, vector[12]);
            if (Vector128<ELEMENT_T>.Count > 13)
                max = ELEMENT_T.Max(max, vector[13]);
            if (Vector128<ELEMENT_T>.Count > 14)
                max = ELEMENT_T.Max(max, vector[14]);
            if (Vector128<ELEMENT_T>.Count > 15)
                max = ELEMENT_T.Max(max, vector[15]);
            return max;
        }

        #endregion

        #region Min

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Min<ELEMENT_T>(this Vector512<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector512<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector512<ELEMENT_T>.Count == 2)
                return ELEMENT_T.Min(vector[0], vector[1]);
#if NET9_0_OR_GREATER
            return
                Vector256.IsHardwareAccelerated
                ? Vector256.Min(vector.GetLower(), vector.GetUpper()).Min()
                : ELEMENT_T.Min(vector.GetLower().Min(), vector.GetUpper().Min());
#else
            return
                Vector256.IsHardwareAccelerated && !IsIeee754FloatingNumberType<ELEMENT_T>()
                ? Vector256.Min(vector.GetLower(), vector.GetUpper()).Min()
                : ELEMENT_T.Min(vector.GetLower().Min(), vector.GetUpper().Min());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Min<ELEMENT_T>(this Vector256<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector256<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector256<ELEMENT_T>.Count == 2)
                return ELEMENT_T.Min(vector[0], vector[1]);
#if NET9_0_OR_GREATER
            return
                Vector128.IsHardwareAccelerated
                ? Vector128.Min(vector.GetLower(), vector.GetUpper()).Min()
                : ELEMENT_T.Min(vector.GetLower().Min(), vector.GetUpper().Min());
#else
            return
                Vector128.IsHardwareAccelerated && !IsIeee754FloatingNumberType<ELEMENT_T>()
                ? Vector128.Min(vector.GetLower(), vector.GetUpper()).Min()
                : ELEMENT_T.Min(vector.GetLower().Min(), vector.GetUpper().Min());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Min<ELEMENT_T>(this Vector128<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector128<ELEMENT_T>.Count == 1)
                return vector[0];

            Validation.Assert(Vector128<ELEMENT_T>.Count > 1);
            Validation.Assert(Vector128<ELEMENT_T>.Count <= 16);

            var min = ELEMENT_T.Min(vector[0], vector[1]);
            if (Vector128<ELEMENT_T>.Count > 2)
                min = ELEMENT_T.Min(min, vector[2]);
            if (Vector128<ELEMENT_T>.Count > 2)
                min = ELEMENT_T.Min(min, vector[2]);
            if (Vector128<ELEMENT_T>.Count > 3)
                min = ELEMENT_T.Min(min, vector[3]);
            if (Vector128<ELEMENT_T>.Count > 4)
                min = ELEMENT_T.Min(min, vector[4]);
            if (Vector128<ELEMENT_T>.Count > 5)
                min = ELEMENT_T.Min(min, vector[5]);
            if (Vector128<ELEMENT_T>.Count > 6)
                min = ELEMENT_T.Min(min, vector[6]);
            if (Vector128<ELEMENT_T>.Count > 7)
                min = ELEMENT_T.Min(min, vector[7]);
            if (Vector128<ELEMENT_T>.Count > 8)
                min = ELEMENT_T.Min(min, vector[8]);
            if (Vector128<ELEMENT_T>.Count > 9)
                min = ELEMENT_T.Min(min, vector[9]);
            if (Vector128<ELEMENT_T>.Count > 10)
                min = ELEMENT_T.Min(min, vector[10]);
            if (Vector128<ELEMENT_T>.Count > 11)
                min = ELEMENT_T.Min(min, vector[11]);
            if (Vector128<ELEMENT_T>.Count > 12)
                min = ELEMENT_T.Min(min, vector[12]);
            if (Vector128<ELEMENT_T>.Count > 13)
                min = ELEMENT_T.Min(min, vector[13]);
            if (Vector128<ELEMENT_T>.Count > 14)
                min = ELEMENT_T.Min(min, vector[14]);
            if (Vector128<ELEMENT_T>.Count > 15)
                min = ELEMENT_T.Min(min, vector[15]);
            return min;
        }

        #endregion

        #region MaxNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MaxNumber<ELEMENT_T>(this Vector512<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector512<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector512<ELEMENT_T>.Count == 2)
                return ELEMENT_T.MaxNumber(vector[0], vector[1]);
#if NET9_0_OR_GREATER
            return
                Vector256.IsHardwareAccelerated
                ? Vector256.MaxNumber(vector.GetLower(), vector.GetUpper()).MaxNumber()
                : ELEMENT_T.MaxNumber(vector.GetLower().MaxNumber(), vector.GetUpper().MaxNumber());
#else
            return
                Vector256.IsHardwareAccelerated && !IsIeee754FloatingNumberType<ELEMENT_T>()
                ? Vector256.Max(vector.GetLower(), vector.GetUpper()).MaxNumber()
                : ELEMENT_T.MaxNumber(vector.GetLower().MaxNumber(), vector.GetUpper().MaxNumber());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MaxNumber<ELEMENT_T>(this Vector256<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector256<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector256<ELEMENT_T>.Count == 2)
                return ELEMENT_T.MaxNumber(vector[0], vector[1]);
#if NET9_0_OR_GREATER
            return
                Vector128.IsHardwareAccelerated
                ? Vector128.MaxNumber(vector.GetLower(), vector.GetUpper()).MaxNumber()
                : ELEMENT_T.MaxNumber(vector.GetLower().MaxNumber(), vector.GetUpper().MaxNumber());
#else
            return
                Vector128.IsHardwareAccelerated && !IsIeee754FloatingNumberType<ELEMENT_T>()
                ? Vector128.Max(vector.GetLower(), vector.GetUpper()).MaxNumber()
                : ELEMENT_T.MaxNumber(vector.GetLower().MaxNumber(), vector.GetUpper().MaxNumber());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MaxNumber<ELEMENT_T>(this Vector128<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector128<ELEMENT_T>.Count == 1)
                return vector[0];

            Validation.Assert(Vector128<ELEMENT_T>.Count > 1);
            Validation.Assert(Vector128<ELEMENT_T>.Count <= 16);

            var max = ELEMENT_T.MaxNumber(vector[0], vector[1]);
            if (Vector128<ELEMENT_T>.Count > 2)
                max = ELEMENT_T.MaxNumber(max, vector[2]);
            if (Vector128<ELEMENT_T>.Count > 2)
                max = ELEMENT_T.MaxNumber(max, vector[2]);
            if (Vector128<ELEMENT_T>.Count > 3)
                max = ELEMENT_T.MaxNumber(max, vector[3]);
            if (Vector128<ELEMENT_T>.Count > 4)
                max = ELEMENT_T.MaxNumber(max, vector[4]);
            if (Vector128<ELEMENT_T>.Count > 5)
                max = ELEMENT_T.MaxNumber(max, vector[5]);
            if (Vector128<ELEMENT_T>.Count > 6)
                max = ELEMENT_T.MaxNumber(max, vector[6]);
            if (Vector128<ELEMENT_T>.Count > 7)
                max = ELEMENT_T.MaxNumber(max, vector[7]);
            if (Vector128<ELEMENT_T>.Count > 8)
                max = ELEMENT_T.MaxNumber(max, vector[8]);
            if (Vector128<ELEMENT_T>.Count > 9)
                max = ELEMENT_T.MaxNumber(max, vector[9]);
            if (Vector128<ELEMENT_T>.Count > 10)
                max = ELEMENT_T.MaxNumber(max, vector[10]);
            if (Vector128<ELEMENT_T>.Count > 11)
                max = ELEMENT_T.MaxNumber(max, vector[11]);
            if (Vector128<ELEMENT_T>.Count > 12)
                max = ELEMENT_T.MaxNumber(max, vector[12]);
            if (Vector128<ELEMENT_T>.Count > 13)
                max = ELEMENT_T.MaxNumber(max, vector[13]);
            if (Vector128<ELEMENT_T>.Count > 14)
                max = ELEMENT_T.MaxNumber(max, vector[14]);
            if (Vector128<ELEMENT_T>.Count > 15)
                max = ELEMENT_T.MaxNumber(max, vector[15]);
            return max;
        }

        #endregion

        #region MinNumber

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MinNumber<ELEMENT_T>(this Vector512<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector512<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector512<ELEMENT_T>.Count == 2)
                return ELEMENT_T.MinNumber(vector[0], vector[1]);
#if NET9_0_OR_GREATER
            return
                Vector256.IsHardwareAccelerated
                ? Vector256.MinNumber(vector.GetLower(), vector.GetUpper()).MinNumber()
                : ELEMENT_T.MinNumber(vector.GetLower().MinNumber(), vector.GetUpper().MinNumber());
#else
            return
                Vector256.IsHardwareAccelerated && !IsIeee754FloatingNumberType<ELEMENT_T>()
                ? Vector256.Min(vector.GetLower(), vector.GetUpper()).MinNumber()
                : ELEMENT_T.MinNumber(vector.GetLower().MinNumber(), vector.GetUpper().MinNumber());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MinNumber<ELEMENT_T>(this Vector256<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector256<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector256<ELEMENT_T>.Count == 2)
                return ELEMENT_T.MinNumber(vector[0], vector[1]);
#if NET9_0_OR_GREATER
            return
                Vector128.IsHardwareAccelerated
                ? Vector128.MinNumber(vector.GetLower(), vector.GetUpper()).MinNumber()
                : ELEMENT_T.MinNumber(vector.GetLower().MinNumber(), vector.GetUpper().MinNumber());
#else
            return
                Vector128.IsHardwareAccelerated && !IsIeee754FloatingNumberType<ELEMENT_T>()
                ? Vector128.Min(vector.GetLower(), vector.GetUpper()).MinNumber()
                : ELEMENT_T.MinNumber(vector.GetLower().MinNumber(), vector.GetUpper().MinNumber());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MinNumber<ELEMENT_T>(this Vector128<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector128<ELEMENT_T>.Count == 1)
                return vector[0];

            Validation.Assert(Vector128<ELEMENT_T>.Count > 1);
            Validation.Assert(Vector128<ELEMENT_T>.Count <= 16);

            var min = ELEMENT_T.MinNumber(vector[0], vector[1]);
            if (Vector128<ELEMENT_T>.Count > 2)
                min = ELEMENT_T.MinNumber(min, vector[2]);
            if (Vector128<ELEMENT_T>.Count > 2)
                min = ELEMENT_T.MinNumber(min, vector[2]);
            if (Vector128<ELEMENT_T>.Count > 3)
                min = ELEMENT_T.MinNumber(min, vector[3]);
            if (Vector128<ELEMENT_T>.Count > 4)
                min = ELEMENT_T.MinNumber(min, vector[4]);
            if (Vector128<ELEMENT_T>.Count > 5)
                min = ELEMENT_T.MinNumber(min, vector[5]);
            if (Vector128<ELEMENT_T>.Count > 6)
                min = ELEMENT_T.MinNumber(min, vector[6]);
            if (Vector128<ELEMENT_T>.Count > 7)
                min = ELEMENT_T.MinNumber(min, vector[7]);
            if (Vector128<ELEMENT_T>.Count > 8)
                min = ELEMENT_T.MinNumber(min, vector[8]);
            if (Vector128<ELEMENT_T>.Count > 9)
                min = ELEMENT_T.MinNumber(min, vector[9]);
            if (Vector128<ELEMENT_T>.Count > 10)
                min = ELEMENT_T.MinNumber(min, vector[10]);
            if (Vector128<ELEMENT_T>.Count > 11)
                min = ELEMENT_T.MinNumber(min, vector[11]);
            if (Vector128<ELEMENT_T>.Count > 12)
                min = ELEMENT_T.MinNumber(min, vector[12]);
            if (Vector128<ELEMENT_T>.Count > 13)
                min = ELEMENT_T.MinNumber(min, vector[13]);
            if (Vector128<ELEMENT_T>.Count > 14)
                min = ELEMENT_T.MinNumber(min, vector[14]);
            if (Vector128<ELEMENT_T>.Count > 15)
                min = ELEMENT_T.MinNumber(min, vector[15]);
            return min;
        }

        #endregion

        #region IsIeee754FloatingNumberType

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static Boolean IsIeee754FloatingNumberType<VALUE_T>()
            => typeof(VALUE_T) == typeof(Half) || typeof(VALUE_T) == typeof(Single) || typeof(VALUE_T) == typeof(Double) || typeof(VALUE_T) == typeof(System.Runtime.InteropServices.NFloat);

        #endregion
    }
}
