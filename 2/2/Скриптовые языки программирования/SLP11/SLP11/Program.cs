using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using SLP11.Models;              
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration
                                      .GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(connectionString));

       
        builder.Services.AddCors(opts =>
        {
            opts.AddDefaultPolicy(policy =>
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
        });

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseCors();
        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
            if (!db.Posts.Any())
            {
                db.Posts.AddRange(
                    new Post { UserId = 1, Title = "Hello", Body = "World" },
                    new Post { UserId = 1, Title = "Foo", Body = "Bar" }
                );
                db.SaveChanges();
            }
        }

        app.Run();
    }
}
