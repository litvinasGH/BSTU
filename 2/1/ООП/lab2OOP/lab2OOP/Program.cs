using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Product[] products = {
                new Product("Молоко", "123456789101", "БЛ", 24.4f, 1, 50),
                new Product("Печеньки", "123456765243", "К", 5.4f, 12, 30),
                new Product("Конфеты", "457656789876", "К", .4f, 5, 100)
            };
                Console.WriteLine("____Список_продуктов____");
                for (int i = 0; i < Product.count_of_types; i++)
                {
                    products[i].DisplayInfo();
                    Console.WriteLine("________________________");
                }
                Console.WriteLine("_______Сравнение_______");
                Console.WriteLine("product1 == product2: " + products[0].Equals(products[1]));
                Console.WriteLine("product3 == product3: " + products[2].Equals(products[2]));
                Console.WriteLine("______Измен_кол-во_____");
                int newC = 10;
                products[0].ChangeCount(ref newC);
                products[0].DisplayInfo();
                Console.WriteLine("________________________");
                Console.WriteLine("Введите имя для поиска продукта");
                string name = Console.ReadLine();
                bool flag = false;
                foreach (Product prod in products)
                {
                    if (prod.Name == name)
                    {
                        prod.DisplayInfo();
                        Console.WriteLine("________________________");
                        flag = true;
                    }
                }
                if (!flag)
                {
                    Console.WriteLine("Товар не найден");
                    Console.WriteLine("________________________");
                }
                Console.WriteLine("Введите имя и мин цену для поиска продукта");
                name = Console.ReadLine();
                flag = false;
                
                float price;
                string price_str;
                do
                {
                    price_str = Console.ReadLine();
                }
                while (!float.TryParse(price_str, out price));
                foreach (Product prod in products)
                {
                    if (prod.Name == name && prod.Price >= price)
                    {
                        prod.DisplayInfo();
                        Console.WriteLine("________________________");
                        flag = true;
                    }
                }
                if (!flag)
                {
                    Console.WriteLine("Товар не найден");
                    Console.WriteLine("________________________");
                }

                var AnonProd = new
                {
                    id = 1212,
                    name = "Имя",
                    upc = "7628686",
                    produser = "П",
                    price = 32.4f,
                    shelf_live_month = 32,
                    count = 11
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex.Source}: {ex.Message}");
            }
        }
    }
}