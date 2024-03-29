﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Palmtree.IO
{
    public static class AsyncFileExtensions
    {
        static AsyncFileExtensions()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static Task<Byte[]> ReadAllBytesAsync(this FileInfo file, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return File.ReadAllBytesAsync(file.FullName, cancellationToken);
        }

        public static Task<Byte[]> ReadAllBytesAsync(this FilePath file, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            return File.ReadAllBytesAsync(file.FullName, cancellationToken);
        }

        public static Task<String[]> ReadAllLinesAsync(this FileInfo file, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return File.ReadAllLinesAsync(file.FullName, cancellationToken);
        }

        public static Task<String[]> ReadAllLinesAsync(this FilePath file, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return File.ReadAllLinesAsync(file.FullName, cancellationToken);
        }

        public static IAsyncEnumerable<String> ReadLinesAsync(this FileInfo file, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return File.ReadLinesAsync(file.FullName, cancellationToken);
        }

        public static IAsyncEnumerable<String> ReadLinesAsync(this FilePath file, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            return File.ReadLinesAsync(file.FullName, cancellationToken);
        }

        public static IAsyncEnumerable<String> ReadLinesAsync(this FileInfo file, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return File.ReadLinesAsync(file.FullName, encoding, cancellationToken);
        }

        public static IAsyncEnumerable<String> ReadLinesAsync(this FilePath file, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            return File.ReadLinesAsync(file.FullName, encoding, cancellationToken);
        }

        public static async Task WriteAllBytesAsync(this FileInfo file, IEnumerable<Byte> data, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var stream = file.OpenWrite();
            await using (stream.ConfigureAwait(false))
            {
                await stream.WriteByteSequenceAsync(data, cancellationToken).ConfigureAwait(false);
            }
        }

        public static async Task WriteAllBytesAsync(this FilePath file, IEnumerable<Byte> data, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var stream = file.OpenWrite();
            await using (stream.ConfigureAwait(false))
            {
                await stream.WriteByteSequenceAsync(data, cancellationToken).ConfigureAwait(false);
            }
        }

        public static async Task WriteAllBytesAsync(this FileInfo file, IAsyncEnumerable<Byte> data, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var stream = file.OpenWrite();
            await using (stream.ConfigureAwait(false))
            {
                await stream.WriteByteSequenceAsync(data, cancellationToken).ConfigureAwait(false);
            }
        }

        public static async Task WriteAllBytesAsync(this FilePath file, IAsyncEnumerable<Byte> data, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            var stream = file.OpenWrite();
            await using (stream.ConfigureAwait(false))
            {
                await stream.WriteByteSequenceAsync(data, cancellationToken).ConfigureAwait(false);
            }
        }

        public static async Task WriteAllBytesAsync(this FileInfo file, Byte[] data, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            await File.WriteAllBytesAsync(file.FullName, data, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllBytesAsync(this FilePath file, Byte[] data, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            await File.WriteAllBytesAsync(file.FullName, data, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllBytesAsync(this FileInfo file, ReadOnlyMemory<Byte> data, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            var stream = file.OpenWrite();
            await using (stream.ConfigureAwait(false))
            {
                await stream.WriteBytesAsync(data, cancellationToken).ConfigureAwait(false);
            }
        }

        public static async Task WriteAllBytesAsync(this FilePath file, ReadOnlyMemory<Byte> data, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            var stream = file.OpenWrite();
            await using (stream.ConfigureAwait(false))
            {
                await stream.WriteBytesAsync(data, cancellationToken).ConfigureAwait(false);
            }
        }

        public static async Task WriteAllTextAsync(this FileInfo file, String text, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            await File.WriteAllTextAsync(file.FullName, text, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllTextAsync(this FilePath file, String text, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            await File.WriteAllTextAsync(file.FullName, text, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllTextAsync(this FileInfo file, String text, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (text is null)
                throw new ArgumentNullException(nameof(text));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            await File.WriteAllTextAsync(file.FullName, text, encoding, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllTextAsync(this FilePath file, String text, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (text is null)
                throw new ArgumentNullException(nameof(text));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            await File.WriteAllTextAsync(file.FullName, text, encoding, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllLinesAsync(this FileInfo file, IEnumerable<String> lines, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));

            await File.WriteAllLinesAsync(file.FullName, lines, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllLinesAsync(this FilePath file, IEnumerable<String> lines, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));

            await File.WriteAllLinesAsync(file.FullName, lines, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllTextAsync(this FileInfo file, IEnumerable<String> lines, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            await File.WriteAllLinesAsync(file.FullName, lines, encoding, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllTextAsync(this FilePath file, IEnumerable<String> lines, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            await File.WriteAllLinesAsync(file.FullName, lines, encoding, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllLinesAsync(this FileInfo file, IAsyncEnumerable<String> lines, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));

            await InternalWriteAllLinesAsync(file.FullName, lines, Encoding.UTF8, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllLinesAsync(this FilePath file, IAsyncEnumerable<String> lines, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));

            await InternalWriteAllLinesAsync(file.FullName, lines, Encoding.UTF8, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllTextAsync(this FileInfo file, IAsyncEnumerable<String> lines, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            await InternalWriteAllLinesAsync(file.FullName, lines, Encoding.UTF8, cancellationToken).ConfigureAwait(false);
        }

        public static async Task WriteAllTextAsync(this FilePath file, IAsyncEnumerable<String> lines, Encoding encoding, CancellationToken cancellationToken = default)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            if (lines is null)
                throw new ArgumentNullException(nameof(lines));
            if (encoding is null)
                throw new ArgumentNullException(nameof(encoding));

            await InternalWriteAllLinesAsync(file.FullName, lines, Encoding.UTF8, cancellationToken).ConfigureAwait(false);
        }

        public static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this FileInfo sourceFile, CancellationToken cancellationToken = default)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));

            return sourceFile.OpenRead().CalculateCrc24Async(cancellationToken);
        }

        public static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this FilePath sourceFile, CancellationToken cancellationToken = default)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));

            return sourceFile.OpenRead().CalculateCrc24Async(cancellationToken);
        }

        public static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this FileInfo sourceFile, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));

            return sourceFile.OpenRead().CalculateCrc24Async(progress, cancellationToken);
        }

        public static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc24Async(this FilePath sourceFile, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));

            return sourceFile.OpenRead().CalculateCrc24Async(progress, cancellationToken);
        }

        public static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this FileInfo sourceFile, CancellationToken cancellationToken = default)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));

            return sourceFile.OpenRead().CalculateCrc32Async(cancellationToken);
        }

        public static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this FilePath sourceFile, CancellationToken cancellationToken = default)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));

            return sourceFile.OpenRead().CalculateCrc32Async(cancellationToken);
        }

        public static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this FileInfo sourceFile, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));

            return sourceFile.OpenRead().CalculateCrc32Async(progress, cancellationToken);
        }

        public static Task<(UInt32 Crc, UInt64 Length)> CalculateCrc32Async(this FilePath sourceFile, IProgress<UInt64>? progress, CancellationToken cancellationToken = default)
        {
            if (sourceFile is null)
                throw new ArgumentNullException(nameof(sourceFile));

            return sourceFile.OpenRead().CalculateCrc32Async(progress, cancellationToken);
        }

#if NET8_0_OR_GREATER
#else
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2016:'CancellationToken' パラメーターをメソッドに転送する", Justification = "<保留中>")]
#endif
        private static async Task InternalWriteAllLinesAsync(String fileFullPath, IAsyncEnumerable<String> lines, Encoding encoding, CancellationToken cancellationToken)
        {
            var writer = new StreamWriter(fileFullPath, false, encoding);
            await using (writer.ConfigureAwait(false))
            {
                var enumerator = lines.GetAsyncEnumerator(cancellationToken);
                await using (enumerator.ConfigureAwait(false))
                {
                    while (!await enumerator.MoveNextAsync().ConfigureAwait(false))
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        writer.WriteLine(enumerator.Current);
                    }
                }

#if NET8_0_OR_GREATER
                await writer.FlushAsync(cancellationToken).ConfigureAwait(false);
#else
                await writer.FlushAsync().ConfigureAwait(false);
#endif
            }
        }
    }
}
