using System;

namespace Palmtree.IO.Compression.Stream
{
    public enum DeflateCompressionLevel
        : Int32
    { 
        /// <summary>
        /// 通常の圧縮です。
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 最大の圧縮率です。
        /// </summary>
        Maximum,

        /// <summary>
        /// 高速な圧縮です。ただし圧縮率は <see cref="Normal"/> より低下します。
        /// </summary>
        Fast,

        /// <summary>
        /// 非常に高速な圧縮です。ただし圧縮率 <see cref="Fast"/> より低下します。
        /// </summary>
        SuperFast
    }
}
