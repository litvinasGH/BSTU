using System;
using System.Collections.Generic;

namespace lab2
{
    internal class FileBook
    {
        private string genre;
        private double file_size;
        private string title;
        private int count_of_page;
        private Publisher publisher;
        private int year;
        private List<Author> authorList;
        private double price;

        string Genre
        {
            get { return genre; }
            set { genre = value; }
        }
        double FileSize
        {
            get { return file_size; }
            set { file_size = value; }
        }
        string Title
        { get { return title; } set { title = value; } }
        int CountOfPage
        {
            get { return count_of_page; }
            set { count_of_page = value; }
        }
        Publisher _publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }
        int Year
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
        List<Author> AuthorList
        {
            get { return authorList; }
            set { authorList = value; }
        }
        double Price
        {
            get { return price; }
            set { price = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="genre"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        /// <param name="publisher"></param>
        /// <param name="year"></param>
        /// <param name="authors"></param>
        /// <param name="price"></param>
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
    }
}
