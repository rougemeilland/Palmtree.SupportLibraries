using System;

namespace Palmtree
{
    /// <summary>
    /// 各種の値からバイト列を構築するためのバッファのクラスです。
    /// </summary>
    public partial class ByteArrayBuilder
    {
        private const Int32 _SIZE_OF_INT128 = 16;
        private const Int32 _SIZE_OF_UINT128 = 16;
        private const Int32 _SIZE_OF_HALF = 2;
        private readonly Byte[] _destinationArray;
        private Int32 _currentIndex;

        static ByteArrayBuilder()
        {
#if DEBUG
            unsafe
            {
                Validation.Assert(_SIZE_OF_INT128 == sizeof(Int128));
                Validation.Assert(_SIZE_OF_UINT128 == sizeof(UInt128));
                Validation.Assert(_SIZE_OF_HALF == sizeof(Half));
            }
#endif
        }

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="maximumBufferSize">
        /// バッファの最大サイズです。
        /// </param>
        public ByteArrayBuilder(Int32 maximumBufferSize)
        {
            _destinationArray = new Byte[maximumBufferSize];
            _currentIndex = 0;
        }

        #region AppendByte

        /// <summary>
        /// バッファに <see cref="Byte"/> 型の値を追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        public void AppendByte(Byte value)
        {
            if (checked(_currentIndex + sizeof(Byte)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray[_currentIndex] = value;
            checked
            {
                _currentIndex += sizeof(Byte);
            }
        }

        #endregion

        #region AppendBytes

        /// <summary>
        /// バッファにバイト列を追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        public void AppendBytes(ReadOnlySpan<Byte> value)
        {
            if (checked(_currentIndex + value.Length) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            value.CopyTo(_destinationArray.Slice(_currentIndex).Span);
            checked
            {
                _currentIndex += value.Length;
            }
        }

        #endregion

        #region ToByteArray

        /// <summary>
        /// バッファの内容をバイト列として取得します。
        /// </summary>
        /// <returns>
        /// バッファの内容を示すバイト列です。
        /// </returns>
        public ReadOnlyMemory<Byte> ToByteArray()
        {
            var buffer = new Byte[_currentIndex];
            _destinationArray.AsReadOnlySpan(0, _currentIndex).CopyTo(buffer);
            return buffer;
        }

        #endregion
    }
}
