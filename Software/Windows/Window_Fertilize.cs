using GHI.Glide;
using GHI.Glide.UI;
using Microsoft.SPOT;
using System.Threading;
using System;

namespace AquaComp.Windows
{
    class Window_Fertilize : IWindow
    {
        #region Controls

        private GHI.Glide.Display.Window window;
        private TextBox textBox_Header;
        private TextBox textBox_Time;
        private TextBox textBox_Back;
        private Button button_Properties;
        private Button button_Schedule;
        private Button button_AwcSettings;
        private Button button_AwcRun;
        private TextBox textBox_Name_Pump1;
        private TextBox textBox_Name_Pump2;
        private TextBox textBox_Name_Pump3;
        private TextBox textBox_Name_Pump4;
        private CheckBox checkBox_Running_Pump1;
        private CheckBox checkBox_Running_Pump2;
        private CheckBox checkBox_Running_Pump3;
        private CheckBox checkBox_Running_Pump4;
        private TextBox textBox_NextRun_Pump1;
        private TextBox textBox_NextRun_Pump2;
        private TextBox textBox_NextRun_Pump3;
        private TextBox textBox_NextRun_Pump4;
        private TextBox textBox_RemainingTime_Pump1;
        private TextBox textBox_RemainingTime_Pump2;
        private TextBox textBox_RemainingTime_Pump3;
        private TextBox textBox_RemainingTime_Pump4;

