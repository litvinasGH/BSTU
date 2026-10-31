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
    public enum IndexE
    {
        first = 1000, second = 2000, third = 3000, fourth = 4000, fifth = 5000, sixth = 6000, seven = 7000
    }

    public struct AdressS
    {
        public int Num;
        public string Street;
        public IndexE Index;

        public AdressS(int num, string street, IndexE index)
        {
            Num = num;
            Street = street;
            Index = index;
        }
    }

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
    class DocumentContainer:Container<DocumentBase>;

    static class DocumentController
    {

        public static double GetTotalAmount(DocumentContainer container, string itemName)
        {
            double totalAmount = 0;
            foreach (var document in container)
            {
                Invoice? invoice = document as Invoice;
                if (invoice != null && invoice.ItemDescription == itemName)
                {
                    totalAmount += invoice.Amount;
                }
            }
            return totalAmount;
        }


        public static int GetCheckCount(DocumentContainer container)
        {
            int total = 0;
            foreach (var document in container)
            {
                if (document is Check)
                {
                    total++;
                }
            }
            return total;
        }

        public static List<DocumentBase> GetDocumentsByPeriod(DocumentContainer container, DateInfo startDate, DateInfo endDate)
        {
            var filteredDocuments = container.Documents
                .Where(doc => doc.Date.CompareTo(startDate) >= 0 && doc.Date.CompareTo(endDate) <= 0)
                .OrderBy(doc => doc.Date)
                .Take(2)
                .ToList();

            return filteredDocuments;
        }

        public static void PrintDocuments(List<DocumentBase> documents)
        {
            foreach (var doc in documents)
            {
                Console.WriteLine(doc.ToString());
            }
        }
    }
}
