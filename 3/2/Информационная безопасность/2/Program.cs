using System.Text;
using System.Text.Json;
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("латинский: ");
string latinFilePath = Path.Combine(Directory.GetCurrentDirectory(), "latin.txt");
double latinEntropy = AnalyzeText(latinFilePath);
Console.WriteLine($"энтропия латинского алфавита: {latinEntropy:F4} бит/символ");

Console.WriteLine("\nкириллица: ");
string кириллическийFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ru.txt");
double кириллическийEntropy = AnalyzeText(кириллическийFilePath);
Console.WriteLine($"энтропия кириллического алфавита: {кириллическийEntropy:F4} бит/символ");

Console.WriteLine("\nбинарный");
string binaryFilePath = Path.Combine(Directory.GetCurrentDirectory(), "bin.bin");
double binaryEntropy = AnalyzeBinary(binaryFilePath);
Console.WriteLine($"энтропия бинарного алфавита: {binaryEntropy:F4} бит/бит");

Console.WriteLine("\nввести фио: ");
string fullname = Console.ReadLine();
int namelength = fullname.Length;
double infoText = namelength * кириллическийEntropy;
Console.WriteLine($"количество информации в ФИО (кириллическое представление): {infoText:F2} бит");
Console.WriteLine($"кол-во информации в ФИО (латинское представление): {namelength * latinEntropy} бит");
Encoding enc = Encoding.GetEncoding("windows-1251");
byte[] nameBytes = enc.GetBytes(fullname);
int bitCount = nameBytes.Length * 8;
double infoBinary = bitCount * binaryEntropy;
Console.WriteLine($"количество информации в ФИО (бинарное представление): {infoBinary:F2} бит");
Console.WriteLine($"длина сообщения: {nameBytes.Length} байт = {bitCount} бит");

Console.WriteLine("\nс ошибками передачи:");
double[] errors = { 0.1, 0.5, 1.0 };
foreach (double p in errors)
{
    double hError = BinaryEntropy(p);
    double capacity = 1 - hError;
    double infoWithError = bitCount * capacity;
    Console.WriteLine($" p = {p:F1}:");
    Console.WriteLine($"   H(p) {hError:F4} ошибка");
    Console.WriteLine($"   C = {capacity:F4} пропускная способность");
    Console.WriteLine($"   количество информации {infoWithError:F2} бит");
}

static double AnalyzeText(string filepath)
{
    if (!File.Exists(filepath))
    {
        Console.WriteLine("file not found");
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
    Console.WriteLine($"частоты сохранены в файл");
    double entropy = 0;
    foreach (var kv in frequency)
    {
        double p = (double)kv.Value / total;
        entropy -= p * Math.Log2(p);
    }

    Console.WriteLine($"всего символов: {total}");
    return entropy;
}

static double AnalyzeBinary(string filepath)
{
    if (!File.Exists(filepath))
    {
        Console.WriteLine("file not found");
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

    Console.WriteLine($"количество нулей: {zero}");
    Console.WriteLine($"количество единиц: {one}");
    Console.WriteLine($"p(0) = {p0:F6}");
    Console.WriteLine($"p(1) = {p1:F6}");
    return entropy;
}

static double BinaryEntropy(double p)
{
    if (p <= 0 || p >= 1) return 0;
    return -p * Math.Log2(p) - (1 - p) * Math.Log2(1 - p);
}