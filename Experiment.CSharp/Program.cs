using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Experiment.CSharp
{
    internal sealed partial class Program
    {
        [SuppressMessage("Style", "IDE0060:未使用のパラメーターを削除します", Justification = "非公開の内部コマンドのMainメソッドであり、将来パラメタが使用される可能性があるため。")]
        static void Main(string[] args)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "work.dat");
            var originalText1 = "<<Data Block 1>>";
            var originalText2 = "<<Data Block 2>>";
            var originalText3 = "<<Data Block 3>>";

            for (var count = 0; count < 1000; ++count)
                DoTest(path, originalText1, originalText2, originalText3);

            Console.Beep();
            Console.WriteLine("Complete");
            _ = Console.ReadLine();
        }

        private static void DoTest(string path, string originalText1, string originalText2, string originalText3)
        {
            var originalData1 = Encoding.UTF8.GetBytes(originalText1);
            var originalData2 = Encoding.UTF8.GetBytes(originalText2);
            var originalData3 = Encoding.UTF8.GetBytes(originalText3);

            Span<byte> buffer = stackalloc byte[256];
            try
            {
                using (var outStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var outStream2= new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    var tempFilePath1 = Path.GetTempFileName();
                    try
                    {
                        using (var outTempStream1 = new FileStream(tempFilePath1, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            var tempFilePath2 = Path.GetTempFileName();
                            try
                            {
                                using (var outTempStream2 = new FileStream(tempFilePath2, FileMode.Create, FileAccess.Write, FileShare.None))
                                {
                                    WriteData(outTempStream2, originalData1);
                                    outTempStream2.Flush();
                                }

                                using (var inTempStream2 = new FileStream(tempFilePath2, FileMode.Open, FileAccess.Read, FileShare.None))
                                {
                                    var length = ReadData(inTempStream2, buffer);
                                    if (!string.Equals(Encoding.UTF8.GetString(buffer[..length]), originalText1, StringComparison.Ordinal))
                                        throw new Exception();
                                }
                            }
                            finally
                            {
                                File.Delete(tempFilePath2);
                            }

                            WriteData(outTempStream1, originalData2);
                            outTempStream1.Flush();
                        }

                        using (var inTempStream1 = new FileStream(tempFilePath1, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            var length = ReadData(inTempStream1, buffer);
                            if (!string.Equals(Encoding.UTF8.GetString(buffer[..length]), originalText2, StringComparison.Ordinal))
                                throw new Exception();
                        }

                        WriteData(outStream, originalData3);
                        outStream.Flush();
                    }
                    finally
                    {
                        File.Delete(tempFilePath1);
                    }
                }

                using (var inStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None)) // ここで共用違反例外の可能性
                {
                    var length = ReadData(inStream, buffer);
                    if (!string.Equals(Encoding.UTF8.GetString(buffer[..length]), originalText3, StringComparison.Ordinal))
                        throw new Exception();
                }
            }
            finally
            {
                File.Delete(path);  // ここで共用違反例外の可能性
            }
        }

        private static int ReadData(Stream inStream, Span<byte> buffer)
        {
            var totalLength = 0;
            while (buffer.Length > 0)
            {
                var length = inStream.Read(buffer);
                if (length <= 0)
                    return totalLength;
                buffer = buffer[length..];
                totalLength += length;
            }

            throw new Exception("The length of buffer is insufficient.");
        }

        private static void WriteData(Stream outStream, ReadOnlySpan<byte> buffer)
        {
            outStream.Write(buffer);
        }
    }
}
