using System;
using System.IO;
using System.Reflection;

namespace Palmtree
{
    public static class AssemblyExtensions
    {
        static AssemblyExtensions()
        {
#if DEBUG
            System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
            System.Diagnostics.Trace.WriteLine($"{typeof(AssemblyExtensions).FullName}: name={typeof(AssemblyExtensions).Assembly.GetAssemblyFileNameWithoutExtension()}");
#endif
        }

        public static String GetAssemblyFileNameWithoutExtension(this Assembly assembly)
        {
            if (assembly is null)
                throw new ArgumentNullException(nameof(assembly));

            var location = assembly.Location;
            return
                !String.IsNullOrEmpty(location)
                ? Path.GetFileNameWithoutExtension(location)
                : AppDomain.CurrentDomain.FriendlyName;
        }
    }
}
