using System;
using System.Collections.Generic;

namespace Lab08OOP
{
    
    class Program
    {
        static void Main(string[] args)
        {
            
            Director director = new Director();

            
            Worker worker1 = new Worker("Иван", 50000);
            Worker worker2 = new Worker("Петр", 45000);
            Student student1 = new Student("Ольга", 10000);
            Student student2 = new Student("Екатерина", 12000);

            
            director.OnRaiseSalary += worker1.HandleRaise;
            director.OnRaiseSalary += student1.HandleRaise;
            director.OnFine += worker2.HandleFine;
            director.OnFine += student2.HandleFine;

            
            director.RaiseSalary(5000);
            director.ApplyFine(2000);

            
            Console.WriteLine(worker1);
            Console.WriteLine(worker2);
            Console.WriteLine(student1);
            Console.WriteLine(student2);

            string input = "  Пример! строки, с   лишними   пробелами и пунктуацией. ";

            StringProcessor.ProcessString(input,
                StringProcessor.RemovePunctuation,
                StringProcessor.TrimExtraSpaces,
                StringProcessor.ToUpperCase,
                StringProcessor.AddPrefix,
                StringProcessor.AddSuffix);
        }
    }
}


