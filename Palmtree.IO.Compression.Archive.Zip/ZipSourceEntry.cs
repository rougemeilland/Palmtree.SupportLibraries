﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Palmtree.IO.Compression.Archive.Zip.ExtraFields;
using Palmtree.IO.Compression.Archive.Zip.Headers.Parser;
using Palmtree.IO.Compression.Stream;
using Palmtree.Linq;

namespace Palmtree.IO.Compression.Archive.Zip
{
    /// <summary>
    /// 読み込んだ ZIP エントリのクラスです。
    /// </summary>
    public class ZipSourceEntry
    {
        private readonly ZipArchiveFileReader.IZipReaderEnvironment _zipReader;
        private readonly ZipArchiveFileReader.IZipReaderStream _zipStream;
        private readonly ZipEntryGeneralPurposeBitFlag _generalPurposeBitFlag;

        internal ZipSourceEntry(
            ZipArchiveFileReader.IZipReaderEnvironment zipReader,
            ZipArchiveFileReader.IZipReaderStream zipStream,
            ZipEntryHeader internalHeader)
        {
            _zipReader = zipReader;
            _zipStream = zipStream;

            ID = internalHeader.ID;
            LocationOrder = internalHeader.LocationOrder;
            IsDirectory = internalHeader.CentralDirectoryHeader.IsDirectory;
            LocalHeaderPosition = internalHeader.CentralDirectoryHeader.LocalHeaderPosition;
            HostSystem = internalHeader.CentralDirectoryHeader.HostSystem;
            ExternalFileAttributes = internalHeader.CentralDirectoryHeader.ExternalFileAttributes;
            Crc = internalHeader.CentralDirectoryHeader.Crc;
            Size = internalHeader.CentralDirectoryHeader.Size;
            PackedSize = internalHeader.CentralDirectoryHeader.PackedSize;
            EntryTextEncoding =
                internalHeader.LocalHeader.GeneralPurposeBitFlag.HasFlag(ZipEntryGeneralPurposeBitFlag.UseUnicodeEncodingForNameAndComment)
                ? ZipEntryTextEncoding.UTF8
                : ZipEntryTextEncoding.Unknown;
            LocalHeaderExtraFields = new ExtraFieldCollection(internalHeader.LocalHeader.ExtraFields);
            CentralDirectoryHeaderExtraFields = new ExtraFieldCollection(internalHeader.CentralDirectoryHeader.ExtraFields);

            // セントラルディレクトリヘッダおよびローカルヘッダの複数の拡張ヘッダから、最も精度の高い (つまり precision が最も小さい) タイムスタンプを取得する。
            LastWriteTimeOffsetUtc =
                new[]
                {
                    internalHeader.LocalHeader.DosDateTimeOffset,
                    internalHeader.LocalHeader.LastWriteTimeOffsetUtc,
                    internalHeader.CentralDirectoryHeader.DosDateTimeOffset,
                    internalHeader.CentralDirectoryHeader.LastWriteTimeOffsetUtc,
                }
                .WhereNotNull()
                .OrderBy(item => item.precition)
                .Select(item => (DateTimeOffset?)item.dateTimeOffset)
                .FirstOrDefault();
            Validation.Assert(LastWriteTimeUtc is null || LastWriteTimeUtc.Value.Kind == DateTimeKind.Utc, "LastWriteTimeUtc is null || LastWriteTimeUtc.Value.Kind == DateTimeKind.Utc");
            Validation.Assert(LastWriteTimeOffsetUtc is null || LastWriteTimeOffsetUtc.Value.Offset == TimeSpan.Zero, "LastWriteTimeOffsetUtc is null || LastWriteTimeOffsetUtc.Value.Offset == TimeSpan.Zero");
            LastAccessTimeOffsetUtc =
                new[]
                {
                    internalHeader.LocalHeader.LastAccessTimeOffsetUtc,
                    internalHeader.CentralDirectoryHeader.LastAccessTimeOffsetUtc,
                }
                .WhereNotNull()
                .OrderBy(item => item.precition)
                .Select(item => (DateTimeOffset?)item.dateTimeOffset)
                .FirstOrDefault();
            Validation.Assert(LastAccessTimeUtc is null || LastAccessTimeUtc.Value.Kind == DateTimeKind.Utc, "LastAccessTimeUtc is null || LastAccessTimeUtc.Value.Kind == DateTimeKind.Utc");
            Validation.Assert(LastAccessTimeOffsetUtc is null || LastAccessTimeOffsetUtc.Value.Offset == TimeSpan.Zero, "LastAccessTimeOffsetUtc is null || LastAccessTimeOffsetUtc.Value.Offset == TimeSpan.Zero");
            CreationTimeOffsetUtc =
                new[]
                {
                    internalHeader.LocalHeader.CreationTimeOffsetUtc,
                    internalHeader.CentralDirectoryHeader.CreationTimeOffsetUtc,
                }
                .WhereNotNull()
                .OrderBy(item => item.precition)
                .Select(item => (DateTimeOffset?)item.dateTimeOffset)
                .FirstOrDefault();
            Validation.Assert(CreationTimeUtc is null || CreationTimeUtc.Value.Kind == DateTimeKind.Utc, "CreationTimeUtc is null || CreationTimeUtc.Value.Kind == DateTimeKind.Utc");
            Validation.Assert(CreationTimeOffsetUtc is null || CreationTimeOffsetUtc.Value.Offset == TimeSpan.Zero, "CreationTimeOffsetUtc is null || CreationTimeOffsetUtc.Value.Offset == TimeSpan.Zero");

            FullName = internalHeader.LocalHeader.FullName;
            FullNameBytes = internalHeader.LocalHeader.FullNameBytes;
            Comment = internalHeader.CentralDirectoryHeader.Comment;
            CommentBytes = internalHeader.CentralDirectoryHeader.CommentBytes;
            ExactEntryEncoding = internalHeader.LocalHeader.ExactEntryEncoding;
            PossibleEntryEncodings = internalHeader.LocalHeader.PossibleEntryEncodings;
            CompressionMethodId = internalHeader.LocalHeader.CompressionMethodId;
            _generalPurposeBitFlag = internalHeader.LocalHeader.GeneralPurposeBitFlag;
            DataPosition = internalHeader.LocalHeader.DataPosition;
            RequiredZip64ForLocalHeader = internalHeader.LocalHeader.RequiredZip64;
            RequiredZip64ForCentralDirectoryHeader = internalHeader.CentralDirectoryHeader.RequiredZip64;
            if (HostSystem.IsAnyOf(ZipEntryHostSystem.FAT, ZipEntryHostSystem.VFAT, ZipEntryHostSystem.Windows_NTFS, ZipEntryHostSystem.OS2_HPFS))
                FullName = FullName.Replace(@"\", "/");
        }

        /// <summary>
        /// エントリを識別する値を取得します。
        /// </summary>
        public ZipEntryId ID { get; }

        /// <summary>
        /// ZIP アーカイブ上で エントリのデータが配置されている順序を示す値を取得します。
        /// </summary>
        public ZipEntryLocationOrder LocationOrder { get; }

        /// <summary>
        /// エントリがファイルであるかどうかを示す <see cref="Boolean"/> 値を取得します。
        /// </summary>
        /// <value>
        /// エントリがファイルである場合は true、そうではない場合は false です。
        /// </value>
        public Boolean IsFile => !IsDirectory;

        /// <summary>
        /// エントリがディレクトリであるかどうかを示す <see cref="Boolean"/> 値を取得します。
        /// </summary>
        /// <value>
        /// エントリがディレクトリである場合は true、そうではない場合は false です。
        /// </value>
        public Boolean IsDirectory { get; }

        /// <summary>
        /// エントリのデータの CRC 値を取得します。
        /// </summary>
        public UInt32 Crc { get; }

        /// <summary>
        /// エントリのデータの長さを取得します。
        /// </summary>
        public UInt64 Size { get; }

        /// <summary>
        /// エントリの圧縮されたデータの長さを取得します。
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>エントリが圧縮されていない場合は、このプロパティの値は <see cref="Size"/> プロパティの値と等しくなります。</item>
        /// <item>このプロパティの値は必ずしも <see cref="Size"/> プロパティの値より小さいとは限らないことに注意してください。</item>
        /// </list>
        /// </remarks>
        public UInt64 PackedSize { get; }

        /// <summary>
        /// エントリのデータの圧縮方式を示す値を取得します。
        /// </summary>
        public ZipEntryCompressionMethodId CompressionMethodId { get; }

        /// <summary>
        /// エントリを追加した OS の種類を示す値を取得します。
        /// </summary>
        public ZipEntryHostSystem HostSystem { get; }

        /// <summary>
        /// エントリの属性を取得します。
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>
        /// このプロパティの値はの意味は <see cref="HostSystem"/> の値によって異なります。
        /// <list type="bullet">
        /// <item>Windows または MS-DOS 系の OS の場合は、<see cref="ExternalAttributesForDos"/> を参考にしてください。</item>
        /// <item>UNIX 系の OS の場合は、<see cref="ExternalAttributesForUnix"/> を参考にしてください。</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public UInt32 ExternalFileAttributes { get; }

        /// <summary>
        /// エントリのエントリ名およびコメントのエンコーディング方式を示す値を取得します。
        /// </summary>
        public ZipEntryTextEncoding EntryTextEncoding { get; }

        /// <summary>
        /// エントリのローカルヘッダの拡張フィールドのコレクションを取得します。
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>
        /// <term>[拡張フィールドについて]</term>
        /// <description>
        /// <para>拡張フィールドとは、ZIP の正式フォーマットには含まれていない様々な追加情報です。</para>
        /// <para>拡張フィールドには多くの種類があります。例えば以下のようなものがあります。</para>
        /// <list type="bullet">
        /// <item>NTFS 上でのファイル/ディレクトリのセキュリティディスクリプタを保持する拡張フィールド</item>
        /// <item>NTFS 上でのファイル/ディレクトリのタイムスタンプを保持する拡張フィールド</item>
        /// <item>UNIX上でのファイル/ディレクトリのタイムスタンプやユーザID/グループIDを保持する拡張フィールド</item>
        /// <item>エントリ名やコメントのUNICODE文字列を保持する拡張フィールド</item>
        /// <item>エントリ名やコメントのコードページを保持する拡張フィールド</item>
        /// </list>
        /// <para>これはごく一部の例ですが、明らかに目的が重複している拡張フィールドもありますし、特定のオペレーティングシステムでしか意味を持たない拡張フィールドも存在します。</para>
        /// <para>そして、これらの拡張フィールドをZIPアーカイバソフトウェアがどう扱うかは、ZIPアーカイバソフトウェアに任されています。適切に対応されることもあれば、無視されることもあるでしょう。異なる実行環境での拡張フィールドの互換性には注意してください。</para>
        /// </description>
        /// </item>
        /// <item>
        /// <term>[<see cref="LocalHeaderExtraFields"/> プロパティと <see cref="CentralDirectoryHeaderExtraFields"/> プロパティとの違いについて]</term>
        /// <description>
        /// <para>
        /// 必ずしもすべての拡張フィールドに言える話ではありませんが、一部の拡張フィールドの内容は、<see cref="LocalHeaderExtraFields"/> から取得したものと <see cref="CentralDirectoryHeaderExtraFields"/> から取得したものの内容は一致しません。
        /// 多くの場合は、<see cref="CentralDirectoryHeaderExtraFields"/> から取得したものの内容は、<see cref="LocalHeaderExtraFields"/> から取得したものの内容に比べて省略されているようです。
        /// 拡張フィールドの内容を取得する場合は、<see href="https://libzip.org/specifications/extrafld.txt">"/> 拡張フィールドの仕様 </see> を調べた上で、<see cref="LocalHeaderExtraFields"/> プロパティから取得した方がいいでしょう。
        /// </para>
        /// </description>
        /// </item>
        /// <item>
        /// <term>[拡張フィールドの仕様について]</term>
        /// <description>
        /// <para>よく知られている拡張フィールドの仕様については、<see href="https://libzip.org/specifications/extrafld.txt">info-zip の記事</see> が一番詳しいようです。</para>
        /// </description>
        /// </item>
        /// <item>
        /// <term>[拡張フィールドの取得方法]</term>
        /// <description>
        /// <para>NTFS のセキュリティディスクリプタを保持する拡張フィールドを取得するするサンプルプログラムを以下に示します。</para>
        /// <code>
        /// using System;
        /// using System.IO;
        /// using ZipUtility;
        /// using ZipUtility.ZipExtraField;
        ///
        /// internal class Program
        /// {
        ///     private static void Main(string[] args)
        ///     {
        ///         using var reader = new FilePath(args[0]).OpenAsZipFile(ZipEntryNameEncodingProvider.Create(Array.Empty&lt;string&gt;(), Array.Empty&lt;string&gt;()));
        ///         foreach (var entry in reader.GetEntries())
        ///         {
        ///             // NTFS のセキュリティディスクリプタを保持する拡張フィールドを実装しているクラスは <see cref="WindowsSecurityDescriptorExtraField"/> なので、型パラメタに <see cref="WindowsSecurityDescriptorExtraField"/> を指定する。
        ///             var extraField = entry.LocalHeaderExtraFields.GetExtraField&lt;WindowsSecurityDescriptorExtraField&gt;();
        ///
        ///             // これ以降、extraField オブジェクトのプロパティを参照することにより、NTFS のセキュリティディスクリプタを取得できる。
        ///         }
        ///     }
        /// }
        /// </code>
        /// </description>
        /// </item>
        /// <item>
        /// <term>[拡張フィールドのカスタマイズについて]</term>
        /// <description>
        /// <para>もし、あなたがこのソフトウェアでサポートされていない拡張フィールドの情報を取得したい場合には、以下の手順に従ってください。</para>
        /// <list type="number">
        /// <item><see cref="ExtraField"/> を継承した、拡張フィールドのクラスを作成する。</item>
        /// <item><see cref="IReadOnlyExtraFieldCollection.GetExtraField{EXTRA_FIELD_T}"/>メソッドを使用して拡張フィールドのオブジェクトを取得する。</item>
        /// <item>前項で取得した拡張フィールドのオブジェクトのプロパティを参照する。</item>
        /// </list>
        /// <para>拡張フィールドのクラスの実装例については、<see cref="WindowsSecurityDescriptorExtraField"/> クラスまたは <see cref="XceedUnicodeExtraField"/> クラスのソースコードを参照してください。</para>
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        public IReadOnlyExtraFieldCollection LocalHeaderExtraFields { get; }

        /// <summary>
        /// セントラルディレクトリヘッダの拡張フィールドのコレクションを取得します。
        /// </summary>
        /// <remarks>
        /// 拡張フィールドに関する詳細な説明については、<see cref="LocalHeaderExtraFields"/>プロパティの記事を参照してください。
        /// </remarks>
        public IReadOnlyExtraFieldCollection CentralDirectoryHeaderExtraFields { get; }

        /// <summary>
        /// エントリの最終更新日時(UTC)を取得します。
        /// </summary>
        /// <value>
        /// エントリに最終更新日時が設定されている場合はその日時を示す <see cref="DateTime"/> 構造体、そうではない場合は null が返ります。
        /// </value>
        public DateTime? LastWriteTimeUtc => LastWriteTimeOffsetUtc?.ToDateTime();

        /// <summary>
        /// エントリの最終更新日時(UTC)を取得します。
        /// </summary>
        /// <value>
        /// エントリに最終更新日時が設定されている場合はその日時を示す <see cref="DateTimeOffset"/> 構造体、そうではない場合は null が返ります。
        /// </value>
        public DateTimeOffset? LastWriteTimeOffsetUtc { get; }

        /// <summary>
        /// エントリの最終アクセス日時(UTC)を取得します。
        /// </summary>
        /// <value>
        /// エントリに最終アクセス日時が設定されている場合はその日時を示す <see cref="DateTime"/> 構造体、そうではない場合は null が返ります。
        /// </value>
        public DateTime? LastAccessTimeUtc => LastAccessTimeOffsetUtc?.ToDateTime();

        /// <summary>
        /// エントリの最終アクセス日時(UTC)を取得します。
        /// </summary>
        /// <value>
        /// エントリに最終アクセス日時が設定されている場合はその日時を示す <see cref="DateTimeOffset"/> 構造体、そうではない場合は null が返ります。
        /// </value>
        public DateTimeOffset? LastAccessTimeOffsetUtc { get; }

        /// <summary>
        /// エントリの作成日時(UTC)を取得します。
        /// </summary>
        /// <value>
        /// エントリに作成日時が設定されている場合はその日時を示す <see cref="DateTime"/> 構造体、そうではない場合は null が返ります。
        /// </value>
        public DateTime? CreationTimeUtc => CreationTimeOffsetUtc?.ToDateTime();

        /// <summary>
        /// エントリの作成日時(UTC)を取得します。
        /// </summary>
        /// <value>
        /// エントリに作成日時が設定されている場合はその日時を示す <see cref="DateTime"/> 構造体、そうではない場合は null が返ります。
        /// </value>
        public DateTimeOffset? CreationTimeOffsetUtc { get; }

        /// <summary>
        /// エントリのエントリ名の生のバイト列です。
        /// </summary>
        public ReadOnlyMemory<Byte> FullNameBytes { get; }

        /// <summary>
        /// エントリのコメントの生のバイト列です。
        /// </summary>
        public ReadOnlyMemory<Byte> CommentBytes { get; }

        /// <summary>
        /// エントリのエントリ名です。
        /// </summary>
        public String FullName { get; }

        /// <summary>
        /// エントリのコメントです。
        /// </summary>
        public String Comment { get; }

        /// <summary>
        /// エントリのエントリ名およびコメントのバイト列が文字列に変換されるエンコーディングを取得します。
        /// </summary>
        /// <value>
        /// エンコーディングが明示的に規定されていたならばそのエンコーディングを示す <see cref="Encoding"/> オブジェクトです。
        /// 明示的には規定されていなかった場合は null です。
        /// </value>
        public Encoding? ExactEntryEncoding { get; }

        /// <summary>
        /// エントリのエントリ名およびコメントのバイト列が文字列に変換されるエンコーディングの候補のコレクションを取得します。
        /// </summary>
        /// <value>
        /// エントリのエントリ名およびコメントのバイト列が文字列に変換されるエンコーディングの候補のコレクションです。候補となるエンコーディングが存在しない場合は空のコレクションです。
        /// </value>
        /// <remarks>
        /// <list type="bullet">
        /// <item><see cref="ExactEntryEncoding"/> プロパティの値が null ではない場合は、このプロパティの値のコレクションは空です。</item>
        /// <item>
        /// このプロパティが返す結果には、以下の条件をすべて満たすエンコーディングが格納されます。
        /// <list type="number">
        /// <item>実行中の .NET ランタイムでサポートされている。</item>
        /// <item>生のエントリ名およびコメントのバイト列をフォールバックエラーなしにデコードすることが可能である。(つまり文字化けなしにデコードできる)</item>
        /// <item>生のエントリ名およびコメントのバイト列をデコードした文字列を再エンコードした結果が、元の生のバイト列と一致する。</item>
        /// </list>
        /// </item>
        /// <item>このプロパティのエンコーディングのコレクションは「おそらくは正しいであろう」順番に並んでいます。しかし、この順番が常に正しいとは限らないことに注意してください。</item>
        /// </list>
        /// </remarks>
        public IEnumerable<Encoding> PossibleEntryEncodings { get; }

        /// <summary>
        /// 指定したパス名のファイルにこのエントリの最終更新日時・最終アクセス日時・作成日時を設定します。
        /// </summary>
        /// <param name="extractedEntryFilePath">
        /// 日時を設定する対象のファイルのフルパス名です。
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="extractedEntryFilePath"/> が null または 空文字列です。
        /// </exception>
        public void SeTimeStampToExtractedFile(String extractedEntryFilePath)
        {
            if (String.IsNullOrEmpty(extractedEntryFilePath))
                throw new ArgumentException($"'{nameof(extractedEntryFilePath)}' must not be nul or empty string.", nameof(extractedEntryFilePath));

            try
            {
                if (LastWriteTimeUtc is not null)
                    File.SetLastWriteTimeUtc(extractedEntryFilePath, LastWriteTimeUtc.Value);
            }
            catch (Exception)
            {
                // 対象のファイルが存在するファイルシステムと設定する時刻によって例外が発生することがあるが無視する。
            }

            try
            {
                if (CreationTimeUtc is not null)
                    File.SetCreationTimeUtc(extractedEntryFilePath, CreationTimeUtc.Value);
            }
            catch (Exception)
            {
                // 対象のファイルが存在するファイルシステムと設定する時刻によって例外が発生することがあるが無視する。
            }

            try
            {
                if (LastAccessTimeUtc is not null)
                    File.SetLastAccessTimeUtc(extractedEntryFilePath, LastAccessTimeUtc.Value);
            }
            catch (Exception)
            {
                // 対象のファイルが存在するファイルシステムと設定する時刻によって例外が発生することがあるが無視する。
            }
        }

        /// <summary>
        /// エントリのデータを読み込むためのストリームオブジェクトを取得します。
        /// </summary>
        /// <param name="progress">
        /// <para>
        /// 処理の進行状況の通知を受け取るためのオブジェクトです。通知を受け取らない場合は null です。
        /// </para>
        /// <para>
        /// 進行状況は、読み込みが完了したデータの長さを示すタプル値です。このタプル値は、圧縮されたデータの長さと解凍されたデータの長さのペアです。
        /// </para>
        /// </param>
        /// <returns>
        /// エントリのデータを読み込むためのストリームオブジェクトです。
        /// </returns>
        public ISequentialInputByteStream OpenContentStream(IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? progress = null)
        {
            var stream =
                CompressionMethodId.GetCompressionMethod()
                .CreateDecoderStream(
                    _zipStream.Stream.WithPartial(DataPosition, PackedSize, true),
                    GetDecoderOption(),
                    Size,
                    PackedSize,
                    progress)
                .WithCrc32Calculation(EndOfReadingStream);

            _zipStream.LockZipStream();

            return stream;

            void EndOfReadingStream(UInt32 actualCrc, UInt64 actualSize)
            {
                _zipStream.UnlockZipStream();
                if (actualCrc != Crc)
                    throw new BadZipFileFormatException($"CRC of entry data does not match. Perhaps the entry's data is corrupted.: \"{_zipReader.ZipArchiveFile.FullName}/{FullName}\"");
                if (actualSize != Size)
                    throw new BadZipFileFormatException($"Entry data lengths do not match. Perhaps the entry's data is corrupted.: \"{_zipReader.ZipArchiveFile.FullName}/{FullName}\"");
            }
        }

        /// <summary>
        /// エントリのデータが正しいか検証します。
        /// </summary>
        /// <param name="progress">
        /// <para>
        /// 処理の進行状況の通知を受け取るためのオブジェクトです。通知を受け取らない場合は null です。
        /// </para>
        /// <para>
        /// 進行状況は、読み込みが完了したデータの長さを示すタプル値です。このタプル値は、圧縮されたデータの長さと解凍されたデータの長さのペアです。
        /// </para>
        /// </param>
        /// <exception cref="BadZipFileFormatException">
        /// エントリの CRC が一致しません。おそらく、エントリのデータが破損しています。
        /// </exception>
        public void ValidateData(IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? progress = null)
        {
            if (!IsFile)
                return;

            _zipStream.LockZipStream();
            try
            {
                var actualCrc =
                    CompressionMethodId.GetCompressionMethod()
                    .CreateDecoderStream(
                        _zipStream.Stream.WithPartial(DataPosition, PackedSize, true),
                        GetDecoderOption(),
                        Size,
                        PackedSize,
                        progress)
                    .CalculateCrc32().Crc;
                if (actualCrc != Crc)
                    throw new BadZipFileFormatException($"Bad entry data: position=\"{LocalHeaderPosition}\", name=\"{FullName}\", desired crc=0x{Crc:x8}, actual crc=0x{actualCrc:x8}");
            }
            finally
            {
                _zipStream.UnlockZipStream();
            }
        }

        /// <summary>
        /// エントリのデータが正しいか非同期的に検証します。
        /// </summary>
        /// <param name="progress">
        /// <para>
        /// 処理の進行状況の通知を受け取るためのオブジェクトです。通知を受け取らない場合は null です。
        /// </para>
        /// <para>
        /// 進行状況は、読み込みが完了したデータの長さを示すタプル値です。このタプル値は、圧縮されたデータの長さと解凍されたデータの長さのペアです。
        /// </para>
        /// </param>
        /// <param name="cancellationToken">
        /// キャンセル要求を監視するためのトークンです。既定値は <see cref="CancellationToken.None"/> です。
        /// </param>
        /// <exception cref="BadZipFileFormatException">
        /// エントリの CRC が一致しません。おそらく、エントリのデータが破損しています。
        /// </exception>
        public async Task ValidateDataAsync(IProgress<(UInt64 inCompressedStreamProcessedCount, UInt64 outUncompressedStreamProcessedCount)>? progress = null, CancellationToken cancellationToken = default)
        {
            if (!IsFile)
                return;

            await _zipStream.LockZipStreamAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                var result =
                    await CompressionMethodId.GetCompressionMethod()
                    .CreateDecoderStream(
                        _zipStream.Stream.WithPartial(DataPosition, PackedSize, true),
                        GetDecoderOption(),
                        Size,
                        PackedSize,
                        progress)
                    .CalculateCrc32Async(cancellationToken)
                    .ConfigureAwait(false);
                if (result.Crc != Crc)
                    throw new BadZipFileFormatException($"Bad entry data: position=\"{LocalHeaderPosition}\", name=\"{FullName}\", desired crc=0x{Crc:x8}, actual crc=0x{result:x8}");
            }
            finally
            {
                _zipStream?.UnlockZipStream();
            }
        }

