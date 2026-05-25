using System.Text;
using System.Text.Json;

const string latinFail = "latin.txt";
const string cyrilFail = "cyril.txt";
const string binFail = "bin.bin";
const string fullname = "Качинскас вацловас Вацловович";

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("\n------------------------------------------");
Console.WriteLine("Латинский(литовский): ");
string latinFilePath = Path.Combine(Directory.GetCurrentDirectory(), latinFail);
double latinEntropy = AnalyzeText(latinFilePath);
Console.WriteLine($"\tЭнтропия: {latinEntropy:F4} бит/символ");

Console.WriteLine("\n------------------------------------------");
Console.WriteLine("Кириллица(сербский): ");
string cyrilFilePath = Path.Combine(Directory.GetCurrentDirectory(), cyrilFail);
double cyrilEntropy = AnalyzeText(cyrilFilePath);
Console.WriteLine($"\tЭнтропия: {cyrilEntropy:F4} бит/символ");

Console.WriteLine("\n------------------------------------------");
Console.WriteLine("Бинарный:");
string binaryFilePath = Path.Combine(Directory.GetCurrentDirectory(), binFail);
double binaryEntropy = AnalyzeBinary(binaryFilePath);
Console.WriteLine($"\tЭнтропия: {binaryEntropy:F4} бит/бит");

Console.WriteLine("\n------------------------------------------");
Console.WriteLine("ФИО: ");
int namelength = fullname.Length;
double infoText = namelength * cyrilEntropy;
Console.WriteLine($"\tКириллическое представление:");
Console.WriteLine($"\t\tКоличество информации: {infoText:F2} бит");
Console.WriteLine($"\tЛатинское представление:");
Console.WriteLine($"\t\tКол-во информации: {namelength * latinEntropy} бит");
Encoding enc = Encoding.GetEncoding("windows-1251");
byte[] nameBytes = enc.GetBytes(fullname);
int bitCount = nameBytes.Length * 8;
double infoBinary = bitCount * binaryEntropy;
Console.WriteLine($"\tБинарное представление:");
Console.WriteLine($"\t\tколичество информации: {infoBinary:F2} бит");
Console.WriteLine($"\t\tДлина сообщения: {nameBytes.Length} байт = {bitCount} бит");

Console.WriteLine("\n------------------------------------------");
double[] errors = { 0.1, 0.5, 1.0 };



foreach (double p in errors)
{
    double hError = ((Func<double, double>)(p =>
    {
        if (p <= 0 || p >= 1) return 0;
        return -p * Math.Log2(p) - (1 - p) * Math.Log2(1 - p);
    }))(p);


    double capacity = 1 - hError;
    double infoWithError = bitCount * capacity;
    Console.WriteLine($"\tp = {p:F1}:");
    Console.WriteLine($"\t\tH(p) = {hError:F4} ошибка");
    Console.WriteLine($"\t\tC = {capacity:F4} пропускная способность");
    Console.WriteLine($"\t\tКоличество информации {infoWithError:F2} бит");
}

static double AnalyzeText(string filepath)
{
    if (!File.Exists(filepath))
    {
        Console.WriteLine("404");
        return 0;
    }
    string text = File.ReadAllText(filepath, Encoding.UTF8).ToLower();
    var frequency = new Dictionary<char, int>();
    foreach (char c in text)
    {
        if (frequency.ContainsKey(c))
            frequency[c]++;
        else
            frequency[c] = 1;
    }
    int total = text.Length;

    var freqList = new List<Dictionary<string, object>>();

    foreach (var kv in frequency.OrderByDescending(kv => kv.Value))
    {
        double prob = (double)kv.Value / total;
        var item = new Dictionary<string, object>
        {
            ["Symbol"] = kv.Key.ToString(),
            ["Count"] = kv.Value,
            ["Probability"] = prob
        };
        freqList.Add(item);
    }

    string jsonString = JsonSerializer.Serialize(freqList, new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    });

    File.WriteAllText($"{filepath}.json", jsonString, Encoding.UTF8);
    Console.WriteLine($"Частоты сохранены в файл");
    double entropy = 0;
    foreach (var kv in frequency)
    {
        double p = (double)kv.Value / total;
        entropy -= p * Math.Log2(p);
    }

    Console.WriteLine($"Всего символов: {total}");
    return entropy;
}

static double AnalyzeBinary(string filepath)
{
    if (!File.Exists(filepath))
    {
        Console.WriteLine("404");
        return 0;
    }
    byte[] data = File.ReadAllBytes(filepath);
    long zero = 0, one = 0;
    foreach (byte b in data)
    {
        for (int i = 0; i < 8; i++)
        {
            if (((b >> i) & 1) == 0)
                zero++;
            else
                one++;
        }
    }
    long total = zero + one;
    double p0 = (double)zero / total;
    double p1 = (double)one / total;
    double entropy = 0;
    if (p0 > 0) entropy -= p0 * Math.Log2(p0);
    if (p1 > 0) entropy -= p1 * Math.Log2(p1);

    Console.WriteLine($"Количество нулей: {zero}");
    Console.WriteLine($"Количество единиц: {one}");
    Console.WriteLine($"p(0) = {p0:F6}");
    Console.WriteLine($"p(1) = {p1:F6}");
    return entropy;
}

