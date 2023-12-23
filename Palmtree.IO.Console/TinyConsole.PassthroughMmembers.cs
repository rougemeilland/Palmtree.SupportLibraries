using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Palmtree.IO.Console
{
    partial class TinyConsole
    {
        /// <summary>
        /// 標準入力ストリームを取得します。
        /// </summary>
        public static TextReader In
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.In;
        }

        /// <summary>
        /// 標準出力ストリームを取得します。
        /// </summary>
        public static TextWriter Out
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.Out;
        }

        /// <summary>
        /// 標準エラー出力ストリームを取得します。
        /// </summary>
        public static TextWriter Error
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.Error;
        }

        /// <summary>
        /// 標準入力ストリームがリダイレクトされているかどうかを示す値を取得します。
        /// </summary>
        public static Boolean IsInputRedirected
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.IsInputRedirected;
        }

        /// <summary>
        /// 標準出力ストリームがリダイレクトされているかどうかを示す値を取得します。
        /// </summary>
        public static Boolean IsOutputRedirected
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.IsOutputRedirected;
        }

        /// <summary>
        /// 標準エラー出力ストリームがリダイレクトされているかどうかを示す値を取得します。
        /// </summary>
        public static Boolean IsErrorRedirected
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.IsErrorRedirected;
        }

        /// <summary>
        /// コンソールが入力内容の読み取り時に使用するエンコーディングを取得または設定します。
        /// </summary>
        public static Encoding InputEncoding
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.InputEncoding;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => System.Console.InputEncoding = value;
        }

        /// <summary>
        /// コンソールが出力内容の書き込み時に使用するエンコーディングを取得または設定します。
        /// </summary>
        public static Encoding OutputEncoding
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.OutputEncoding;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => System.Console.OutputEncoding = value;
        }

        /// <summary>
        /// 標準入力ストリームを取得します。
        /// </summary>
        /// <returns>
        /// 標準入力ストリームです。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISequentialInputByteStream OpenStandardInput() => System.Console.OpenStandardInput().AsInputByteStream();

        /// <summary>
        /// 標準出力ストリームを取得します。
        /// </summary>
        /// <returns>
        /// 標準出力ストリームです。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISequentialOutputByteStream OpenStandardOutput() => System.Console.OpenStandardOutput().AsOutputByteStream();

        /// <summary>
        /// 標準エラー出力ストリームを取得します。
        /// </summary>
        /// <returns>
        /// 標準エラー出力ストリームです。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ISequentialOutputByteStream OpenStandardError() => System.Console.OpenStandardError().AsOutputByteStream();

        /// <summary>
        /// 指定した <see cref="TextReader"/> を <see cref="In"/> プロパティに設定します。
        /// </summary>
        /// <param name="newIn">
        /// 新しい標準入力であるストリームです。
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIn(TextReader newIn) => System.Console.SetIn(newIn);

        /// <summary>
        /// 指定した <see cref="TextWriter"/> を <see cref="Out"/> プロパティに設定します。
        /// </summary>
        /// <param name="newOut">
        /// 新しい標準出力であるストリームです。
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetOut(TextWriter newOut) => System.Console.SetOut(newOut);

        /// <summary>
        /// 指定した <see cref="TextWriter"/> を <see cref="Error"/> プロパティに設定します。
        /// </summary>
        /// <param name="newError">
        /// 新しい標準エラー出力であるストリームです。
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetError(TextWriter newError) => System.Console.SetError(newError);

        /// <summary>
        /// カーソルの位置を取得します。
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>
        /// <term>Left</term>
        /// <description>コンソールウィンドウの左端からの桁数です。</description>
        /// </item>
        /// <item>
        /// <term>Top</term>
        /// <description>コンソールウィンドウの上端からの行数です。</description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>標準入力がリダイレクトされています。</item>
        /// <item>ターミナルがカーソル位置の取得をサポートしていません。</item>
        /// </list>
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Int32 Left, Int32 Top) GetCursorPosition()
        {
            var (Left, Top) = System.Console.GetCursorPosition();
            return (Left - System.Console.WindowLeft, Top - System.Console.WindowTop);
        }

        /// <summary>
        /// カーソルの位置を設定します。
        /// </summary>
        /// <param name="left">コンソールウィンドウの左端からの桁数です。</param>
        /// <param name="top">コンソールウィンドウの上端からの桁数です。</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCursorPosition(Int32 left, Int32 top) => System.Console.SetCursorPosition(left + System.Console.WindowLeft, top + System.Console.WindowTop);

        /// <summary>
        /// コンソールウィンドウの左端からカーソル位置までの桁数を取得または設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>標準入力がリダイレクトされています。</item>
        /// <item>ターミナルがカーソル位置の取得/設定をサポートしていません。</item>
        /// </list>
        /// </exception>
        public static Int32 CursorLeft
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.CursorLeft - System.Console.WindowLeft;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => System.Console.CursorLeft = value + System.Console.CursorLeft;
        }

        /// <summary>
        /// コンソールウィンドウの上端からカーソル位置までの行数を取得または設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>標準入力がリダイレクトされています。</item>
        /// <item>ターミナルがカーソル位置の取得/設定をサポートしていません。</item>
        /// </list>
        /// </exception>
        public static Int32 CursorTop
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.CursorTop - System.Console.WindowTop;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => System.Console.CursorTop = value + System.Console.WindowTop;
        }

        /// <summary>
        /// キーが押されたかどうか、つまり、押されたキーが入力ストリームに存在するかどうかを示す値を取得します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準入力がリダイレクトされています。</item>
        /// </list>
        /// </exception>
        public static Boolean KeyAvailable
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.KeyAvailable;
        }

        /// <summary>
        /// ユーザーによって押された次の文字キーまたはファンクション キーを取得します。
        /// 押されたキーは、コンソール ウィンドウに表示されます。
        /// </summary>
        /// <returns>
        /// 押されたコンソール キーに対応する <see cref="ConsoleKey"/> 定数と (もし存在する場合は) Unicode 文字を記述する <see cref="ConsoleKeyInfo"/> オブジェクトです。
        /// <see cref="ConsoleKeyInfo"/> オブジェクトは、1 つ以上の Shift、Alt、Ctrl の各修飾子キーがコンソール キーと同時に押されたかどうかを <see cref="ConsoleModifiers"/> 値のビットごとの組み合わせで記述します。
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準入力がリダイレクトされています。</item>
        /// </list>
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConsoleKeyInfo ReadKey() => System.Console.ReadKey();

        /// <summary>
        /// ユーザーによって押された次の文字キーまたはファンクション キーを取得します。
        /// 押されたキーは、オプションでコンソール ウィンドウに表示されます。
        /// </summary>
        /// <param name="intercept">
        /// 押されたキーをコンソール ウィンドウに表示するかどうかを決定します。
        /// 押されたキーを表示しない場合は true。それ以外の場合は false です。
        /// </param>
        /// <returns>
        /// 押されたコンソール キーに対応する <see cref="ConsoleKey"/> 定数と (もし存在する場合は) Unicode 文字を記述する <see cref="ConsoleKeyInfo"/> オブジェクトです。
        /// <see cref="ConsoleKeyInfo"/> オブジェクトは、1 つ以上の Shift、Alt、Ctrl の各修飾子キーがコンソール キーと同時に押されたかどうかを <see cref="ConsoleModifiers"/> 値のビットごとの組み合わせで記述します。
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準入力がリダイレクトされています。</item>
        /// </list>
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConsoleKeyInfo ReadKey(Boolean intercept) => System.Console.ReadKey(intercept);

        /// <summary>
        /// Control 修飾キー (Ctrl) と C コンソール キー (c) または Break キーが同時に押された場合 (Ctrl + C または Ctrl + Break) に発生するイベントです。
        /// </summary>
        public static event ConsoleCancelEventHandler? CancelKeyPress
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            add => System.Console.CancelKeyPress += value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            remove => System.Console.CancelKeyPress -= value;
        }

        /// <summary>
        /// Control修飾子キーとCコンソール キー (Ctrl+C)の組み合わせが、通常の入力として扱われるか、オペレーティングシステムによって処理される割り込みとして扱われるかを示す値を取得または設定します。
        /// </summary>
        public static Boolean TreatControlCAsInput
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.TreatControlCAsInput;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => System.Console.TreatControlCAsInput = value;
        }

        /// <summary>
        /// 標準入力ストリームから次の文字を読み取ります。
        /// </summary>
        /// <returns>
        /// 入力ストリームから文字が読み込めた場合は、その文字を表す<see cref="Int32"/>値です。
        /// 次の文字がない場合は -1 です。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Read() => System.Console.Read();

        /// <summary>
        /// 標準入力ストリームから次の 1 行分の文字を読み取ります。
        /// </summary>
        /// <returns>
        /// 入力ストリームから次の行が読み込めた場合は、その行を表す<see cref="String"/>値です。
        /// 次の行がない場合は null です。
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? ReadLine() => System.Console.ReadLine();
    }
}
