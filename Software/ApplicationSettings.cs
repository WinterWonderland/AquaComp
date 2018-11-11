using System;
using Microsoft.SPOT;
using GHI.SQLite;
using System.Collections;

namespace AquaComp
{
    static class ApplicationSettings
    {
        private static Database settingsDatabase;

        internal static void Initialize()
        {
            settingsDatabase = new Database(SD_Card.GetSDRoot() + "\\ApplicationSettings.db");
            settingsDatabase.ExecuteNonQuery("CREATE TABLE IF NOT EXISTS Parameter (Name TEXT PRIMARY KEY, Value TEXT)");
            settingsDatabase.ExecuteNonQuery("CREATE TABLE IF NOT EXISTS Fertilize_Jobs (Idx INTEGER PRIMARY KEY, Pump_Index TEXT, Starttime TEXT, Quantity TEXT)");

            Debug.Print("Application Settings initialized.");
        }

        internal static void SetParameter(string name, string value)
        {
            settingsDatabase.ExecuteNonQuery("INSERT OR REPLACE INTO Parameter(Name,Value) VALUES('" + name + "','" + value + "')");
        }

        internal static void SetParameter(string name, float value)
        {
            settingsDatabase.ExecuteNonQuery("INSERT OR REPLACE INTO Parameter(Name,Value) VALUES('" + name + "','" + value.ToString() + "')");
        }

        internal static void SetParameter(string name, int value)
        {
            settingsDatabase.ExecuteNonQuery("INSERT OR REPLACE INTO Parameter(Name,Value) VALUES('" + name + "','" + value.ToString() + "')");
        }

        internal static void SetParameter(int Pump_Index, Fertilize_Job[] jobs)
        {
            string pump_Index = Pump_Index.ToString();
            settingsDatabase.ExecuteNonQuery("DELETE FROM Fertilize_Jobs WHERE Pump_Index='" + pump_Index + "'");

            foreach (Fertilize_Job job in jobs)
            {
                string startTime = job.StartTime.Hour.ToString("D2") + ":" + job.StartTime.Minute.ToString("D2");
                string quantity = job.Quantity_ml.ToString();
                settingsDatabase.ExecuteNonQuery("INSERT INTO Fertilize_Jobs(Pump_Index,Starttime,Quantity) VALUES('" + pump_Index + "','" + startTime + "','" + quantity + "')");
            }
        }

        internal static string GetParameter(string name, string defaultValue)
        {
            ResultSet result = settingsDatabase.ExecuteQuery("SELECT value FROM Parameter WHERE Name='" + name + "'");

            if (result.RowCount < 1)
            {
                return defaultValue;
            }
            else
            {
                return (string)((ArrayList)result.Data[0])[0];
            }
        }

        internal static float GetParameter(string name, float defaultValue)
        {
            ResultSet result = settingsDatabase.ExecuteQuery("SELECT value FROM Parameter WHERE Name='" + name + "'");

            if (result.RowCount < 1)
            {
                return defaultValue;
            }
            else
            {
                return (float)double.Parse((string)((ArrayList)result.Data[0])[0]);
            }
        }

        internal static int GetParameter(string name, int defaultValue)
        {
            ResultSet result = settingsDatabase.ExecuteQuery("SELECT value FROM Parameter WHERE Name='" + name + "'");

            if (result.RowCount < 1)
            {
                return defaultValue;
            }
            else
            {
                return (int)double.Parse((string)((ArrayList)result.Data[0])[0]);
            }
        }

        internal static Fertilize_Job[] GetParameter(int Pump_Index)
        {
            string pump_Index = Pump_Index.ToString();
            ResultSet result = settingsDatabase.ExecuteQuery("SELECT Starttime,Quantity FROM Fertilize_Jobs WHERE Pump_Index='" + pump_Index + "'");

            Fertilize_Job[] jobs = new Fertilize_Job[result.RowCount];

            for (int row = 0; row < result.RowCount; row++)
            {
                string startTime = (string)((ArrayList)result.Data[row])[0];
                string hour = startTime.Substring(0, startTime.IndexOf(":"));
                string minute = startTime.Substring(startTime.IndexOf(":") + 1, startTime.Length - startTime.IndexOf(":") - 1);
                string quantity = (string)((ArrayList)result.Data[row])[1];
                DateTime time = new DateTime(0).AddHours(int.Parse(hour)).AddMinutes(int.Parse(minute));
                jobs[row] = new Fertilize_Job(time, int.Parse(quantity));
            }

            return jobs;
        }
    }
}
