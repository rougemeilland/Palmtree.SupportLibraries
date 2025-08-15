using System;
using System.Runtime.CompilerServices;

namespace Palmtree
{
    public partial class ByteArrayBuilder
    {
        #region AppendInt16LE

        /// <summary>
        /// バッファに <see cref="Int16"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Int16"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendInt16LE(Int16 value)
        {
            if (checked(_currentIndex + sizeof(Int16)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Int16);
            }
        }

        #endregion

        #region AppendUInt16LE

        /// <summary>
        /// バッファに <see cref="UInt16"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="UInt16"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendUInt16LE(UInt16 value)
        {
            if (checked(_currentIndex + sizeof(UInt16)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(UInt16);
            }
        }

        #endregion

        #region AppendInt32LE

        /// <summary>
        /// バッファに <see cref="Int32"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Int32"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendInt32LE(Int32 value)
        {
            if (checked(_currentIndex + sizeof(Int32)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Int32);
            }
        }

        #endregion

        #region AppendUInt32LE

        /// <summary>
        /// バッファに <see cref="UInt32"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="UInt32"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendUInt32LE(UInt32 value)
        {
            if (checked(_currentIndex + sizeof(UInt32)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(UInt32);
            }
        }

        #endregion

        #region AppendInt64LE

        /// <summary>
        /// バッファに <see cref="Int64"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Int64"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendInt64LE(Int64 value)
        {
            if (checked(_currentIndex + sizeof(Int64)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Int64);
            }
        }

        #endregion

        #region AppendUInt64LE

        /// <summary>
        /// バッファに <see cref="UInt64"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="UInt64"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendUInt64LE(UInt64 value)
        {
            if (checked(_currentIndex + sizeof(UInt64)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(UInt64);
            }
        }

        #endregion

        #region AppendInt128LE

        /// <summary>
        /// バッファに <see cref="Int128"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Int128"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendInt128LE(Int128 value)
        {
            if (checked(_currentIndex + _SIZE_OF_INT128) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += _SIZE_OF_INT128;
            }
        }

        #endregion

        #region AppendUInt128LE

        /// <summary>
        /// バッファに <see cref="UInt128"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="UInt128"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendUInt128LE(UInt128 value)
        {
            if (checked(_currentIndex + _SIZE_OF_UINT128) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += _SIZE_OF_UINT128;
            }
        }

        #endregion

        #region AppendHalfLE

        /// <summary>
        /// バッファに <see cref="Half"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Half"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendHalfLE(Half value)
        {
            if (checked(_currentIndex + _SIZE_OF_HALF) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += _SIZE_OF_HALF;
            }
        }

        #endregion

        #region AppendSingleLE

        /// <summary>
        /// バッファに <see cref="Single"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Single"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendSingleLE(Single value)
        {
            if (checked(_currentIndex + sizeof(Single)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Single);
            }
        }

        #endregion

        #region AppendDoubleLE

        /// <summary>
        /// バッファに <see cref="Double"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Double"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendDoubleLE(Double value)
        {
            if (checked(_currentIndex + sizeof(Double)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Double);
            }
        }

        #endregion

        #region AppendDecimalLE

        /// <summary>
        /// バッファに <see cref="Decimal"/> 型の値をリトルエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Decimal"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendDecimalLE(Decimal value)
        {
            if (checked(_currentIndex + sizeof(Decimal)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueLE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Decimal);
            }
        }

        #endregion

        #region AppendInt16BE

        /// <summary>
        /// バッファに <see cref="Int16"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Int16"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendInt16BE(Int16 value)
        {
            if (checked(_currentIndex + sizeof(Int16)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Int16);
            }
        }

        #endregion

        #region AppendUInt16BE

        /// <summary>
        /// バッファに <see cref="UInt16"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="UInt16"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendUInt16BE(UInt16 value)
        {
            if (checked(_currentIndex + sizeof(UInt16)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(UInt16);
            }
        }

        #endregion

        #region AppendInt32BE

        /// <summary>
        /// バッファに <see cref="Int32"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Int32"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendInt32BE(Int32 value)
        {
            if (checked(_currentIndex + sizeof(Int32)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Int32);
            }
        }

        #endregion

        #region AppendUInt32BE

        /// <summary>
        /// バッファに <see cref="UInt32"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="UInt32"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendUInt32BE(UInt32 value)
        {
            if (checked(_currentIndex + sizeof(UInt32)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(UInt32);
            }
        }

        #endregion

        #region AppendInt64BE

        /// <summary>
        /// バッファに <see cref="Int64"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Int64"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendInt64BE(Int64 value)
        {
            if (checked(_currentIndex + sizeof(Int64)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Int64);
            }
        }

        #endregion

        #region AppendUInt64BE

        /// <summary>
        /// バッファに <see cref="UInt64"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="UInt64"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendUInt64BE(UInt64 value)
        {
            if (checked(_currentIndex + sizeof(UInt64)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(UInt64);
            }
        }

        #endregion

        #region AppendInt128BE

        /// <summary>
        /// バッファに <see cref="Int128"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Int128"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendInt128BE(Int128 value)
        {
            if (checked(_currentIndex + _SIZE_OF_INT128) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += _SIZE_OF_INT128;
            }
        }

        #endregion

        #region AppendUInt128BE

        /// <summary>
        /// バッファに <see cref="UInt128"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="UInt128"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendUInt128BE(UInt128 value)
        {
            if (checked(_currentIndex + _SIZE_OF_UINT128) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += _SIZE_OF_UINT128;
            }
        }

        #endregion

        #region AppendHalfBE

        /// <summary>
        /// バッファに <see cref="Half"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Half"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendHalfBE(Half value)
        {
            if (checked(_currentIndex + _SIZE_OF_HALF) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += _SIZE_OF_HALF;
            }
        }

        #endregion

        #region AppendSingleBE

        /// <summary>
        /// バッファに <see cref="Single"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Single"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendSingleBE(Single value)
        {
            if (checked(_currentIndex + sizeof(Single)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Single);
            }
        }

        #endregion

        #region AppendDoubleBE

        /// <summary>
        /// バッファに <see cref="Double"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Double"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendDoubleBE(Double value)
        {
            if (checked(_currentIndex + sizeof(Double)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Double);
            }
        }

        #endregion

        #region AppendDecimalBE

        /// <summary>
        /// バッファに <see cref="Decimal"/> 型の値をビッグエンディアン形式で追加します。
        /// </summary>
        /// <param name="value">
        /// 追加する値を示す <see cref="Decimal"/> です。
        /// </param>
        /// <exception cref="UnexpectedEndOfBufferException">
        /// バッファの空き領域が不足しています。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AppendDecimalBE(Decimal value)
        {
            if (checked(_currentIndex + sizeof(Decimal)) > _destinationArray.Length)
                throw new UnexpectedEndOfBufferException();
            _destinationArray.SetValueBE(value, _currentIndex);
            checked
            {
                _currentIndex += sizeof(Decimal);
            }
        }

        #endregion
    }
}
