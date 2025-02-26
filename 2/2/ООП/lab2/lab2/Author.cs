using System;

namespace lab2
{
    [Serializable]
    /// <summary>
    /// Класс автора
    /// </summary>
    public class Author
    {
        private readonly int _id;
        private string full_name;
        private string contry;
        private DateTime birDate;
        private static int count = 0;

        public int Id
        {
            get { return _id; }
        }
        public string FullName
        {
            get { return full_name; }
            set { full_name = value; }
        }
        public string Contry
        {
            get { return contry; }
            set { contry = value; }
        }
        public DateTime BirDate
        {
            get { return birDate; }
            set { birDate = value; }
        }

        /// <summary>
        /// Класс автора
        /// </summary>
        /// <param name="fullname">Фио автора</param>
        /// <param name="contry">Страна автора</param>
        public Author(string fullname, string contry, DateTime bir)
        {
            FullName = fullname;
            Contry = contry;
            birDate = bir;
            _id = count++;
        }

        public Author() { }
        public override string ToString()
        {
            return $"{_id}: {full_name} ({Contry}, {birDate.Day}.{birDate.Month}.{birDate.Year})";
        }
    }
}
