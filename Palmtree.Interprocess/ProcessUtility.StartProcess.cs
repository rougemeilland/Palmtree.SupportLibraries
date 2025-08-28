using System;
using System.Buffers;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Palmtree.IO;
using Palmtree.IO.Console;

namespace Palmtree.Interprocess
{
    public static partial class ProcessUtility
    {
        private sealed class BinaryInputRedirector
            : IChildProcessInputRedirectable
        {
            private readonly ISequentialInputByteStream _inputStream;

            public BinaryInputRedirector(ISequentialInputByteStream inputStream)
            {
                _inputStream = inputStream;
            }

            async Task IChildProcessInputRedirectable.RedirectInputAsync(StreamWriter writer, CancellationToken cancellationToken)
            {
                try
                {
                    writer.Flush();
                    await _inputStream.AsDotNetStream().CopyToAsync(writer.BaseStream, cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    writer.Dispose();
                }
            }
        }

        private sealed class TextInputRedirector
            : IChildProcessInputRedirectable
        {
            private readonly TextReader _textReader;

            public TextInputRedirector(TextReader textReader)
            {
                _textReader = textReader;
            }

            async Task IChildProcessInputRedirectable.RedirectInputAsync(StreamWriter writer, CancellationToken cancellationToken)
            {
                try
                {
                    await _textReader.CopyToAsync(writer, cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    writer.Dispose();
                }
            }
        }

        private sealed class NullInputRedirector
            : IChildProcessInputRedirectable
        {
            public NullInputRedirector()
            {
            }

            Task IChildProcessInputRedirectable.RedirectInputAsync(StreamWriter writer, CancellationToken cancellationToken)
            {
                writer.Dispose();
                return Task.CompletedTask;
            }
        }

        private sealed class BinaryOutputRedirector
            : IChildProcessOutputRedirectable
        {
            private readonly ISequentialOutputByteStream _outStream;

            public BinaryOutputRedirector(ISequentialOutputByteStream outStream)
            {
                _outStream = outStream;
            }

            async Task IChildProcessOutputRedirectable.RedirectOutputAsync(StreamReader reader, CancellationToken cancellationToken)
            {
                try
                {
                    await reader.BaseStream.CopyToAsync(_outStream.AsDotNetStream(), cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    reader.Dispose();
                }
            }
        }

        private sealed class TextOutputRedirector
            : IChildProcessOutputRedirectable
        {
            private readonly TextWriter _textWriter;

            public TextOutputRedirector(TextWriter textWriter)
            {
                _textWriter = textWriter;
            }

            async Task IChildProcessOutputRedirectable.RedirectOutputAsync(StreamReader reader, CancellationToken cancellationToken)
            {
                try
                {
                    await reader.CopyToAsync(_textWriter, cancellationToken).ConfigureAwait(false);
                }
                finally
                {
                    reader.Dispose();
                }
            }
        }

        private sealed class NullOutputRedirector
            : IChildProcessOutputRedirectable
        {
            public NullOutputRedirector()
            {
            }

            async Task IChildProcessOutputRedirectable.RedirectOutputAsync(StreamReader reader, CancellationToken cancellationToken)
            {
                var buffer = ArrayPool<Char>.Shared.Rent(_IO_CHAR_BUFFER_SIZE);
                try
                {
                    while (await reader.ReadAsync(buffer, cancellationToken).ConfigureAwait(false) > 0)
                    {
                    }
                }
                finally
                {
                    ArrayPool<Char>.Shared.Return(buffer);
                }
            }
        }

        private const Int32 _IO_CHAR_BUFFER_SIZE = 256;
        private const Int32 _IO_BYTE_BUFFER_SIZE = 8 * 1024;
        private static readonly Encoding _defaultInputOutputEncoding = new UTF8Encoding(false);

        public static Task StartProcessAsync(
            String commandLine,
            Encoding? intpuEncoding,
            Encoding? outputEncoding,
            IChildProcessInputRedirectable? standardInputRedirector,
            IChildProcessOutputRedirectable? standardOutputRedirector,
            IChildProcessOutputRedirectable? standardErrorRedirector,
            IValidationLogger? logWriter,
            CancellationToken cancellationToken = default)
        {
            var args = commandLine.SplitCommandLineArguments().Select(element => element.arg.DecodeCommandLineArgument()).ToArray();
            if (args.Length <= 0)
                throw new ArgumentException("Empty command line.", nameof(commandLine));
            var commandFilePath =
                WhereIs(args[0])
                ?? throw new ArgumentException($"Command not found.: \"{args[0]}\"", nameof(commandLine));
            return StartProcessAsync(
                        commandFilePath,
                        String.Join(" ", args.Skip(1).Select(arg => arg.EncodeCommandLineArgument())),
                        intpuEncoding,
                        outputEncoding,
                        standardInputRedirector,
                        standardOutputRedirector,
                        standardErrorRedirector,
                        logWriter,
                        cancellationToken);
        }

        public static Task StartProcessAsync(
            FilePath programFile,
            ReadOnlyMemory<String> arguments,
            Encoding? intpuEncoding,
            Encoding? outputEncoding,
            IChildProcessInputRedirectable? standardInputRedirector,
            IChildProcessOutputRedirectable? standardOutputRedirector,
            IChildProcessOutputRedirectable? standardErrorRedirector,
            IValidationLogger? logWriter,
            CancellationToken cancellationToken = default)
            => StartProcessAsync(
                programFile,
                String.Join(" ", arguments.GetSequence().Select(arg => arg.EncodeCommandLineArgument())),
                intpuEncoding,
                outputEncoding,
                standardInputRedirector,
                standardOutputRedirector,
                standardErrorRedirector,
                logWriter,
                cancellationToken);

        public static async Task StartProcessAsync(
            FilePath programFile,
            String arguments,
            Encoding? intpuEncoding,
            Encoding? outputEncoding,
            IChildProcessInputRedirectable? standardInputRedirector,
            IChildProcessOutputRedirectable? standardOutputRedirector,
            IChildProcessOutputRedirectable? standardErrorRedirector,
            IValidationLogger? logWriter,
            CancellationToken cancellationToken = default)
        {
            var commandLine =
                arguments.Length > 0
                ? $"{programFile.FullName} {arguments}"
                : programFile.FullName;

            standardInputRedirector ??= new BinaryInputRedirector(TinyConsole.StandatdInput);
            standardOutputRedirector ??= new BinaryOutputRedirector(TinyConsole.StandardOutput);
            standardErrorRedirector ??= new BinaryOutputRedirector(TinyConsole.StandardError);
#if DEBUG
            Validation.Assert(standardInputRedirector is not null);
            Validation.Assert(standardOutputRedirector is not null);
            Validation.Assert(standardErrorRedirector is not null);
#endif
            var processStartInfo =
                new ProcessStartInfo
                {
                    Arguments = arguments,
                    FileName = programFile.FullName,
                    CreateNoWindow = false,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    StandardErrorEncoding = intpuEncoding ?? _defaultInputOutputEncoding,
                    StandardInputEncoding = outputEncoding ?? _defaultInputOutputEncoding,
                    StandardOutputEncoding = outputEncoding ?? _defaultInputOutputEncoding,
                    UseShellExecute = false,
                };
            var process =
                Process.Start(processStartInfo)
                ?? throw new ApplicationException("Could not start process");
            try
            {
                logWriter?.WriteLog(LogCategory.Information, $"Child process started.: id={process.Id} \"{process.StartInfo.FileName}\" {process.StartInfo.Arguments}");
                await Task.WhenAll(
                    standardInputRedirector.RedirectInputAsync(process.StandardInput, cancellationToken),
                    standardOutputRedirector.RedirectOutputAsync(process.StandardOutput, cancellationToken),
                    standardErrorRedirector.RedirectOutputAsync(process.StandardError, cancellationToken),
                    process.WaitForExitAsync(cancellationToken))
                    .ConfigureAwait(false);
                logWriter?.WriteLog(LogCategory.Information, $"Child process exited.: id={process.Id}, process-total-time={process.TotalProcessorTime.TotalSeconds:F2}[sec], commandLine=({commandLine})");
                if (process.ExitCode != 0)
                    throw new ApplicationException($"The process terminated abnormally: exitCode={process.ExitCode}, commandLine=({commandLine})");
            }
            finally
            {
                process.Dispose();
            }
        }

        public static IChildProcessInputRedirectable GetBinaryInputRedirector(ISequentialInputByteStream inStream)
           => new BinaryInputRedirector(inStream);

        public static IChildProcessInputRedirectable GetTextInputRedirector(TextReader reader)
           => new TextInputRedirector(reader);

        public static IChildProcessInputRedirectable GetNullInputRedirector()
           => new NullInputRedirector();

        public static IChildProcessOutputRedirectable GetBinaryOutputRedirector(ISequentialOutputByteStream outStream)
           => new BinaryOutputRedirector(outStream);

        public static IChildProcessOutputRedirectable GetTextOutputRedirector(TextWriter writer)
           => new TextOutputRedirector(writer);

        public static IChildProcessOutputRedirectable GetNullOutputRedirector()
           => new NullOutputRedirector();
    }
}
