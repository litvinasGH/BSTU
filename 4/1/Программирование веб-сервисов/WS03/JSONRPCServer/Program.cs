
using JSONRPCServer.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CalculatorService>();
builder.Services.AddSingleton<JsonRpcDispatcher>();

var app = builder.Build();

app.MapPost("/rpc", async (HttpContext context, JsonRpcDispatcher dispatcher) =>
{
    try
    {
        using var document = await JsonDocument.ParseAsync(context.Request.Body);

        var result = await dispatcher.ProcessAsync(document.RootElement);

        return Results.Json(result);
    }
    catch (JsonException)
    {
        return Results.Json(new
        {
            jsonrpc = "2.0",
            error = new
            {
                code = -32700,
                message = "Parse error"
            },
            id = (object?)null
        });
    }
});

app.Run();
