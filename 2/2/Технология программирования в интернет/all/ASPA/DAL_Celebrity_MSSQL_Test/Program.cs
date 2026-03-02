using System;
using System.Collections.Generic;
using System.Linq;
using DAL_Celebrity_MSSQL;
using static DAL_Celebrity_MSSQL.DAL_Celebrity_MSSQL;

internal class Program
{
    private static void Main(string[] args)
    {
        string CS = @"Data Source=ArchFanBTW\MYDB;Initial Catalog=CelebrityDB;Integrated Security=True;TrustServerCertificate=True;";

        Init init = new Init(CS);
        Init.Execute(delete: true, create: true);

        Func<Celebrity, string> printC = (c) => $"Id = {c.id}, FullName = {c.FullName}, Nationality = {c.Nationality}, ReqPhotoPath={c.ReqPhotoPath}";
        Func<Lifeevent, string> printL = (l) => $"Id = {l.id}, CelebrityId = {l.CelebrityId}, Date = {l.Date}, Description = {l.Description}, ReqPhotoPath={l.ReqPhotoPath}";
        Func<string, string> puri = (string f) => $"{f}";


        using (IRepository repo = Repository.Create(CS))
        {
            {
                Console.WriteLine("------ GetAllCelebrities() ------------- ");
                repo.GetAllCelebrities().ForEach(celebrity => Console.WriteLine(printC(celebrity)));
            }
            {
                Console.WriteLine("------ GetAllLifeevents() ------------- ");
                repo.GetAllLifeevents().ForEach(life => Console.WriteLine(printL(life)));
            }
            {
                Console.WriteLine("------ AddCelebrity() --------------- ");
                Celebrity c = new Celebrity { FullName = "Albert Einstein", Nationality = "DE", ReqPhotoPath = puri("Einstein.jpg") };
                if (repo.AddCelebrity(c)) Console.WriteLine($"OK: AddCelebrity: {printC(c)}");
                else Console.WriteLine($"ERROR:AddCelebrity: {printC(c)}");
            }
            {
                Console.WriteLine("------ AddCelebrity() --------------- ");
                Celebrity c = new Celebrity { FullName = "Samuel Huntington", Nationality = "US", ReqPhotoPath = puri("Huntington.jpg") };
                if (repo.AddCelebrity(c)) Console.WriteLine($"OK: AddCelebrity: {printC(c)}");
                else Console.WriteLine($"ERROR:AddCelebrity: {printC(c)}");
            }
            {
                Console.WriteLine("------ DelCelebrity() --------------- ");
                int id = 0;
                if ((id = repo.GetCelebrityIdByName("Einstein")) > 0)
                {
                    repo.DelCelebrity(id);
                    Console.WriteLine($"OK: DelCelebrity: {id}");
                }
                else Console.WriteLine($"ERROR: GetCelebrityIdByName");
            }
            {
                Console.WriteLine("------ UpdCelebrity() --------------- ");
                int id = 0;
                if ((id = repo.GetCelebrityIdByName("Huntington")) > 0)
                {
                    Celebrity? c = repo.GetCelebrityById(id);
                    if (c is not null)
                    {
                        c.FullName = "Samuel P. Huntington";
                        c.Nationality = "US";
                        if (repo.UpdCelebrity(c)) Console.WriteLine($"OK: UpdCelebrity: {printC(c)}");
                        else Console.WriteLine($"ERROR: UpdCelebrity: {printC(c)}");
                    }
                    else Console.WriteLine($"ERROR: GetCelebrityById: {id}");

                    Console.WriteLine($"OK:GetCelebrityById: {printC(c)}");
                }
                else Console.WriteLine($"ERROR: GetCelebrityIdByName");
            }
            {
                Console.WriteLine("------AddLifeEvent()------");

                int id = 0;
                id = repo.GetCelebrityIdByName("Huntington");
                if (id > 0)
                {
                    Lifeevent le = new Lifeevent() { CelebrityId = id, Date = new DateTime(1927, 04, 18), Description = "Дата рождения ", ReqPhotoPath = null };
                    Lifeevent le2 = new Lifeevent() { CelebrityId = id, Date = new DateTime(1927, 04, 18), Description = "Дата рождения ", ReqPhotoPath = null };
                    Lifeevent le3 = new Lifeevent() { CelebrityId = id, Date = new DateTime(2008, 12, 24), Description = "Дата рождения ", ReqPhotoPath = null };
                    Celebrity? c = repo.GetCelebrityById(id);
                    if (c is not null)
                    {
                        if (repo.AddLifeevent(le)) Console.WriteLine($"OK: AddLifeevent {printL(le)}");
                        else Console.WriteLine($"ERROR: AddLifeevent {printL(le)}");
                        if (repo.AddLifeevent(le2)) Console.WriteLine($"OK: AddLifeevent {printL(le2)}");
                        else Console.WriteLine($"ERROR: AddLifeevent {printL(le2)}");
                        if (repo.AddLifeevent(le3)) Console.WriteLine($"OK: AddLifeevent {printL(le3)}");
                        else Console.WriteLine($"ERROR: AddLifeevent {printL(le3)}");
                    }
                    else Console.WriteLine($"ERROR: GetCelebById ");
                }

                else Console.WriteLine($"ERROR: GetCelebIdByNam ");
            }
            {
                Console.WriteLine("------ DelLifeevent() --------------- ");
                int id = 22;
                if (repo.DelLifeevent(id)) Console.WriteLine($"OK: DelLifeevent: {id}");
                else Console.WriteLine($"ERROR: DelLifeevent: {id}");
            }
            {
                Console.WriteLine("------ UpdLifeevent() --------------- ");
                int id = 23;
                Lifeevent? l1;
                if ((l1 = repo.GetLifeeventById(id)) is not null)
                {
                    l1.Description = "Дата смерти";
                    if (repo.UpdLifeevent(l1)) Console.WriteLine($"OK:UpdLifeevent {id}, {printL(l1)}");
                    else Console.WriteLine($"ERROR:UpdLifeevent {id}, {printL(l1)}");
                }
            }
            {
                Console.WriteLine("------ GetLifeeventsByCelebrityId ------------- ");
                int id = 0;
                if ((id = repo.GetCelebrityIdByName("Huntington")) > 0)
                {
                    Celebrity? c = repo.GetCelebrityById(id);
                    if (c != null) repo.GetLifeeventsByCelebrityId(c.id).ForEach(l => Console.WriteLine($"OK: GetLifeeventsByCelebrityId, {id}, {printL(l)}"));
                    else Console.WriteLine($"ERROR: GetLifeeventsByCelebrityId: {id}");
                }
                else Console.WriteLine($"ERROR: GetCelebrityIdByName");
            }
            {
                Console.WriteLine("------ GetCelebrityByLifeeventId ------------- ");
                int id = 23;
                Celebrity? c;
                if ((c = repo.GetCelebrityByLifeeventId(id)) != null) Console.WriteLine($"OK:{printC(c)}");
                else Console.WriteLine($"ERROR: GetCelebrityByLifeeventId, {id}");
            }
        }
        Console.WriteLine("------------>");
        Console.ReadKey();
    }
}