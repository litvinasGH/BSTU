using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_12
{
    static class KVVDirInfo
    {
        public static event LogInfo? WriteLog;
        public static void GetDirectoryInfo(string dirpath)
        {
            DirectoryInfo info = new DirectoryInfo(dirpath);
            if (info.Exists)
            {
                Console.WriteLine("Кол-во файлов: " + info.GetFiles().Length);
                Console.WriteLine("Время создания: " + info.CreationTime);
                Console.WriteLine("Кол-во поддиректориев: " + info.GetDirectories().Length);
                Console.WriteLine("Список родительских директореив: " + info.Parent);
                Console.WriteLine();
            }
            if (WriteLog != null)
            {
                WriteLog.Invoke("Получение информации о директории", info.Name, info.FullName);
            }
        }
    }
}
