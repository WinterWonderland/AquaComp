using GHI.Pins;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace AquaComp
{
    static class Fertilize_Manager
    {
        internal static Fertilize_Pump Pump1;
        internal static Fertilize_Pump Pump2;
        internal static Fertilize_Pump Pump3;
        internal static Fertilize_Pump Pump4;

        internal static void Initialize()
        {
            Pump1 = new Fertilize_Pump(G120.P1_31, 1);
            Pump1.IsRunning_Changed += Pump_IsRunning_Changed;
            Pump1.Name = ApplicationSettings.GetParameter("Name_Pump1", "Pump 1");
            Pump1.Name_Changed += Pump1_Name_Changed;
            Pump1.RuntimePerMilliliter = ApplicationSettings.GetParameter("RuntimePerMilliliter_Pump1", 1.5f);
            Pump1.RuntimePerMilliliter_Changed += Pump1_RuntimePerMilliliter_Changed;
            Pump1.AWC_Quantity = ApplicationSettings.GetParameter("AWC_Quantity_Pump1", 0);
            Pump1.AWC_Quantity_Changed += Pump1_AWC_Quantity_Changed;
            Pump1.Fertilize_Schedule_Changed += Pump1_Fertilize_Schedule_Changed;
            Load_Fertilize_Jobs(1, Pump1);
            

            Pump2 = new Fertilize_Pump(G120.P1_30, 2);
            Pump2.IsRunning_Changed += Pump_IsRunning_Changed;
            Pump2.Name = ApplicationSettings.GetParameter("Name_Pump2", "Pump 2");
            Pump2.Name_Changed += Pump2_Name_Changed;
            Pump2.RuntimePerMilliliter = ApplicationSettings.GetParameter("RuntimePerMilliliter_Pump2", 1.5f);
            Pump2.RuntimePerMilliliter_Changed += Pump2_RuntimePerMilliliter_Changed;
            Pump2.AWC_Quantity = ApplicationSettings.GetParameter("AWC_Quantity_Pump2", 0);
            Pump2.AWC_Quantity_Changed += Pump2_AWC_Quantity_Changed;
            Pump2.Fertilize_Schedule_Changed += Pump2_Fertilize_Schedule_Changed;
            Load_Fertilize_Jobs(2, Pump2);
            

            Pump3 = new Fertilize_Pump(G120.P0_12, 3);
            Pump3.IsRunning_Changed += Pump_IsRunning_Changed;
            Pump3.Name = ApplicationSettings.GetParameter("Name_Pump3", "Pump 3");
            Pump3.Name_Changed += Pump3_Name_Changed;
            Pump3.RuntimePerMilliliter = ApplicationSettings.GetParameter("RuntimePerMilliliter_Pump3", 1.5f);
            Pump3.RuntimePerMilliliter_Changed += Pump3_RuntimePerMilliliter_Changed;
            Pump3.AWC_Quantity = ApplicationSettings.GetParameter("AWC_Quantity_Pump3", 0);
            Pump3.AWC_Quantity_Changed += Pump3_AWC_Quantity_Changed;
            Pump3.Fertilize_Schedule_Changed += Pump3_Fertilize_Schedule_Changed;
            Load_Fertilize_Jobs(3, Pump3);
            

            Pump4 = new Fertilize_Pump(G120.P0_13, 4);
            Pump4.IsRunning_Changed += Pump_IsRunning_Changed;
            Pump4.Name = ApplicationSettings.GetParameter("Name_Pump4", "Pump 4");
            Pump4.Name_Changed += Pump4_Name_Changed;
            Pump4.RuntimePerMilliliter = ApplicationSettings.GetParameter("RuntimePerMilliliter_Pump4", 1.5f);
            Pump4.RuntimePerMilliliter_Changed += Pump4_RuntimePerMilliliter_Changed;
            Pump4.AWC_Quantity = ApplicationSettings.GetParameter("AWC_Quantity_Pump4", 0);
            Pump4.AWC_Quantity_Changed += Pump4_AWC_Quantity_Changed;
            Pump4.Fertilize_Schedule_Changed += Pump4_Fertilize_Schedule_Changed;
            Load_Fertilize_Jobs(4, Pump4);
            

            Debug.Print("Fertilize Manager initialized.");
        }

        internal static Fertilize_Pump GetFertilizePumpByIndex(int index)
        {
            switch (index)
            {
                case 1:
                    return Pump1;

                case 2:
                    return Pump2;

                case 3:
                    return Pump3;

                case 4:
                    return Pump4;

                default:
                    return null;
            }
        }

        private static void Pump_IsRunning_Changed(object sender, EventArgs e)
        {
            OnPumpIsRunning_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        private static void OnPumpIsRunning_Changed(PumpEventArgs e)
        {
            if (PumpIsRunning_Changed != null)
            {
                PumpIsRunning_Changed(null, e);
            }
        }

        internal static event PumpEventHandler PumpIsRunning_Changed;

        static void Pump1_Name_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("Name_Pump1", Pump1.Name);
            OnPumpName_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        static void Pump2_Name_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("Name_Pump2", Pump2.Name);
            OnPumpName_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        static void Pump3_Name_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("Name_Pump3", Pump3.Name);
            OnPumpName_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        static void Pump4_Name_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("Name_Pump4", Pump4.Name);
            OnPumpName_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        private static void OnPumpName_Changed(PumpEventArgs e)
        {
            if (PumpName_Changed != null)
            {
                PumpName_Changed(null, e);
            }
        }

        internal static event PumpEventHandler PumpName_Changed;

        internal delegate void PumpEventHandler(object sender, PumpEventArgs e);

        static void Pump1_RuntimePerMilliliter_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("RuntimePerMilliliter_Pump1", Pump1.RuntimePerMilliliter);
            OnRuntimePerMilliliter_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        static void Pump2_RuntimePerMilliliter_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("RuntimePerMilliliter_Pump2", Pump1.RuntimePerMilliliter);
            OnRuntimePerMilliliter_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        static void Pump3_RuntimePerMilliliter_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("RuntimePerMilliliter_Pump3", Pump1.RuntimePerMilliliter);
            OnRuntimePerMilliliter_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        static void Pump4_RuntimePerMilliliter_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("RuntimePerMilliliter_Pump4", Pump1.RuntimePerMilliliter);
            OnRuntimePerMilliliter_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        static void Pump1_AWC_Quantity_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("AWC_Quantity_Pump1", Pump1.AWC_Quantity);
        }

        static void Pump2_AWC_Quantity_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("AWC_Quantity_Pump2", Pump2.AWC_Quantity);
        }

        static void Pump3_AWC_Quantity_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("AWC_Quantity_Pump3", Pump3.AWC_Quantity);
        }

        static void Pump4_AWC_Quantity_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter("AWC_Quantity_Pump4", Pump4.AWC_Quantity);
        }

        private static void OnRuntimePerMilliliter_Changed(PumpEventArgs e)
        {
            if (RuntimePerMilliliter_Changed != null)
            {
                RuntimePerMilliliter_Changed(null, e);
            }
        }

        internal static event PumpEventHandler RuntimePerMilliliter_Changed;

        static void Pump1_Fertilize_Schedule_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter(1, Pump1.Get_All_Fertilize_Jobs());
            OnFertilize_Schedule_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        static void Pump2_Fertilize_Schedule_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter(2, Pump1.Get_All_Fertilize_Jobs());
            OnFertilize_Schedule_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        static void Pump3_Fertilize_Schedule_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter(3, Pump1.Get_All_Fertilize_Jobs());
            OnFertilize_Schedule_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        static void Pump4_Fertilize_Schedule_Changed(object sender, EventArgs e)
        {
            ApplicationSettings.SetParameter(4, Pump1.Get_All_Fertilize_Jobs());
            OnFertilize_Schedule_Changed(new PumpEventArgs((Fertilize_Pump)sender));
        }

        private static void OnFertilize_Schedule_Changed(PumpEventArgs e)
        {
            if (Fertilize_Schedule_Changed != null)
            {
                Fertilize_Schedule_Changed(null, e);
            }
        }

        internal static event PumpEventHandler Fertilize_Schedule_Changed;

        private static void Load_Fertilize_Jobs(int pumpIndex, Fertilize_Pump pump)
        {
            Fertilize_Job[] jobs = ApplicationSettings.GetParameter(pumpIndex);

            foreach (Fertilize_Job job in jobs)
            {
                pump.Add_Fertilize_Job(job);
            }
        }
    }

    class PumpEventArgs : EventArgs
    {
        internal Fertilize_Pump Pump;

        internal PumpEventArgs(Fertilize_Pump pump)
        {
            Pump = pump;
        }
    }
}
