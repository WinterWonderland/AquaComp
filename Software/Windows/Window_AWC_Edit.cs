using GHI.Glide;
using GHI.Glide.UI;
using Microsoft.SPOT;
using System.Threading;
using System;

namespace AquaComp.Windows
{
    class Window_AWC_Edit : IWindow
    {
        #region Controls

        private GHI.Glide.Display.Window window;
        private TextBox textBox_Header;
        private TextBox textBox_Time;
        private TextBox textBox_Back;
        private TextBox textBox_Name_Pump1;
        private TextBox textBox_Name_Pump2;
        private TextBox textBox_Name_Pump3;
        private TextBox textBox_Name_Pump4;
        private TextBox textBox_Quantity_Pump1;
        private TextBox textBox_Quantity_Pump2;
        private TextBox textBox_Quantity_Pump3;
        private TextBox textBox_Quantity_Pump4;

        private void Initialize_Components()
        {
            // window
            window = new GHI.Glide.Display.Window("window_AWC_Edit", Window_Manager.Display_Width, Window_Manager.Display_Heigth);
            window.BackColor = Colors.White;

            // textBox_Header
            textBox_Header = new TextBox("textBox_Header", 255, 120, 20, 270, 32);
            textBox_Header.Text = "AWC Edit";
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

            // textBox_Name_Pump1
            textBox_Name_Pump1 = new TextBox("textBox_Name_Pump1", 255, 120, 70, 170, 32);
            textBox_Name_Pump1.Text = Fertilize_Manager.Pump1.Name;
            window.AddChild(textBox_Name_Pump1);

            // textBox_Name_Pump2
            textBox_Name_Pump2 = new TextBox("textBox_Name_Pump2", 255, 120, 120, 170, 32);
            textBox_Name_Pump2.Text = Fertilize_Manager.Pump2.Name;
            window.AddChild(textBox_Name_Pump2);

            // textBox_Name_Pump3
            textBox_Name_Pump3 = new TextBox("textBox_Name_Pump3", 255, 120, 170, 170, 32);
            textBox_Name_Pump3.Text = Fertilize_Manager.Pump3.Name;
            window.AddChild(textBox_Name_Pump3);

            // textBox_Name_Pump4
            textBox_Name_Pump4 = new TextBox("textBox_Name_Pump4", 255, 120, 220, 170, 32);
            textBox_Name_Pump4.Text = Fertilize_Manager.Pump4.Name;
            window.AddChild(textBox_Name_Pump4);

            // textBox_Quantity_Pump1
            textBox_Quantity_Pump1 = new TextBox("textBox_Quantity_Pump1", 255, 310, 70, 80, 32);
            textBox_Quantity_Pump1.Text = Fertilize_Manager.Pump1.AWC_Quantity.ToString() + " ml";
            textBox_Quantity_Pump1.TextAlign = 2;
            textBox_Quantity_Pump1.TapEvent += textBox_Quantity_Pump1_TapEvent;
            textBox_Quantity_Pump1.ValueChangedEvent += textBox_Quantity_Pump1_ValueChangedEvent;
            window.AddChild(textBox_Quantity_Pump1);

            // textBox_Quantity_Pump2
            textBox_Quantity_Pump2 = new TextBox("textBox_Quantity_Pump2", 255, 310, 120, 80, 32);
            textBox_Quantity_Pump2.Text = Fertilize_Manager.Pump2.AWC_Quantity.ToString() + " ml";
            textBox_Quantity_Pump2.TextAlign = 2;
            textBox_Quantity_Pump2.TapEvent += textBox_Quantity_Pump2_TapEvent;
            textBox_Quantity_Pump2.ValueChangedEvent += textBox_Quantity_Pump2_ValueChangedEvent;
            window.AddChild(textBox_Quantity_Pump2);

            // textBox_Quantity_Pump3
            textBox_Quantity_Pump3 = new TextBox("textBox_Quantity_Pump3", 255, 310, 170, 80, 32);
            textBox_Quantity_Pump3.Text = Fertilize_Manager.Pump3.AWC_Quantity.ToString() + " ml";
            textBox_Quantity_Pump3.TextAlign = 2;
            textBox_Quantity_Pump3.TapEvent += textBox_Quantity_Pump3_TapEvent;
            textBox_Quantity_Pump3.ValueChangedEvent += textBox_Quantity_Pump3_ValueChangedEvent;
            window.AddChild(textBox_Quantity_Pump3);

            // textBox_Quantity_Pump4
            textBox_Quantity_Pump4 = new TextBox("textBox_Quantity_Pump4", 255, 310, 220, 80, 32);
            textBox_Quantity_Pump4.Text = Fertilize_Manager.Pump4.AWC_Quantity.ToString() + " ml";
            textBox_Quantity_Pump4.TextAlign = 2;
            textBox_Quantity_Pump4.TapEvent += textBox_Quantity_Pump4_TapEvent;
            textBox_Quantity_Pump4.ValueChangedEvent += textBox_Quantity_Pump4_ValueChangedEvent;
            window.AddChild(textBox_Quantity_Pump4);
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

        public Window_AWC_Edit()
        {
            Initialize_Components();

            Fertilize_Manager.PumpName_Changed += Fertilize_Manager_PumpName_Changed;
        }

        #endregion


        #region EventHandler

        private static void button_Back_TapEvent(object sender)
        {
            Window_Manager.changeWindow(Window_Manager.window_Fertilize);
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

        void textBox_Quantity_Pump1_TapEvent(object sender)
        {
            Window_Manager.GlideOpenKeyBoard(sender);
        }

        void textBox_Quantity_Pump2_TapEvent(object sender)
        {
            Window_Manager.GlideOpenKeyBoard(sender);
        }

        void textBox_Quantity_Pump3_TapEvent(object sender)
        {
            Window_Manager.GlideOpenKeyBoard(sender);
        }

        void textBox_Quantity_Pump4_TapEvent(object sender)
        {
            Window_Manager.GlideOpenKeyBoard(sender);
        }

        void textBox_Quantity_Pump1_ValueChangedEvent(object sender)
        {
            string sValue;

            if (textBox_Quantity_Pump1.Text.IndexOf(" ml") == (textBox_Quantity_Pump1.Text.Length - 3) && textBox_Quantity_Pump1.Text.IndexOf(" ml") > 0)
            {
                sValue = textBox_Quantity_Pump1.Text.Substring(0, textBox_Quantity_Pump1.Text.Length - 3);
            }
            else
            {
                sValue = textBox_Quantity_Pump1.Text;
            }

            textBox_Quantity_Pump1.ValueChangedEvent -= textBox_Quantity_Pump1_ValueChangedEvent;

            try
            {
                int iValue = int.Parse(sValue);
                Fertilize_Manager.Pump1.AWC_Quantity = iValue;
                textBox_Quantity_Pump1.Text = iValue.ToString() + " ml";
            }
            catch (Exception)
            {
                Fertilize_Manager.Pump1.AWC_Quantity = 0;
                textBox_Quantity_Pump1.Text = "0 ml";
            }

            textBox_Quantity_Pump1.ValueChangedEvent += textBox_Quantity_Pump1_ValueChangedEvent;
        }

        void textBox_Quantity_Pump2_ValueChangedEvent(object sender)
        {
            string sValue;

            if (textBox_Quantity_Pump2.Text.IndexOf(" ml") == (textBox_Quantity_Pump2.Text.Length - 3) && textBox_Quantity_Pump2.Text.IndexOf(" ml") > 0)
            {
                sValue = textBox_Quantity_Pump2.Text.Substring(0, textBox_Quantity_Pump2.Text.Length - 3);
            }
            else
            {
                sValue = textBox_Quantity_Pump2.Text;
            }

            textBox_Quantity_Pump2.ValueChangedEvent -= textBox_Quantity_Pump2_ValueChangedEvent;

            try
            {
                int iValue = int.Parse(sValue);
                Fertilize_Manager.Pump2.AWC_Quantity = iValue;
                textBox_Quantity_Pump2.Text = iValue.ToString() + " ml";
            }
            catch (Exception)
            {
                Fertilize_Manager.Pump2.AWC_Quantity = 0;
                textBox_Quantity_Pump2.Text = "0 ml";
            }

            textBox_Quantity_Pump2.ValueChangedEvent += textBox_Quantity_Pump2_ValueChangedEvent;
        }

        void textBox_Quantity_Pump3_ValueChangedEvent(object sender)
        {
            string sValue;

            if (textBox_Quantity_Pump3.Text.IndexOf(" ml") == (textBox_Quantity_Pump3.Text.Length - 3) && textBox_Quantity_Pump3.Text.IndexOf(" ml") > 0)
            {
                sValue = textBox_Quantity_Pump3.Text.Substring(0, textBox_Quantity_Pump3.Text.Length - 3);
            }
            else
            {
                sValue = textBox_Quantity_Pump3.Text;
            }

            textBox_Quantity_Pump3.ValueChangedEvent -= textBox_Quantity_Pump3_ValueChangedEvent;

            try
            {
                int iValue = int.Parse(sValue);
                Fertilize_Manager.Pump3.AWC_Quantity = iValue;
                textBox_Quantity_Pump3.Text = iValue.ToString() + " ml";
            }
            catch (Exception)
            {
                Fertilize_Manager.Pump3.AWC_Quantity = 0;
                textBox_Quantity_Pump3.Text = "0 ml";
            }

            textBox_Quantity_Pump3.ValueChangedEvent += textBox_Quantity_Pump3_ValueChangedEvent;
        }

        void textBox_Quantity_Pump4_ValueChangedEvent(object sender)
        {
            string sValue;

            if (textBox_Quantity_Pump4.Text.IndexOf(" ml") == (textBox_Quantity_Pump4.Text.Length - 3) && textBox_Quantity_Pump4.Text.IndexOf(" ml") > 0)
            {
                sValue = textBox_Quantity_Pump4.Text.Substring(0, textBox_Quantity_Pump4.Text.Length - 3);
            }
            else
            {
                sValue = textBox_Quantity_Pump4.Text;
            }

            textBox_Quantity_Pump4.ValueChangedEvent -= textBox_Quantity_Pump4_ValueChangedEvent;

            try
            {
                int iValue = int.Parse(sValue);
                Fertilize_Manager.Pump4.AWC_Quantity = iValue;
                textBox_Quantity_Pump4.Text = iValue.ToString() + " ml";
            }
            catch (Exception)
            {
                Fertilize_Manager.Pump4.AWC_Quantity = 0;
                textBox_Quantity_Pump4.Text = "0 ml";
            }

            textBox_Quantity_Pump4.ValueChangedEvent += textBox_Quantity_Pump4_ValueChangedEvent;
        }

        #endregion


        #region Functions

        void IWindow.UpdateTime(string time)
        {
            textBox_Time.Text = time;
            textBox_Time.Invalidate();
        }

        #endregion
    }
}
