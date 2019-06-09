using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFiUtil
{
    static class UI
    {
        public const byte Width = 120;
        public const byte Height = 48;

        private static readonly char[] BorderCharTable =
            {'\u2551', '\u2550', '\u2554', '\u2557', '\u255a', '\u255d', ' ', '[', ']'};

        static class Chars
        {
            public const int Vertical = 0;
            public const int Horizontal = 1;
            public const int TopLeft = 2;
            public const int TopRight = 3;
            public const int BottomLeft = 4;
            public const int BottomRight = 5;
            public const int Empty = 6;
            public const int BraceLeft = 7;
            public const int BraceRight = 8;
        }

        private const string Title = "WiFiUtil Alpha 0.0.1";

        // Previous state recovery vars
        private static bool PrevCursorVisible;
        private static string PrevTitle;
        private static int PrevWindowWidth, PrevWindowHeight;
        private static int PrevBufferWidth, PrevBufferHeight;
        private static ConsoleColor PrevForegroundColor;
        private static ConsoleColor PrevBackgroundColor;
        private static int PrevCursorSize;

        static public readonly object Lock = new object();

        static public void ScreenStart()
        {
            // Save state
            PrevCursorVisible = Console.CursorVisible;
            PrevTitle = Console.Title;
            PrevWindowWidth = Console.WindowWidth;
            PrevWindowHeight = Console.WindowHeight;
            PrevBufferWidth = Console.BufferWidth;
            PrevBufferHeight = Console.BufferHeight;
            PrevForegroundColor = Console.ForegroundColor;
            PrevBackgroundColor = Console.BackgroundColor;
            PrevCursorSize = Console.CursorSize;

            // Set new params
            Console.SetWindowSize(Width, Height);
            Console.SetBufferSize(Width, Height);
            Console.Title = Title;
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorSize = 100;
        }

        static public void ScreenRestore()
        {
            Console.CursorVisible = PrevCursorVisible;
            Console.Title = PrevTitle;
            Console.WindowWidth = PrevWindowWidth;
            Console.WindowHeight = PrevWindowHeight;
            Console.BufferWidth = PrevBufferWidth;
            Console.BufferHeight = PrevBufferHeight;
            Console.ForegroundColor = PrevForegroundColor;
            Console.BackgroundColor = PrevBackgroundColor;
            Console.CursorSize = PrevCursorSize;
        }

        static void CursorVisible(bool Visible)
        {
            Console.CursorSize = 100;
            if (Visible)
            {
                Console.CursorVisible = true;
            }
            else
            {
                Console.CursorVisible = false;
            }
        }

        static public ConsoleKey WaitForKey(bool Block)
        {
            ConsoleKeyInfo Ck;
            if (!Block && !Console.KeyAvailable) return ConsoleKey.NoName;
            Ck = Console.ReadKey(true);
            return Ck.Key;
        }

        static public byte WaitForDigit()
        {
            ConsoleKeyInfo Ck;

            while (true)
            {
                Ck = Console.ReadKey(true);
                switch (Ck.Key)
                {
                    case ConsoleKey.D0: return 0;
                    case ConsoleKey.D1: return 1;
                    case ConsoleKey.D2: return 2;
                    case ConsoleKey.D3: return 3;
                    case ConsoleKey.D4: return 4;
                    case ConsoleKey.D5: return 5;
                    case ConsoleKey.D6: return 6;
                    case ConsoleKey.D7: return 7;
                    case ConsoleKey.D8: return 8;
                    case ConsoleKey.D9: return 9;
                    case ConsoleKey.Escape: return 0;
                    case ConsoleKey.Enter: return 255;
                }
            }
        }

        static public void Write(string Text, ConsoleColor ForegroundColor)
        {
            ConsoleColor PrevForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ForegroundColor;
            Console.Write(Text);
            Console.ForegroundColor = PrevForegroundColor;
        }

        static public void Write(int aPosX, int aPosY, string aText)
        {
            Console.SetCursorPosition(aPosX, aPosY);
            Console.Write(aText);
        }
        static public void Write(int aPosX, int aPosY, string aText, ConsoleColor aForegroundColor)
        {
            ConsoleColor prevForeColor = Console.ForegroundColor;
            Console.ForegroundColor = aForegroundColor;
            Write(aPosX, aPosY, aText);
            Console.ForegroundColor = prevForeColor;
        }


        static public void DrawBorder(int aStartX, int aStartY, int aSizeX, int aSizeY, bool aEmpty, string aTitle)
        {
            byte k, x, y;
            ConsoleColor prevForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.SetCursorPosition(aStartX, aStartY);
            Console.Write(BorderCharTable[Chars.TopLeft]);
            for (k = 0; k < (aSizeX - 2); k++) Console.Write(BorderCharTable[Chars.Horizontal]);
            Console.Write(BorderCharTable[Chars.TopRight]);

            Console.SetCursorPosition(aStartX, aStartY + aSizeY - 1);
            Console.Write(BorderCharTable[Chars.BottomLeft]);
            for (k = 0; k < (aSizeX - 2); k++) Console.Write(BorderCharTable[Chars.Horizontal]);
            Console.Write(BorderCharTable[Chars.BottomRight]);

            for (k = 0; k < (aSizeY - 2); k++)
            {
                Console.SetCursorPosition(aStartX, aStartY + 1 + k);
                Console.Write(BorderCharTable[Chars.Vertical]);
                Console.SetCursorPosition(aStartX + aSizeX - 1, aStartY + 1 + k);
                Console.Write(BorderCharTable[Chars.Vertical]);
            }

            if (aTitle != null)
            {
                Console.SetCursorPosition(aStartX + 3, aStartY);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(BorderCharTable[Chars.BraceLeft] + " " + aTitle + " " + BorderCharTable[Chars.BraceRight]);
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }

            if (aEmpty)
            {
                char[] line = new char[aSizeX - 2];
                for (x = 0; x < (aSizeX - 2); x++) line[x] = BorderCharTable[Chars.Empty];
                for (y = 1; y < (aSizeY - 1); y++)
                {
                    Console.SetCursorPosition(aStartX + 1, aStartY + y);
                    Console.Write(line);
                }
            }

            Console.ForegroundColor = prevForeColor;
        }

        static public void DrawBorder(int aStartX, int aStartY, int aSizeX, int aSizeY, bool aEmpty)
        {
            DrawBorder(aStartX, aStartY, aSizeX, aSizeY, aEmpty, null);
        }
    }
}
