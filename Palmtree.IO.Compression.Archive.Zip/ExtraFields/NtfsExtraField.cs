﻿using System;

namespace Palmtree.IO.Compression.Archive.Zip.ExtraFields
{
    /// <summary>
    /// PKWARE Win95/WinNT Extra Field の拡張フィールドのクラスです。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// <term>[拡張フィールド 0x000a [PKWARE Win95/WinNT Extra Field] の付加条件について]</term>
    /// <description>
    /// <para>
    /// <see href="https://pkware.cachefly.net/webdocs/casestudies/APPNOTE.TXT">PKWAREの APPNOTE</see> では特に規定されていないが、
    /// <see href="https://libzip.org/specifications/extrafld.txt">Info-ZIP によって公開されている拡張フィールドの仕様書</see> には、以下のように記載されている。
    /// </para>
    /// <para>
    /// --
    /// In the current implementations, this field has a fixed total data size of 32 bytes and is only stored as local extra field.
    /// (現在の実装では、このフィールドの合計データ サイズは 32 バイトに固定されており、ローカルの拡張フィールドとしてのみ保存されます。)
    /// --
    /// </para>
    /// <para>
    /// しかし、実際には、拡張フィールド 0x000a がどのヘッダに格納されるかは実装により異なる。
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <term>PKZIPの場合</term>
    /// <description>
    /// <list type="bullet">
    /// <item>読み込みの際はセントラルディレクトリヘッダとローカルヘッダのどちらか拡張フィールドが存在する方から読み込む。</item>
    /// <item>書き込みの際はセントラルディレクトリヘッダへ書き込む。</item>
    /// </list>
    /// </description>
    /// </item>
    /// <item>
    /// <term>7-ZIP / WinRar の場合</term>
    /// <description>
    /// <list type="bullet">
    /// <item>読み込みの際はセントラルディレクトリヘッダから読み込む。</item>
    /// <item>書き込みの際はセントラルディレクトリヘッダへ書き込む。</item>
    /// </list>
    /// </description>
    /// </item>
    /// </list>
    /// これらの実装状況と、仕様を鑑み、このクラスでは以下の様に実装している。
    /// <list type="bullet">
    /// <item>読み込みの際はセントラルディレクトリヘッダとローカルヘッダの両方から読み込む。</item>
    /// <item>書き込みの際はセントラルディレクトリヘッダとローカルヘッダの両方へ書き込む。</item>
    /// </list>
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    public class NtfsExtraField
        : TimestampExtraField
    {
        // PKWARE の APPNOTE ではローカルヘッダ/セントラルディレクトリヘッダのどちらのヘッダに設定するかは規定されていない。
        // 実際の実装 (PKZIP, 7-zip, Xceed, WinRar で確認) では、セントラルディレクトリヘッダにのみ設定されている。

        // しかし、info-ZIP extrafield.txt ではローカルヘッダにのみ設定することになっている…

        private const UInt16 _subTag0001Id = 0x0001;

        /// <summary>
        /// デフォルトコンストラクタです。
        /// </summary>
        public NtfsExtraField()
            : base(ExtraFieldId)
        {
            LastWriteTimeOffsetUtc = null;
            LastAccessTimeOffsetUtc = null;
            CreationTimeOffsetUtc = null;
        }

        /// <summary>
        /// 拡張フィールドの ID です。
        /// </summary>
        public const UInt16 ExtraFieldId = 0x000a;

        /// <inheritdoc/>
        public override ReadOnlyMemory<Byte>? GetData(ZipEntryHeaderType headerType, IExtraFieldEncodingParameter parameter)
        {
            switch (headerType)
            {
                case ZipEntryHeaderType.LocalHeader:
                    return null;
                case ZipEntryHeaderType.CentralDirectoryHeader:
                {
                    var dataOfSubTag0001 = GetDataForSubTag0001();
                    if (dataOfSubTag0001 is null)
                        return null;

                    var bufferLength = checked(sizeof(UInt32) + sizeof(UInt16) + sizeof(UInt16) + dataOfSubTag0001.Value.Length);
                    if (bufferLength > UInt16.MaxValue)
                        return null;

                    var builder = new ByteArrayBuilder(bufferLength);
                    builder.AppendUInt32LE(0); //Reserved
                    builder.AppendUInt16LE(_subTag0001Id);
                    builder.AppendUInt16LE((UInt16)dataOfSubTag0001.Value.Length);
                    builder.AppendBytes(dataOfSubTag0001.Value.Span);
                    return builder.ToByteArray();
                }
                default:
                    throw Validation.GetFailErrorException($"Unknown header type: {nameof(headerType)}={headerType}");
            }
        }

        /// <inheritdoc/>
        public override void SetData(ZipEntryHeaderType headerType, ReadOnlyMemory<Byte> data, IExtraFieldDecodingParameter parameter)
        {
            LastWriteTimeOffsetUtc = null;
            LastAccessTimeOffsetUtc = null;
            CreationTimeOffsetUtc = null;
            var reader = new ByteArrayReader(data);
            var success = false;
            try
            {
                switch (headerType)
                {
                    case ZipEntryHeaderType.LocalHeader:
                    case ZipEntryHeaderType.CentralDirectoryHeader:
                    {
                        if (!parameter.Stringency.HasFlag(ValidationStringency.StrictlyCheckExtraFieldValues)
                            || headerType == ZipEntryHeaderType.CentralDirectoryHeader)
                        {
                            _ = reader.ReadUInt32LE(); //Reserved
                            while (!reader.IsEmpty)
                            {
                                var subTagId = reader.ReadUInt16LE();
                                var subTagLength = reader.ReadUInt16LE();
                                var subTagData = reader.ReadBytes(subTagLength);
                                switch (subTagId)
                                {
                                    case _subTag0001Id:
                                        SetDataForSubTag0001(subTagData);
                                        break;
                                    default:
                                        // unknown sub tag id
                                        break;
                                }
                            }
                        }

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
                    LastWriteTimeOffsetUtc = null;
                    LastAccessTimeOffsetUtc = null;
                    CreationTimeOffsetUtc = null;
                }
            }
        }

        /// <inheritdoc/>
        public override TimeSpan DateTimePrecision => TimeSpan.FromTicks(1);

        private ReadOnlyMemory<Byte>? GetDataForSubTag0001()
        {
            // 最終更新日時/最終アクセス日時/作成日時のいずれかが未設定の場合は、この拡張フィールドは無効とする。
            if (LastWriteTimeOffsetUtc is null ||
                LastAccessTimeOffsetUtc is null ||
                CreationTimeOffsetUtc is null)
            {
                return null;
            }

            var builder = new ByteArrayBuilder(sizeof(UInt64) + sizeof(UInt64) + sizeof(UInt64));
            builder.AppendUInt64LE((UInt64)LastWriteTimeOffsetUtc.Value.ToFileTime());
            builder.AppendUInt64LE((UInt64)LastAccessTimeOffsetUtc.Value.ToFileTime());
            builder.AppendUInt64LE((UInt64)CreationTimeOffsetUtc.Value.ToFileTime());
            return builder.ToByteArray();
        }

        private void SetDataForSubTag0001(ReadOnlyMemory<Byte> data)
        {
            var reader = new ByteArrayReader(data);
            LastWriteTimeOffsetUtc = DateTimeOffset.FromFileTime((Int64)reader.ReadUInt64LE());
            LastAccessTimeOffsetUtc = DateTimeOffset.FromFileTime((Int64)reader.ReadUInt64LE());
            CreationTimeOffsetUtc = DateTimeOffset.FromFileTime((Int64)reader.ReadUInt64LE());
        }
    }
}
