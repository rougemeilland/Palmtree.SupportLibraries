#define USE_WIN32_API_TO_CONSOLE_OPERATION_FOR_WINDOWS
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Palmtree.Text;

namespace Palmtree.IO.Console
{
    /// <summary>
    /// コンソールの操作を行うクラスです。
    /// </summary>
    public static partial class TinyConsole
    {
        private enum CharacterSet
        {
            Primary,
            Alternative,
        }

        private const String _NATIVE_METHOD_DLL_NAME = "Palmtree.IO.Console.Native";
#if USE_WIN32_API_TO_CONSOLE_OPERATION_FOR_WINDOWS
        private const Boolean _useAnsiEscapeCodeEvenOnWindows = false;
#else
        private const Boolean _useAnsiEscapeCodeEvenOnWindows = true;
#endif
        private const Char _alternativeCharacterSetMapMinimumKey = '\u0020';
        private const Char _alternativeCharacterSetMapMaximumKey = '\u007e';

        private static readonly NativeDllNameResolver _dllNameResolver;
        private static readonly IntPtr _consoleOutputHandle;
        private static readonly Int32 _consoleOutputFileNo;
        private static readonly TextWriter _consoleTextWriter;
        private static readonly TextWriter? _escapeCodeWriter;
        private static readonly ConsoleColor _defaultBackgrouongColor = System.Console.BackgroundColor;
        private static readonly ConsoleColor _defaultForegrouongColor = System.Console.ForegroundColor;
        private static readonly TerminalInfo? ___thisTerminalInfo = TerminalInfo.GetTerminalInfo(true);
        private static readonly Char[] _alternativeCharacterSetMap;

        private static ConsoleColor _currentBackgrouongColor = System.Console.BackgroundColor;
        private static ConsoleColor _currentForegrouongColor = System.Console.ForegroundColor;
        private static CharacterSet _currentCharSet;

        private static TerminalInfo ThisTerminalInfo
            => ___thisTerminalInfo
                ?? throw new InvalidOperationException("Terminal information not found.");

