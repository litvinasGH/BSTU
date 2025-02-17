using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    /// <summary>
    /// Класс автора
    /// </summary>
    internal class Author
    {
        private readonly int _id;
        private string full_name;
        private string contry;
        private static int count = 0;

        int Id
        {
            get { return _id; }
        }
        string FullName
        {
            get { return full_name; }
            set { full_name = value; }
        }
        string Contry
        {
            get { return contry; }
            set { contry = value; }
        }

        /// <summary>
        /// Класс автора
        /// </summary>
        /// <param name="fullname">Фио автора</param>
        /// <param name="contry">Страна автора</param>
        Author (string fullname, string contry)
        {
            FullName = fullname;
            Contry = contry;
            _id = count++;
        }
    }
}
