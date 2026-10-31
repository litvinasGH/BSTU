using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    private const string Alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

    private const int SpiralRows = 5;
    private const int SpiralCols = 5;

    private const string RowKey = "вацловас";
    private const string ColKey = "качинскас";

    private const string path = "input.txt";
    private const char PadChar = '\u0001';

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        string text = File.ReadAllText(path, Encoding.UTF8);

        // --- МАРШРУТНАЯ ПЕРЕСТАНОВКА ---
        var sw1 = Stopwatch.StartNew();
        string routeEnc = RouteEncrypt(text);
        sw1.Stop();

        var sw2 = Stopwatch.StartNew();
        string routeDec = RouteDecrypt(routeEnc);
        sw2.Stop();

        Console.WriteLine($"RouteEncrypt: {sw1.ElapsedMilliseconds} ms");
        Console.WriteLine($"RouteDecrypt: {sw2.ElapsedMilliseconds} ms");

        File.WriteAllText("route_encrypted.txt", routeEnc, Encoding.UTF8);
        File.WriteAllText("route_decrypted.txt", routeDec, Encoding.UTF8);

        // --- МНОЖЕСТВЕННАЯ ПЕРЕСТАНОВКА ---
        var sw3 = Stopwatch.StartNew();
        string multiEnc = MultipleEncrypt(text);
        sw3.Stop();

        var sw4 = Stopwatch.StartNew();
        string multiDec = MultipleDecrypt(multiEnc);
        sw4.Stop();

        Console.WriteLine($"MultipleEncrypt: {sw3.ElapsedMilliseconds} ms");
        Console.WriteLine($"MultipleDecrypt: {sw4.ElapsedMilliseconds} ms");

        File.WriteAllText("multiple_encrypted.txt", multiEnc, Encoding.UTF8);
        File.WriteAllText("multiple_decrypted.txt", multiDec, Encoding.UTF8);

        // --- ЧАСТОТЫ ---
        var origFreq = Analyze(text);
        var routeFreq = Analyze(routeEnc);
        var multiFreq = Analyze(multiEnc);

        var origRel = AnalyzeRelative(origFreq);
        var routeRel = AnalyzeRelative(routeFreq);
        var multiRel = AnalyzeRelative(multiFreq);

        File.WriteAllText("freq_route.csv",
            BuildCsvFull(origFreq, routeFreq, origRel, routeRel),
            Encoding.UTF8);

        File.WriteAllText("freq_multiple.csv",
            BuildCsvFull(origFreq, multiFreq, origRel, multiRel),
            Encoding.UTF8);

        // --- БЕНЧМАРК ---
        File.WriteAllText("benchmark.csv",
            BuildBenchmark(text),
            Encoding.UTF8);
    }

    // ============================================================
    // МАРШРУТНАЯ ПЕРЕСТАНОВКА
    // ============================================================

    static string RouteEncrypt(string text)
    {
        int size = SpiralRows * SpiralCols;
        var sb = new StringBuilder();

        for (int i = 0; i < text.Length; i += size)
        {
            string block = text.Substring(i, Math.Min(size, text.Length - i))
                .PadRight(size, PadChar);

            char[,] m = new char[SpiralRows, SpiralCols];
            FillMatrix(m, block);

            foreach (var p in Spiral())
                sb.Append(m[p.Item1, p.Item2]);
        }

        return sb.ToString();
    }

    static string RouteDecrypt(string text)
    {
        int size = SpiralRows * SpiralCols;
        var sb = new StringBuilder();

        for (int i = 0; i < text.Length; i += size)
        {
            string block = text.Substring(i, Math.Min(size, text.Length - i))
                .PadRight(size, PadChar);

            char[,] m = new char[SpiralRows, SpiralCols];
            int k = 0;

            foreach (var p in Spiral())
                m[p.Item1, p.Item2] = block[k++];

            sb.Append(ReadMatrix(m));
        }

        return sb.ToString().TrimEnd(PadChar);
    }

    // ============================================================
    // МНОЖЕСТВЕННАЯ ПЕРЕСТАНОВКА
    // ============================================================

    static string MultipleEncrypt(string text)
    {
        int rows = RowKey.Length;
        int cols = ColKey.Length;

        int[] r = GetPermutation(RowKey);
        int[] c = GetPermutation(ColKey);

        int size = rows * cols;
        var sb = new StringBuilder();

        for (int i = 0; i < text.Length; i += size)
        {
            string block = text.Substring(i, Math.Min(size, text.Length - i))
                .PadRight(size, PadChar);

            char[,] m = new char[rows, cols];
            FillMatrix(m, block);

            char[,] res = new char[rows, cols];

            for (int i1 = 0; i1 < rows; i1++)
                for (int j1 = 0; j1 < cols; j1++)
                    res[i1, j1] = m[r[i1], c[j1]];

            sb.Append(ReadMatrix(res));
        }

        return sb.ToString();
    }

    static string MultipleDecrypt(string text)
    {
        int rows = RowKey.Length;
        int cols = ColKey.Length;

        int[] r = GetPermutation(RowKey);
        int[] c = GetPermutation(ColKey);

        int size = rows * cols;
        var sb = new StringBuilder();

        for (int i = 0; i < text.Length; i += size)
        {
            string block = text.Substring(i, Math.Min(size, text.Length - i))
                .PadRight(size, PadChar);

            char[,] m = new char[rows, cols];
            FillMatrix(m, block);

            char[,] res = new char[rows, cols];

            for (int i1 = 0; i1 < rows; i1++)
                for (int j1 = 0; j1 < cols; j1++)
                    res[r[i1], c[j1]] = m[i1, j1];

            sb.Append(ReadMatrix(res));
        }

        return sb.ToString().TrimEnd(PadChar);
    }

    // ============================================================
    // ЧАСТОТЫ
    // ============================================================

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

    static Dictionary<string, double> AnalyzeRelative(Dictionary<string, int> counts)
    {
        var result = new Dictionary<string, double>();

        double total = counts.Values.Sum();

        foreach (var pair in counts)
            result[pair.Key] = total == 0 ? 0 : pair.Value / total;

        return result;
    }

    static string BuildCsvFull(
        Dictionary<string, int> original,
        Dictionary<string, int> encrypted,
        Dictionary<string, double> originalRel,
        Dictionary<string, double> encryptedRel)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Символ;Исходный;Зашифрованный;Отн_исходный;Отн_зашифрованный");

        foreach (char c in Alphabet)
        {
            string s = c.ToString();

            sb.AppendLine(
                $"{s};{original[s]};{encrypted[s]};{originalRel[s]:F6};{encryptedRel[s]:F6}"
            );
        }

        return sb.ToString();
    }

    // ============================================================
    // ВСПОМОГАТЕЛЬНЫЕ
    // ============================================================

    static void FillMatrix(char[,] m, string text)
    {
        int k = 0;

        for (int i = 0; i < m.GetLength(0); i++)
            for (int j = 0; j < m.GetLength(1); j++)
                m[i, j] = text[k++];
    }

    static string ReadMatrix(char[,] m)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < m.GetLength(0); i++)
            for (int j = 0; j < m.GetLength(1); j++)
                sb.Append(m[i, j]);

        return sb.ToString();
    }

    static IEnumerable<(int, int)> Spiral()
    {
        int top = 0, bottom = SpiralRows - 1;
        int left = 0, right = SpiralCols - 1;

        while (top <= bottom && left <= right)
        {
            for (int j = left; j <= right; j++) yield return (top, j);
            top++;

            for (int i = top; i <= bottom; i++) yield return (i, right);
            right--;

            for (int j = right; j >= left; j--) yield return (bottom, j);
            bottom--;

            for (int i = bottom; i >= top; i--) yield return (i, left);
            left++;
        }
    }

    static int[] GetPermutation(string key)
    {
        return key
            .Select((ch, i) => new { ch, i })
            .OrderBy(x => x.ch)
            .ThenBy(x => x.i)
            .Select(x => x.i)
            .ToArray();
    }

    static string BuildBenchmark(string text)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Длина;RouteEnc;RouteDec;MultiEnc;MultiDec");

        for (int len = 0; len <= text.Length; len += 100000)
        {
            string part = text.Substring(0, len);

            var sw1 = Stopwatch.StartNew();
            var e1 = RouteEncrypt(part);
            sw1.Stop();

            var sw2 = Stopwatch.StartNew();
            RouteDecrypt(e1);
            sw2.Stop();

            var sw3 = Stopwatch.StartNew();
            var e2 = MultipleEncrypt(part);
            sw3.Stop();

            var sw4 = Stopwatch.StartNew();
            MultipleDecrypt(e2);
            sw4.Stop();

            sb.AppendLine($"{len};{sw1.ElapsedMilliseconds};{sw2.ElapsedMilliseconds};{sw3.ElapsedMilliseconds};{sw4.ElapsedMilliseconds}");
        }

        return sb.ToString();
    }
}