using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using static DAL_Celebrity_MSSQL.DAL_Celebrity_MSSQL;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace DAL_Celebrity_MSSQL
{
    public class DAL_Celebrity_MSSQL
    {
        public interface IRepository : DAL_Celebrity.IRepository<Celebrity, Lifeevent> { }

        public class Celebrity  // Знаменитость
        {
            public Celebrity() { this.FullName = string.Empty; this.Nationality = string.Empty; }

            public int id { get; set; }                         // Id Знаменитости
            public string FullName { get; set; }                // полное имя Знаменитости
            public string Nationality { get; set; }             // гражданство Знаменитости (2 символа ISO)
            public string? ReqPhotoPath { get; set; }           // request path Фотографии

            public virtual bool Update(Celebrity celebrity)     // вспомогательный метод
            {
                return true;
            }
        }

        public class Lifeevent  // Событие в жизни знаменитости
        {
            public Lifeevent() { this.Description = string.Empty; }

            public int id { get; set; }                         // Id События
            public int CelebrityId { get; set; }                // Id Знаменитости
            public DateTime? Date { get; set; }                 // дата События
            public string Description { get; set; }             // описание События
            public string? ReqPhotoPath { get; set; }           // request path Фотографии

            public virtual bool Update(Lifeevent lifeevent)     // вспомогательный метод
            {
                return true;
            }
        }


        public class Repository : IRepository
        {
            Context context;

            public Repository() { this.context = new Context(); }

            public Repository(string connectionString)
            {
                this.context = new Context(connectionString);
            }

            public static IRepository Create() { return new Repository(); }

            public static IRepository Create(string connectionString)
            {
                return new Repository(connectionString);
            }

            public List<Celebrity> GetAllCelebrities()
            {
                return this.context.Celebrities.ToList<Celebrity>();
            }

            public Celebrity? GetCelebrityById(int id)
            {
                try
                {
                    return this.context.Celebrities.FirstOrDefault(c => c.id == id);
                }
                catch
                {
                    return null;
                }
            }

            public bool AddCelebrity(Celebrity celebrity)
            {
                try
                {
                    this.context.Celebrities.Add(celebrity);
                    this.context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool DelCelebrity(int id)
            {
                try
                {
                    Celebrity? celebrity = this.context.Celebrities.FirstOrDefault(c => c.id == id);
                    if (celebrity != null)
                    {
                        this.context.Celebrities.Remove(celebrity);
                        this.context.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }

            public bool UpdCelebrity(Celebrity celebrity)
            {
                try
                {
                    Celebrity? existingCelebrity = this.context.Celebrities.Find(celebrity.id);
                    if (existingCelebrity != null)
                    {
                        existingCelebrity.FullName = celebrity.FullName;
                        existingCelebrity.Nationality = celebrity.Nationality;
                        existingCelebrity.ReqPhotoPath = celebrity.ReqPhotoPath;

                        this.context.Celebrities.Update(existingCelebrity);
                        this.context.SaveChanges();

                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обновлении знаменитости: {ex.Message}");
                    return false;
                }
            }

            public List<Lifeevent> GetAllLifeevents()
            {
                return this.context.LifeEvents.ToList<Lifeevent>();
            }

            public Lifeevent? GetLifeeventById(int id)
            {
                try
                {
                    return this.context.LifeEvents.FirstOrDefault(l => l.id == id);
                }
                catch
                {
                    return null;
                }
            }

            public bool AddLifeevent(Lifeevent lifeevent)
            {
                try
                {
                    this.context.LifeEvents.Add(lifeevent);
                    this.context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public bool DelLifeevent(int id)
            {
                try
                {
                    Lifeevent? lifeevent = this.context.LifeEvents.FirstOrDefault(l => l.id == id);
                    if (lifeevent != null)
                    {
                        this.context.LifeEvents.Remove(lifeevent);
                        this.context.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }

            public bool UpdLifeevent(Lifeevent lifeevent)
            {
                try
                {
                    Lifeevent? existingLifeevent = this.context.LifeEvents.FirstOrDefault(l => l.id == lifeevent.id);
                    if (existingLifeevent != null)
                    {
                        existingLifeevent.CelebrityId = lifeevent.CelebrityId;
                        existingLifeevent.Date = lifeevent.Date;
                        existingLifeevent.Description = lifeevent.Description;
                        existingLifeevent.ReqPhotoPath = lifeevent.ReqPhotoPath;
                        this.context.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }

            public List<Lifeevent> GetLifeeventsByCelebrityId(int celebrityId)
            {
                try
                {
                    return this.context.LifeEvents.Where(l => l.CelebrityId == celebrityId).ToList();
                }
                catch
                {
                    return new List<Lifeevent>();
                }
            }

            public Celebrity? GetCelebrityByLifeeventId(int lifeeventId)
            {
                try
                {
                    Lifeevent? lifeevent = this.context.LifeEvents.FirstOrDefault(l => l.id == lifeeventId);
                    if (lifeevent != null)
                    {
                        return this.context.Celebrities.FirstOrDefault(c => c.id == lifeevent.CelebrityId);
                    }
                    return null;
                }
                catch
                {
                    return null;
                }
            }

            public int GetCelebrityIdByName(string name)
            {
                try
                {
                    Celebrity? celebrity = this.context.Celebrities.FirstOrDefault(c => c.FullName.Contains(name));
                    return celebrity != null ? celebrity.id : -1;
                }
                catch
                {
                    return -1;
                }
            }

            public void Dispose()
            {
                this.context.Dispose();
            }
        }


        public class Context : DbContext
        {
            public string? ConnectionString { get; private set; } = null;

            public Context(string connstring) : base()
            {
                this.ConnectionString = connstring;
            }

            public Context() : base()
            {
            }

            public DbSet<Celebrity> Celebrities { get; set; }
            public DbSet<Lifeevent> LifeEvents { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (this.ConnectionString == null) this.ConnectionString = @"Data Source=localhost;Initial Catalog=CelebrityDB;Integrated Security=True;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(this.ConnectionString);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Celebrity>().ToTable("Celebrities").HasKey(p => p.id);
                modelBuilder.Entity<Celebrity>().Property(p => p.id).IsRequired();
                modelBuilder.Entity<Celebrity>().Property(p => p.FullName).IsRequired().HasMaxLength(50);
                modelBuilder.Entity<Celebrity>().Property(p => p.Nationality).IsRequired().HasMaxLength(2);
                modelBuilder.Entity<Celebrity>().Property(p => p.ReqPhotoPath).HasMaxLength(200);

                modelBuilder.Entity<Lifeevent>().ToTable("Lifeevents").HasKey(p => p.id);
                modelBuilder.Entity<Lifeevent>().ToTable("Lifeevents");
                modelBuilder.Entity<Lifeevent>().Property(p => p.id).IsRequired();
                modelBuilder.Entity<Lifeevent>().ToTable("Lifeevents").HasOne<Celebrity>().WithMany().HasForeignKey(p => p.CelebrityId);
                modelBuilder.Entity<Lifeevent>().Property(p => p.CelebrityId).IsRequired();
                modelBuilder.Entity<Lifeevent>().Property(p => p.Date);
                modelBuilder.Entity<Lifeevent>().Property(p => p.Description).HasMaxLength(256);
                modelBuilder.Entity<Lifeevent>().Property(p => p.ReqPhotoPath).HasMaxLength(256);

                base.OnModelCreating(modelBuilder);
            }

        }


    }
}
