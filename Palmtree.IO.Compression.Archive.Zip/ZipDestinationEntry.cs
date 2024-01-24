using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Palmtree.IO.Compression.Archive.Zip.ExtraFields;
using Palmtree.IO.Compression.Archive.Zip.Headers.Builder;
using Palmtree.Text;

namespace Palmtree.IO.Compression.Archive.Zip
{
    /// <summary>
    /// 出力先 ZIP ファイルのエントリのクラスです。
    /// </summary>
    public class ZipDestinationEntry
    {
        private class ExtraFieldCollection
            : IWriteOnlyExtraFieldCollection
        {
            private readonly ExtraFields.ExtraFieldCollection _localHeaderExtraFields;
            private readonly ExtraFields.ExtraFieldCollection _centralDirectoryHeaderExtraFields;

            public ExtraFieldCollection(ExtraFields.ExtraFieldCollection localHeaderExtraFields, ExtraFields.ExtraFieldCollection centralDirectoryHeaderExtraFields)
            {
                _localHeaderExtraFields = localHeaderExtraFields;
                _centralDirectoryHeaderExtraFields = centralDirectoryHeaderExtraFields;
            }

            public void Delete(UInt16 extraFieldId)
            {
                _localHeaderExtraFields.Delete(extraFieldId);
                _centralDirectoryHeaderExtraFields.Delete(extraFieldId);
            }

            public void AddExtraField<EXTRA_FIELD_T>(EXTRA_FIELD_T extraField)
                where EXTRA_FIELD_T : IExtraField
            {
                _localHeaderExtraFields.AddExtraField(extraField);
                _centralDirectoryHeaderExtraFields.AddExtraField(extraField);
            }
        }

        private static readonly Encoding _utf8Encoding;
        private static readonly Regex _dotEntryNamePattern;

        private readonly IZipFileWriterParameter _zipWriterParameter;
        private readonly IZipFileWriterOutputStreamAccesser _zipWriterStreamAccesser;
        private readonly ExtraFields.ExtraFieldCollection _localHeaderExtraFields;
        private readonly ExtraFields.ExtraFieldCollection _centralDirectoryHeaderExtraFields;
        private readonly ExtraFieldCollection _extraFields;
        private Boolean _isFile;
        private ZipEntryGeneralPurposeBitFlag _generalPurposeBitFlag;
        private ZipEntryCompressionMethodId _compressionMethodId;
        private ZipEntryCompressionLevel _compressionLevel;
        private DateTime? _lastWriteTimeUtc;
        private DateTime? _lastAccessTimeUtc;
        private DateTime? _creationTimeUtc;
        private UInt32? _externalAttributes;
        private ZipDestinationEntryFlag _flags;
        private UInt64 _size;
        private UInt64 _packedSize;
        private UInt32 _crc;
        private Boolean _written;

        static ZipDestinationEntry()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _utf8Encoding = Encoding.UTF8.WithFallback(null, null).WithoutPreamble();
            _dotEntryNamePattern = new Regex(@"(^|/|\\)\.{1,2}($|/|\\)", RegexOptions.Compiled);
        }

