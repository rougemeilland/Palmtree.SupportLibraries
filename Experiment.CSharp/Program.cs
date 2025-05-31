using System;
using System.IO;
using System.Runtime.InteropServices;
using Palmtree.IO;

namespace Experiment.CSharp
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
#if false
            {
                var tempFile1 = FilePath.CreateTemporaryFile();
                Console.WriteLine($"Created \"{tempFile1.FullName}\"");
                Console.WriteLine($"{(tempFile1.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile1.FullName}\"");
                tempFile1.Delete();
                var tempFile2 = FilePath.CreateTemporaryFile(".XXX");
                Console.WriteLine($"Created \"{tempFile2.FullName}\"");
                Console.WriteLine($"{(tempFile2.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile2.FullName}\"");
                tempFile2.Delete();
                var tempFile3 = FilePath.CreateTemporaryFile(suffix: ".mkv");
                Console.WriteLine($"Created \"{tempFile3.FullName}\"");
                Console.WriteLine($"{(tempFile3.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile3.FullName}\"");
                tempFile3.Delete();
                var tempFile4 = FilePath.CreateTemporaryFile(".XXX", ".mkv");
                Console.WriteLine($"Created \"{tempFile4.FullName}\"");
                Console.WriteLine($"{(tempFile4.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile4.FullName}\"");
                tempFile4.Delete();
            }
#endif

            {

                var tempFile1 = FilePath.CreateTemporaryFile();
                Console.WriteLine($"Created \"{tempFile1.FullName}\"");
                Console.WriteLine($"{(tempFile1.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile1.FullName}\"");
                var tempFile2 = FilePath.CreateTemporaryFile(".XXX");
                Console.WriteLine($"Created \"{tempFile2.FullName}\"");
                Console.WriteLine($"{(tempFile2.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile2.FullName}\"");
                var tempFile3 = FilePath.CreateTemporaryFile(suffix: ".mkv");
                Console.WriteLine($"Created \"{tempFile3.FullName}\"");
                Console.WriteLine($"{(tempFile3.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile3.FullName}\"");
                var tempFile4 = FilePath.CreateTemporaryFile(".XXX", ".mkv");
                Console.WriteLine($"Created \"{tempFile4.FullName}\"");
                Console.WriteLine($"{(tempFile4.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile4.FullName}\"");
            }

            { 
                var tempFile1 = FilePath.CreateTemporaryFile();
                Console.WriteLine($"Created \"{tempFile1.FullName}\"");
                Console.WriteLine($"{(tempFile1.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile1.FullName}\"");
                var tempFile2 = FilePath.CreateTemporaryFile(".XXX");
                Console.WriteLine($"Created \"{tempFile2.FullName}\"");
                Console.WriteLine($"{(tempFile2.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile2.FullName}\"");
                var tempFile3 = FilePath.CreateTemporaryFile(suffix: ".mkv");
                Console.WriteLine($"Created \"{tempFile3.FullName}\"");
                Console.WriteLine($"{(tempFile3.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile3.FullName}\"");
                var tempFile4 = FilePath.CreateTemporaryFile(".XXX", ".mkv");
                Console.WriteLine($"Created \"{tempFile4.FullName}\"");
                Console.WriteLine($"{(tempFile4.Exists ? "EXISTS" : "NOT EXISTS")} \"{tempFile4.FullName}\"");
            }

            Console.Beep();
            Console.WriteLine("Complete");
            _ = Console.ReadLine();
        }
    }
}
