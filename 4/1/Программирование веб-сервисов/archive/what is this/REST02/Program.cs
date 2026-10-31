using ResultsCollection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTransient<ResultsCollection.Results>(provider =>
{
    string filePath = Path.Combine(
        AppContext.BaseDirectory,
        "results.json");

    return new ResultsCollection.Results(filePath);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();