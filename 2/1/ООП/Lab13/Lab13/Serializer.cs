using Lab4OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace Lab13
{
    interface ISerialize<T>
    {
        void Serialize(string filename, T document);
        void Deserialize(string filename);

    }

    public class BinSerializer<T> : ISerialize<T>
    {

        public void Serialize(string filename, T document)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream sw = new FileStream(filename, FileMode.Create))
            {
                bf.Serialize(sw, document);
                Console.WriteLine("binserialize");
            }

        }
        public void Deserialize(string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream sw = new FileStream(filename, FileMode.Open))
            {
                var document = (object)bf.Deserialize(sw);
                Console.WriteLine($"{document.ToString()}");
            }
        }
    }

    public class SOAPSerializer<T> : ISerialize<T>
    {
        public void Serialize(string filename, T document)
        {
            SoapFormatter bf = new SoapFormatter();
            using (Stream sw = new FileStream(filename, FileMode.Create))
            {
                bf.Serialize(sw, document);
                Console.WriteLine("SOAPserialize");
            }

        }
        public void Deserialize(string filename)
        {
            SoapFormatter bf = new SoapFormatter();
            using (Stream sw = new FileStream(filename, FileMode.Open))
            {
                var document = (object)bf.Deserialize(sw);
                Console.WriteLine($"{document.ToString()}");
            }
        }
    }


    public class XmlSerializer<T> : ISerialize<T> where T : DocumentBase
    {
        public void Serialize(string filename, T document)
        {
            Type type = document.GetType();

            XmlSerializer xmlSerializer = new XmlSerializer(type);
            using (Stream sw = new FileStream(filename, FileMode.Create))
            {
                xmlSerializer.Serialize(sw, document);
                Console.WriteLine("xmlserialize");
            }
        }
        public void Deserialize(string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (FileStream sw = new FileStream(filename, FileMode.Open))
            {
                T? document = xmlSerializer.Deserialize(sw) as T;
                Console.WriteLine(document.ToString());
            }
        }
    }
    public class JsonSerializer<T> : ISerialize<T>
    {
        public void Serialize(string filename, T document)
        {
            using (Stream sw = new FileStream(filename, FileMode.Create))
            {
                JsonSerializer.Serialize(sw, document);
                Console.WriteLine("jsonserialize");
            }

        }
        public void Deserialize(string filename)
        {
            using (Stream sw = new FileStream(filename, FileMode.Open))
            {
                var document = JsonSerializer.Deserialize<object>(sw);
                Console.WriteLine(document.ToString());
            }
        }
    }
    public class CollectionSerializer<T>
    {
        public void Serialize(string filename, List<T> documents)
        {
            string serializer = JsonSerializer.Serialize(documents, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filename, serializer);
            Console.WriteLine("collectionserializer");
        }
        public void Deserialize(string filename)
        {
            string jsontext = File.ReadAllText(filename);
            List<T> list = JsonSerializer.Deserialize<List<T>>(jsontext);
            foreach (var document in list)
            {
                Console.WriteLine(document.ToString());
            }
        }
    }
}
