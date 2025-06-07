using System.IO;

namespace Palmtree.IO
{
    /// <summary>
    ///  <see cref="Stream"/> クラスの単純なラッパークラスのインターフェースです。
    /// </summary>
    /// <remarks>
    /// <para>
    /// このインターフェースを実装する <see cref="Stream"/> オブジェクトのラッパークラスに対して、内部の <see cref="Stream"/> オブジェクトに直接アクセスすることが可能になり、若干のパフォーマンス向上が見込めます。
    /// </para>
    /// <para>
    /// <see cref="Stream"/> オブジェクトとは別に「ストリームの状態」を保持するラッパークラスでは、<see cref="IDirectDotNetStreamWrapper"/> インターフェースは実装しないでください。
    /// 何故ならば、内部の <see cref="Stream"/> オブジェクトが直接アクセスされることにより、ラッパークラスの動作に不整合が生じる可能性があるからです。
    /// </para>
    /// <para>
    /// 例えば、<see cref="Stream"/> の I/O バッファリングを行うラッパークラスでは、<see cref="IDirectDotNetStreamWrapper"/> を実装してはいけません。
    /// 実装した場合、おそらくデータの消失や重複が発生する可能性があるでしょう。
    /// </para>
    /// </remarks>
    internal interface IDirectDotNetStreamWrapper
    {
        /// <summary>
        /// ラッパークラスの内部の <see cref="Stream"/> オブジェクトを取得します。
        /// </summary>
        Stream BaseStream { get; }
    }
}
