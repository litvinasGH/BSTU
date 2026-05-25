using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Lab8Crypto
{
    public partial class MainWindow : Window
    {
        private RSA rsa;

        private ElGamal elGamal =
            new ElGamal();

        public MainWindow()
        {
            InitializeComponent();

            RsaInputBox.Text =
                "Качинскас Вацловас Вацловович";

            ElInputBox.Text =
                "Качинскас Вацловас Вацловович";
        }

        // =========================
        // RSA
        // =========================

        private void GenerateRSAKeys_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rsa = RSA.Create(2048);

                MessageBox.Show(
                    "RSA ключи успешно сгенерированы");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EncryptRSA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rsa == null)
                {
                    MessageBox.Show(
                        "Сначала создайте RSA ключи");
                    return;
                }

                string text =
                    RsaInputBox.Text;

                byte[] data =
                    Encoding.UTF8.GetBytes(text);

                Stopwatch sw =
                    Stopwatch.StartNew();

                byte[] encrypted =
                    rsa.Encrypt(
                        data,
                        RSAEncryptionPadding.OaepSHA256);

                sw.Stop();

                string base64 =
                    Convert.ToBase64String(encrypted);

                RsaEncryptedBox.Text =
                    base64;

                RsaTimeBox.Text =
                    $"RSA шифрование: " +
                    $"{sw.Elapsed.TotalMilliseconds:F4} мс\n" +
                    $"Размер исходного текста: {data.Length} байт\n" +
                    $"Размер шифротекста: {encrypted.Length} байт";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DecryptRSA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rsa == null)
                {
                    MessageBox.Show(
                        "Сначала создайте RSA ключи");
                    return;
                }

                byte[] encrypted =
                    Convert.FromBase64String(
                        RsaEncryptedBox.Text);

                Stopwatch sw =
                    Stopwatch.StartNew();

                byte[] decrypted =
                    rsa.Decrypt(
                        encrypted,
                        RSAEncryptionPadding.OaepSHA256);

                sw.Stop();

                string text =
                    Encoding.UTF8.GetString(decrypted);

                RsaDecryptedBox.Text =
                    text;

                RsaTimeBox.Text =
                    $"RSA расшифрование: " +
                    $"{sw.Elapsed.TotalMilliseconds:F4} мс";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =========================
        // ЭЛЬ-ГАМАЛЬ
        // =========================

        private void GenerateElGamalKeys_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                elGamal.GenerateKeys();

                MessageBox.Show(
                    "Ключи Эль-Гамаля успешно сгенерированы");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EncryptElGamal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text =
                    ElInputBox.Text;

                byte[] source =
                    Encoding.UTF8.GetBytes(text);

                Stopwatch sw =
                    Stopwatch.StartNew();

                string encrypted =
                    elGamal.Encrypt(text);

                sw.Stop();

                ElEncryptedBox.Text =
                    encrypted;

                byte[] encryptedBytes =
                    Convert.FromBase64String(encrypted);

                ElTimeBox.Text =
                    $"Эль-Гамаль шифрование: " +
                    $"{sw.Elapsed.TotalMilliseconds:F4} мс\n" +
                    $"Размер исходного текста: {source.Length} байт\n" +
                    $"Размер шифротекста: {encryptedBytes.Length} байт";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DecryptElGamal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Stopwatch sw =
                    Stopwatch.StartNew();

                string decrypted =
                    elGamal.Decrypt(
                        ElEncryptedBox.Text);

                sw.Stop();

                ElDecryptedBox.Text =
                    decrypted;

                ElTimeBox.Text =
                    $"Эль-Гамаль расшифрование: " +
                    $"{sw.Elapsed.TotalMilliseconds:F4} мс";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}