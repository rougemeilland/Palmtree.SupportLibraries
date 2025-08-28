//#define USE_WIN32_API_TO_CONSOLE_OPERATION_FOR_WINDOWS
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

        private sealed class ConsoleOutputState
            : IDisposable
        {
            private Boolean _isDisposed;

            public ConsoleOutputState()
            {
                if (!System.Console.IsOutputRedirected)
                {
                    ConsoleOutputHandle =
                        OperatingSystem.IsWindows()
                        ? InterOpWindows.GetStdHandle(InterOpWindows.STD_OUTPUT_HANDLE)
                        : InterOpWindows.INVALID_HANDLE_VALUE;
                    ConsoleOutputFileNo =
                        OperatingSystem.IsWindows()
                        ? -1
                        : InterOpUnix.GetStandardFileNo(InterOpUnix.STANDARD_FILE_OUT);
                    ConsoleTextWriter = System.Console.Out;
                    EscapeCodeWriter = IsSupportedAnsiEscapeSequence(ConsoleOutputHandle) ? ConsoleTextWriter : null;
                    OutputExitAltCharsetMode(EscapeCodeWriter);
                }
                else if (!System.Console.IsErrorRedirected)
                {
                    ConsoleOutputHandle =
                        OperatingSystem.IsWindows()
                        ? InterOpWindows.GetStdHandle(InterOpWindows.STD_ERROR_HANDLE)
                        : InterOpWindows.INVALID_HANDLE_VALUE;
                    ConsoleOutputFileNo =
                        OperatingSystem.IsWindows()
                        ? -1
                        : InterOpUnix.GetStandardFileNo(InterOpUnix.STANDARD_FILE_ERR);
                    ConsoleTextWriter = System.Console.Error;
                    EscapeCodeWriter = IsSupportedAnsiEscapeSequence(ConsoleOutputHandle) ? ConsoleTextWriter : null;
                    OutputExitAltCharsetMode(EscapeCodeWriter);
                }
                else
                {
                    ConsoleOutputHandle = InterOpWindows.INVALID_HANDLE_VALUE;
                    ConsoleOutputFileNo = -1;
                    switch (_defaultTextWriter)
                    {
                        case ConsoleTextWriterType.StandardOutput:
                            ConsoleTextWriter = System.Console.Out;
                            EscapeCodeWriter = IsSupportedAnsiEscapeSequence(ConsoleOutputHandle) ? ConsoleTextWriter : null;
                            break;
                        case ConsoleTextWriterType.StandardError:
                            ConsoleTextWriter = System.Console.Error;
                            EscapeCodeWriter = IsSupportedAnsiEscapeSequence(ConsoleOutputHandle) ? ConsoleTextWriter : null;
                            break;
                        default:
                            ConsoleTextWriter = TextWriter.Null;
                            EscapeCodeWriter = null;
                            break;
                    }

                    OutputExitAltCharsetMode(EscapeCodeWriter);
                }

                static void OutputExitAltCharsetMode(TextWriter? escapeCodeWriter)
                {
                    if (!ImplementWithWin32Api && escapeCodeWriter is not null)
                    {
                        var exitAltCharsetMode = _thisTerminalInfo.Value.ExitAltCharsetMode;
                        if (exitAltCharsetMode is not null)
                            escapeCodeWriter.Write(exitAltCharsetMode);
                    }
                }
            }

            public IntPtr ConsoleOutputHandle { get; }
            public Int32 ConsoleOutputFileNo { get; }
            public TextWriter ConsoleTextWriter { get; }
            public TextWriter? EscapeCodeWriter { get; }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(Boolean disposing)
            {
                if (!_isDisposed)
                {
                    if (disposing)
                    {
                        ConsoleTextWriter.Dispose();
                        EscapeCodeWriter?.Dispose();
                    }

                    _isDisposed = true;
                }
            }
        }

        private const String _NATIVE_METHOD_DLL_NAME = "Palmtree.IO.Console.Native";
