using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace ONTUI
{
    class NetworkTests
    {
        private static List<NetworkInterface> staticNetworkInterfaces = new List<NetworkInterface>();

        public static Boolean GetPingResult(String address = "127.0.0.1", int timeout = 1000)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(address, timeout);
                pingable = (reply.Status == IPStatus.Success);
            }
            catch (PingException)
            {
                // discard
            }
            return pingable;
        }

        public static void CheckNetworkInterfacesForStaticIP()
        {
            staticNetworkInterfaces = UpdateNetworkInterfaceWithStaticIPList();
        }
        public static List<NetworkInterface> UpdateNetworkInterfaceWithStaticIPList()
        {
            List<NetworkInterface> list = new List<NetworkInterface>();

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType != NetworkInterfaceType.Ethernet)
                    continue;

                if (!item.Supports(NetworkInterfaceComponent.IPv4))
                    continue;

                if (item.GetIPProperties().DhcpServerAddresses.Count > 0)
                    continue;

                list.Add(item);
            }

            return list;
        }


    }
}
