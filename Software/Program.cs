using Microsoft.SPOT;

namespace AquaComp
{
    public partial class Program
    {
        // This method is run when the mainboard is powered up or reset.   
        private void ProgramStarted()
        {
            Debug.Print("Program started.");
            Initialize();
        }

        private void Initialize()
        {
            SD_Card.Initialize();
            ApplicationSettings.Initialize();
            Fertilize_Manager.Initialize();
            Window_Manager.Initialize();
            Ethernet.Initialize();
            Time_Service.Initialize();
            
            
            Window_Manager.changeWindow(Window_Manager.window_Main);

            Debug.Print("Initializing finished.");
        }
    }
}
