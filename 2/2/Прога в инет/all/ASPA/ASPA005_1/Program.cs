using DAL004;
using Microsoft.AspNetCore.Diagnostics;
internal partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        Repository.JSONFileName = "Celebrities.json";
        using (Repository repository = Repository.Create("Celebrities"))
        {
            app.UseExceptionHandler("/Celebrities/Error");
            app.MapGet("/Celebrities", () => repository.GetAllCelebrities());
            app.MapGet("/Celebrities/{id:int}", (int id) =>
            {
                Celebrity? celebrity = repository.GetCelebrityById(id);
                if (celebrity == null) throw new FoundByIdException($"/Celebrities, Celebrity Id = {id}");
                return celebrity;
            });

            app.MapPost("/Celebrities", (Celebrity celebrity) =>
            {
                int? id = repository.addCelebrity(celebrity);
                if (id == null)
                    throw new AddCelebrityException("/Celebrities error, id null");
                if (repository.saveChanges() <= 0) throw new SaveException("/Celebrities error, SaveChanges() <= 0");
                return new Celebrity((int)id, celebrity.Firstname, celebrity.Surname, celebrity.PhotoPath);
            })
            .AddEndpointFilter(async (context, next) =>
            {
                Celebrity? cel = context.GetArgument<Celebrity>(0);
                if (cel == null)
                    throw new CelebrityArgumentException("Celebrity is null", 500);
                if (String.IsNullOrWhiteSpace(cel.Surname) || cel.Surname.Length < 2)
                    throw new CelebrityArgumentException("Surname is null or small(<2)", 409);
                return await next(context);
            }).AddEndpointFilter(async (context, next) =>
            {
                Celebrity? cel = context.GetArgument<Celebrity>(0);
                if (cel == null)
                    throw new CelebrityArgumentException("Celebrity is null", 500);
                if (repository.celebrities.Any(buf => buf.Surname == cel.Surname))
                    throw new CelebrityArgumentException("This surename already exists", 409);
                return await next(context);
            }).AddEndpointFilter(async (context, next) =>
            {
                Celebrity? cel = context.GetArgument<Celebrity>(0);
                if (cel == null)
                    throw new CelebrityArgumentException("Celebrity is null", 500);
                if (!File.Exists(Path.Combine(repository.FullFilePath, cel.PhotoPath)))
                    context.HttpContext.Response.Headers["X-Celebrity"] = $"NotFound = {cel.PhotoPath}";
                return await next(context);
            });

            app.MapDelete("/Celebrities/{id:int}", (int id) =>
            {
                if (!repository.delCelebrity(id)) throw new DelByIdException($"DELETE /Celebrities error, Id = {id}");
                else if (repository.saveChanges() >= 0) throw new SaveException("/Celebrities error, SaveChanges() <= 0");
                return $"Celebrity with Id = {id} deleted";
            });

            app.MapPut("/Celebrities/{id:int}", (int id, Celebrity celebrity) =>
            {
                int? id_upd = repository.updCelebrityById(id, celebrity);
                if (id_upd == null) throw new UpdByIdExeption($"UPDATE /Celebrities error, Id = {id}");
                repository.saveChanges();
                return repository.GetCelebrityById((int)id_upd);
            });

            app.MapFallback((HttpContext ctx) => Results.NotFound(new { error = $"path {ctx.Request.Path} not supported" }));


            app.Map("/Celebrities/Error", (HttpContext ctx) =>
            {
                Exception? ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
                IResult rc = Results.Problem(detail: "Panic", instance: app.Environment.EnvironmentName, title: "ASPA004", statusCode: 500);
                if (ex != null)
                {
                    if (ex is FoundByIdException) rc = Results.NotFound(ex.Message);
                    else if (ex is BadHttpRequestException) rc = Results.BadRequest(ex.Message);
                    else if (ex is SaveException) rc = Results.Problem(title: "ASPA001/SaveChanges", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
                    else if (ex is AddCelebrityException) rc = Results.Problem(title: "ASPA004/addCelebrity", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
                    else if (ex is FileNotFoundException) rc = Results.Problem(title: "ASPA00", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
                    else if (ex is DelByIdException) rc = Results.NotFound(ex.Message);
                    else if (ex is UpdByIdExeption) rc = Results.NotFound(ex.Message);

                }
                return rc;
            });

            app.Run();
        }
    }
}
public class FoundByIdException : Exception { public FoundByIdException(string message) : base($"Found by Id: {message}") { } }
public class SaveException : Exception { public SaveException(string message) : base($"SaveChanges error:{message}") { } }
public class AddCelebrityException : Exception { public AddCelebrityException(string message) : base($"AddCelebrityException error:{message}") { } };
public class DelByIdException : Exception { public DelByIdException(string message) : base($"Delete by Id: {message}") { } }
public class UpdByIdExeption : Exception { public UpdByIdExeption(string message) : base($"Update by Id: {message}") { } }
public class CelebrityArgumentException : AddCelebrityException
{
    public string titel;
    public int code;
    public CelebrityArgumentException(string message, int errT, string titel = "ASPA005/addCelebrity/ArgumentException") : base($"CelebrityArgumentException error:{message}")
    {
        code = errT;
        this.titel = titel;
    }
}
