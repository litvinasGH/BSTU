using Microsoft.Extensions.Options;
using DAL_Celebrity;
using DAL_Celebrity_MSSQL;
using static DAL_Celebrity_MSSQL.DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Diagnostics;

namespace ASPA006_1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("Celebrities.config.json", optional: false, reloadOnChange: true);
            builder.Services.Configure<CelebritiesConfig>(builder.Configuration.GetSection("Celebrities"));

            builder.Services.AddScoped<IRepository, Repository>((p) =>
            {
                CelebritiesConfig config = p.GetRequiredService<IOptions<CelebritiesConfig>>().Value;
                return new Repository(config.ConnectionString);
            });

            var app = builder.Build();

            var celebritiesConfig = app.Services.GetRequiredService<IOptions<CelebritiesConfig>>().Value;
            celebritiesConfig.PhotoPath = celebritiesConfig.PhotoPath ?? celebritiesConfig.PhotosFolder;

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseExceptionHandler("/Celebrities/Error");
            app.UseExceptionHandler("/Lifeevents/Error");

            app.Map("/Celebrities/Error", (HttpContext ctx) =>
            {
                Exception? ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
                IResult rc = Results.Problem(detail: "Panic", instance: app.Environment.EnvironmentName, title: "ASPA004", statusCode: 500);
                return rc;
            });
            app.Map("/Lifeevents/Error", (HttpContext ctx) =>
            {
                Exception? ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
                IResult rc = Results.Problem(detail: "Panic", instance: app.Environment.EnvironmentName, title: "ASPA004", statusCode: 500);
                return rc;
            });

            // ---------- ЗНАМЕНИТОСТИ (Celebrities) -----------------------------------
            var celebrities = app.MapGroup("/api/Celebrities");
            celebrities.MapGet("/", (IRepository repo) => repo.GetAllCelebrities());
            celebrities.MapGet("/{id:int:min(1)}", (IRepository repo, int id) => {
                var celebrity = repo.GetCelebrityById(id);
                if (celebrity == null)
                    return Results.NotFound();
                return Results.Ok(celebrity);
            });
            celebrities.MapGet("/Lifeevents/{id:int:min(1)}", (IRepository repo, int id) => {

                var celebrity = repo.GetLifeeventById(id);
                if (celebrity == null)
                    return Results.NotFound();
                return Results.Ok(celebrity);
            });
            celebrities.MapDelete("/{id:int:min(1)}", (IRepository repo, int id) =>
            {
                var celebrity = repo.GetCelebrityById(id);
                if (celebrity == null)
                    return Results.NotFound(new
                    {
                        type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        title = "Not Found",
                        status = 404,
                        detail = $"404002:Celebrity Id = {id}",
                        instance = "ANC25"
                    });

                if (repo.DelCelebrity(id))
                    return Results.Ok(celebrity);
                return Results.Problem();
            });
            celebrities.MapPost("/", (IRepository repo, Celebrity celebrity) =>
            {
                if (celebrity == null)
                    return Results.Problem();

                repo.AddCelebrity(celebrity);
                return Results.Ok(celebrity);
            });
            celebrities.MapPut("/{id:int:min(1)}", (IRepository repo, int id, Celebrity celebrity) =>
            {
                if (celebrity == null)
                    return Results.Problem();

                if (celebrity.id != id)
                {
                    celebrity.id = id;
                }

                if (repo.UpdCelebrity(celebrity))
                    return Results.Ok(celebrity);
                else
                    return Results.NotFound();
            });

            celebrities.MapGet("/photo/{fname}", async (IOptions<CelebritiesConfig> iconfig, HttpContext context, string fname) =>
            {
                var config = iconfig.Value;

                Console.WriteLine($"Запрос на получение фотографии: {fname}");

                string imagePath;
                if (!string.IsNullOrEmpty(config.PhotoPath))
                {
                    imagePath = Path.Combine(config.PhotoPath, fname);
                }
                else
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Photos", fname);
                    Console.WriteLine($"PhotoPath не задан в конфигурации, используем путь по умолчанию: {imagePath}");
                }

                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"Файл не найден: {imagePath}");
                    return Results.NotFound();
                }

                try
                {
                    var bytes = await File.ReadAllBytesAsync(imagePath);
                    string contentType = GetContentTypeByExtension(Path.GetExtension(imagePath));
                    return Results.File(bytes, contentType);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                    return Results.Problem($"Ошибка при чтении файла: {ex.Message}");
                }
            });

            string GetContentTypeByExtension(string extension)
            {
                return extension.ToLower() switch
                {
                    ".jpg" => "image/jpeg",
                    ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".gif" => "image/gif",
                    ".bmp" => "image/bmp",
                    _ => "application/octet-stream",
                };
            }

            // ---------- СОБЫТИЯ (Lifeevents) ----------------------------------------
            var lifeevents = app.MapGroup("/api/Lifeevents");
            lifeevents.MapGet("/", (IRepository repo) => repo.GetAllLifeevents());
            lifeevents.MapGet("/{id:int:min(1)}", (IRepository repo, int id) =>
            {
                var lifeevent = repo.GetLifeeventById(id);
                if (lifeevent == null)
                    return Results.NotFound($"Событие с ID {id} не найдено");

                return Results.Ok(lifeevent);
            });
            lifeevents.MapGet("/Celebrities/{id:int:min(1)}", (IRepository repo, int id) =>
            {
                var lifeevents = repo.GetLifeeventsByCelebrityId(id);
                if (lifeevents == null || !lifeevents.Any())
                    return Results.NotFound($"События для знаменитости с ID {id} не найдены");

                return Results.Ok(lifeevents);
            });
            lifeevents.MapDelete("/{id:int:min(1)}", (IRepository repo, int id) =>
            {
                var lifeevent = repo.GetLifeeventById(id);
                if (lifeevent == null)
                    return Results.NotFound(new
                    {
                        type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        title = "Not Found",
                        status = 404,
                        detail = $"404002:Lifeevent Id = {id}",
                        instance = "ANC25"
                    });

                if (repo.DelLifeevent(id))
                    return Results.Ok(lifeevent);
                return Results.Problem();
            });
            lifeevents.MapPost("/", (IRepository repo, Lifeevent lifeevent) =>
            {
                if (lifeevent == null)
                    return Results.Problem();

                repo.AddLifeevent(lifeevent);
                return Results.Ok(lifeevent);
            });
            lifeevents.MapPut("/{id:int:min(1)}", (IRepository repo, Lifeevent lifeevent) =>
            {
                if (lifeevent == null)
                    return Results.NotFound(lifeevent);

                repo.UpdLifeevent(lifeevent);
                return Results.Ok("Событие успешно добавлено");
            });
            app.Run();
        }
    }
}
