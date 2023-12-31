using System;
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
            var pipe = new InProcessPipe();
            var task1 =
                Task.Run(() =>
                {
                    try
                    {
                        using (var inStream = pipe.OpenInputStream())
                        {
                            var data = inStream.ReadBytes(16);
                            Console.WriteLine(data.ToFriendlyString());
                            _ = inStream.ReadAllBytes();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                });
            var task2 =
                Task.Run(() =>
                {
                    try
                    {
                        using (var outStream = pipe.OpenOutputStream())
                        {
                            var buffer = new byte[1024 * 1024];
                            buffer.FillArray((byte)0xff);
                            buffer[0] = 1;
                            buffer[1] = 2;
                            buffer[2] = 3;
                            buffer[3] = 4;
                            outStream.WriteBytes(buffer);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                });

            Task.WhenAll(task1, task2).Wait();

            Console.Beep();
            _ = Console.ReadLine();
        }
    }
}
