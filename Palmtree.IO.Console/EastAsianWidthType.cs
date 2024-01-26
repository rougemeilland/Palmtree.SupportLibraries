namespace Palmtree.IO.Console
{
    /// <summary>
    /// 東アジアの組版における文字の幅を示す列挙体です。
    /// </summary>
    internal enum EastAsianWidthType
    {
        /// <summary>
        /// 中立な文字であることを示します。
        /// </summary>
        /// <remarks>
        /// <para>
        /// 他のいずれにも属さない文字です。
        /// 東アジアの組版には通常出現せず、全角でも半角でもありません。
        /// </para>
        /// <para>
        /// 例) アラビア文字など。
        /// </para>
        /// </remarks>
        Neutral = 0,

        /// <summary>
        /// 全角文字であることを示します。
        /// </summary>
        /// <remarks>
        /// <para>
        /// 対応する <see cref="Narrow"/> の文字が存在する、幅の広い文字です。
        /// </para>
        /// <para>
        /// 例) 全角英数など。
        /// </para>
        /// </remarks>
        Fullwidth = 1,

        /// <summary>
        /// 半角文字であることを示します。
        /// </summary>
        /// <remarks>
        /// <para>
        /// 対応する <see cref="Wide"/> の文字が存在する、幅の狭い文字です。
        /// </para>
        /// <para>
        /// 例) 半角カナなど。
        /// </para>
        /// </remarks>
        Halfwidth = 2,

        /// <summary>
        /// 幅の広い文字であることを示します。
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="Fullwidth"/> 以外で、従来文字コードではいわゆる全角であった文字です。
        /// </para>
        /// <para>
        /// 例) 漢字や仮名文字、東アジアの組版にしか使われない記述記号 (たとえば句読点) など。
        /// </para>
        /// </remarks>
        Wide = 3,

        /// <summary>
        /// 幅の狭い文字を示します。
        /// </summary>
        /// <remarks>
        /// <para>
        /// <see cref="Halfwidth"/> 以外の文字で、従来文字コードでは対応するいわゆる全角の文字が存在した半角文字です。
        /// </para>
        /// <para>
        /// 例) 半角英数など。
        /// </para>
        /// </remarks>
        Narrow = 4,

        /// <summary>
        /// 曖昧な幅の文字を示します。
        /// </summary>
        /// <remarks>
        /// 文脈によって文字幅が異なる文字。
        /// 東アジアの組版とそれ以外の組版の両方に出現し、東アジアの従来文字コードではいわゆる全角として扱われることがある。
        /// ギリシア文字やキリル文字など。
        /// </remarks>
        Ambiguous = 5,

        /// <inheritdoc cref="Neutral"/>
        N = Neutral,

        /// <inheritdoc cref="Fullwidth"/>
        F = Fullwidth,

        /// <inheritdoc cref="Halfwidth"/>
        H = Halfwidth,

        /// <inheritdoc cref="Wide"/>
        W = Wide,

        /// <inheritdoc cref="Narrow"/>
        Na = Narrow,

        /// <inheritdoc cref="Ambiguous"/>
        A = Ambiguous,
    }
}
