using System;

namespace Palmtree.IO.Compression.Archive.Zip.ExtraFields
{
    /// <summary>
    /// エントリのタイムスタンプを維持する拡張フィールドの基底クラスです。
    /// </summary>
    public abstract class TimestampExtraField
        : ExtraField, ITimestampExtraField
    {
        private DateTimeOffset? _lastWriteTimeUtc;
        private DateTimeOffset? _lastAccessTimeUtc;
        private DateTimeOffset? _creationTimeUtc;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="extraFieldId">
        /// 拡張フィールドの ID です。
        /// </param>
        protected TimestampExtraField(UInt16 extraFieldId)
            : base(extraFieldId)
        {
            _lastWriteTimeUtc = null;
            _lastAccessTimeUtc = null;
            _creationTimeUtc = null;
        }

        /// <inheritdoc/>
        public virtual DateTimeOffset? LastWriteTimeOffsetUtc
        {
            get => _lastWriteTimeUtc;
            set
            {
                _lastWriteTimeUtc = value?.ToUniversalTime();
                Validation.Assert(_lastWriteTimeUtc is null || _lastWriteTimeUtc.Value.Offset == TimeSpan.Zero, "_lastWriteTimeUtc is null || _lastWriteTimeUtc.Value.Offset == TimeSpan.Zero");
            }
        }

        /// <inheritdoc/>
        public virtual DateTimeOffset? LastAccessTimeOffsetUtc
        {
            get => _lastAccessTimeUtc;
            set
            {
                _lastAccessTimeUtc = value?.ToUniversalTime();
                Validation.Assert(_lastAccessTimeUtc is null || _lastAccessTimeUtc.Value.Offset == TimeSpan.Zero, "_lastAccessTimeUtc is null || _lastAccessTimeUtc.Value.Offset == TimeSpan.Zero");
            }
        }

        /// <inheritdoc/>
        public virtual DateTimeOffset? CreationTimeOffsetUtc
        {
            get => _creationTimeUtc;
            set
            {
                _creationTimeUtc = value?.ToUniversalTime();
                Validation.Assert(_creationTimeUtc is null || _creationTimeUtc.Value.Offset == TimeSpan.Zero, "_creationTimeUtc is null || _creationTimeUtc.Value.Offset == TimeSpan.Zero");
            }
        }

        /// <inheritdoc/>
        public abstract TimeSpan DateTimePrecision { get; }
    }
}
