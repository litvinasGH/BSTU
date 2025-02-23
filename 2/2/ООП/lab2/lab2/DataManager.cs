using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab2
{
    [Serializable]
    public class DataContainer
    {
        public DataContainer(BindingList<FileBook> fileBooks, BindingList<Author> authors, BindingList<Publisher> publishers)
        {
            FileBooks = fileBooks;
            Authors = authors;
            Publishers = publishers;
        }
        public DataContainer() { }

        public BindingList<FileBook> FileBooks { get; set; }
        public BindingList<Author> Authors { get; set; }
        public BindingList<Publisher> Publishers { get; set; }
    }
    internal static class DataManager
    {
        public static void SaveToXml(DataContainer data, string filePath)
        {
            var serializer = new XmlSerializer(typeof(DataContainer));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, data);
            }
        }

        public static DataContainer LoadFromXml(string filePath)
        {
            if (!File.Exists(filePath))
                return new DataContainer(); 

            var serializer = new XmlSerializer(typeof(DataContainer)); 
            using (var reader = new StreamReader(filePath))
            {
                return (DataContainer)serializer.Deserialize(reader);
            }
        }
    }
}
