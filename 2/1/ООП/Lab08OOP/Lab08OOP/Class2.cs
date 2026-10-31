using System;
namespace Lab08OOP
{
    static   class StringProcessor
    {
        public static string RemovePunctuation(string input)
        {
            char[] punctuation = { '.', ',', '!', '?', ';', ':' };
            foreach (var p in punctuation)
                input = input.Replace(p.ToString(), "");
            return input;
        }

        public static string ToUpperCase(string input)
        {
            return input.ToUpper();
        }

        public static string TrimExtraSpaces(string input)
        {
            return string.Join(" ", input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static string AddPrefix(string input)
        {
            return ">> " + input;
        }

        public static string AddSuffix(string input)
        {
            return input + " <<";
        }

        public static void ProcessString(string input, params Func<string, string>[] operations)
        {
            string result = input;
            foreach (var operation in operations)
            {
                result = operation(result);
            }
            Console.WriteLine("Результат обработки: " + result);
        }

    }
}