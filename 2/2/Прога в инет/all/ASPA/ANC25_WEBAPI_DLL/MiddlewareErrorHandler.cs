using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using static DAL_Celebrity_MSSQL.DAL_Celebrity_MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using DAL_Celebrity;

namespace ANC25_WEBAPl_DLL
{
    public static class MiddlewareErrorHandler
    {
        public static RouteHandlerBuilder MapCelebrities(this IEndpointRouteBuilder routeBuilder, string prefix = "/api/Celebrities")
        {
            var celebrities = routeBuilder.MapGroup(prefix);

            // Get all celebrities
            celebrities.MapGet("/", (IRepository repo) => repo.GetAllCelebrities());

            // Get celebrity by ID
            celebrities.MapGet("/{id:int:min(1)}", (IRepository repo, int id) =>
            {
                var celebrity = repo.GetCelebrityById(id);
                if (celebrity == null)
                    throw new ANC25Exception(status: 404, code: "MSMBB1", detail: $"Celebrity Id = {id}");
                return Results.Ok(celebrity);
            });

            // Get celebrity events by ID
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

            // Get photo by filename
            return celebrities.MapGet("/photo/{fname}", async (IOptions<CelebritiesConfig> iconfig, HttpContext context, string fname) => {
                var config = iconfig.Value;

                Console.WriteLine($"Запрос на получение фотографии: {fname}");

                string imagePath;
                if (!string.IsNullOrEmpty(config.PhotosRequestPath))
                {
                    imagePath = Path.Combine(config.PhotosRequestPath, fname);
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
            });
        }

        public static RouteHandlerBuilder MapPhotoCelebrities(this IEndpointRouteBuilder routeBuilder, string? prefix = "/api/Lifewents")
        {
            if (string.IsNullOrEmpty(prefix)) prefix = routeBuilder.ServiceProvider.GetRequiredService<IOptions<CelebritiesConfig>>().Value.PhotosRequestPath;
            return routeBuilder.MapGet($"{prefix}/{{fname}}", async (IOptions<CelebritiesConfig> iconfig, HttpContext context, string fname) =>
            {
                CelebritiesConfig config = iconfig.Value;
                string filepath = Path.Combine(config.PhotosFolder, fname);
                FileStream file = File.OpenRead(filepath);
                BinaryReader sr = new BinaryReader(file);
                BinaryReader sw = new BinaryReader(context.Response.BodyWriter.AsStream());
                int n = 0; byte[] buffer = new byte[2048];
                context.Response.ContentType = "image/jpeg";
                context.Response.StatusCode = StatusCodes.Status200OK;
                while ((n = await sr.BaseStream.ReadAsync(buffer, 0, 2048)) > 0) await sw.BaseStream.WriteAsync(buffer, 0, n);
                sr.Close(); sw.Close();
            });
        }


        public static RouteHandlerBuilder MapLifewents(this IEndpointRouteBuilder routeBuilder, string prefix = "/api/Lifewents")
        {
            var lifeevents = routeBuilder.MapGroup(prefix);

            // Get all lifewents
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

            // Update lifewent by ID
            return lifeevents.MapPut("/{id:int:min(1)}", (IRepository repo, int id, Lifeevent lifeevent) => {
                if (lifeevent == null)
                    return Results.NotFound(lifeevent);

                repo.UpdLifeevent(lifeevent);
                return Results.Ok("Событие успешно добавлено");
            });
        }

        public class ANC25Exception : Exception
        {

            public HttpStatusCode StatusCode { get; }


            public string ErrorCode { get; }

            public ANC25Exception(int status, string code, string detail)
                : base(detail)
            {
                StatusCode = (HttpStatusCode)status;
                ErrorCode = code;
            }


            public ANC25Exception(int status, string code, string format, params object[] args)
                : this(status, code, string.Format(format, args))
            {
            }

            public ANC25Exception(string errorCode, string message)
                : this((int)HttpStatusCode.InternalServerError, errorCode, message)
            {
            }
        }
    }
}
