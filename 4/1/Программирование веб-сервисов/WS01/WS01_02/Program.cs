var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".WS01_02.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.MapControllers();

app.Run();