        private void Initialize_Components()
        {
            // window
            window = new GHI.Glide.Display.Window("window_Fertilize", Window_Manager.Display_Width, Window_Manager.Display_Heigth);
            window.BackColor = Colors.White;

            // textBox_Header
            textBox_Header = new TextBox("textBox_Header", 255, 120, 20, 270, 32);
            textBox_Header.Text = "Fertilize";
            textBox_Header.TextAlign = 2;
            window.AddChild(textBox_Header);

            // textBox_Time
            textBox_Time = new TextBox("textBox_Time", 255, 410, 20, 50, 32);
            textBox_Time.Text = "--:--";
            textBox_Time.TextAlign = 2;
            window.AddChild(textBox_Time);

            // textBox_Back
            textBox_Back = new TextBox("button_Back", 255, 20, 20, 80, 32);
            textBox_Back.Text = "Back";
            textBox_Back.TextAlign = 2;
            textBox_Back.TapEvent += new OnTap(button_Back_TapEvent);
            window.AddChild(textBox_Back);

            // button_Properties
            button_Properties = new Button("button_Properties", 255, 20, 70, 80, 32);
            button_Properties.Text = "Properties";
            button_Properties.TapEvent += button_Properties_TapEvent;
            window.AddChild(button_Properties);

            // button_Schedule
            button_Schedule = new Button("button_Schedule", 255, 20, 120, 80, 32);
            button_Schedule.Text = "Schedule";
            button_Schedule.TapEvent += button_Schedule_TapEvent;
            window.AddChild(button_Schedule);

            // button_AwcSettings
            button_AwcSettings = new Button("button_AwcSettings", 255, 20, 170, 80, 32);
            button_AwcSettings.Text = "AWC Edit";
            button_AwcSettings.TapEvent += button_AwcSettings_TapEvent;
            window.AddChild(button_AwcSettings);

            // button_AwcRun
            button_AwcRun = new Button("button_AwcRun", 255, 20, 220, 80, 32);
            button_AwcRun.Text = "AWC Run";
            button_AwcRun.TapEvent += button_AwcRun_TapEvent;
            window.AddChild(button_AwcRun);

            // textBox_Name_Pump1
            textBox_Name_Pump1 = new TextBox("textBox_Name_Pump1", 255, 120, 70, 150, 32);
            textBox_Name_Pump1.Text = Fertilize_Manager.Pump1.Name;
            window.AddChild(textBox_Name_Pump1);

            // textBox_Name_Pump2
            textBox_Name_Pump2 = new TextBox("textBox_Name_Pump2", 255, 120, 120, 150, 32);
            textBox_Name_Pump2.Text = Fertilize_Manager.Pump2.Name;
            window.AddChild(textBox_Name_Pump2);

            // textBox_Name_Pump3
            textBox_Name_Pump3 = new TextBox("textBox_Name_Pump3", 255, 120, 170, 150, 32);
            textBox_Name_Pump3.Text = Fertilize_Manager.Pump3.Name;
            window.AddChild(textBox_Name_Pump3);

            // textBox_Name_Pump4
            textBox_Name_Pump4 = new TextBox("textBox_Name_Pump4", 255, 120, 220, 150, 32);
            textBox_Name_Pump4.Text = Fertilize_Manager.Pump4.Name;
            window.AddChild(textBox_Name_Pump4);

            // checkBox_Running_Pump1
            checkBox_Running_Pump1 = new CheckBox("checkBox_Running_Pump1", 255, 290, 70);
            checkBox_Running_Pump1.TapEvent += checkBox_Running_Pump1_TapEvent;
            window.AddChild(checkBox_Running_Pump1);

            // checkBox_Running_Pump2
            checkBox_Running_Pump2 = new CheckBox("checkBox_Running_Pump2", 255, 290, 120);
            checkBox_Running_Pump2.TapEvent += checkBox_Running_Pump2_TapEvent;
            window.AddChild(checkBox_Running_Pump2);

            // checkBox_Running_Pump3
            checkBox_Running_Pump3 = new CheckBox("checkBox_Running_Pump3", 255, 290, 170);
            checkBox_Running_Pump3.TapEvent += checkBox_Running_Pump3_TapEvent;
            window.AddChild(checkBox_Running_Pump3);

            // checkBox_Running_Pump4
            checkBox_Running_Pump4 = new CheckBox("checkBox_Running_Pump4", 255, 290, 220);
            checkBox_Running_Pump4.TapEvent += checkBox_Running_Pump4_TapEvent;
            window.AddChild(checkBox_Running_Pump4);

            // textBox_NextRun_Pump1
            textBox_NextRun_Pump1 = new TextBox("textBox_NextRun_Pump1", 255, 340, 70, 50, 32);
            textBox_NextRun_Pump1.Text = "--:--";
            textBox_NextRun_Pump1.TextAlign = 2;
            window.AddChild(textBox_NextRun_Pump1);

            // textBox_NextRun_Pump2
            textBox_NextRun_Pump2 = new TextBox("textBox_NextRun_Pump2", 255, 340, 120, 50, 32);
            textBox_NextRun_Pump2.Text = "--:--";
            textBox_NextRun_Pump2.TextAlign = 2;
            window.AddChild(textBox_NextRun_Pump2);

            // textBox_NextRun_Pump3
            textBox_NextRun_Pump3 = new TextBox("textBox_NextRun_Pump3", 255, 340, 170, 50, 32);
            textBox_NextRun_Pump3.Text = "--:--";
            textBox_NextRun_Pump3.TextAlign = 2;
            window.AddChild(textBox_NextRun_Pump3);

            // textBox_NextRun_Pump4
            textBox_NextRun_Pump4 = new TextBox("textBox_NextRun_Pump4", 255, 340, 220, 50, 32);
            textBox_NextRun_Pump4.Text = "--:--";
            textBox_NextRun_Pump4.TextAlign = 2;
            window.AddChild(textBox_NextRun_Pump4);

            // textBox_RemainingTime_Pump1
            textBox_RemainingTime_Pump1 = new TextBox("textBox_RemainingTime_Pump1", 255, 410, 70, 50, 32);
            textBox_RemainingTime_Pump1.Text = "--:--";
            textBox_RemainingTime_Pump1.TextAlign = 2;
            window.AddChild(textBox_RemainingTime_Pump1);

            // textBox_RemainingTime_Pump2
            textBox_RemainingTime_Pump2 = new TextBox("textBox_RemainingTime_Pump2", 255, 410, 120, 50, 32);
            textBox_RemainingTime_Pump2.Text = "--:--";
            textBox_RemainingTime_Pump2.TextAlign = 2;
            window.AddChild(textBox_RemainingTime_Pump2);

            // textBox_RemainingTime_Pump3
            textBox_RemainingTime_Pump3 = new TextBox("textBox_RemainingTime_Pump3", 255, 410, 170, 50, 32);
            textBox_RemainingTime_Pump3.Text = "--:--";
            textBox_RemainingTime_Pump3.TextAlign = 2;
            window.AddChild(textBox_RemainingTime_Pump3);

            // textBox_RemainingTime_Pump4
            textBox_RemainingTime_Pump4 = new TextBox("textBox_RemainingTime_Pump4", 255, 410, 220, 50, 32);
            textBox_RemainingTime_Pump4.Text = "--:--";
            textBox_RemainingTime_Pump4.TextAlign = 2;
            window.AddChild(textBox_RemainingTime_Pump4);
        }

        public GHI.Glide.Display.Window Window
        {
            get
            {
                return window;
            }
        }

        #endregion


        #region Constructors

        public Window_Fertilize()
        {
            Initialize_Components();

            Fertilize_Manager.PumpIsRunning_Changed += Fertilize_Manager_PumpIsRunning_Changed;
            Fertilize_Manager.PumpName_Changed += Fertilize_Manager_PumpName_Changed;
            timer_DisplayNextRuntime = new Timer(timer_DisplayNextRuntime_Tick, null, 1000, 500);
        }

        #endregion


        #region EventHandler

        private static void button_Back_TapEvent(object sender)
        {
            Window_Manager.changeWindow(Window_Manager.window_Main);
        }

        void button_Properties_TapEvent(object sender)
        {
            Window_Manager.changeWindow(Window_Manager.window_PumpProperties);
        }

        void button_Schedule_TapEvent(object sender)
        {
            Window_Manager.changeWindow(Window_Manager.window_PumpSchedule);
        }

        void button_AwcSettings_TapEvent(object sender)
        {
            Window_Manager.changeWindow(Window_Manager.window_AWC_Edit);
        }

