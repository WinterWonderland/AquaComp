using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;
using System.Collections;
using System;

namespace AquaComp
{
    class Fertilize_Pump
    {
        #region Constructors

        internal Fertilize_Pump(Cpu.Pin pin, int pumpNumber)
        {
            port = new OutputPort(pin, false);
            PumpNumber = pumpNumber;
            timer_CheckFertilizeJobs = new Timer(timer_CheckFertilizeJobs_Tick, null, 0, 30 * 1000);
        }

        #endregion


        #region Pump Settings

        private int pumpNumber = 0;

        internal int PumpNumber
        {
            get
            {
                return pumpNumber;
            }

            private set
            {
                pumpNumber = value;
            }
        }

        private OutputPort port;

        private string name = "";

        internal string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnName_Changed(new EventArgs());
            }
        }

        protected virtual void OnName_Changed(EventArgs e)
        {
            if (Name_Changed != null)
            {
                Name_Changed(this, e);
            }
        }

        internal event EventHandler Name_Changed;

        private float runtimePerMilliliter;

        internal float RuntimePerMilliliter
        {
            set
            {
                runtimePerMilliliter = value;
                OnRuntimePerMilliliter_Changed(new EventArgs());
            }

            get
            {
                return runtimePerMilliliter;
            }
        }

        protected virtual void OnRuntimePerMilliliter_Changed(EventArgs e)
        {
            if (RuntimePerMilliliter_Changed != null)
            {
                RuntimePerMilliliter_Changed(this, e);
            }
        }

        internal event EventHandler RuntimePerMilliliter_Changed;

        internal void Adjust_RuntimePerMilliliter(int quantity, int measured)
        {
            RuntimePerMilliliter = ((float)quantity) * RuntimePerMilliliter / measured;
        }

        private int aWC_Quantity;

        internal int AWC_Quantity
        {
            set
            {
                aWC_Quantity = value;
                OnAWC_Quantity_Changed(new EventArgs());
            }

            get
            {
                return aWC_Quantity;
            }
        }

        protected virtual void OnAWC_Quantity_Changed(EventArgs e)
        {
            if (AWC_Quantity_Changed != null)
            {
                AWC_Quantity_Changed(this, e);
            }
        }

        internal event EventHandler AWC_Quantity_Changed;

        #endregion


        #region Pump Operations

        internal void Start()
        {
            port.Write(true);
            IsRunning = true;
        }

        internal void Stop()
        {
            port.Write(false);
            IsRunning = false;
        }

        internal void Toggle()
        {
            if (IsRunning)
            {
                Stop();
            }
            else
            {
                Start();
            }
        }

        private bool isRunning = false;

        internal bool IsRunning
        {
            get
            {
                return isRunning;
            }

            private set
            {
                isRunning = value;
                OnIsRunning_Changed(new EventArgs());
            }
        }

        protected virtual void OnIsRunning_Changed(EventArgs e)
        {
            if (IsRunning_Changed != null)
            {
                IsRunning_Changed(this, e);
            }
        }

        internal event EventHandler IsRunning_Changed;

        Timer timer_Run;

        internal void Run(float quantity_ml)
        {
            int runtime_milliseconds = (int)(quantity_ml * RuntimePerMilliliter * 1000);
            timer_Run = new Timer(timer_Run_Tick, null, runtime_milliseconds, Timeout.Infinite);
            Start();
        }

        private void timer_Run_Tick(object state)
        {
            Stop();
        }

        #endregion


        #region Fertilize_Schedule

        private ArrayList Fertilize_Schedule = new ArrayList();

        internal void Add_Empty_Fertilize_Job()
        {
            Add_Fertilize_Job(new DateTime(0), 0);
        }

        internal void Add_Fertilize_Job(DateTime startTime, int quantity_ml)
        {
            Add_Fertilize_Job(new Fertilize_Job(startTime, quantity_ml));
        }

        internal void Add_Fertilize_Job(Fertilize_Job job)
        {
            Fertilize_Schedule.Add(job);
            OnFertilize_Schedule_Changed(new EventArgs());
        }

        internal void Delete_Fertilize_Job(Fertilize_Job job)
        {
            if (Fertilize_Schedule.Contains(job))
            {
                Fertilize_Schedule.Remove(job);
                OnFertilize_Schedule_Changed(new EventArgs());
            }
        }

        internal void Modify_Fertilize_Job(Fertilize_Job job)
        {
            Delete_Fertilize_Job(job);
            Add_Fertilize_Job(job);
        }

        internal Fertilize_Job Get_Nearest_Fertilize_Job()
        {
            Fertilize_Job nearest_Job = null;
            DateTime actualTime = new DateTime(0).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

            foreach (Fertilize_Job job in Fertilize_Schedule)
            {
                if (nearest_Job == null)
                {
                    nearest_Job = job;
                }
                else
                {
                    if (job.StartTime > actualTime && nearest_Job.StartTime > actualTime)
                    {
                        if (job.StartTime < nearest_Job.StartTime)
                        {
                            nearest_Job = job;
                        }
                    }
                    else if (job.StartTime > actualTime && nearest_Job.StartTime < actualTime)
                    {
                        nearest_Job = job;
                    }
                    else if (job.StartTime < actualTime && nearest_Job.StartTime < actualTime)
                    {
                        if (job.StartTime < nearest_Job.StartTime)
                        {
                            nearest_Job = job;
                        }
                    }
                }
            }

            return nearest_Job;
        }

        internal Fertilize_Job[] Get_All_Fertilize_Jobs()
        {
            Fertilize_Job[] all_Jobs = new Fertilize_Job[Fertilize_Schedule.Count];

            for (int jobIndex = 0; jobIndex < Fertilize_Schedule.Count; jobIndex++)
            {
                all_Jobs[jobIndex] = (Fertilize_Job)Fertilize_Schedule[jobIndex];
            }

            return all_Jobs;
        }

        protected virtual void OnFertilize_Schedule_Changed(EventArgs e)
        {
            if (Fertilize_Schedule_Changed != null)
            {
                Fertilize_Schedule_Changed(this, e);
            }
        }

        internal event EventHandler Fertilize_Schedule_Changed;

        Timer timer_CheckFertilizeJobs;

        private void timer_CheckFertilizeJobs_Tick(object state)
        {
            DateTime actualTime = new DateTime(0).AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute);

            foreach (Fertilize_Job job in Fertilize_Schedule)
            {
                if (!job.IsBlocked)
                {
                    if (job.StartTime < actualTime)
                    {
                        Run(job.Quantity_ml);
                        job.IsBlocked = true;
                    }
                }
                else
                {
                    if (new DateTime(0).AddHours(1) > actualTime)
                    {
                        job.IsBlocked = false;
                    }
                }
            }
        }

        #endregion
    }

    class Fertilize_Job
    {
        public bool IsBlocked = true;
        public DateTime StartTime;
        public int Quantity_ml;

        public Fertilize_Job(DateTime startTime, int quantity_ml)
        {
            StartTime = startTime;
            Quantity_ml = quantity_ml;
        }
    }
}
