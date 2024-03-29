﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Palmtree.IO.Compression.Archive.Zip.ExtraFields;
using Palmtree.Text;

namespace Palmtree.IO.Compression.Archive.Zip.Headers.Parser
{
    internal abstract class ZipEntryInternalHeader
    {
        private static readonly Encoding _utf8EncodingWithoutBOM;

        static ZipEntryInternalHeader()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _utf8EncodingWithoutBOM = Encoding.UTF8.WithFallback(null, null).WithoutPreamble();
        }

        protected ZipEntryInternalHeader(
            IZipEntryNameEncodingProvider zipEntryNameEncodingProvider,
            ZipStreamPosition localHeaderPosition,
            UInt16 versionNeededToExtract,
            ZipEntryGeneralPurposeBitFlag generalPurposeBitFlag,
            ZipEntryCompressionMethodId compressionMethodId,
            DateTimeOffset? dosDateTimeOffset,
            UInt32 rawCrc,
            UInt32 rawPackedSize,
            UInt32 rawSize,
            UInt64 packedSize,
            UInt64 size,
            ReadOnlyMemory<Byte> fullNameBytes,
            ReadOnlyMemory<Byte> commentBytes,
            ExtraFieldCollection extraFields,
            UInt64 headerSize,
            Boolean requiredZip64,
            ValidationStringency stringency)
        {
            LocalHeaderPosition = localHeaderPosition;
            VersionNeededToExtract = versionNeededToExtract;
            GeneralPurposeBitFlag = generalPurposeBitFlag;
            CompressionMethodId = compressionMethodId;
            DosDateTimeOffset = dosDateTimeOffset is not null ? (dosDateTimeOffset.Value, TimeSpan.FromSeconds(2)) : null;
            RawCrc = rawCrc;
            RawPackedSize = rawPackedSize;
            RawSize = rawSize;
            PackedSize = packedSize;
            Size = size;
            FullName = "";
            FullNameBytes = fullNameBytes;
            Comment = "";
            CommentBytes = commentBytes;
            ExtraFields = extraFields;
            HeaderSize = headerSize;
            RequiredZip64 = requiredZip64;
            LastWriteTimeOffsetUtc = null;
            LastAccessTimeOffsetUtc = null;
            CreationTimeOffsetUtc = null;
            ExactEntryEncoding = null;
            PossibleEntryEncodings = Array.Empty<Encoding>();

            #region タイムスタンプを設定する

            //
            // タイムスタンプを設定する
            //

            // 拡張フィールドの優先順位は以下の通り
            // 1) まず、日時の最小単位 (precition) が小さいものを優先
            // 2) 次に、以下の配列で先に記述されているものを優先
            var timeStampExtraFields = new[]
            {
                ExtraFields.GetExtraField<NtfsExtraField>(stringency) as ITimestampExtraField,
                ExtraFields.GetExtraField<ExtendedTimestampExtraField>(stringency),
                ExtraFields.GetExtraField<UnixExtraFieldType1>(stringency),
                ExtraFields.GetExtraField<UnixExtraFieldType0>(stringency),
            };
            foreach (var timeStampExtraField in timeStampExtraFields)
            {
                if (timeStampExtraField is not null)
                {
                    if (timeStampExtraField.LastWriteTimeOffsetUtc is not null && (LastWriteTimeOffsetUtc is null || LastWriteTimeOffsetUtc.Value.precition > timeStampExtraField.DateTimePrecision))
                        LastWriteTimeOffsetUtc = (timeStampExtraField.LastWriteTimeOffsetUtc.Value, timeStampExtraField.DateTimePrecision);

                    if (timeStampExtraField.LastAccessTimeOffsetUtc is not null && (LastAccessTimeOffsetUtc is null || LastAccessTimeOffsetUtc.Value.precition > timeStampExtraField.DateTimePrecision))
                        LastAccessTimeOffsetUtc = (timeStampExtraField.LastAccessTimeOffsetUtc.Value, timeStampExtraField.DateTimePrecision);

                    if (timeStampExtraField.CreationTimeOffsetUtc is not null && (CreationTimeOffsetUtc is null || CreationTimeOffsetUtc.Value.precition > timeStampExtraField.DateTimePrecision))
                        CreationTimeOffsetUtc = (timeStampExtraField.CreationTimeOffsetUtc.Value, timeStampExtraField.DateTimePrecision);
                }
            }

            #endregion

            #region エントリ名とコメントを設定する

            //
            // エントリ名とコメントを設定する
            //

            var encodingIsKnown = false;

            // 1. 汎用フラグで UTF-8 であることの指定がされているかどうかをチェックする
            if ((GeneralPurposeBitFlag & ZipEntryGeneralPurposeBitFlag.UseUnicodeEncodingForNameAndComment) != ZipEntryGeneralPurposeBitFlag.None)
            {
                ExactEntryEncoding = _utf8EncodingWithoutBOM;
                PossibleEntryEncodings = Array.Empty<Encoding>();
                FullName = _utf8EncodingWithoutBOM.GetString(fullNameBytes);
                Comment = _utf8EncodingWithoutBOM.GetString(commentBytes);
                encodingIsKnown = true;
            }

            // 2. エンコーディングが未解決であれば、拡張フィールド CodePageExtraField の参照を試みる
            if (!encodingIsKnown)
            {
                if (TryResolveEncodingByCodePadeExtraField(ExtraFields, zipEntryNameEncodingProvider, fullNameBytes, commentBytes, stringency, out var fullName, out var comment, out var originalEncoding))
                {
                    ExactEntryEncoding = originalEncoding;
                    PossibleEntryEncodings = Array.Empty<Encoding>();
                    FullName = fullName;
                    Comment = comment;
                    encodingIsKnown = true;
                }
            }

            // 3. エンコーディングが未解決であれば、拡張フィールド XceedUnicodeExtraField の参照を試みる
            if (!encodingIsKnown)
            {
                if (TryResolveEncodingByXceedUnicodeExtraField(ExtraFields, zipEntryNameEncodingProvider, fullNameBytes, commentBytes, stringency, out var fullName, out var comment, out var originalEncodings))
                {
                    ExactEntryEncoding = null;
                    PossibleEntryEncodings = originalEncodings.ToList();
                    FullName = fullName;
                    Comment = comment;
                    encodingIsKnown = true;
                }
            }

            // 4. エンコーディングが未解決であれば、拡張フィールド UnicodePathExtraField および UnicodeCommentExtraField の参照を試みる
            if (!encodingIsKnown)
            {
                if (TryResolveEncodingByUnicodePathCommentExtraField(ExtraFields, zipEntryNameEncodingProvider, fullNameBytes, commentBytes, stringency, out var fullName, out var comment, out var originalEncodings))
                {
                    ExactEntryEncoding = null;
                    PossibleEntryEncodings = originalEncodings.ToList();
                    FullName = fullName;
                    Comment = comment;
                    encodingIsKnown = true;
                }
            }

            // 5. エンコーディングが未解決であれば、適用可能なエンコーディングを探す
            if (!encodingIsKnown)
            {
                var originalEncodings = zipEntryNameEncodingProvider.GetBestEncodings(fullNameBytes, null, commentBytes, null).ToList();
                var bestEncoding = originalEncodings.FirstOrDefault();
                if (bestEncoding is not null)
                {
                    ExactEntryEncoding = null;
                    PossibleEntryEncodings = originalEncodings.ToList();
                    FullName = bestEncoding.GetString(fullNameBytes);
                    Comment = bestEncoding.GetString(commentBytes);
                    encodingIsKnown = true;
                }
            }

            // 6. 最後に、エンコーディングが未解決であれば、最低限の設定を行う
            if (!encodingIsKnown)
            {
                ExactEntryEncoding = null;
                PossibleEntryEncodings = Array.Empty<Encoding>();
                FullName = fullNameBytes.GetStringByUnknownDecoding();
                Comment = commentBytes.GetStringByUnknownDecoding();
            }

            #endregion
        }

