using System;
using System.Collections.Generic;
using System.Reflection;

namespace Palmtree.IO.Compression.Archive.Zip
{
    internal static class AssemblyExtensions
    {
        public static IEnumerable<(Assembly assembly, Boolean isExternalAssembly)> DistinctAssembly(this IEnumerable<Assembly> assemblies, Assembly thisAssembly)
        {
            var uniqueAssemblies = new Dictionary<Assembly, Assembly>();
            foreach (var assembly in assemblies)
            {
                if (uniqueAssemblies.TryAdd(assembly, assembly))
                    yield return (assembly, assembly != thisAssembly);
            }
        }
    }
}
