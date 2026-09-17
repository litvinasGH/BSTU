using System.Text.Json;

namespace ResultsCollection;

public class Results
{
    private readonly string _filePath;

    private static readonly object _lock = new();

    public Results(string filePath)
    {
        _filePath = filePath;

        string? directory = Path.GetDirectoryName(_filePath);

        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    public List<Result> GetAll()
    {
        lock (_lock)
        {
            return Read();
        }
    }

    public Result? Get(int id)
    {
        lock (_lock)
        {
            return Read().FirstOrDefault(x => x.Id == id);
        }
    }

    public Result Add(string value)
    {
        lock (_lock)
        {
            List<Result> results = Read();

            int newId = results.Count == 0
                ? 1
                : results.Max(x => x.Id) + 1;

            Result result = new Result
            {
                Id = newId,
                Value = value
            };

            results.Add(result);

            Write(results);

            return result;
        }
    }

    public Result? Update(int id, string value)
    {
        lock (_lock)
        {
            List<Result> results = Read();

            Result? result = results.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                return null;
            }

            result.Value = value;

            Write(results);

            return result;
        }
    }

    public Result? Delete(int id)
    {
        lock (_lock)
        {
            List<Result> results = Read();

            Result? result = results.FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                return null;
            }

            results.Remove(result);

            Write(results);

            return result;
        }
    }

    private List<Result> Read()
    {
        string json = File.ReadAllText(_filePath);

        return JsonSerializer.Deserialize<List<Result>>(json)
               ?? new List<Result>();
    }

    private void Write(List<Result> results)
    {
        string json = JsonSerializer.Serialize(
            results,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(_filePath, json);
    }
}