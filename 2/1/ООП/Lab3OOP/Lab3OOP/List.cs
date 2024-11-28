using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Lab3OOP
{
    public class List
    {
        private int[] elements;
        public Production production;


        public class Production
        {
            public int Id { get; set; }
            public string OrganizationName { get; set; }

            public Production(int id, string organizationName)
            {
                Id = id;
                OrganizationName = organizationName;
            }
        }

        public class Developer
        {
            public string FullName { get; set; }
            public int Id { get; set; }
            public string Department { get; set; }

            public Developer(string fullname, int id, string department)
            {
                FullName = fullname;
                Id = id;
                Department = department;
            }

        }
        public List()
        {
            elements = new int[0];
        }
        public List(int[] values)
        {
            elements = new int[values.Length];
            Array.Copy(values, elements, values.Length);
        }
        public int this[int index]
        {
            get { return elements[index]; }
            set { elements[index] = value; }
        }
        public static List operator +(int item, List list)
        {
            int[] NewElement = new int[list.elements.Length + 1];
            list.elements.CopyTo(NewElement, 1);
            NewElement[0] = item;
            return new List(NewElement);
        }
        public static List operator --(List list)
        {
            int[] newElement = new int[list.elements.Length - 1];
            Array.Copy(list.elements, 1, newElement, 0, list.elements.Length - 1);
            return new List(newElement);
        }
        public static List operator ++(List list)
        {
            int[] NewElement = new int[list.elements.Length + 1];
            list.elements.CopyTo(NewElement, 1);
            NewElement[0] = 1;
            return new List(NewElement);
        }
        public static bool operator ==(List list1, List list2)
        {
            if (list1.elements.Length != list2.elements.Length)
                return false;

            for (int i = 0; i < list1.elements.Length; i++)
            {
                if (list1.elements[i] != list2.elements[i])
                    return false;
            }

            return true;
        }
        public static bool operator !=(List list1, List list2)
        {
            return !(list1 == list2);
        }
        public override int GetHashCode()
        {
            var hash = 0;
            foreach (char temp in elements)
            {
                hash += temp;
            }
            return hash;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(List)) return false;
            List? prod = (List)obj;
            if (prod == null) return false;
            return this == prod;
        }
        public static List operator *(List list1, List list2)
        {
            int[] combinedElements = new int[list1.elements.Length + list2.elements.Length];
            list1.elements.CopyTo(combinedElements, 0);
            list2.elements.CopyTo(combinedElements, list1.elements.Length);
            return new List(combinedElements);
        }

        
        
        public int Max()
        {
            if (StatisticOperation.CountElements(this) == 0)
                throw new ArgumentException("Список пустой");
            int max = 0;
            foreach(int element in elements)
            {
                if (element > max) max = element;
            }
            return max;
        }
        public int Min()
        {
            if (StatisticOperation.CountElements(this) == 0)
                throw new ArgumentException("Список пустой");
            int min = elements[0];
            foreach (var element in elements)
            {
                if (element < min) min = element;
            }
            return min;
        }
        public IEnumerator GetEnumerator()
        {
            return elements.GetEnumerator();
        }
        public string Out()
        {
            string str = "";
            foreach (var element in elements)
            {
                str += element.ToString();
                str += ", ";
            }
            return str;
        }

    }
    static class StatisticOperation
    {
        public static int Sum(List list)
        {
            int sum = 0;
            foreach (int element in list)
            {
                sum += element;
            }
            return sum;
        }

        public static int DifferenceBetweenMaxAndMin(List list)
        {
            return list.Max() - list.Min();
        }

        public static int CountElements(List list)
        {
            int count = 0;
            foreach (int element in list)
            {
                count++; 
            }
            return count;
        }

        public static int CountWordsWithCapital(this string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Пустая строка или null");

            var words = Regex.Matches(str, @"\b\p{Lu}\w*\b");
            return words.Count;
        }

        public static bool HasDuplicates(this List list)
        {
            List list1 = new List();
            foreach (int element in list)
            {
                foreach (int element2 in list1)
                {
                    if (element2.Equals(element))
                    {
                        return true;
                    }
                }
                list1 = element + list1;
            }
            return false;
        }
    }
}
