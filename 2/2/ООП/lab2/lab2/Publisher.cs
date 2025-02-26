using System;

namespace lab2
{
    [Serializable]
    /// <summary>
    /// Класс издателя 
    /// </summary>
    public class Publisher
    {
        private string name;
        private string contry;
        private string city;
        private int year;
        private bool state_owned;

        public string Name
        {
            get
            { return name; }
            set
            { name = value; }
        }
        public string Contry
        {
            get
            { return contry; }
            set
            { contry = value; }
        }
        public string City
        {
            get
            { return city; }
            set
            { city = value; }
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
        public bool State_owned
        {
            get { return state_owned; }
            set { state_owned = value; }
        }

        /// <summary>
        /// Класс издателя 
        /// </summary>
        /// <param name="name">Имя издателя</param>
        /// <param name="contry">Страна издателя</param>
        /// <param name="city">Город издателя</param>
        /// <param name="year">Год издателя</param>
        /// <param name="state_owned">Является ли гос</param>
        public Publisher(string name, string contry, string city, int year, bool state_owned)
        {
            Name = name;
            Contry = contry;
            City = city;
            Year = year;
            State_owned = state_owned;
        }

        public Publisher()
        {

        }
        public override string ToString()
        {
            return $"{name}({contry}, {city}, {year}, гос: {state_owned})";
        }
    }
}
