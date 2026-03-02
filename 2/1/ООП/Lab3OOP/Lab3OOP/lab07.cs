using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IGenericCollection<T>
{
    void Add(T item);
    bool Remove(T item);
    IEnumerable<T> View();
}

namespace Lab3OOP
{
    public class CollectionType<T> : IGenericCollection<T> where T : IComparable<T>
    {
        public List<T> elements;

        public CollectionType()
        {
            elements = new List<T>();
        }

        public CollectionType(IEnumerable<T> items)
        {
            elements = new List<T>(items);
        }

        public void Add(T item)
        {
            elements.Add(item);
        }

        public bool Remove(T item)
        {
            return elements.Remove(item);
        }

        public IEnumerable<T> View()
        {
            return elements.AsReadOnly();
        }

        public T? Max()
        {
            try
            {
                if (!elements.Any())
                    throw new InvalidOperationException("Collection is empty.");
                return elements.Max();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            finally
            {
                Console.WriteLine("Max method executed.");
            }
        }

        public T? Min()
        {
            try
            {
                if (!elements.Any())
                    throw new InvalidOperationException("Collection is empty.");
                return elements.Min();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            finally
            {
                Console.WriteLine("Max method executed.");
            }
        }

        public bool Contains(Predicate<T> predicate)
        {
            return elements.Exists(predicate);
        }

        public override string ToString()
        {
            return string.Join(", ", elements);
        }

        public void PrintAll()
        {
            foreach (T item in elements)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
