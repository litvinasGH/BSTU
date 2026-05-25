using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

AppContext.SetSwitch("System.Security.Cryptography.AesCryptoServiceProvider.DontConsiderStaleKeys", true);

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Encoding cp1251 = Encoding.GetEncoding(1251);

const int BlockSize = 8;

var studentKeys = new[]
{
    WordToDesKey("informat", cp1251),
    WordToDesKey("ecurityy", cp1251),
    WordToDesKey("boratory", cp1251)
};

string sourceText = "kachynskasvaclovasvaclovovich";

byte[] plainBytes = cp1251.GetBytes(sourceText);
byte[] paddedPlain = ApplyPkcs7Padding(plainBytes, BlockSize);

Console.WriteLine("DES-EEE3");
Console.WriteLine($"Исходный текст: {sourceText}");
Console.WriteLine($"Длина исходного текста: {plainBytes.Length} байт");
Console.WriteLine($"Длина после дополнения: {paddedPlain.Length} байт");
Console.WriteLine();

PrintBlocks("Открытый текст (по 8 байт):", paddedPlain);

byte[] cipher = EncryptEee3Cbc(paddedPlain, studentKeys, new byte[BlockSize]);
byte[] decrypted = DecryptEee3Cbc(cipher, studentKeys, new byte[BlockSize]);
byte[] unpadded = RemovePkcs7Padding(decrypted, BlockSize);
string restoredText = cp1251.GetString(unpadded);

PrintBlocks("Шифртекст (по 8 байт):", cipher);
Console.WriteLine($"Совпадение после расшифрования: {restoredText == sourceText}");
Console.WriteLine();

for (int i = 0; i < 5; i++)
{
    MeasureSpeed(paddedPlain, studentKeys);
    Console.WriteLine();
}

AnalyzeAvalancheEffect("информац", cp1251, studentKeys);
Console.WriteLine();

AnalyzeCompression(plainBytes, cipher);
Console.WriteLine();


AnalyzeWeakKeys(paddedPlain, cipher);




static byte[] WordToDesKey(string word, Encoding encoding)
{
    byte[] key = encoding.GetBytes(word);
    if (key.Length != BlockSize)
        throw new Exception("Ключ должен быть 8 байт");
    return key;
}

static byte[] ApplyPkcs7Padding(byte[] data, int blockSize)
{
    int pad = blockSize - (data.Length % blockSize);
    if (pad == 0) pad = blockSize;

    byte[] result = new byte[data.Length + pad];
    Buffer.BlockCopy(data, 0, result, 0, data.Length);

    for (int i = data.Length; i < result.Length; i++)
        result[i] = (byte)pad;

    return result;
}

static byte[] RemovePkcs7Padding(byte[] data, int blockSize)
{
    int pad = data[^1];
    byte[] result = new byte[data.Length - pad];
    Buffer.BlockCopy(data, 0, result, 0, result.Length);
    return result;
}

static byte[] EncryptEee3Cbc(byte[] data, byte[][] keys, byte[] iv)
{
    byte[] result = new byte[data.Length];
    byte[] prev = (byte[])iv.Clone();

    for (int i = 0; i < data.Length; i += BlockSize)
    {
        byte[] block = new byte[BlockSize];
        Buffer.BlockCopy(data, i, block, 0, BlockSize);

        Xor(block, prev);

        block = DesEncrypt(block, keys[0]);
        block = DesEncrypt(block, keys[1]);
        block = DesEncrypt(block, keys[2]);

        Buffer.BlockCopy(block, 0, result, i, BlockSize);
        prev = block;
    }

    return result;
}

static byte[] DecryptEee3Cbc(byte[] data, byte[][] keys, byte[] iv)
{
    byte[] result = new byte[data.Length];
    byte[] prev = (byte[])iv.Clone();

    for (int i = 0; i < data.Length; i += BlockSize)
    {
        byte[] current = new byte[BlockSize];
        Buffer.BlockCopy(data, i, current, 0, BlockSize);

        byte[] block = DesDecrypt(current, keys[2]);
        block = DesDecrypt(block, keys[1]);
        block = DesDecrypt(block, keys[0]);

        Xor(block, prev);

        Buffer.BlockCopy(block, 0, result, i, BlockSize);
        prev = current;
    }

    return result;
}

static byte[] DesEncrypt(byte[] block, byte[] key)
{
    var engine = new DesEngine();
    engine.Init(true, new KeyParameter(key));
    byte[] output = new byte[8];
    engine.ProcessBlock(block, 0, output, 0);
    return output;
}

static byte[] DesDecrypt(byte[] block, byte[] key)
{
    var engine = new DesEngine();
    engine.Init(false, new KeyParameter(key));
    byte[] output = new byte[8];
    engine.ProcessBlock(block, 0, output, 0);
    return output;
}

static void Xor(byte[] a, byte[] b)
{
    for (int i = 0; i < a.Length; i++)
        a[i] ^= b[i];
}

static void PrintBlocks(string title, byte[] data)
{
    Console.WriteLine(title);
    int index = 1;
    for (int i = 0; i < data.Length; i += BlockSize)
    {
        byte[] block = new byte[BlockSize];
        Buffer.BlockCopy(data, i, block, 0, BlockSize);
        Console.WriteLine($"  Блок {index++}: {Convert.ToHexString(block)}");
    }
}

