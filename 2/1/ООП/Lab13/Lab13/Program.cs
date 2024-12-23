using Lab4OOP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace Lab13
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

            BinSerializer<Check> serializer = new BinSerializer<Check>();
            serializer.Serialize("check.dat", check);
            serializer.Deserialize("check.dat");

            SOAPSerializer<Check> sOAPSerializer = new SOAPSerializer<Check>();
            sOAPSerializer.Serialize("checkSOAP.dat", check);
            sOAPSerializer.Deserialize("checkSOAP.dat");


            JsonSerializer<Check> bs = new JsonSerializer<Check>();
            bs.Serialize("receipt.json", check);
            bs.Deserialize("receipt.json");
            Console.WriteLine();


            XmlSerializer<Check> xs = new XmlSerializer<Check>();
            xs.Serialize("invoice.xml", check);
            xs.Deserialize("invoice.xml");


            List<Check> list = new List<Check>() {check, check, check};

            CollectionSerializer<Check> cs = new CollectionSerializer<Check>();
            cs.Serialize("list.json", list);
            cs.Deserialize("list1.json");
            Console.WriteLine();






            XmlDocument xdoc1 = new XmlDocument();
            xdoc1.Load("invoice.xml");

            XmlElement? xroot = xdoc1.DocumentElement;

            XmlNodeList? nodes = xroot?.SelectNodes("*");
            foreach (XmlNode node in nodes)
            {
                Console.WriteLine(node.OuterXml);
            }
            XmlNodeList? nodes1 = xroot?.SelectNodes("Amount");

            foreach (XmlNode node in nodes1)
            {
                Console.WriteLine(node.OuterXml);
            }

            XDocument xdoc2 = new XDocument();
            XElement doc = new XElement("LOL");
            XElement name = new XElement("SomeElement");
            name.Value = "NAME";

            doc.Add(name);


            XElement decname = new XElement("SomeElement");
            decname.Value = "secNAME";

            doc.Add(decname);

            xdoc2.Add(doc);


            xdoc2.Save("Docs.xml");
        }
    }
}
