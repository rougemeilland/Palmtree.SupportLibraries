using System;

namespace Palmtree.IO.Compression.Archive.Zip.ExtraFields
{
    /// <summary>
    /// Info-ZIP Unix Extra Field (type 1) 拡張フィールドのクラスです。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// <para>
    /// この拡張フィールドは過去に作成された ZIP アーカイブとの互換性のためにのみ残されています。
    /// </para>
    /// <para>
    /// この拡張フィールドを新たにエントリに追加してはいけません。
    /// その代わりに、以下の何れかの拡張フィールドを使用してください。
    /// </para>
    /// <list type="bullet">
    /// <item><see cref="ExtendedTimestampExtraField"/> (タイムスタンプをサポート)</item>
    /// <item><see cref="NewUnixExtraField"/> (16bit を超える UID/GID をサポート)</item>
    /// <item><see cref="UnixExtraFieldType2"/> (16bit UID/GID をサポート)</item>
    /// </list>
    /// </item>
    /// </list>
    /// </remarks>
    public class UnixExtraFieldType1
        : UnixTimestampExtraField
    {
        private UInt16? _userId;
        private UInt16? _groupId;

        /// <summary>
        /// デフォルトコンストラクタです。
        /// </summary>
        public UnixExtraFieldType1()
            : base(ExtraFieldId)
        {
            LastAccessTimeOffsetUtc = null;
            LastWriteTimeOffsetUtc = null;
            _userId = null;
            _groupId = null;
        }

        /// <summary>
        /// 拡張フィールドの ID です。
        /// </summary>
        public const UInt16 ExtraFieldId = 0x5855;

        /// <inheritdoc/>
        public override ReadOnlyMemory<Byte>? GetData(ZipEntryHeaderType headerType, IExtraFieldEncodingParameter parameter)
        {
            switch (headerType)
            {
                case ZipEntryHeaderType.LocalHeader:
                {
                    var lastAccessTimestamp =
                        LastAccessTimeOffsetUtc is not null
                        ? ToUnixTimeStamp(LastAccessTimeOffsetUtc.Value)
                        : null;
                    var lastWriteTimestamp =
                        LastWriteTimeOffsetUtc is not null
                        ? ToUnixTimeStamp(LastWriteTimeOffsetUtc.Value)
                        : null;
                    if (lastAccessTimestamp is null
                        || lastWriteTimestamp is null
                        || _userId is null
                        || _groupId is null)
                    {
                        return null;
                    }

                    var builder = new ByteArrayBuilder(sizeof(Int32) + sizeof(Int32) + sizeof(Int16) + sizeof(Int16));
                    builder.AppendInt32LE(lastAccessTimestamp.Value);
                    builder.AppendInt32LE(lastWriteTimestamp.Value);
                    builder.AppendUInt16LE(_userId.Value);
                    builder.AppendUInt16LE(_groupId.Value);

                    return builder.ToByteArray();
                }
                case ZipEntryHeaderType.CentralDirectoryHeader:
                {
                    var lastAccessTimestamp =
                        LastAccessTimeOffsetUtc is not null
                        ? ToUnixTimeStamp(LastAccessTimeOffsetUtc.Value)
                        : null;
                    var lastWriteTimestamp =
                        LastWriteTimeOffsetUtc is not null
                        ? ToUnixTimeStamp(LastWriteTimeOffsetUtc.Value)
                        : null;
                    if (lastAccessTimestamp is null || lastWriteTimestamp is null)
                        return null;

                    var builder = new ByteArrayBuilder(sizeof(Int32) + sizeof(Int32));
                    builder.AppendInt32LE(lastAccessTimestamp.Value);
                    builder.AppendInt32LE(lastWriteTimestamp.Value);

                    return builder.ToByteArray();
                }
                default:
                    throw Validation.GetFailErrorException($"Unknown header type: {nameof(headerType)}={headerType}");
            }
        }

        /// <inheritdoc/>
        public override void SetData(ZipEntryHeaderType headerType, ReadOnlyMemory<Byte> data, IExtraFieldDecodingParameter parameter)
        {
            LastAccessTimeOffsetUtc = null;
            LastWriteTimeOffsetUtc = null;
            _userId = null;
            _groupId = null;
            var reader = new ByteArrayReader(data);
            var success = false;
            try
            {
                switch (headerType)
                {
                    case ZipEntryHeaderType.LocalHeader:
                    {
                        LastAccessTimeOffsetUtc = FromUnixTimeStamp(reader.ReadInt32LE());
                        LastWriteTimeOffsetUtc = FromUnixTimeStamp(reader.ReadInt32LE());
                        _userId = reader.ReadUInt16LE();
                        _groupId = reader.ReadUInt16LE();

                        break;
                    }
                    case ZipEntryHeaderType.CentralDirectoryHeader:
                    {
                        LastAccessTimeOffsetUtc = FromUnixTimeStamp(reader.ReadInt32LE());
                        LastWriteTimeOffsetUtc = FromUnixTimeStamp(reader.ReadInt32LE());
                        _userId = null;
                        _groupId = null;

                        break;
                    }
                    default:
                        throw Validation.GetFailErrorException($"Unknown header type: {nameof(headerType)}={headerType}");
                }

                if (!reader.IsEmpty)
                    throw GetBadFormatException(headerType, data);
                success = true;
            }
            catch (UnexpectedEndOfBufferException ex)
            {
                throw GetBadFormatException(headerType, data, ex);
            }
            finally
            {
                if (!success)
                {
                    LastAccessTimeOffsetUtc = null;
                    LastWriteTimeOffsetUtc = null;
                    _userId = null;
                    _groupId = null;
                }
            }
        }

        /// <inheritdoc/>
        public override DateTimeOffset? CreationTimeOffsetUtc
        {
            get => null;
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        public UInt16 UserId
        {
            get => _userId ?? throw new InvalidOperationException();
            set => _userId = value;
        }

        /// <summary>
        /// グループ ID を取得または設定します。
        /// </summary>
        public UInt16 GroupId
        {
            get => _groupId ?? throw new InvalidOperationException();
            set => _groupId = value;
        }
    }
}
