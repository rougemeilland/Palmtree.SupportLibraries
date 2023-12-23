using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Palmtree.IO.Console
{
    partial class TinyConsole
    {
        private static class InterOpWindows
        {
            public static readonly UInt32 STD_INPUT_HANDLE = unchecked((UInt32)(-10));
            public static readonly UInt32 STD_OUTPUT_HANDLE = unchecked((UInt32)(-11));
            public static readonly UInt32 STD_ERROR_HANDLE = unchecked((UInt32)(-12));
            public static readonly IntPtr INVALID_HANDLE_VALUE = new(-1);
            public static readonly UInt32 ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
            public static readonly UInt32 ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200;

            [StructLayout(LayoutKind.Sequential)]
            public struct CONSOLE_SCREEN_BUFFER_INFO
            {
                public COORD dwSize;
                public COORD dwCursorPosition;
                public UInt16 wAttributes;
                public SMALL_RECT srWindow;
                public COORD dwMaximumWindowSize;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct COORD
            {
                public Int16 X;
                public Int16 Y;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct SMALL_RECT
            {
                public Int16 Left;
                public Int16 Top;
                public Int16 Right;
                public Int16 Bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct CONSOLE_CURSOR_INFO
            {
                public UInt32 dwSize;
                public Boolean bVisible;
            }

            [DllImport("kernel32.dll")]
            public extern static IntPtr GetStdHandle(UInt32 nStdHandle);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public extern static Boolean GetConsoleScreenBufferInfo(IntPtr hConsoleHandle, out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public extern static Boolean SetConsoleTextAttribute(IntPtr hConsoleHandle, UInt16 wAttributes);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public extern static Boolean SetConsoleCursorPosition(IntPtr hConsoleOutput, COORD cursorPosition);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public extern static Boolean GetConsoleCursorInfo(IntPtr hConsoleHandle, out CONSOLE_CURSOR_INFO lpConsoleCursorInfo);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public extern static Boolean SetConsoleCursorInfo(IntPtr hConsoleHandle, ref CONSOLE_CURSOR_INFO lpConsoleCursorInfo);

            [DllImport("kernel32.dll", EntryPoint = "FillConsoleOutputCharacterW", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public extern static Boolean FillConsoleOutputCharacter(IntPtr hConsoleHandle, Int16 cCharacter, UInt32 nLength, COORD dwWriteCoord, out UInt32 lpNumberOfCharsWritten);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public extern static Boolean FillConsoleOutputAttribute(IntPtr hConsoleHandle, UInt16 wAttribute, UInt32 nLength, COORD dwWriteCoord, out UInt32 lpNumberOfAttrsWritten);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public extern static Boolean GetConsoleMode(IntPtr hConsoleHandle, out UInt32 mode);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public extern static Boolean SetConsoleMode(IntPtr hConsoleHandle, UInt32 mode);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static UInt16 FromConsoleColorsToConsoleAttribute(ConsoleColor backgroundColor, ConsoleColor foregroundColor)
                => (UInt16)((((Int32)backgroundColor & 0x0f) << 4) | ((Int32)foregroundColor & 0x0f));

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static (ConsoleColor backgroundColor, ConsoleColor foregroundColor) FromConsoleAttributeToConsoleColors(UInt16 consoleAttribute)
                => ((ConsoleColor)((consoleAttribute >> 4) & 0x0f), (ConsoleColor)(consoleAttribute & 0x0f));
        }
    }
}
