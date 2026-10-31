using System;
using System.ComponentModel.DataAnnotations;

namespace lab2
{
    [Serializable]
    /// <summary>
    /// Класс издателя 
    /// </summary>
    public class Publisher
    {
        [Required(ErrorMessage = "Название издателя обязательно для заполнения")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я\s\-\.]+$",
            ErrorMessage = "Название может содержать только буквы, пробелы, дефисы и точки")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Страна обязательна для заполнения")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я\s\-]+$",
            ErrorMessage = "Название страны может содержать только буквы, пробелы и дефисы")]
        public string Country { get; set; } // Исправлено с Contry → Country

        [Required(ErrorMessage = "Город обязателен для заполнения")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я\s\-]+$",
            ErrorMessage = "Название города может содержать только буквы, пробелы и дефисы")]
        public string City { get; set; }

        [Range(1500, 2100, ErrorMessage = "Год должен быть между 1500 и 2100")]
        public int Year { get; set; }

        public bool StateOwned { get; set; } // Переименовано в PascalCase

        /// <summary>
        /// Конструктор издателя
        /// </summary>
        public Publisher(
            string name,
            string country,
            string city,
            int year,
            bool stateOwned)
        {
            Name = name;
            Country = country;
            City = city;
            Year = year;
            StateOwned = stateOwned;
        }

        public Publisher() { }

        public override string ToString()
        {
            return $"{Name} ({Country}, {City}, {Year}, гос.: {StateOwned})";
        }
    }
}
