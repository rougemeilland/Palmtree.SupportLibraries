using System;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public partial class ByteArrayReader
    {
        #region ReadInt16LE

        /// <summary>
        /// <see cref="Int16"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Int16 ReadInt16LE()
        {
            if (checked(_currentIndex + sizeof(Int16)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Int16)).ToInt16LE();
            checked
            {
                _currentIndex += sizeof(Int16);
            }

            return value;
        }

        #endregion

        #region ReadUInt16LE

        /// <summary>
        /// <see cref="UInt16"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public UInt16 ReadUInt16LE()
        {
            if (checked(_currentIndex + sizeof(UInt16)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(UInt16)).ToUInt16LE();
            checked
            {
                _currentIndex += sizeof(UInt16);
            }

            return value;
        }

        #endregion

        #region ReadInt32LE

        /// <summary>
        /// <see cref="Int32"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Int32 ReadInt32LE()
        {
            if (checked(_currentIndex + sizeof(Int32)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Int32)).ToInt32LE();
            checked
            {
                _currentIndex += sizeof(Int32);
            }

            return value;
        }

        #endregion

        #region ReadUInt32LE

        /// <summary>
        /// <see cref="UInt32"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public UInt32 ReadUInt32LE()
        {
            if (checked(_currentIndex + sizeof(UInt32)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(UInt32)).ToUInt32LE();
            checked
            {
                _currentIndex += sizeof(UInt32);
            }

            return value;
        }

        #endregion

        #region ReadInt64LE

        /// <summary>
        /// <see cref="Int64"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Int64 ReadInt64LE()
        {
            if (checked(_currentIndex + sizeof(Int64)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Int64)).ToInt64LE();
            checked
            {
                _currentIndex += sizeof(Int64);
            }

            return value;
        }

        #endregion

        #region ReadUInt64LE

        /// <summary>
        /// <see cref="UInt64"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public UInt64 ReadUInt64LE()
        {
            if (checked(_currentIndex + sizeof(UInt64)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(UInt64)).ToUInt64LE();
            checked
            {
                _currentIndex += sizeof(UInt64);
            }

            return value;
        }

        #endregion

        #region ReadInt128LE

        /// <summary>
        /// <see cref="Int128"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Int128 ReadInt128LE()
        {
            if (checked(_currentIndex + _SIZE_OF_INT128) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, _SIZE_OF_INT128).ToInt128LE();
            checked
            {
                _currentIndex += _SIZE_OF_INT128;
            }

            return value;
        }

        #endregion

        #region ReadUInt128LE

        /// <summary>
        /// <see cref="UInt128"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public UInt128 ReadUInt128LE()
        {
            if (checked(_currentIndex + _SIZE_OF_UINT128) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, _SIZE_OF_UINT128).ToUInt128LE();
            checked
            {
                _currentIndex += _SIZE_OF_UINT128;
            }

            return value;
        }

        #endregion

        #region ReadHalfLE

        /// <summary>
        /// <see cref="Half"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Half ReadHalfLE()
        {
            if (checked(_currentIndex + _SIZE_OF_HALF) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, _SIZE_OF_HALF).ToHalfLE();
            checked
            {
                _currentIndex += _SIZE_OF_HALF;
            }

            return value;
        }

        #endregion

        #region ReadSingleLE

        /// <summary>
        /// <see cref="Single"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Single ReadSingleLE()
        {
            if (checked(_currentIndex + sizeof(Single)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Single)).ToSingleLE();
            checked
            {
                _currentIndex += sizeof(Single);
            }

            return value;
        }

        #endregion

        #region ReadDoubleLE

        /// <summary>
        /// <see cref="Double"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Double ReadDoubleLE()
        {
            if (checked(_currentIndex + sizeof(Double)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Double)).ToDoubleLE();
            checked
            {
                _currentIndex += sizeof(Double);
            }

            return value;
        }

        #endregion

        #region ReadDecimalLE

        /// <summary>
        /// <see cref="Decimal"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Decimal ReadDecimalLE()
        {
            if (checked(_currentIndex + sizeof(Decimal)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Decimal)).ToDecimalLE();
            checked
            {
                _currentIndex += sizeof(Decimal);
            }

            return value;
        }

        #endregion

        #region ReadInt16BE

        /// <summary>
        /// <see cref="Int16"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Int16 ReadInt16BE()
        {
            if (checked(_currentIndex + sizeof(Int16)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Int16)).ToInt16BE();
            checked
            {
                _currentIndex += sizeof(Int16);
            }

            return value;
        }

        #endregion

        #region ReadUInt16BE

        /// <summary>
        /// <see cref="UInt16"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public UInt16 ReadUInt16BE()
        {
            if (checked(_currentIndex + sizeof(UInt16)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(UInt16)).ToUInt16BE();
            checked
            {
                _currentIndex += sizeof(UInt16);
            }

            return value;
        }

        #endregion

        #region ReadInt32BE

        /// <summary>
        /// <see cref="Int32"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Int32 ReadInt32BE()
        {
            if (checked(_currentIndex + sizeof(Int32)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Int32)).ToInt32BE();
            checked
            {
                _currentIndex += sizeof(Int32);
            }

            return value;
        }

        #endregion

        #region ReadUInt32BE

        /// <summary>
        /// <see cref="UInt32"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public UInt32 ReadUInt32BE()
        {
            if (checked(_currentIndex + sizeof(UInt32)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(UInt32)).ToUInt32BE();
            checked
            {
                _currentIndex += sizeof(UInt32);
            }

            return value;
        }

        #endregion

        #region ReadInt64BE

        /// <summary>
        /// <see cref="Int64"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Int64 ReadInt64BE()
        {
            if (checked(_currentIndex + sizeof(Int64)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Int64)).ToInt64BE();
            checked
            {
                _currentIndex += sizeof(Int64);
            }

            return value;
        }

        #endregion

        #region ReadUInt64BE

        /// <summary>
        /// <see cref="UInt64"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public UInt64 ReadUInt64BE()
        {
            if (checked(_currentIndex + sizeof(UInt64)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(UInt64)).ToUInt64BE();
            checked
            {
                _currentIndex += sizeof(UInt64);
            }

            return value;
        }

        #endregion

        #region ReadInt128BE

        /// <summary>
        /// <see cref="Int128"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Int128 ReadInt128BE()
        {
            if (checked(_currentIndex + _SIZE_OF_INT128) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, _SIZE_OF_INT128).ToInt128BE();
            checked
            {
                _currentIndex += _SIZE_OF_INT128;
            }

            return value;
        }

        #endregion

        #region ReadUInt128BE

        /// <summary>
        /// <see cref="UInt128"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public UInt128 ReadUInt128BE()
        {
            if (checked(_currentIndex + _SIZE_OF_UINT128) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, _SIZE_OF_UINT128).ToUInt128BE();
            checked
            {
                _currentIndex += _SIZE_OF_UINT128;
            }

            return value;
        }

        #endregion

        #region ReadHalfBE

        /// <summary>
        /// <see cref="Half"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Half ReadHalfBE()
        {
            if (checked(_currentIndex + _SIZE_OF_HALF) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, _SIZE_OF_HALF).ToHalfBE();
            checked
            {
                _currentIndex += _SIZE_OF_HALF;
            }

            return value;
        }

        #endregion

        #region ReadSingleBE

        /// <summary>
        /// <see cref="Single"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Single ReadSingleBE()
        {
            if (checked(_currentIndex + sizeof(Single)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Single)).ToSingleBE();
            checked
            {
                _currentIndex += sizeof(Single);
            }

            return value;
        }

        #endregion

        #region ReadDoubleBE

        /// <summary>
        /// <see cref="Double"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Double ReadDoubleBE()
        {
            if (checked(_currentIndex + sizeof(Double)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Double)).ToDoubleBE();
            checked
            {
                _currentIndex += sizeof(Double);
            }

            return value;
        }

        #endregion

        #region ReadDecimalBE

        /// <summary>
        /// <see cref="Decimal"/> の値をリトルエンディアン形式でストリームから読み込みます。
        /// </summary>
        /// <returns>
        /// 読み込んだデータです。
        /// </returns>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// ストリームに残っているデータの長さが不足しています。
        /// </exception>
        public Decimal ReadDecimalBE()
        {
            if (checked(_currentIndex + sizeof(Decimal)) > _sourceArray.Length)
                throw new UnexpectedEndOfBufferException();
            var value = _sourceArray.Slice(_currentIndex, sizeof(Decimal)).ToDecimalBE();
            checked
            {
                _currentIndex += sizeof(Decimal);
            }

            return value;
        }

        #endregion
    }
}
