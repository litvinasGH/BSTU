using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<BigInteger> d;
        List<BigInteger> e;
        BigInteger n, a;

        long encTime, decTime;

        public MainWindow()
        {
            InitializeComponent();
        }

        int GetZ()
        {
            return (ModeBox.Text == "ASCII") ? 8 : 6;
        }

        // 🔑 Генерация ключей
        private void GenerateKeys_Click(object sender, RoutedEventArgs e)
        {
            int z = GetZ();
            d = GenerateSuperIncreasing(z);

            (n, a, this.e) = GeneratePublicKey(d);

            MessageBox.Show("Ключи сгенерированы");
        }

        // 🔐 Шифрование
        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            int z = GetZ();
            var sw = Stopwatch.StartNew();

            var cipher = Encrypt(InputBox.Text, this.e, z);

            sw.Stop();
            encTime = sw.ElapsedMilliseconds;

            CipherBox.Text = string.Join(" ", cipher);
            TimeText.Text = $"Encrypt: {encTime} ms";
        }

        // 🔓 Расшифрование
        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            int z = GetZ();

            var cipher = CipherBox.Text.Split(' ')
                .Select(BigInteger.Parse).ToList();

            var sw = Stopwatch.StartNew();

            var text = Decrypt(cipher, d, a, n, z);

            sw.Stop();
            decTime = sw.ElapsedMilliseconds;

            OutputBox.Text = text;
            TimeText.Text += $" | Decrypt: {decTime} ms";
        }

        // 📄 CSV
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            string path = "results.csv";

            var lines = new List<string>
            {
                "Mode,z,Encrypt(ms),Decrypt(ms)",
                $"{ModeBox.Text},{GetZ()},{encTime},{decTime}"
            };

            File.WriteAllLines(path, lines);

            MessageBox.Show("Сохранено в results.csv");
        }

        // ================= ЛОГИКА =================

        List<BigInteger> GenerateSuperIncreasing(int z)
        {
            var rnd = new Random();
            var list = new List<BigInteger>();
            BigInteger sum = 0;

            for (int i = 0; i < z; i++)
            {
                BigInteger next;

                if (i == z - 1)
                    next = BigInteger.Pow(2, 100) + rnd.Next(1, 1000);
                else
                    next = sum + rnd.Next(1, 100);

                list.Add(next);
                sum += next;
            }

            return list;
        }

        (BigInteger, BigInteger, List<BigInteger>) GeneratePublicKey(List<BigInteger> d)
        {
            BigInteger sum = d.Aggregate(BigInteger.Zero, (acc, x) => acc + x);
            BigInteger n = sum + 100;

            BigInteger a = 3;
            while (BigInteger.GreatestCommonDivisor(a, n) != 1)
                a++;

            var e = d.Select(di => (di * a) % n).ToList();

            return (n, a, e);
        }

        List<BigInteger> Encrypt(string text, List<BigInteger> e, int z)
        {
            var result = new List<BigInteger>();

            foreach (char c in text)
            {
                string binary = Convert.ToString(c, 2).PadLeft(z, '0');

                BigInteger sum = 0;

                for (int i = 0; i < z; i++)
                    if (binary[i] == '1')
                        sum += e[i];

                result.Add(sum);
            }

            return result;
        }

        string Decrypt(List<BigInteger> cipher, List<BigInteger> d, BigInteger a, BigInteger n, int z)
        {
            BigInteger aInv = ModInverse(a, n);
            string result = "";

            foreach (var c in cipher)
            {
                BigInteger S = (c * aInv) % n;
                char[] bits = new char[z];

                for (int i = z - 1; i >= 0; i--)
                {
                    if (d[i] <= S)
                    {
                        bits[i] = '1';
                        S -= d[i];
                    }
                    else
                        bits[i] = '0';
                }

                int val = Convert.ToInt32(new string(bits), 2);
                result += (char)val;
            }

            return result;
        }

        BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m, t, q;
            BigInteger x0 = 0, x1 = 1;

            while (a > 1)
            {
                q = a / m;
                t = m;

                m = a % m;
                a = t;
                t = x0;

                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0)
                x1 += m0;

            return x1;
        }
    }
}
