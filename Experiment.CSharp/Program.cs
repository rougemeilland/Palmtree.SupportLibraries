using System;
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
            Console.WriteLine(FormatExpression("aaaaa".EncodeCommandLineArgument()));
            Console.WriteLine(FormatExpression("aaa aa".EncodeCommandLineArgument()));
            Console.WriteLine(FormatExpression("aaa \"aa\"aa".EncodeCommandLineArgument()));
            Console.WriteLine(FormatExpression("aaa \\\"aa\\\"aa".EncodeCommandLineArgument()));
            Console.WriteLine(FormatExpression("a aaaa\\".EncodeCommandLineArgument()));

            if (OperatingSystem.IsWindows())
            {
                Console.WriteLine(FormatExpression("aaaaa".EncodeCommandPromptCommandLineArgument()));
                Console.WriteLine(FormatExpression("aa&aaa".EncodeCommandPromptCommandLineArgument()));
                Console.WriteLine(FormatExpression("aa&a<a>a^a|".EncodeCommandPromptCommandLineArgument()));
                Console.WriteLine(FormatExpression("aaa aa".EncodeCommandPromptCommandLineArgument()));
                Console.WriteLine(FormatExpression("aaa \\\"aa\\\"aa".EncodeCommandPromptCommandLineArgument()));
                Console.WriteLine(FormatExpression("a aaaa\\".EncodeCommandPromptCommandLineArgument()));
            }

            Console.Beep();
            Console.WriteLine("Complete");
            _ = Console.ReadLine();
        }

        private static String FormatExpression(Object value, [CallerArgumentExpression(nameof(value))] String? expression = null) => $"{expression}={value}";

    }
}