static void MeasureSpeed(byte[] data, byte[][] keys)
{
    Console.WriteLine("Оценка скорости");

    int iterations = 30000;
    byte[] iv = new byte[BlockSize];

    Stopwatch sw = Stopwatch.StartNew();
    for (int i = 0; i < iterations; i++)
        EncryptEee3Cbc(data, keys, iv);
    sw.Stop();

    Stopwatch sw2 = Stopwatch.StartNew();
    for (int i = 0; i < iterations; i++)
        DecryptEee3Cbc(data, keys, iv);
    sw2.Stop();

    Console.WriteLine($"Шифрование: {sw.ElapsedMilliseconds} мс");
    Console.WriteLine($"Дишифрование: {sw2.ElapsedMilliseconds} мс");
}

static void AnalyzeAvalancheEffect(string word, Encoding enc, byte[][] keys)
{
    Console.WriteLine("Лавинный эффект");

    byte[] baseData = ApplyPkcs7Padding(enc.GetBytes(word), BlockSize);
    byte[] baseCipher = EncryptEee3Cbc(baseData, keys, new byte[BlockSize]);
    string baseHex = Convert.ToHexString(baseCipher);

    Console.WriteLine($"Базовое слово: {word}");
    Console.WriteLine($"Базовый шифртекст: {baseHex}");

    for (int i = 0; i < word.Length; i++)
    {
        char[] arr = word.ToCharArray();
        char oldChar = arr[i];
        char newChar = (char)(arr[i] + 1);

        arr[i] = newChar;
        string changed = new string(arr);

        byte[] changedData = ApplyPkcs7Padding(enc.GetBytes(changed), BlockSize);
        string hex = Convert.ToHexString(EncryptEee3Cbc(changedData, keys, new byte[BlockSize]));

        int diff = CountDiff(baseHex, hex);
        int total = baseHex.Length;

        Console.WriteLine(
            $"Шаг {i + 1}: '{oldChar}' -> '{newChar}', " +
            $"'{changed}', {diff}/{total}"
        );
    }
}

static int CountDiff(string a, string b)
{
    int diff = 0;
    for (int i = 0; i < Math.Min(a.Length, b.Length); i++)
        if (a[i] != b[i]) diff++;
    return diff;
}


static void AnalyzeWeakKeys(byte[] paddedPlain, byte[] baselineCipher)
{
    Console.WriteLine("Слабые ключи DES");

    var weakKeys = new[]
    {
        new byte[] {0x01,0x01,0x01,0x01,0x01,0x01,0x01,0x01},
        new byte[] {0xFE,0xFE,0xFE,0xFE,0xFE,0xFE,0xFE,0xFE},
        new byte[] {0x1F,0x1F,0x1F,0x1F,0x0E,0x0E,0x0E,0x0E}
    };

    foreach (var key in weakKeys)
    {
        var keys = new[] { key, key, key };

        byte[] cipher = EncryptEee3Cbc(paddedPlain, keys, new byte[BlockSize]);

        int diff = CountByteDiff(cipher, baselineCipher);

        Console.WriteLine($"\nКлюч: {Convert.ToHexString(key)}");
        Console.WriteLine($"Шифртекст: {Convert.ToHexString(cipher)}");
        Console.WriteLine($"Отличий от baseline: {diff}/{cipher.Length}");
    }

    Console.WriteLine("\nПолуслабые ключи DES");

    byte[] k1 = { 0x01, 0xFE, 0x01, 0xFE, 0x01, 0xFE, 0x01, 0xFE };
    byte[] k2 = { 0xFE, 0x01, 0xFE, 0x01, 0xFE, 0x01, 0xFE, 0x01 };

    var semiKeys = new[] { k1, k2, k1 };

    byte[] semiCipher = EncryptEee3Cbc(paddedPlain, semiKeys, new byte[BlockSize]);

    int semiDiff = CountByteDiff(semiCipher, baselineCipher);

    Console.WriteLine($"\nКлюч 1: {Convert.ToHexString(k1)}");
    Console.WriteLine($"Ключ 2: {Convert.ToHexString(k2)}");
    Console.WriteLine($"Шифртекст: {Convert.ToHexString(semiCipher)}");
    Console.WriteLine($"Отличий от baseline: {semiDiff}/{semiCipher.Length}");
}

static int CountByteDiff(byte[] a, byte[] b)
{
    int diff = 0;
    for (int i = 0; i < a.Length; i++)
        if (a[i] != b[i]) diff++;
    return diff;
}

static void AnalyzeCompression(byte[] plain, byte[] cipher)
{
    Console.WriteLine("Сжимаемость");

    int p = Gzip(plain);
    int c = Gzip(cipher);

    Console.WriteLine($"Открытый текст: {plain.Length} → {p}");
    Console.WriteLine($"Шифртекст: {cipher.Length} → {c}");
}

static int Gzip(byte[] data)
{
    using var ms = new MemoryStream();
    using (var gz = new GZipStream(ms, CompressionLevel.SmallestSize, leaveOpen: true))
        gz.Write(data, 0, data.Length);
    return (int)ms.Length;
}