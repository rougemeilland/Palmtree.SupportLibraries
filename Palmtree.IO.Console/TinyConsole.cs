﻿#define USE_WIN32_API_TO_CONSOLE_OPERATION_FOR_WINDOWS
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
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
        private static readonly Object _lockObject;
        private static readonly ConsoleColor _defaultBackgrouongColor;
        private static readonly ConsoleColor _defaultForegrouongColor;

        private static TerminalInfo? __thisTerminalInfo;
        private static Boolean __initializedRedirection;
        private static IntPtr __consoleOutputHandle;
        private static Int32 __consoleOutputFileNo;
        private static Char[]? __alternativeCharacterSetMap;
        private static TextWriter? __consoleTextWriter;
        private static TextWriter? __escapeCodeWriter;
        private static ConsoleColor _currentBackgrouongColor;
        private static ConsoleColor _currentForegrouongColor;
        private static CharacterSet _currentCharSet;
        private static ConsoleTextWriterType _defaultTextWriter;

        #region private properties

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "<保留中>")]
        private static TerminalInfo _thisTerminalInfo
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                lock (_lockObject)
                {
                    if (__thisTerminalInfo is not null)
                        return __thisTerminalInfo;
                    __thisTerminalInfo = TerminalInfo.GetTerminalInfo(true);
                    if (__thisTerminalInfo is null)
                        throw new InvalidOperationException("Terminal information not found.");
                    return __thisTerminalInfo;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "<保留中>")]
        private static IntPtr _consoleOutputHandle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                lock (_lockObject)
                {
                    if (!__initializedRedirection)
                        RefreshRedirectionSettings();
                    return __consoleOutputHandle;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "<保留中>")]
        private static Int32 _consoleOutputFileNo
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                lock (_lockObject)
                {
                    if (!__initializedRedirection)
                        RefreshRedirectionSettings();
                    return __consoleOutputFileNo;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "<保留中>")]
        private static TextWriter _consoleTextWriter
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                lock (_lockObject)
                {
                    if (!__initializedRedirection)
                        RefreshRedirectionSettings();
                    Validation.Assert(__consoleTextWriter is not null, "__consoleTextWriter is not null");
                    return __consoleTextWriter;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "<保留中>")]
        private static TextWriter? _escapeCodeWriter
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                lock (_lockObject)
                {
                    if (!__initializedRedirection)
                        RefreshRedirectionSettings();
                    return __escapeCodeWriter;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:命名スタイル", Justification = "<保留中>")]
        private static Char[] _alternativeCharacterSetMap
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                lock (_lockObject)
                {
                    if (__alternativeCharacterSetMap is not null)
                        return __alternativeCharacterSetMap;

                    __alternativeCharacterSetMap = Array.Empty<Char>();
                    var acs = _thisTerminalInfo.AcsChars;
                    if (acs is not null)
                    {
                        __alternativeCharacterSetMap = new Char[_alternativeCharacterSetMapMaximumKey - _alternativeCharacterSetMapMinimumKey + 1];
                        Array.Fill(__alternativeCharacterSetMap, '\u0000');
                        for (var index = 0; index + 1 < acs.Length; index += 2)
                            __alternativeCharacterSetMap[acs[index] - _alternativeCharacterSetMapMinimumKey] = acs[index + 1];
                    }

                    return __alternativeCharacterSetMap;
                }
            }
        }

        #endregion

        static TinyConsole()
        {
            _dllNameResolver = new NativeDllNameResolver();
            NativeLibrary.SetDllImportResolver(
                typeof(InterOpUnix).Assembly,
                (libraryName, assembly, searchPath) => _dllNameResolver.ResolveDllName(libraryName, assembly, searchPath));
            _lockObject = new Object();
            _defaultBackgrouongColor = System.Console.BackgroundColor;
            _defaultForegrouongColor = System.Console.ForegroundColor;

            __thisTerminalInfo = null;
            __initializedRedirection = false;
            __consoleOutputHandle = InterOpWindows.INVALID_HANDLE_VALUE;
            __consoleOutputFileNo = -1;
            __consoleTextWriter = null;
            __escapeCodeWriter = null;
            __alternativeCharacterSetMap = null;
            _currentBackgrouongColor = System.Console.BackgroundColor;
            _currentForegrouongColor = System.Console.ForegroundColor;
            _currentCharSet = CharacterSet.Primary;
            _defaultTextWriter = ConsoleTextWriterType.None;
        }

        #region DefaultTextWriter

        /// <summary>
        /// 標準出力および標準エラー出力がともにリダイレクトされている場合の既定の出力先を取得または設定します。
        /// </summary>
        /// <value>
        /// テキストの出力先を示す <see cref="ConsoleTextWriterType"/> 値です。
        /// </value>
        /// <remarks>
        /// </remarks>
        public static ConsoleTextWriterType DefaultTextWriter
        {
            get => _defaultTextWriter;

            set
            {
                if (value != _defaultTextWriter)
                {
                    _defaultTextWriter = value;
                    ClearRedirectionSettings();
                }
            }
        }

        #endregion

        #region InputEncoding / OutputEncoding

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
            set
            {
                System.Console.OutputEncoding = value;
                ClearRedirectionSettings();
            }
        }

        #endregion

        #region SetIn / SetOut / SetErr

        /// <summary>
        /// 指定した <see cref="TextReader"/> を <see cref="In"/> プロパティに設定します。
        /// </summary>
        /// <param name="newIn">
        /// 新しい標準入力であるストリームです。
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetIn(TextReader newIn)
        {
            System.Console.SetIn(newIn);
            ClearRedirectionSettings();
        }

        /// <summary>
        /// 指定した <see cref="TextWriter"/> を <see cref="Out"/> プロパティに設定します。
        /// </summary>
        /// <param name="newOut">
        /// 新しい標準出力であるストリームです。
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetOut(TextWriter newOut)
        {
            System.Console.SetOut(newOut);
            ClearRedirectionSettings();
        }

        /// <summary>
        /// 指定した <see cref="TextWriter"/> を <see cref="Error"/> プロパティに設定します。
        /// </summary>
        /// <param name="newError">
        /// 新しい標準エラー出力であるストリームです。
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetError(TextWriter newError)
        {
            System.Console.SetError(newError);
            ClearRedirectionSettings();
        }

        #endregion

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
                if (ImplementWithWin32Api && _consoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
                {
                    if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                        throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                    (_currentBackgrouongColor, _) = InterOpWindows.FromConsoleAttributeToConsoleColors(consoleInfo.wAttributes);
                    return _currentBackgrouongColor;
                }
                else
                {
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
                if (ImplementWithWin32Api && _consoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
                {
                    if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                        throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                    (_, _currentForegrouongColor) = InterOpWindows.FromConsoleAttributeToConsoleColors(consoleInfo.wAttributes);
                    return _currentBackgrouongColor;
                }
                else
                {
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
            if (ImplementWithWin32Api && _consoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
                var consoleAtrribute = InterOpWindows.FromConsoleColorsToConsoleAttribute(_defaultBackgrouongColor, _defaultForegrouongColor);
                if (!InterOpWindows.SetConsoleTextAttribute(_consoleOutputHandle, consoleAtrribute))
                    throw new InvalidOperationException("Failed to set console text attribute.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                _currentBackgrouongColor = _defaultBackgrouongColor;
                _currentForegrouongColor = _defaultForegrouongColor;
            }
            else
            {
                var resetColorEscapeCode = _thisTerminalInfo.ResetColor;
                if (resetColorEscapeCode is not null)
                {
                    // 標準出力及び標準エラー出力が共にリダイレクトされている場合でもエラーとはしない。
                    WriteAnsiEscapeCodeToConsole(resetColorEscapeCode, () => { });
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
                        _thisTerminalInfo.SetTitle(value)
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
            // Windows でのみ "System.Console.Beep()" を呼び出しているのは、UNIX 系の実装ではエスケープコードの出力先が標準出力に固定されているから。
            if (ImplementWithWin32Api)
                System.Console.Beep();
            else if (_escapeCodeWriter is not null && _thisTerminalInfo.Bell is not null)
                WriteAnsiEscapeCodeToConsole(_thisTerminalInfo.Bell, () => { });
            else
                _escapeCodeWriter?.Write('\a');
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
            if (ImplementWithWin32Api && _consoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
                if (!InterOpWindows.GetConsoleScreenBufferInfo(_consoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                if (!InterOpWindows.SetConsoleCursorPosition(_consoleOutputHandle, new InterOpWindows.COORD { X = 0, Y = 0 }))
                    throw new InvalidOperationException("Failed to set cursor position.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                ClearScreenCore(0, 0, consoleInfo.dwSize.X * consoleInfo.dwSize.Y, consoleInfo.wAttributes);

                // Windows ターミナルなどのターミナルでは Win32 API のみではコンソールバッファが消去されないため、エスケープコードも併用する。
                var eraseScrollBufferEscapeSequence = _thisTerminalInfo.EraseScrollBuffer;
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
                    _thisTerminalInfo.ClearBuffer
                        ?? _thisTerminalInfo.ClearScreen
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
            if (ImplementWithWin32Api && _consoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
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
                            var eraseScrollBufferEscapeSequence = _thisTerminalInfo.EraseScrollBuffer;
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
                        ConsoleEraseMode.FromCursorToEndOfScreen => _thisTerminalInfo.ClrEos ?? throw new InvalidOperationException("This terminal does not support the capability \"clr_eos\"."),
                        ConsoleEraseMode.FromBeggingOfScreenToCursor => _thisTerminalInfo.EraseInDisplay1 ?? throw new InvalidOperationException("This terminal does not support the capability to erase from the beginning of the screen to the cursor position."),
                        ConsoleEraseMode.EntireScreen => _thisTerminalInfo.ClearScreen ?? throw new InvalidOperationException("This terminal does not support the capability \"clear_screen\"."),
                        ConsoleEraseMode.EntireConsoleBuffer => _thisTerminalInfo.ClearBuffer ?? throw new InvalidOperationException("This terminal doesn't support the capability to clear the console buffer."),
                        ConsoleEraseMode.FromCursorToEndOfLine => _thisTerminalInfo.ClrEol ?? throw new InvalidOperationException("This terminal does not support the capability \"clr_eol\"."),
                        ConsoleEraseMode.FromBeggingOfLineToCursor => _thisTerminalInfo.ClrBol ?? throw new InvalidOperationException("This terminal does not support the capability \"clr_bol\"."),
                        ConsoleEraseMode.EntireLine => _thisTerminalInfo.EraseInLine2 ?? throw new InvalidOperationException("This terminal does not support the capability to erase entire lines."),
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

                if (ImplementWithWin32Api && _consoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
                {
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
                    // 標準出力及び標準エラー出力が共にリダイレクトされている場合でもエラーとはしない。
                    WriteAnsiEscapeCodeToConsole(
                        value switch
                        {
                            ConsoleCursorVisiblity.Invisible => _thisTerminalInfo.CursorInvisible,
                            ConsoleCursorVisiblity.NormalMode => _thisTerminalInfo.CursorNormal,
                            ConsoleCursorVisiblity.HighVisibilityMode => _thisTerminalInfo.CursorVisible ?? _thisTerminalInfo.CursorNormal,
                            _ => throw Validation.GetFailErrorException($"Unexpected value \"{value}\""),
                        }
                        ?? throw new ArgumentException($"This terminal does not support {value}."),
                        () => { });
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
            => _thisTerminalInfo
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

        private static void ClearRedirectionSettings()
        {
            lock (_lockObject)
            {
                __initializedRedirection = false;
            }
        }

        private static void RefreshRedirectionSettings()
        {
            if (!__initializedRedirection)
            {
                __consoleTextWriter?.Dispose();
                __escapeCodeWriter?.Dispose();
                if (!System.Console.IsOutputRedirected)
                {
                    __consoleOutputHandle =
                        OperatingSystem.IsWindows()
                        ? InterOpWindows.GetStdHandle(InterOpWindows.STD_OUTPUT_HANDLE)
                        : InterOpWindows.INVALID_HANDLE_VALUE;
                    __consoleOutputFileNo =
                        OperatingSystem.IsWindows()
                        ? -1
                        : InterOpUnix.GetStandardFileNo(InterOpUnix.STANDARD_FILE_OUT);
                    __consoleTextWriter = CreateTextWriter(System.Console.OpenStandardOutput(), System.Console.OutputEncoding);
                    __escapeCodeWriter = IsSupportedAnsiEscapeSequence(__consoleOutputHandle) ? __consoleTextWriter : null;
                    OutputExitAltCharsetMode();
                }
                else if (!System.Console.IsErrorRedirected)
                {
                    __consoleOutputHandle =
                        OperatingSystem.IsWindows()
                        ? InterOpWindows.GetStdHandle(InterOpWindows.STD_ERROR_HANDLE)
                        : InterOpWindows.INVALID_HANDLE_VALUE;
                    __consoleOutputFileNo =
                        OperatingSystem.IsWindows()
                        ? -1
                        : InterOpUnix.GetStandardFileNo(InterOpUnix.STANDARD_FILE_ERR);
                    __consoleTextWriter = CreateTextWriter(System.Console.OpenStandardError(), System.Console.OutputEncoding);
                    __escapeCodeWriter = IsSupportedAnsiEscapeSequence(__consoleOutputHandle) ? __consoleTextWriter : null;
                    OutputExitAltCharsetMode();
                }
                else
                {
                    __consoleOutputHandle = InterOpWindows.INVALID_HANDLE_VALUE;
                    __consoleOutputFileNo = -1;
                    switch (_defaultTextWriter)
                    {
                        case ConsoleTextWriterType.StandardOutput:
                            __consoleTextWriter = CreateTextWriter(System.Console.OpenStandardOutput(), System.Console.OutputEncoding);
                            __escapeCodeWriter = IsSupportedAnsiEscapeSequence(__consoleOutputHandle) ? __consoleTextWriter : null;
                            break;
                        case ConsoleTextWriterType.StandardError:
                            __consoleTextWriter = CreateTextWriter(System.Console.OpenStandardError(), System.Console.OutputEncoding);
                            __escapeCodeWriter = IsSupportedAnsiEscapeSequence(__consoleOutputHandle) ? __consoleTextWriter : null;
                            break;
                        default:
                            __consoleTextWriter = TextWriter.Null;
                            __escapeCodeWriter = null;
                            break;
                    }

                    OutputExitAltCharsetMode();
                }

                __initializedRedirection = true;
            }

            static TextWriter CreateTextWriter(Stream outStream, Encoding encoding)
                => outStream.AsTextWriter(encoding.WithoutPreamble(), 256, true, true);

            static void OutputExitAltCharsetMode()
            {
                if (!ImplementWithWin32Api && __escapeCodeWriter is not null && __thisTerminalInfo is not null)
                {
                    var exitAltCharsetMode = __thisTerminalInfo.ExitAltCharsetMode;
                    if (exitAltCharsetMode is not null)
                        __escapeCodeWriter.Write(exitAltCharsetMode);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean IsSupportedAnsiEscapeSequence(IntPtr consoleOutputHandle)
        {
            if (!OperatingSystem.IsWindows())
            {
                // Windows プラットフォームではない場合

                // エスケープコードを解釈可能と判断する
                return true;
            }

            if (consoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE)
            {
                // コンソール出力ハンドルが無効である場合

                // Win32 API によるコンソール操作ができないので、(かなり強引ではあるが) エスケープコードを解釈可能とする。
                return true;
            }

            // Windows プラットフォームであり、かつ
            // コンソール出力ハンドルが有効である (つまり標準出力と標準エラー出力のどちらかがリダイレクトされていない) 場合

            // コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグ (エスケープコードを解釈可能かどうか) を調べる
            if (!InterOpWindows.GetConsoleMode(consoleOutputHandle, out var mode))
                throw new Exception("Failed to get console mode.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

            if ((mode & InterOpWindows.ENABLE_VIRTUAL_TERMINAL_PROCESSING) != 0)
            {
                // コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグが立っている (既にエスケープコードを解釈可能である) 場合
                return true;
            }

            // コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグが立っていない場合

            // コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグをセットする
            if (!InterOpWindows.SetConsoleMode(consoleOutputHandle, mode | InterOpWindows.ENABLE_VIRTUAL_TERMINAL_PROCESSING))
                throw new Exception("Failed to set console mode.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

            // 再度、コンソールモードの ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグを調べる
            if (!InterOpWindows.GetConsoleMode(consoleOutputHandle, out mode))
                throw new Exception("Failed to get console mode.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

            if ((mode & InterOpWindows.ENABLE_VIRTUAL_TERMINAL_PROCESSING) != 0)
            {
                // コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグが立っている (エスケープコードを解釈可能になった) 場合
                return true;
            }

            // 一度コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグをセットしたにもかかわらず、ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグがセットされていない場合

            // ターミナルがエスケープコードをサポートしていないとみなす
            return false;
        }

        private static void SetBackgroundColorCore(ConsoleColor value)
        {
            if (ImplementWithWin32Api && _consoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
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
                // 標準出力及び標準エラー出力が共にリダイレクトされている場合でもエラーとはしない。
                WriteAnsiEscapeCodeToConsole(
                    _thisTerminalInfo.SetABackground(value.ToAnsiColor16())
                    ?? _thisTerminalInfo.SetBackground(value.ToColor8())
                    ?? throw new InvalidOperationException("This terminal does not define the capability to change the text background color."),
                    () => { });

            }

            _currentBackgrouongColor = value;
        }

        private static void SetForegroundColorCore(ConsoleColor value)
        {
            if (ImplementWithWin32Api && _consoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
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
                // 標準出力及び標準エラー出力が共にリダイレクトされている場合でもエラーとはしない。
                WriteAnsiEscapeCodeToConsole(
                    _thisTerminalInfo.SetAForeground(value.ToAnsiColor16())
                        ?? _thisTerminalInfo.SetForeground(value.ToColor8())
                        ?? throw new InvalidOperationException("This terminal does not define the capability to change the foreground color of characters."),
                    () => { });
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
                return (_thisTerminalInfo.Columns ?? throw new InvalidOperationException("The terminal does not have the capability \"columns\" defined."), _thisTerminalInfo.Lines ?? throw new InvalidOperationException("The terminal does not have the capability \"lines\" defined."));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void MoveCursorVertically(Int32 n, Action errorHandler)
        {
            if (ImplementWithWin32Api && _consoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
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
                        _thisTerminalInfo.ParmDownCursor(n) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_down_cursor\"."),
                        errorHandler);
                }
                else if (n < 0)
                {
                    WriteAnsiEscapeCodeToConsole(
                        _thisTerminalInfo.ParmUpCursor(checked(-n)) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_up_cursor\"."),
                        errorHandler);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void MoveCursorHorizontally(Int32 n, Action errorHandler)
        {
            if (ImplementWithWin32Api && _consoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
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
                        _thisTerminalInfo.ParmRightCursor(n) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_right_cursor\"."),
                        errorHandler);
                }
                else if (n < 0)
                {
                    WriteAnsiEscapeCodeToConsole(
                        _thisTerminalInfo.ParmLeftCursor(checked(-n)) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_left_cursor\"."),
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
                        var escapeCode = _thisTerminalInfo.ExitAltCharsetMode ?? throw new InvalidOperationException("The terminal does not define the capability \"exit_alt_charset_mode\".");
                        _escapeCodeWriter.Write(escapeCode);
                        _currentCharSet = charSet;
                    }

                    break;
                case CharacterSet.Alternative:
                    if (_currentCharSet != charSet)
                    {
                        if (_escapeCodeWriter is null)
                            throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to change the character set.");
                        var escapeCode = _thisTerminalInfo.EnterAltCharsetMode ?? throw new InvalidOperationException("The terminal does not define the capability \"enter_alt_charset_mode\".");
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
