using System;
using System.Reflection;

namespace Palmtree.IO
{
    public static class AssemblyExtensions
    {
        static AssemblyExtensions()
        {
#if DEBUG
            System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
            System.Diagnostics.Trace.WriteLine($"{typeof(AssemblyExtensions).FullName}: directory={typeof(AssemblyExtensions).Assembly.GetBaseDirectory().FullName}");
#endif
        }

        public static DirectoryPath GetBaseDirectory(this Assembly assembly)
        {
            if (assembly is null)
                throw new ArgumentNullException(nameof(assembly));

            var location = assembly.Location;
            return
                !String.IsNullOrEmpty(location)
                ? new FilePath(location).Directory
                : new DirectoryPath(AppContext.BaseDirectory);
        }
    }
}
