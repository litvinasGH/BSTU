using Lab4OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var intCollection = new CollectionType<int>(new[] { 1, 2, 3, 4, 5 });
            intCollection.Add(6);
            intCollection.Remove(3);
            Console.WriteLine("INT: " + string.Join(", ", intCollection.View()));
            Console.WriteLine("Max: " + intCollection.Max());
            Console.WriteLine("Min: " + intCollection.Min());

            var doubleCollection = new CollectionType<double>(new[] { 1.1, 2.2, 3.3 });
            doubleCollection.Add(4.4);
            Console.WriteLine("DOUBLE: " + string.Join(", ", doubleCollection.View()));

            var stringCollection = new CollectionType<string>(new[] { "apple", "banana", "cherry" });
            stringCollection.Add("date");
            Console.WriteLine("STRING: " + string.Join(", ", stringCollection.View()));
            Console.WriteLine("Contains 'banana': " + stringCollection.Contains(s => s == "banana"));

            FileHandler.SaveToXmlFile("docSTR.xml", stringCollection);
            FileHandler.LoadFromXmlFile("docSTR.xml", stringCollection);



            AdressS adress = new AdressS();
            adress.Street = "Street";
            adress.Num = 123;
            adress.Index = IndexE.fifth;
            Organization org1 = new Organization("Company A", adress);
            AdressS adress2 = new AdressS();
            adress2.Street = "Avenue";
            adress2.Num = 456;
            adress2.Index = IndexE.third;
            Organization org2 = new Organization("Company B", adress2);
            DateInfo date = new DateInfo(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);

            Receipt receipt = new Receipt(org1, date, 150.50);
            Invoice invoice = new Invoice(org1, org2, date, "Чай", 20);
            Check check = new Check(org2, date, 1000);

            CollectionType<DocumentBase> manager = new CollectionType<DocumentBase>();
            manager.Add(receipt);
            manager.Add(invoice);
            manager.Add(check);

            FileHandler.SaveToXmlFile("doc.xml", manager);
            FileHandler.LoadFromXmlFile("doc.xml", manager);

            manager.PrintAll();


        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//
//namespace Lab3OOP
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            List list1 = new List(new int[] { 1, 2, 3 });
//            List list2 = new List(new int[] { 4, 5, 6 });

//            list2.production = new List.Production(1, "name");
//            List.Developer developer = new List.Developer("name", 1, "dep"); 

//            Console.WriteLine("List 1: " + list1.Out());
//            Console.WriteLine("List 2: " + list2.Out());

//            List list3 = 10 + list1;
//            Console.WriteLine("List 3 (10 + List 1): " + list3.Out());

//            --list3;
//            Console.WriteLine("--List3: " + list3.Out());

//            bool isEqual = list1 != list2;
//            Console.WriteLine("List 1 != List 2: " + isEqual);
//            isEqual = list1 != list3;
//            Console.WriteLine("List 1 != List 3: " + isEqual);

//            Console.WriteLine("List 1 Sum: " + StatisticOperation.Sum(list1));
//            Console.WriteLine("List 2 DifferenceBetweenMaxAndMin: " + StatisticOperation.DifferenceBetweenMaxAndMin(list2));
//            Console.WriteLine("List 3 CountElements: " + StatisticOperation.CountElements(list3));
//            string str = "Hello I am hiro Boy";
//            Console.WriteLine("CountWordsWithCapital \"Hello I am hiro Boy\": " +
//                str.CountWordsWithCapital());
//            Console.WriteLine("HasDuplicates list 1: " + list1.HasDuplicates());
//            list1 = 2 + list1;
//            Console.WriteLine("HasDuplicates list 1: " + list1.HasDuplicates());
//        }
//    }
//}