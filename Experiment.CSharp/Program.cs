using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Palmtree;
using Palmtree.IO;

namespace Experiment.CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies.Where(assembly => !assembly.ManifestModule.Name.StartsWith("System.", StringComparison.Ordinal)))
            {
                Console.WriteLine(assembly.FullName?.CSharpEncode());
            }
            Console.Beep();
            _ = Console.ReadLine();
        }
    }
}
