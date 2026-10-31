using System;
using System.ComponentModel.DataAnnotations;

namespace lab2
{
    [Serializable]
    /// <summary>
    /// Класс автора
    /// </summary>
    public class Author
    {
        private readonly int _id;
        private static int count = 0;

        /// <summary>
        /// Класс автора
        /// </summary>
        /// <param name="fullname">Фио автора</param>
        /// <param name="contry">Страна автора</param>
        public Author(string fullName, string country, DateTime birthDate)
        {
            FullName = fullName;
            Country = country;
            BirthDate = birthDate;
            _id = count++;
        }

        public Author() { }

        [Required(ErrorMessage = "Поле 'ФИО автора' обязательно для заполнения.")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я\s\-]+$",
            ErrorMessage = "ФИО может содержать только буквы, пробелы и дефисы.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Поле 'Страна' обязательно для заполнения.")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я\s\-]+$",
            ErrorMessage = "Название страны может содержать только буквы, пробелы и дефисы.")]
        public string Country { get; set; }

        [Range(typeof(DateTime), "1/1/1500", "1/1/2100",
            ErrorMessage = "Дата рождения должна быть между 1500 и 2100 годом.")]
        public DateTime BirthDate { get; set; }

        public int Id => _id;

        public override string ToString()
        {
            return $"{Id}: {FullName} ({Country}, {BirthDate:dd.MM.yyyy})";
        }
    }
}