using System;
using System.Diagnostics;

namespace Lab14
{
    public static class ProcessManager
    {
        public static void ShowProcesses()
        {
            Console.WriteLine("Процессы");
            Console.WriteLine(new string('-', 110));
            var processlist = Process.GetProcesses();

            Console.WriteLine($"{"ID",-10}{"Имя",-50}{"Приоритет",-15}{"Время запуска",-25}{"Состояние",-10}");
            Console.WriteLine(new string('-', 110));

            foreach (var process in processlist)
            {
                try
                {
                    Console.WriteLine($"{process.Id,-10}{process.ProcessName,-50}{process.PriorityClass,-15}" +
                        $"{process.StartTime,-25}{(process.Responding ? "Работает" : "Не отвечает"),-10}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{process.Id,-10}{process.ProcessName,-50}Ошибка - {ex.Message}");
                }
            }
        }

        public static void ShowDomain()
        {
            Console.WriteLine("Домен");
            Console.WriteLine(new string('-', 110));
            AppDomain domain = AppDomain.CurrentDomain;

            Console.WriteLine($"Имя: {domain.FriendlyName}");
            Console.WriteLine($"Конфиг: {domain.SetupInformation}");

            Console.WriteLine("\nСборки, загруженные в домен:");
            Console.WriteLine(new string('-', 110));

            var assemblies = domain.GetAssemblies();
            foreach (var item in assemblies)
            {
                Console.WriteLine(item.GetName().Name);
            }
        }

        public static void CreateNewDomain()
        {
            AppDomain newDomain = AppDomain.CreateDomain("MyNewDomain");
            Console.WriteLine($"Домен создан: {newDomain.FriendlyName}");

            try
            {
                newDomain.Load("System.Text.Json");
                Console.WriteLine("Сборка успешно загружена.");

                Console.WriteLine("Загруженные сборки:");
                foreach (var assembly in newDomain.GetAssemblies())
                {
                    Console.WriteLine($"- {assembly.GetName().Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            finally
            {
                AppDomain.Unload(newDomain);
                Console.WriteLine("Домен выгружен.");
            }

        }
    }
}

