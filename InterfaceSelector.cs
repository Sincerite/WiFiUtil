using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiFiUtil
{
    public static class InterfaceSelector
    {

        public static void SelectInterface()
        {
            List<string> Interfaces = new List<string>();
            ArrayList InterfacesAreAvailableList = new ArrayList(1);
            byte Selected;

            byte k = 0;

            try
            {
                foreach (var interfaceInfo in WifiClient.EnumerateInterfaces())
                {
                    Interfaces.Add(interfaceInfo.Description);
                }
            }
            catch
            {
                throw new AggregateException("Baba");
            }

            Selected = Views.InterfaceSelector.Draw(k, Interfaces);
            
        }

    }
}
