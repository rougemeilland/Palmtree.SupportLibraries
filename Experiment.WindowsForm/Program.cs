using System;
using System.Text;
using Palmtree.Application;
using Palmtree.IO;

namespace Experiment.WindowsForm
{
    internal static partial class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Environment.CurrentDirectory = typeof(Program).Assembly.GetBaseDirectory().FullName;
            var launcher = new ConsoleApplicationLauncher("experiment", Encoding.UTF8);
            launcher.Launch(args);
        }
    }
}
