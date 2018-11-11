using GHI.Glide;
using GHI.Glide.UI;
using Microsoft.SPOT;
using System;

namespace AquaComp.Windows
{
    class Window_PumpSchedule : IWindow
    {
        #region Controls

        private GHI.Glide.Display.Window window;
        private TextBox textBox_Header;
        private TextBox textBox_Time;
        private TextBox textBox_Back;
        private Dropdown dropDown_PumpSelection;
        private List list_dropDown_PumpSelection;
        private Button button_New;
        private Button button_Delete;
        private DataGrid dataGrid_Schedule;
        private TextBox textbox_StartTime;
        private TextBox textbox_Quantity;

        private void Initialize_Components()
        {
            // window
            window = new GHI.Glide.Display.Window("window_Fertilize", Window_Manager.Display_Width, Window_Manager.Display_Heigth);
            window.BackColor = Colors.White;

            // textBox_Header
            textBox_Header = new TextBox("textBox_Header", 255, 120, 20, 270, 32);
            textBox_Header.Text = "Pump Schedule";
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

            // button_New
            button_New = new Button("button_New", 255, 20, 70, 80, 32);
            button_New.Text = "New";
            button_New.TapEvent += button_New_TapEvent;
            window.AddChild(button_New);

            // button_Delete
            button_Delete = new Button("button_Delete", 255, 20, 120, 80, 32);
            button_Delete.Text = "Delete";
            button_Delete.TapEvent += button_Delete_TapEvent;
            window.AddChild(button_Delete);

            // dataGrid_Schedule
            DataGridColumn dataGridColumn_StartTime = new DataGridColumn("Start Time", 135);
            DataGridColumn dataGridColumn_Quantity = new DataGridColumn("Quantity", 135);
            DataGridColumn dataGridColumn_FertilizeJob = new DataGridColumn("Job", 0);
            dataGrid_Schedule = new DataGrid("dataGrid_Schedule", 255, 120, 120, 270, 32, 4);
            dataGrid_Schedule.HeadersBackColor = Colors.LightGray;
            dataGrid_Schedule.HeadersFontColor = Colors.Black;
            dataGrid_Schedule.AddColumn(dataGridColumn_StartTime);
            dataGrid_Schedule.AddColumn(dataGridColumn_Quantity);
            dataGrid_Schedule.AddColumn(dataGridColumn_FertilizeJob);
            dataGrid_Schedule.TapCellEvent += dataGrid_Schedule_TapCellEvent;
            window.AddChild(dataGrid_Schedule);

            // textbox_StartTime
            textbox_StartTime = new TextBox("textbox_StartTime", 255, 20, 170, 80, 32);
            textbox_StartTime.TextAlign = 3;
            textbox_StartTime.TapEvent += textbox_StartTime_TapEvent;
            textbox_StartTime.ValueChangedEvent += textbox_StartTime_ValueChangedEvent;
            window.AddChild(textbox_StartTime);

            // textbox_Quantity
            textbox_Quantity = new TextBox("textbox_Quantity", 255, 20, 220, 80, 32);
            textbox_Quantity.TextAlign = 3;
            textbox_Quantity.TapEvent += textbox_Quantity_TapEvent;
            textbox_Quantity.ValueChangedEvent += textbox_Quantity_ValueChangedEvent;
            window.AddChild(textbox_Quantity);
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

        public Window_PumpSchedule()
        {
            Initialize_Components();

            Fertilize_Manager.Fertilize_Schedule_Changed += fertilize_Manager_Fertilize_Schedule_Changed;
        }

        #endregion


        #region EventHandler

        private static void button_Back_TapEvent(object sender)
        {
            Window_Manager.changeWindow(Window_Manager.window_Fertilize);
        }

        private void dropDown_PumpSelection_TapEvent(object sender)
        {
            Glide.OpenList(sender, list_dropDown_PumpSelection);
        }

        private void dropDown_PumpSelection_ValueChangedEvent(object sender)
        {
            update_Schedule_DataGrid();
        }

        private void list_dropDown_PumpSelection_CloseEvent(object sender)
        {
            Glide.CloseList();
        }

        void button_New_TapEvent(object sender)
        {
            if (dropDown_PumpSelection.Value != null)
            {
                Fertilize_Manager.GetFertilizePumpByIndex((int)dropDown_PumpSelection.Value).Add_Empty_Fertilize_Job();
            }
        }

        void button_Delete_TapEvent(object sender)
        {
            if (dropDown_PumpSelection.Value != null)
            {
                Fertilize_Manager.GetFertilizePumpByIndex((int)dropDown_PumpSelection.Value).Delete_Fertilize_Job((Fertilize_Job)dataGrid_Schedule.GetCellData(2, dataGrid_Schedule.SelectedIndex));
            }
        }

        void fertilize_Manager_Fertilize_Schedule_Changed(object sender, PumpEventArgs e)
        {
            if (e.Pump == Fertilize_Manager.GetFertilizePumpByIndex((int)dropDown_PumpSelection.Value))
            {
                update_Schedule_DataGrid();
            }
        }

        void dataGrid_Schedule_TapCellEvent(object sender, TapCellEventArgs args)
        {
            update_DataTextboxes(dataGrid_Schedule.SelectedIndex);
        }

        void textbox_StartTime_TapEvent(object sender)
        {
            if (dataGrid_Schedule.SelectedIndex >= 0 && dropDown_PumpSelection.Value != null)
            {
                Window_Manager.GlideOpenKeyBoard(sender);
            }
        }

        void textbox_StartTime_ValueChangedEvent(object sender)
        {
            if (dataGrid_Schedule.SelectedIndex >= 0 && dropDown_PumpSelection.Value != null)
            {
                Fertilize_Job job = ((Fertilize_Job)dataGrid_Schedule.GetCellData(2, dataGrid_Schedule.SelectedIndex));
                DateTime startTime = new DateTime(0);

                if ((textbox_StartTime.Text.IndexOf(":") > 0) && ((textbox_StartTime.Text.Length - 1) > textbox_StartTime.Text.IndexOf(":")))
                {
                    string hour = textbox_StartTime.Text.Substring(0, textbox_StartTime.Text.IndexOf(":"));
                    string minute = textbox_StartTime.Text.Substring(textbox_StartTime.Text.IndexOf(":") + 1, textbox_StartTime.Text.Length - textbox_StartTime.Text.IndexOf(":") - 1);

                    startTime = startTime.AddHours(int.Parse(hour)).AddMinutes(int.Parse(minute));
                }

                job.StartTime = startTime;
                Fertilize_Manager.GetFertilizePumpByIndex((int)dropDown_PumpSelection.Value).Modify_Fertilize_Job(job);
            }
        }

        void textbox_Quantity_TapEvent(object sender)
        {
            if (dataGrid_Schedule.SelectedIndex >= 0 && dropDown_PumpSelection.Value != null)
            {
                Window_Manager.GlideOpenKeyBoard(sender);
            }
        }

        void textbox_Quantity_ValueChangedEvent(object sender)
        {
            if (dataGrid_Schedule.SelectedIndex >= 0 && dropDown_PumpSelection.Value != null)
            {
                Fertilize_Job job = ((Fertilize_Job)dataGrid_Schedule.GetCellData(2, dataGrid_Schedule.SelectedIndex));
                string quantity_ml_string;

                if (textbox_Quantity.Text.IndexOf(" ml") == (textbox_Quantity.Text.Length - 3) && textbox_Quantity.Text.IndexOf(" ml") > 0)
                {
                    quantity_ml_string = textbox_Quantity.Text.Substring(0, textbox_Quantity.Text.Length - 3);
                }
                else
                {
                    quantity_ml_string = textbox_Quantity.Text;
                }

                int quantity_ml = 0;

                try
                {
                    quantity_ml = int.Parse(quantity_ml_string);
                }
                catch (Exception)
                {
                }

                job.Quantity_ml = quantity_ml;
                Fertilize_Manager.GetFertilizePumpByIndex((int)dropDown_PumpSelection.Value).Modify_Fertilize_Job(job);
            }
        }

        #endregion


        #region Functions

        void IWindow.UpdateTime(string time)
        {
            textBox_Time.Text = time;
            textBox_Time.Invalidate();
        }

        private void update_Schedule_DataGrid()
        {
            dataGrid_Schedule.Clear();

            if (dropDown_PumpSelection.Value != null)
            {
                Fertilize_Job[] jobs = Fertilize_Manager.GetFertilizePumpByIndex((int)dropDown_PumpSelection.Value).Get_All_Fertilize_Jobs();

                foreach (Fertilize_Job job in jobs)
                {
                    dataGrid_Schedule.AddItem(new DataGridItem(new object[] { job.StartTime.Hour.ToString("D2") + ":" + job.StartTime.Minute.ToString("D2"), job.Quantity_ml.ToString() + " ml", job }));
                }
            }

            update_DataTextboxes(dataGrid_Schedule.SelectedIndex);
            dataGrid_Schedule.Invalidate();
        }

        private void update_DataTextboxes(int row)
        {
            textbox_StartTime.ValueChangedEvent -= textbox_StartTime_ValueChangedEvent;
            textbox_Quantity.ValueChangedEvent -= textbox_Quantity_ValueChangedEvent;

            if (row >= 0 && dropDown_PumpSelection.Value != null)
            {
                textbox_StartTime.Text = (string)dataGrid_Schedule.GetCellData(0, row);
                textbox_Quantity.Text = (string)dataGrid_Schedule.GetCellData(1, row);
            }
            else
            {
                textbox_StartTime.Text = "";
                textbox_Quantity.Text = "";
            }

            textbox_StartTime.ValueChangedEvent += textbox_StartTime_ValueChangedEvent;
            textbox_Quantity.ValueChangedEvent += textbox_Quantity_ValueChangedEvent;
            textbox_StartTime.Invalidate();
            textbox_Quantity.Invalidate();
        }

        #endregion
    }
}
