using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EnigmaSimulator
{
    internal static class Program
    {
        public static double CalculateIC(string text)
        {
            int[] counts = new int[26];
            int N = 0;

            foreach (char c in text.ToUpper())
            {
                if (c >= 'A' && c <= 'Z')
                {
                    counts[c - 'A']++;
                    N++;
                }
            }

            if (N <= 1)
                return 0;

            double sum = 0;
            for (int i = 0; i < 26; i++)
            {
                sum += counts[i] * (counts[i] - 1);
            }

            return sum / (N * (N - 1));
        }
    
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var machine = new EnigmaMachine(
                rotorLeft: new Rotor(RotorWiring.RotorII, 0),
                rotorMiddle: new Rotor(RotorWiring.RotorIII, 0),
                rotorRight: new Rotor(RotorWiring.RotorV, 0),
                reflector: new Reflector(ReflectorWiring.C),
                stepLeft: 1,
                stepMiddle: 2,
                stepRight: 2
            );

            Console.WriteLine("Энигма-симулятор на C#");
            Console.WriteLine("Вариант 2: L=II, M=III, R=V, Re=C, шаги 1-2-2");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1 - Зашифровать/расшифровать текст");
                Console.WriteLine("2 - Показать пример для 5 начальных установок и экспорт CSV");
                Console.WriteLine("3 - Экспорт частот в CSV для введенного текста");
                Console.WriteLine("0 - Выход");
                Console.Write("Выбор: ");
                var choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                if (choice == "0")
                    break;

                if (choice == "1")
                {
                    Console.Write("Введите текст: ");
                    string? input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Пустой ввод.");
                        Console.WriteLine();
                        continue;
                    }

                    Console.Write("Начальная установка роторов (3 буквы, например ABC): ");
                    string? setup = Console.ReadLine();
                    if (!TryParseSetup(setup, out int l, out int m, out int r))
                    {
                        Console.WriteLine("Неверная начальная установка.");
                        Console.WriteLine();
                        continue;
                    }

                    machine.SetPositions(l, m, r);
                    string transformed = machine.ProcessText(input);
                    Console.WriteLine("Результат:");
                    Console.WriteLine(transformed);
                    Console.WriteLine();
                }
                else if (choice == "2")
                {
                    string message = "informationsecurityisimportantforeveryuserbecauseitprotectspersonaldataprivacyandpreventsunauthorizedaccessfromexternalattackers";
                    string[] setups = { "AAA", "BCD", "MNO", "QWE", "XYZ" };

                    Console.WriteLine("Сообщение: " + message);
                    Console.WriteLine();

                    // Экспорт частот исходного текста
                    CsvFrequencyExporter.ExportLetterFrequencies(
                        Path.Combine(AppContext.BaseDirectory, "frequencies_source.csv"),
                        message,
                        label: "source");

                    foreach (var setup in setups)
                    {
                        if (!TryParseSetup(setup, out int l, out int m, out int r))
                            continue;

                        machine.SetPositions(l, m, r);
                        string encrypted = machine.ProcessText(message);
                        Console.WriteLine($"Старт {setup} -> {encrypted}");
                        double ic = CalculateIC(encrypted);
                        Console.WriteLine($"Индекс совпадения: {ic:F3}");

                        // Экспорт частот шифртекста по каждой начальной установке
                        string fileName = $"frequencies_{setup}.csv";
                        CsvFrequencyExporter.ExportLetterFrequencies(
                            Path.Combine(AppContext.BaseDirectory, fileName),
                            encrypted,
                            label: setup);
                    }

                    Console.WriteLine();
                    Console.WriteLine("CSV-файлы частот сохранены рядом с exe.");
                    Console.WriteLine();
                }
                else if (choice == "3")
                {
                    Console.Write("Введите текст: ");
                    string? input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Пустой ввод.");
                        Console.WriteLine();
                        continue;
                    }

                    string filePath = Path.Combine(AppContext.BaseDirectory, "frequencies_custom.csv");
                    CsvFrequencyExporter.ExportLetterFrequencies(filePath, input, label: "custom");
                    Console.WriteLine($"CSV сохранён: {filePath}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Неизвестная команда.");
                    Console.WriteLine();
                }
            }
        }

        private static bool TryParseSetup(string? setup, out int left, out int middle, out int right)
        {
            left = middle = right = 0;
            if (string.IsNullOrWhiteSpace(setup))
                return false;

            setup = setup.Trim().ToUpperInvariant();
            if (setup.Length != 3)
                return false;

            if (!IsLatinLetter(setup[0]) || !IsLatinLetter(setup[1]) || !IsLatinLetter(setup[2]))
                return false;

            left = setup[0] - 'A';
            middle = setup[1] - 'A';
            right = setup[2] - 'A';
            return true;
        }

        private static bool IsLatinLetter(char c) => c >= 'A' && c <= 'Z';
    }

    internal sealed class EnigmaMachine
    {
        private readonly Rotor _left;
        private readonly Rotor _middle;
        private readonly Rotor _right;
        private readonly Reflector _reflector;
        private readonly int _stepLeft;
        private readonly int _stepMiddle;
        private readonly int _stepRight;

        public EnigmaMachine(Rotor rotorLeft, Rotor rotorMiddle, Rotor rotorRight, Reflector reflector,
                             int stepLeft, int stepMiddle, int stepRight)
        {
            _left = rotorLeft;
            _middle = rotorMiddle;
            _right = rotorRight;
            _reflector = reflector;
            _stepLeft = stepLeft;
            _stepMiddle = stepMiddle;
            _stepRight = stepRight;
        }

        public void SetPositions(int left, int middle, int right)
        {
            _left.Position = left;
            _middle.Position = middle;
            _right.Position = right;
        }

        public string ProcessText(string input)
        {
            var sb = new StringBuilder();

            foreach (char raw in input)
            {
                if (char.IsWhiteSpace(raw))
                {
                    sb.Append(raw);
                    continue;
                }

                string normalized = Transliteration.ExpandChar(raw);
                if (normalized.Length == 0)
                {
                    // Пропускаем символы, которые не участвуют в шифровании.
                    continue;
                }

                foreach (char c0 in normalized)
                {
                    char c = char.ToUpperInvariant(c0);
                    if (!IsLatinLetter(c))
                    {
                        sb.Append(c0);
                        continue;
                    }

                    // Путь сигнала: R -> M -> L -> Reflector -> L^-1 -> M^-1 -> R^-1
                    c = _right.EncodeForward(c);
                    c = _middle.EncodeForward(c);
                    c = _left.EncodeForward(c);
                    c = _reflector.Reflect(c);
                    c = _left.EncodeBackward(c);
                    c = _middle.EncodeBackward(c);
                    c = _right.EncodeBackward(c);

                    sb.Append(c);
                }

                StepRotors();
            }

            return sb.ToString();
        }

        private void StepRotors()
        {
            // В данном варианте шаги фиксированные: 1-2-2.
            // Если нужен более "классический" вариант с 0, можно расширить логику здесь.
            _left.Step(_stepLeft);
            _middle.Step(_stepMiddle);
            _right.Step(_stepRight);
        }

        private static bool IsLatinLetter(char c) => c >= 'A' && c <= 'Z';
    }

    internal sealed class Rotor
    {
        private readonly int[] _forward = new int[26];
        private readonly int[] _backward = new int[26];

        public int Position { get; set; }

        public Rotor(string wiring, int position)
        {
            if (wiring.Length != 26)
                throw new ArgumentException("Wiring must contain 26 letters.");

            Position = Mod(position, 26);

            for (int i = 0; i < 26; i++)
            {
                int mapped = wiring[i] - 'A';
                _forward[i] = mapped;
                _backward[mapped] = i;
            }
        }

        public void Step(int amount)
        {
            Position = Mod(Position + amount, 26);
        }

        public char EncodeForward(char c)
        {
            int input = c - 'A';
            int shifted = Mod(input + Position, 26);
            int wired = _forward[shifted];
            int output = Mod(wired - Position, 26);
            return (char)('A' + output);
        }

        public char EncodeBackward(char c)
        {
            int input = c - 'A';
            int shifted = Mod(input + Position, 26);
            int wired = _backward[shifted];
            int output = Mod(wired - Position, 26);
            return (char)('A' + output);
        }

        private static int Mod(int x, int m) => (x % m + m) % m;
    }

    internal sealed class Reflector
    {
        private readonly Dictionary<char, char> _map;

        public Reflector(string wiringPairs)
        {
            _map = new Dictionary<char, char>();
            foreach (var pair in wiringPairs.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                var p = pair.Trim();
                if (p.Length != 2)
                    throw new ArgumentException("Reflector pair must contain 2 letters.");

                char a = char.ToUpperInvariant(p[0]);
                char b = char.ToUpperInvariant(p[1]);
                _map[a] = b;
                _map[b] = a;
            }
        }

        public char Reflect(char c)
        {
            if (_map.TryGetValue(c, out char mapped))
                return mapped;
            throw new ArgumentException($"Reflector has no mapping for {c}.");
        }
    }

    internal static class RotorWiring
    {
        // Роторы из условия.
        public const string RotorII = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
        public const string RotorIII = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
        public const string RotorV = "VZBRGITYUPSDNHLXAWMJQOFECK";
    }

    internal static class ReflectorWiring
    {
        // Рефлектор C из рисунка.
        public const string C = "AF BV CP DJ EI GO HY KR LZ MX NW TQ SU";
    }

    internal static class Transliteration
    {
        private static readonly Dictionary<char, string> Map = new()
        {
            ['А'] = "A",
            ['Б'] = "B",
            ['В'] = "V",
            ['Г'] = "G",
            ['Д'] = "D",
            ['Е'] = "E",
            ['Ё'] = "E",
            ['Ж'] = "ZH",
            ['З'] = "Z",
            ['И'] = "I",
            ['Й'] = "Y",
            ['К'] = "K",
            ['Л'] = "L",
            ['М'] = "M",
            ['Н'] = "N",
            ['О'] = "O",
            ['П'] = "P",
            ['Р'] = "R",
            ['С'] = "S",
            ['Т'] = "T",
            ['У'] = "U",
            ['Ф'] = "F",
            ['Х'] = "H",
            ['Ц'] = "C",
            ['Ч'] = "CH",
            ['Ш'] = "SH",
            ['Щ'] = "SCH",
            ['Ъ'] = "",
            ['Ы'] = "Y",
            ['Ь'] = "",
            ['Э'] = "E",
            ['Ю'] = "YU",
            ['Я'] = "YA",

            ['а'] = "A",
            ['б'] = "B",
            ['в'] = "V",
            ['г'] = "G",
            ['д'] = "D",
            ['е'] = "E",
            ['ё'] = "E",
            ['ж'] = "ZH",
            ['з'] = "Z",
            ['и'] = "I",
            ['й'] = "Y",
            ['к'] = "K",
            ['л'] = "L",
            ['м'] = "M",
            ['н'] = "N",
            ['о'] = "O",
            ['п'] = "P",
            ['р'] = "R",
            ['с'] = "S",
            ['т'] = "T",
            ['у'] = "U",
            ['ф'] = "F",
            ['х'] = "H",
            ['ц'] = "C",
            ['ч'] = "CH",
            ['ш'] = "SH",
            ['щ'] = "SCH",
            ['ъ'] = "",
            ['ы'] = "Y",
            ['ь'] = "",
            ['э'] = "E",
            ['ю'] = "YU",
            ['я'] = "YA"
        };

        public static string ExpandChar(char c)
        {
            if (Map.TryGetValue(c, out var s))
                return s;

            return c.ToString();
        }
    }

    internal static class CsvFrequencyExporter
    {
        public static void ExportLetterFrequencies(string filePath, string text, string label)
        {
            var counts = new int[26];
            int total = 0;

            foreach (char raw in text)
            {
                string normalized = Transliteration.ExpandChar(raw);
                foreach (char c0 in normalized)
                {
                    char c = char.ToUpperInvariant(c0);
                    if (c >= 'A' && c <= 'Z')
                    {
                        counts[c - 'A']++;
                        total++;
                    }
                }
            }

            var sb = new StringBuilder();
            sb.AppendLine("label;symbol;count;frequency_percent");

            for (int i = 0; i < 26; i++)
            {
                char symbol = (char)('A' + i);
                double freq = total == 0 ? 0.0 : (counts[i] * 100.0 / total);
                sb.Append(label).Append(';')
                  .Append(symbol).Append(';')
                  .Append(counts[i]).Append(';')
                  .Append(freq.ToString("F4", System.Globalization.CultureInfo.InvariantCulture))
                  .AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

    }
}
