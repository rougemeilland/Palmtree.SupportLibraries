using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Palmtree.IO.Console
{
    partial class TinyConsole
    {
        private class NativeDllNameResolver
        {
            private readonly Dictionary<String, IntPtr> _loadedDllHandles;

            public NativeDllNameResolver()
            {
                _loadedDllHandles = new Dictionary<String, IntPtr>();
            }

            public IntPtr ResolveDllName(String libraryName, Assembly assembly, DllImportSearchPath? searchPath)
            {
                if (_loadedDllHandles.TryGetValue(libraryName, out var loadedLibraryHandle))
                {
                    return loadedLibraryHandle;
                }
                else if (libraryName == _NATIVE_METHOD_DLL_NAME)
                {
                    foreach (var libraryPath in EnumerateNativeMethodDllNames(assembly))
                    {
                        if (NativeLibrary.TryLoad(libraryPath, assembly, searchPath, out var handle))
                        {
                            _loadedDllHandles.Add(libraryName, handle);
                            return handle;
                        }
                    }

                    return IntPtr.Zero;
                }
                else
                {
                    if (NativeLibrary.TryLoad(libraryName, assembly, searchPath, out var handle))
                    {
                        _loadedDllHandles.Add(libraryName, handle);
                        return handle;
                    }

                    return IntPtr.Zero;
                }
            }

            public IntPtr GetDllHandle(String dllName) => _loadedDllHandles.TryGetValue(dllName, out var handle) ? handle : IntPtr.Zero;

            private static IEnumerable<String> EnumerateNativeMethodDllNames(Assembly assembly)
            {
                if (OperatingSystem.IsWindows())
                {
                    return
                        RuntimeInformation.ProcessArchitecture switch
                        {
                            Architecture.X86 => EnumerablePath(assembly, "Palmtree.IO.Console.Native.win_x86.dll", "win-x86"),
                            Architecture.X64 => EnumerablePath(assembly, "Palmtree.IO.Console.Native.win_x64.dll", "win-x64"),
                            Architecture.Arm => EnumerablePath(assembly, "Palmtree.IO.Console.Native.win_arm32.dll", "win-arm"),
                            Architecture.Arm64 => EnumerablePath(assembly, "Palmtree.IO.Console.Native.win_arm64.dll", "win-arm64"),
                            _ => throw new NotSupportedException($"Running on this architecture is not supported. : architecture={RuntimeInformation.ProcessArchitecture}"),
                        };
                }
                else if (OperatingSystem.IsLinux())
                {
                    return
                        RuntimeInformation.ProcessArchitecture switch
                        {
                            Architecture.X86 => EnumerablePath(assembly, "libPalmtree.IO.Console.Native.linux_x86.so", "linux-x86"),
                            Architecture.X64 => EnumerablePath(assembly, "libPalmtree.IO.Console.Native.linux_x64.so", "linux-x64"),
                            Architecture.Arm => EnumerablePath(assembly, "libPalmtree.IO.Console.Native.linux_arm32.so", "linux-arm"),
                            Architecture.Arm64 => EnumerablePath(assembly, "libPalmtree.IO.Console.Native.linux_arm64.so", "linux-arm32"),
                            _ => throw new NotSupportedException($"Running on this architecture is not supported. : architecture={RuntimeInformation.ProcessArchitecture}"),
                        };
                }
                else if (OperatingSystem.IsMacOS())
                {
                    return
                        RuntimeInformation.ProcessArchitecture switch
                        {
                            Architecture.X86 => EnumerablePath(assembly, "libPalmtree.IO.Console.Native.osx_x86.dylib", "osx-x86"),
                            Architecture.X64 => EnumerablePath(assembly, "libPalmtree.IO.Console.Native.osx_x64.dylib", "osx-x64"),
                            Architecture.Arm => EnumerablePath(assembly, "libPalmtree.IO.Console.Native.osx_arm32.dylib", "osx-arm"),
                            Architecture.Arm64 => EnumerablePath(assembly, "libPalmtree.IO.Console.Native.osx_arm64.dylib", "osx-arm64"),
                            _ => throw new NotSupportedException($"Running on this architecture is not supported. : architecture={RuntimeInformation.ProcessArchitecture}"),
                        };
                }
                else
                {
                    throw new NotSupportedException("Running on this operating system is not supported.");
                }
            }

            private static IEnumerable<String> EnumerablePath(Assembly assembly, String libraryName, String platformId)
            {
                // アセンブリと同じディレクトリの下にライブラリファイルが存在しているかどうかを確認する
                var dllFile1 = assembly.GetBaseDirectory().GetFile(libraryName);
                if (dllFile1.Exists)
                {
                    // 存在していればそのフルパスを返す
                    yield return dllFile1.FullName;
                }

                // アセンブリと同じディレクトリの "./runtimes/<platform-id>/native" の下にライブラリファイルが存在しているかどうかを確認する
                var dllFile2 = assembly.GetBaseDirectory().GetSubDirectory("runtimes", platformId, "native").GetFile(libraryName);
                if (dllFile2.Exists)
                {
                    // 存在していればそのフルパスを返す
                    yield return dllFile2.FullName;
                }

                // 与えられたライブラリ名をそのまま返す。
                yield return libraryName;
            }
        }
    }
}
