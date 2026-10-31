using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4OOP
{
    class IndexOutOfRangeContainerException : Exception
    {
        public IndexOutOfRangeContainerException(string mesage = "Выход за пределы контейнера") : base("Исключение: " + mesage)
        {
        }

    }


    public class BadDataException : ArgumentException
    {
        public int Value { get; set; }
        public BadDataException(string mesage = "Указаны не верные данные", int value = 0) : base("Исключение: " + mesage)
        {
            Value = value;
        }
    }
    class BadDateException : BadDataException
    {
        public BadDateException(string mesage = "Указана не верная дата", int value = 0) : base("Исключение: " + mesage, value)
        {
           
        }
    }
}
