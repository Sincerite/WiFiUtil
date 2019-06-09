using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static WiFiUtil.NativeMethod;
using Base = WiFiUtil.BaseMethod;

namespace WiFiUtil
{
    class WifiClient
    {

        // Enumerate WiFi Interfaces

        public static IEnumerable<InterfaceInfo> EnumerateInterfaces()
        {
            return EnumerateInterfaces(null);
        }

        internal static IEnumerable<InterfaceInfo> EnumerateInterfaces(Base.WlanClient client)
        {
            using (var container = new DisposableContainer<Base.WlanClient>(client))
            {
                return Base.GetInterfaceInfoList(container.Content.Handle)
                    .Select(x => new InterfaceInfo(x));
            }
        }

    }
}
