using System;

namespace Palmtree.IO.Compression.Archive.Zip
{
    /// <summary>
    /// ZIP エントリの書き込み時の特殊な動作を指定するフラグの列挙体です。
    /// </summary>
    [Flags]
    public enum ZipDestinationEntryFlag
    {
        /// <summary>
        /// 何も指定しません。
        /// </summary>
        None = 0,

        /// <summary>
        /// データディスクリプタを使用します。
        /// </summary>
        /// <remarks>
        /// <para>
        /// このフラグは使用しないことを推奨します。
        /// その理由は、データディスクリプタの仕様は ZIP アーカイブソフトウェアの実装によりまちまちであり、必ずしも正しく読み込まれるとは限らないからです。
        /// </para>
        /// <para>
        /// このフラグは ZIP アーカイブの互換性の確認などのテストのためにのみ使用してください。
        /// </para>
        /// </remarks>
        UseDataDescriptor = 1 << 0,

        /// <summary>
        /// ローカルヘッダに拡張フィールドを付加しません。
        /// </summary>
        /// <remarks>
        /// <para>
        /// このフラグは、"epub" フォーマットの "mimetype" エントリなどの非常に限定された目的のために用意されています。
        /// 通常は指定しないでください。
        /// </para>
        /// <para>
        /// このフラグを指定することにより、以下の制限が発生します。
        /// </para>
        /// <list type="bullet">
        /// <item>ZIP64 拡張仕様を使用できません。つまり、このフラグを指定した場合、エントリのデータの長さは <see cref="UInt32.MaxValue"/> (4,294,967,295) バイト未満である必要があります。</item>
        /// <item>エントリのプロパティのうち、<see cref="ZipDestinationEntry.LastAccessTimeUtc"/> および <see cref="ZipDestinationEntry.CreationTimeUtc"/> の値は無視されます。また、実際に書き込まれる <see cref="ZipDestinationEntry.LastWriteTimeUtc"/> の値の最大誤差が 2 秒になります。</item>
        /// <item>UNICODE で表現できない文字 (例: 一部の SHIFT-JIS 文字) がエントリのファイル名およびコメントに使用できません。</item>
        /// </list>
        /// </remarks>
        DoNotUseExtraFieldsInLocalHeaders = 1 << 1,
    }
}
