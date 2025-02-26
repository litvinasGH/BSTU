using System;
using System.Collections.Generic;

namespace lab2
{
    [Serializable]

    /// <summary>
    /// Класс книга
    /// </summary>
    public class FileBook
    {
        private string genre;
        private double file_size;
        private string title;
        private int count_of_page;
        private Publisher publisher;
        private int year;
        private List<Author> authorList;
        private double price;

        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }
        public double FileSize
        {
            get { return file_size; }
            set { file_size = value; }
        }
        public string Title
        { get { return title; } set { title = value; } }
        public int CountOfPage
        {
            get { return count_of_page; }
            set { count_of_page = value; }
        }
        public Publisher _publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }
        public int Year
        {
            get
            { return year; }
            set
            {
                if (year < 0 || year > DateTime.Today.Year)
                    throw new Exception("Не верный год!");
                year = value;
            }
        }
        public List<Author> AuthorList
        {
            get { return authorList; }
            set { authorList = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

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
            _publisher = publisher;
            Year = year;
            AuthorList = authors;
            Price = price;
        }

        public FileBook() { }
        public override string ToString()
        {
            
            return $"{title}|{Genre}|{AuthorList[0].FullName}{(AuthorList.Count != 1 ? $"...[{AuthorList.Count}]" : "")}|{publisher.Name}|{FileSize}кб|{count_of_page}стр|{year}г|{price}д.е";
        }
    }
}
