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

            var location = assembly.Location;
            return
                !String.IsNullOrEmpty(location)
                ? new FilePath(location).Directory
                : new DirectoryPath(AppContext.BaseDirectory);
        }
    }
}
