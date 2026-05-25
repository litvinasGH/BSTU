using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _8
{
    public partial class RsaWindow : Window
    {
        private BigInteger p, q, n, phi, e;

        public RsaWindow()
        {
            InitializeComponent();
        }

        // Генерация p и q (АСИНХРОННО)
        private async void Generate_Click(object sender, RoutedEventArgs eClick)
        {
            ProgressBar.Value = 0;
            TimeText.Text = "Генерация...";

            var sw = Stopwatch.StartNew();

            await Task.Run(() =>
            {
                p = GeneratePrime(256);
                q = GeneratePrime(256);

                n = p * q;
                phi = (p - 1) * (q - 1);
                e = 65537;
            });

            sw.Stop();

            ParamsText.Text =
                $"p = {p}\n\nq = {q}\n\nn = {n}\n\ne = {e}";

            ProgressBar.Value = 100;
            TimeText.Text = $"Время: {sw.ElapsedMilliseconds} мс";
        }

        // Генерация ПСП (АСИНХРОННО)
        private async void GenerateSequence_Click(object sender, RoutedEventArgs eClick)
        {
            if (n == 0)
            {
                MessageBox.Show("Сначала сгенерируй параметры!");
                return;
            }

            if (!BigInteger.TryParse(SeedBox.Text, out BigInteger x))
            {
                MessageBox.Show("Ошибка seed");
                return;
            }

            int count = int.Parse(CountBox.Text);

            ProgressBar.Value = 0;
            TimeText.Text = "Генерация ПСП...";

            var sw = Stopwatch.StartNew();

            IProgress<int> progress = new Progress<int>(v => ProgressBar.Value = v);

            StringBuilder result = await Task.Run(() =>
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < count; i++)
                {
                    x = BigInteger.ModPow(x, e, n);
                    sb.AppendLine(x.ToString());

                    int percent = (int)((i + 1) * 100.0 / count);
                    progress.Report(percent);
                }

                return sb;
            });

            sw.Stop();

            OutputText.Text = result.ToString();

            int bytes = Encoding.UTF8.GetByteCount(OutputText.Text);
            InfoText.Text = $"Размер ПСП: {bytes} байт";

            TimeText.Text = $"Время: {sw.ElapsedMilliseconds} мс";
        }

        // Сохранение
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, OutputText.Text);
            }
        }

        // ===== RSA =====

        private BigInteger GeneratePrime(int bits)
        {
            while (true)
            {
                BigInteger num = RandomBigInteger(bits);

                if (num % 2 == 0)
                    num += 1;

                if (IsProbablyPrime(num, 10))
                    return num;
            }
        }

        private BigInteger RandomBigInteger(int bits)
        {
            byte[] bytes = new byte[bits / 8];

            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(bytes);

            bytes[bytes.Length - 1] |= 1;
            return new BigInteger(bytes);
        }

        private bool IsProbablyPrime(BigInteger n, int k)
        {
            if (n < 2) return false;
            if (n % 2 == 0) return n == 2;

            BigInteger d = n - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[n.ToByteArray().Length];

                for (int i = 0; i < k; i++)
                {
                    BigInteger a;

                    do
                    {
                        rng.GetBytes(bytes);
                        a = new BigInteger(bytes);
                    }
                    while (a < 2 || a >= n - 2);

                    BigInteger x = BigInteger.ModPow(a, d, n);

                    if (x == 1 || x == n - 1)
                        continue;

                    bool cont = false;

                    for (int r = 1; r < s; r++)
                    {
                        x = BigInteger.ModPow(x, 2, n);

                        if (x == n - 1)
                        {
                            cont = true;
                            break;
                        }
                    }

                    if (!cont)
                        return false;
                }
            }

            return true;
        }
    }
}
