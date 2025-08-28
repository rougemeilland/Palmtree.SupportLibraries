using System;

namespace Palmtree
{
    /// <summary>
    /// バイトストリームから各種の値を読み込むクラスです。
    /// </summary>
    public partial class ByteArrayReader
    {
        private const Int32 _SIZE_OF_INT128 = 16;
        private const Int32 _SIZE_OF_UINT128 = 16;
        private const Int32 _SIZE_OF_HALF = 2;
        private readonly ReadOnlyMemory<Byte> _sourceArray;
        private Int32 _currentIndex;

        static ByteArrayReader()
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
        /// <param name="sourceArray">
        /// 読み込み元のバイト列です。
        /// </param>
        public ByteArrayReader(ReadOnlyMemory<Byte> sourceArray)
        {
            _sourceArray = sourceArray;
            _currentIndex = 0;
        }

        /// <summary>
        /// ストリームが空であるかどうかの値を取得します。
        /// </summary>
        /// <value>
        /// 空である場合は true、そうではない場合は false です。
        /// </value>
        public Boolean IsEmpty => _currentIndex >= _sourceArray.Length;

        #region ReadByte

        /// <summary>
        /// 1 バイトだけストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Byte ReadByte()
        {
            const Int32 valueLength = sizeof(Byte);
            if (checked(_currentIndex + valueLength) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Span[_currentIndex];
            checked
            {
                _currentIndex += valueLength;
            }

            return value;
        }

        #endregion

        #region ReadBytes

        /// <summary>
        /// 指定された長さのバイト列をストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public ReadOnlyMemory<Byte> ReadBytes(Int32 length)
        {
            if (_currentIndex + length > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = new Byte[length];
            _sourceArray.Slice(_currentIndex, value.Length).CopyTo(value);
            _currentIndex += length;
            return value;
        }

        #endregion

        #region ReadBytes

        /// <summary>
        /// バイト列をストリームから読み込みます。
        /// </summary>
        /// <param name="buffer">
        /// バイト列を読み込むためのバッファーです。
        /// </param>
        /// <remarks>
        /// このメソッドは <paramref name="buffer"/> の長さだけのデータをストリームから読み込みます。
        /// </remarks>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public void ReadBytes(Span<Byte> buffer)
        {
            if (_currentIndex + buffer.Length > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            _sourceArray.Slice(_currentIndex, buffer.Length).Span.CopyTo(buffer);
            _currentIndex += buffer.Length;
        }

        #endregion

        #region ReadAllBytes

        /// <summary>
        /// ストリームに残されたすべてのデータを読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        public ReadOnlyMemory<Byte> ReadAllBytes()
        {
            var value = new Byte[_sourceArray.Length - _currentIndex];
            _sourceArray[_currentIndex..].CopyTo(value);
            _currentIndex = _sourceArray.Length;
            return value;
        }

        #endregion
    }
}
