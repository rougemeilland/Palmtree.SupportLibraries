using System;
using System.IO;
using System.Reflection;

namespace Palmtree
{
    public static class AssemblyExtensions
    {
        public static String GetAssemblyFileNameWithoutExtension(this Assembly assembly)
        {
            if (assembly is null)
                throw new ArgumentNullException(nameof(assembly));

#pragma warning disable IL3000 // Avoid accessing Assembly file path when publishing as a single file
            // If published as a single file, assembly.Location returns an empty string.
            // In that case, AppDomain.CurrentDomain.FriendlyName is used as the return value.
            var location = assembly.Location;
#pragma warning restore IL3000 // Avoid accessing Assembly file path when publishing as a single file
            return
                !String.IsNullOrEmpty(location)
                ? Path.GetFileNameWithoutExtension(location)
                : AppDomain.CurrentDomain.FriendlyName;
        }
    }
}
