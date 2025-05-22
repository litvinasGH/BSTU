using ANC25_WEBAPl_DLL;
using static ANC25_WEBAPl_DLL.CelebritiesAPIExtensions;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddCelebritiesConfiguration();
        builder.AddCelebritiesServices();
        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseANCErrorHandler("ANC28");
        app.MapCelebrities();
        app.MapLifewents();
        app.MapPhotoCelebrities(null);


        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Celebrities}/{action=Index}/{id?}");
        app.MapControllerRoute(
            name: "celebrity",
            pattern: "/{id:int:min(1)}",
            defaults: new { Controller = "Celebrities", Action = "Human" });
        app.MapControllerRoute(
            name: "celebrity",
            pattern: "/0",
            defaults: new { Controller = "Celebrities", Action = "NewHumanForm" });

        app.Run();
    }
}