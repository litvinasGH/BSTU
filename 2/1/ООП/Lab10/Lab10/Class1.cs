using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab10
{
    internal partial class Product
    {
        private readonly int id;
        private string name;
        private string upc;
        private string produser;
        private float price;
        private int shelf_live_month;
        private int count;
        public static int count_of_types = 0;
        public const int cnt = 10;
        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Produser
        {
            get { return produser; }
            set { produser = value; }
        }
        public string Upc
        {
            get { return upc; }
            set
            {
                int c = 0;
                foreach (var ch in value)
                {
                    if (!(ch >= '0' && ch <= '9'))
                    {
                        throw new ArgumentException("НЕКОРЕКТНЫЙ UPC");
                    }
                    c++;
                }
                if (c != 12)
                {
                    throw new ArgumentException("НЕКОРЕКТНЫЙ UPC");
                }
                upc = value;
            }
        }
        public float Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("НЕКОРЕКТНАЯ цена");
                }
                price = value;
            }
        }
        public int Shelf_live_month
        {
            get { return shelf_live_month; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("НЕКОРЕКТНЫЙ срок годности");
                }
                shelf_live_month = value;
            }
        }
        public int Count
        {
            get { return count; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("НЕКОРЕКТНОЕ кол-во");
                }
                count = value;
            }
        }
        public Product(string name, string upc, string produser, float price, int shelf_live_month, int count)
        {
            Name = name;
            id = GetHashCode();
            Upc = upc;
            Produser = produser;
            Price = price;
            Shelf_live_month = shelf_live_month;
            Count = count;
            count_of_types++;
        }
        static Product()
        {
            count_of_types = 0;
        }
        private Product(string name, string upc, string produser, float price, int shelf_live_month = 0)
        {
            Name = name;
            id = GetHashCode();
            Upc = upc;
            Produser = produser;
            Price = price;
            Shelf_live_month = shelf_live_month;
            Count = 0;
            count_of_types++;
        }
        public void DisplayInfo()
        {
            Console.WriteLine("id: " + id);
            Console.WriteLine("Имя: " + name);
            Console.WriteLine("UPC: " + upc);
            Console.WriteLine("Изготовитель: " + produser);
            Console.WriteLine("Цена: " + price + " д.е.");
            Console.WriteLine("Срок годности: " + shelf_live_month + " мес");
            Console.WriteLine("Кол-во: " + count);
        }
        public Product CreateObject(string name, string upc, string produser, float price, int shelf_live_month = 0)
        {
            return new Product(name, upc, produser, price, shelf_live_month);

        }
        public override int GetHashCode()
        {
            var hash = 0;
            foreach (char temp in name)
            {
                hash += Convert.ToInt32(temp);
            }
            return Convert.ToInt32(hash);
        }

        public void ChangeCount(ref int newCount)
        {
            if (newCount <= 0)
            {
                Console.WriteLine("Количество должно быть положительным числом.");
            }
            else
            {
                count = newCount;
                Console.WriteLine($"Новое количество товара \"{name}\": {newCount}");
            }
        }


        public override string ToString()
        {
            return $"ID: {id}, имя: {name}, UPC: {upc}, изготовитель {produser}, цена {price} д.е., " +
                $"срок годности: {shelf_live_month}, кол-во: {count}";
        }

        static void Getinfo()
        {
            Console.WriteLine("Type: " + typeof(Product).FullName);
            Console.WriteLine("Count: " + count_of_types);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Product)) return false;
            Product prod = (Product)obj;
            if (prod == null) return false;
            return id == prod.id && name == prod.name && upc == prod.upc &&
                produser == prod.produser && price == prod.price && shelf_live_month == prod.shelf_live_month &&
                count == prod.count;
        }



    }







    class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
