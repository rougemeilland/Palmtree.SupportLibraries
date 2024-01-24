using System;
using System.Text;
using Palmtree.IO;
using Palmtree.IO.Compression.Archive.Zip;
using Palmtree.IO.Compression.Stream.Plugin;
using Palmtree.IO.Console;

namespace Experiment.CSharp
{
    internal class Program
    {
        //private static readonly ReadOnlyMemory<byte> _entryNameBytes = new byte[] { 0xed, 0x40, 0xed, 0x41, 0xed, 0x42 };
        private static readonly ReadOnlyMemory<byte> _entryNameBytes = new byte[] { 0x41, 0x42, 0x43 };
        private static readonly Encoding _shiftJisEncoding;

        static Program()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _shiftJisEncoding = Encoding.GetEncoding("shift_jis");

            DeflateCoderPlugin.EnablePlugin();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:未使用のパラメーターを削除します", Justification = "<保留中>")]
        static void Main(string[] args)
        {
            var baseDirectory = new DirectoryPath(args[0]);
            using (var zipWriter = baseDirectory.GetFile("normal.zip").CreateAsZipFile())
            {
                var entry = zipWriter.CreateEntry(_shiftJisEncoding.GetString(_entryNameBytes.Span), _entryNameBytes, "", ReadOnlyMemory<byte>.Empty, Encoding.ASCII, Array.Empty<Encoding>());
                entry.IsFile = true;
                entry.CreationTimeUtc = DateTime.UtcNow;
                entry.CompressionLevel = ZipEntryCompressionLevel.Maximum;
                entry.CompressionMethodId = ZipEntryCompressionMethodId.Stored;
                entry.LastAccessTimeUtc = DateTime.UtcNow;
                entry.LastWriteTimeUtc = DateTime.UtcNow;
                entry.Flags = ZipDestinationEntryFlag.DoNotUseExtraFieldsInLocalHeaders;
                var buffer = new byte[1024 * 1024];
                using var contentStream = entry.CreateContentStream();
                var limit = ((long)uint.MaxValue + 1) / buffer.Length;
                for (var count = 0; count < limit; ++count)
                    contentStream.WriteBytes(buffer);
            }

            TinyConsole.Beep();
            _ = TinyConsole.ReadLine();
        }
    }
}
