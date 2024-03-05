using System;

namespace Palmtree.IO.Compression.Archive.Zip.ExtraFields
{
    /// <summary>
    /// 一般的な日時 (最終更新日時/最終アクセス日時/作成日時) を取得/設定するインターフェースです。
    /// </summary>
    internal interface ITimestampExtraField
        : IExtraField
    {
        /// <summary>
        /// 最終更新日時 (UTC) を取得または設定します。
        /// </summary>
        /// <value>
        /// 作成日時である <see cref="DateTimeOffset"/> オブジェクトです。
        /// 値が設定されていない場合は null です。
        /// </value>
        /// <remarks>
        /// 値を設定する際に、値のタイムゾーンは自動的にUTCに変換されます。
        /// </remarks>
        DateTimeOffset? LastWriteTimeOffsetUtc { get; set; }

        /// <summary>
        /// 最終アクセス日時 (UTC) を取得または設定します。
        /// </summary>
        /// <value>
        /// 作成日時である <see cref="DateTimeOffset"/> オブジェクトです。
        /// 値が設定されていない場合は null です。
        /// </value>
        /// <remarks>
        /// 値を設定する際に、値のタイムゾーンは自動的にUTCに変換されます。
        /// </remarks>
        DateTimeOffset? LastAccessTimeOffsetUtc { get; set; }

        /// <summary>
        /// 作成日時 (UTC) を取得または設定します。
        /// </summary>
        /// <value>
        /// 作成日時である <see cref="DateTimeOffset"/> オブジェクトです。
        /// 値が設定されていない場合は null です。
        /// </value>
        /// <remarks>
        /// 値を設定する際に、値のタイムゾーンは自動的にUTCに変換されます。
        /// </remarks>
        DateTimeOffset? CreationTimeOffsetUtc { get; set; }

        /// <summary>
        /// 拡張フィールドが保持する日時の最小単位を取得します。
        /// </summary>
        /// <value>日時の最小単位である <see cref="TimeSpan"/> 値です。</value>
        TimeSpan DateTimePrecision { get; }
    }
}
