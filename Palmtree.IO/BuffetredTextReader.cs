﻿using System;
using System.IO;

namespace Palmtree.IO
{
    /// <summary>
    /// <see cref="TextReader"/> から文字単位の読み込みと先読みをするラッパークラスです。
    /// </summary>
    public class BuffetredTextReader
        : IDisposable, IPrefetchableTextReader
    {
        private readonly TextReader _rawReader;
        private readonly Char[] _cacheBuffer;
        private readonly Boolean _leaveOpen;
        private Boolean _isDisposed;
        private Boolean _endOfStream;
        private Int32 _cacheLength;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="reader">
        /// 基本となる <see cref="TextReader"/> オブジェクトです。
        /// </param>
        /// <param name="cacheSize">
        /// 先読みが可能な最大文字数です。
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="cacheSize"/> には1以上の値を与えなければなりません。
        /// </exception>
        public BuffetredTextReader(TextReader reader, Int32 cacheSize, Boolean leaveOpen = false)
        {
            if (cacheSize < 1)
                throw new ArgumentException($"Invalid {nameof(cacheSize)} value.", nameof(cacheSize));

            _rawReader = reader;
            _cacheBuffer = new Char[cacheSize];
            _leaveOpen = leaveOpen;
            _isDisposed = false;
            _endOfStream = false;
            _cacheLength = 0;

        }

        /// <summary>
        /// ストリームから1文字読み込みます。
        /// </summary>
        /// <returns>
        /// nullである場合は、ストリームの終端に達したことを意味します。
        /// nullではない場合、それはストリームから読み込んだ文字です。
        /// </returns>
        public Char? Read()
        {
            if (_cacheLength > 0)
            {
                var c = _cacheBuffer[0];
                if (_cacheLength > 1)
                    Array.Copy(_cacheBuffer, 1, _cacheBuffer, 0, _cacheLength - 1);
                --_cacheLength;
                return c;
            }
            else if (_endOfStream)
            {
                return null;
            }
            else
            {
                var c = _rawReader.Read();
                if (c < 0)
                {
                    _endOfStream = true;
                    return null;
                }

                return (Char)c;
            }
        }

        /// <summary>
        /// ストリームの終端に達している場合は true、そうではない場合は false です。
        /// </summary>
        public Boolean IsEndOfReader
        {
            get
            {
                FillCache();
                return _cacheLength <= 0 && _endOfStream;
            }
        }

        /// <summary>
        /// ストリームのまだ読み込んでいない部分の先頭が指定した文字と一致しているかどうかを調べます。
        /// </summary>
        /// <param name="c">
        /// 比較する文字です。
        /// </param>
        /// <returns>
        /// ストリームのまだ読み込んでいない部分の先頭が <paramref name="c"/> と一致していれば true、そうではない場合は false です。
        /// </returns>
        public Boolean StartsWith(Char c)
        {
            FillCache();
            return _cacheLength > 0 && _cacheBuffer[0] == c;
        }

        /// <summary>
        /// ストリームのまだ読み込んでいない部分の先頭文字を指定したデリゲートによって比較します。
        /// </summary>
        /// <param name="predicate">
        /// 文字を比較するためのデリゲートです。条件が一致していれば true、そうではない場合は false を返します。
        /// </param>
        /// <returns>
        /// ストリームのまだ読み込んでいない部分の先頭文字が <paramref name="predicate"/> によって肯定されれば true、そうではない場合は false です。
        /// </returns>
        public Boolean StartsWith(Func<Char?, Boolean> predicate)
        {
            FillCache();
            return _cacheLength > 0 && predicate(_cacheBuffer[0]);
        }

        /// <summary>
        /// ストリームのまだ読み込んでいない部分の先頭が指定した文字列から始まっているかどうかを調べます。
        /// </summary>
        /// <param name="s">
        /// 比較する文字列です。
        /// </param>
        /// <returns>
        /// ストリームのまだ読み込んでいない部分の先頭が <paramref name="s"/> から始まっていれば true、そうではない場合は false です。
        /// </returns>
        public Boolean StartsWith(String s)
        {
            if (s.Length > _cacheBuffer.Length)
                throw new ArgumentException($"The string length of parameter \"{nameof(s)}\" must be less than or equal to {_cacheBuffer.Length}.", nameof(s));
            FillCache();
            if (_cacheLength < s.Length)
                return false;
            for (var index = 0; index < s.Length; ++index)
            {
                if (_cacheBuffer[index] != s[index])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// このオブジェクトに関連付けられたリソースを解放します
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// このオブジェクトに関連付けられたリソースを解放します
        /// </summary>
        /// <param name="disposing"><see cref="Dispose()"/> から呼び出されたかどうかの <see cref="Boolean"/> 値です。 </param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (!_leaveOpen)
                        _rawReader.Dispose();
                }

                _isDisposed = true;
            }
        }

        private void FillCache()
        {
            while (_cacheLength < _cacheBuffer.Length)
            {
                if (_endOfStream)
                    break;

                // インタラクティブな入力ストリームなどにおいて、次のデータがまだ読める状態にない場合は -1 を返す。
                if (_rawReader.Peek() < 0)
                    break;

                var c = _rawReader.Read();
                if (c < 0)
                {
                    _endOfStream = true;
                    break;
                }

                _cacheBuffer[_cacheLength] = (Char)c;
                ++_cacheLength;
            }
        }
    }
}
