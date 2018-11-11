using GHI.Glide;
using GHI.Glide.UI;
using Microsoft.SPOT;
using System;

namespace AquaComp.Windows
{
    class Window_PumpProperties : IWindow
    {
        #region Controls

        private GHI.Glide.Display.Window window;
        private TextBox textBox_Header;
        private TextBox textBox_Time;
        private TextBox textBox_Back;
        private Dropdown dropDown_PumpSelection;
        private List list_dropDown_PumpSelection;
        private TextBox textBox_PumpName;
        private Button button_Calibration_Run;
        private Button button_Calibration_Adjust;
        private TextBox textBox_Calibration_Quantity_Header;
        private TextBox textBox_Calibration_Quantity_Value;
        private TextBox textBox_Calibration_Measured_Header;
        private TextBox textBox_Calibration_Measured_Value;
        private TextBox textBox_Calibration_Runtime_Value;

        private void Initialize_Components()
        {
            // window
            window = new GHI.Glide.Display.Window("window_Fertilize", Window_Manager.Display_Width, Window_Manager.Display_Heigth);
            window.BackColor = Colors.White;

            // textBox_Header
            textBox_Header = new TextBox("textBox_Header", 255, 120, 20, 270, 32);
            textBox_Header.Text = "Pump Properties";
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

            // dropDown_PumpSelection
            dropDown_PumpSelection = new Dropdown("dropDown_PumpSelection", 255, 120, 70, 270, 32);
            dropDown_PumpSelection.Text = "Select Pump";
            dropDown_PumpSelection.Options.Add(new object[] { "Pump 1", 1 });
            dropDown_PumpSelection.Options.Add(new object[] { "Pump 2", 2 });
            dropDown_PumpSelection.Options.Add(new object[] { "Pump 3", 3 });
            dropDown_PumpSelection.Options.Add(new object[] { "Pump 4", 4 });
            dropDown_PumpSelection.TapEvent += dropDown_PumpSelection_TapEvent;
            dropDown_PumpSelection.ValueChangedEvent += dropDown_PumpSelection_ValueChangedEvent;
            window.AddChild(dropDown_PumpSelection);

            // list_dropDown_PumpSelection
            list_dropDown_PumpSelection = new List(dropDown_PumpSelection.Options, 4);
            list_dropDown_PumpSelection.CloseEvent += list_dropDown_PumpSelection_CloseEvent;

            // textBox_PumpName
            textBox_PumpName = new TextBox("textBox_PumpName", 255, 120, 120, 270, 32);
            textBox_PumpName.TapEvent += textBox_PumpName_TapEvent;
            textBox_PumpName.ValueChangedEvent += textBox_PumpName_ValueChangedEvent;
            window.AddChild(textBox_PumpName);

            // button_Calibration_Set
            button_Calibration_Run = new Button("button_Calibration_Run", 255, 20, 170, 80, 32);
            button_Calibration_Run.Text = "Run";
            button_Calibration_Run.TapEvent += button_Calibration_Run_TapEvent;
            window.AddChild(button_Calibration_Run);

            // button_Calibration_Adjust
            button_Calibration_Adjust = new Button("button_Calibration_Adjust", 255, 20, 220, 80, 32);
            button_Calibration_Adjust.Text = "Adjust";
            button_Calibration_Adjust.TapEvent += button_Calibration_Adjust_TapEvent;
            window.AddChild(button_Calibration_Adjust);

            // textBox_Calibration_Quantity_Header
            textBox_Calibration_Quantity_Header = new TextBox("textBox_Calibration_Quantity_Header", 255, 120, 170, 170, 32);
            textBox_Calibration_Quantity_Header.Text = "Calibration Quantity:";
            window.AddChild(textBox_Calibration_Quantity_Header);

            // textBox_Calibration_Quantity_Value
            textBox_Calibration_Quantity_Value = new TextBox("textBox_Calibration_Quantity_Value", 255, 310, 170, 80, 32);
            textBox_Calibration_Quantity_Value.Text = "15 ml";
            textBox_Calibration_Quantity_Value.TextAlign = 3;
            textBox_Calibration_Quantity_Value.TapEvent += textBox_Calibration_Quantity_Value_TapEvent;
            textBox_Calibration_Quantity_Value.ValueChangedEvent += textBox_Calibration_Quantity_Value_ValueChangedEvent;
            window.AddChild(textBox_Calibration_Quantity_Value);

            // textBox_Calibration_Measured_Header
            textBox_Calibration_Measured_Header = new TextBox("textBox_Calibration_Measured_Header", 255, 120, 220, 170, 32);
            textBox_Calibration_Measured_Header.Text = "Calibration Measured:";
            window.AddChild(textBox_Calibration_Measured_Header);

            // textBox_Calibration_Measured_Value
            textBox_Calibration_Measured_Value = new TextBox("textBox_Calibration_Measured_Value", 255, 310, 220, 80, 32);
            textBox_Calibration_Measured_Value.Text = "15 ml";
            textBox_Calibration_Measured_Value.TextAlign = 3;
            textBox_Calibration_Measured_Value.TapEvent += textBox_Calibration_Measured_Value_TapEvent;
            textBox_Calibration_Measured_Value.ValueChangedEvent += textBox_Calibration_Measured_Value_ValueChangedEvent;
            window.AddChild(textBox_Calibration_Measured_Value);

            // textBox_Calibration_Runtime_Value
            textBox_Calibration_Runtime_Value = new TextBox("textBox_Calibration_Runtime_Value", 255, 20, 120, 80, 32);
            textBox_Calibration_Runtime_Value.Text = "-.- s/ml";
            textBox_Calibration_Runtime_Value.TextAlign = 3;
            window.AddChild(textBox_Calibration_Runtime_Value);
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

        public Window_PumpProperties()
        {
            Initialize_Components();

            Fertilize_Manager.RuntimePerMilliliter_Changed += Fertilize_Manager_RuntimePerMilliliter_Changed;
        }

        #endregion


        #region EventHandler

        private static void button_Back_TapEvent(object sender)
        {
            Window_Manager.changeWindow(Window_Manager.window_Fertilize);
        }

        void dropDown_PumpSelection_TapEvent(object sender)
        {
            Glide.OpenList(sender, list_dropDown_PumpSelection);
        }

        void dropDown_PumpSelection_ValueChangedEvent(object sender)
        {
            if (dropDown_PumpSelection.Value != null)
            {
                textBox_PumpName.Text = Fertilize_Manager.GetFertilizePumpByIndex((int)dropDown_PumpSelection.Value).Name;
                textBox_Calibration_Runtime_Value.Text = Fertilize_Manager.GetFertilizePumpByIndex((int)dropDown_PumpSelection.Value).RuntimePerMilliliter.ToString("F1") + " s/ml";
            }
        }

        void list_dropDown_PumpSelection_CloseEvent(object sender)
        {
            Glide.CloseList();
        }

        void textBox_PumpName_ValueChangedEvent(object sender)
        {
            if (dropDown_PumpSelection.Value != null)
            {
                Fertilize_Manager.GetFertilizePumpByIndex((int)dropDown_PumpSelection.Value).Name = textBox_PumpName.Text;
            }
        }

        void textBox_PumpName_TapEvent(object sender)
        {
            if (dropDown_PumpSelection.Value != null)
            {
                Window_Manager.GlideOpenKeyBoard(sender);
            }
        }

        void button_Calibration_Run_TapEvent(object sender)
        {
            if (dropDown_PumpSelection.Value != null)
            {
                float value = (float)double.Parse(textBox_Calibration_Quantity_Value.Text.Substring(0, textBox_Calibration_Quantity_Value.Text.Length - 3));
                Fertilize_Manager.GetFertilizePumpByIndex((int)dropDown_PumpSelection.Value).Run(value);
            }
        }

        void textBox_Calibration_Quantity_Value_TapEvent(object sender)
        {
            Window_Manager.GlideOpenKeyBoard(sender);
        }

        void textBox_Calibration_Quantity_Value_ValueChangedEvent(object sender)
        {
            string sValue;

            if (textBox_Calibration_Quantity_Value.Text.IndexOf(" ml") == (textBox_Calibration_Quantity_Value.Text.Length - 3))
            {
                sValue = textBox_Calibration_Quantity_Value.Text.Substring(0, textBox_Calibration_Quantity_Value.Text.Length - 3);
            }
            else
            {
                sValue = textBox_Calibration_Quantity_Value.Text;
            }

            textBox_Calibration_Quantity_Value.ValueChangedEvent -= textBox_Calibration_Quantity_Value_ValueChangedEvent;

            try
            {
                int iValue = int.Parse(sValue);
                textBox_Calibration_Quantity_Value.Text = iValue.ToString() + " ml";
            }
            catch (Exception)
            {
                textBox_Calibration_Quantity_Value.Text = "15 ml";
            }

            textBox_Calibration_Quantity_Value.ValueChangedEvent += textBox_Calibration_Quantity_Value_ValueChangedEvent;
        }

        void textBox_Calibration_Measured_Value_TapEvent(object sender)
        {
            Window_Manager.GlideOpenKeyBoard(sender);
        }

        void textBox_Calibration_Measured_Value_ValueChangedEvent(object sender)
        {
            string sValue;

            if (textBox_Calibration_Measured_Value.Text.IndexOf(" ml") == (textBox_Calibration_Measured_Value.Text.Length - 3) && textBox_Calibration_Measured_Value.Text.IndexOf(" ml") > 0)
            {
                sValue = textBox_Calibration_Measured_Value.Text.Substring(0, textBox_Calibration_Measured_Value.Text.Length - 3);
            }
            else
            {
                sValue = textBox_Calibration_Measured_Value.Text;
            }

            textBox_Calibration_Measured_Value.ValueChangedEvent -= textBox_Calibration_Measured_Value_ValueChangedEvent;

            try
            {
                int iValue = int.Parse(sValue);
                textBox_Calibration_Measured_Value.Text = iValue.ToString() + " ml";
            }
            catch (Exception)
            {
                textBox_Calibration_Measured_Value.Text = "15 ml";
            }

            textBox_Calibration_Measured_Value.ValueChangedEvent += textBox_Calibration_Measured_Value_ValueChangedEvent;
        }

        void button_Calibration_Adjust_TapEvent(object sender)
        {
            if (dropDown_PumpSelection.Value != null)
            {
                int quantity = int.Parse(textBox_Calibration_Quantity_Value.Text.Substring(0, textBox_Calibration_Quantity_Value.Text.Length - 3));
                int measured = int.Parse(textBox_Calibration_Measured_Value.Text.Substring(0, textBox_Calibration_Measured_Value.Text.Length - 3));
                Fertilize_Manager.GetFertilizePumpByIndex((int)dropDown_PumpSelection.Value).Adjust_RuntimePerMilliliter(quantity, measured);
            }
        }

        void Fertilize_Manager_RuntimePerMilliliter_Changed(object sender, PumpEventArgs e)
        {
            textBox_Calibration_Runtime_Value.Text = e.Pump.RuntimePerMilliliter.ToString("F1") + " s/ml";
            textBox_Calibration_Runtime_Value.Invalidate();
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
