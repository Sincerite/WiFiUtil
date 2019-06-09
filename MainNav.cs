using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WiFiUtil
{
    class MainNav
    {


        static public void MainScreen()
        {
            ConsoleKey Key;

            Console.Clear();
            Thread StatusTop = new Thread(() =>  {} );
            StatusTop.Start();
            Views.MainMenu.Draw();

            do
            {
                Key = UI.WaitForKey(false);
                switch (Key)
                {
                    case ConsoleKey.Escape:
                        StatusTop.Abort();
                        return;
                    case ConsoleKey.W:
                        Views.WifiScanner.Draw();
                        break;

                }

            }
            while (true);
        }

    }
}
