using ANC25_WEBAPl_DLL;
using static ANC25_WEBAPl_DLL.CelebritiesAPIExtensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ������������ Celebrities
        builder.AddCelebritiesConfiguration();
        // ������� Celebrities
        builder.AddCelebritiesServices();

        builder.Services.AddRazorPages();
        builder.Services.AddRazorPages(options =>
        {
            // ��� �����������
            options.Conventions.AddPageRoute("/Celebrities", "/");
            // ���������� ����� ������������
            options.Conventions.AddPageRoute("/NewCelebrity", "/0");
            // ����������� ������������ � id
            options.Conventions.AddPageRoute("/Celebrity", "/Celebrities/{id:int:min(1)}");
            // ����������� ������������ � id
            options.Conventions.AddPageRoute("/Celebrity", "/{id:int:min(1)}");
        });

        var app = builder.Build();
        app.UseStaticFiles();

        // ��������� ���������� Celebrities
        
        app.UseANCErrorHandler("ANC27X");

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();

        // API Celebrities
        app.MapCelebrities();
        // API Lifeevents
        app.MapLifewents();
        // API ��� ����������
        app.MapPhotoCelebrities(null);

        app.Run();
    }
}
