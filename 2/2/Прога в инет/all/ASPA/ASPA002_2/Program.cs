using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        var defaultFilesOptions = new DefaultFilesOptions();
        defaultFilesOptions.DefaultFileNames.Add("Neumann.html");

        app.UseDefaultFiles(defaultFilesOptions);

        app.UseStaticFiles();

        app.UseStaticFiles("/static");
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Picture")),
            RequestPath = "/Picture"
        });

        app.Run();
    }
}