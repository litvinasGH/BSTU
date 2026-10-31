using System;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace EdsaLabWpf
{
    public interface ISignatureAlgorithm
    {
        string Name { get; }

        void GenerateKeys(int bitLength);

        KeyInfo GetKeyInfo();

        string Sign(string message, out TimeSpan elapsed);

        bool Verify(
            string message,
            string signature,
            out TimeSpan elapsed);
    }

    public static class CryptoMath
    {
        public static BigInteger Hash(string text)
        {
            SHA256 sha = SHA256.Create();

            byte[] hash =
                sha.ComputeHash(
                    Encoding.UTF8.GetBytes(text));

            byte[] positive =
                new byte[hash.Length + 1];

            Array.Copy(hash, positive, hash.Length);

            positive[positive.Length - 1] = 0;

            return new BigInteger(positive);
        }

        public static BigInteger ModInverse(
            BigInteger a,
            BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger y = 0;
            BigInteger x = 1;

            if (m == 1)
                return 0;

            while (a > 1)
            {
                BigInteger q = a / m;

                BigInteger t = m;

                m = a % m;
                a = t;

                t = y;

                y = x - q * y;
                x = t;
            }

            if (x < 0)
                x += m0;

            return x;
        }

        public static BigInteger GeneratePrime(int bits)
        {
            Random rnd = new Random();

            while (true)
            {
                byte[] bytes = new byte[bits / 8];

                rnd.NextBytes(bytes);

                bytes[bytes.Length - 1] |= 1;

                BigInteger p = new BigInteger(bytes);

                if (p < 0)
                    p = -p;

                if (IsPrime(p))
                    return p;
            }
        }

        public static bool IsPrime(BigInteger n, int k = 5)
        {
            if (n < 2)
                return false;

            if (n == 2 || n == 3)
                return true;

            if (n % 2 == 0)
                return false;

            BigInteger d = n - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            Random rnd = new Random();

            byte[] bytes = new byte[n.ToByteArray().LongLength];

            for (int i = 0; i < k; i++)
            {
                BigInteger a;

                do
                {
                    rnd.NextBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= n - 2);

                BigInteger x = BigInteger.ModPow(a, d, n);

                if (x == 1 || x == n - 1)
                    continue;

                bool continueOuter = false;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, n);

                    if (x == n - 1)
                    {
                        continueOuter = true;
                        break;
                    }
                }

                if (continueOuter)
                    continue;

                return false;
            }

            return true;
        }
    }

    public class RsaSignature : ISignatureAlgorithm
    {
        private BigInteger p;
        private BigInteger q;
        private BigInteger n;
        private BigInteger phi;
        private BigInteger e;
        private BigInteger d;

        public string Name
        {
            get { return "RSA"; }
        }

        public void GenerateKeys(int bitLength)
        {
            p = CryptoMath.GeneratePrime(bitLength / 2);
            q = CryptoMath.GeneratePrime(bitLength / 2);

            n = p * q;

            phi = (p - 1) * (q - 1);

            e = 65537;

            d = CryptoMath.ModInverse(e, phi);
        }

        public KeyInfo GetKeyInfo()
        {
            return new KeyInfo(
                "n = " + n + "\ne = " + e,
                "d = " + d);
        }

        public string Sign(
            string message,
            out TimeSpan elapsed)
        {
            Stopwatch sw = Stopwatch.StartNew();

            BigInteger hash =
                CryptoMath.Hash(message);

            BigInteger sign =
                BigInteger.ModPow(hash, d, n);

            sw.Stop();

            elapsed = sw.Elapsed;

            return sign.ToString("X");
        }

        public bool Verify(
            string message,
            string signature,
            out TimeSpan elapsed)
        {
            Stopwatch sw = Stopwatch.StartNew();

            BigInteger hash =
                CryptoMath.Hash(message);

            BigInteger s =
                BigInteger.Parse(signature);

            BigInteger check =
                BigInteger.ModPow(s, e, n);

            sw.Stop();

            elapsed = sw.Elapsed;

            return hash == check;
        }
    }

    public static class SignatureFactory
    {
        public static ISignatureAlgorithm Create(
            SignatureAlgorithmKind kind)
        {
            return new RsaSignature();
        }
    }
}