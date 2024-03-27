using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using Palmtree.IO;

namespace Palmtree.Application
{
    /// <summary>
    /// コンソールアプリケーションをシェル (コマンドプロンプト) 経由で起動するためのクラスです。
    /// </summary>
    public class ConsoleApplicationLauncher
    {
        private const String _COMMAND_PROMPT_COMMAND_NAME = "cmd";
        private const String _ENVIRONMENT_VARIABLE_LAUNCHED_BY_THIS_LAUNCHER = "__LAUNCHED_BY_PALMTREE_APPLICATION_CONSOLE_APPLICATION_LAUNCHER";
        private const String _ENVIRONMENT_VALUE_LAUNCHED_BY_THIS_LAUNCHER = "1";
        private const String _PATH_ENVIRONMENT_VARIABLE_NAME = "PATH";
        private readonly String _commandName;
        private readonly Encoding _encoding;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="commandName">
        /// コンソールアプリケーションの名前を示す <see cref="String"/> オブジェクトです。
        /// </param>
        /// <param name="encoding">
        /// 起動するコンソールアプリケーションの入出力のエンコーディングを示す <see cref="Encoding"/> オブジェクトです。
        /// </param>
        /// <remarks>
        /// <para>
        /// <paramref name="commandName"/> の値は、例えば以下のように、シェル (コマンドプロンプト) が識別できる文字列である必要があります。
        /// </para>
        /// <list type="bullet">
        /// <item>コンソールアプリケーションの実行可能ファイルのフルパス名、またはカレントディレクトリからの相対パス名。(いずれも拡張子は省略可能)</item>
        /// <item>コンソールアプリケーションが配置されているディレクトリが PATH 環境変数に設定されている場合は、コンソールアプリケーションのファイル名。(拡張子は省略可)</item>
        /// </list>
        /// </remarks>
        public ConsoleApplicationLauncher(String commandName, Encoding encoding)
        {
            _commandName = commandName;
            _encoding = encoding;
        }

        /// <summary>
        /// コンソールアプリケーションを起動します。
        /// </summary>
        /// <param name="args">
        /// コンソールアプリケーションに渡す引数を示す <see cref="String"/> の配列です。
        /// </param>
        /// <param name="keepShellRunning">
        /// コンソールアプリケーションが終了した後もシェル (コマンドプロンプト) を実行中のままにする場合は true、そうではない場合は false です。既定値は false であり、この場合はコンソールアプリケーションが終了するとシェル (コマンドプロンプト) も終了します。
        /// </param>
        /// <param name="baseDirectory">
        /// コンソールアプリケーションの実行可能ファイルがあるディレクトリです。null を指定した場合にはカレントディレクトリとみなされます。
        /// </param>
        [SupportedOSPlatform("windows")]
        public void Launch(String[] args, Boolean keepShellRunning = false, DirectoryPath? baseDirectory = null)
        {
            var commandParameters =
                String.Concat(
                    args
                    .Select(arg => $" {arg.CommandPromptCommandLineArgumentEncode()}"));
            var commandLine = $"{_commandName}{commandParameters}";
            var shellCommandLine = $"chcp {_encoding.CodePage}>NUL&&{commandLine}";
            var startInfo =
                new ProcessStartInfo
                {
                    FileName = _COMMAND_PROMPT_COMMAND_NAME,
                    Arguments = $"/{(keepShellRunning ? "k" : "c")} {shellCommandLine}",
                    UseShellExecute = false,
                    CreateNoWindow = false,
                };
            startInfo.EnvironmentVariables[_ENVIRONMENT_VARIABLE_LAUNCHED_BY_THIS_LAUNCHER] = _ENVIRONMENT_VALUE_LAUNCHED_BY_THIS_LAUNCHER;
            startInfo.EnvironmentVariables[_PATH_ENVIRONMENT_VARIABLE_NAME] = BuildPathEnvironmentValue(baseDirectory);
            _ = Process.Start(startInfo);
        }

        public static Boolean IsLaunchedByThisLauncher
            => Environment.GetEnvironmentVariable(_ENVIRONMENT_VARIABLE_LAUNCHED_BY_THIS_LAUNCHER) == _ENVIRONMENT_VALUE_LAUNCHED_BY_THIS_LAUNCHER;

        private static String BuildPathEnvironmentValue(DirectoryPath? baseDirectory)
        {
            var path = Environment.GetEnvironmentVariable(_PATH_ENVIRONMENT_VARIABLE_NAME) ?? "";
            if (path != "" && !path.EndsWith(Path.PathSeparator))
                path += Path.PathSeparator;
            path += baseDirectory?.FullName ?? Environment.CurrentDirectory;
            return path;
        }
    }
}
