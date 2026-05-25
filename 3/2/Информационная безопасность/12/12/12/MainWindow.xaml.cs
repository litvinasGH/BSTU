using System;
using System.Windows;

namespace EdsaLabWpf
{
    public partial class MainWindow : Window
    {
        private ISignatureAlgorithm algorithm;

        public MainWindow()
        {
            InitializeComponent();

            AlgorithmBox.ItemsSource =
                Enum.GetValues(
                    typeof(SignatureAlgorithmKind));

            AlgorithmBox.SelectedIndex = 0;

            algorithm =
                SignatureFactory.Create(
                    SignatureAlgorithmKind.RSA);
        }

        private void GenerateKeys_Click(
            object sender,
            RoutedEventArgs e)
        {
            int bits =
                int.Parse(BitsBox.Text);

            algorithm.GenerateKeys(bits);

            KeyInfo info =
                algorithm.GetKeyInfo();

            PublicKeyBox.Text =
                info.PublicKeyText;

            PrivateKeyBox.Text =
                info.PrivateKeyText;
        }

        private void Sign_Click(
            object sender,
            RoutedEventArgs e)
        {
            TimeSpan time;

            string sign =
                algorithm.Sign(
                    MessageBox.Text,
                    out time);

            SignatureBox.Text = sign;

            SignTimeBox.Text =
                time.TotalMilliseconds.ToString("F3") + " мс";
        }

        private void Verify_Click(
            object sender,
            RoutedEventArgs e)
        {
            TimeSpan time;

            bool ok =
                algorithm.Verify(
                    MessageBox.Text,
                    SignatureBox.Text,
                    out time);

            VerifyBox.Text =
                ok.ToString() + "\n" +
                time.TotalMilliseconds.ToString("F3") + " мс";
        }
    }
}