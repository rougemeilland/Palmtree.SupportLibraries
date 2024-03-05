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
        /// <item>
        /// エントリの以下のタイムスタンプが実際には ZIP アーカイブに書き込まれなくなります。
        /// <list type="bullet">
        /// <item>作成日時 (<see cref="ZipDestinationEntry.CreationTimeUtc"/> / <see cref="ZipDestinationEntry.CreationTimeOffsetUtc"/>)</item>
        /// <item>最終アクセス日時 (<see cref="ZipDestinationEntry.LastAccessTimeUtc"/> / <see cref="ZipDestinationEntry.LastAccessTimeOffsetUtc"/>)</item>
        /// </list>
        /// また、実際に ZIP アーカイブに書き込まれる以下のタイムスタンプの精度が大きく低下します。(最大誤差 2秒)
        /// <list type="bullet">
        /// <item>最終更新日時 (<see cref="ZipDestinationEntry.LastWriteTimeUtc"/> / <see cref="ZipDestinationEntry.LastWriteTimeOffsetUtc"/>)</item>
        /// </list>
        /// </item>
        /// <item>UNICODE で表現できない文字 (例: 一部の SHIFT-JIS 文字) がエントリのファイル名およびコメントに使用できません。</item>
        /// </list>
        /// </remarks>
        DoNotUseExtraFieldsInLocalHeaders = 1 << 1,

        /// <summary>
        /// 拡張フィールド「PKWARE Win95/WinNT Extra Field」(0x000a) を付加しません。
        /// </summary>
        /// <remarks>
        /// <para>
        /// 既定では、ZIP アーカイブのエントリの高精度のタイムスタンプを保持するために拡張フィールド「PKWARE Win95/WinNT Extra Field」が付加されます。
        /// しかし、このフラグを指定することにより拡張フィールド「PKWARE Win95/WinNT Extra Field」は付加されなくなります。
        /// </para>
        /// </remarks>
        DisableNtfsHighPrecisionTimestamp = 1 << 2,

        /// <summary>
        /// 拡張フィールド「Extended Timestamp Extra Field」(0x5455) を付加しません。
        /// </summary>
        /// <remarks>
        /// <para>
        /// 既定では、ZIP アーカイブのエントリの高精度のタイムスタンプを保持するために拡張フィールド「Extended Timestamp Extra Field」が付加されます。
        /// しかし、このフラグを指定することにより拡張フィールド「Extended Timestamp Extra Field」は付加されなくなります。
        /// </para>
        /// </remarks>
        DisableUnixHighPrecisionTimestamp = 1 << 3,
    }
}
