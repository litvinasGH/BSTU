using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_12
{
    static class KVVDiskInfo
    {
        public static event LogInfo? WriteLog;
        public static void GetDiskInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Имя: {drive.Name}");
                    Console.WriteLine($"Всего места: {drive.TotalSize}");
                    Console.WriteLine($"Cвободное место на диске: {drive.AvailableFreeSpace}");
                    Console.WriteLine($"Файловая система: {drive.DriveFormat}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                    Console.WriteLine();
                    if (WriteLog != null)
                    {
                        WriteLog.Invoke("Получение информации о диске", drive.Name, drive.Name);
                    }
                }
            }
            Console.WriteLine();



        }
    }
}
