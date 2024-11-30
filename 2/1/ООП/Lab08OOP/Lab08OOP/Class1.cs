using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab08OOP
{
    public class Director
    {
        public delegate void SomeDel(int amount);
        public event SomeDel OnRaiseSalary;
        public event SomeDel OnFine;

        public void RaiseSalary(int amount)
        {
            Console.WriteLine($"Директор повышает зарплату на {amount} рублей.");
            OnRaiseSalary?.Invoke(amount);
        }

        public void ApplyFine(int amount)
        {
            Console.WriteLine($"Директор применяет штраф на {amount} рублей.");
            OnFine?.Invoke(amount);
        }
    }

    public class Worker
    {
        public string Name { get; }
        public int Salary { get; private set; }

        public Worker(string name, int salary)
        {
            Name = name;
            Salary = salary;
        }

        public void HandleRaise(int amount)
        {
            Salary += amount;
            Console.WriteLine($"{Name} получил повышение зарплаты на {amount} рублей.");
        }

        public void HandleFine(int amount)
        {
            Salary -= amount;
            Console.WriteLine($"{Name} получил штраф на {amount} рублей.");
        }

        public override string ToString()
        {
            return $"{Name}: Зарплата = {Salary} рублей.";
        }
    }

    public class Student : Worker
    {
        public Student(string name, int salary) : base(name, salary)
        {
        }
    }
}
