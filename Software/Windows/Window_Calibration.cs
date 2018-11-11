using GHI.Glide.Display;
using Microsoft.SPOT;

namespace AquaComp.Windows
{
    class Window_Calibration : IWindow
    {
        #region Controls

        internal Window window;

        private void Initialize_Components()
        {
            window = new CalibrationWindow(false, true);
            window.CloseEvent += window_OnClose;
        }

        public Window Window
        {
            get
            {
                return window;
            }
        }

        #endregion


        #region Constructors

        public Window_Calibration()
        {
            Initialize_Components();
        }

        #endregion


        #region EventHandler

        private static void window_OnClose(object sender)
        {
            Window_Manager.changeWindow(Window_Manager.window_Main);
            Debug.Print("Display calibrated.");
        }

        #endregion


        #region Functions

        void IWindow.UpdateTime(string time)
        {
            // do nothing
        }

        #endregion
    }
}
