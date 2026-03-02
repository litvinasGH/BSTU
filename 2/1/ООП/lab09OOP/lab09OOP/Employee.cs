using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab09OOP
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }

        public Employee(int id, string name, string position, decimal salary)
        {
            ID = id;
            Name = name;
            Position = position;
            Salary = salary;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Имя: {Name}, Должность: {Position}, Зарплата: {Salary:C}";
        }
    }

    public class EmployeeCollection : IEnumerable<Employee>
    {
        private Hashtable employeeTable = new Hashtable();

        
        public void AddEmployee(Employee employee)
        {
            if (!employeeTable.ContainsKey(employee.ID))
            {
                employeeTable.Add(employee.ID, employee);
            }
            else
            {
                Console.WriteLine("Уже есть такой id.");
            }
        }

        
        public void RemoveEmployee(int id)
        {
            if (employeeTable.ContainsKey(id))
            {
                employeeTable.Remove(id);
            }
            else
            {
                Console.WriteLine("Нет такого id.");
            }
        }

        
        public Employee? FindEmployee(int id)
        {
            if (employeeTable.ContainsKey(id))
            {
                return (Employee)employeeTable[id];
            }
            Console.WriteLine("Нет такого id.");
            return null;
        }

        
        public void DisplayAllEmployees()
        {
            foreach (DictionaryEntry entry in employeeTable)
            {
                Console.WriteLine(entry.Value);
            }
        }

        
        public IEnumerator<Employee> GetEnumerator()
        {
            foreach (DictionaryEntry entry in employeeTable)
            {
                yield return (Employee)entry.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
