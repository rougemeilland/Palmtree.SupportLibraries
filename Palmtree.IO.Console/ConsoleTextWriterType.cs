namespace Palmtree.IO.Console
{
    /// <summary>
    /// コンソールの出力先を示す列挙体です。
    /// </summary>
    public enum ConsoleTextWriterType
    {
        /// <summary>
        /// 出力を行わないことを意味します。
        /// </summary>
        None = 0,

        /// <summary>
        /// 標準出力に出力することを意味します。
        /// </summary>
        StandardOutput,

        /// <summary>
        /// 標準エラー出力に出力することを意味します。
        /// </summary>
        StandardError,
    }
}
