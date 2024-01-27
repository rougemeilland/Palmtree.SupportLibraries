using System;
using System.Runtime.InteropServices;

namespace Palmtree
{
    public abstract partial class WindowsDesktopApplication
    {
        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool AllocConsole();

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool FreeConsole();

        public WindowsDesktopApplication()
        {
            if (!OperatingSystem.IsWindows())
                throw new NotSupportedException();
        }

        public Int32 Run()
        {
            if (!AllocConsole())
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()) ?? new Exception();

            try
            {
                return Main();
            }
            finally
            {
                _ = FreeConsole();
            }
        }

        protected abstract Int32 Main();
    }
}
