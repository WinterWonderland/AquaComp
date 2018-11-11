using GHI.Networking;
using GHI.Pins;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Net.NetworkInformation;
using System.Threading;

namespace AquaComp
{
    static class Ethernet
    {
        private static EthernetENC28J60 ethernet_Modul;

        internal static void Initialize()
        {
            ethernet_Modul = new EthernetENC28J60(SPI.SPI_module.SPI2, G120.P1_10, G120.P2_11, G120.P1_9);
            ethernet_Modul.Open();
            ethernet_Modul.EnableDhcp();
            ethernet_Modul.EnableDynamicDns();

            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;

            Debug.Print("Ethernet initialized.");
        }

        static void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            Debug.Print("Network available: "+ e.IsAvailable.ToString());

            if (e.IsAvailable)
            {
                Time_Service.Start();
            }
            else
            {
                Time_Service.Stop();
            }
        }

        static void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            Debug.Print("Network address changed: " + ethernet_Modul.IPAddress);
            Time_Service.Start();
        }
    }
}
