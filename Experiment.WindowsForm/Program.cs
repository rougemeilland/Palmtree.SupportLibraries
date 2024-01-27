using System;
using System.Text;
using Palmtree;
using Palmtree.IO.Console;

namespace Experiment.WindowsForm
{
    internal static partial class Program
    {
        private class DesctopApplication
            : WindowsDesktopApplication
        {
            private readonly Func<int> _main;

            public DesctopApplication(Func<int> main)
            {
                _main = main;
            }

            protected override int Main()
            {
                var exitCode = _main();
                TinyConsole.Beep();
                _ = TinyConsole.ReadLine();
                return exitCode;
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            var application = new DesctopApplication(() => CSharp.Program.Launch("experiment for desktop", Encoding.UTF8, args));
            _ = application.Run();
        }
    }
}