        static TinyConsole()
        {
            _dllNameResolver = new NativeDllNameResolver();
            NativeLibrary.SetDllImportResolver(
                typeof(InterOpUnix).Assembly,
                (libraryName, assembly, searchPath) => _dllNameResolver.ResolveDllName(libraryName, assembly, searchPath));

            if (!System.Console.IsOutputRedirected)
            {
                _consoleOutputHandle =
                    OperatingSystem.IsWindows()
                    ? InterOpWindows.GetStdHandle(InterOpWindows.STD_OUTPUT_HANDLE)
                    : InterOpWindows.INVALID_HANDLE_VALUE;

                _consoleOutputFileNo =
                    OperatingSystem.IsWindows()
                    ? -1
                    : InterOpUnix.GetStandardFileNo(InterOpUnix.STANDARD_FILE_OUT);
                _consoleTextWriter =
                    new StreamWriter(
                        System.Console.OpenStandardOutput(),
                        System.Console.OutputEncoding.WithoutPreamble(),
                        256,
                        true)
                    { AutoFlush = true };
                _escapeCodeWriter = _consoleTextWriter;
            }
            else if (!System.Console.IsErrorRedirected)
            {
                _consoleOutputHandle =
                    OperatingSystem.IsWindows()
                    ? InterOpWindows.GetStdHandle(InterOpWindows.STD_ERROR_HANDLE)
                    : InterOpWindows.INVALID_HANDLE_VALUE;
                _consoleOutputFileNo =
                    OperatingSystem.IsWindows()
                    ? -1
                    : InterOpUnix.GetStandardFileNo(InterOpUnix.STANDARD_FILE_ERR);
                _consoleTextWriter =
                    new StreamWriter(
                        System.Console.OpenStandardError(),
                        System.Console.OutputEncoding.WithoutPreamble(),
                        256,
                        true)
                    { AutoFlush = true };
                _escapeCodeWriter = _consoleTextWriter;
            }
            else
            {
                _consoleOutputHandle = InterOpWindows.INVALID_HANDLE_VALUE;
                _consoleOutputFileNo = -1;
                _consoleTextWriter =
                        new StreamWriter(
                            System.Console.OpenStandardError(),
                            System.Console.OutputEncoding.WithoutPreamble(),
                            256,
                            true)
                        { AutoFlush = true };
                _escapeCodeWriter = null;
            }

            _alternativeCharacterSetMap = Array.Empty<Char>();
            _currentCharSet = CharacterSet.Primary;

            if (___thisTerminalInfo is not null)
            {
                var acs = ___thisTerminalInfo.AcsChars;
                if (acs is not null)
                {
                    _alternativeCharacterSetMap = new Char[_alternativeCharacterSetMapMaximumKey - _alternativeCharacterSetMapMinimumKey + 1];
                    Array.Fill(_alternativeCharacterSetMap, '\u0000');
                    for (var index = 0; index + 1 < acs.Length; index += 2)
                        _alternativeCharacterSetMap[acs[index] - _alternativeCharacterSetMapMinimumKey] = acs[index + 1];
                }
            }

            if (OperatingSystem.IsWindows() && _consoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
                // Windows プラットフォームであり、かつ
                // コンソール出力ハンドルが有効である (つまり標準出力と標準エラー出力のどちらかがリダイレクトされていない) 場合

                // コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグ (エスケープコードを解釈可能かどうか) を調べる

                if (!InterOpWindows.GetConsoleMode(_consoleOutputHandle, out var mode))
                    throw new Exception("Failed to get console mode.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                if ((mode & InterOpWindows.ENABLE_VIRTUAL_TERMINAL_PROCESSING) == 0)
                {
                    // コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグが立っていない場合

                    // コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグをセットする
                    if (!InterOpWindows.SetConsoleMode(_consoleOutputHandle, mode | InterOpWindows.ENABLE_VIRTUAL_TERMINAL_PROCESSING))
                        throw new Exception("Failed to set console mode.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                    // 再度、コンソールモードの ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグを調べる
                    if (!InterOpWindows.GetConsoleMode(_consoleOutputHandle, out mode))
                        throw new Exception("Failed to get console mode.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                    if ((mode & InterOpWindows.ENABLE_VIRTUAL_TERMINAL_PROCESSING) == 0)
                    {
                        // 一度コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグをセットしたにもかかわらず、ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグがセットされていない場合

                        // ターミナルがエスケープコードをサポートしていないとみなす
                        _escapeCodeWriter = null;
                    }
                }
            }

            if (!ImplementWithWin32Api && _escapeCodeWriter is not null && ___thisTerminalInfo is not null)
            {
                var exitAltCharsetMode = ___thisTerminalInfo.ExitAltCharsetMode;
                if (exitAltCharsetMode is not null)
                    _escapeCodeWriter.Write(exitAltCharsetMode);
            }
        }

        #region BackgroundColor / ForegroundColor / ResetColor

        /// <summary>
        /// コンソールの文字の前景色を取得/設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>ターミナルが文字の前景色の変更をサポートしていません。</item>
        /// </list>
        /// </exception>
        public static ConsoleColor BackgroundColor
        {
            get
            {
                if (ImplementWithWin32Api)
                {
                    if (_consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
                        throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to get console attributes.");

                    if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                        throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                    (_currentBackgrouongColor, _) = InterOpWindows.FromConsoleAttributeToConsoleColors(consoleInfo.wAttributes);
                    return _currentBackgrouongColor;
                }
                else
                {
                    if (_escapeCodeWriter is null)
                        throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to get console attributes.");

                    return _currentBackgrouongColor;
                }
            }

            set => SetBackgroundColorCore(value);
        }

        /// <summary>
        /// コンソールの文字の前景色を取得/設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>ターミナルが文字の前景色の変更をサポートしていません。</item>
        /// </list>
        /// </exception>
        public static ConsoleColor ForegroundColor
        {
            get
            {
                if (ImplementWithWin32Api)
                {
                    if (_consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
                        throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to get console attributes.");

                    if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                        throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                    (_, _currentForegrouongColor) = InterOpWindows.FromConsoleAttributeToConsoleColors(consoleInfo.wAttributes);
                    return _currentBackgrouongColor;
                }
                else
                {
                    if (_escapeCodeWriter is null)
                        throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to get console attributes.");

                    return _currentForegrouongColor;
                }
            }

            set => SetForegroundColorCore(value);
        }

        /// <summary>
        /// コンソールの文字の前景色と背景色を初期値に変更します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>ターミナルが文字の前景色・背景色の初期化をサポートしていません。</item>
        /// </list>
        /// </exception>
        public static void ResetColor()
        {
            if (ImplementWithWin32Api)
            {
                if (_consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
                    throw new InvalidOperationException("Both standard output and standard error output are redirected, so console attributes cannot be changed.");

                var consoleAtrribute = InterOpWindows.FromConsoleColorsToConsoleAttribute(_defaultBackgrouongColor, _defaultForegrouongColor);
                if (!InterOpWindows.SetConsoleTextAttribute(_consoleOutputHandle, consoleAtrribute))
                    throw new InvalidOperationException("Failed to set console text attribute.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                _currentBackgrouongColor = _defaultBackgrouongColor;
                _currentForegrouongColor = _defaultForegrouongColor;
            }
            else
            {
                var resetColorEscapeCode = ThisTerminalInfo.ResetColor;
                if (resetColorEscapeCode is not null)
                {
                    WriteAnsiEscapeCodeToConsole(
                        resetColorEscapeCode,
                        () => throw new InvalidOperationException("Both standard output and standard error output are redirected, so console attributes cannot be changed."));
                }
                else
                {
                    SetBackgroundColorCore(_defaultBackgrouongColor);
                    SetForegroundColorCore(_defaultForegrouongColor);
                }
            }
        }

        #endregion

        #region WindowWidth

        /// <summary>
        /// コンソールウィンドウの桁数を取得します。
        /// </summary>
        public static Int32 WindowWidth => GetWindowSizeCore().windowWidth;

        #endregion

        #region WindowHeight

        /// <summary>
        /// コンソールウィンドウの行数を取得します。
        /// </summary>
        public static Int32 WindowHeight
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetWindowSizeCore().windowHeight;
        }

        #endregion

        #region Title

        /// <summary>
        /// ウィンドウタイトルを設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>ターミナルがウィンドウのタイトルの変更をサポートしていません。</item>
        /// </list>
        /// </exception>
        public static String Title
        {
            set
            {
                if (ImplementWithWin32Api)
                {
                    System.Console.Title = value;
                }
                else
                {
                    WriteAnsiEscapeCodeToConsole(
                        ThisTerminalInfo.SetTitle(value)
                        ?? throw new InvalidOperationException("This terminal does not define the capability to change the window title."),
                        () => throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to set the title of the cursor."));
                }
            }
        }

        #endregion

        #region Beep

        /// <summary>
        /// コンソールから BEEP 音を鳴らします。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>ターミナルがBEEP音をサポートしていません。</item>
        /// </list>
        /// </exception>
        public static void Beep()
        {
            if (ImplementWithWin32Api)
            {
                System.Console.Beep();
            }
            else
            {
                WriteAnsiEscapeCodeToConsole(
                    ThisTerminalInfo.Bell ?? throw new InvalidOperationException("This terminal does not define the \"bell\" capability."),
                    () => throw new InvalidOperationException("Since both standard output and standard error output are redirected, it cannot beep."));
            }
        }

        #endregion

        #region Clear

        /// <summary>
        /// コンソールバッファを消去します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>ターミナルがコンソールバッファの消去をサポートしていません。</item>
        /// </list>
        /// </exception>

        public static void Clear()
        {
            if (ImplementWithWin32Api)
            {
                if (_consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
                    throw new InvalidOperationException("Since both standard output and standard error output are redirected, the console screen cannot be cleared.");

                if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                if (!InterOpWindows.SetConsoleCursorPosition(_consoleOutputHandle, new InterOpWindows.COORD { X = 0, Y = 0 }))
                    throw new InvalidOperationException("Failed to set cursor position.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                ClearScreenCore(0, 0, consoleInfo.dwSize.X * consoleInfo.dwSize.Y, consoleInfo.wAttributes);

                // Windows ターミナルなどのターミナルでは Win32 API のみではコンソールバッファが消去されないため、エスケープコードも併用する。
                var eraseScrollBufferEscapeSequence = ThisTerminalInfo.EraseScrollBuffer;
                if (eraseScrollBufferEscapeSequence is not null)
                {
                    WriteAnsiEscapeCodeToConsole(
                        eraseScrollBufferEscapeSequence,
                        () => throw new InvalidOperationException("Since both standard output and standard error output are redirected, the console screen cannot be cleared."));
                }
            }
            else
            {
                WriteAnsiEscapeCodeToConsole(
                    ThisTerminalInfo.ClearBuffer
                        ?? ThisTerminalInfo.ClearScreen
                        ?? throw new InvalidOperationException("This terminal does not define the capability to clear the console buffer."),
                    () => throw new InvalidOperationException("Since both standard output and standard error output are redirected, the console screen cannot be cleared."));
            }
        }

        #endregion

        #region Erase

        /// <summary>
        /// コンソールバッファまたはコンソールウィンドウの全体または一部を消去します。
        /// </summary>
        /// <param name="eraseMode">
        /// 消去の方法を示す<see cref="ConsoleEraseMode"/>値です。
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item><paramref name="eraseMode"/>で指定された方法での消去をターミナルがサポートしていません。</item>
        /// </list>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="eraseMode"/>の値がサポートされていません。
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Erase(ConsoleEraseMode eraseMode)
        {
            if (ImplementWithWin32Api)
            {
                if (_consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
                    throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to delete console characters.");

                if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console buffer info.", Marshal.GetExceptionForHR(Marshal.GetLastWin32Error()));

                var screenWidth = consoleInfo.srWindow.Right - consoleInfo.srWindow.Left + 1;
                switch (eraseMode)
                {
                    case ConsoleEraseMode.FromCursorToEndOfScreen:
                        ClearScreenCore(
                            consoleInfo.dwCursorPosition.X,
                            consoleInfo.dwCursorPosition.Y,
                            consoleInfo.srWindow.Right - consoleInfo.dwCursorPosition.X + 1,
                            consoleInfo.wAttributes);
                        for (var row = consoleInfo.dwCursorPosition.Y + 1; row <= consoleInfo.srWindow.Bottom; row++)
                            ClearScreenCore(consoleInfo.srWindow.Left, row, screenWidth, consoleInfo.wAttributes);
                        break;
                    case ConsoleEraseMode.FromBeggingOfScreenToCursor:
                        for (var row = consoleInfo.srWindow.Top; row <= consoleInfo.dwCursorPosition.Y - 1; row++)
                            ClearScreenCore(consoleInfo.srWindow.Left, row, screenWidth, consoleInfo.wAttributes);
                        ClearScreenCore(
                            consoleInfo.srWindow.Left,
                            consoleInfo.dwCursorPosition.Y,
                            consoleInfo.dwCursorPosition.X - consoleInfo.srWindow.Left + 1,
                            consoleInfo.wAttributes);
                        break;
                    case ConsoleEraseMode.EntireScreen:
                    {
                        // カーソルをホームポジションに設定
                        if (!InterOpWindows.SetConsoleCursorPosition(_consoleOutputHandle, new InterOpWindows.COORD { X = consoleInfo.srWindow.Left, Y = consoleInfo.srWindow.Top }))
                            throw new InvalidOperationException("Failed to set cursor position.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                        for (var row = consoleInfo.srWindow.Top; row <= consoleInfo.srWindow.Bottom; row++)
                            ClearScreenCore(consoleInfo.srWindow.Left, row, screenWidth, consoleInfo.wAttributes);
                        break;
                    }
                    default:
                    {
                        Int32 startX;
                        Int32 startY;
                        Int32 length;
                        switch (eraseMode)
                        {
                            case ConsoleEraseMode.EntireConsoleBuffer:
                            {
                                // カーソルをホームポジションに設定
                                if (!InterOpWindows.SetConsoleCursorPosition(_consoleOutputHandle, new InterOpWindows.COORD { X = 0, Y = 0 }))
                                    throw new InvalidOperationException("Failed to set cursor position.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                                startX = 0;
                                startY = 0;
                                length = consoleInfo.dwSize.X * consoleInfo.dwSize.Y;
                                break;
                            }
                            case ConsoleEraseMode.FromCursorToEndOfLine:
                                startX = consoleInfo.dwCursorPosition.X;
                                startY = consoleInfo.dwCursorPosition.Y;
                                length = screenWidth - consoleInfo.dwCursorPosition.X;
                                break;
                            case ConsoleEraseMode.FromBeggingOfLineToCursor:
                                startX = consoleInfo.srWindow.Left;
                                startY = consoleInfo.dwCursorPosition.Y;
                                length = consoleInfo.dwCursorPosition.X + 1;
                                break;
                            case ConsoleEraseMode.EntireLine:
                                startX = consoleInfo.srWindow.Left;
                                startY = consoleInfo.dwCursorPosition.Y;
                                length = screenWidth;
                                break;
                            default:
                                throw new ArgumentException($"Invalid value: {eraseMode}", nameof(eraseMode));
                        }

                        ClearScreenCore(startX, startY, length, consoleInfo.wAttributes);

                        if (eraseMode == ConsoleEraseMode.EntireConsoleBuffer)
                        {
                            // Windows ターミナルなどのターミナルでは Win32 API のみではコンソールバッファが消去されないため、エスケープコードも併用する。
                            var eraseScrollBufferEscapeSequence = ThisTerminalInfo.EraseScrollBuffer;
                            if (eraseScrollBufferEscapeSequence is not null)
                                WriteAnsiEscapeCodeToConsole(eraseScrollBufferEscapeSequence, () => { });
                        }

                        break;
                    }
                }
            }
            else
            {
                WriteAnsiEscapeCodeToConsole(
                    eraseMode switch
                    {
                        ConsoleEraseMode.FromCursorToEndOfScreen => ThisTerminalInfo.ClrEos ?? throw new InvalidOperationException("This terminal does not support the capability \"clr_eos\"."),
                        ConsoleEraseMode.FromBeggingOfScreenToCursor => ThisTerminalInfo.EraseInDisplay1 ?? throw new InvalidOperationException("This terminal does not support the capability to erase from the beginning of the screen to the cursor position."),
                        ConsoleEraseMode.EntireScreen => ThisTerminalInfo.ClearScreen ?? throw new InvalidOperationException("This terminal does not support the capability \"clear_screen\"."),
                        ConsoleEraseMode.EntireConsoleBuffer => ThisTerminalInfo.ClearBuffer ?? throw new InvalidOperationException("This terminal doesn't support the capability to clear the console buffer."),
                        ConsoleEraseMode.FromCursorToEndOfLine => ThisTerminalInfo.ClrEol ?? throw new InvalidOperationException("This terminal does not support the capability \"clr_eol\"."),
                        ConsoleEraseMode.FromBeggingOfLineToCursor => ThisTerminalInfo.ClrBol ?? throw new InvalidOperationException("This terminal does not support the capability \"clr_bol\"."),
                        ConsoleEraseMode.EntireLine => ThisTerminalInfo.EraseInLine2 ?? throw new InvalidOperationException("This terminal does not support the capability to erase entire lines."),
                        _ => throw new ArgumentException($"Invalid erase mode.: {eraseMode}", nameof(eraseMode)),
                    },
                    () => throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to delete console characters."));
            }
        }

        #endregion

        #region CursorVisible

        /// <summary>
        /// カーソルの可視性を <see cref="ConsoleCursorVisiblity"/> 列挙体で設定します。
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>カーソルの可視性の変更をターミナルがサポートしていません。</item>
        /// </list>
        /// </exception>
        public static ConsoleCursorVisiblity CursorVisible
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value.IsNoneOf(ConsoleCursorVisiblity.Invisible, ConsoleCursorVisiblity.NormalMode, ConsoleCursorVisiblity.HighVisibilityMode))
                    throw new ArgumentOutOfRangeException(nameof(value));

                if (ImplementWithWin32Api)
                {
                    if (_consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
                        throw new InvalidOperationException("Since both standard output and standard error are redirected, it is not possible to change the visibility of the cursor.");

                    if (!InterOpWindows.GetConsoleCursorInfo(_consoleOutputHandle, out var cursorInfo))
                        throw new InvalidOperationException("Failed to get console cursor info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                    (cursorInfo.bVisible, cursorInfo.dwSize) =
                        value switch
                        {
                            ConsoleCursorVisiblity.Invisible => (false, 1U),
                            ConsoleCursorVisiblity.NormalMode => (true, 25U),
                            ConsoleCursorVisiblity.HighVisibilityMode => (true, 100U),
                            _ => throw Validation.GetFailErrorException($"Unexpected value \"{value}\""),
                        };
                    if (!InterOpWindows.SetConsoleCursorInfo(_consoleOutputHandle, ref cursorInfo))
                        throw new InvalidOperationException("Failed to set console cursor info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));
                }
                else
                {
                    WriteAnsiEscapeCodeToConsole(
                        value switch
                        {
                            ConsoleCursorVisiblity.Invisible => ThisTerminalInfo.CursorInvisible,
                            ConsoleCursorVisiblity.NormalMode => ThisTerminalInfo.CursorNormal,
                            ConsoleCursorVisiblity.HighVisibilityMode => ThisTerminalInfo.CursorVisible ?? ThisTerminalInfo.CursorNormal,
                            _ => throw Validation.GetFailErrorException($"Unexpected value \"{value}\""),
                        }
                        ?? throw new ArgumentException($"This terminal does not support {value}."),
                        () => throw new InvalidOperationException("Since both standard output and standard error are redirected, it is not possible to change the visibility of the cursor."));
                }
            }
        }

        #endregion

        #region CursorUp

        /// <summary>
        /// カーソルを指定された行数だけ上に移動します。
        /// </summary>
        /// <param name="n">
        /// カーソルを移動する行数です。
        /// </param>
        /// <remarks>
        /// コンソールウィンドウの上端を超えて移動することはできません。
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>カーソルの行の移動をターミナルがサポートしていません。</item>
        /// </list>
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CursorUp(Int32 n)
            => MoveCursorVertically(-n, () => throw new InvalidOperationException("Since both standard output and standard error output are redirected, the cursor position cannot be changed."));

        #endregion

        #region CursorDown

        /// <summary>
        /// カーソルを指定された行数だけ下に移動します。
        /// </summary>
        /// <param name="n">
        /// カーソルを移動する行数です。
        /// </param>
        /// <remarks>
        /// コンソールウィンドウの下端を超えて移動することはできません。
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>カーソルの行の移動をターミナルがサポートしていません。</item>
        /// </list>
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CursorDown(Int32 n)
            => MoveCursorVertically(n, () => throw new InvalidOperationException("Since both standard output and standard error output are redirected, the cursor position cannot be changed."));

        #endregion

        #region CursorBack

        /// <summary>
        /// カーソルを指定された桁数だけ左に移動します。
        /// </summary>
        /// <param name="n">
        /// カーソルを移動する桁数です。
        /// </param>
        /// <remarks>
        /// コンソールウィンドウの左端を超えて移動することはできません。
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>カーソルの桁の移動をターミナルがサポートしていません。</item>
        /// </list>
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CursorBack(Int32 n)
            => MoveCursorHorizontally(-n, () => throw new InvalidOperationException("Since both standard output and standard error output are redirected, the cursor position cannot be changed."));

        #endregion

        #region CursorForward

        /// <summary>
        /// カーソルを指定された桁数だけ右に移動します。
        /// </summary>
        /// <param name="n">
        /// カーソルを移動する桁数です。
        /// </param>
        /// <remarks>
        /// コンソールウィンドウの右端を超えて移動することはできません。
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// <item>カーソルの桁の移動をターミナルがサポートしていません。</item>
        /// </list>
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CursorForward(Int32 n)
            => MoveCursorHorizontally(n, () => throw new InvalidOperationException("Since both standard output and standard error output are redirected, the cursor position cannot be changed."));

        #endregion

        #region Terminal

        /// <summary>
        /// 現在使用中のターミナルの情報を取得します。
        /// </summary>
        public static TerminalInfo Terminal
            => ThisTerminalInfo
                ?? throw new InvalidOperationException("Information about the terminal currently in use cannot be found.");

        #endregion

        #region OutputEscapeCode

        /// <summary>
        /// 指定されたエスケープコードをターミナルに出力します。
        /// </summary>
        /// <param name="escapeCode">
        /// ターミナルに出力するエスケープコードです。
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// <list type="bullet">
        /// <item>標準出力と標準エラー出力がともにリダイレクトされています。</item>
        /// </list>
        /// </exception>
        /// <remarks>
        /// <list type="bullet">
        /// <item><paramref name="escapeCode"/>で与えられたエスケープコードが正しいかどうかはチェックされません。</item>
        /// <item>ターミナルの種類によりどのエスケープコードがサポートされているかは異なります。実行環境によっては期待した結果を生まない可能性があることを忘れないでください。</item>
        /// </list>
        /// </remarks>
        public static void OutputEscapeCode(String escapeCode)
            => WriteAnsiEscapeCodeToConsole(
                escapeCode,
                () => throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to output the escape code."));

        #endregion

        #region private methods

        private static void SetBackgroundColorCore(ConsoleColor value)
        {
            if (ImplementWithWin32Api)
            {
                if (_consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
                    throw new InvalidOperationException("Both standard output and standard error output are redirected, so console attributes cannot be changed.");

                if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                var consoleAtrribute =
                    InterOpWindows.FromConsoleColorsToConsoleAttribute(
                        value,
                        InterOpWindows.FromConsoleAttributeToConsoleColors(consoleInfo.wAttributes).foregroundColor);
                if (!InterOpWindows.SetConsoleTextAttribute(_consoleOutputHandle, consoleAtrribute))
                    throw new InvalidOperationException("Failed to set console text attribute.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));
            }
            else
            {

                WriteAnsiEscapeCodeToConsole(
                    ThisTerminalInfo.SetABackground(value.ToAnsiColor16())
                    ?? ThisTerminalInfo.SetBackground(value.ToColor8())
                    ?? throw new InvalidOperationException("This terminal does not define the capability to change the text background color."),
                    () => throw new InvalidOperationException("Both standard output and standard error output are redirected, so console attributes cannot be changed."));

            }

            _currentBackgrouongColor = value;
        }

        private static void SetForegroundColorCore(ConsoleColor value)
        {
            if (ImplementWithWin32Api)
            {
                if (_consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
                    throw new InvalidOperationException("Both standard output and standard error output are redirected, so console attributes cannot be changed.");

                if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                var consoleAtrribute =
                    InterOpWindows.FromConsoleColorsToConsoleAttribute(
                        InterOpWindows.FromConsoleAttributeToConsoleColors(consoleInfo.wAttributes).backgroundColor,
                        value);
                if (!InterOpWindows.SetConsoleTextAttribute(_consoleOutputHandle, consoleAtrribute))
                    throw new InvalidOperationException("Failed to set console text attribute.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

            }
            else
            {
                WriteAnsiEscapeCodeToConsole(
                    ThisTerminalInfo.SetAForeground(value.ToAnsiColor16())
                        ?? ThisTerminalInfo.SetForeground(value.ToColor8())
                        ?? throw new InvalidOperationException("This terminal does not define the capability to change the foreground color of characters."),
                    () => throw new InvalidOperationException("Both standard output and standard error output are redirected, so console attributes cannot be changed."));
            }

            _currentForegrouongColor = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (Int32 windowWidth, Int32 windowHeight) GetWindowSizeCore()
        {
            if (OperatingSystem.IsWindows())
            {
                if (_consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
                    throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to get window size.");

                if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                return (consoleInfo.srWindow.Right - consoleInfo.srWindow.Left + 1, consoleInfo.srWindow.Bottom - consoleInfo.srWindow.Top + 1);
            }
            else
            {
                if (_consoleOutputFileNo < 0)
                    throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to get window size.");

                if (InterOpUnix.GetWindowSize(_consoleOutputFileNo, out var windowSize, out _) == 0)
                    return (windowSize.Col, windowSize.Row);
                return (ThisTerminalInfo.Columns ?? throw new InvalidOperationException("The terminal does not have the capability \"columns\" defined."), ThisTerminalInfo.Lines ?? throw new InvalidOperationException("The terminal does not have the capability \"lines\" defined."));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void MoveCursorVertically(Int32 n, Action errorHandler)
        {
            if (ImplementWithWin32Api)
            {
                if (_consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
                    throw new InvalidOperationException("Both standard output and standard error output are redirected, so the cursor position cannot be moved.");

                if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console buffer info.", Marshal.GetExceptionForHR(Marshal.GetLastWin32Error()));

                if (!InterOpWindows.SetConsoleCursorPosition(
                    _consoleOutputHandle,
                    new InterOpWindows.COORD
                    {
                        X = consoleInfo.dwCursorPosition.X,
                        Y = checked((Int16)(consoleInfo.dwCursorPosition.Y + n).Maximum(consoleInfo.srWindow.Top).Minimum(consoleInfo.srWindow.Bottom)),
                    }))
                {
                    throw new InvalidOperationException("Failed to set console cursor position.", Marshal.GetExceptionForHR(Marshal.GetLastWin32Error()));
                }
            }
            else
            {
                if (n > 0)
                {
                    WriteAnsiEscapeCodeToConsole(
                        ThisTerminalInfo.ParmDownCursor(n) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_down_cursor\"."),
                        errorHandler);
                }
                else if (n < 0)
                {
                    WriteAnsiEscapeCodeToConsole(
                        ThisTerminalInfo.ParmUpCursor(checked(-n)) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_up_cursor\"."),
                        errorHandler);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void MoveCursorHorizontally(Int32 n, Action errorHandler)
        {
            if (ImplementWithWin32Api)
            {
                if (_consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
                    throw new InvalidOperationException("Both standard output and standard error output are redirected, so the cursor position cannot be moved.");

                if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console buffer info.", Marshal.GetExceptionForHR(Marshal.GetLastWin32Error()));

                if (!InterOpWindows.SetConsoleCursorPosition(
                    _consoleOutputHandle,
                    new InterOpWindows.COORD
                    {
                        X = checked((Int16)(consoleInfo.dwCursorPosition.X + n).Maximum(consoleInfo.srWindow.Left).Minimum(consoleInfo.srWindow.Right)),
                        Y = consoleInfo.dwCursorPosition.Y
                    }))
                {
                    throw new InvalidOperationException("Failed to set console cursor position.", Marshal.GetExceptionForHR(Marshal.GetLastWin32Error()));
                }
            }
            else
            {
                if (n > 0)
                {
                    WriteAnsiEscapeCodeToConsole(
                        ThisTerminalInfo.ParmRightCursor(n) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_right_cursor\"."),
                        errorHandler);
                }
                else if (n < 0)
                {
                    WriteAnsiEscapeCodeToConsole(
                        ThisTerminalInfo.ParmLeftCursor(checked(-n)) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_left_cursor\"."),
                        errorHandler);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteAnsiEscapeCodeToConsole(String ansiEscapeCode, Action errorHandler)
        {
            if (_escapeCodeWriter is not null)
                _escapeCodeWriter.Write(ansiEscapeCode);
            else
                errorHandler();
        }

        // Win32 API を使用する条件: 強制的に ANSI エスケープコードを使用する実装ではなく、かつプラットフォームが Windows である
        private static Boolean ImplementWithWin32Api
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => !_useAnsiEscapeCodeEvenOnWindows && OperatingSystem.IsWindows();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ClearScreenCore(Int32 startX, Int32 startY, Int32 length, UInt16 attribute)
        {
            var startPosition =
                new InterOpWindows.COORD
                {
                    X = checked((Int16)startX),
                    Y = checked((Int16)startY),
                };
            if (!InterOpWindows.FillConsoleOutputCharacter(_consoleOutputHandle, (Int16)' ', (UInt32)length, startPosition, out _))
                throw new InvalidOperationException("Failed to clear console buffer characters.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

            if (!InterOpWindows.FillConsoleOutputAttribute(_consoleOutputHandle, attribute, (UInt32)length, startPosition, out _))
                throw new InvalidOperationException("Failed to clear console buffer attributes.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetCharacterSet(CharacterSet charSet)
        {
            switch (charSet)
            {
                case CharacterSet.Primary:
                    if (_currentCharSet != charSet)
                    {
                        if (_escapeCodeWriter is null)
                            throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to change the character set.");
                        var escapeCode = ThisTerminalInfo.ExitAltCharsetMode ?? throw new InvalidOperationException("The terminal does not define the capability \"exit_alt_charset_mode\".");
                        _escapeCodeWriter.Write(escapeCode);
                        _currentCharSet = charSet;
                    }

                    break;
                case CharacterSet.Alternative:
                    if (_currentCharSet != charSet)
                    {
                        if (_escapeCodeWriter is null)
                            throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to change the character set.");
                        var escapeCode = ThisTerminalInfo.EnterAltCharsetMode ?? throw new InvalidOperationException("The terminal does not define the capability \"enter_alt_charset_mode\".");
                        _escapeCodeWriter.Write(escapeCode);
                        _currentCharSet = charSet;
                    }

                    break;
                default:
                    throw Validation.GetFailErrorException($"Unexpected {nameof(CharacterSet)} value: {charSet}");
            }
        }

        #endregion
    }
}
