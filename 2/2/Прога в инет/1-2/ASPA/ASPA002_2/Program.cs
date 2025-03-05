using Microsoft.AspNetCore.Builder;

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

        app.Run();
    }
}