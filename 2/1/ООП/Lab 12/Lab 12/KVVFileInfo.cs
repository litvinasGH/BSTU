using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_12
{
    static class KVVFileInfo
    {
        public static event LogInfo? WriteLog;
        public static void GetFileInfo(string filepath)
        {
            FileInfo info = new FileInfo(filepath);
            if (info.Exists)
            {
                Console.WriteLine("Полный путь: " + info.DirectoryName);
                Console.WriteLine("Размер: " + info.Length + "\nИмя: " + info.Name + "\nРасширение: " + info.Extension);
                Console.WriteLine("Дата создания: " + info.CreationTime + "\nДата изменения: " + info.LastWriteTime);
            }
            Console.WriteLine();
            if (WriteLog != null)
            {
                WriteLog.Invoke("Получение информации о файле", info.Name, info.DirectoryName);
            }

        }
    }
}
