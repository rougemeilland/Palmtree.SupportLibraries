using System;
using System.Linq;
using System.Text;
using System.Threading;
using Palmtree.Application;
using Palmtree.IO;
using Palmtree.IO.Console;

namespace Experiment.CSharp
{
    public class Program
    {
        private class Application
            : BatchApplication
        {
            private readonly string? _title;
            private readonly Encoding? _encoding;

            static Application()
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            }

            public Application(string? title, Encoding? encoding)
            {
                _title = title;
                _encoding = encoding;
            }

            protected override string ConsoleWindowTitle => _title ?? base.ConsoleWindowTitle;
            protected override Encoding? InputOutputEncoding => _encoding;

            protected override ResultCode Main(string[] args)
            {
                try
                {
                    var baseDirectory = new DirectoryPath(args[0]);
                    ReportProgress("Searching files...");
                    var zipFiles =
                        baseDirectory.EnumerateFiles(true)
                        .Where(file => string.Equals(file.Extension, ".zip", StringComparison.OrdinalIgnoreCase));
                    var totalCount = zipFiles.Aggregate(0UL, (total, file) => total + file.Length);
                    var currentCount = 0UL;
                    ReportProgress((double)currentCount / totalCount);
                    foreach (var zipFile in zipFiles)
                    {
                        if (IsPressedBreak)
                            return ResultCode.Cancelled;
                        Thread.Sleep(100);
                        currentCount += zipFile.Length;
                        ReportProgress((double)currentCount / totalCount, zipFile.FullName, (percentage, content) => $"{percentage}: processed \"{content}\"");
                    }

                    return ResultCode.Success;
                }
                catch (Exception ex)
                {
                    ReportException(ex);
                    return ResultCode.Failed;
                }
            }

            protected override void Finish(ResultCode result)
            {
                if (result == ResultCode.Success)
                    TinyConsole.WriteLine("Completed.");
                else
                    TinyConsole.WriteLine("Cancelled.");
            }
        }

        public static int Launch(string? title, Encoding? encoding, string[] args)
        {
            return new Application(title, encoding).Run(args);
        }

        private static int Main(string[] args)
        {
            return Launch(null, null, args);
        }
    }
}
