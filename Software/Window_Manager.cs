using AquaComp.Windows;
using GHI.Glide;
using GHI.Pins;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System;
using System.Threading;
using Gadgeteer.Modules.GHIElectronics;

namespace AquaComp
{
    static class Window_Manager
    {
        internal const int Display_Width = 480;
        internal const int Display_Heigth = 272;

        internal static Window_Main window_Main;
        internal static Window_Calibration window_Calibration;
        internal static Window_Fertilize window_Fertilize;
        internal static Window_PumpProperties window_PumpProperties;
        internal static Window_PumpSchedule window_PumpSchedule;
        internal static Window_AWC_Edit window_AWC_Edit;

        internal static IWindow activeWindow;

        private static string[] backgroundImages;
        private const byte MaxBackgroundImageIndex = 39;
        private static Timer backgroundImageTimer;
        private const int BackgroundImageTimer_TickTime = 1 * 60 * 1000;
        private static Random random = new Random();

        private static OutputPort backlight = new OutputPort(G120.P1_19, true);
        private static Timer backlightTimer = new Timer(backlightTimer_Tick, null, Timeout.Infinite, Timeout.Infinite);
        private const int backlightTimer_TickTime = 5 * 60 * 1000;

        internal static void Initialize()
        {
            GlideTouch.Initialize();
            GlideTouch.TouchDownEvent += new TouchEventHandler(Display_TouchDown);
            Glide.Keyboard.TapKeyEvent += Keyboard_TapKeyEvent;

            backgroundImages = SD_Card.GetAllFilesInFolder("Bilder");

            window_Main = new Window_Main();
            window_Calibration = new Window_Calibration();
            window_Fertilize = new Window_Fertilize();
            window_PumpProperties = new Window_PumpProperties();
            window_PumpSchedule = new Window_PumpSchedule();
            window_AWC_Edit = new Window_AWC_Edit();
            
            backgroundImageTimer = new Timer(backgroundImageTimer_Tick, null, 0, BackgroundImageTimer_TickTime);
            backlightTimer_Restart();

            Debug.Print("Window Manager initialized.");
        }

        private static void backlightTimer_Tick(object state)
        {
            backlight.Write(false);
            Debug.Print("Display backlight turned off.");
        }

        private static void backlightTimer_Restart()
        {
            backlightTimer.Change(backlightTimer_TickTime, Timeout.Infinite);
        }

        private static void backgroundImageTimer_Tick(object state)
        {
            if (Glide.MainWindow != null)
            {
                byte actualBackgroundImageIndex = (byte)(random.Next(MaxBackgroundImageIndex - 1) + 1);
                Bitmap backgroundImage = new Bitmap(SD_Card.ReadFile("Bilder\\BackImage_" + actualBackgroundImageIndex.ToString() + ".jpg"), Bitmap.BitmapImageType.Jpeg);
                Glide.MainWindow.BackImage = backgroundImage;
                Glide.MainWindow.Invalidate();

                Debug.Print("Background Image changed.");
            }
        }

        private static void Display_TouchDown(object sender, TouchEventArgs e)
        {
            if (backlight.Read())
            {
                backlightTimer_Restart();
                e.Propagate = true;
            }
            else
            {
                backlight.Write(true);
                Debug.Print("Display backlight turned on.");
                backlightTimer_Restart();
                e.Propagate = false;
            }
            
            Debug.Print("Display touched: x = " + e.Point.X + " ; y = " + e.Point.Y.ToString());
        }

        internal static void changeWindow(IWindow window)
        {
            if (Glide.MainWindow != null)
            {
                if (Glide.MainWindow.BackImage != null)
                {
                    window.Window.BackImage = Glide.MainWindow.BackImage;
                }
            }

            activeWindow = window;
            Glide.MainWindow = window.Window;

            if (Glide.MainWindow.BackImage == null)
            {
                backgroundImageTimer_Tick(null);
            }

            Debug.Print("Active window changed.");
        }

        internal static void UpdateTime_Tick(DateTime dateTime)
        {
            if (!isKeyboardOpen)
            {
                string time;

                if (dateTime.Ticks != 0)
                {
                    time = dateTime.Hour.ToString("D2") + ":" + dateTime.Minute.ToString("D2");

                }
                else
                {
                    time = "--:--";
                }

                if (activeWindow != null)
                {
                    ((IWindow)activeWindow).UpdateTime(time);
                }
            }
        }

        private static bool isKeyboardOpen = false;

        internal static void GlideOpenKeyBoard(object sender)
        {
            isKeyboardOpen = true;
            Glide.OpenKeyboard(sender);
        }

        static void Keyboard_TapKeyEvent(object sender, TapKeyEventArgs e)
        {
            if (e.Value.ToUpper() == "RETURN")
            {
                isKeyboardOpen = false;
            }
        }
    }
}