using System;
using System.Linq;
using Palmtree.IO;
using Palmtree.IO.Compression.Archive.Zip;

namespace Test.Zip.Timestamp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TODO: エントリのタイムスタンプのテストコードを書く。
            // 組み合わせ
            // ・LastWrite/LastAccess/Creation
            // ・Utc/Local
            // ・null/非null
            // ・DateTime/DateTimeOffset (書き込み時)
            // ・DateTime/DateTimeOffset (読み込み時)

            var random = new Random();

            foreach (var high_precision_timestamp in new[] { true, false })
            {
                foreach (var w_isNull in high_precision_timestamp ? new[] { true, false } : new[] { true })
                {
                    foreach (var w_isLocal in new[] { true, false })
                    {
                        foreach (var w_outDateTime in new[] { true, false })
                        {
                            foreach (var w_inDateTime in new[] { true, false })
                            {
                                foreach (var a_isNull in high_precision_timestamp ? new[] { true, false } : new[] { true })
                                {
                                    foreach (var a_isLocal in new[] { true, false })
                                    {
                                        foreach (var a_outDateTime in new[] { true, false })
                                        {
                                            foreach (var a_inDateTime in new[] { true, false })
                                            {
                                                foreach (var c_isNull in high_precision_timestamp ? new[] { true, false } : new[] { true })
                                                {
                                                    foreach (var c_isLocal in new[] { true, false })
                                                    {
                                                        foreach (var c_outDateTime in new[] { true, false })
                                                        {
                                                            foreach (var c_inDateTime in new[] { true, false })
                                                            {
                                                                var now = DateTimeOffset.UtcNow;
                                                                var lastWriteTimeOffset = !w_isNull ? now.AddDays(-3 * random.NextDouble()) : (DateTimeOffset?)null;
                                                                var lastAccessTimeOffset = !a_isNull ? now.AddDays(-3 * random.NextDouble()) : (DateTimeOffset?)null;
                                                                var creationTimeOffset = !c_isNull ? now.AddDays(-3 * random.NextDouble()) : (DateTimeOffset?)null;
                                                                var lastWriteTime = lastWriteTimeOffset?.UtcDateTime;
                                                                var lastAccessTime = lastAccessTimeOffset?.UtcDateTime;
                                                                var creationTime = creationTimeOffset?.UtcDateTime;
                                                                var ok_ntfs_timestamp = !w_isNull && !a_isNull && !c_isNull;
                                                                var ok_unix_timestamp = !w_isNull || !a_isNull || !c_isNull;

                                                                var baseDirectory = new DirectoryPath(args[0]);
                                                                var zipPath = baseDirectory.GetFile("test.zip");
                                                                try
                                                                {
                                                                    using (var zipWriter = zipPath.CreateAsZipFile())
                                                                    {
                                                                        var entry = zipWriter.CreateEntry("test.txt");

                                                                        if (!high_precision_timestamp)
                                                                        {
                                                                            entry.Flags |= ZipDestinationEntryFlag.DisableNtfsHighPrecisionTimestamp;
                                                                            entry.Flags |= ZipDestinationEntryFlag.DisableUnixHighPrecisionTimestamp;
                                                                        }

                                                                        if (w_outDateTime)
                                                                            entry.LastWriteTimeUtc = w_isLocal ? lastWriteTime?.ToLocalTime() : lastWriteTime;
                                                                        else
                                                                            entry.LastWriteTimeOffsetUtc = w_isLocal ? lastWriteTimeOffset?.ToLocalTime() : lastWriteTimeOffset;

                                                                        if (a_outDateTime)
                                                                            entry.LastAccessTimeUtc = a_isLocal ? lastAccessTime?.ToLocalTime() : lastAccessTime;
                                                                        else
                                                                            entry.LastAccessTimeOffsetUtc = a_isLocal ? lastAccessTimeOffset?.ToLocalTime() : lastAccessTimeOffset;

                                                                        if (c_outDateTime)
                                                                            entry.CreationTimeUtc = c_isLocal ? creationTime?.ToLocalTime() : creationTime;
                                                                        else
                                                                            entry.CreationTimeOffsetUtc = c_isLocal ? creationTimeOffset?.ToLocalTime() : creationTimeOffset;

                                                                        using (var content = entry.CreateContentStream())
                                                                        using (var contentWriter = content.AsTextWriter())
                                                                        {
                                                                            contentWriter.Write("こんにちは。");
                                                                        }

                                                                        zipWriter.Close();
                                                                    }

                                                                    using (var zipReader = zipPath.OpenAsZipFile())
                                                                    {
                                                                        var entry = zipReader.EnumerateEntries().Where(entry => entry.FullName == "test.txt").Single();

                                                                        if (w_inDateTime)
                                                                        {
                                                                            if (w_isNull)
                                                                            {
                                                                                if (!NearyEqual(entry.LastWriteTimeUtc, now.UtcDateTime, TimeSpan.FromSeconds(2)))
                                                                                    throw new Exception();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (entry.LastWriteTimeUtc is null)
                                                                                    throw new Exception();
                                                                                if (ok_ntfs_timestamp && !NearyEqual(entry.LastWriteTimeUtc, lastWriteTime, TimeSpan.Zero))
                                                                                    throw new Exception();
                                                                                if (ok_unix_timestamp && !NearyEqual(entry.LastWriteTimeUtc, lastWriteTime, TimeSpan.FromSeconds(1)))
                                                                                    throw new Exception();
                                                                                if (!NearyEqual(entry.LastWriteTimeUtc, lastWriteTime, TimeSpan.FromSeconds(2)))
                                                                                    throw new Exception();
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (w_isNull)
                                                                            {
                                                                                if (!NearyEqual(entry.LastWriteTimeUtc, now, TimeSpan.FromSeconds(2)))
                                                                                    throw new Exception();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (entry.LastWriteTimeOffsetUtc is null)
                                                                                    throw new Exception();
                                                                                if (ok_ntfs_timestamp && !NearyEqual(entry.LastWriteTimeOffsetUtc, lastWriteTimeOffset, TimeSpan.Zero))
                                                                                    throw new Exception();
                                                                                if (ok_unix_timestamp && !NearyEqual(entry.LastWriteTimeOffsetUtc, lastWriteTimeOffset, TimeSpan.FromSeconds(1)))
                                                                                    throw new Exception();
                                                                                if (!NearyEqual(entry.LastWriteTimeOffsetUtc, lastWriteTimeOffset, TimeSpan.FromSeconds(2)))
                                                                                    throw new Exception();
                                                                            }
                                                                        }

                                                                        if (a_inDateTime)
                                                                        {
                                                                            if (a_isNull)
                                                                            {
                                                                                if (entry.LastAccessTimeUtc is not null)
                                                                                    throw new Exception();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (entry.LastAccessTimeUtc is null)
                                                                                    throw new Exception();
                                                                                if (ok_ntfs_timestamp && !NearyEqual(entry.LastAccessTimeUtc, lastAccessTime, TimeSpan.Zero))
                                                                                    throw new Exception();
                                                                                if (ok_unix_timestamp && !NearyEqual(entry.LastAccessTimeUtc, lastAccessTime, TimeSpan.FromSeconds(1)))
                                                                                    throw new Exception();
                                                                                if (!NearyEqual(entry.LastAccessTimeUtc, lastAccessTime, TimeSpan.FromSeconds(2)))
                                                                                    throw new Exception();
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (a_isNull)
                                                                            {
                                                                                if (entry.LastAccessTimeOffsetUtc is not null)
                                                                                    throw new Exception();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (entry.LastAccessTimeOffsetUtc is null)
                                                                                    throw new Exception();
                                                                                if (ok_ntfs_timestamp && !NearyEqual(entry.LastAccessTimeOffsetUtc, lastAccessTimeOffset, TimeSpan.Zero))
                                                                                    throw new Exception();
                                                                                if (ok_unix_timestamp && !NearyEqual(entry.LastAccessTimeOffsetUtc, lastAccessTimeOffset, TimeSpan.FromSeconds(1)))
                                                                                    throw new Exception();
                                                                                if (!NearyEqual(entry.LastAccessTimeOffsetUtc, lastAccessTimeOffset, TimeSpan.FromSeconds(2)))
                                                                                    throw new Exception();
                                                                            }
                                                                        }

                                                                        if (c_inDateTime)
                                                                        {
                                                                            if (c_isNull)
                                                                            {
                                                                                if (entry.CreationTimeUtc is not null)
                                                                                    throw new Exception();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (entry.CreationTimeUtc is null)
                                                                                    throw new Exception();
                                                                                if (ok_ntfs_timestamp && !NearyEqual(entry.CreationTimeUtc, creationTime, TimeSpan.Zero))
                                                                                    throw new Exception();
                                                                                if (ok_unix_timestamp && !NearyEqual(entry.CreationTimeUtc, creationTime, TimeSpan.FromSeconds(1)))
                                                                                    throw new Exception();
                                                                                if (!NearyEqual(entry.CreationTimeUtc, creationTime, TimeSpan.FromSeconds(2)))
                                                                                    throw new Exception();
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            if (c_isNull)
                                                                            {
                                                                                if (entry.CreationTimeOffsetUtc is not null)
                                                                                    throw new Exception();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (entry.CreationTimeOffsetUtc is null)
                                                                                    throw new Exception();
                                                                                if (ok_ntfs_timestamp && !NearyEqual(entry.CreationTimeOffsetUtc, creationTimeOffset, TimeSpan.Zero))
                                                                                    throw new Exception();
                                                                                if (ok_unix_timestamp && !NearyEqual(entry.CreationTimeOffsetUtc, creationTimeOffset, TimeSpan.FromSeconds(1)))
                                                                                    throw new Exception();
                                                                                if (!NearyEqual(entry.CreationTimeOffsetUtc, creationTimeOffset, TimeSpan.FromSeconds(2)))
                                                                                    throw new Exception();
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                finally
                                                                {
                                                                    zipPath.SafetyDelete();
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
                    }
                }
            }

            Console.WriteLine("OK");
            Console.Beep();
            _ = Console.ReadLine();
        }

        private static bool NearyEqual(DateTime? x, DateTime? y, TimeSpan delta)
        {
            if (x is null)
            {
                return y is null;
            }
            else if (y is null)
            {
                return false;
            }
            else if (x.Value.Kind == DateTimeKind.Unspecified || y.Value.Kind == DateTimeKind.Unspecified)
            {
                throw new Exception();
            }
            else
            {
                var xValue = x.Value.ToUniversalTime();
                var yValue = y.Value.ToUniversalTime();
                if (xValue >= yValue)
                    return xValue - yValue <= delta;
                else
                    return yValue - xValue <= delta;
            }
        }

        private static bool NearyEqual(DateTimeOffset? x, DateTimeOffset? y, TimeSpan delta)
        {
            if (x is null)
            {
                return y is null;
            }
            else if (y is null)
            {
                return false;
            }
            else
            {
                var xValue = x.Value.ToUniversalTime();
                var yValue = y.Value.ToUniversalTime();
                if (xValue >= yValue)
                    return xValue - yValue <= delta;
                else
                    return yValue - xValue <= delta;
            }
        }
    }
}
