
namespace Lab14
{
    internal class Program
    {

        private static void DisplayTime(object state)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"Текущее время: {DateTime.Now:HH:mm:ss}");
        }
        static void Main(string[] args)
        {
            ProcessManager.ShowProcesses();
            Console.WriteLine("\n\n\n\n\n");
            ProcessManager.ShowDomain();
            //Console.WriteLine("\n\n\n\n\n");
            //ProcessManager.CreateNewDomain();//net 5.0
            Console.WriteLine("\n\n\n\n\n");
            int n;
            int.TryParse(Console.ReadLine(), out n);
            ThreadsManager.PrimeNumbers(n);
            Console.WriteLine("\n\n\n\n\n");
            ThreadsManager.task4(n);
            Console.WriteLine("\n\n\n\n\n");
            ThreadsManager.task4_1_i(n);


            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Таймер запускается...");
            Console.WriteLine("Нажмите S для завершения программы.");

            Timer timer = new Timer(DisplayTime, null, 0, 1000);

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.S)
                {
                    timer.Dispose();
                    Console.SetCursorPosition(0, 3);
                    Console.WriteLine("Таймер остановлен. Программа завершена.");
                    break;
                }
            }


            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }
}