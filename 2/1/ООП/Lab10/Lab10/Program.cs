using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    class Program
    {
        static void Main()
        {

            string[] months = { "June", "July", "May", "December", "January", "February", "March", "April", "August", "September", "October", "November" };


            int n = 4;
            var monthsWithLengthN = months.Where(m => m.Length == n);

            Console.WriteLine($"Месяцы с длиной {n}: {string.Join(", ", monthsWithLengthN)}");


            var summerAndWinterMonths = months.Where(m =>
                m == "June" || m == "July" || m == "August" ||
                m == "December" || m == "January" || m == "February");

            Console.WriteLine($"Летние и зимние месяцы: {string.Join(", ", summerAndWinterMonths)}");


            var monthsAlphabetically = months.OrderBy(m => m);

            Console.WriteLine($"Месяцы в алфавитном порядке: {string.Join(", ", monthsAlphabetically)}");


            var monthsWithUAndLength4 = months.Where(m => m.Contains('u') && m.Length >= 4);

            Console.WriteLine($"Месяцы с буквой 'u' и длиной не менее 4: {string.Join(", ", monthsWithUAndLength4)}");







            List<Product> products = new List<Product>
        {
            new Product("Milk", "123456789012", "DairyCo", 1.5f, 10, 50),
            new Product("Bread", "223456789012", "BakeryCo", 0.8f, 7, 100),
            new Product("Cheese", "323456789012", "DairyCo", 3.5f, 30, 25),
            new Product("Butter", "423456789012", "DairyCo", 2.0f, 20, 40),
            new Product("Eggs", "523456789012", "FarmCo", 2.5f, 15, 60),
            new Product("Juice", "623456789012", "BeverageCo", 1.2f, 60, 30),
            new Product("Water", "723456789012", "BeverageCo", 0.5f, 90, 200),
            new Product("Apple", "823456789012", "FarmCo", 1.0f, 5, 150),
            new Product("Yogurt", "923456789012", "DairyCo", 1.8f, 14, 45),
            new Product("Cake", "013456789012", "BakeryCo", 5.0f, 10, 10)
        };

            
            string targetName = "Milk";
            var productsByName = products.Where(p => p.Name == targetName);

            Console.WriteLine($"\n\nТовары с наименованием \"{targetName}\":\n{string.Join(",\n", productsByName)}");

            
            float maxPrice = 2.0f;
            var affordableProducts = products.Where(p => p.Name == targetName && p.Price <= maxPrice);

            Console.WriteLine($"\nТовары с наименованием \"{targetName}\" и ценой не выше {maxPrice}:\n{string.Join(",\n", affordableProducts)}");

            
            int countExpensive = products.Count(p => p.Price > 100);

            Console.WriteLine($"\nКоличество товаров с ценой больше 100: {countExpensive}");

            
            var maxProduct = products.Max(p => p.Price);

            Console.WriteLine($"\nТовар с максимальной ценой: {maxProduct}");


            var sortedProducts =
            from p in products
            orderby p.Produser, p.Count
            select p;

            Console.WriteLine($"\nТовары, упорядоченные по производителю и количеству:\n{string.Join(",\n", sortedProducts)}");






            var query =
           from product in products
           where product.Price > 1.0f
           orderby product.Produser, product.Price descending
           group product by product.Produser into grouped
           select new
           {
               Producer = grouped.Key,
               TotalProducts = grouped.Count(),
               MaxPrice = grouped.Max(p => p.Price),
               HasExpensiveItems = grouped.Any(p => p.Price > 3.0f),
               Items = grouped.ToArray()
           };

            
            Console.WriteLine("\n\nРезультаты сложного запроса:");
            foreach (var result in query)
            {
                Console.WriteLine($"Производитель: {result.Producer}");
                Console.WriteLine($"  Всего товаров: {result.TotalProducts}");
                Console.WriteLine($"  Максимальная цена: {result.MaxPrice}");
                Console.WriteLine($"  Есть ли товары дороже 3.0: {result.HasExpensiveItems}");
                foreach (var item in result.Items)
                {
                    Console.WriteLine(item + ",");
                }
            }








            List<Category> categories = new List<Category>
        {
            new Category { Id = "DairyCo", Name = "Молочные продукты" },
            new Category { Id = "BakeryCo", Name = "Выпечка" },
            new Category { Id = "FarmCo", Name = "Фермерские продукты" },
            new Category { Id = "BeverageCo", Name = "Напитки" }
        };




            var queryCat =
            from product in products
            join category in categories
            on product.Produser equals category.Id
            select new
            {
                ProductName = product.Name,
                CategoryName = category.Name,
                Price = product.Price
            };

            Console.WriteLine("\n\nПродукты и их категории:");
            foreach (var item in queryCat)
            {
                Console.WriteLine($"Товар: {item.ProductName}, Категория: {item.CategoryName}, Цена: {item.Price}");
            }
        }
    }
}
