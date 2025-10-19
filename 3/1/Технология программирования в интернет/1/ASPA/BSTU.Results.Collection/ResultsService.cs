using System.Text.Json;

namespace BSTU.Results.Collection
{
    public class ResultsService : IResultsService
    {
        private readonly string _filePath;
        private readonly ReaderWriterLockSlim _lock = new();
        private List<ResultItem> _cache = new();

        public ResultsService(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            EnsureLoaded();
        }

        private void EnsureLoaded()
        {
            _lock.EnterWriteLock();
            try
            {
                var dir = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);

                if (!File.Exists(_filePath))
                {
                    _cache = new List<ResultItem>();
                    SaveToFile();
                    return;
                }

                var json = File.ReadAllText(_filePath);
                _cache = string.IsNullOrWhiteSpace(json)
                    ? new List<ResultItem>()
                    : JsonSerializer.Deserialize<List<ResultItem>>(json) ?? new List<ResultItem>();
            }
            finally { _lock.ExitWriteLock(); }
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_cache);
            File.WriteAllText(_filePath, json);
        }

        public Task<List<ResultItem>> GetAllAsync()
        {
            _lock.EnterReadLock();
            try { return Task.FromResult(_cache.AsEnumerable().ToList()); }
            finally { _lock.ExitReadLock(); }
        }

        public Task<ResultItem?> GetAsync(int id)
        {
            _lock.EnterReadLock();
            try { return Task.FromResult(_cache.FirstOrDefault(x => x.Id == id)); }
            finally { _lock.ExitReadLock(); }
        }

        public Task<ResultItem> AddAsync(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _lock.EnterWriteLock();
            try
            {
                var nextId = _cache.Any() ? _cache.Max(x => x.Id) + 1 : 1;
                var item = new ResultItem(nextId, value);
                _cache.Add(item);
                SaveToFile();
                return Task.FromResult(item);
            }
            finally { _lock.ExitWriteLock(); }
        }

        public Task<bool> UpdateAsync(int id, string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _lock.EnterWriteLock();
            try
            {
                var idx = _cache.FindIndex(x => x.Id == id);
                if (idx < 0) return Task.FromResult(false);
                _cache[idx] = new ResultItem(id, value);
                SaveToFile();
                return Task.FromResult(true);
            }
            finally { _lock.ExitWriteLock(); }
        }

        public Task<bool> DeleteAsync(int id)
        {
            _lock.EnterWriteLock();
            try
            {
                var removed = _cache.RemoveAll(x => x.Id == id) > 0;
                if (removed) SaveToFile();
                return Task.FromResult(removed);
            }
            finally { _lock.ExitWriteLock(); }
        }
    }
}
