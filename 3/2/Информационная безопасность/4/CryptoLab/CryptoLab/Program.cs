using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;


class Program
{
    private const string Alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
    private const int A = 7;
    private const int B = 10;
    private const string path = "input.txt";
    private const string key = "качинскас";

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

            

        string text = File.ReadAllText(path, Encoding.UTF8);


        // --- ШИФРОВАНИЕ Цезарь ---
        var sw1 = Stopwatch.StartNew();

        string encrypted = AffineEncrypt(text);

        sw1.Stop();

        // --- РАСШИФРОВАНИЕ Цезарь ---
        var sw2 = Stopwatch.StartNew();

        string decrypted = AffineDecrypt(encrypted);

        sw2.Stop();

        Console.WriteLine($"AffineEncrypt: {sw1.ElapsedMilliseconds} ms");
        Console.WriteLine($"AffineDecrypt: {sw2.ElapsedMilliseconds} ms");

        File.WriteAllText("AffineEncrypted.txt", encrypted, Encoding.UTF8);
        File.WriteAllText("AffineDecrypted.txt", decrypted, Encoding.UTF8);

        // --- ШИФРОВАНИЕ Винжера ---
        var sw3 = Stopwatch.StartNew();

        string encryptedV = VigenereEncrypt(text, key);

        sw3.Stop();

        // --- РАСШИФРОВАНИЕ Винжера ---
        var sw4 = Stopwatch.StartNew();

        string decryptedV = VigenereDecrypt(encryptedV, key);

        sw4.Stop();

        Console.WriteLine($"VigenereEncrypt: {sw3.ElapsedMilliseconds} ms");
        Console.WriteLine($"VigenereDecrypt: {sw4.ElapsedMilliseconds} ms");

        File.WriteAllText("VigenereEncrypted.txt", encryptedV, Encoding.UTF8);
        File.WriteAllText("VigenereDecrypted.txt", decryptedV, Encoding.UTF8);

        var origFreq = Analyze(text);
        var encFreq = Analyze(encrypted);
        var encVFreq = Analyze(encryptedV);

        var origRel = AnalyzeRelative(origFreq);
        var encRel = AnalyzeRelative(encFreq);
        var encVRel = AnalyzeRelative(encVFreq);


        File.WriteAllText("freq_caesar.csv",
            BuildCsvFull(origFreq, encFreq, origRel, encRel),
            Encoding.UTF8);

        File.WriteAllText("freq_vigenere.csv",
            BuildCsvFull(origFreq, encVFreq, origRel, encVRel),
            Encoding.UTF8);


        File.WriteAllText("benchmark.csv",
            BuildBenchmark(text, key),
            Encoding.UTF8);
    }




    // АФФИННЫЙ ЦЕЗАРЬ


    static string AffineEncrypt(string text)
    {
        var sb = new StringBuilder();

        foreach (char ch in text)
            sb.Append(TransformAffine(ch, A, B, true));

        return sb.ToString();
    }

    static string AffineDecrypt(string text)
    {
        int m = Alphabet.Length;
        int aInv = ModInverse(A, m);

        var sb = new StringBuilder();

        foreach (char ch in text)
            sb.Append(TransformAffine(ch, aInv, B, false));

        return sb.ToString();
    }

    static char TransformAffine(char ch, int a, int b, bool encrypt)
    {
        bool upper = char.IsUpper(ch);
        char lower = char.ToLower(ch);

        int index = Alphabet.IndexOf(lower);
        if (index == -1) return ch;

        int m = Alphabet.Length;
        int newIndex = encrypt
            ? (a * index + b) % m
            : Mod(a * (index - b), m);

        char res = Alphabet[newIndex];
        return upper ? char.ToUpper(res) : res;
    }


    // ВИЖЕНЕР


    static string VigenereEncrypt(string text, string key)
    {
        var sb = new StringBuilder();
        int ki = 0;

        foreach (char ch in text)
            sb.Append(TransformVigenere(ch, key, ref ki, true));

        return sb.ToString();
    }

    static string VigenereDecrypt(string text, string key)
    {
        var sb = new StringBuilder();
        int ki = 0;

        foreach (char ch in text)
            sb.Append(TransformVigenere(ch, key, ref ki, false));

        return sb.ToString();
    }

    static char TransformVigenere(char ch, string key, ref int ki, bool encrypt)
    {
        bool upper = char.IsUpper(ch);
        char lower = char.ToLower(ch);

        int textPos = Alphabet.IndexOf(lower);
        if (textPos == -1) return ch;

        int keyPos = Alphabet.IndexOf(key[ki % key.Length]);
        int m = Alphabet.Length;

        int newPos = encrypt
            ? (textPos + keyPos) % m
            : Mod(textPos - keyPos, m);

        ki++;

        char res = Alphabet[newPos];
        return upper ? char.ToUpper(res) : res;
    }


    // ЧАСТОТЫ

    
    static Dictionary<string, int> Analyze(string text)
    {
        var dict = new Dictionary<string, int>();

        foreach (char c in Alphabet)
            dict[c.ToString()] = 0;

        foreach (char ch in text.ToLower())
        {
            string s = ch.ToString();
            if (dict.ContainsKey(s))
                dict[s]++;
        }

        return dict;
    }

    // ВСПОМОГАТЕЛЬНЫЕ

    static int Mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }

    static int ModInverse(int a, int m)
    {
        int t = 0, newT = 1;
        int r = m, newR = a;

        while (newR != 0)
        {
            int q = r / newR;
            (t, newT) = (newT, t - q * newT);
            (r, newR) = (newR, r - q * newR);
        }

        if (t < 0)
            t += m;

        return t;
    }
    static string BuildCsvFull(
        Dictionary<string, int> original,
        Dictionary<string, int> encrypted,
        Dictionary<string, double> originalRel,
        Dictionary<string, double> encryptedRel)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Символ;Исходный;Зашифрованный;Отн.исходный;Отн.зашифрованный");

        foreach (char c in Alphabet)
        {
            string s = c.ToString();

            sb.AppendLine(
                $"{s};{original[s]};{encrypted[s]};{originalRel[s]:F6};{encryptedRel[s]:F6}"
            );
        }

        return sb.ToString();
    }

    static Dictionary<string, double> AnalyzeRelative(Dictionary<string, int> counts)
    {
        var result = new Dictionary<string, double>();

        double total = counts.Values.Sum();

        foreach (var pair in counts)
        {
            result[pair.Key] = total == 0 ? 0 : pair.Value / total;
        }

        return result;
    }



    static string BuildBenchmark(string text, string key)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Длина;Цезарь_шифр;Цезарь_дешифр;Виженер_шифр;Виженер_дешифр");

        for (int len = 0; len <= text.Length; len += 100000)
        {
            string part = text.Substring(0, len);

            // --- Цезарь ---
            var sw1 = Stopwatch.StartNew();
            string enc = AffineEncrypt(part);
            sw1.Stop();

            var sw2 = Stopwatch.StartNew();
            string dec = AffineDecrypt(enc);
            sw2.Stop();

            // --- Виженер ---
            var sw3 = Stopwatch.StartNew();
            string encV = VigenereEncrypt(part, key);
            sw3.Stop();

            var sw4 = Stopwatch.StartNew();
            string decV = VigenereDecrypt(encV, key);
            sw4.Stop();

            sb.AppendLine($"{len};{sw1.ElapsedMilliseconds};{sw2.ElapsedMilliseconds};{sw3.ElapsedMilliseconds};{sw4.ElapsedMilliseconds}");
        }

        return sb.ToString();
    }
}


