
using System.Xml.Serialization;

namespace Lab4OOP{
    static class FileHandler
{
    public static void SaveToXmlFile<T>(string filePath, Container<T> container)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, container.Documents);
        }
    }

    public static void LoadFromXmlFile<T>(string filePath, Container<T> container) where T : DocumentBase
    {
        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                var documents = (List<T>)serializer.Deserialize(stream);
                container.Documents.Clear();
                container.Documents.AddRange(documents);
            }
        }
        else
        {
            throw new FileNotFoundException("Файл не найден: " + filePath);
        }
    }
}
}