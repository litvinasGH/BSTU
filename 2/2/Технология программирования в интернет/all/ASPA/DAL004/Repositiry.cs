using System.Diagnostics.SymbolStore;
using System.Text.Json;

namespace DAL004
{
    public interface IRepository : IDisposable
    {
        string BasePath { get; }
        Celebrity[] GetAllCelebrities();
        Celebrity? GetCelebrityById(int id);
        Celebrity[] GetCelebritiesBySurename(string surename);
        string? GetPhotoPathById(int id);
        int? addCelebrity(Celebrity celeb);

        bool delCelebrity(int id);
        int? updCelebrityById(int id, Celebrity celeb);
        int saveChanges();
    }

    public record Celebrity(int? Id, string Firstname, string Surname, string PhotoPath);
    public class Repository : IRepository
    {
        public static string JSONFileName = "Celebrities.json";
        public string BasePath { get; }
        public string FullFilePath { get; }
        public List<Celebrity> celebrities;

        public Repository(string dirPath)
        {
            this.BasePath = Path.Combine(Directory.GetCurrentDirectory(), dirPath);
            this.FullFilePath = Path.Combine(BasePath, JSONFileName);
            try
            {
                var jsonString = File.ReadAllText(this.FullFilePath);
                celebrities = JsonSerializer.Deserialize<List<Celebrity>>(jsonString) ?? new List<Celebrity>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public static Repository Create(string dirPath)
        {
            return new Repository(dirPath);
        }
        public Celebrity[] GetAllCelebrities()
        {
            return this.celebrities.ToArray();
        }

        public Celebrity? GetCelebrityById(int id)
        {
            return this.celebrities.FirstOrDefault(c => c.Id == id);
        }

        public Celebrity[] GetCelebritiesBySurename(string surename)
        {
            return this.celebrities.Where(c => c.Surname == surename).ToArray();
        }

        public string? GetPhotoPathById(int id)
        {
            return this.GetCelebrityById(id)?.PhotoPath;
        }

        public int? addCelebrity(Celebrity celeb)
        {
            //if (!File.Exists(Path.Combine(FullFilePath, celeb.PhotoPath)))
            //    throw new ArgumentException("Нет такой фотографии!");
            if (celeb.Id == null)
            {
                HashSet<int?> usedIds = new HashSet<int?>(celebrities.Select(item => item.Id));
                int candidate = 0;
                while (usedIds.Contains(candidate))
                {
                    candidate++;
                }
                this.celebrities.Add(new Celebrity(candidate, celeb.Firstname, celeb.Surname, celeb.PhotoPath));
                return candidate;
            }
            else
            {
                if (celebrities.Any((celebA) => celebA.Id == celeb.Id)) return null;
                this.celebrities.Add(celeb);
                return celeb.Id;
            }
        }

        public bool delCelebrity(int id)
        {
            if (celebrities.Find(c => c.Id == id) == null)
            {
                return false;
            }
            else
            {
                this.celebrities.RemoveAt(this.celebrities.FindIndex(c => c.Id == id));
                return true;
            }
        }

        public int? updCelebrityById(int id, Celebrity celeb)
        {
            if (this.celebrities.Find(c => c.Id == id) == null)
            {
                return null;
            }
            else
            {
                this.celebrities[this.celebrities.FindIndex(c => c.Id == id)] = celeb;
                return id;
            }
        }

        public int saveChanges()
        {
            int beforeUpdLength = File.ReadAllText(this.FullFilePath).Length;
            var updatedJsonString = JsonSerializer.Serialize(this.celebrities);
            File.WriteAllText(this.FullFilePath, updatedJsonString);
            int afterUpdLength = File.ReadAllText(this.FullFilePath).Length;

            return afterUpdLength - beforeUpdLength;
        }


        public void Dispose()
        {

        }
    }
}