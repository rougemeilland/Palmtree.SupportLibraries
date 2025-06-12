using System;
using System.Linq;
using Palmtree;
using Palmtree.Collections;
using Palmtree.IO;
using Palmtree.IO.Compression.Archive.Zip;
using Palmtree.IO.Compression.Stream.Plugin;

namespace Test.Zip.MultiVolume
{
    internal sealed class Program
    {
        static Program()
        {
            StoredCoderPlugin.EnablePlugin();
            DeflateCoderPlugin.EnablePlugin();
        }

        private static void Main(String[] args)
        {
            var baseDirectory = new DirectoryPath(args[0]);

            Test1_2番目以降のローカルヘッダがボリュームの先頭にある場合(baseDirectory, $"{GetFileName(nameof(Test1_2番目以降のローカルヘッダがボリュームの先頭にある場合))}.zip");

            Test2_データディスクリプタがボリュームの先頭にある場合(baseDirectory, $"{GetFileName(nameof(Test2_データディスクリプタがボリュームの先頭にある場合))}.zip");

            // WinRar で開くとエラーとなる。PKZIP および 7-zip は OK。
            Test3_最初のセントラルディレクトリヘッダがボリュームディスクの先頭にある場合(baseDirectory, $"{GetFileName(nameof(Test3_最初のセントラルディレクトリヘッダがボリュームディスクの先頭にある場合))}.zip");

            // WinRar で開くとエラーとなる。PKZIP および 7-zip は OK。
            Test4_ZIP64_EOCDR_がボリュームディスクの先頭にある場合(baseDirectory, $"{GetFileName(nameof(Test4_ZIP64_EOCDR_がボリュームディスクの先頭にある場合))}.zip");

            // WinRar で開くとエラーとなる。PKZIP および 7-zip は OK。
            Test5_ZIP64_EOCDR_があるボリュームディスクにセントラルディレクトリヘッダが部分的に含まれている場合(baseDirectory, $"{GetFileName(nameof(Test5_ZIP64_EOCDR_があるボリュームディスクにセントラルディレクトリヘッダが部分的に含まれている場合))}.zip");

            // WinRar で開くとエラーとなる。PKZIP および 7-zip は OK。
            Test6_EOCDR_がボリュームディスクの先頭にある場合(baseDirectory, $"{GetFileName(nameof(Test6_EOCDR_がボリュームディスクの先頭にある場合))}.zip");

            Test7_EOCDR_があるボリュームディスクにセントラルディレクトリヘッダが部分的に含まれている場合(baseDirectory, $"{GetFileName(nameof(Test7_EOCDR_があるボリュームディスクにセントラルディレクトリヘッダが部分的に含まれている場合))}.zip");

            Console.WriteLine("終了しました。");
            Console.Beep();
            _ = Console.ReadLine();
        }

        private static String GetFileName(String text)
        {
            var index = text.IndexOf('_');
            if (index < 0)
                return text;
            else
                return text[(index + 1)..];
        }

        private static void Test1_2番目以降のローカルヘッダがボリュームの先頭にある場合(DirectoryPath baseDirectory, String fileName)
        {
            const UInt64 VOLUME_SIZE = 1024;
            DoTest1(baseDirectory, fileName, VOLUME_SIZE, ZipWriterFlags.None, 2, checked((UInt16)(VOLUME_SIZE - 69)), 0, false);
        }

        private static void Test2_データディスクリプタがボリュームの先頭にある場合(DirectoryPath baseDirectory, String fileName)
        {
            const UInt64 VOLUME_SIZE = 1024;
            DoTest1(baseDirectory, fileName, VOLUME_SIZE, ZipWriterFlags.None, 2, checked((UInt16)(VOLUME_SIZE - 69)), 0, true);
        }

        private static void Test3_最初のセントラルディレクトリヘッダがボリュームディスクの先頭にある場合(DirectoryPath baseDirectory, String fileName)
        {
            const UInt64 VOLUME_SIZE = 1024;
            DoTest1(baseDirectory, fileName, VOLUME_SIZE, ZipWriterFlags.None, 2, 16, checked((UInt16)(VOLUME_SIZE - 130)), false);
        }

        private static void Test4_ZIP64_EOCDR_がボリュームディスクの先頭にある場合(DirectoryPath baseDirectory, String fileName)
        {
            const UInt64 VOLUME_SIZE = (UInt32.MaxValue + 100UL) * 2;
            DoTest1(baseDirectory, fileName, VOLUME_SIZE, ZipWriterFlags.None, 2, UInt32.MaxValue - 97, 0, false);
        }

        private static void Test5_ZIP64_EOCDR_があるボリュームディスクにセントラルディレクトリヘッダが部分的に含まれている場合(DirectoryPath baseDirectory, String fileName)
        {
            const UInt64 VOLUME_SIZE = (UInt32.MaxValue + 100UL) * 2;
            DoTest1(baseDirectory, fileName, VOLUME_SIZE, ZipWriterFlags.None, 4, UInt32.MaxValue / 2 - 70, 0, false);
        }
        private static void Test6_EOCDR_がボリュームディスクの先頭にある場合(DirectoryPath baseDirectory, String fileName)
        {
            const UInt64 VOLUME_SIZE = 1024;
            DoTest1(baseDirectory, fileName, VOLUME_SIZE, ZipWriterFlags.None, 2, VOLUME_SIZE - 180, 0, false);
        }

