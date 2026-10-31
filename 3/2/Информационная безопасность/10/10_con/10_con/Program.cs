using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Значения a
        int[] aValues = { 5, 15, 25, 35 };

        // Простые x
        BigInteger[] xValues =
        {
            BigInteger.Parse("1013"),
            BigInteger.Parse("10007"),
            BigInteger.Parse("100003"),
            BigInteger.Parse("1000003"),
            BigInteger.Parse("10000019")
        };

        // Генерация n
        BigInteger n1024 = GenerateBigInteger(1024);
        BigInteger n2048 = GenerateBigInteger(2048);

        Console.WriteLine("ИССЛЕДОВАНИЕ ВРЕМЕНИ ВЫЧИСЛЕНИЯ y = a^x mod n");
        Console.WriteLine(new string('-', 100));

        // Файл для 1024 бит
        string file1024 = "results_1024.csv";

        // Файл для 2048 бит
        string file2048 = "results_2048.csv";

        // Заголовки CSV
        CreateCsvHeader(file1024);
        CreateCsvHeader(file2048);

        // Тест 1024
        TestValues(aValues, xValues, n1024, 1024, file1024);

        Console.WriteLine();

        // Тест 2048
        TestValues(aValues, xValues, n2048, 2048, file2048);

        Console.WriteLine("\nCSV файлы созданы:");
        Console.WriteLine("results_1024.csv");
        Console.WriteLine("results_2048.csv");

        Console.WriteLine("\nНажмите любую клавишу...");
        Console.ReadKey();
    }

    static void TestValues(
        int[] aValues,
        BigInteger[] xValues,
        BigInteger n,
        int bits,
        string csvFile)
    {
        Console.WriteLine($"\nn = {bits} бит");
        Console.WriteLine(new string('-', 100));

        Console.WriteLine(
            $"{"a",-10}" +
            $"{"x",-20}" +
            $"{"Время (мс)",-20}" +
            $"{"y",-35}");

        Console.WriteLine(new string('-', 100));

        foreach (int a in aValues)
        {
            foreach (BigInteger x in xValues)
            {
                Stopwatch sw = Stopwatch.StartNew();

                // y = a^x mod n
                BigInteger y = BigInteger.ModPow(a, x, n);

                sw.Stop();

                double time = sw.Elapsed.TotalMilliseconds;

                // Вывод в консоль
                Console.WriteLine(
                    $"{a,-10}" +
                    $"{ShortNumber(x),-20}" +
                    $"{time,-20:F6}" +
                    $"{ShortNumber(y),-35}");

                // Запись в CSV
                string csvLine =
                    $"{a};" +
                    $"{x};" +
                    $"{time:F6};" +
                    $"{y}";

                File.AppendAllText(
                    csvFile,
                    csvLine + Environment.NewLine);
            }
        }
    }

    static void CreateCsvHeader(string file)
    {
        File.WriteAllText(
            file,
            "a;x;time_ms;y_result\n",
            Encoding.UTF8);
    }

    // Генерация большого числа
    static BigInteger GenerateBigInteger(int bits)
    {
        byte[] bytes = new byte[bits / 8];

        RandomNumberGenerator.Fill(bytes);

        // Старший бит = 1
        bytes[0] |= 0x80;

        // Нечетное число
        bytes[^1] |= 1;

        return new BigInteger(
            bytes,
            isUnsigned: true,
            isBigEndian: true);
    }

    // Сокращение длинных чисел
    static string ShortNumber(BigInteger value)
    {
        string s = value.ToString();

        if (s.Length <= 20)
            return s;

        return s.Substring(0, 8)
               + "..."
               + s.Substring(s.Length - 8);
    }
}