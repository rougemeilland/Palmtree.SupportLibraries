using System;

namespace Palmtree
{
    public interface IValidationLogger
    {
        /// <summary>
        /// インデントを1段階だけ増やします。
        /// </summary>
        void Indent();

        /// <summary>
        /// インデントを1段階だけ減らします。
        /// </summary>
        void Unindent();

        /// <summary>
        /// 空行を出力します。
        /// </summary>
        void WriteLog();

        /// <summary>
        /// メッセージを出力します。
        /// </summary>
        /// <param name="prefix">ログ行の先頭に出力される文字列です。</param>
        /// <param name="message">ログに出力されるメッセージです。</param>
        /// <remarks>
        /// <para>
        /// <paramref name="prefix"/> が <see langword="null"/> である場合、<paramref name="message"/> を出力先に出力し、更に改行を出力します。このとき、インデントは行われません。
        /// </para>
        /// <para>
        /// <paramref name="prefix"/> が <see langword="null"/> ではない場合、まず <paramref name="prefix"/> を出力先に出力し、更に適切な幅のインデントと、<paramref name="message"/>、改行を出力します。
        /// </para>
        /// </remarks>
        void WriteLog(String? prefix, String message);
    }
}
