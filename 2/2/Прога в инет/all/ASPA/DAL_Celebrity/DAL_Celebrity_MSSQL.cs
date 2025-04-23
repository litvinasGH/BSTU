using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL_Celebrity;

public class CelebrityRepository : IRepository<Celebrity, Lifewent>, IDisposable
{
    private readonly CelebrityContext _context;

    public CelebrityRepository(string connectionString)
    {
        _context = new CelebrityContext(connectionString);
    }

    // IMix implementation
    public List<Lifewent> GetLifeventsByCelebrityId(int celebrityId)
    {
        return _context.Lifewents
            .Where(e => e.CelebrityId == celebrityId)
            .ToList();
    }

    public Celebrity? GetCelebrityByLifeventId(int lifeventId)
    {
        return _context.Lifewents
            .Include(e => e.Celebrity)
            .FirstOrDefault(e => e.Id == lifeventId)
            ?.Celebrity;
    }

    // ICelebrity implementation
    public List<Celebrity> GetAllCelebrities()
    {
        return _context.Celebrities.ToList();
    }

    public Celebrity GetCelebrityById(int id)
    {
        return _context.Celebrities.Find(id);
    }

    public bool DeleteCelebrity(int id)
    {
        var celebrity = _context.Celebrities.Find(id);
        if (celebrity == null) return false;

        _context.Celebrities.Remove(celebrity);
        return _context.SaveChanges() > 0;
    }

    public bool AddCelebrity(Celebrity celebrity)
    {
        _context.Celebrities.Add(celebrity);
        return _context.SaveChanges() > 0;
    }

    public bool UpdateCelebrity(int id, Celebrity celebrity)
    {
        var existing = _context.Celebrities.Find(id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(celebrity);
        return _context.SaveChanges() > 0;
    }

    public int GetCelebrityIdByName(string name)
    {
        return _context.Celebrities
            .FirstOrDefault(c => c.FullName.Contains(name))
            ?.Id ?? -1;
    }

    // ILifevent implementation
    public List<Lifewent> GetAllLifevents()
    {
        return _context.Lifewents.ToList();
    }

    public Lifewent GetLifeventById(int id)
    {
        return _context.Lifewents.Find(id);
    }

    public bool DeleteLifevent(int id)
    {
        var lifevent = _context.Lifewents.Find(id);
        if (lifevent == null) return false;

        _context.Lifewents.Remove(lifevent);
        return _context.SaveChanges() > 0;
    }

    public bool AddLifevent(Lifewent lifevent)
    {
        _context.Lifewents.Add(lifevent);
        return _context.SaveChanges() > 0;
    }

    public bool UpdateLifevent(int id, Lifewent lifevent)
    {
        var existing = _context.Lifewents.Find(id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(lifevent);
        return _context.SaveChanges() > 0;
    }

    // IDisposable
    public void Dispose()
    {
        _context?.Dispose();
    }
}

// DbContext class
public class CelebrityContext : DbContext
{
    public CelebrityContext(string connectionString) : base(connectionString) { }

    public DbSet<Celebrity> Celebrities { get; set; }
    public DbSet<Lifewent> Lifewents { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Конфигурация отношений (пример)
        modelBuilder.Entity<Lifewent>()
            .HasRequired(l => l.Celebrity)
            .WithMany()
            .HasForeignKey(l => l.CelebrityId);
    }
}