        private static void Test7_EOCDR_があるボリュームディスクにセントラルディレクトリヘッダが部分的に含まれている場合(DirectoryPath baseDirectory, String fileName)
        {
            const UInt64 VOLUME_SIZE = 1024;
            DoTest1(baseDirectory, fileName, VOLUME_SIZE, ZipWriterFlags.None, 4, VOLUME_SIZE / 2 - 150, 0, false);
        }

        private static void DoTest1(DirectoryPath baseDirectory, String fileName, UInt64 volumeSize, ZipWriterFlags flag, Int32 numberOfEntries, UInt64 contentSize, UInt16 commentSize, Boolean useDatadescriptor)
        {
            var zipArchive = baseDirectory.GetFile(fileName);
            using (var zipWriter = zipArchive.CreateAsZipFile(volumeSize))
            {
                zipWriter.Flags = flag;
                for (var count = 1; count <= numberOfEntries; ++count)
                {
                    Console.WriteLine($"書き込み中 {count}/{numberOfEntries}... \"{zipArchive.FullName}\"");
                    var file = zipWriter.CreateEntry($"ファイル{count}.bin", new String([.. RandomSequence.GetAsciiCharSequence().Take(commentSize)]));
                    file.IsFile = true;
                    file.CreationTimeUtc = DateTime.Now;
                    file.LastAccessTimeUtc = DateTime.Now;
                    file.LastWriteTimeUtc = DateTime.Now;
                    file.CompressionMethodId = ZipEntryCompressionMethodId.Stored;
                    if (useDatadescriptor)
                        file.Flags = ZipDestinationEntryFlag.UseDataDescriptor;
                    WriteContentData(file, contentSize);
                }
            }

            try
            {
                using (var reader = zipArchive.OpenAsZipFile(ValidationStringency.Strict))
                {
                    Console.WriteLine($"検査中... \"{zipArchive.FullName}\"");
                    var entries = reader.EnumerateEntries();
                    var count = 0UL;
                    foreach (var entry in entries)
                    {
                        Console.WriteLine($"検査中 ({count + 1}/{numberOfEntries})... \"{zipArchive.FullName}\"");
                        VerifyContentData(entry);
                        checked
                        {
                            ++count;
                        }
                    }
                }

                Console.WriteLine($"検査終了");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                try
                {
                    Console.WriteLine($"検査中にエラーが発生しました。: {ex.Message}");
                }
                finally
                {
                    Console.ResetColor();
                }
            }
        }

        private static void WriteContentData(ZipDestinationEntry fileEntry, UInt64 contentLength)
        {
            const UInt64 BUFFER_LENGTH = 1024UL * 1024UL;

            ArgumentOutOfRangeException.ThrowIfLessThan(contentLength, (UInt64)(sizeof(UInt32) + sizeof(UInt64)));

            var crcHolder = new ValueHolder<(UInt32 crc, UInt64 length)>();
            using var outStream1 = fileEntry.CreateContentStream();
            var dataLength = checked(contentLength - (sizeof(UInt32) + sizeof(UInt64)));
            outStream1.WriteUInt64LE(dataLength);
            using (var outStream2 = outStream1.WithCrc32Calculation(crcHolder, true))
            {
                var buffer = new Byte[BUFFER_LENGTH];
                for (var index = 0; index < buffer.Length; ++index)
                    buffer[index] = unchecked((Byte)index);
                var remain = dataLength;
                while (remain > 0)
                {
                    var length = checked((Int32)remain.Minimum(BUFFER_LENGTH));
                    outStream2.WriteBytes(buffer, 0, length);
                    remain -= checked((UInt32)length);
                }
            }

            outStream1.WriteUInt32LE(crcHolder.Value.crc);
        }

        private static void VerifyContentData(ZipSourceEntry entry)
        {
            if (entry.IsFile && entry.Size > 0)
            {
                try
                {
                    var crcHolder = new ValueHolder<(UInt32 crc, UInt64 length)>();
                    using var inStream1 = entry.OpenContentStream();
                    var contentLength = inStream1.ReadUInt64LE();
                    using (var inStream2 = inStream1.WithCrc32Calculation(crcHolder, true))
                    {
                        var buffer = new Byte[1024 * 1024];
                        var count = 0UL;
                        while (count < contentLength)
                        {
                            var length = inStream2.ReadBytes(buffer.Slice(0, checked((Int32)(contentLength - count).Minimum((UInt64)buffer.Length))));
                            if (length <= 0)
                                throw new ApplicationException($"データが短すぎます。: 期待された長さ=0x{contentLength + sizeof(UInt64) + sizeof(UInt32):x16}, 実際の長さ=0x{count + sizeof(UInt64):x16}, entry={entry}");
                            checked
                            {
                                count += (UInt64)length;
                            }
                        }
                    }

                    var crc = inStream1.ReadUInt32LE();
                    if (crc != crcHolder.Value.crc)
                        throw new ApplicationException($"データの内容が一致しません。: entry={entry}");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    try
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}
