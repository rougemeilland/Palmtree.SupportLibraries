using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Palmtree.IO
{
    [UnsupportedOSPlatform("windows")]
    internal partial class InterOpForLinux
    {
        [Flags]
        public enum OpenMode
        {
            O_CREAT = 0x40,
            O_EXCL = 0x80,
        }

        public enum Permission
        {
            S_IRWXU = 0x1c0,
        }

        public const Int32 ERROR_CODE_EEXIST = 17;

        [LibraryImport("c", EntryPoint = "open", StringMarshalling = StringMarshalling.Utf8, SetLastError = true)]
        public static partial Int32 Open(String pathName, OpenMode flag, Permission mode);

        [LibraryImport("c", EntryPoint = "close", SetLastError = true)]
        public static partial Int32 Close(Int32 handle);

        [LibraryImport("c", EntryPoint = "strerror", StringMarshalling = StringMarshalling.Utf8, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static partial String StrError(Int32 handle);
    }
}
