using System;
using System.Reflection;

namespace Palmtree.IO
{
    public static class AssemblyExtensions
    {
        public static DirectoryPath GetBaseDirectory(this Assembly assembly)
        {
            if (assembly is null)
                throw new ArgumentNullException(nameof(assembly));

#pragma warning disable IL3000 // Avoid accessing Assembly file path when publishing as a single file
            // If published as a single file, assembly.Location returns an empty string.
            // In that case, use AppContext.BaseDirectory instead.
            var location = assembly.Location;
#pragma warning restore IL3000 // Avoid accessing Assembly file path when publishing as a single file
            return
                !String.IsNullOrEmpty(location)
                ? new FilePath(location).Directory
                : new DirectoryPath(AppContext.BaseDirectory);
        }
    }
}
