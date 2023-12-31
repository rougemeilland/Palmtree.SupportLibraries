﻿using System;
using System.Text;

namespace Palmtree.IO.Compression.Archive.Zip.ExtraFields
{
    /// <summary>
    /// エントリのコードページを保持する拡張フィールドのクラスです。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>この拡張フィールドは well known ではないことに注意してください。</item>
    /// <item>この拡張フィールドの存在については、<seealso href="https://gnqg.hatenablog.com/entry/2016/09/11/155033">出典</seealso> も参照してください。</item>
    /// </list>
    /// </remarks>
    public class CodePageExtraField
        : ExtraField
    {
        private Int32? _codePage;

        static CodePageExtraField()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// デフォルトコンストラクタです。
        /// </summary>
        public CodePageExtraField()
            : base(ExtraFieldId)
        {
            _codePage = null;
        }

        /// <summary>
        /// 拡張フィールドの ID です。
        /// </summary>
        public const UInt16 ExtraFieldId = 0xe57a;

        /// <inheritdoc/>
        public override ReadOnlyMemory<Byte>? GetData(ZipEntryHeaderType headerType, IExtraFieldEncodingParameter parameter)
        {
            if (_codePage is null)
                return null;
            var builder = new ByteArrayBuilder(sizeof(Int32));
            builder.AppendInt32LE(_codePage.Value);
            return builder.ToByteArray();
        }

        /// <inheritdoc/>
        public override void SetData(ZipEntryHeaderType headerType, ReadOnlyMemory<Byte> data, IExtraFieldDecodingParameter parameter)
        {
            if (parameter.Stringency.HasFlag(ValidationStringency.DisallowNotWellKnownExtraField))
                throw GetBadFormatException(headerType, data);

            _codePage = null;
            var reader = new ByteArrayReader(data);
            var succes = false;
            try
            {
                _codePage = reader.ReadInt32LE();
                if (!reader.IsEmpty)
                    throw GetBadFormatException(headerType, data);
                succes = true;

            }
            catch (UnexpectedEndOfBufferException ex)
            {
                throw GetBadFormatException(headerType, data, ex);
            }
            finally
            {
                if (!succes)
                {
                    _codePage = null;
                }
            }
        }

        /// <summary>
        /// エントリのコードページを示す整数を取得または設定します。
        /// </summary>
        public Int32 CodePage
        {
            get => _codePage ?? throw new InvalidOperationException();
            set => _codePage = value;
        }
    }
}
