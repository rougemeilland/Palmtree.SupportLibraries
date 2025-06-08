using System;
using System.Diagnostics.CodeAnalysis;
using Palmtree.IO;
using Palmtree.IO.Console;

namespace Experiment.CSharp
{
    internal sealed partial class Program
    {
        [SuppressMessage("Style", "IDE0060:未使用のパラメーターを削除します", Justification = "非公開の内部コマンドのMainメソッドであり、将来パラメタが使用される可能性があるため。")]
        private static void Main(string[] args)
        {
            TinyConsole.WriteLine($"default input encoding: {TinyConsole.InputEncoding.GetType().FullName} ({TinyConsole.InputEncoding.CodePage})");
            TinyConsole.WriteLine($"default output encoding: {TinyConsole.OutputEncoding.GetType().FullName} ({TinyConsole.OutputEncoding.CodePage})");
            TinyConsole.DefaultTextWriter = ConsoleTextWriterType.StandardError;
            TinyConsole.InputEncoding = System.Text.Encoding.UTF8;
            TinyConsole.OutputEncoding = System.Text.Encoding.UTF8;
            var tempFile = FilePath.CreateTemporaryFile();
            try
            {
                using (var logWriter = tempFile.Create())
                {
                    var originalStandardOutput = TinyConsole.StandardOutput;
                    var originalStandardError = TinyConsole.StandardError;
                    TinyConsole.StandardOutput = originalStandardOutput.WithBranch(logWriter);
                    TinyConsole.StandardError = originalStandardError.WithBranch(logWriter);
                    TinyConsole.Out.WriteLine("これは標準出力に出力されています。(1)");
                    TinyConsole.Error.WriteLine("これは標準エラー出力に出力されています。(1)");
                    TinyConsole.WriteLine("これは既定の出力先に出力されています。(1)");
                    TinyConsole.StandardOutput = originalStandardOutput;
                    TinyConsole.StandardError = originalStandardError;
                    TinyConsole.Out.WriteLine("これは標準出力に出力されています。(2)");
                    TinyConsole.Error.WriteLine("これは標準エラー出力に出力されています。(2)");
                    TinyConsole.WriteLine("これは既定の出力先に出力されています。(2)");
                }

                TinyConsole.WriteLine("---- ここから先はファイルの内容 -----");

                TinyConsole.Write (tempFile.ReadAllText(TinyConsole.OutputEncoding));
            }
            finally
            {
                tempFile.Delete();
            }

            // TODO: TinyConsole.StandatdInputなどの getter setter のテスト 
            // TODO: IDirectDotNetStreamWrapper のテスト
            // TODO: コンソールで WithBranch のテスト

            Console.Beep();
            Console.WriteLine("Complete");
            _ = Console.ReadLine();
        }
    }
}
