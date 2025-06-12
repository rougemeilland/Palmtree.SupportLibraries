namespace Palmtree
{
    public enum LogCategory
    {
        /// <summary>
        /// ログメッセージのカテゴリが指定されていないことを示します。
        /// </summary>
        None = 0,

        /// <summary>
        /// ログメッセージのカテゴリが情報であることを示します。
        /// </summary>
        Information = 1,

        /// <summary>
        /// ログメッセージのカテゴリが警告であることを示します。
        /// </summary>
        Warning = 2,

        /// <summary>
        /// ログメッセージのカテゴリが外的要因によるエラーであることを示します。
        /// </summary>
        Error = 3,

        /// <summary>
        /// ログメッセージのカテゴリが内的要因のみによるエラーあることを示します。
        /// </summary>
        Critical = 4,
    }
}
