using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_12
{
    class Program
    {
        static void Main(string[] args)
        {
            KVVDiskInfo.WriteLog += KVVLog.WriteLog;
            KVVFileInfo.WriteLog += KVVLog.WriteLog;
            KVVDirInfo.WriteLog += KVVLog.WriteLog;
            KVVFileManager.WriteLog += KVVLog.WriteLog;
            Console.WriteLine("------Получение-информации-о-диске------");
            KVVDiskInfo.GetDiskInfo();
            Console.WriteLine("\n\n------Получение-информации-о-файле------");
            KVVFileInfo.GetFileInfo(@"kvvlogfile.txt");
            Console.WriteLine("\n\n----Получение-информации-о-директории---");
            KVVDirInfo.GetDirectoryInfo(@"KVV");
            Console.WriteLine("\n\n----Получение-информации-о-содержании---");
            KVVFileManager.GetFoldersAndFiles(@"KVV");
            Console.WriteLine("\n\n------------Получение-копии-------------");
            KVVFileManager.CopyWithE(@"KVV\HI_copy", @"KVV\HI", "*.txt");
            Console.WriteLine("\n\n--------Архивация-и-разархивация--------");
            KVVFileManager.ToZip(@"KVV\archLinux.zip", @"KVV\HI_copy", @"KVV\HI_new_copy");

            Console.WriteLine("\n\n-------Поиск-по-\"Создание архива\"-------");
            KVVLog.SearchLogByAction("Создание архива");
            Console.WriteLine("\n\n-----Поиск-по-времени(последний-час)----");
            KVVLog.SearchByTimeRange(DateTime.Now.AddHours(-1), DateTime.Now);
            Console.WriteLine("\n\n----------Чистка-старых-логов-----------");
            KVVLog.RemoveOldLogs();
            Console.WriteLine("\n\n--------------Вывод-логов---------------");
            KVVLog.ReadLog();
            Console.WriteLine("\n\n-------------Подсчёт-логов--------------");
            Console.WriteLine($"Кол-во записей: {KVVLog.CountLogs()}");
        }
    }
}
