using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Celebrity_MSSQL;
using static DAL_Celebrity_MSSQL.DAL_Celebrity_MSSQL;

namespace DAL_Celebrity_MSSQL
{

    public class Init
    {
        static string connstring = "Data Source=localhost;Initial Catalog=CelebrityDB;Integrated Security=True;TrustServerCertificate=True;";
        public Init() { }

        public Init(string conn) { connstring = conn; }

        public static void Execute(bool delete = true, bool create = true)
        {
            Context context = new Context(connstring);

            if (delete) context.Database.EnsureDeleted();
            if (create) context.Database.EnsureCreated();

            Func<string, string> puri = (string f) => $"{f}";

            //1
            {
                Celebrity c = new Celebrity() { FullName = "Noam Chomsky", Nationality = "US", ReqPhotoPath = puri("Chomsky.jpg") };
                Lifeevent l1 = new Lifeevent() { CelebrityId = 1, Date = new DateTime(1928, 12, 7), Description = "Дата рождения", ReqPhotoPath = null };
                Lifeevent l2 = new Lifeevent() { CelebrityId = 1, Date = new DateTime(1955, 1, 1), Description = "Издание книги \"Логическая структура лингвистической теории\"", ReqPhotoPath = null };

                context.Celebrities.Add(c);
                context.LifeEvents.Add(l1);
                context.LifeEvents.Add(l2);
            }

            // //2
            {
                Celebrity c = new Celebrity() { FullName = "Tim Berners-Lee", Nationality = "UK", ReqPhotoPath = puri("Berners-Lee.jpg") };
                Lifeevent l1 = new Lifeevent() { CelebrityId = 2, Date = new DateTime(1955, 6, 8), Description = "Дата рождения", ReqPhotoPath = null };
                Lifeevent l2 = new Lifeevent() { CelebrityId = 2, Date = new DateTime(1989, 6, 8), Description = "В CERN предложил \"Гипертекстовый проект\"", ReqPhotoPath = null };

                context.Celebrities.Add(c);
                context.LifeEvents.Add(l1);
                context.LifeEvents.Add(l2);
            }

            // //3
            {
                Celebrity c = new Celebrity() { FullName = "Edgar Codd", Nationality = "US", ReqPhotoPath = puri("Codd.jpg") };
                Lifeevent l1 = new Lifeevent() { CelebrityId = 3, Date = new DateTime(1923, 8, 23), Description = "Дата рождения", ReqPhotoPath = null };
                Lifeevent l2 = new Lifeevent() { CelebrityId = 3, Date = new DateTime(2003, 4, 18), Description = "Дата смерти", ReqPhotoPath = null };

                context.Celebrities.Add(c);
                context.LifeEvents.Add(l1);
                context.LifeEvents.Add(l2);
            }

            // //4
            {
                Celebrity c = new Celebrity() { FullName = "Donald Knuth", Nationality = "US", ReqPhotoPath = puri("Knuth.jpg") };
                Lifeevent l1 = new Lifeevent() { CelebrityId = 4, Date = new DateTime(1938, 1, 10), Description = "Дата рождения", ReqPhotoPath = null };
                Lifeevent l2 = new Lifeevent() { CelebrityId = 4, Date = new DateTime(1974, 1, 1), Description = "Премия Тьюринга", ReqPhotoPath = null };

                context.Celebrities.Add(c);
                context.LifeEvents.Add(l1);
                context.LifeEvents.Add(l2);
            }

            //5
            {
                Celebrity c = new Celebrity() { FullName = "Linus Torvalds", Nationality = "US", ReqPhotoPath = puri("Linus.jpg") };
                Lifeevent l1 = new Lifeevent()
                {
                    CelebrityId = 5,
                    Date = new DateTime(1969, 12, 28),
                    Description = "Дата рождения. Финляндия.",
                    ReqPhotoPath = null
                };
                Lifeevent l2 = new Lifeevent()
                {
                    CelebrityId = 5,
                    Date = new DateTime(1991, 9, 17),
                    Description = "Выложил исходный код OS Linus (версия 0.01)",
                    ReqPhotoPath = null
                };

                context.Celebrities.Add(c);
                context.LifeEvents.Add(l1);
                context.LifeEvents.Add(l2);
            }
            {
                //6
                Celebrity c = new Celebrity()
                {
                    FullName = "John Neumann",
                    Nationality = "US",
                    ReqPhotoPath = puri("Neumann.jpg")
                };

                Lifeevent l1 = new Lifeevent()
                {
                    CelebrityId = 6,
                    Date = new DateTime(1903, 12, 28),
                    Description = "Дата рождения. Венгрия",
                    ReqPhotoPath = null
                };

                Lifeevent l2 = new Lifeevent()
                {
                    CelebrityId = 6,
                    Date = new DateTime(1957, 2, 8),
                    Description = "Дата смерти.",
                    ReqPhotoPath = null
                };

                context.Celebrities.Add(c);
                context.LifeEvents.Add(l1);
                context.LifeEvents.Add(l2);
            }
            // 7
            {
                Celebrity c = new Celebrity()
                {
                    FullName = "Edger Dijkstra",
                    Nationality = "NL",
                    ReqPhotoPath = puri("Dijkstra.jpg")
                };

                Lifeevent l1 = new Lifeevent()
                {
                    CelebrityId = 7,
                    Date = new DateTime(1930, 12, 28),
                    Description = "Дата рождения.",
                    ReqPhotoPath = null
                };

                Lifeevent l2 = new Lifeevent()
                {
                    CelebrityId = 7,
                    Date = new DateTime(2002, 8, 6),
                    Description = "Дата смерти.",
                    ReqPhotoPath = null
                };

                context.Celebrities.Add(c);
                context.LifeEvents.Add(l1);
                context.LifeEvents.Add(l2);
            }
            //8
            {
                Celebrity c = new Celebrity()
                {
                    FullName = "Ada Lovelace",
                    Nationality = "UK",
                    ReqPhotoPath = puri("Lovelace.jpg")
                };

                Lifeevent l1 = new Lifeevent()
                {
                    CelebrityId = 8,
                    Date = new DateTime(1815, 12, 10),
                    Description = "Дата рождения.",
                    ReqPhotoPath = null
                };

                Lifeevent l2 = new Lifeevent()
                {
                    CelebrityId = 8,
                    Date = new DateTime(1852, 11, 27),
                    Description = "Дата смерти.",
                    ReqPhotoPath = null
                };

                context.Celebrities.Add(c);
                context.LifeEvents.Add(l1);
                context.LifeEvents.Add(l2);
            }
            //9
            {
                Celebrity c = new Celebrity()
                {
                    FullName = "Charles Babbage",
                    Nationality = "UK",
                    ReqPhotoPath = puri("Babbage.jpg")
                };

                Lifeevent l1 = new Lifeevent()
                {
                    CelebrityId = 9,
                    Date = new DateTime(1791, 12, 26),
                    Description = "Дата рождения.",
                    ReqPhotoPath = null
                };

                Lifeevent l2 = new Lifeevent()
                {
                    CelebrityId = 9,
                    Date = new DateTime(1871, 10, 18),
                    Description = "Дата смерти.",
                    ReqPhotoPath = null
                };

                context.Celebrities.Add(c);
                context.LifeEvents.Add(l1);
                context.LifeEvents.Add(l2);
            }
            //10
            {
                Celebrity c = new Celebrity()
                {
                    FullName = "Andrew Tanenbaum",
                    Nationality = "NL",
                    ReqPhotoPath = puri("Tanenbaum.jpg")
                };

                Lifeevent l1 = new Lifeevent()
                {
                    CelebrityId = 10,
                    Date = new DateTime(1944, 3, 16),
                    Description = "Дата рождения.",
                    ReqPhotoPath = null
                };

                Lifeevent l2 = new Lifeevent()
                {
                    CelebrityId = 10,
                    Date = new DateTime(1987, 1, 1),
                    Description = "Создал OS MINIX - бесплатную Unix-подобную систему",
                    ReqPhotoPath = null
                };

                context.Celebrities.Add(c);
                context.LifeEvents.Add(l1);
                context.LifeEvents.Add(l2);
            }
            context.SaveChanges();
        }
    }
}

