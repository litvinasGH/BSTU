using System.Collections.Concurrent;
using System.Diagnostics;

namespace lab15
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int vectorSize = 100000;
            double[] vector = new double[vectorSize];
            Console.Write("Скаляр: ");
            double scalar = double.Parse(Console.ReadLine());

            for (int i = 0; i < vectorSize; i++)
            {
                vector[i] = i;
                //Console.WriteLine(vector[i]);
            }

            Task<double[]> vectorMultiplicationTask = new Task<double[]>(() =>
            {
                double[] result = new double[vectorSize];
                for (int i = 0; i < vectorSize; i++)
                {
                    result[i] = vector[i] * scalar;
                }
                Console.WriteLine("Выполнено!!!");
                return result;
            });
            var watch = new Stopwatch();
            Console.WriteLine($"Id:{vectorMultiplicationTask.Id},\tStatus: {vectorMultiplicationTask.Status}");


            vectorMultiplicationTask.Start();
            watch.Start();
            Console.WriteLine($"Id:{vectorMultiplicationTask.Id},\tStatus: {vectorMultiplicationTask.Status}");


            vectorMultiplicationTask.Wait();
            watch.Stop();
            Console.WriteLine($"Id:{vectorMultiplicationTask.Id},\tStatus: {vectorMultiplicationTask.Status}");


            Console.WriteLine($"Время выполнения {watch.ElapsedMilliseconds} ms");

            Console.WriteLine("\n\n\n");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Task<double[]> vectorMultiplicationTaskCANC = new Task<double[]>(() =>
            {
                double[] result = new double[vectorSize];
                for (int i = 0; i < vectorSize; i++)
                {
                    result[i] = vector[i] * scalar;
                }
                Console.WriteLine("Выполнено!!!");
                return result;
            }, cancellationTokenSource.Token);
            Console.WriteLine($"Id: {vectorMultiplicationTaskCANC.Id},\tStatus: {vectorMultiplicationTaskCANC.Status}");
            cancellationTokenSource.Cancel();
            Console.WriteLine($"Id: {vectorMultiplicationTaskCANC.Id},\tStatus: {vectorMultiplicationTaskCANC.Status}");

            Console.WriteLine("\n\n\n");



            Task firstTask = new Task(() => Console.Write("1. "));

            Task secondTask = firstTask.ContinueWith(t => Console.WriteLine("...(ContinueWith)"));
            firstTask.Start();
            secondTask.Wait();


            Console.Write("1 - 100: ");
            int num = int.Parse(Console.ReadLine());
            Console.Write("Степень: ");
            int power = int.Parse(Console.ReadLine());

            Task<double> awaitedTask = Task<double>.Run(() =>
            {
                return Math.Pow(num, power);
            });
            var awaiter = awaitedTask.GetAwaiter();
            awaiter.OnCompleted(async () =>
            {
                double result = awaiter.GetResult();
                Console.WriteLine($"Результат: {result}\n");
            });

            Console.WriteLine("\n\n\n");


            var array1 = new int[100000];
            var array2 = new int[100000];
            var array3 = new int[100000];

            void FillWithElements(int index)
            {
                array1[index] = index;
                array2[index] = index;
                array3[index] = index;
            }

            var watch1 = new Stopwatch();
            watch1.Start();
            Parallel.For(0, 100000, FillWithElements);
            watch1.Stop();
            Console.WriteLine($"Parallel.For() время выполнения {watch1.ElapsedMilliseconds} ms");


            watch1.Restart();
            for (int i = 0; i < 100000; i++)
            {
                array1[i] = i;
                array2[i] = i;
                array3[i] = i;
            }
            watch1.Stop();
            Console.WriteLine($"For время выполнения {watch1.ElapsedMilliseconds} ms");

            int accumulator = 0;
            watch1.Restart();
            Parallel.ForEach(array2, GetElemetnsSumm);
            watch1.Stop();
            Console.WriteLine($"Parallel.ForEach() время выполнения {watch1.ElapsedMilliseconds} ms");
            void GetElemetnsSumm(int element)
            {
                accumulator += element;
            }

            watch1.Restart();
            accumulator = 0;
            foreach (int item in array2)
            {
                accumulator += item;
            }
            watch1.Stop();
            Console.WriteLine($"ForeEach время выполнения {watch1.ElapsedMilliseconds} ms");


            Console.WriteLine("\n\n\n");


            var array16 = new int[10000000];
            var array26 = new int[10000000];
            var array36 = new int[10000000];


            Parallel.Invoke
            (

                () =>
                {
                    for (var i = 0; i < array16.Length; i++)
                    {
                        array16[i] = i;
                    }
                },
                () =>
                {
                    for (var i = 0; i < array26.Length; i++)
                    {
                        array26[i] = i;
                    }
                },
                () =>
                {
                    for (var i = 0; i < array36.Length; i++)
                    {
                        array36[i] = i;
                    }
                }
            );

            Console.WriteLine("\n\n\n");


            Console.WriteLine();
            BlockingCollection<string> bc = new BlockingCollection<string>(5);

            //5 suppliers
            Task[] sellers = new Task[5]
            {
                 new Task(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(700);
                        bc.Add("Стиралка");
                    }
                }),
                new Task(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(400);
                        bc.Add("Блендер");
                    }
                }),
                new Task(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(500);
                        bc.Add("Тостер");
                    }
                }),
                new Task(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(300);
                        bc.Add("Микроволновка");
                    }
                }),
                new Task(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(700);
                        bc.Add("Готовый билд");
                    }
                })
            };

            foreach (var i in sellers)
                if (i.Status != TaskStatus.Running)
                    i.Start();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Кол-во: {bc.Count}");
                Thread.Sleep(80);
                Thread thr = new Thread(Customer);
                thr.Start();
            }

            void Customer()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (bc.Count == 0)
                {
                    Console.WriteLine("Ушёл пустым =[");
                    return;
                }
                Console.WriteLine($"Купил {bc.Take()}");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Thread.Sleep(80);
            Console.WriteLine("\n\n\n");

            Thread.Sleep(1000);
            Console.WriteLine();
            int Fibonacci(int n)
            {
                if (n == 0 || n == 1)
                {
                    return n;
                }

                Thread.Sleep(100);
                return Fibonacci(n - 1) + Fibonacci(n - 2);
            }

            async void FibonacciAsync(int n)
            {
                var resultAs = Task<int>.Factory.StartNew(() => Fibonacci(n));
                int value = await resultAs;
                Console.WriteLine($"Result: {value}");
            }

            FibonacciAsync(7);
        }


    }
}