        public ZipStreamPosition LocalHeaderPosition { get; }
        public UInt16 VersionNeededToExtract { get; }
        public ZipEntryGeneralPurposeBitFlag GeneralPurposeBitFlag { get; }
        public ZipEntryCompressionMethodId CompressionMethodId { get; }
        public (DateTimeOffset dateTimeOffset, TimeSpan precition)? DosDateTimeOffset { get; }
        public UInt32 RawCrc { get; }
        public abstract UInt32 Crc { get; }
        public UInt32 RawPackedSize { get; }
        public UInt32 RawSize { get; }
        public virtual UInt64 PackedSize { get; }
        public virtual UInt64 Size { get; }
        public ReadOnlyMemory<Byte> FullNameBytes { get; }
        public String FullName { get; }
        public virtual ReadOnlyMemory<Byte> CommentBytes { get; }
        public virtual String Comment { get; }
        public ExtraFieldCollection ExtraFields { get; }
        public UInt64 HeaderSize { get; }
        public Boolean RequiredZip64 { get; }
        public Encoding? ExactEntryEncoding { get; }
        public IEnumerable<Encoding> PossibleEntryEncodings { get; }
        public (DateTimeOffset dateTimeOffset, TimeSpan precition)? LastWriteTimeOffsetUtc { get; }
        public (DateTimeOffset dateTimeOffset, TimeSpan precition)? LastAccessTimeOffsetUtc { get; }
        public (DateTimeOffset dateTimeOffset, TimeSpan precition)? CreationTimeOffsetUtc { get; }

