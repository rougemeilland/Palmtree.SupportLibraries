using System;
using System.Linq;
using Palmtree;
using Palmtree.Collections;
using Palmtree.IO;
using Palmtree.IO.Compression.Archive.Zip;
using Palmtree.IO.Compression.Stream.Plugin;

namespace Test.Compression
{
    internal sealed class Program
    {
        static Program()
        {
            StoredCoderPlugin.EnablePlugin();
            DeflateCoderPlugin.EnablePlugin();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:未使用のパラメーターを削除します", Justification = "非公開の内部コマンドのMainメソッドであり、将来パラメタが使用される可能性があるため。")]
        private static void Main(String[] args)
        {
            Console.WriteLine("**Stored**");
            DoTest1(3, 1024 * 1024, ZipEntryCompressionMethodId.Stored);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("**Deflate**");
            DoTest1(3, 1024 * 1024, ZipEntryCompressionMethodId.Deflate);

            Console.WriteLine("終了しました。");
            Console.Beep();
            _ = Console.ReadLine();
        }

        private static void DoTest1(Int32 numberOfEntries, UInt64 contentSize, ZipEntryCompressionMethodId compressMethodId)
        {
            var zipArchive = FilePath.CreateTemporaryFile();
            try
            {
                using (var zipWriter = zipArchive.CreateAsZipFile())
                {
                    for (var count = 1; count <= numberOfEntries; ++count)
                    {
                        Console.WriteLine($"書き込み中 {count}/{numberOfEntries}... \"{zipArchive.FullName}\"");
                        var file = zipWriter.CreateEntry($"ファイル{count}.bin");
                        file.IsFile = true;
                        file.CreationTimeUtc = DateTime.Now;
                        file.LastAccessTimeUtc = DateTime.Now;
                        file.LastWriteTimeUtc = DateTime.Now;
                        file.CompressionMethodId = compressMethodId;
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
            finally
            {
                zipArchive.Delete();
            }
        }

        private static void WriteContentData(ZipDestinationEntry fileEntry, UInt64 contentLength)
        {
            const Int32 BUFFER_LENGTH = 1024 * 1024;

            ArgumentOutOfRangeException.ThrowIfLessThan(contentLength, (UInt64)sizeof(UInt32) + sizeof(UInt64));

            var crcHolder = new ValueHolder<(UInt32 crc, UInt64 length)>();
            using var outStream1 = fileEntry.CreateContentStream(new SimpleProgress<(UInt64 inSize, UInt64 outSize)>(value => Console.WriteLine($"[Writing] in: {value.inSize:N0} bytes, out: {value.outSize:N0} bytes")));
            var dataLength = checked(contentLength - (sizeof(UInt32) + sizeof(UInt64)));
            outStream1.WriteUInt64LE(dataLength);
            using (var outStream2 = outStream1.WithCrc32Calculation(crcHolder, true))
            {
                var buffer = RandomSequence.GetByteSequence().Take(BUFFER_LENGTH).Select(b => (Byte)(b & 0x3f)).ToArray();
                var remain = dataLength;
                while (remain > 0)
                {
                    var length = checked((Int32)remain.Minimum((UInt64)BUFFER_LENGTH));
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
                    using var inStream1 = entry.OpenContentStream(new SimpleProgress<(UInt64 inSize, UInt64 outSize)>(value => Console.WriteLine($"[Reading] in: {value.inSize:N0} bytes, out: {value.outSize:N0} bytes")));
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
