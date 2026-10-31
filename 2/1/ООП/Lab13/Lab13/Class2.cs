using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab4OOP
{
    

    partial class DateInfo : IComparable<DateInfo>
    {
        public int CompareTo(DateInfo other)
        {
            if (Year != other.Year) return Year.CompareTo(other.Year);
            if (Month != other.Month) return Month.CompareTo(other.Month);
            return Day.CompareTo(other.Day);
        }
    }


    abstract class Container<T> 
    {
        public List<T> Documents { get; set; }
        public Container()
        {
            Documents = new List<T>();
        }

        public virtual void Add(T[] parm)
        {
            foreach (T document in parm)
                Documents.Add(document);
        }
        public virtual void Add(T parm)
        {
            Documents.Add(parm);
        }

        public virtual bool Remove(T document)
        {
            return Documents.Remove(document);
        }

        public virtual T Get(int index)
        {
            if (index >= 0 && index < Documents.Count)
            {
                return Documents[index];
            }
            throw new IndexOutOfRangeContainerException();
        }

        public virtual void Set(int index, T document)
        {
            if (index >= 0 && index < Documents.Count)
            {
                Documents[index] = document;
            }
            else
            {
                throw new IndexOutOfRangeContainerException();
            }
        }

        public virtual void PrintAll()
        {
            foreach (var document in Documents)
            {
                Console.WriteLine(document.ToString());
            }
        }

        public virtual IEnumerator GetEnumerator()
        {
            return Documents.GetEnumerator();
        }
    }

    
}
