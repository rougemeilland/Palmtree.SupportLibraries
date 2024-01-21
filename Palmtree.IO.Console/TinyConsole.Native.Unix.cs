using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Palmtree.IO.Console
{
    partial class TinyConsole
    {
        private static partial class InterOpUnix
        {
            public const Int32 ENOTSUP = 95;
            public const Int32 STANDARD_FILE_IN = 0;
            public const Int32 STANDARD_FILE_OUT = 1;
            public const Int32 STANDARD_FILE_ERR = 2;

            [StructLayout(LayoutKind.Sequential)]
            internal struct WinSize
            {
                internal UInt16 Row;
                internal UInt16 Col;
                internal UInt16 XPixel;
                internal UInt16 YPixel;
            };

            #region GetStandardFileNo

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Int32 GetStandardFileNo(Int32 standardFileType)
            {
                Validation.Assert(OperatingSystem.IsWindows() == false, "OperatingSystem.IsWindows() == false");
                if (OperatingSystem.IsLinux())
                    return GetStandardFileNo_linux(standardFileType);
                else if (OperatingSystem.IsMacOS())
                    return GetStandardFileNo_osx(standardFileType);
                else
                    throw new NotSupportedException("Running on this operating system is not supported.");
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [LibraryImport(_NATIVE_METHOD_DLL_NAME, EntryPoint = "PalmtreeNative_GetStandardFileNo", SetLastError = true)]
            [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
            private static partial Int32 GetStandardFileNo_linux(Int32 standardFileType);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [LibraryImport(_NATIVE_METHOD_DLL_NAME, EntryPoint = "PalmtreeNative_GetStandardFileNo", SetLastError = true)]
            [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
            private static partial Int32 GetStandardFileNo_osx(Int32 standardFileType);

            #endregion

            #region GetWindowSize

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Int32 GetWindowSize(Int32 consoleFileNo, out WinSize windowSize, out Int32 errno)
            {
                Validation.Assert(OperatingSystem.IsWindows() == false, "OperatingSystem.IsWindows() == false");
                if (OperatingSystem.IsLinux())
                    return GetWindowSize_linux(consoleFileNo, out windowSize, out errno);
                else if (OperatingSystem.IsMacOS())
                    return GetWindowSize_osx(consoleFileNo, out windowSize, out errno);
                else
                    throw new NotSupportedException("Running on this operating system is not supported.");
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [LibraryImport(_NATIVE_METHOD_DLL_NAME, EntryPoint = "PalmtreeNative_GetWindowSize")]
            [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
            private static partial Int32 GetWindowSize_linux(Int32 consoleFileNo, out WinSize windowSize, out Int32 errno);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [LibraryImport(_NATIVE_METHOD_DLL_NAME, EntryPoint = "PalmtreeNative_GetWindowSize")]
            [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
            private static partial Int32 GetWindowSize_osx(Int32 consoleFileNo, out WinSize windowSize, out Int32 errno);

            #endregion
        }
    }
}
