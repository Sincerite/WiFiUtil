using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFiUtil {

    class Program
    {
        static void Main(string[] args)
        {
            UI.ScreenStart();
            try
            {
                InterfaceSelector.SelectInterface();
            }
            finally
            {
                UI.ScreenRestore();
            }
        }
    }


}
