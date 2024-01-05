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

            var location = assembly.Location;
            return
                !String.IsNullOrEmpty(location)
                ? Path.GetFileNameWithoutExtension(location)
                : AppDomain.CurrentDomain.FriendlyName;
        }
    }
}
