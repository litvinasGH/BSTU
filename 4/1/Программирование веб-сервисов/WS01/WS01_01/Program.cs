var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".WS01_01.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// app.UseHttpsRedirection();

var instanceName = Environment.GetEnvironmentVariable("INSTANCE_NAME") ?? "ws01-01-unknown";
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Backend-Instance"] = instanceName;
    await next();
});

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseRouting();

app.UseSession();

app.MapControllers();

app.Run();