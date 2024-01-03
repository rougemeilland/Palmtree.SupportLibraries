using System;

namespace Palmtree.IO.Compression.Archive.Zip
{
    /// <summary>
    /// ZIP アーカイブに対する検証の厳格さを示す列挙体です。
    /// </summary>
    [Flags]
    public enum ValidationStringency
    {
        /// <summary>
        /// 通常の検証を行います。
        /// </summary>
        Normal = 0,

        /// <summary>
        /// ヘッダまたはデータの間にある用途不明の隙間の存在を許容しません。
        /// </summary>
        /// <remarks>
        /// <para>
        /// 例えば、以下のような場所には「隙間」があっても、ZIP フォーマットの仕様上、問題なく読み取ることが出来ます。しかし、このフラグを指定することにより、そういった「隙間」の存在は不正であるとみなされます。
        /// </para>
        /// <list type="bullet">
        /// <item>最後のディスク上のセントラルディレクトリヘッダ と EOCDR との間</item>
        /// <item>エントリのファイルデータと、次のローカルヘッダ (あるいはセントラルディレクトリヘッダ) との間</item>
        /// </list>
        /// </remarks>
        DisallowUnknownPayloadExists = 1 << 0,

        /// <summary>
        /// 各種ヘッダ上の ZIP アーカイブ上のデータを指すポインタ (ディスク番号と、そのディスクの先頭からのオフセットのペア) が、ディスクの終端を指すことを許容しません。
        /// </summary>
        /// <remarks>
        /// <para>
        /// 通常は、ポインタがとあるディスクの終端を指している場合は、それは次のディスクの先頭に等しいとみなされます。
        /// </para>
        /// <para>
        /// しかし、このフラグを設定した場合、ディスクの終端を指すポインタは不正であるとみなされます。
        /// </para>
        /// </remarks>
        DisallowPointerToEndOfDisk = 1 << 1,

        /// <summary>
        /// EOCDRの後にある、内容がすべて 0 であるペイロードの存在を許容します。
        /// </summary>
        /// <remarks>
        /// <para>
        /// .epub ファイルにおいて、EOCDR の後に 0 が埋め込まれていることがあるようです。
        /// </para>
        /// </remarks>
        AllowNullPayloadAfterEOCDR = 1 << 2,

        /// <summary>
        /// 拡張フィールドの値のチェックを厳密に行います。
        /// </summary>
        /// <remarks>
        /// <para>
        /// このフラグを指定することにより、例えば以下のような場合にエラーとみなされます。
        /// </para>
        /// <list type="bullet">
        /// <item>セントラルディレクトリヘッダの拡張フィールド "Extended Timestamp Extra Field" (0x5455) に、更新日時以外のタイムスタンプが含まれている場合</item>
        /// <item>ローカルヘッダに拡張フィールド "PKWARE Win95/WinNT Extra Field" (0x000a) が存在する場合</item>
        /// </list>
        /// </remarks>
        StrictlyCheckExtraFieldValues = 1 << 3,

        /// <summary>
        /// 一部の well known ではない拡張フィールドを許容しません。
        /// </summary>
        DisallowNotWellKnownExtraField = 1 << 4,

        /// <summary>
        /// 最後のディスク上のセントラルディレクトリヘッダ数を厳密にチェックします。
        /// </summary>
        /// <remarks>
        /// <para>
        /// 決して無視できないシェアを誇るとある ZIP アーカイブソフトウェアにおいて、以下の不具合があることが判明しているため、このチェックは既定では行われません。
        /// </para>
        /// <list type="bullet">
        /// <item>
        /// EOCDR 上の「最後のディスク上のセントラルディレクトリヘッダ数」のフィールドに、実際のセントラルディレクトリヘッダ数とは異なる値が書き込まれることがある。
        /// </item>
        /// </list>
        /// </remarks>
        StrictlyCheckNumberOfCentralDirectoryHeadersOnLastDisk = 1 << 5,

        /// <summary>
        /// 厳密な検証を行うためのフラグの組み合わせです。
        /// </summary>
        Strict = DisallowUnknownPayloadExists | DisallowPointerToEndOfDisk | StrictlyCheckExtraFieldValues | DisallowNotWellKnownExtraField | StrictlyCheckNumberOfCentralDirectoryHeadersOnLastDisk,

        /// <summary>
        /// 大雑把な検証を行うためのフラグの組み合わせです。
        /// </summary>
        Lazy = AllowNullPayloadAfterEOCDR,
    }
}
