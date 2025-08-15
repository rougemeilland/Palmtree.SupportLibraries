using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Experiment.CSharp.Library
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
            return Vector256.Max(vector.GetLower(), vector.GetUpper()).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Max<ELEMENT_T>(this Vector256<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector256<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector256<ELEMENT_T>.Count == 2)
                return ELEMENT_T.Max(vector[0], vector[1]);
            return Vector128.Max(vector.GetLower(), vector.GetUpper()).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Max<ELEMENT_T>(this Vector128<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector128<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector128<ELEMENT_T>.Count == 2)
                return ELEMENT_T.Max(vector[0], vector[1]);
            return Vector64.Max(vector.GetLower(), vector.GetUpper()).Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Max<ELEMENT_T>(this Vector64<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector64<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector64<ELEMENT_T>.Count == 2)
                return ELEMENT_T.Max(vector[0], vector[1]);
            var max = vector[0];
            for (var index = 1; index < Vector64<ELEMENT_T>.Count; ++index)
                max = ELEMENT_T.Max(max, vector[index]);
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
            return Vector256.Min(vector.GetLower(), vector.GetUpper()).Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Min<ELEMENT_T>(this Vector256<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector256<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector256<ELEMENT_T>.Count == 2)
                return ELEMENT_T.Min(vector[0], vector[1]);
            return Vector128.Min(vector.GetLower(), vector.GetUpper()).Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Min<ELEMENT_T>(this Vector128<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector128<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector128<ELEMENT_T>.Count == 2)
                return ELEMENT_T.Min(vector[0], vector[1]);
            return Vector64.Min(vector.GetLower(), vector.GetUpper()).Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T Min<ELEMENT_T>(this Vector64<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector64<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector64<ELEMENT_T>.Count == 2)
                return ELEMENT_T.Min(vector[0], vector[1]);
            var max = vector[0];
            for (var index = 1; index < Vector64<ELEMENT_T>.Count; ++index)
                max = ELEMENT_T.Min(max, vector[index]);
            return max;
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
            return Vector256.MaxNumber(vector.GetLower(), vector.GetUpper()).MaxNumber();
#else
            return ELEMENT_T.MaxNumber(vector.GetLower().MaxNumber(), vector.GetUpper().MaxNumber());
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
            return Vector128.MaxNumber(vector.GetLower(), vector.GetUpper()).MaxNumber();
#else
            return ELEMENT_T.MaxNumber(vector.GetLower().MaxNumber(), vector.GetUpper().MaxNumber());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MaxNumber<ELEMENT_T>(this Vector128<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector128<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector128<ELEMENT_T>.Count == 2)
                return ELEMENT_T.MaxNumber(vector[0], vector[1]);
#if NET9_0_OR_GREATER
            return Vector64.MaxNumber(vector.GetLower(), vector.GetUpper()).MaxNumber();
#else
            return ELEMENT_T.MaxNumber(vector.GetLower().MaxNumber(), vector.GetUpper().MaxNumber());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MaxNumber<ELEMENT_T>(this Vector64<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector64<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector64<ELEMENT_T>.Count == 2)
                return ELEMENT_T.MaxNumber(vector[0], vector[1]);
            var max = vector[0];
            for (var index = 1; index < Vector64<ELEMENT_T>.Count; ++index)
                max = ELEMENT_T.MaxNumber(max, vector[index]);
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
            return Vector256.MinNumber(vector.GetLower(), vector.GetUpper()).MinNumber();
#else
            return ELEMENT_T.MinNumber(vector.GetLower().MinNumber(), vector.GetUpper().MinNumber());
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
            return Vector128.MinNumber(vector.GetLower(), vector.GetUpper()).MinNumber();
#else
            return ELEMENT_T.MinNumber(vector.GetLower().MinNumber(), vector.GetUpper().MinNumber());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MinNumber<ELEMENT_T>(this Vector128<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector128<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector128<ELEMENT_T>.Count == 2)
                return ELEMENT_T.MinNumber(vector[0], vector[1]);
#if NET9_0_OR_GREATER
            return Vector64.MinNumber(vector.GetLower(), vector.GetUpper()).MinNumber();
#else
            return ELEMENT_T.MinNumber(vector.GetLower().MinNumber(), vector.GetUpper().MinNumber());
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static ELEMENT_T MinNumber<ELEMENT_T>(this Vector64<ELEMENT_T> vector)
            where ELEMENT_T : INumber<ELEMENT_T>
        {
            if (Vector64<ELEMENT_T>.Count == 1)
                return vector[0];
            if (Vector64<ELEMENT_T>.Count == 2)
                return ELEMENT_T.MinNumber(vector[0], vector[1]);
            var max = vector[0];
            for (var index = 1; index < Vector64<ELEMENT_T>.Count; ++index)
                max = ELEMENT_T.MinNumber(max, vector[index]);
            return max;
        }

        #endregion
    }
}
