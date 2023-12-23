using System;
using System.Runtime.InteropServices;

namespace Palmtree.IO.Console
{
    partial class TinyConsole
    {
        private static class InterOpUnix
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

            [DllImport("Palmtree.Console.InterOp.Unix_x86", EntryPoint = "PalmtreeNative_GetStandardFileNo")]
            private extern static Int32 GetStandardFileNo_x86(Int32 standardFileType);

            [DllImport("Palmtree.IO.Console.InterOp.Unix_x64", EntryPoint = "PalmtreeNative_GetStandardFileNo")]
            private extern static Int32 GetStandardFileNo_x64(Int32 standardFileType);

            [DllImport("Palmtree.IO.Console.InterOp.Unix_arm32", EntryPoint = "PalmtreeNative_GetStandardFileNo")]
            private extern static Int32 GetStandardFileNo_arm32(Int32 standardFileType);

            [DllImport("Palmtree.IO.Console.InterOp.Unix_arm64", EntryPoint = "PalmtreeNative_GetStandardFileNo")]
            private extern static Int32 GetStandardFileNo_arm64(Int32 standardFileType);

            public static Int32 GetStandardFileNo(Int32 standardFileType)
                => RuntimeInformation.ProcessArchitecture switch
                {
                    Architecture.X86 or Architecture.X64 =>
                        Environment.Is64BitProcess
                        ? GetStandardFileNo_x64(standardFileType)
                        : GetStandardFileNo_x86(standardFileType),
                    Architecture.Arm or Architecture.Arm64 =>
                        Environment.Is64BitProcess
                        ? GetStandardFileNo_arm64(standardFileType)
                        : GetStandardFileNo_arm32(standardFileType),
                    _ => throw new NotSupportedException(),
                };

            #endregion

            #region GetWindowSize

            [DllImport("Palmtree.Console.InterOp.Unix_x86", EntryPoint = "PalmtreeNative_GetWindowSize")]
            private extern static Int32 GetWindowSize_x86(Int32 consoleFileNo, out WinSize windowSize, out Int32 errno);

            [DllImport("Palmtree.IO.Console.InterOp.Unix_x64", EntryPoint = "PalmtreeNative_GetWindowSize")]
            private extern static Int32 GetWindowSize_x64(Int32 consoleFileNo, out WinSize windowSize, out Int32 errno);

            [DllImport("Palmtree.IO.Console.InterOp.Unix_arm32", EntryPoint = "PalmtreeNative_GetWindowSize")]
            private extern static Int32 GetWindowSize_arm32(Int32 consoleFileNo, out WinSize windowSize, out Int32 errno);

            [DllImport("Palmtree.IO.Console.InterOp.Unix_arm64", EntryPoint = "PalmtreeNative_GetWindowSize")]
            private extern static Int32 GetWindowSize_arm64(Int32 consoleFileNo, out WinSize windowSize, out Int32 errno);

            public static Int32 GetWindowSize(Int32 consoleFileNo, out WinSize windowSize, out Int32 errno)
                => RuntimeInformation.ProcessArchitecture switch
                {
                    Architecture.X86 or Architecture.X64 =>
                        Environment.Is64BitProcess
                        ? GetWindowSize_x64(consoleFileNo, out windowSize, out errno)
                        : GetWindowSize_x86(consoleFileNo, out windowSize, out errno),
                    Architecture.Arm or Architecture.Arm64 =>
                        Environment.Is64BitProcess
                        ? GetWindowSize_arm64(consoleFileNo, out windowSize, out errno)
                        : GetWindowSize_arm32(consoleFileNo, out windowSize, out errno),
                    _ => throw new NotSupportedException(),
                };

            #endregion
        }
    }
}
