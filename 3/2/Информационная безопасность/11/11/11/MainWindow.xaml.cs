using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace HashApp
{
    public partial class MainWindow : Window
    {
        private string selectedFile = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                selectedFile = ofd.FileName;
                tbFile.Text = "Файл: " + selectedFile;
            }
        }

        private void btnHash_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFile))
            {
                MessageBox.Show("Выберите файл");
                return;
            }

            byte[] data = File.ReadAllBytes(selectedFile);

            int repeats = 1000;

            Stopwatch sw = new Stopwatch();
            byte[] hash = null;

            sw.Start();

            for (int i = 0; i < repeats; i++)
            {
                using (MD5 md5 = MD5.Create())
                {
                    hash = md5.ComputeHash(data);
                }
            }

            sw.Stop();

            string hashString = BitConverter
                .ToString(hash)
                .Replace("-", "")
                .ToLower();

            double seconds = sw.Elapsed.TotalSeconds;
            //double speed = (data.Length * repeats / 1024.0 / 1024.0) / seconds;
            double totalBytes = (double)data.Length * repeats;

            double speed =
                (totalBytes / 1024.0 / 1024.0)
                / sw.Elapsed.TotalSeconds;

            tbResult.Text =
                $"Файл: {selectedFile}\n" +
                $"Размер: {data.Length} байт\n" +
                $"Алгоритм: MD5\n" +
                $"Хеш: {hashString}\n" +
                $"Количество повторов: {repeats}\n" +
                $"Время: {sw.Elapsed.TotalMilliseconds:F3} мс\n" +
                $"Скорость: {speed:F2} MB/s";
        }
    }
}