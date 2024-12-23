using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_12
{
    static class KVVFileManager
    {
        public static event LogInfo? WriteLog;
        public static void GetFoldersAndFiles(string dirpath)
        {
            if (!Directory.Exists(dirpath))
            {
                throw new Exception("Путь к папкам не найден");
            }

            DirectoryInfo dirinfo = new DirectoryInfo(dirpath);
            DirectoryInfo subDir = dirinfo.CreateSubdirectory("KVVInspect");
            string filepath = Path.Combine(subDir.FullName, "kvvdirinfo.txt");
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                writer.WriteLine("Список файлов: ");
                foreach (var file in dirinfo.GetFiles())
                {
                    writer.WriteLine(file.Name);
                }
                writer.WriteLine("Список папок: ");
                foreach (var folder in dirinfo.GetDirectories())
                {
                    writer.WriteLine(folder.Name);
                }
            }
            string copyfilepath = Path.Combine(subDir.FullName, "kvvdirinfo_copy.txt");
            File.Copy(filepath, copyfilepath, true);
            File.Delete(filepath);

            if (WriteLog != null)
            {
                WriteLog.Invoke("Получение файлов и папок", dirinfo.Name, filepath);
            }
        }
        public static void CopyWithE(string destination, string source, string extension)
        {
            if (!Directory.Exists(destination))
            {
                throw new Exception("Итоговая папка для перемещния файлов не найден");
            }
            if (!Directory.Exists(source))
            {
                throw new Exception("Исходная папка для перемещения не найдена");
            }

            DirectoryInfo dirinfo = new DirectoryInfo(destination);
            DirectoryInfo subdir = dirinfo.CreateSubdirectory("KVVFiles");

            foreach (string filepath in Directory.GetFiles(source, extension))
            {
                string filename = Path.GetFileName(filepath);
                string copyTo = Path.Combine(subdir.FullName, filename);
                File.Copy(filepath, copyTo, true);
            }

            if (WriteLog != null)
            {
                WriteLog.Invoke("Перемещение файлов", "KVVFiles", subdir.FullName);
            }

        }
        public static void ToZip(string destination, string source, string toExtract)
        {
            if (!Directory.Exists(source))
            {
                throw new Exception("Исходная папка не найдена!!");
            }
            if (!Directory.Exists(toExtract))
            {
                throw new Exception("Папка для разархивирования не найдена");
            }
            if (File.Exists(destination))
            {
                File.Delete(destination);
            }

            ZipFile.CreateFromDirectory(source, destination);
            ZipFile.ExtractToDirectory(destination, toExtract, true);

            if (WriteLog != null)
            {
                string[] parts = destination.Split('\\');
                WriteLog.Invoke("Создание архива", parts.Last(), destination);
            }
        }
    }
}
