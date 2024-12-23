using System.Reflection;
using System.Xml.Linq;

public static class Reflector
{
    public static void SaveAssemblyName(string className, string filePath)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Class {className} not found.");

        string assemblyName = type.Assembly.FullName;
        var xml = new XElement("AssemblyName", assemblyName);
        xml.Save(filePath);
    }

    public static void SaveHasPublicConstructors(string className, string filePath)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Class {className} not found.");

        bool hasPublicConstructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Any();
        var xml = new XElement("HasPublicConstructors", hasPublicConstructors);
        xml.Save(filePath);
    }

    public static IEnumerable<string> GetPublicMethods(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Class {className} not found.");

        return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                   .Select(m => m.Name);
    }

    public static void SavePublicMethods(string className, string filePath)
    {
        var methods = GetPublicMethods(className);
        var xml = new XElement("PublicMethods",
            methods.Select(m => new XElement("Method", m)));
        xml.Save(filePath);
    }

    public static IEnumerable<string> GetFieldsAndProperties(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Class {className} not found.");

        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                         .Select(f => f.Name);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                              .Select(p => p.Name);

        return fields.Concat(properties);
    }

    public static void SaveFieldsAndProperties(string className, string filePath)
    {
        var fieldsAndProperties = GetFieldsAndProperties(className);
        var xml = new XElement("FieldsAndProperties",
            fieldsAndProperties.Select(fp => new XElement("Member", fp)));
        xml.Save(filePath);
    }

    public static IEnumerable<string> GetImplementedInterfaces(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Class {className} not found.");

        return type.GetInterfaces().Select(i => i.Name);
    }

    public static void SaveImplementedInterfaces(string className, string filePath)
    {
        var interfaces = GetImplementedInterfaces(className);
        var xml = new XElement("ImplementedInterfaces",
            interfaces.Select(i => new XElement("Interface", i)));
        xml.Save(filePath);
    }

    public static IEnumerable<string> GetMethodsWithParameterType(string className, string parameterTypeName)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Class {className} not found.");

        Type parameterType = Type.GetType(parameterTypeName);
        if (parameterType == null) throw new ArgumentException($"Parameter type {parameterTypeName} not found.");

        return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                   .Where(m => m.GetParameters().Any(p => p.ParameterType == parameterType))
                   .Select(m => m.Name);
    }

    public static void SaveMethodsWithParameterType(string className, string parameterTypeName, string filePath)
    {
        var methods = GetMethodsWithParameterType(className, parameterTypeName);
        var xml = new XElement("MethodsWithParameterType",
            methods.Select(m => new XElement("Method", m)));
        xml.Save(filePath);
    }

    public static object InvokeMethod(object obj, string methodName, object[] parameters)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));

        Type type = obj.GetType();
        MethodInfo method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        if (method == null) throw new ArgumentException($"Method {methodName} not found.");

        return method.Invoke(obj, parameters);
    }

    public static object[] GenerateParameters(string className, string methodName)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Class {className} not found.");

        MethodInfo method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        if (method == null) throw new ArgumentException($"Method {methodName} not found.");

        var parameters = method.GetParameters();
        var generatedParameters = new List<object>();

        foreach (var parameter in parameters)
        {
            Type parameterType = parameter.ParameterType;
            if (parameterType == typeof(int))
                generatedParameters.Add(42);
            else if (parameterType == typeof(string))
                generatedParameters.Add("example");
            else if (parameterType == typeof(bool))
                generatedParameters.Add(true);
            else
                generatedParameters.Add(Activator.CreateInstance(parameterType));
        }

        return generatedParameters.ToArray();
    }

    public static object InvokeFromFile(string className, string methodName, string filePath)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Class {className} not found.");

        string fileContent = File.ReadAllText(filePath);
        var parameters = XElement.Parse(fileContent)
                                 .Elements("Parameter")
                                 .Select(e => Convert.ChangeType(e.Value, Type.GetType(e.Attribute("Type").Value)))
                                 .ToArray();

        object instance = Activator.CreateInstance(type);
        return InvokeMethod(instance, methodName, parameters);
    }



    public static T Create<T>() where T : class
    {
        Type type = typeof(T);
        ConstructorInfo constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                                           .FirstOrDefault();

        if (constructor == null)
            throw new InvalidOperationException($"No public constructors found for type {type.FullName}.");

        var parameters = constructor.GetParameters()
                                    .Select(p => Activator.CreateInstance(p.ParameterType))
                                    .ToArray();

        return (T)constructor.Invoke(parameters);
    }
}
public class ExampleClass : IComparable
{
    public int Number { get; set; }
    public string Text { get; set; }

    public ExampleClass() { }

    public ExampleClass(int number, string text)
    {
        Number = number;
        Text = text;
    }

    public void PrintMessage(string message)
    {
        Console.WriteLine($"Message: {message}");
    }

    public int AddNumbers(int a, int b)
    {
        return a + b;
    }

    public int CompareTo(object? obj)
    {
        throw new NotImplementedException();
    }
}

class Program
{
    static void Main(string[] args)
    {
        string className = "ExampleClass";
        string assemblyFilePath = "AssemblyName.xml";
        string publicConstructionPth = "PublicConstruction.xml";
        string methodsFilePath = "PublicMethods.xml";
        string fieldsFilePath = "FieldsAndProperties.xml";
        string interfacesFilePath = "ImplementedInterfaces.xml";

        Reflector.SaveAssemblyName(className, assemblyFilePath);
        Console.WriteLine($"Имя сборки сохранено в {assemblyFilePath}");

        Reflector.SaveHasPublicConstructors(className, publicConstructionPth);
        Console.WriteLine($"Публичные конструкторы? в {publicConstructionPth}");

        Reflector.SavePublicMethods(className, methodsFilePath);
        Console.WriteLine($"Публичные методы сохранены в {methodsFilePath}");

        Reflector.SaveFieldsAndProperties(className, fieldsFilePath);
        Console.WriteLine($"Поля и свойства сохранены в {fieldsFilePath}");

        Reflector.SaveImplementedInterfaces(className, interfacesFilePath);
        Console.WriteLine($"Реализованные интерфейсы сохранены в {interfacesFilePath}");

        var instance = new ExampleClass();
        Reflector.InvokeMethod(instance, "PrintMessage", new object[] { "Hello, World!" });

        object[] parameters = Reflector.GenerateParameters(className, "AddNumbers");
        int result = (int)Reflector.InvokeMethod(instance, "AddNumbers", parameters);
        Console.WriteLine($"Результат вызова AddNumbers: {result}");
    }
}

