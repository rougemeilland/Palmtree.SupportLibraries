using System;
using Palmtree;
using Palmtree.IO;

namespace Experiment.CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var pipe = new InProcessPipe();
            using (var inStream = pipe.OpenInputStream())
            using (var outStream = pipe.OpenOutputStream())
            {
                outStream.WriteBytes(new byte[10]);
                outStream.Dispose();
                var data = inStream.ReadAllBytes();
                Console.WriteLine(data.Length);
            }

            Console.Beep();
            _ = Console.ReadLine();
        }
    }
}
