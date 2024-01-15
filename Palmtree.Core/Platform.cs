using System;
using System.Runtime.InteropServices;

namespace Palmtree
{
    public static class Platform
    {
        public static String NugetResourceId
        {
            get
            {
                if (OperatingSystem.IsWindows())
                {
                    return
                        RuntimeInformation.ProcessArchitecture switch
                        {
                            Architecture.X86 => "win-x86",
                            Architecture.X64 => "win-x64",
                            Architecture.Arm => "win-arm",
                            Architecture.Arm64 => "win-arm64",
                            _ => throw new NotSupportedException($"Running on this architecture is not supported. : architecture={RuntimeInformation.ProcessArchitecture}"),
                        };
                }
                else if (OperatingSystem.IsLinux())
                {
                    return
                        RuntimeInformation.ProcessArchitecture switch
                        {
                            Architecture.X86 => "linux-x86",
                            Architecture.X64 => "linux-x64",
                            Architecture.Arm => "linux-arm",
                            Architecture.Arm64 => "linux-arm64",
                            _ => throw new NotSupportedException($"Running on this architecture is not supported. : architecture={RuntimeInformation.ProcessArchitecture}"),
                        };
                }
                else if (OperatingSystem.IsMacOS())
                {
                    return
                        RuntimeInformation.ProcessArchitecture switch
                        {
                            Architecture.X86 => "osx-x86",
                            Architecture.X64 => "osx-x64",
                            Architecture.Arm => "osx-arm",
                            Architecture.Arm64 => "osx-arm64",
                            _ => throw new NotSupportedException($"Running on this architecture is not supported. : architecture={RuntimeInformation.ProcessArchitecture}"),
                        };
                }
                else
                {
                    throw new NotSupportedException("Running on this operating system is not supported.");
                }
            }
        }

        public static String NativeCodeId
        {
            get
            {
                if (OperatingSystem.IsWindows())
                {
                    return
                        RuntimeInformation.ProcessArchitecture switch
                        {
                            Architecture.X86 => "win_x86",
                            Architecture.X64 => "win_x64",
                            Architecture.Arm => "win_arm32",
                            Architecture.Arm64 => "win_arm64",
                            _ => throw new NotSupportedException($"Running on this architecture is not supported. : architecture={RuntimeInformation.ProcessArchitecture}"),
                        };
                }
                else if (OperatingSystem.IsLinux())
                {
                    return
                        RuntimeInformation.ProcessArchitecture switch
                        {
                            Architecture.X86 => "linux_x86",
                            Architecture.X64 => "linux_x64",
                            Architecture.Arm => "linux_arm32",
                            Architecture.Arm64 => "linux_arm64",
                            _ => throw new NotSupportedException($"Running on this architecture is not supported. : architecture={RuntimeInformation.ProcessArchitecture}"),
                        };
                }
                else if (OperatingSystem.IsMacOS())
                {
                    return
                        RuntimeInformation.ProcessArchitecture switch
                        {
                            Architecture.X86 => "osx_x86",
                            Architecture.X64 => "osx_x64",
                            Architecture.Arm => "osx_arm32",
                            Architecture.Arm64 => "osx_arm64",
                            _ => throw new NotSupportedException($"Running on this architecture is not supported. : architecture={RuntimeInformation.ProcessArchitecture}"),
                        };
                }
                else
                {
                    throw new NotSupportedException("Running on this operating system is not supported.");
                }
            }
        }
    }
}
