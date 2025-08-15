using System;

namespace Palmtree
{
    public interface IValidationLogger
    {
        /// <summary>
        /// ログのインデントを1段階だけ増やします。
        /// </summary>
        void Indent();

        /// <summary>
        /// ログのインデントを1段階だけ減らします。
        /// </summary>
        void Unindent();

        /// <summary>
        /// ログに空行を出力します。
        /// </summary>
        void WriteLog();

        /// <summary>
        /// ログにメッセージおよび改行を出力します。
        /// また、オプションで先頭にインデントを出力します。
        /// </summary>
        /// <param name="message">
        /// 出力するメッセージを示す <see cref="String"/> です。
        /// </param>
        /// <param name="writeIndent">
        /// インデントを出力するかどうかを示す <see cref="Boolean"/> です。
        /// <see langword="true"/> である場合は、メッセージの前にインデントを出力します。
        /// <see langword="false"/> である場合は、メッセージの前にインデントを出力しません。
        /// </param>
        void WriteLog(String message, Boolean writeIndent = false);

        /// <summary>
        /// ログに既定のアプリケーション名およびカテゴリ名、インデント、メッセージ、改行を出力します。
        /// </summary>
        /// <param name="category">
        /// 出力するカテゴリ名を示す <see cref="LogCategory"/> です。
        /// </param>
        /// <param name="message">
        /// 出力するメッセージを示す <see cref="String"/> です。
        /// </param>
        void WriteLog(LogCategory category, String message);

        /// <summary>
        /// ログにアプリケーション名およびカテゴリ名、インデント、メッセージ、改行を出力します。
        /// </summary>
        /// <param name="applicationName">
        /// 出力するアプリケーション名を示す <see cref="String"/> です。
        /// </param>
        /// <param name="category">
        /// 出力するカテゴリ名を示す <see cref="LogCategory"/> です。
        /// </param>
        /// <param name="message">
        /// 出力するメッセージを示す <see cref="String"/> です。
        /// </param>
        void WriteLog(String applicationName, LogCategory category, String message);
    }
}
