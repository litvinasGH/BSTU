using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab09OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeCollection employees = new EmployeeCollection();

            
            employees.AddEmployee(new Employee(1, "Имя1", "Разработчик", 5000));
            employees.AddEmployee(new Employee(2, "Имя2", "Тостер", 2000));
            employees.AddEmployee(new Employee(3, "Имя3", "Мэнэджер", 6000));

            
            Console.WriteLine("Все рабочие:");
            employees.DisplayAllEmployees();

            
            Console.WriteLine("\nПоиск ID 2:");
            var foundEmployee = employees.FindEmployee(2);
            if (foundEmployee != null)
            {
                Console.WriteLine(foundEmployee);
            }

            
            Console.WriteLine("\nУдаление ID 1.");
            employees.RemoveEmployee(1);

            
            Console.WriteLine("\nВсе рабочие:");
            employees.DisplayAllEmployees();






            
            Hashtable hashTable = new Hashtable
        {
            { 1, 'A' },
            { 2, 'B' },
            { 3, 'C' },
            { 4, 'D' },
            { 5, 'E' }
        };

            Console.WriteLine("HashTable после создания:");
            foreach (DictionaryEntry item in hashTable)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }

            
            int n = 2; 
            for (int i = 1; i <= n; i++)
            {
                hashTable.Remove(i);
            }

            Console.WriteLine("\nHashTable после удаления элементов:");
            foreach (DictionaryEntry item in hashTable)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }

            
            hashTable.Add(6, 'F');
            hashTable.Add(7, 'G');
            hashTable[8] = 'H'; 

            Console.WriteLine("\nHashTable после добавления элементов:");
            foreach (DictionaryEntry item in hashTable)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }

            
            List<KeyValuePair<int, char>> list = new List<KeyValuePair<int, char>>();
            foreach (DictionaryEntry item in hashTable)
            {
                list.Add(new KeyValuePair<int, char>((int)item.Key, (char)item.Value));
            }

            Console.WriteLine("\nList<T>, созданный из HashTable:");
            foreach (var pair in list)
            {
                Console.WriteLine($"Key: {pair.Key}, Value: {pair.Value}");
            }

            
            char searchValue = 'G';
            var foundItem = list.Find(pair => pair.Value == searchValue);

            Console.WriteLine($"\nПоиск значения '{searchValue}' во второй коллекции:");
            if (!foundItem.Equals(default(KeyValuePair<int, char>)))
            {
                Console.WriteLine($"Найдено: Key: {foundItem.Key}, Value: {foundItem.Value}");
            }
            else
            {
                Console.WriteLine("Значение не найдено.");
            }







            
            ObservableCollection<Employee> people = new ObservableCollection<Employee>();
            people.CollectionChanged += People_CollectionChanged;


            foreach (Employee item in employees)
            {
                people.Add(item);
            }
            

            
            people.RemoveAt(0);

            
            people.Add(new Employee (5, "Имя5", "Директор", 6000));
        }

        private static void People_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("\nИзменение коллекции:");
            switch (e.Action){
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Employee newItem in e.NewItems)
                    {
                        Console.WriteLine($"Добавлен элемент: {newItem}");
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    
                    foreach (Employee oldItem in e.OldItems)
                    {
                        Console.WriteLine($"Удалён элемент: {oldItem}");
                    }
                    break;
            }
        }
    }
}