        void button_AwcRun_TapEvent(object sender)
        {
            Fertilize_Manager.Pump1.Run(Fertilize_Manager.Pump1.AWC_Quantity);
            Fertilize_Manager.Pump2.Run(Fertilize_Manager.Pump2.AWC_Quantity);
            Fertilize_Manager.Pump3.Run(Fertilize_Manager.Pump3.AWC_Quantity);
            Fertilize_Manager.Pump4.Run(Fertilize_Manager.Pump4.AWC_Quantity);
        }

        void Fertilize_Manager_PumpIsRunning_Changed(object sender, PumpEventArgs e)
        {
            switch (e.Pump.PumpNumber)
            {
                case 1:
                    checkBox_Running_Pump1.Checked = e.Pump.IsRunning;
                    break;

                case 2:
                    checkBox_Running_Pump2.Checked = e.Pump.IsRunning;
                    break;

                case 3:
                    checkBox_Running_Pump3.Checked = e.Pump.IsRunning;
                    break;

                case 4:
                    checkBox_Running_Pump4.Checked = e.Pump.IsRunning;
                    break;
            }
        }

        void Fertilize_Manager_PumpName_Changed(object sender, PumpEventArgs e)
        {
            switch (e.Pump.PumpNumber)
            {
                case 1:
                    textBox_Name_Pump1.Text = e.Pump.Name;
                    break;

                case 2:
                    textBox_Name_Pump2.Text = e.Pump.Name;
                    break;

                case 3:
                    textBox_Name_Pump3.Text = e.Pump.Name;
                    break;

                case 4:
                    textBox_Name_Pump4.Text = e.Pump.Name;
                    break;
            }
        }

        void set_TextBox_NextFertilizeJob(Fertilize_Pump pump, TextBox textBox)
        {
            string nextStartTime = "--:--";
            Fertilize_Job job = pump.Get_Nearest_Fertilize_Job();

            if (job != null)
            {
                nextStartTime = job.StartTime.Hour.ToString("D2") + ":" + job.StartTime.Minute.ToString("D2");
            }

            textBox.Text = nextStartTime;
        }

        void checkBox_Running_Pump1_TapEvent(object sender)
        {
            Fertilize_Manager.Pump1.Toggle();
        }

        void checkBox_Running_Pump2_TapEvent(object sender)
        {
            Fertilize_Manager.Pump2.Toggle();
        }

        void checkBox_Running_Pump3_TapEvent(object sender)
        {
            Fertilize_Manager.Pump3.Toggle();
        }

        void checkBox_Running_Pump4_TapEvent(object sender)
        {
            Fertilize_Manager.Pump4.Toggle();
        }

        private Timer timer_DisplayNextRuntime;

        private void timer_DisplayNextRuntime_Tick(object state)
        {
            set_TextBox_NextFertilizeJob(Fertilize_Manager.Pump1, textBox_NextRun_Pump1);
            set_TextBox_RemainingTime(Fertilize_Manager.Pump1, textBox_RemainingTime_Pump1);
            set_TextBox_NextFertilizeJob(Fertilize_Manager.Pump2, textBox_NextRun_Pump2);
            set_TextBox_RemainingTime(Fertilize_Manager.Pump2, textBox_RemainingTime_Pump2);
            set_TextBox_NextFertilizeJob(Fertilize_Manager.Pump3, textBox_NextRun_Pump3);
            set_TextBox_RemainingTime(Fertilize_Manager.Pump3, textBox_RemainingTime_Pump3);
            set_TextBox_NextFertilizeJob(Fertilize_Manager.Pump4, textBox_NextRun_Pump4);
            set_TextBox_RemainingTime(Fertilize_Manager.Pump4, textBox_RemainingTime_Pump4);
        }

        #endregion


        #region Functions

        void IWindow.UpdateTime(string time)
        {
            textBox_Time.Text = time;
            textBox_Time.Invalidate();
        }

        private void set_TextBox_RemainingTime(Fertilize_Pump pump, TextBox textBox)
        {
            if (Time_Service.isTimeSet)
            {
                Fertilize_Job job = pump.Get_Nearest_Fertilize_Job();
                DateTime actualTime = new DateTime(0).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

                if (job != null)
                {
                    if (actualTime < job.StartTime)
                    {
                        TimeSpan remainingTimeToStart = job.StartTime - actualTime;
                        textBox.Text = remainingTimeToStart.Hours.ToString("D2") + ":" + remainingTimeToStart.Minutes.ToString("D2");
                    }
                    else
                    {
                        DateTime remainingTimeToStart = job.StartTime + (new DateTime(0).AddHours(24) - actualTime);
                        textBox.Text = remainingTimeToStart.Hour.ToString("D2") + ":" + remainingTimeToStart.Minute.ToString("D2");
                    }
                }
                else
                {
                    textBox.Text = "--:--";
                }
            }
            else
            {
                textBox.Text = "--:--";
            }

            textBox.Invalidate();
        }

        #endregion
    }
}
