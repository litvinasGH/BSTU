using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace _8
{
    public partial class Rc4Window : Window
    {
        private readonly byte[] key = { 76, 111, 85, 54, 211 };

        public Rc4Window()
        {
            InitializeComponent();
            ProgressBar.Value = 0;
            UpdateInputInfo();
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                InputText.Text = File.ReadAllText(dialog.FileName, Encoding.UTF8);
                UpdateInputInfo();
            }
        }

        private void InputText_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateInputInfo();
        }

        private void UpdateInputInfo()
        {
            string text = InputText.Text ?? string.Empty;
            InputInfoText.Text = $"Входное поле: {text.Length} символов, {Encoding.UTF8.GetByteCount(text)} байт (UTF-8)";
        }

        private async void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            await ProcessAsync(true);
        }

        private async void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            await ProcessAsync(false);
        }

        private async Task ProcessAsync(bool encrypt)
        {
            string input = InputText.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Введите текст или загрузите файл.");
                return;
            }

            byte[] data;

            if (encrypt)
            {
                data = Encoding.UTF8.GetBytes(input);
                PlainInfoText.Text = $"Открытый текст: {input.Length} символов, {data.Length} байт (UTF-8)";
            }
            else
            {
                try
                {
                    data = Convert.FromBase64String(input.Trim());
                }
                catch
                {
                    MessageBox.Show("Для расшифрования в поле ввода должен быть Base64-шифртекст.");
                    return;
                }

                CipherInfoText.Text = $"Шифртекст: {input.Trim().Length} символов Base64, {data.Length} байт";
            }

            ProgressBar.Value = 0;
            TimeText.Text = "Выполнение...";

            var sw = Stopwatch.StartNew();
            var progress = new Progress<int>(value => ProgressBar.Value = value);

            byte[] result = await Task.Run(() => RC4(data, key, progress));

            sw.Stop();

            if (encrypt)
            {
                string cipherBase64 = Convert.ToBase64String(result);
                OutputText.Text = cipherBase64;
                CipherInfoText.Text = $"Шифртекст: {cipherBase64.Length} символов Base64, {result.Length} байт";
            }
            else
            {
                string plainText = Encoding.UTF8.GetString(result);
                OutputText.Text = plainText;
                PlainInfoText.Text = $"Открытый текст: {plainText.Length} символов, {result.Length} байт (UTF-8)";
            }

            ProgressBar.Value = 100;
            TimeText.Text = $"Время выполнения: {sw.ElapsedMilliseconds} мс";
        }

        private byte[] RC4(byte[] data, byte[] key, IProgress<int> progress)
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

            progress?.Report(10);

            // PRGA
            int iIndex = 0;
            j = 0;
            byte[] result = new byte[data.Length];

            int step = Math.Max(1, data.Length / 90);

            for (int k = 0; k < data.Length; k++)
            {
                iIndex = (iIndex + 1) % 256;
                j = (j + S[iIndex]) % 256;

                Swap(S, iIndex, j);

                int t = (S[iIndex] + S[j]) % 256;
                int keyStream = S[t];

                result[k] = (byte)(data[k] ^ keyStream);

                if (k % step == 0)
                {
                    int percent = 10 + (int)(90.0 * (k + 1) / data.Length);
                    progress?.Report(Math.Min(100, percent));
                }
            }

            progress?.Report(100);
            return result;
        }
        private void SaveResult_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(OutputText.Text))
            {
                MessageBox.Show("Нет данных для сохранения");
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FileName = "result.txt";

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, OutputText.Text);
                MessageBox.Show("Файл сохранён");
            }
        }

        private void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
