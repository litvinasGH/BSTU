using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_12
{
    public delegate void LogInfo(string action, string filename, string? filepath);
    static class KVVLog
    {
        public const string _filepath = "kvvlogfile.txt";

        class LogRecord
        {
            public string? Action { get; set; }
            public string? Filename { get; set; }
            public string? Filepath { get; set; }
            public DateTime? Date { get; set; }
        }

        public static void WriteLog(string action, string filename, string? filepath)
        {
            string record = $"{DateTime.Now}|{action}|{filename}|{filepath}";
            File.AppendAllText(_filepath, record + Environment.NewLine);
        }

        public static void ReadLog()
        {
            if (!File.Exists(_filepath))
            {
                throw new Exception("Лог файл не найден.");
            }

            string[] lines = File.ReadAllLines(_filepath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        public static void SearchLogByAction(string action)
        {
            if (!File.Exists(_filepath))
            {
                throw new Exception("Лог файл не найден.");
            }

            string[] lines = File.ReadAllLines(_filepath);
            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 4 && parts[1] == action)
                {
                    Console.WriteLine($"Дата и время: {parts[0]}, Действие: {parts[1]}, Имя файла: {parts[2]}, Путь файла: {parts[3]}");
                }
            }
        }

        public static void SearchByTimeRange(DateTime from, DateTime to)
        {
            if (!File.Exists(_filepath))
            {
                throw new Exception("Лог файл не найден.");
            }

            string[] lines = File.ReadAllLines(_filepath);


            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 4 && DateTime.TryParse(parts[0], out DateTime logDate))
                {
                    if (logDate >= from && logDate <= to)
                    {
                        Console.WriteLine($"Дата и время: {parts[0]}, Действие: {parts[1]}, Имя файла: {parts[2]}, Путь файла: {parts[3]}");
                    }
                }
            }


        }

        public static void RemoveOldLogs()
        {
            if (!File.Exists(_filepath))
            {
                throw new Exception("Лог файл не найден.");
            }

            string[] lines = File.ReadAllLines(_filepath);
            List<string> updatedLines = new List<string>();

            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 4 && DateTime.TryParse(parts[0], out DateTime logDate))
                {
                    if (logDate >= DateTime.Now.AddHours(-1))
                    {
                        updatedLines.Add(line);
                    }
                }
            }

            File.WriteAllLines(_filepath, updatedLines);
        }

        public static int CountLogs()
        {
            if (!File.Exists(_filepath))
            {
                throw new Exception("Лог файл не найден.");
            }
            string[] lines = File.ReadAllLines(_filepath);
            return lines.Length;
        }
    }
}
