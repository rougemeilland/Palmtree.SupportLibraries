﻿using System;
using System.Linq;
using System.Text;
using Palmtree;
using Palmtree.IO;
using Palmtree.IO.Compression.Archive.Zip;
using Palmtree.IO.Compression.Stream.Plugin;

namespace Test.ZipUtility.Validation
{
    internal class Program
    {
        static Program()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            StoredCoderPlugin.EnablePlugin();
            DeflateCoderPlugin.EnablePlugin();
            StoredCoderPlugin.EnablePlugin();
        }

        static void Main(string[] args)
        {
            var fileList =
                args.EnumerateFilesFromArgument(true)
                .Where(file => file.Extension.IsAnyOf(".zip", ".001", ".exe", ".epub", StringComparison.OrdinalIgnoreCase))
                .ToList();
            var totalSize = fileList.Aggregate(0UL, (length, file) => checked(length + file.Length));
            var completed = 0UL;
            foreach (var file in fileList)
            {
                try
                {
                    var result =
                        file.ValidateAsZipFile(
                            ValidationStringency.Strict | ValidationStringency.AllowNullPayloadAfterEOCDR,
                            new SimpleProgress<double>(value => Console.Write($"  {(completed + value * file.Length) * 100.0 / totalSize:F2}%\r")));
                    if (result.ResultId != ZipArchiveValidationResultId.Ok)
                        Console.ForegroundColor = ConsoleColor.Red;
                    checked
                    {
                        completed += file.Length;
                    }

                    Console.WriteLine($"\"{(double)completed * 100 / totalSize:F2}% {file.FullName}\": {result.ResultId}, \"{result.Message}\"");
                }
                finally
                {
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Completed.");
            Console.Beep();
            _ = Console.ReadLine();
        }
    }
}
