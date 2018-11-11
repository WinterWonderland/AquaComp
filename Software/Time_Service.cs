using Microsoft.SPOT;
using Microsoft.SPOT.Time;
using System;
using System.Net;
using System.Threading;

namespace AquaComp
{
    static class Time_Service
    {
        internal static void Initialize()
        {
            Debug.Print("Time Service initialized.");
        }

        private const string NPT_Server_1 = "0.de.pool.ntp.org";
        private const string NPT_Server_2 = "3.de.pool.ntp.org";
        private const int localTimeZone = 1 * 60;
        private const int RefreshTime = 60 * 60;
        private static bool isStarted = false;
        public static bool isTimeSet = false;

        private static Timer displayTimeTimer = new Timer(displayTimeTimer_Tick, null, 1000, 500);

        internal static void Start()
        {
            if (!isStarted)
            {
                TimeServiceSettings timeServiceSettings = new TimeServiceSettings();
                timeServiceSettings.AutoDayLightSavings = true;
                timeServiceSettings.ForceSyncAtWakeUp = true;
                timeServiceSettings.RefreshTime = RefreshTime;
                timeServiceSettings.PrimaryServer = Dns.GetHostEntry(NPT_Server_1).AddressList[0].GetAddressBytes();
                timeServiceSettings.AlternateServer = Dns.GetHostEntry(NPT_Server_2).AddressList[0].GetAddressBytes();

                TimeService.Settings = timeServiceSettings;
                TimeService.SetTimeZoneOffset(localTimeZone);
                TimeService.SystemTimeChanged += onSystemTimeChanged;
                TimeService.TimeSyncFailed += onTimeSyncFailed;
                TimeService.Start();

                isStarted = true;
                Debug.Print("Time Service started.");
            }
        }

        internal static void Stop()
        {
            isStarted = false;
            TimeService.Stop();
            Debug.Print("Time Service stopped.");
        }

        private static void onSystemTimeChanged(object sender, SystemTimeChangedEventArgs e)
        {
            Debug.Print("System time changed by Time Service: " + DateTime.Now.ToString());
            isTimeSet = true;
        }

        private static void onTimeSyncFailed(object sender, TimeSyncFailedEventArgs e)
        {
            Debug.Print("Update the system time by Time Service failed.");
        }

        private static void displayTimeTimer_Tick(object state)
        {
            if (isTimeSet)
            {
                Window_Manager.UpdateTime_Tick(DateTime.Now);
            }
            else
            {
                Window_Manager.UpdateTime_Tick(new DateTime(0));
            }

        }
    }
}