        /// <summary>
        /// オブジェクトの内容を分かりやすい文字列に変換します。
        /// </summary>
        /// <returns>
        /// オブジェクトの内容を示す文字列です。
        /// </returns>
        public override String ToString() => $"\"{_zipReader.ZipArchiveFile.FullName}/{FullName}\"";

        internal Boolean RequiredZip64ForLocalHeader { get; }
        internal Boolean RequiredZip64ForCentralDirectoryHeader { get; }
        internal ZipStreamPosition LocalHeaderPosition { get; }
        internal ZipStreamPosition DataPosition { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ICoderOption GetDecoderOption() => CompressionMethodId switch
        {
            ZipEntryCompressionMethodId.Stored => ZipStoredCompressionCoderOption.CreateDecoderOption(),
            ZipEntryCompressionMethodId.Deflate => ZipDeflateCompressionCoderOption.CreateDecoderOption(_generalPurposeBitFlag.GetDecoderParameter()),
            ZipEntryCompressionMethodId.Deflate64 => ZipDeflate64CompressionCoderOption.CreateDecoderOption(_generalPurposeBitFlag.GetDecoderParameter()),
            ZipEntryCompressionMethodId.BZIP2 => ZipBzip2CompressionCoderOption.CreateDecoderOption(),
            ZipEntryCompressionMethodId.LZMA => ZipLzmaCompressionCoderOption.CreateDecoderOption(_generalPurposeBitFlag.GetDecoderParameter()),
            _ => throw new CompressionMethodNotSupportedException(CompressionMethodId),
        };
    }
}
