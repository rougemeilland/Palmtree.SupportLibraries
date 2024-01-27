namespace Palmtree.IO.Console
{
    /// <summary>
    /// テキストを縮小する際にどの位置を省略するかを示す列挙体です。
    /// </summary>
    public enum TextShrinkingStyle
    {
        /// <summary>
        /// 中央部分を省略します。
        /// </summary>
        OmitCenter = 0,

        /// <summary>
        /// 先頭部分を省略します。
        /// </summary>
        OmitBeginning,

        /// <summary>
        /// 末尾部分を省略します。
        /// </summary>
        OmitEnd,
    }
}
