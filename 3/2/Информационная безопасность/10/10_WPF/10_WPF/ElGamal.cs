using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Lab8Crypto
{
    public class ElGamal
    {
        public BigInteger P;
        public BigInteger G;
        public BigInteger X;
        public BigInteger Y;

        // Генерация ключей
        public void GenerateKeys()
        {
            P = 30803;
            G = 2;

            Random rnd = new Random();

            X = rnd.Next(2, 1000);

            Y = BigInteger.ModPow(G, X, P);
        }

        // Шифрование
        public string Encrypt(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            StringBuilder sb = new StringBuilder();

            Random rnd = new Random();

            foreach (byte m in bytes)
            {
                BigInteger k = rnd.Next(2, 1000);

                BigInteger a =
                    BigInteger.ModPow(G, k, P);

                BigInteger b =
                    (BigInteger.ModPow(Y, k, P) * m) % P;

                sb.Append(a);
                sb.Append(",");
                sb.Append(b);
                sb.Append(";");
            }

            return Convert.ToBase64String(
                Encoding.UTF8.GetBytes(sb.ToString()));
        }

        // Расшифрование
        public string Decrypt(string encryptedBase64)
        {
            string encrypted =
                Encoding.UTF8.GetString(
                    Convert.FromBase64String(encryptedBase64));

            string[] pairs =
                encrypted.Split(
                    new[] { ';' },
                    StringSplitOptions.RemoveEmptyEntries);

            byte[] result = new byte[pairs.Length];

            for (int i = 0; i < pairs.Length; i++)
            {
                string[] values = pairs[i].Split(',');

                BigInteger a =
                    BigInteger.Parse(values[0]);

                BigInteger b =
                    BigInteger.Parse(values[1]);

                BigInteger s =
                    BigInteger.ModPow(a, X, P);

                BigInteger sInverse =
                    ModInverse(s, P);

                BigInteger m =
                    (b * sInverse) % P;

                result[i] = (byte)m;
            }

            return Encoding.UTF8.GetString(result);
        }

        // Обратный элемент
        private BigInteger ModInverse(BigInteger a, BigInteger mod)
        {
            return BigInteger.ModPow(a, mod - 2, mod);
        }
    }
}