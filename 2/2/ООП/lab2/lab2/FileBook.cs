using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace lab2
{
    [Serializable]

    /// <summary>
    /// Класс книга
    /// </summary>
    public class FileBook
    {
        [Required(ErrorMessage = "Жанр обязателен для заполнения")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я\s\-]+$",
            ErrorMessage = "Жанр может содержать только буквы, пробелы и дефисы")]
        [ValidGenre(
        "Роман",
        "Фантастика",
        "Детектив",
        "Научная литература",
        "Поэзия",
        IgnoreCase = true,
        ErrorMessage = "Недопустимый жанр книги")]
        public string Genre { get; set; }

        [Range(0.1, 1000.0, ErrorMessage = "Размер файла должен быть от 0.1 до 1000 МБ")]
        public double FileSize { get; set; }

        [Required(ErrorMessage = "Название книги обязательно для заполнения")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я0-9\s\-\!\.]+$",
            ErrorMessage = "Название содержит недопустимые символы")]
        public string Title { get; set; }

        [Range(1, 5000, ErrorMessage = "Количество страниц должно быть от 1 до 5000")]
        public int CountOfPage { get; set; }

        [Required(ErrorMessage = "Издатель должен быть указан")]
        public Publisher Publisher { get; set; }

        [Range(1500, 2100, ErrorMessage = "Год издания должен быть между 1500 и 2100")]
        public int Year { get; set; }

        //[MinLength(1, ErrorMessage = "Укажите хотя бы одного автора")]
        public List<Author> AuthorList { get; set; }

        [Range(0.01, 1_000_000, ErrorMessage = "Цена должна быть от 0.01 до 1 000 000")]
        public double Price { get; set; }

        /// <summary>
        /// Класс книга
        /// </summary>
        /// <param name="title">имя книги</param>
        /// <param name="genre">Жанр</param>
        /// <param name="size">Разиер файла</param>
        /// <param name="count">Кол-во страниц</param>
        /// <param name="publisher">Издатель</param>
        /// <param name="year">Год издания</param>
        /// <param name="authors">Список авторов</param>
        /// <param name="price">цена</param>
        public FileBook(string title, string genre, double size, int count,
            Publisher publisher, int year, List<Author> authors, double price)
        {
            Title = title;
            Genre = genre;
            FileSize = size;
            CountOfPage = count;
            Publisher = publisher;
            Year = year;
            AuthorList = authors;
            Price = price;
        }

        public FileBook() { }
        public override string ToString()
        {

            return $"{Title}|{Genre}|{AuthorList[0].FullName}" +
                               $"{(AuthorList.Count != 1 ? $"...[{AuthorList.Count}]" : "")}" +
                               $"|{Publisher.Name}|{FileSize} МБ|{CountOfPage} стр|{Year} г|{Price} д.е";
        }



        /// <summary>
        /// Атрибут для проверки жанра на соответствие списку допустимых значений
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class ValidGenreAttribute : ValidationAttribute
        {
            private readonly string[] _allowedGenres;
            public bool IgnoreCase { get; set; } = true;

            public ValidGenreAttribute(params string[] allowedGenres)
            {
                _allowedGenres = allowedGenres ?? throw new ArgumentNullException(nameof(allowedGenres));
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value == null)
                {
                    // Если жанр не обязателен, убрать эту строку
                    return new ValidationResult("Жанр не указан");
                }

                if (value is string genre)
                {
                    var comparison = IgnoreCase
                        ? StringComparison.OrdinalIgnoreCase
                        : StringComparison.Ordinal;

                    if (_allowedGenres.Any(g => g.Equals(genre, comparison)))
                    {
                        return ValidationResult.Success;
                    }

                    return new ValidationResult(
                        $"Допустимые жанры: {string.Join(", ", _allowedGenres)}. " +
                        $"Получено: {genre}");
                }

                return new ValidationResult("Некорректный формат жанра");
            }
        }

    }
}
