using DAL003;
using Microsoft.Extensions.FileProviders;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        Repository.JSONFileName = "Celebrities.json";
        using (Repository repository = Repository.Create("Celebrities"))
        {
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(repository.BasePath),
                RequestPath = new PathString("/Photo")
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(repository.BasePath),
                RequestPath = new PathString("/Celebrities/download"),

                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Content-Disposition", "attachment");
                }
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(repository.BasePath),
                RequestPath = "/Celebrities/download",

            });
            app.MapGet("/Celebrities", () => repository.GetAllCelebrities());
            app.MapGet("/Celebrities/{id:int}", (int id) => repository.GetCelebrityById(id));
            app.MapGet("/Celebrities/BySurename/{surename}", (string surename) => repository.GetCelebritiesBySurename(surename));
            app.MapGet("/Celebrities/PhotoPathById/{id:int}", (int id) => repository.GetPhotoPathById(id));
            app.MapGet("/", () => $"{repository.filePath}");
            app.Run();
        }
    }
}