using System;

namespace Palmtree
{
    /// <summary>
    /// ログの出力先の実装のインターフェースです。
    /// </summary>
    public interface IValidationLoggerSource
    {
        /// <summary>
        /// ログの出力先の状態を取得または設定します。
        /// </summary>
        /// <value>
        /// ログの出力先の状態を示す <see cref="Object"/> です。
        /// </value>
        /// <remarks>
        /// <para>
        /// このプロパティの値が何を示すかは実装依存です。
        /// 例えば、出力先のコンソールの前景色や背景色という意味を持たせてもいいでしょう。
        /// </para>
        /// <para>
        /// 特に状態を持たないのであれば <see langword="null"/> にすることもできます。
        /// </para>
        /// </remarks>
        Object? State { get; set; }

        /// <summary>
        /// ログのカテゴリにふさわしいログの出力先の状態を取得します。
        /// </summary>
        /// <param name="category">
        /// ログのカテゴリ名を示す <see cref="LogCategory"/> です。
        /// <see cref="LogCategory.None"/> は、カテゴリが指定されていないことを示します。
        /// </param>
        /// <returns>
        /// ログの出力先の状態を示す <see cref="Object"/> です。
        /// </returns>
        /// <remarks>
        /// <para>
        /// <paramref name="category"/> で指定したカテゴリに適した、ログの出力先の状態を取得します。
        /// </para>
        /// <para>
        /// このメソッドが返す値が何を示すかは実装依存です。
        /// 例えば、出力先のコンソールの前景色や背景色という意味を持たせてもいいでしょう。
        /// </para>
        /// <para>
        /// 特に状態を持たないのであれば <see langword="null"/> にすることもできます。
        /// </para>
        /// <para>
        /// このメソッドが返す値の <see cref="State"/> プロパティへの設定は安全でなければならず、例外が発生してはならないことに注意してください。
        /// </para>
        /// </remarks>
        Object? GetStateFromCategory(LogCategory category = LogCategory.None);

        /// <summary>
        /// ログにアプリケーション名を出力します。
        /// </summary>
        /// <param name="applicationName">
        /// 出力するアプリケーション名を示す <see cref="String"/> です。
        /// </param>
        void WriteApplicationName(String applicationName);

        /// <summary>
        /// ログにカテゴリ名を出力します。
        /// </summary>
        /// <param name="category">
        /// 出力するカテゴリ名を示す <see cref="LogCategory"/> です。
        /// </param>
        void WriteLogCategory(LogCategory category);

        /// <summary>
        /// 指定した数の空白文字をログに出力します。
        /// </summary>
        /// <param name="n">
        /// 出力する空白文字の数を示す <see cref="Int32"/> です。
        /// </param>
        void WriteSpaces(Int32 n);

        /// <summary>
        /// ログにメッセージを出力します。
        /// </summary>
        /// <param name="message">
        /// 出力するメッセージを示す <see cref="String"/> です。
        /// </param>
        void WriteLogMessage(String message);

        /// <summary>
        /// ログのアプリケーション名・カテゴリ名・メッセージを区切る文字列を出力します。
        /// </summary>
        /// <remarks>
        /// 通常はコロン (':') を出力します。
        /// </remarks>
        void WriteLogSeparator();

        /// <summary>
        /// ログの行を改行します。
        /// </summary>
        void WriteLine();
    }
}
