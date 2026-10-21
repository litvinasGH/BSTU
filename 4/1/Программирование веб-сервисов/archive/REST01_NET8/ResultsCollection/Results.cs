using System.Collections.Concurrent;
using System.Text.Json;

namespace ResultsCollection;

public sealed record ResultItem(int Id, string Value);

public sealed class Results
{
    private readonly string _path;

    private static readonly ConcurrentDictionary<string, object> FileLocks =
        new(StringComparer.OrdinalIgnoreCase);

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public Results(string path)
    {
        _path = Path.GetFullPath(path);
    }

    private T Locked<T>(
        Func<List<ResultItem>, T> operation,
        bool save = false)
    {
        var lockObject = FileLocks.GetOrAdd(
            _path,
            _ => new object());

        lock (lockObject)
        {
            var directory = Path.GetDirectoryName(_path);
            Directory.CreateDirectory(directory!);

            using var stream = new FileStream(
                _path,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.None);

            var items = ReadItems(stream);
            var result = operation(items);

            if (save)
            {
                SaveItems(stream, items);
            }

            return result;
        }
    }

    private static List<ResultItem> ReadItems(FileStream stream)
    {
        if (stream.Length == 0)
        {
            return new List<ResultItem>();
        }

        return JsonSerializer.Deserialize<List<ResultItem>>(
                   stream,
                   JsonOptions)
               ?? new List<ResultItem>();
    }

    private static void SaveItems(
        FileStream stream,
        List<ResultItem> items)
    {
        stream.Position = 0;
        stream.SetLength(0);

        JsonSerializer.Serialize(
            stream,
            items,
            JsonOptions);

        stream.Flush(flushToDisk: true);
    }

    public IReadOnlyList<ResultItem> All()
    {
        return Locked(items => items.ToList());
    }

    public ResultItem? Get(int id)
    {
        return Locked(items =>
            items.FirstOrDefault(item => item.Id == id));
    }

    public ResultItem Add(string value)
    {
        return Locked(
            items =>
            {
                var nextId = items.Count == 0
                    ? 1
                    : checked(items.Max(item => item.Id) + 1);

                var item = new ResultItem(nextId, value);
                items.Add(item);

                return item;
            },
            save: true);
    }

    public ResultItem? Update(int id, string value)
    {
        return Locked(
            items =>
            {
                var index = items.FindIndex(item => item.Id == id);

                if (index < 0)
                {
                    return null;
                }

                var updatedItem = items[index] with
                {
                    Value = value
                };

                items[index] = updatedItem;

                return updatedItem;
            },
            save: true);
    }

    public ResultItem? Delete(int id)
    {
        return Locked(
            items =>
            {
                var index = items.FindIndex(item => item.Id == id);

                if (index < 0)
                {
                    return null;
                }

                var removedItem = items[index];
                items.RemoveAt(index);

                return removedItem;
            },
            save: true);
    }
}
