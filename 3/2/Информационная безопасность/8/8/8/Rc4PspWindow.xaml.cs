using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _8
{
    public partial class Rc4PspWindow : Window
    {
        public Rc4PspWindow()
        {
            InitializeComponent();
        }

        private async void Generate_Click(object sender, RoutedEventArgs e)
        {
            byte[] key;

            try
            {
                key = KeyBox.Text.Split(',')
                    .Select(x => byte.Parse(x.Trim()))
                    .ToArray();
            }
            catch
            {
                MessageBox.Show("Ошибка ключа");
                return;
            }

            if (!int.TryParse(LengthBox.Text, out int length))
                length = 20;

            var sw = Stopwatch.StartNew();
            ProgressBar.Value = 0;

            var progress = new Progress<int>(v => ProgressBar.Value = v);

            byte[] result = await Task.Run(() => GenerateRC4(length, key, progress));

            sw.Stop();

            OutputText.Text = string.Join(" ", result);
            InfoText.Text = $"Размер: {result.Length} байт";
            TimeText.Text = $"Время: {sw.ElapsedMilliseconds} мс";
        }

        private byte[] GenerateRC4(int length, byte[] key, IProgress<int> progress)
        {
            int[] S = new int[256];
            for (int i = 0; i < 256; i++)
                S[i] = i;

            int j = 0;

            // KSA
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % key.Length]) % 256;
                Swap(S, i, j);
            }

            // PRGA
            int iIndex = 0;
            j = 0;

            byte[] output = new byte[length];

            for (int k = 0; k < length; k++)
            {
                iIndex = (iIndex + 1) % 256;
                j = (j + S[iIndex]) % 256;

                Swap(S, iIndex, j);

                int t = (S[iIndex] + S[j]) % 256;
                output[k] = (byte)S[t];

                progress.Report((k + 1) * 100 / length);
            }

            return output;
        }

        private void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}