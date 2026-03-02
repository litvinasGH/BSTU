namespace Lab14
{
    public static class ThreadsManager
    {
        public static void PrimeNumbers(int n)
        {
            Thread thread = new Thread(PrimeNumbersOutput);
            thread.Name = "primeNumbers";
            thread.Start();
            while (thread.IsAlive)
            {
                Console.WriteLine($"\nИнформация о потоке:");
                Console.WriteLine($"Имя: {thread.Name}");
                Console.WriteLine($"Статус: {(thread.IsAlive ? "Работает" : "Остановлен")}");
                Console.WriteLine($"Приоритет: {thread.Priority}");
                Console.WriteLine($"Идентификатор: {thread.ManagedThreadId}");
                Thread.Sleep(1000);


                //Console.WriteLine("\nВведите команду для управления потоком:");
                //Console.WriteLine("1 - Пауза, 2 - Возобновление, 3 - Завершение");
                //string command = Console.ReadLine();

                //switch (command)
                //{
                //    case "1":
                //        thread.Suspend();
                //        break;
                //    case "2":
                //        thread.Resume();
                //        break;
                //    case "3":
                //        thread.Abort();
                //        return;
                //    default:
                //        Console.WriteLine("Неверная команда.");
                //        break;
                //}
                //net 50
            }

            void PrimeNumbersOutput()
            {
                List<int> numbers = new List<int>();
                for (int i = 2; i < n; i++)
                {
                    numbers.Add(i);
                    //Console.WriteLine("Выполняется второй поток");
                }

                for (int i = 0; i < numbers.Count; i++)
                {
                    for (int j = 2; j < n; j++)
                    {
                        numbers.Remove(numbers[i] * j);
                        //Console.WriteLine("Выполняется второй поток");
                    }
                }

                string primes = string.Join(",", numbers);

                Console.WriteLine(primes);

                string filePath = "prime_numbers.txt";
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine(primes);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
                }
            }
        }

        public static void task4(int n)
        {

            using (StreamWriter writer = new StreamWriter("file.txt"))
            {
                bool thread1turn = false;
                Thread thread1 = new Thread(task4_1);
                Thread thread2 = new Thread(task4_2);



                thread1.Priority = ThreadPriority.Highest;

                thread1.Start();
                thread2.Start();
                while (thread1.IsAlive || thread2.IsAlive)
                {
                    Thread.Sleep(1000);
                }



                void task4_1()
                {
                    for (int i = 0; i <= n; i += 2)
                    {
                        while (!thread1turn)
                        {
                            Thread.Sleep(20);
                        }
                        Console.WriteLine(i);
                        writer.WriteLine(i);

                        Thread.Sleep(100);
                        thread1turn = false;
                    }
                }
                void task4_2()
                {
                    for (int i = 1; i <= n; i += 2)
                    {
                        while (thread1turn)
                        {
                            Thread.Sleep(20);

                        }
                        Console.WriteLine(i);
                        writer.WriteLine(i);

                        Thread.Sleep(200);

                        thread1turn = true;
                    }
                    while (thread1.IsAlive)
                    {
                        thread1turn = true;
                    }
                }
            }
        }
        public static void task4_1_i(int n)
        {
            using (StreamWriter writer = new StreamWriter("file.txt"))
            {
                Thread thread1 = new Thread(task4_1);
                Thread thread2 = new Thread(task4_2);

                thread1.Priority = ThreadPriority.Highest;
                thread1.Start();
                thread2.Start();

                while (thread1.IsAlive || thread2.IsAlive)
                {
                    Thread.Sleep(1000);
                }


                void task4_1()
                {
                    for (int i = 0; i <= n; i += 2)
                    {
                        Console.WriteLine(i);
                        writer.WriteLine(i);

                        Thread.Sleep(100);
                    }
                }
                void task4_2()
                {
                    thread1.Join();
                    for (int i = 1; i <= n; i += 2)
                    {

                        Console.WriteLine(i);
                        writer.WriteLine(i);

                        Thread.Sleep(200);
                    }
                }
            }
        }
    }
}