        private static Boolean TryResolveEncodingByCodePadeExtraField(ExtraFieldCollection extraFields, IZipEntryNameEncodingProvider zipEntryNameEncodingProvider, ReadOnlyMemory<Byte> fullNameBytes, ReadOnlyMemory<Byte> commentBytes, ValidationStringency stringency, [MaybeNullWhen(false)] out String fullName, [MaybeNullWhen(false)] out String comment, [MaybeNullWhen(false)] out Encoding originalEncoding)
        {
            var extraField = extraFields.GetExtraField<CodePageExtraField>(stringency);
            if (extraField is null)
            {
                fullName = null;
                comment = null;
                originalEncoding = null;
                return false;
            }

            var encoding =
                zipEntryNameEncodingProvider.SupportedEncodings
                .Where(encoding => encoding.CodePage == extraField.CodePage)
                .FirstOrDefault();

            if (encoding is null)
            {
                fullName = null;
                comment = null;
                originalEncoding = null;
                return false;
            }

            // 拡張フィールドに指定されたコードページによっては UNICODE にマッピングできない文字を含むことがあるので、絶対に文字化けしないというわけではないことに注意。
            fullName = encoding.GetString(fullNameBytes);
            comment = encoding.GetString(commentBytes);
            originalEncoding = encoding;
            return true;
        }

        private static Boolean TryResolveEncodingByXceedUnicodeExtraField(ExtraFieldCollection extraFields, IZipEntryNameEncodingProvider zipEntryNameEncodingProvider, ReadOnlyMemory<Byte> fullNameBytes, ReadOnlyMemory<Byte> commentBytes, ValidationStringency stringency, [MaybeNullWhen(false)] out String fullName, [MaybeNullWhen(false)] out String comment, out IEnumerable<Encoding> originalEncodings)
        {
            var xceedUnicodeExtraField = extraFields.GetExtraField<XceedUnicodeExtraField>(stringency);
            var extraFieldfullName = xceedUnicodeExtraField?.FullName;
            var extraFieldComment = xceedUnicodeExtraField?.Comment;
            if (extraFieldfullName is null || extraFieldComment is null)
            {
                fullName = null;
                comment = null;
                originalEncodings = Array.Empty<Encoding>();
                return false;
            }

            fullName = extraFieldfullName;
            comment = extraFieldComment;
            originalEncodings = zipEntryNameEncodingProvider.GetBestEncodings(fullNameBytes, extraFieldfullName, commentBytes, extraFieldComment).ToList();
            return true;
        }

        private static Boolean TryResolveEncodingByUnicodePathCommentExtraField(ExtraFieldCollection extraFields, IZipEntryNameEncodingProvider zipEntryNameEncodingProvider, ReadOnlyMemory<Byte> fullNameBytes, ReadOnlyMemory<Byte> commentBytes, ValidationStringency stringency, [MaybeNullWhen(false)] out String fullName, [MaybeNullWhen(false)] out String comment, out IEnumerable<Encoding> originalEncodings)
        {
            var unicodePathExtraField = extraFields.GetExtraField<UnicodePathExtraField>(stringency);
            var unicodeCommentExtraField = extraFields.GetExtraField<UnicodeCommentExtraField>(stringency);
            var extrafieldFullName = unicodePathExtraField?.GetFullName(fullNameBytes.Span);
            var extraFieldComment = unicodeCommentExtraField?.GetComment(commentBytes.Span);
            if (extrafieldFullName is null && extraFieldComment is null)
            {
                fullName = null;
                comment = null;
                originalEncodings = Array.Empty<Encoding>();
                return false;
            }

            originalEncodings =
                zipEntryNameEncodingProvider.GetBestEncodings(
                    fullNameBytes,
                    extrafieldFullName,
                    commentBytes,
                    extraFieldComment)
                .ToList();

            fullName = extrafieldFullName ?? originalEncodings.FirstOrDefault()?.GetString(fullNameBytes) ?? fullNameBytes.GetStringByUnknownDecoding();
            comment = extraFieldComment ?? originalEncodings.FirstOrDefault()?.GetString(commentBytes) ?? commentBytes.GetStringByUnknownDecoding();
            return true;
        }
    }
}