#if USE_WIN32_API_TO_CONSOLE_OPERATION_FOR_WINDOWS
        private const Boolean _useAnsiEscapeCodeEvenOnWindows = false;
#else
        private const Boolean _useAnsiEscapeCodeEvenOnWindows = true;
#endif
        private const Char _alternativeCharacterSetMapMinimumKey = '\u0020';
        private const Char _alternativeCharacterSetMapMaximumKey = '\u007e';

        private static readonly NativeDllNameResolver _dllNameResolver = new();
        private static readonly Object _lockObject = new();
        private static readonly ConsoleColor _defaultBackgrouongColor = System.Console.BackgroundColor;
        private static readonly ConsoleColor _defaultForegrouongColor = System.Console.ForegroundColor;
        private static readonly IResettableLazyValue<TerminalInfo> _thisTerminalInfo = LazyValue.CreateResettable(() => TerminalInfo.GetTerminalInfo(true) ?? throw new InvalidOperationException("Terminal information not found."));
        private static readonly IResettableLazyValue<ConsoleOutputState> _consoleOutputState = LazyValue.CreateResettable(() => new ConsoleOutputState(), o => o.Dispose());
        private static readonly ILazyValue<Char[]> _alternativeCharacterSetMap = LazyValue.Create(EnsureAlternativeCharacterSetMap);

        private static ConsoleColor _currentBackgrouongColor = System.Console.BackgroundColor;
        private static ConsoleColor _currentForegrouongColor = System.Console.ForegroundColor;
        private static CharacterSet _currentCharSet = CharacterSet.Primary;
        private static ConsoleTextWriterType _defaultTextWriter = ConsoleTextWriterType.None;
        private static Stream _standardInputBinaryStream;
        private static Stream _standardOutputBinaryStream;
        private static Stream _standardErrorBinaryStream;
        private static ISequentialInputByteStream _standardInputSequenrialByteStream;
        private static ISequentialOutputByteStream _standardOutputSequenrialByteStream;
        private static ISequentialOutputByteStream _standardErrorSequenrialByteStream;

        static TinyConsole()
        {
            NativeLibrary.SetDllImportResolver(typeof(InterOpUnix).Assembly, _dllNameResolver.ResolveDllName);

            _standardInputBinaryStream = System.Console.OpenStandardInput();
            _standardOutputBinaryStream = System.Console.OpenStandardOutput();
            _standardErrorBinaryStream = System.Console.OpenStandardError();
            _standardInputSequenrialByteStream = _standardInputBinaryStream.AsInputByteStream();
            _standardOutputSequenrialByteStream = _standardOutputBinaryStream.AsOutputByteStream();
            _standardErrorSequenrialByteStream = _standardErrorBinaryStream.AsOutputByteStream();
            System.Console.SetIn(CreateConsoleTextReader(_standardInputBinaryStream));
            System.Console.SetOut(CreateConsoleTextWriter(_standardOutputBinaryStream));
            System.Console.SetError(CreateConsoleTextWriter(_standardErrorBinaryStream));
        }

        #region DefaultTextWriter

        /// <summary>
        /// 標準出力および標準エラー出力がともにリダイレクトされている場合の既定の出力先を取得または設定します。
        /// </summary>
        /// <value>
        /// テキストの出力先を示す <see cref="ConsoleTextWriterType"/> 値です。
        /// 既定値は <see cref="ConsoleTextWriterType.None"/> です。
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
                    _consoleOutputState.Reset();
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

            set
            {
                var standardInputReader = System.Console.In;
                System.Console.InputEncoding = value;
                System.Console.SetIn(CreateConsoleTextReader(_standardInputBinaryStream));
                standardInputReader.Dispose();
            }
        }

        /// <summary>
        /// コンソールが出力内容の書き込み時に使用するエンコーディングを取得または設定します。
        /// </summary>
        public static Encoding OutputEncoding
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => System.Console.OutputEncoding;

            set
            {
                var standardOutputWriter = System.Console.Out;
                var standardErrorWriter = System.Console.Error;
                standardOutputWriter.Flush();
                standardErrorWriter.Flush();
                System.Console.OutputEncoding = value;
                System.Console.SetOut(CreateConsoleTextWriter(_standardOutputBinaryStream));
                System.Console.SetError(CreateConsoleTextWriter(_standardErrorBinaryStream));
                _consoleOutputState.Reset();
                standardOutputWriter.Dispose();
                standardErrorWriter.Dispose();
            }
        }

        #endregion

        #region StandatdInput / StandardOutput / StandardError

        /// <summary>
        /// 標準入力ストリームである <see cref="ISequentialInputByteStream"/> オブジェクトを取得または設定します。
        /// </summary>
        public static ISequentialInputByteStream StandatdInput
        {
            get
            {
                lock (_lockObject)
                {
                    return _standardInputSequenrialByteStream;
                }
            }

            set
            {
                lock (_lockObject)
                {
                    var originalStream = System.Console.In;
                    _standardInputSequenrialByteStream = value;
                    _standardInputBinaryStream = value.AsDotNetStream(true);
                    System.Console.SetIn(CreateConsoleTextReader(_standardInputBinaryStream));
                    originalStream.Dispose();
                }
            }
        }

        /// <summary>
        /// 標準出力ストリームである <see cref="ISequentialOutputByteStream"/> オブジェクトを取得します。
        /// </summary>
        public static ISequentialOutputByteStream StandardOutput
        {
            get
            {
                lock (_lockObject)
                {
                    return _standardOutputSequenrialByteStream;
                }
            }

            set
            {
                lock (_lockObject)
                {
                    var originalStream = System.Console.Out;
                    _standardOutputSequenrialByteStream = value;
                    _standardOutputBinaryStream = value.AsDotNetStream(true);
                    System.Console.SetOut(CreateConsoleTextWriter(_standardOutputBinaryStream));
                    originalStream.Dispose();
                    _consoleOutputState.Reset();
                }
            }
        }

        /// <summary>
        /// 標準エラー出力ストリームである <see cref="ISequentialOutputByteStream"/> オブジェクトを取得します。
        /// </summary>
        public static ISequentialOutputByteStream StandardError
        {
            get
            {
                lock (_lockObject)
                {
                    return _standardErrorSequenrialByteStream;
                }
            }

            set
            {
                lock (_lockObject)
                {
                    var originalStream = System.Console.Error;
                    _standardErrorSequenrialByteStream = value;
                    _standardErrorBinaryStream = value.AsDotNetStream(true);
                    System.Console.SetError(CreateConsoleTextWriter(_standardErrorBinaryStream));
                    originalStream.Dispose();
                    _consoleOutputState.Reset();
                }
            }
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
                var consoleOutputState = _consoleOutputState.Value;
                if (ImplementWithWin32Api && consoleOutputState.ConsoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
                {
                    if (!InterOpWindows.GetConsoleScreenBufferInfo(consoleOutputState.ConsoleOutputHandle, out var consoleInfo))
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
                var consoleOutputState = _consoleOutputState.Value;
                if (ImplementWithWin32Api && consoleOutputState.ConsoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
                {
                    if (!InterOpWindows.GetConsoleScreenBufferInfo(consoleOutputState.ConsoleOutputHandle, out var consoleInfo))
                        throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                    (_, _currentForegrouongColor) = InterOpWindows.FromConsoleAttributeToConsoleColors(consoleInfo.wAttributes);
                    return _currentForegrouongColor;
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
            var consoleOutputState = _consoleOutputState.Value;
            if (ImplementWithWin32Api && consoleOutputState.ConsoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
                var consoleAtrribute = InterOpWindows.FromConsoleColorsToConsoleAttribute(_defaultBackgrouongColor, _defaultForegrouongColor);
                if (!InterOpWindows.SetConsoleTextAttribute(consoleOutputState.ConsoleOutputHandle, consoleAtrribute))
                    throw new InvalidOperationException("Failed to set console text attribute.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                _currentBackgrouongColor = _defaultBackgrouongColor;
                _currentForegrouongColor = _defaultForegrouongColor;
            }
            else
            {
                var resetColorEscapeCode = _thisTerminalInfo.Value.ResetColor;
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
                        _thisTerminalInfo.Value.SetTitle(value)
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
            var consoleOutputState = _consoleOutputState.Value;
            var thisTerminalInfo = _thisTerminalInfo.Value;
            // Windows でのみ "System.Console.Beep()" を呼び出しているのは、UNIX 系の実装ではエスケープコードの出力先が標準出力に固定されているから。
            if (ImplementWithWin32Api)
                System.Console.Beep();
            else if (consoleOutputState.EscapeCodeWriter is not null && thisTerminalInfo.Bell is not null)
                WriteAnsiEscapeCodeToConsole(thisTerminalInfo.Bell, () => { });
            else
                consoleOutputState.EscapeCodeWriter?.Write('\a');
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
            var consoleOutputState = _consoleOutputState.Value;
            var thisTerminalInfo = _thisTerminalInfo.Value;
            if (ImplementWithWin32Api && consoleOutputState.ConsoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
                if (!InterOpWindows.GetConsoleScreenBufferInfo(consoleOutputState.ConsoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                if (!InterOpWindows.SetConsoleCursorPosition(consoleOutputState.ConsoleOutputHandle, new InterOpWindows.COORD { X = 0, Y = 0 }))
                    throw new InvalidOperationException("Failed to set cursor position.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                ClearScreenCore(0, 0, consoleInfo.dwSize.X * consoleInfo.dwSize.Y, consoleInfo.wAttributes);

                // Windows ターミナルなどのターミナルでは Win32 API のみではコンソールバッファが消去されないため、エスケープコードも併用する。
                var eraseScrollBufferEscapeSequence = thisTerminalInfo.EraseScrollBuffer;
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
                    thisTerminalInfo.ClearBuffer
                        ?? thisTerminalInfo.ClearScreen
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
            var consoleOutputState = _consoleOutputState.Value;
            if (ImplementWithWin32Api && consoleOutputState.ConsoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
                if (!InterOpWindows.GetConsoleScreenBufferInfo(consoleOutputState.ConsoleOutputHandle, out var consoleInfo))
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
                        if (!InterOpWindows.SetConsoleCursorPosition(consoleOutputState.ConsoleOutputHandle, new InterOpWindows.COORD { X = consoleInfo.srWindow.Left, Y = consoleInfo.srWindow.Top }))
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
                                if (!InterOpWindows.SetConsoleCursorPosition(consoleOutputState.ConsoleOutputHandle, new InterOpWindows.COORD { X = 0, Y = 0 }))
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
                            var eraseScrollBufferEscapeSequence = _thisTerminalInfo.Value.EraseScrollBuffer;
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
                        ConsoleEraseMode.FromCursorToEndOfScreen => _thisTerminalInfo.Value.ClrEos ?? throw new InvalidOperationException("This terminal does not support the capability \"clr_eos\"."),
                        ConsoleEraseMode.FromBeggingOfScreenToCursor => _thisTerminalInfo.Value.EraseInDisplay1 ?? throw new InvalidOperationException("This terminal does not support the capability to erase from the beginning of the screen to the cursor position."),
                        ConsoleEraseMode.EntireScreen => _thisTerminalInfo.Value.ClearScreen ?? throw new InvalidOperationException("This terminal does not support the capability \"clear_screen\"."),
                        ConsoleEraseMode.EntireConsoleBuffer => _thisTerminalInfo.Value.ClearBuffer ?? throw new InvalidOperationException("This terminal doesn't support the capability to clear the console buffer."),
                        ConsoleEraseMode.FromCursorToEndOfLine => _thisTerminalInfo.Value.ClrEol ?? throw new InvalidOperationException("This terminal does not support the capability \"clr_eol\"."),
                        ConsoleEraseMode.FromBeggingOfLineToCursor => _thisTerminalInfo.Value.ClrBol ?? throw new InvalidOperationException("This terminal does not support the capability \"clr_bol\"."),
                        ConsoleEraseMode.EntireLine => _thisTerminalInfo.Value.EraseInLine2 ?? throw new InvalidOperationException("This terminal does not support the capability to erase entire lines."),
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

                var consoleOutputState = _consoleOutputState.Value;
                if (ImplementWithWin32Api && consoleOutputState.ConsoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
                {
                    if (!InterOpWindows.GetConsoleCursorInfo(consoleOutputState.ConsoleOutputHandle, out var cursorInfo))
                        throw new InvalidOperationException("Failed to get console cursor info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                    (cursorInfo.bVisible, cursorInfo.dwSize) =
                        value switch
                        {
                            ConsoleCursorVisiblity.Invisible => (false, 1U),
                            ConsoleCursorVisiblity.NormalMode => (true, 25U),
                            ConsoleCursorVisiblity.HighVisibilityMode => (true, 100U),
                            _ => throw Validation.GetFailErrorException(),
                        };
                    if (!InterOpWindows.SetConsoleCursorInfo(consoleOutputState.ConsoleOutputHandle, ref cursorInfo))
                        throw new InvalidOperationException("Failed to set console cursor info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));
                }
                else
                {
                    // 標準出力及び標準エラー出力が共にリダイレクトされている場合でもエラーとはしない。
                    WriteAnsiEscapeCodeToConsole(
                        value switch
                        {
                            ConsoleCursorVisiblity.Invisible => _thisTerminalInfo.Value.CursorInvisible,
                            ConsoleCursorVisiblity.NormalMode => _thisTerminalInfo.Value.CursorNormal,
                            ConsoleCursorVisiblity.HighVisibilityMode => _thisTerminalInfo.Value.CursorVisible ?? _thisTerminalInfo.Value.CursorNormal,
                            _ => throw Validation.GetFailErrorException(),
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
            => _thisTerminalInfo.Value
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

        private static Char[] EnsureAlternativeCharacterSetMap()
        {
            var acs = _thisTerminalInfo.Value.AcsChars;
            if (acs is not null)
            {
                var __alternativeCharacterSetMap = new Char[_alternativeCharacterSetMapMaximumKey - _alternativeCharacterSetMapMinimumKey + 1];
                Array.Fill(__alternativeCharacterSetMap, '\u0000');
                for (var index = 0; index + 1 < acs.Length; index += 2)
                    __alternativeCharacterSetMap[acs[index] - _alternativeCharacterSetMapMinimumKey] = acs[index + 1];
                return __alternativeCharacterSetMap;
            }
            else
            {
                return [];
            }
        }

        private static TextReader CreateConsoleTextReader(Stream inStream)
        {
#if DEBUG
            Validation.Assert(inStream.CanRead == true);
#endif
            return
                inStream == Stream.Null
                ? TextReader.Null
                : TextReader.Synchronized(inStream.AsTextReader(System.Console.InputEncoding.WithoutPreamble(), false, 4096, true));
        }

        private static TextWriter CreateConsoleTextWriter(Stream outStream)
        {
#if DEBUG
            Validation.Assert(outStream.CanWrite == true);
#endif
            return
                outStream == Stream.Null
                ? TextWriter.Null
                : TextWriter.Synchronized(outStream.AsTextWriter(System.Console.OutputEncoding.WithoutPreamble(), 256, true, true));
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
                throw new ApplicationException("Failed to get console mode.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

            if ((mode & InterOpWindows.ENABLE_VIRTUAL_TERMINAL_PROCESSING) != 0)
            {
                // コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグが立っている (既にエスケープコードを解釈可能である) 場合
                return true;
            }

            // コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグが立っていない場合

            // コンソールモードに ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグをセットする
            if (!InterOpWindows.SetConsoleMode(consoleOutputHandle, mode | InterOpWindows.ENABLE_VIRTUAL_TERMINAL_PROCESSING))
                throw new ApplicationException("Failed to set console mode.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

            // 再度、コンソールモードの ENABLE_VIRTUAL_TERMINAL_PROCESSING フラグを調べる
            if (!InterOpWindows.GetConsoleMode(consoleOutputHandle, out mode))
                throw new ApplicationException("Failed to get console mode.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

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
            var consoleOutputState = _consoleOutputState.Value;
            if (ImplementWithWin32Api && consoleOutputState.ConsoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
                if (!InterOpWindows.GetConsoleScreenBufferInfo(consoleOutputState.ConsoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                var consoleAtrribute =
                    InterOpWindows.FromConsoleColorsToConsoleAttribute(
                        value,
                        InterOpWindows.FromConsoleAttributeToConsoleColors(consoleInfo.wAttributes).foregroundColor);
                if (!InterOpWindows.SetConsoleTextAttribute(consoleOutputState.ConsoleOutputHandle, consoleAtrribute))
                    throw new InvalidOperationException("Failed to set console text attribute.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));
            }
            else
            {
                var thisTerminalInfo = _thisTerminalInfo.Value;
                // 標準出力及び標準エラー出力が共にリダイレクトされている場合でもエラーとはしない。
                WriteAnsiEscapeCodeToConsole(
                    thisTerminalInfo.SetABackground(value.ToAnsiColor16())
                    ?? thisTerminalInfo.SetBackground(value.ToColor8())
                    ?? throw new InvalidOperationException("This terminal does not define the capability to change the text background color."),
                    () => { });

            }

            _currentBackgrouongColor = value;
        }

        private static void SetForegroundColorCore(ConsoleColor value)
        {
            var consoleOutputState = _consoleOutputState.Value;
            if (ImplementWithWin32Api && consoleOutputState.ConsoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
                if (!InterOpWindows.GetConsoleScreenBufferInfo(consoleOutputState.ConsoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

                var consoleAtrribute =
                    InterOpWindows.FromConsoleColorsToConsoleAttribute(
                        InterOpWindows.FromConsoleAttributeToConsoleColors(consoleInfo.wAttributes).backgroundColor,
                        value);
                if (!InterOpWindows.SetConsoleTextAttribute(consoleOutputState.ConsoleOutputHandle, consoleAtrribute))
                    throw new InvalidOperationException("Failed to set console text attribute.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

            }
            else
            {
                var thisTerminalInfo = _thisTerminalInfo.Value;
                // 標準出力及び標準エラー出力が共にリダイレクトされている場合でもエラーとはしない。
                WriteAnsiEscapeCodeToConsole(
                    thisTerminalInfo.SetAForeground(value.ToAnsiColor16())
                        ?? thisTerminalInfo.SetForeground(value.ToColor8())
                        ?? throw new InvalidOperationException("This terminal does not define the capability to change the foreground color of characters."),
                    () => { });
            }

            _currentForegrouongColor = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (Int32 windowWidth, Int32 windowHeight) GetWindowSizeCore()
        {
            var consoleOutputState = _consoleOutputState.Value;
            if (OperatingSystem.IsWindows())
            {
                return
                    consoleOutputState.ConsoleOutputHandle == InterOpWindows.INVALID_HANDLE_VALUE
                    ? throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to get window size.")
                    : !InterOpWindows.GetConsoleScreenBufferInfo(consoleOutputState.ConsoleOutputHandle, out var consoleInfo)
                    ? throw new InvalidOperationException("Failed to get console screen buffer info.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()))
                    : ((Int32 windowWidth, Int32 windowHeight))(consoleInfo.srWindow.Right - consoleInfo.srWindow.Left + 1, consoleInfo.srWindow.Bottom - consoleInfo.srWindow.Top + 1);
            }
            else
            {
                if (consoleOutputState.ConsoleOutputFileNo < 0)
                    throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to get window size.");

                if (InterOpUnix.GetWindowSize(consoleOutputState.ConsoleOutputFileNo, out var windowSize, out _) == 0)
                    return (windowSize.Col, windowSize.Row);
                return (_thisTerminalInfo.Value.Columns ?? throw new InvalidOperationException("The terminal does not have the capability \"columns\" defined."), _thisTerminalInfo.Value.Lines ?? throw new InvalidOperationException("The terminal does not have the capability \"lines\" defined."));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void MoveCursorVertically(Int32 n, Action errorHandler)
        {
            var consoleOutputState = _consoleOutputState.Value;
            if (ImplementWithWin32Api && consoleOutputState.ConsoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
                if (!InterOpWindows.GetConsoleScreenBufferInfo(consoleOutputState.ConsoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console buffer info.", Marshal.GetExceptionForHR(Marshal.GetLastWin32Error()));

                if (!InterOpWindows.SetConsoleCursorPosition(
                    consoleOutputState.ConsoleOutputHandle,
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
                        _thisTerminalInfo.Value.ParmDownCursor(n) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_down_cursor\"."),
                        errorHandler);
                }
                else if (n < 0)
                {
                    WriteAnsiEscapeCodeToConsole(
                        _thisTerminalInfo.Value.ParmUpCursor(checked(-n)) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_up_cursor\"."),
                        errorHandler);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void MoveCursorHorizontally(Int32 n, Action errorHandler)
        {
            var consoleOutputState = _consoleOutputState.Value;
            if (ImplementWithWin32Api && consoleOutputState.ConsoleOutputHandle != InterOpWindows.INVALID_HANDLE_VALUE)
            {
                if (!InterOpWindows.GetConsoleScreenBufferInfo(consoleOutputState.ConsoleOutputHandle, out var consoleInfo))
                    throw new InvalidOperationException("Failed to get console buffer info.", Marshal.GetExceptionForHR(Marshal.GetLastWin32Error()));

                if (!InterOpWindows.SetConsoleCursorPosition(
                    consoleOutputState.ConsoleOutputHandle,
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
                        _thisTerminalInfo.Value.ParmRightCursor(n) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_right_cursor\"."),
                        errorHandler);
                }
                else if (n < 0)
                {
                    WriteAnsiEscapeCodeToConsole(
                        _thisTerminalInfo.Value.ParmLeftCursor(checked(-n)) ?? throw new InvalidOperationException("This terminal does not define the capability \"parm_left_cursor\"."),
                        errorHandler);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteAnsiEscapeCodeToConsole(String ansiEscapeCode, Action errorHandler)
        {
            var consoleOutputState = _consoleOutputState.Value;
            if (consoleOutputState.EscapeCodeWriter is not null)
                consoleOutputState.EscapeCodeWriter.Write(ansiEscapeCode);
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
            var consoleOutputState = _consoleOutputState.Value;
            if (!InterOpWindows.FillConsoleOutputCharacter(consoleOutputState.ConsoleOutputHandle, (Int16)' ', (UInt32)length, startPosition, out _))
                throw new InvalidOperationException("Failed to clear console buffer characters.", Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error()));

            if (!InterOpWindows.FillConsoleOutputAttribute(consoleOutputState.ConsoleOutputHandle, attribute, (UInt32)length, startPosition, out _))
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
                        var consoleOutputState = _consoleOutputState.Value;
                        if (consoleOutputState.EscapeCodeWriter is null)
                            throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to change the character set.");
                        var escapeCode = _thisTerminalInfo.Value.ExitAltCharsetMode ?? throw new InvalidOperationException("The terminal does not define the capability \"exit_alt_charset_mode\".");
                        consoleOutputState.EscapeCodeWriter.Write(escapeCode);
                        _currentCharSet = charSet;
                    }

                    break;
                case CharacterSet.Alternative:
                    if (_currentCharSet != charSet)
                    {
                        var consoleOutputState = _consoleOutputState.Value;
                        if (consoleOutputState.EscapeCodeWriter is null)
                            throw new InvalidOperationException("Since both standard output and standard error output are redirected, it is not possible to change the character set.");
                        var escapeCode = _thisTerminalInfo.Value.EnterAltCharsetMode ?? throw new InvalidOperationException("The terminal does not define the capability \"enter_alt_charset_mode\".");
                        consoleOutputState.EscapeCodeWriter.Write(escapeCode);
                        _currentCharSet = charSet;
                    }

                    break;
                default:
                    throw Validation.GetFailErrorException();
            }
        }

        #endregion
    }
}
