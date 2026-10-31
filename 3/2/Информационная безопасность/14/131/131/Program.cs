using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace LsbLab
{
    public enum EmbeddingMethod
    {
        Sequential,
        PseudoRandom
    }

    public static class LsbStego
    {
        // ====== PUBLIC API ======
        public static void SaveDifferenceMap(
    string originalPath,
    string stegoPath,
    string outputPath)
        {
            using var original = To24bppRgb(new Bitmap(originalPath));
            using var stego = To24bppRgb(new Bitmap(stegoPath));

            if (original.Width != stego.Width ||
                original.Height != stego.Height)
            {
                throw new InvalidOperationException("Размеры изображений не совпадают.");
            }

            using var result = new Bitmap(
                original.Width,
                original.Height,
                PixelFormat.Format24bppRgb);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color c1 = original.GetPixel(x, y);
                    Color c2 = stego.GetPixel(x, y);

                    // Сравнение LSB красного канала
                    int bit1 = c1.R & 1;
                    int bit2 = c2.R & 1;

                    // Белый = изменился
                    // Черный = не изменился
                    byte value = (bit1 != bit2)
                        ? (byte)255
                        : (byte)0;

                    result.SetPixel(x, y,
                        Color.FromArgb(value, value, value));
                }
            }

            result.Save(outputPath, ImageFormat.Png);
        }

        public static void Embed(
            string containerPath,
            string outputPath,
            string message,
            EmbeddingMethod method,
            int seed = 12345)
        {
            using var source = new Bitmap(containerPath);
            using var bmp = To24bppRgb(source);

            var positions = BuildPositions(bmp.Width, bmp.Height, method, seed);

            byte[] data = Encoding.UTF8.GetBytes(message);
            byte[] lengthPrefix = BitConverter.GetBytes(data.Length); // 4 bytes
            byte[] payload = lengthPrefix.Concat(data).ToArray();

            var bits = ToBits(payload).ToArray();

            if (bits.Length > positions.Count)
                throw new InvalidOperationException(
                    $"Недостаточно места в контейнере. Нужно {bits.Length} бит, доступно {positions.Count} бит.");

            for (int i = 0; i < bits.Length; i++)
            {
                var p = positions[i];
                Color c = bmp.GetPixel(p.X, p.Y);

                byte r = c.R;
                byte g = c.G;
                byte b = c.B;

                switch (p.Channel)
                {
                    case 0: b = SetLsb(b, bits[i]); break; // B
                    case 1: g = SetLsb(g, bits[i]); break; // G
                    case 2: r = SetLsb(r, bits[i]); break; // R
                }

                bmp.SetPixel(p.X, p.Y, Color.FromArgb(r, g, b));
            }

            bmp.Save(outputPath, ImageFormat.Png);
        }

        public static string Extract(
            string stegoPath,
            EmbeddingMethod method,
            int seed = 12345)
        {
            using var source = new Bitmap(stegoPath);
            using var bmp = To24bppRgb(source);

            var positions = BuildPositions(bmp.Width, bmp.Height, method, seed);

            // Считываем первые 32 бита = длина сообщения в байтах
            byte[] lenBytes = new byte[4];
            for (int i = 0; i < 32; i++)
            {
                var p = positions[i];
                Color c = bmp.GetPixel(p.X, p.Y);
                int bit = GetLsb(GetChannel(c, p.Channel));
                SetBitInArray(lenBytes, i, bit);
            }

            int messageLength = BitConverter.ToInt32(lenBytes, 0);
            if (messageLength < 0)
                throw new InvalidOperationException("Некорректная длина сообщения.");

            int totalBits = 32 + messageLength * 8;
            if (totalBits > positions.Count)
                throw new InvalidOperationException("Повреждённый контейнер или неверный ключ/метод.");

            byte[] messageBytes = new byte[messageLength];
            for (int i = 32; i < totalBits; i++)
            {
                var p = positions[i];
                Color c = bmp.GetPixel(p.X, p.Y);
                int bit = GetLsb(GetChannel(c, p.Channel));
                SetBitInArray(messageBytes, i - 32, bit);
            }

            return Encoding.UTF8.GetString(messageBytes);
        }

        // Цветовая матрица для визуализации выбранной битовой плоскости.
        // bitIndex = 0 показывает младший бит, 1 — следующий и т.д.
        // На выходе получается цветное изображение: R/G/B каналы показывают
        // соответствующие биты исходного изображения.
        public static void SaveBitPlane(
            string inputPath,
            string outputPath,
            int bitIndex)
        {
            if (bitIndex < 0 || bitIndex > 7)
                throw new ArgumentOutOfRangeException(nameof(bitIndex), "bitIndex должен быть в диапазоне 0..7");

            using var source = new Bitmap(inputPath);
            using var bmp = To24bppRgb(source);

            using var result = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);

                    byte r = ((c.R >> bitIndex) & 1) == 1 ? (byte)255 : (byte)0;
                    byte g = ((c.G >> bitIndex) & 1) == 1 ? (byte)255 : (byte)0;
                    byte b = ((c.B >> bitIndex) & 1) == 1 ? (byte)255 : (byte)0;

                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            result.Save(outputPath, ImageFormat.Png);
        }

        // ====== INTERNALS ======

        private static Bitmap To24bppRgb(Bitmap src)
        {
            var bmp = new Bitmap(src.Width, src.Height, PixelFormat.Format24bppRgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.DrawImage(src, 0, 0, src.Width, src.Height);
            }
            return bmp;
        }

        private static List<PixelChannel> BuildPositions(int width, int height, EmbeddingMethod method, int seed)
        {
            var list = new List<PixelChannel>(width * height * 3);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    list.Add(new PixelChannel(x, y, 0)); // B
                    list.Add(new PixelChannel(x, y, 1)); // G
                    list.Add(new PixelChannel(x, y, 2)); // R
                }
            }

            if (method == EmbeddingMethod.PseudoRandom)
            {
                var rnd = new Random(seed);
                for (int i = list.Count - 1; i > 0; i--)
                {
                    int j = rnd.Next(i + 1);
                    (list[i], list[j]) = (list[j], list[i]);
                }
            }

            return list;
        }

        private static IEnumerable<int> ToBits(byte[] data)
        {
            foreach (byte b in data)
            {
                for (int i = 0; i < 8; i++)
                    yield return (b >> i) & 1;
            }
        }

        private static byte SetLsb(byte value, int bit)
        {
            return (byte)((value & 0xFE) | (bit & 1));
        }

        private static int GetLsb(byte value)
        {
            return value & 1;
        }

        private static byte GetChannel(Color c, int channel)
        {
            return channel switch
            {
                0 => c.B,
                1 => c.G,
                2 => c.R,
                _ => throw new ArgumentOutOfRangeException(nameof(channel))
            };
        }

        private static void SetBitInArray(byte[] bytes, int bitIndex, int bit)
        {
            int byteIndex = bitIndex / 8;
            int offset = bitIndex % 8;

            if (bit == 1)
                bytes[byteIndex] |= (byte)(1 << offset);
            else
                bytes[byteIndex] &= (byte)~(1 << offset);
        }

        private readonly record struct PixelChannel(int X, int Y, int Channel);
    }

    internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

            // =========================
            // 1. Встраивание сообщения
            // =========================

            var message = File.ReadAllText("message.txt");

            LsbStego.Embed(
                containerPath: "cover.png",
                outputPath: "stego.png",
                message: message,
                method: EmbeddingMethod.PseudoRandom,
                seed: 2026);
            Console.WriteLine("Сообщение встроено.");

            LsbStego.Embed(
    containerPath: "cover.png",
    outputPath: "stegoSequential.png",
    message: message,
    method: EmbeddingMethod.Sequential,
    seed: 2026);
            Console.WriteLine("Сообщение встроено.");

            // =========================
            // 2. Извлечение сообщения
            // =========================

            string text = LsbStego.Extract(
            stegoPath: "stego.png",
            method: EmbeddingMethod.PseudoRandom,
            seed: 2026);
            string text2 = LsbStego.Extract(
stegoPath: "stegoSequential.png",
method: EmbeddingMethod.Sequential,
seed: 2026);

            Console.WriteLine("Извлечено: " + text);


            // =========================
            // 3. Карта различий
            // =========================

            LsbStego.SaveDifferenceMap(
            originalPath: "cover.png",
            stegoPath: "stego.png",
            outputPath: "difference.png");
            LsbStego.SaveDifferenceMap(
originalPath: "cover.png",
stegoPath: "stegoSequential.png",
outputPath: "difference2.png");

            Console.WriteLine("Создана карта различий difference.png");

        // =========================
        // 4. BitPlane исходного контейнера
        // =========================

        LsbStego.SaveBitPlane(
            inputPath: "cover.png",
            outputPath: "bitplane_cover.png",
            bitIndex: 0);

        Console.WriteLine("Создан bitplane_cover.png");

        // =========================
        // 5. BitPlane стегоконтейнера
        // =========================

        LsbStego.SaveBitPlane(
            inputPath: "stego.png",
            outputPath: "bitplane_stego.png",
            bitIndex: 0);
            LsbStego.SaveBitPlane(
    inputPath: "stegoSequential.png",
    outputPath: "bitplane_stego2.png",
    bitIndex: 0);

            Console.WriteLine("Создан bitplane_stego.png");

        // =========================
        // 6. Дополнительная плоскость
        // =========================

        LsbStego.SaveBitPlane(
            inputPath: "stego.png",
            outputPath: "bitplane1.png",
            bitIndex: 1);
            LsbStego.SaveBitPlane(
    inputPath: "stegoSequential.png",
    outputPath: "bitplane2.png",
    bitIndex: 1);

            Console.WriteLine("Создан bitplane1.png");

        Console.WriteLine();
        Console.WriteLine("Готово.");
        Console.ReadKey();
    }
}
}