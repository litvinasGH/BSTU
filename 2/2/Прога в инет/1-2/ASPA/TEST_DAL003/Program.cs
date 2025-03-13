using DAL003;
internal class Program
{
    public static void Main(string[] args)
    {
        using (IRepository repository = Repository.Create("Celebrities"))
        {
            Console.WriteLine(repository.BasePath);
            foreach (Celebrity celebrity in repository.GetAllCelebrities())
            {
                Console.WriteLine($"Id= {celebrity.Id}, First name = {celebrity.Firstname}, " +
                    $"Surename = {celebrity.Surname},Photo Path = {celebrity.PhotoPath} ");
            }

            Celebrity? celeb1 = repository.GetCelebrityById(1);

            if (celeb1 != null)
            {
                Console.WriteLine($"Id = {celeb1.Id},First name = {celeb1.Firstname}, " +
                    $"Surename = {celeb1.Surname}, Photopath = {celeb1.PhotoPath}");

            }

            Celebrity? celeb3 = repository.GetCelebrityById(3);
            if (celeb3 != null)
            {
                Console.WriteLine($"Id = {celeb3.Id},First name = {celeb3.Firstname}, " +
                    $"Surename = {celeb3.Surname}, Photopath = {celeb3.PhotoPath}");
            }

            Celebrity? celeb7 = repository.GetCelebrityById(7);
            if (celeb7 != null)
            {
                Console.WriteLine($"Id = {celeb7.Id},First name = {celeb7.Firstname}, " +
                     $"Surename = {celeb7.Surname}, Photopath = {celeb7.PhotoPath}");
            }

            Celebrity? celeb222 = repository.GetCelebrityById(222);
            if (celeb222 != null)
            {
                Console.WriteLine($"Id = {celeb222.Id},First name = {celeb222.Firstname}, " +
                    $"Surename = {celeb222.Surname}, Photopath = {celeb222.PhotoPath}");
            }
            else
            {
                Console.WriteLine("Not found 222");
            }

            foreach (Celebrity celebr in repository.GetCelebritiesBySurename("Chomsky"))
            {
                Console.WriteLine($"Id = {celebr.Id},First name = {celebr.Firstname}, " +
                    $"Surename = {celebr.Surname}, Photopath = {celebr.PhotoPath}");
            }
            foreach (Celebrity celebr in repository.GetCelebritiesBySurename("Knuth"))
            {
                Console.WriteLine($"Id = {celebr.Id},First name = {celebr.Firstname}, " +
                     $"Surename = {celebr.Surname}, Photopath = {celebr.PhotoPath}");
            }
            foreach (Celebrity celebr in repository.GetCelebritiesBySurename("XXXX"))
            {
                Console.WriteLine($"Id = {celebr.Id},First name = {celebr.Firstname}, " +
                    $"Surename = {celebr.Surname}, Photopath = {celebr.PhotoPath}");
            }
        }
    }
}