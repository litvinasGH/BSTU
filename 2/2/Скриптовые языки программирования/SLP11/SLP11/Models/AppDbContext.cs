using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SLP11.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts) { }

        public DbSet<Post> Posts => Set<Post>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=ARCHFANBTW\\MYDB;Database=PostsDb;Trusted_Connection=True;MultipleActiveResultSets=true"
                );
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
