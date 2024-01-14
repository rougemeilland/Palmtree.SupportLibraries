using System;
using Palmtree;
using Palmtree.Debug.IO;
using Palmtree.IO;

namespace Experiment.CSharp
{
    internal class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:未使用のパラメーターを削除します", Justification = "<保留中>")]
        static void Main(string[] args)
        {
            var exception = (Exception?)null;
            using (var inStream = InputTestDataStream.Create(1024 * 1024 * 2, b => (byte)(b & 0x3f)))
            using (var outStream = OutputTestDataStream.Create(ex => exception = ex))
            {
                var progressValue = new ProgressCounterUInt64(value => Console.WriteLine($"copied {value:N0} (0x{value:x16}) bytes."), TimeSpan.FromMilliseconds(1000));
                progressValue.Report();
                inStream.CopyTo(outStream, new SimpleProgress<ulong>(value => progressValue.Value = value));
                outStream.Flush();
                progressValue.Report();
            }

            if (exception is not null)
                throw exception;

            Console.Beep();
            _ = Console.ReadLine();
        }
    }
}
