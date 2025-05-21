using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Palmtree;
using Palmtree.Collections;

namespace Experiment.CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var x = "\"     \\ \\\"\"".CommandLineArgumentDecode();

            Test1();

            Console.Beep();
            Console.WriteLine("\aComplete");
            _ = Console.ReadLine();
        }

        static void Test1()
        {
            var testCharacter= new[] {' ', 'a', '\\', '"', '\0' };
            foreach (var c0 in testCharacter)
            {
                foreach (var c1 in testCharacter)
                {
                    foreach (var c2 in testCharacter)
                    {
                        foreach (var c3 in testCharacter)
                        {
                            foreach (var c4 in testCharacter)
                            {
                                foreach (var c5 in testCharacter)
                                {
                                    foreach (var c6 in testCharacter)
                                    {
                                        foreach (var c7 in testCharacter)
                                        {
                                            var sb = new StringBuilder();
                                            if (c0 != '\0')
                                                _=sb.Append(c0);
                                            if (c1 != '\0')
                                                _ = sb.Append(c1);
                                            if (c2 != '\0')
                                                _ = sb.Append(c2);
                                            if (c3 != '\0')
                                                _ = sb.Append(c3);
                                            if (c4 != '\0')
                                                _ = sb.Append(c4);
                                            if (c5 != '\0')
                                                _ = sb.Append(c5);
                                            if (c6 != '\0')
                                                _ = sb.Append(c6);
                                            if (c7 != '\0')
                                                _ = sb.Append(c7);
                                            var s0 = sb.ToString();
                                            var s1 = s0.CommandLineArgumentEncode();
                                            var s2 = s1.CommandLineArgumentDecode();
                                            if (s2 != s0)
                                                throw new Exception();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
