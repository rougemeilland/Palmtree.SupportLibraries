using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Palmtree;
using Palmtree.IO.Console;

namespace Experiment.CSharp
{
    internal static partial class Program
    {
        private static void Main()
        {
            Console.WriteLine(FormatExpression(Environment.GetEnvironmentVariable("__PALMTREE_PROCESS_PRIORITY") ?? "(undefined)"));
            Console.WriteLine(FormatExpression(Process.GetCurrentProcess().PriorityClass));
            ProcessUtility.SetupCurrentProcessPriority();
            Console.WriteLine(FormatExpression(Environment.GetEnvironmentVariable("__PALMTREE_PROCESS_PRIORITY") ?? "(undefined)"));
            Console.WriteLine(FormatExpression(Process.GetCurrentProcess().PriorityClass));

            var s = new ProcessStartInfo
            {
                FileName = "experiment.exe",
                UseShellExecute = false,
                CreateNoWindow = false,
            };

            foreach (var key in s.EnvironmentVariables.Keys)
            {
                Console.WriteLine(FormatExpression(key));
            }

            Console.Beep();
            Console.WriteLine("Complete");
            _ = Console.ReadLine();
        }

        private static String FormatExpression(Object value, [CallerArgumentExpression(nameof(value))] String? expression = null) => $"{expression}={value}";

    }
}
