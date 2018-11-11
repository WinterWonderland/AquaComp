using GHI.Glide;
using GHI.Glide.UI;

namespace AquaComp.Windows
{
    class Window_Main : IWindow
    {
        #region Controls

        private GHI.Glide.Display.Window window;
        private TextBox textBox_Header;
        private TextBox textBox_Time;
        private TextBox textBox_Calibrate;
        private Button button_Fertilize;

        private void Initialize_Components()
        {
            // window
            window = new GHI.Glide.Display.Window("window_Main", Window_Manager.Display_Width, Window_Manager.Display_Heigth);
            window.BackColor = Colors.White;

            // textBox_Header
            textBox_Header = new TextBox("textBox_Header", 255, 120, 20, 270, 32);
            textBox_Header.Text = "Aqua-Comp";
            textBox_Header.TextAlign = 2;
            window.AddChild(textBox_Header);

            // textBox_Time
            textBox_Time = new TextBox("textBox_Time", 255, 410, 20, 50, 32);
            textBox_Time.Text = "--:--";
            textBox_Time.TextAlign = 2;
            window.AddChild(textBox_Time);

            // button_Calibrate
            textBox_Calibrate = new TextBox("textBox_Calibrate", 255, 20, 20, 80, 32);
            textBox_Calibrate.Text = "Calibrate";
            textBox_Calibrate.TextAlign = 2;
            textBox_Calibrate.TapEvent += new OnTap(button_Calibrate_TapEvent);
            window.AddChild(textBox_Calibrate);

            // button_Fertilize
            button_Fertilize = new Button("button_Fertilize", 255, 20, 70, 80, 32);
            button_Fertilize.Text = "Fertilize";
            button_Fertilize.TapEvent += new OnTap(button_Fertilize_TapEvent);
            window.AddChild(button_Fertilize);
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

        public Window_Main()
        {
            Initialize_Components();
        }

        #endregion


        #region EventHandler

        private static void button_Calibrate_TapEvent(object sender)
        {
            Window_Manager.changeWindow(Window_Manager.window_Calibration);
        }

        private static void button_Fertilize_TapEvent(object sender)
        {
            Window_Manager.changeWindow(Window_Manager.window_Fertilize);
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
