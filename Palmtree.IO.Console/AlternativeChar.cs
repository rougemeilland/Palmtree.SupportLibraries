using System;

namespace Palmtree.IO.Console
{
    /// <summary>
    /// ライングラフィックスの描画で指定するシンボルの列挙体です。
    /// </summary>
    public enum AlternativeChar
        : Byte
    {
        /// <summary>
        /// 左上の罫線です。('┏'に似ています)
        /// </summary>
        ULCORNER = (Byte)'l',

        /// <summary>
        /// 左下の罫線です。('┗'に似ています)
        /// </summary>
        LLCORNER = (Byte)'m',

        /// <summary>
        /// 右上の罫線です。('┓'に似ています)
        /// </summary>
        URCORNER = (Byte)'k',

        /// <summary>
        /// 右下の罫線です。('┛'に似ています)
        /// </summary>
        LRCORNER = (Byte)'j',

        /// <summary>
        /// 縦棒から右に突き出た罫線です。('┣'に似ています)
        /// </summary>
        LTEE = (Byte)'t',

        /// <summary>
        /// 縦棒から左に突き出た罫線です。('┫'に似ています)
        /// </summary>
        RTEE = (Byte)'u',

        /// <summary>
        /// 横棒から上に突き出た罫線です。('┻'に似ています)
        /// </summary>
        BTEE = (Byte)'v',

        /// <summary>
        /// 横棒から下に突き出た罫線です。('┳'に似ています)
        /// </summary>
        TTEE = (Byte)'w',

        /// <summary>
        /// 横棒の罫線です。('━'に似ています)
        /// </summary>
        HLINE = (Byte)'q',

        /// <summary>
        /// 縦棒の罫線です。('┃'に似ています)
        /// </summary>
        VLINE = (Byte)'x',

        /// <summary>
        /// 縦棒と横棒が交差した罫線です。('╋'に似ています)
        /// </summary>
        PLUS = (Byte)'n',

        /// <summary>
        /// 走査線1です。(上寄りの'-'に似ています。)
        /// </summary>
        S1 = (Byte)'o',

        /// <summary>
        /// 走査線3です。(やや上寄りの'-'に似ています。)
        /// </summary>
        S3 = (Byte)'p',

        /// <summary>
        /// 走査線7です。(やや下寄りの'-'に似ています。)
        /// </summary>
        S7 = (Byte)'r',

        /// <summary>
        /// 走査線9です。(下寄りの'-'に似ています。)
        /// </summary>
        S9 = (Byte)'s',

        /// <summary>
        /// ダイヤの記号です。('♦'に似ています。)
        /// </summary>
        DIAMOND = (Byte)'`',

        /// <summary>
        /// 市松模様のブロックの記号です。
        /// </summary>
        CKBOARD = (Byte)'a',

        /// <summary>
        /// 角度の記号です。('°'に似ています)
        /// </summary>
        DEGREE = (Byte)'f',

        /// <summary>
        /// プラスマイナスの記号です。('±'に似ています)
        /// </summary>
        PLMINUS = (Byte)'g',

        /// <summary>
        /// 中点の記号です。('・'に似ています。)
        /// </summary>
        BULLET = (Byte)'~',

        /// <summary>
        /// 左向きの矢印です。('&lt;'に似ています。)
        /// </summary>
        LARROW = (Byte)',',

        /// <summary>
        /// 右向きの矢印です。('&gt;'に似ています。)
        /// </summary>
        RARROW = (Byte)'+',

        /// <summary>
        /// 下向きの矢印です。('v'に似ています。)
        /// </summary>
        DARROW = (Byte)'.',

        /// <summary>
        /// 上向きの矢印です。('v'を上下反転したものに似ています。)
        /// </summary>
        UARROW = (Byte)'-',

        /// <summary>
        /// 四角形の記号です。
        /// </summary>
        BOARD = (Byte)'h',

        /// <summary>
        /// ランタンの記号です。
        /// </summary>
        LANTERN = (Byte)'i',

        /// <summary>
        /// 塗りつぶされた四角形の記号です。
        /// </summary>
        BLOCK = (Byte)'0',

        /// <summary>
        /// 小なりイコールの記号です。('≦'に似ています)
        /// </summary>
        LEQUAL = (Byte)'y',

        /// <summary>
        /// 大なりイコールの記号です。('≧'に似ています)
        /// </summary>
        GEQUAL = (Byte)'z',

        /// <summary>
        /// 円周率の記号です。('π'に似ています)
        /// </summary>
        PI = (Byte)'{',

        /// <summary>
        /// 不等号の記号です。('≠'に似ています)
        /// </summary>
        NEQUAL = (Byte)'|',

        /// <summary>
        /// ポンド記号です。('￡'に似ています)
        /// </summary>
        STERLING = (Byte)'}',

        /// <summary>
        /// <see cref="ULCORNER"/>の別名です。
        /// </summary>
        BSSB = ULCORNER,

        /// <summary>
        /// <see cref="LLCORNER"/>の別名です。
        /// </summary>
        SSBB = LLCORNER,

        /// <summary>
        /// <see cref="URCORNER"/>の別名です。
        /// </summary>
        BBSS = URCORNER,

        /// <summary>
        /// <see cref="LRCORNER"/>の別名です。
        /// </summary>
        SBBS = LRCORNER,

        /// <summary>
        /// <see cref="RTEE"/>の別名です。
        /// </summary>
        SBSS = RTEE,

        /// <summary>
        /// <see cref="LTEE"/>の別名です。
        /// </summary>
        SSSB = LTEE,

        /// <summary>
        /// <see cref="BTEE"/>の別名です。
        /// </summary>
        SSBS = BTEE,

        /// <summary>
        /// <see cref="TTEE"/>の別名です。
        /// </summary>
        BSSS = TTEE,

        /// <summary>
        /// <see cref="HLINE"/>の別名です。
        /// </summary>
        BSBS = HLINE,

        /// <summary>
        /// <see cref="VLINE"/>の別名です。
        /// </summary>
        SBSB = VLINE,

        /// <summary>
        /// <see cref="PLUS"/>の別名です。
        /// </summary>
        SSSS = PLUS,
    }
}
