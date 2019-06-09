using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFiUtil
{
    class Views
    {
        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        static public class InterfaceSelector
        {
            private const byte Width = UI.Width - 4;
            private const byte Height = 42;
            private const byte PosX = 2;
            private const byte PosY = 5;

            static public byte Draw(byte LastKeyToPress, List<string> Interfaces)
            {
                byte k = 0;
                byte Selected;

                Console.Clear();
                UI.DrawBorder(PosX, PosY, Width, Height, false, "Interface Selector");
                UI.Write(PosX + 3, PosY + 2, "Detected following network adapters");

                foreach (string Interface in Interfaces)
                {
                    Console.SetCursorPosition(PosX + 3, PosY + 4 + k);
                    Console.Write((k + 1) + ". ");
                    Console.Write(Truncate(Interface, 120));

                    k++;
                }

                UI.Write(PosX + 3, PosY + 5 + k, "0. Exit");
                while (true)
                {
                    Selected = UI.WaitForDigit();
                    if (Selected <= LastKeyToPress) break;
                }
                return Selected;
            }
        }


        static public class MainMenu
        {
            private const byte Width = UI.Width - 4;
            private const byte Height = 42;
            private const byte PosX = 2;
            private const byte PosY = 5;

            static public void Draw()
            {
                int row, col;

                col = PosX + 4;
                row = PosY + 2;

                lock (UI.Lock)
                {
                    UI.DrawBorder(PosX, PosY, Width, Height, true, "Main Nav");
                    UI.Write(col, row++, "ESC - Quit");
                    row++;
                    row++;
                    UI.Write(col, row++, "W - Wifi Scanner");
                    row++;
                }
            }
        }

        static public class WifiScanner
        {
            private const byte Width = UI.Width - 4;
            private const byte Height = 42;
            private const byte PosX = 2;
            private const byte PosY = 5;

            static public void Draw()
            {
                int row, col;

                col = PosX + 4;
                row = PosY + 2;

                lock (UI.Lock)
                {
                    UI.DrawBorder(PosX, PosY, Width, Height, true, "Wifi Scanner");
                    UI.Write(col, row++, "ESC - Quit");
                    row++;
                    row++;
                    UI.Write(col, row++, ": )");
                    row++;
                }
            }
        }
    }
}
