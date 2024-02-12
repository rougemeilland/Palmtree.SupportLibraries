using System.Linq;
using Palmtree.IO;
using Palmtree.IO.Console;
using Palmtree.IO.Serialization;

namespace Experiment.CSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var rows = CsvSerializer.Deserialize(typeof(Program).Assembly.GetBaseDirectory().GetFile("data.csv").ReadAllText());
            foreach (var row in rows)
            {
                var columns = row.ToArray();
                if (columns.Length >= 6)
                {
                    TinyConsole.WriteLine(columns[0]);
                    TinyConsole.WriteLine(columns[1]);
                    TinyConsole.WriteLine(columns[2]);
                    TinyConsole.WriteLine(columns[3]);
                    TinyConsole.WriteLine($"発売日:{columns[4]}, {columns[5]}");
                    TinyConsole.WriteLine();
                }
            }

            TinyConsole.WriteLine();

            foreach (var row in rows)
            {
                var columns = row.ToArray();
                if (columns.Length >= 6)
                {
                    TinyConsole.WriteLine($"タイトル: {columns[0]}");
                    TinyConsole.WriteLine($"著者: {columns[1]}");
                    TinyConsole.WriteLine($"出版社: {columns[2]}");
                    TinyConsole.WriteLine($"レーベル: {columns[3]}");
                    TinyConsole.WriteLine($"発売日: {columns[4]}");
                    TinyConsole.WriteLine($"その他: {columns[5]}");
                    TinyConsole.WriteLine();
                }
            }

            TinyConsole.Beep();
            _ = TinyConsole.ReadLine();
        }
    }
}