        internal ZipDestinationEntry(
            IZipFileWriterParameter zipWriterParameter,
            IZipFileWriterOutputStreamAccesser zipWriterStreamAccesser,
            UInt64 index,
            String fullName,
            ReadOnlyMemory<Byte> fullNameBytes,
            String comment,
            ReadOnlyMemory<Byte> commentBytes,
            Encoding? exactEntryEncoding,
            IEnumerable<Encoding> possibleEntryEncodings)
        {
            if (String.IsNullOrEmpty(fullName))
                throw new ArgumentException($"{nameof(fullName)} must not be null or empty.", nameof(fullName));
            if (fullNameBytes.Length <= 0)
                throw new InvalidOperationException($"The {nameof(fullNameBytes)} value must not be empty.");
            if (fullNameBytes.Length > UInt16.MaxValue)
                throw new InvalidOperationException($"The value of the {nameof(fullNameBytes)} is too long.: {fullNameBytes.Length} bytes");
            if (comment is null)
                throw new ArgumentNullException(nameof(comment));
            if (commentBytes.Length > UInt16.MaxValue)
                throw new InvalidOperationException($"The value of the {nameof(commentBytes)} is too long.: {commentBytes.Length} bytes");
            if (possibleEntryEncodings is null)
                throw new ArgumentNullException(nameof(possibleEntryEncodings));
            if (_dotEntryNamePattern.IsMatch(fullName))
                throw new ArgumentException($"Entry names containing directory names \".\" or \"..\" are not allowed.: {fullName}", nameof(fullName));

            _zipWriterParameter = zipWriterParameter ?? throw new ArgumentNullException(nameof(zipWriterParameter));
            _zipWriterStreamAccesser = zipWriterStreamAccesser ?? throw new ArgumentNullException(nameof(zipWriterStreamAccesser));
            _localHeaderExtraFields = new ExtraFields.ExtraFieldCollection(ZipEntryHeaderType.LocalHeader);
            _centralDirectoryHeaderExtraFields = new ExtraFields.ExtraFieldCollection(ZipEntryHeaderType.CentralDirectoryHeader);
            _extraFields = new ExtraFieldCollection(_localHeaderExtraFields, _centralDirectoryHeaderExtraFields);

            _generalPurposeBitFlag = ZipEntryGeneralPurposeBitFlag.None;
            _isFile = true;
            _compressionMethodId = ZipEntryCompressionMethodId.Stored;
            _compressionLevel = ZipEntryCompressionLevel.Normal;
            _lastWriteTimeUtc = null;
            _lastAccessTimeUtc = null;
            _creationTimeUtc = null;
            _externalAttributes = null;
            _flags = ZipDestinationEntryFlag.None;
            _size = 0;
            _packedSize = 0;
            _crc = 0;
            _written = false;

            Index = index;
            FullName = fullName;
            FullNameBytes = fullNameBytes;
            Comment = comment;
            CommentBytes = commentBytes;

            #region エントリのエンコーディングを決定する

            //
            // エントリのエンコーディングを決定する
            //

            _extraFields.Delete(UnicodePathExtraField.ExtraFieldId);
            _extraFields.Delete(UnicodeCommentExtraField.ExtraFieldId);
            _extraFields.Delete(CodePageExtraField.ExtraFieldId);

            if (exactEntryEncoding is not null)
            {
                // 確実なエンコーディングが与えられている場合

                if (ValidateEncoding(exactEntryEncoding, fullName, fullNameBytes.Span, comment, commentBytes.Span))
                {
                    // エントリのエンコーディングが明確に判明しており、かつ
                    // 与えられた文字列をエンコードしたバイト列と与えられたバイト列が一致している場合

                    // エンコーディングが何かはともかく、バイト列が示す文字はすべて UNICODE 文字セットにも含まれている文字である (.NET の文字列の内部表現は UNICODE (UTF-16) であるため)
                    // => 拡張フィールドの単純化のため、エントリ名とコメントを UTF-8 でエンコードしなおす

                    FullNameBytes = _utf8Encoding.GetReadOnlyBytes(fullName);
                    CommentBytes = _utf8Encoding.GetReadOnlyBytes(comment);

                    // エントリのエンコーディングが UTF-8 であることを示す汎用フラグを立てる
                    _generalPurposeBitFlag |= ZipEntryGeneralPurposeBitFlag.UseUnicodeEncodingForNameAndComment;
                }
                else
                {
                    // エントリのエンコーディングが明確に判明しており、かつ
                    // 与えられた文字列をエンコードしたバイト列と与えられたバイト列が一致していない場合

                    // バイト列が示す文字列の文字の中に、UNICODE にマッピングできない文字が含まれている (.NET の文字列の内部表現は UNICODE (UTF-16) であるため)

                    if ((fullNameBytes.Length > 0 || commentBytes.Length > 0)
                        && (fullName.Length > 0 || comment.Length > 0)
                        && !fullName.IsUnknownEncodingText()
                        && !comment.IsUnknownEncodingText())
                    {
                        // 有効なエントリ名またはコメントが与えられている場合

                        // 与えられたバイト列をテキストにデコードするための拡張フィールドを付加する
                        _extraFields.AddExtraField(
                            new CodePageExtraField
                            {
                                CodePage = exactEntryEncoding.CodePage,
                            });
                    }

                    if (fullNameBytes.Length > 0 && fullName.Length > 0 && !fullName.IsUnknownEncodingText())
                    {
                        // 有効なエントリ名文字列が与えられている場合

                        // 与えられたエントリ名文字列(UNICODE)の拡張フィールドを付加する
                        var extraField = new UnicodePathExtraField();
                        extraField.SetFullName(fullName, fullNameBytes.Span);
                        _extraFields.AddExtraField(extraField);
                    }

                    if (commentBytes.Length > 0 && comment.Length > 0 && !comment.IsUnknownEncodingText())
                    {
                        // 有効なコメント文字列が与えられている場合

                        // 与えられたコメント(UNICODE)の拡張フィールドを付加する
                        var extraField = new UnicodeCommentExtraField();
                        extraField.SetComment(comment, commentBytes.Span);
                        _extraFields.AddExtraField(extraField);
                    }
                }
            }
            else
            {
                // 確実なエンコーディングが与えられていない場合

                // 正しい可能性のあるエンコーディングを探す
                var possibleEntryEncoding =
                    possibleEntryEncodings
                    .Where(encoding => ValidateEncoding(encoding, fullName, fullNameBytes.Span, comment, commentBytes.Span))
                    .FirstOrDefault();

                if (possibleEntryEncoding is not null)
                {
                    // 確実に正しいエンコーディングが与えられておらず、かつ
                    // 正しい可能性のあるエンコーディングが存在する場合

                    // (エンコーディングが何かはともかく) バイト列に UNICODE 文字セットのみが含まれている
                    // エントリ名とコメントを UTF-8 でエンコードしなおす

                    FullNameBytes = _utf8Encoding.GetReadOnlyBytes(fullName);
                    CommentBytes = _utf8Encoding.GetReadOnlyBytes(comment);

                    // エントリのエンコーディングが UTF-8 であることを示す汎用フラグを立てる
                    _generalPurposeBitFlag |= ZipEntryGeneralPurposeBitFlag.UseUnicodeEncodingForNameAndComment;
                }
                else
                {
                    // 正しい可能性のあるエンコーディングが存在しない場合

                    if (fullNameBytes.Length > 0 && fullName.Length > 0 && !fullName.IsUnknownEncodingText())
                    {
                        // 有効なエントリ名文字列が与えられている場合

                        // 与えられたエントリ名文字列(UNICODE)の拡張フィールドを付加する
                        var extraField = new UnicodePathExtraField();
                        extraField.SetFullName(fullName, fullNameBytes.Span);
                        _extraFields.AddExtraField(extraField);
                    }

                    if (commentBytes.Length > 0 && comment.Length > 0 && !comment.IsUnknownEncodingText())
                    {
                        // 有効なコメント文字列が与えられている場合

                        // 与えられたコメント(UNICODE)の拡張フィールドを付加する
                        var extraField = new UnicodeCommentExtraField();
                        extraField.SetComment(comment, commentBytes.Span);
                        _extraFields.AddExtraField(extraField);
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// このエントリを識別する値を取得します。
        /// </summary>
        public UInt64 Index { get; }

        /// <summary>
        /// このエントリのエントリ名の文字列を取得します。
        /// </summary>
        public String FullName { get; }

        /// <summary>
        /// このエントリのエントリ名のバイト列を取得します。
        /// </summary>
        public ReadOnlyMemory<Byte> FullNameBytes { get; }

        /// <summary>
        /// このエントリのコメントの文字列を取得します。
        /// </summary>
        public String Comment { get; }

        /// <summary>
        /// このエントリのコメントのバイト列を取得します。
        /// </summary>
        public ReadOnlyMemory<Byte> CommentBytes { get; }

        /// <summary>
        /// このエントリがファイルかどうかを示す <see cref="Boolean"/> 値を取得または設定します。
        /// </summary>
        /// <value>
        /// ファイルであれば true、そうではないのなら false です。
        /// </value>
        public Boolean IsFile
        {
            get => _isFile;

            set
            {
                if (_written)
                    throw new InvalidOperationException();

                _isFile = value;
            }
        }

        /// <summary>
        /// このエントリがディレクトリかどうかを示す <see cref="Boolean"/> 値を取得または設定します。
        /// </summary>
        /// <value>
        /// ディレクトリであれば true、そうではないのなら false です。
        /// </value>
        public Boolean IsDirectory
        {
            get => !_isFile;

            set
            {
                if (_written)
                    throw new InvalidOperationException();

                _isFile = !value;
            }
        }

        /// <summary>
        /// 書き込むエントリの圧縮方式を示す値を取得または設定します。
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>必ずしもすべての圧縮方式がサポートされているわけではないことに注意してください。</item>
        /// </list>
        /// </remarks>
        public ZipEntryCompressionMethodId CompressionMethodId
        {
            get => _compressionMethodId;

            set
            {
                if (_written)
                    throw new InvalidOperationException();
                if (!ZipEntryCompressionMethod.SupportedCompresssionMethodIds.Contains(value))
                    throw new ArgumentException($"An unsupported compression method was specified.: {value}", nameof(value));

                _compressionMethodId = value;
            }
        }

        /// <summary>
        /// 書き込むエントリの圧縮率の高さを取得または設定します。
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>このプロパティの値は、非圧縮 (<see cref="_compressionMethodId"/> プロパティの値が <see cref="ZipEntryCompressionMethodId.Stored"/>) 以外の場合に意味を持ちます。</item>
        /// <item>このプロパティの値が意味を持たない圧縮方式も存在します。</item>
        /// </list>
        /// </remarks>
        public ZipEntryCompressionLevel CompressionLevel
        {
            get => _compressionLevel;

            set
            {
                if (_written)
                    throw new InvalidOperationException();

                _compressionLevel = value;
            }
        }

        /// <summary>
        /// 書き込むエントリの最終更新日時(UTC)を取得または設定します。
        /// </summary>
        /// <value>
        /// 最終更新日時(UTC)を示す <see cref="DateTime"/> オブジェクトです。 既定値は null です。
        /// </value>
        /// <remarks>
        /// <list type="bullet">
        /// <item>このプロパティの値が null である場合、エントリの最終更新日時として代わりに現在日時が付加されます。</item>
        /// </list>
        /// </remarks>
        public DateTime? LastWriteTimeUtc
        {
            get => _lastWriteTimeUtc;

            set
            {
                if (_written)
                    throw new InvalidOperationException();
                if (value is not null && value.Value.Kind == DateTimeKind.Unspecified)
                    throw new ArgumentException($"Setting a value where the {nameof(value.Value.Kind)} property is {nameof(DateTimeKind.Unspecified)} is prohibited.", nameof(value));

                _lastWriteTimeUtc = value?.ToUniversalTime();
            }
        }

        /// <summary>
        /// 書き込むエントリの最終アクセス日時(UTC)を取得または設定します。
        /// </summary>
        /// <value>
        /// 最終アクセス日時(UTC)を示す <see cref="DateTime"/> オブジェクトです。 既定値は null です。
        /// </value>
        /// <remarks>
        /// <list type="bullet">
        /// <item>このプロパティの値が null である場合、エントリの最終アクセス日時は付加されません</item>
        /// </list>
        /// </remarks>
        public DateTime? LastAccessTimeUtc
        {
            get => _lastAccessTimeUtc;

            set
            {
                if (_written)
                    throw new InvalidOperationException();
                if (value is not null && value.Value.Kind == DateTimeKind.Unspecified)
                    throw new ArgumentException($"Setting a value where the {nameof(value.Value.Kind)} property is {nameof(DateTimeKind.Unspecified)} is prohibited.", nameof(value));

                _lastAccessTimeUtc = value?.ToUniversalTime();
            }
        }

        /// <summary>
        /// 書き込むエントリの作成日時(UTC)を取得または設定します。
        /// </summary>
        /// <value>
        /// 作成日時(UTC)を示す <see cref="DateTime"/> オブジェクトです。 既定値は null です。
        /// </value>
        /// <remarks>
        /// <list type="bullet">
        /// <item>このプロパティの値が null である場合、エントリの作成日時は付加されません</item>
        /// </list>
        /// </remarks>
        public DateTime? CreationTimeUtc
        {
            get => _creationTimeUtc;

            set
            {
                if (_written)
                    throw new InvalidOperationException();
                if (value is not null && value.Value.Kind == DateTimeKind.Unspecified)
                    throw new ArgumentException($"Setting a value where the {nameof(value.Value.Kind)} property is {nameof(DateTimeKind.Unspecified)} is prohibited.", nameof(value));

                _creationTimeUtc = value?.ToUniversalTime();
            }
        }

        /// <summary>
        /// 書き込むエントリのファイル属性を取得または設定します。
        /// </summary>
        /// <value>
        /// <list type="bullet">
        /// <item>このプロパティの意味は実行時の OS により異なります。
        /// <list type="bullet">
        /// <item>Windows または MS-DOS 系 OS の場合は <see cref="ExternalAttributesForDos"/> を参考にしてください。</item>
        /// <item>UNIX 系 OS の場合は <see cref="ExternalAttributesForUnix"/> を参考にしてください。</item>
        /// </list>
        /// </item>
        /// <item>既定値は実行時の OS による固有の値です。</item>
        /// </list>
        /// </value>
        public UInt32 ExternalAttributes
        {
            get
            {
                if (_externalAttributes is not null)
                    return _externalAttributes.Value;

                if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
                {
                    if (IsDirectory)
                        return (UInt32)(ExternalAttributesForUnix.DOS_DIRECTORY | ExternalAttributesForUnix.UNX_IFDIR | ExternalAttributesForUnix.UNX_IROTH | ExternalAttributesForUnix.UNX_IXOTH | ExternalAttributesForUnix.UNX_IRGRP | ExternalAttributesForUnix.UNX_IXGRP | ExternalAttributesForUnix.UNX_IRUSR | ExternalAttributesForUnix.UNX_IWUSR | ExternalAttributesForUnix.UNX_IXUSR);
                    else
                        return (UInt32)(ExternalAttributesForUnix.DOS_ARCHIVE | ExternalAttributesForUnix.UNX_IFREG | ExternalAttributesForUnix.UNX_IROTH | ExternalAttributesForUnix.UNX_IRGRP | ExternalAttributesForUnix.UNX_IRUSR | ExternalAttributesForUnix.UNX_IWUSR);
                }
                else
                {
                    if (IsDirectory)
                        return (UInt32)ExternalAttributesForDos.DOS_DIRECTORY;
                    else
                        return (UInt32)ExternalAttributesForDos.DOS_ARCHIVE;
                }
            }

            set
            {
                if (_written)
                    throw new InvalidOperationException();

                _externalAttributes = value;
            }
        }

        /// <summary>
        /// エントリの書き込み際の特殊な動作を指定するフラグを取得または設定します。
        /// </summary>
        /// <value>
        /// エントリの書き込み際の特殊な動作を意味する <see cref="ZipDestinationEntryFlag"/> 列挙体です。
        /// </value>
        public ZipDestinationEntryFlag Flags
        {
            get => _flags;

            set
            {
                if (_written)
                    throw new InvalidOperationException();

                _flags = value;
            }
        }

        /// <summary>
        /// 拡張フィールドを設定するためのオブジェクトを取得します。
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
        /// <term>[拡張フィールドの仕様について]</term>
        /// <description>
        /// <para>よく知られている拡張フィールドの仕様については、<see href="https://libzip.org/specifications/extrafld.txt">info-zip の記事</see> が一番詳しいようです。</para>
        /// </description>
        /// </item>
        /// <item>
        /// <term>[拡張フィールドの設定方法]</term>
        /// <description>
        /// <para>NTFS のセキュリティディスクリプタを保持する拡張フィールドを設定するサンプルプログラムを以下に示します。</para>
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
        ///         using var writer = new FilePath(args[0]).CreateAsZipFile(ZipEntryNameEncodingProvider.Create(Array.Empty&lt;string&gt;(), Array.Empty&lt;string&gt;()));
        ///
        ///         // "note.txt" というエントリ名を作る。
        ///         var entry = writer.CreateEntry("note.txt");
        ///
        ///         // "note.txt"の NTFS セキュリティディスクリプタ の設定を行う
        ///         // NTFS のセキュリティディスクリプタを保持する拡張フィールドを実装しているクラスは <see cref="WindowsSecurityDescriptorExtraField"/> なので、<see cref="WindowsSecurityDescriptorExtraField"/> 型のオブジェクトを ExtraFields AddExtraField メソッドに与える。
        ///         entry.ExtraFields.AddExtraField(
        ///             new WindowsSecurityDescriptorExtraField()
        ///             {
        ///                 // 各種プロパティの設定を行う
        ///             });
        ///         using var dataStream = entry.GetContentStream();
        ///
        ///         //これ以降、dataStream に対してデータの書き込みを行う
        ///
        ///     }
        /// }
        /// </code>
        /// </description>
        /// </item>
        /// <item>
        /// <term>[拡張フィールドのカスタマイズについて]</term>
        /// <description>
        /// <para>もし、あなたがこのソフトウェアでサポートされていない拡張フィールドの設定を取得したい場合には、以下の手順に従ってください。</para>
        /// <list type="number">
        /// <item><see cref="ExtraField"/> を継承した、拡張フィールドのクラスを定義する。</item>
        /// <item>前項で定義した拡張フィールドのクラスのインスタンスを作成し、必要なプロパティを設定する。</item>
        /// <item><see cref="IWriteOnlyExtraFieldCollection.AddExtraField{EXTRA_FIELD_T}(EXTRA_FIELD_T)"/>メソッドを使用して前項で作成した拡張フィールドのインスタンスをコレクションに追加する。</item>
        /// </list>
        /// <para>拡張フィールドのクラスの実装例については、<see cref="WindowsSecurityDescriptorExtraField"/> クラスまたは <see cref="XceedUnicodeExtraField"/> クラスのソースコードを参照してください。</para>
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        public IWriteOnlyExtraFieldCollection ExtraFields => _extraFields;

        /// <summary>
        /// 書き込まれたデータの長さを取得します。
        /// </summary>
        public UInt64 Size
        {
            get
            {
                if (!_written)
                    throw new InvalidOperationException();

                return _size;
            }
        }

        /// <summary>
        /// 書き込まれたデータの圧縮された長さを取得します。
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>圧縮方式が <see cref="ZipEntryCompressionMethodId.Stored"/> の場合は、このプロパティの値は <see cref="Size"/> の値に等しくなります。</item>
        /// </list>
        /// </remarks>
        public UInt64 PackedSize
        {
            get
            {
                if (!_written)
                    throw new InvalidOperationException();

                return _packedSize;
            }
        }

        /// <summary>
        /// 書き込まれたデータの CRC 値を取得します。
        /// </summary>
        public UInt32 Crc
        {
            get
            {
                if (!_written)
                    throw new InvalidOperationException();

                return _crc;
            }
        }

        /// <summary>
        /// エントリのデータを書き込むためのストリームを取得します。
        /// </summary>
        /// <param name="progress">
        /// <para>
        /// 処理の進行状況の通知を受け取るためのオブジェクトです。通知を受け取らない場合は null です。
        /// </para>
        /// <para>
        /// 進行状況は、書き込みが完了したデータのバイト数です。
        /// </para>
        /// </param>
        /// <returns>
        /// エントリのデータの出力先ストリームのオブジェクトです。
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// 既にデータは出力済みです。
        /// </exception>
        public ISequentialOutputByteStream CreateContentStream(IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress = null)
        {
            _zipWriterStreamAccesser.LockZipStream();
            _zipWriterStreamAccesser.BeginToWriteContent();
            var success = false;
            try
            {
                if (_written)
                    throw new InvalidOperationException();
                if (!_isFile)
                    throw new InvalidOperationException();
                if (LastWriteTimeUtc is not null && LastWriteTimeUtc.Value.Kind == DateTimeKind.Unspecified)
                    throw new InvalidOperationException($"The value of {nameof(LastWriteTimeUtc)}.{nameof(LastWriteTimeUtc.Value.Kind)} property must not be {nameof(DateTimeKind)}.{nameof(DateTimeKind.Unspecified)}.");
                if (LastAccessTimeUtc is not null && LastAccessTimeUtc.Value.Kind == DateTimeKind.Unspecified)
                    throw new InvalidOperationException($"The value of {nameof(LastAccessTimeUtc)}.{nameof(LastAccessTimeUtc.Value.Kind)} property must not be {nameof(DateTimeKind)}.{nameof(DateTimeKind.Unspecified)}.");
                if (CreationTimeUtc is not null && CreationTimeUtc.Value.Kind == DateTimeKind.Unspecified)
                    throw new InvalidOperationException($"The value of {nameof(CreationTimeUtc)}.{nameof(CreationTimeUtc.Value.Kind)} property must not be {nameof(DateTimeKind)}.{nameof(DateTimeKind.Unspecified)}.");

                var stream =
                    _flags.HasFlag(ZipDestinationEntryFlag.UseDataDescriptor)
                    ? CreateContentStreamWithDataDescriptor(progress)
                    : CreateContentStreamWithoutDataDescriptor(progress);
                success = true;
                return stream;
            }
            finally
            {
                if (!success)
                {
                    _zipWriterStreamAccesser.SetErrorMark();
                    _zipWriterStreamAccesser.UnlockZipStream();
                }
            }
        }

        /// <summary>
        /// まだ書き込まれていないデータを書き込みます。
        /// </summary>
        public void Flush()
        {
            _zipWriterStreamAccesser.LockZipStream();
            try
            {
                InternalFlush();
            }
            finally
            {
                _zipWriterStreamAccesser.UnlockZipStream();
            }
        }

        /// <summary>
        /// オブジェクトの内容を分かりやすい文字列に変換します。
        /// </summary>
        /// <returns>
        /// オブジェクトの内容を示す文字列です。
        /// </returns>
        public override String ToString() => $"\"{_zipWriterParameter.ZipArchiveFile.FullName}/{FullName}\"";

        internal void InternalFlush()
        {
            if (!_written)
            {
                // GetContentStream() が呼ばれないまま Flush() または Dispose() が呼び出された場合

                _size = 0;
                _packedSize = 0;
                _crc = Array.Empty<Byte>().CalculateCrc32().Crc;

                SetupExtraFields(_extraFields, LastWriteTimeUtc, LastAccessTimeUtc, CreationTimeUtc);
                if (_flags.HasFlag(ZipDestinationEntryFlag.DoNotUseExtraFieldsInLocalHeaders))
                    ModifyExtraFields(_localHeaderExtraFields);

                var localHeader =
                    ZipEntryLocalHeader.Build(
                        _zipWriterParameter,
                        _generalPurposeBitFlag,
                        ZipEntryCompressionMethodId.Stored,
                        _size,
                        _packedSize,
                        _crc,
                        _localHeaderExtraFields,
                        FullNameBytes,
                        LastWriteTimeUtc,
                        IsDirectory,
                        _flags.HasFlag(ZipDestinationEntryFlag.DoNotUseExtraFieldsInLocalHeaders));
                var localHeaderPosition = localHeader.WriteTo(_zipWriterStreamAccesser.MainStream);

                var centralDirectoryHeader =
                    ZipEntryCentralDirectoryHeader.Build(
                        _zipWriterParameter,
                        localHeaderPosition,
                        _generalPurposeBitFlag,
                        ZipEntryCompressionMethodId.Stored,
                        _size,
                        _packedSize,
                        _crc,
                        ExternalAttributes,
                        _centralDirectoryHeaderExtraFields,
                        FullNameBytes,
                        CommentBytes,
                        LastWriteTimeUtc,
                        IsDirectory,
                        false);
                _zipWriterStreamAccesser.StreamForCentralDirectoryHeaders.WriteUInt32LE(centralDirectoryHeader.Length);
                centralDirectoryHeader.WriteTo(_zipWriterStreamAccesser.StreamForCentralDirectoryHeaders);

                _written = true;
            }
        }

        private ISequentialOutputByteStream CreateContentStreamWithoutDataDescriptor(IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress)
        {
            var temporaryFile = (FilePath?)null;
            var packedTemporaryFile = (FilePath?)null;
            var success = false;
            try
            {
                try
                {
                    progress?.Report((0, 0));
                }
                catch (Exception)
                {
                }

                SetupExtraFields(_extraFields, LastWriteTimeUtc, LastAccessTimeUtc, CreationTimeUtc);
                if (_flags.HasFlag(ZipDestinationEntryFlag.DoNotUseExtraFieldsInLocalHeaders))
                    ModifyExtraFields(_localHeaderExtraFields);

                var compressionMethod = CompressionMethodId.GetCompressionMethod();
                var compressionOption = CompressionMethodId.GetEncoderOption(CompressionLevel);

                temporaryFile = new FilePath(Path.GetTempFileName());
                packedTemporaryFile =
                    CompressionMethodId == ZipEntryCompressionMethodId.Stored
                    ? null
                    : new FilePath(Path.GetTempFileName());

                var outputStrem =
                    temporaryFile.Create()
                    .WithCache();

                var packedOutputStream =
                    packedTemporaryFile is null
                    ? null
                    : compressionMethod.CreateEncoderStream(
                        packedTemporaryFile.Create()
                            .WithCache(),
                        compressionOption,
                        progress?.Cast<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount), (UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>(
                            value => (value.inUncompressedStreamProcessedCount / 2, value.outCompressedStreamProcessedCount / 2)));

                var tempraryFileStream =
                    (packedOutputStream is null ? (progress is null ? outputStrem : outputStrem.WithProgression(new SimpleProgress<UInt64>(value => progress.Report((value / 2, value / 2))))) : outputStrem.Branch(packedOutputStream))
                    .WithCrc32Calculation(EndOfCopyingToTemporaryFile);
                success = true;
                return tempraryFileStream;
            }
            finally
            {
                if (!success)
                {
                    temporaryFile?.SafetyDelete();
                    packedTemporaryFile?.SafetyDelete();
                }
            }

            void EndOfCopyingToTemporaryFile(UInt32 actualCrc, UInt64 actualSize)
            {
                var success = false;
                try
                {
                    Validation.Assert(temporaryFile is not null && temporaryFile.Exists, "temporaryFile is not null && temporaryFile.Exists");
                    Validation.Assert(CompressionMethodId == ZipEntryCompressionMethodId.Stored == (packedTemporaryFile is null), "CompressionMethodId == ZipEntryCompressionMethodId.Stored != (packedTemporaryFile is null)");
                    var size = temporaryFile.Length;
                    var packedSize = temporaryFile.Length;
                    var crc = actualCrc;
                    Validation.Assert(size == actualSize, "size == actualSize");
                    if (packedTemporaryFile is not null)
                    {
                        Validation.Assert(packedTemporaryFile.Exists, "packedTemporaryFile.Exists");
                        packedSize = packedTemporaryFile.Length;

                        if (size <= 0 || packedSize >= size)
                        {
                            // 圧縮前のサイズが 0、または圧縮後のサイズが圧縮前のサイズより小さくなっていない場合

                            // 圧縮方式を強制的に Stored に変更する。
                            CompressionMethodId = ZipEntryCompressionMethodId.Stored;
                            CompressionLevel = ZipEntryCompressionLevel.Normal;
                            packedSize = size;
                            packedTemporaryFile.SafetyDelete();
                            packedTemporaryFile = null;
                        }
                    }

                    var progressRate = (Double)size / packedSize / 2;
                    var inUncompressedStreamProcessedCountProgress = size / 2;
                    var outCompressedStreamProcessedCountProgress = packedSize / 2;

                    _generalPurposeBitFlag |= CompressionMethodId.GettEncoderOptionFlags(CompressionLevel);

                    var localHeader =
                        ZipEntryLocalHeader.Build(
                            _zipWriterParameter,
                            _generalPurposeBitFlag,
                            CompressionMethodId,
                            size,
                            packedSize,
                            crc,
                            _localHeaderExtraFields,
                            FullNameBytes,
                            LastWriteTimeUtc,
                            IsDirectory,
                            _flags.HasFlag(ZipDestinationEntryFlag.DoNotUseExtraFieldsInLocalHeaders));
                    var localHeaderPosition = localHeader.WriteTo(_zipWriterStreamAccesser.MainStream);

                    var centralDirectoryHeader =
                        ZipEntryCentralDirectoryHeader.Build(
                            _zipWriterParameter,
                            localHeaderPosition,
                            _generalPurposeBitFlag,
                            CompressionMethodId,
                            size,
                            packedSize,
                            crc,
                            ExternalAttributes,
                            _centralDirectoryHeaderExtraFields,
                            FullNameBytes,
                            CommentBytes,
                            LastWriteTimeUtc,
                            IsDirectory,
                            false);
                    _zipWriterStreamAccesser.StreamForCentralDirectoryHeaders.WriteUInt32LE(centralDirectoryHeader.Length);
                    centralDirectoryHeader.WriteTo(_zipWriterStreamAccesser.StreamForCentralDirectoryHeaders);

                    using var sourceStream = (packedTemporaryFile is null ? temporaryFile : packedTemporaryFile).OpenRead();
                    sourceStream.CopyTo(
                        _zipWriterStreamAccesser.MainStream,
                        progress?.Cast<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount), UInt64>(compressedCount => (checked(inUncompressedStreamProcessedCountProgress + (UInt64)(compressedCount * progressRate)), checked(outCompressedStreamProcessedCountProgress + compressedCount / 2))));
                    _size = size;
                    _packedSize = packedSize;
                    _crc = crc;

                    try
                    {
                        progress?.Report((size, packedSize));
                    }
                    catch (Exception)
                    {
                    }

                    _zipWriterStreamAccesser.EndToWritingContent();
                    _written = true;
                    success = true;
                }
                finally
                {
                    if (!success)
                        _zipWriterStreamAccesser.SetErrorMark();
                    temporaryFile?.SafetyDelete();
                    packedTemporaryFile?.SafetyDelete();
                    _zipWriterStreamAccesser.UnlockZipStream();
                }
            }
        }

        private ISequentialOutputByteStream CreateContentStreamWithDataDescriptor(IProgress<(UInt64 inUncompressedStreamProcessedCount, UInt64 outCompressedStreamProcessedCount)>? progress)
        {
            var packedSizeHolder = new ValueHolder<UInt64>();
            var localHeaderPosition = (ZipStreamPosition?)null;
            try
            {
                progress?.Report((0, 0));
            }
            catch (Exception)
            {
            }

            SetupExtraFields(_extraFields, LastWriteTimeUtc, LastAccessTimeUtc, CreationTimeUtc);
            if (_flags.HasFlag(ZipDestinationEntryFlag.DoNotUseExtraFieldsInLocalHeaders))
                ModifyExtraFields(_localHeaderExtraFields);

            var compressionMethod = CompressionMethodId.GetCompressionMethod();
            var compressionOption = CompressionMethodId.GetEncoderOption(CompressionLevel);
            _generalPurposeBitFlag |= CompressionMethodId.GettEncoderOptionFlags(CompressionLevel);

            var localHeaderInfo =
                ZipEntryLocalHeader.Build(
                    _zipWriterParameter,
                    _generalPurposeBitFlag,
                    CompressionMethodId,
                    _localHeaderExtraFields,
                    FullNameBytes,
                    LastWriteTimeUtc,
                    IsDirectory);
            localHeaderPosition = localHeaderInfo.WriteTo(_zipWriterStreamAccesser.MainStream);

            var contentStream =
                compressionMethod.CreateEncoderStream(
                    _zipWriterStreamAccesser.MainStream
                        .WithEndAction(packedSize => packedSizeHolder.Value = packedSize, true),
                    compressionOption,
                    progress)
                .WithCrc32Calculation(EndOfWrintingContents);
            return contentStream;

            void EndOfWrintingContents(UInt32 actualCrc, UInt64 actualSize)
            {
                var success = false;
                try
                {
                    Validation.Assert(localHeaderPosition is not null, "localHeaderPosition is not null");
                    var actualPackedSize = packedSizeHolder.Value;
                    var dataDescriptor = ZipEntryDataDescriptor.Build(actualCrc, actualSize, actualPackedSize);
                    var centralDirectoryHeader =
                        ZipEntryCentralDirectoryHeader.Build(
                            _zipWriterParameter,
                            localHeaderPosition.Value,
                            _generalPurposeBitFlag,
                            CompressionMethodId,
                            actualSize,
                            actualPackedSize,
                            actualCrc,
                            ExternalAttributes,
                            _centralDirectoryHeaderExtraFields,
                            FullNameBytes,
                            CommentBytes,
                            LastWriteTimeUtc,
                            IsDirectory,
                            true);
                    _zipWriterStreamAccesser.StreamForCentralDirectoryHeaders.WriteUInt32LE(centralDirectoryHeader.Length);
                    centralDirectoryHeader.WriteTo(_zipWriterStreamAccesser.StreamForCentralDirectoryHeaders);

                    dataDescriptor.WriteTo(_zipWriterStreamAccesser.MainStream);
                    _size = actualSize;
                    _packedSize = actualPackedSize;
                    _crc = actualCrc;

                    try
                    {
                        progress?.Report((actualSize, actualPackedSize));
                    }
                    catch (Exception)
                    {
                    }

                    _zipWriterStreamAccesser.EndToWritingContent();
                    _written = true;
                    success = true;
                }
                finally
                {
                    if (!success)
                        _zipWriterStreamAccesser.SetErrorMark();
                    _zipWriterStreamAccesser.UnlockZipStream();
                }
            }
        }

        /// <summary>
        /// エンコーディングの検証をします。
        /// </summary>
        /// <param name="encoding">
        /// 検証対象のエンコーディングです。
        /// </param>
        /// <param name="fullName">
        /// 検証のために使用するエントリ名の文字列です。
        /// </param>
        /// <param name="fullNameBytes">
        /// 検証のために使用するエントリ名のバイト列です。
        /// </param>
        /// <param name="comment">
        /// 検証のために使用するコメントの文字列です。
        /// </param>
        /// <param name="commentBytes">
        /// 検証のために使用するコメントのバイト列です。
        /// </param>
        /// <returns>
        /// 検証が成功した場合は true、そうではない場合は false を返します。
        /// </returns>
        /// <remarks>
        /// <para>
        /// このメソッドは、以下の条件で true を返します。
        /// </para>
        /// <list type="number">
        /// <item> <paramref name="encoding"/> により <paramref name="fullName"/> をエンコードした結果が <paramref name="fullNameBytes"/> に等しく、かつ </item>
        /// <item> <paramref name="encoding"/> により <paramref name="fullNameBytes"/> をデコードした結果が <paramref name="fullName"/> に等しく、かつ </item>
        /// <item> <paramref name="encoding"/> により <paramref name="comment"/> をエンコードした結果が <paramref name="commentBytes"/> に等しく、かつ </item>
        /// <item> <paramref name="encoding"/> により <paramref name="commentBytes"/> をデコードした結果が <paramref name="comment"/> に等しい場合</item>
        /// </list>
        /// <para>
        /// エンコーディングが正しくても、<paramref name="fullNameBytes"/> または <paramref name="commentBytes"/> に UNICODE にマッピングできない文字が含まれている場合には
        /// 上記の条件は成立しないことに注意してください。
        /// </para>
        /// </remarks>
        private static Boolean ValidateEncoding(Encoding encoding, String fullName, ReadOnlySpan<Byte> fullNameBytes, String comment, ReadOnlySpan<Byte> commentBytes)
        {
            // このメソッドは、エンコーディングが正しくない場合以外にも、
            // fullNameBytes または commentBytes に UNICODE にマッピングできない文字が含まれている場合にも false を返すことに注意。
            try
            {
                var encodingForTest = encoding.WithFallback(null, null).WithoutPreamble();
                var triedFullName = encodingForTest.GetString(fullNameBytes);
                var triedComment = encodingForTest.GetString(commentBytes);
                var triedFullNameBytes = encodingForTest.GetBytes(fullName);
                var triedCommentBytes = encodingForTest.GetBytes(comment);
                return
                    triedFullName == fullName
                    && triedComment == comment
                    && triedFullNameBytes.SequenceEqual(fullNameBytes)
                    && triedCommentBytes.SequenceEqual(commentBytes);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void SetupExtraFields(ExtraFieldCollection extraFields, DateTime? lastWriteTimeUtc, DateTime? lastAccessTimeUtc, DateTime? creationTimeUtc)
        {
            //
            // 高精度日時の何れかが指定されている場合は、拡張フィールドに設定する
            //

            extraFields.Delete(NtfsExtraField.ExtraFieldId);
            extraFields.Delete(ExtendedTimestampExtraField.ExtraFieldId);

            var windowsTimestampExtraField = new NtfsExtraField()
            {
                LastWriteTimeUtc = lastWriteTimeUtc,
                LastAccessTimeUtc = lastAccessTimeUtc,
                CreationTimeUtc = creationTimeUtc,
            };
            var unixTimeStampExtraField = new ExtendedTimestampExtraField()
            {

                LastWriteTimeUtc = lastWriteTimeUtc,
                LastAccessTimeUtc = lastAccessTimeUtc,
                CreationTimeUtc = creationTimeUtc,
            };
            extraFields.AddExtraField(windowsTimestampExtraField);
            extraFields.AddExtraField(unixTimeStampExtraField);
        }

        private static void ModifyExtraFields(ExtraFields.ExtraFieldCollection localHeaderExtraFields)
        {
            if (localHeaderExtraFields.Contains(CodePageExtraField.ExtraFieldId) ||
                localHeaderExtraFields.Contains(UnicodePathExtraField.ExtraFieldId) ||
                localHeaderExtraFields.Contains(UnicodeCommentExtraField.ExtraFieldId))
            {
                // 上記の拡張フィールドが設定されるのは、エントリのファイル名またはコメントに UNICODE と互換性のない文字が含まれている場合のみ

                throw new InvalidOperationException($"The entry's filename or comment uses a character set that is incompatible with UNICODE, even though the {nameof(ZipDestinationEntryFlag.DoNotUseExtraFieldsInLocalHeaders)} flag was specified.");
            }

            localHeaderExtraFields.Delete(CodePageExtraField.ExtraFieldId); // エントリのファイル名およびコメントの明示的なコードページ指定の削除
            localHeaderExtraFields.Delete(UnicodePathExtraField.ExtraFieldId); // エントリのファイル名の UNICODE 表現の指定を削除
            localHeaderExtraFields.Delete(UnicodeCommentExtraField.ExtraFieldId); // エントリのファイル名の UNICODE 表現の指定を削除
            localHeaderExtraFields.Delete(NtfsExtraField.ExtraFieldId); // エントリの NTFS タイムスタンプを削除
            localHeaderExtraFields.Delete(ExtendedTimestampExtraField.ExtraFieldId); // エントリの UNIX タイムスタンプを削除

            if (localHeaderExtraFields.Count > 0)
                throw new InvalidOperationException($"The extra fields [{String.Join(", ", localHeaderExtraFields.EnumerateExtraFieldIds().Select(id => $"0x{id:x4}"))}] is specified even though the {nameof(ZipDestinationEntryFlag.DoNotUseExtraFieldsInLocalHeaders)} flag is specified.");
        }
    }
}
