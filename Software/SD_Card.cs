using Microsoft.SPOT;
using System.IO;

namespace AquaComp
{
    static class SD_Card
    {
        internal static void Initialize()
        {
            // SD Card must be inserted at start
            if (Program.Mainboard.IsSDCardInserted)
            {
                // SD Card must be mounted automatically
                if (Program.Mainboard.IsSDCardMounted)
                {
                    // SD Card must be formated in FAT16 or FAT32
                    if (Program.Mainboard.SDCardStorageDevice.Volume.IsFormatted)
                    {
                        Debug.Print("SD Card initialized.");
                    }
                    else
                    {
                        Debug.Print("SD Card is not formated.");
                    }
                }
                else
                {
                    Debug.Print("SD Card is not mounted.");
                }
            }
            else
            {
                Debug.Print("SD Card is not inserted.");
            }
        }

        internal static string[] GetAllFilesInFolder(string folder)
        {
            return Directory.GetFiles(Program.Mainboard.SDCardStorageDevice.RootDirectory + "\\" + folder);
        }

        internal static byte[] ReadFile(string file)
        {
            return File.ReadAllBytes(Program.Mainboard.SDCardStorageDevice.RootDirectory + "\\" + file);
        }

        internal static string GetSDRoot()
        {
            return Program.Mainboard.SDCardStorageDevice.RootDirectory;
        }
    }
}
