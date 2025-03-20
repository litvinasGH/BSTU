using DAL004;
internal class Program
{
    public static void Main(string[] args)
    {


        Repository.JSONFileName = "Celebrities.json";
        using (IRepository repository = Repository.Create("Celebrities"))
        {
            void print(string label)
            {
                {
                    Console.WriteLine("--- " + label + " - ");
                    foreach (Celebrity celebrity in repository.GetAllCelebrities())
                        Console.WriteLine($"Id = {celebrity.Id}, Firstname = {celebrity.Firstname}, " + $"Surname = {celebrity.Surname}, PhotoPath = {celebrity.PhotoPath}");
                }
            }
        ;
            print("start");
            int? testdel1 = repository.addCelebrity(new Celebrity(0, "TestDell", "TestDell", "Photo/TestDel1.jpg"));
            int? testdel2 = repository.addCelebrity(new Celebrity(0, "TestDel2", "TestDel2", "Photo/TestDel2.jpg"));
            int? testupd1 = repository.addCelebrity(new Celebrity(0, "Testupd1", "Testupd1", "Photo/Testupd1.jpg"));
            int? testupd2 = repository.addCelebrity(new Celebrity(0, "Testupd2", "Testupd2", "Photo/Testupd2.jpg"));
            repository.saveChanges();
            print("add 4");
            if (testdel1 != null)
                if (repository.delCelebrity((int)testdel1)) Console.WriteLine($" delete {testdel1} "); else Console.WriteLine($"delete {testdel1} error");
            if (testdel2 != null)
                if (repository.delCelebrity((int)testdel2)) Console.WriteLine($" delete {testdel2}"); else Console.WriteLine($"delete {testdel2} error");
            if (repository.delCelebrity(1000)) Console.WriteLine($" delete {1000} "); else Console.WriteLine($"delete {1000} error");
            repository.saveChanges();
            print("del 2");
            if (testupd1 != null)
                if (repository.updCelebrityById((int)testupd1, new Celebrity(0, "Updated1", "Updated1", "Photo/Updated1.jpg")) != -1)
                    Console.WriteLine($" update {testupd1} ");
                else Console.WriteLine($"update {testupd1} error");
            if (testupd2 != null)
                if (repository.updCelebrityById((int)testupd2, new Celebrity(0, "Updated2", "Updated2", "Photo/Updated2.jpg")) != -1) 
                    Console.WriteLine($" update {testupd2}"); else Console.WriteLine($"update {testupd2} error");
            if (repository.updCelebrityById(1000, new Celebrity(0, "Updated1000", "Updated 1000", "Photo/Updated1000.jpg"))!=-1)
                Console.WriteLine($" update {1000} "); else Console.WriteLine($"update {1000} error");
            repository.saveChanges();
            print("upd 2");
        }

    }
}
