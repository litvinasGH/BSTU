using System.Text.Json;
namespace DAL003
{
    public interface IRepository : IDisposable
    {
        string BasePath { get; }
        Celebrity[] GetAllCelebrities();
        Celebrity? GetCelebrityById(int id);
        Celebrity[] GetCelebritiesBySurename(string surename);
        string? GetPhotoPathById(int id);
    }
    public record Celebrity(int Id, string Firstname, string Surname, string PhotoPath);

    public class Repository : IRepository
    {
        public static string JSONFileName = "Celebrities.json";
        public string BasePath { get; }
        public string filePath { get; }
        public List<Celebrity> _celebrities;

        public Repository(string dirPath)
        {
            this.BasePath = Path.Combine(Directory.GetCurrentDirectory(), dirPath);
            this.filePath = Path.Combine(BasePath, JSONFileName);
            if (!Directory.Exists(this.BasePath))
            {
                Directory.CreateDirectory(this.BasePath);
            }
            if (!File.Exists(this.filePath))
            {
                File.WriteAllText(this.filePath, "[]");
            }
            LoadData();
        }

        private void LoadData()
        {
            var json = File.ReadAllText(filePath);
            _celebrities = JsonSerializer.Deserialize<List<Celebrity>>(json) ?? new List<Celebrity>();
        }

        public Celebrity[] GetAllCelebrities()
        {
            return _celebrities.ToArray();
        }

        public Celebrity? GetCelebrityById(int id)
        {
            return _celebrities.FirstOrDefault(c => c.Id == id);
        }

        public Celebrity[] GetCelebritiesBySurename(string surename)
        {
            return _celebrities.Where(c => c.Surname.Equals(surename, StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        public string? GetPhotoPathById(int id)
        {
            return GetCelebrityById(id).PhotoPath;
        }

        public static Repository Create(string dir)
        {
            return new Repository(dir);
        }
        public void Dispose() { }
    }
}