using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Experiment.CSharp.Library
{
    public static class TypeHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Boolean IsBitwiseEquatableQuickly<ELEMENT_T>()
        {
            if (typeof(ELEMENT_T) == typeof(Boolean))
                return true;
            if (typeof(ELEMENT_T) == typeof(Char))
                return true;
            if (typeof(ELEMENT_T) == typeof(System.Text.Rune))
                return true;
            if (typeof(ELEMENT_T) == typeof(SByte))
                return true;
            if (typeof(ELEMENT_T) == typeof(Byte))
                return true;
            if (typeof(ELEMENT_T) == typeof(Int16))
                return true;
            if (typeof(ELEMENT_T) == typeof(UInt16))
                return true;
            if (typeof(ELEMENT_T) == typeof(Int32))
                return true;
            if (typeof(ELEMENT_T) == typeof(UInt32))
                return true;
            if (typeof(ELEMENT_T) == typeof(Int64))
                return true;
            if (typeof(ELEMENT_T) == typeof(UInt64))
                return true;
            if (typeof(ELEMENT_T) == typeof(Int128))
                return true;
            if (typeof(ELEMENT_T) == typeof(UInt128))
                return true;
            if (typeof(ELEMENT_T) == typeof(IntPtr))
                return true;
            if (typeof(ELEMENT_T) == typeof(UIntPtr))
                return true;

            if (typeof(ELEMENT_T) == typeof(Single))
                return false;
            if (typeof(ELEMENT_T) == typeof(Double))
                return false;
            if (typeof(ELEMENT_T) == typeof(Decimal))
                return false;
            if (typeof(ELEMENT_T) == typeof(NFloat))
                return false;

            if (typeof(ELEMENT_T).IsEnum)
                return true;

#if false // Pointer cannot be used in type parameters.
            if (typeof(ELEMENT_T).IsPointer)
                return false;
            if (typeof(ELEMENT_T).IsFunctionPointer)
                return false;
            if (typeof(ELEMENT_T).IsUnmanagedFunctionPointer)
                return true;
#endif

            return typeof(ELEMENT_T).IsValueType;
        }
    }
}
