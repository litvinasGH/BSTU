
using Lab3OOP;
using System.Xml.Serialization;

namespace Lab4OOP{


   

    static class FileHandler
{
    public static void SaveToXmlFile<T>(string filePath, CollectionType<T> Man) where T : IComparable<T>
        {
        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, Man.elements);
        }
    }

    public static void LoadFromXmlFile<T>(string filePath, CollectionType<T> Man) where T : IComparable<T>
    {
        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                var documents = (List<T>)serializer.Deserialize(stream);
                Man.elements.Clear();
                Man.elements.AddRange(documents);
            }
        }
        else
        {
            throw new FileNotFoundException("Файл не найден: " + filePath);
        }
    }
}
}