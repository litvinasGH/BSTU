using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.UseExceptionHandler("/Error");
        // A
        app.MapGet("/A/{x:max(100)}", (HttpContext context, [FromRoute] int? x) => Results.Ok(new { path = context.Request.Path.Value, x = x }));
        app.MapPost("/A/{x:int:range(0,100)}", (HttpContext context, [FromRoute] int? x) => Results.Ok(new { path = context.Request.Path.Value, x = x }));
        app.MapPut("/A/{x:min(1)}/{y:min(1)}", (HttpContext context, [FromRoute] int? x, [FromRoute] int? y) => Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));
        app.MapDelete("/A/{x:int:min(1)}-{y:int:min(1)}", (HttpContext context, [FromRoute] int? x, [FromRoute] int? y) => Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));
        //B
        app.MapGet("/B/{x:float}", (HttpContext context, [FromRoute] float x) => Results.Ok(new { path = context.Request.Path.Value, x = x }));
        app.MapPost("/B/{x:float}/{y:float}", (HttpContext context, [FromRoute] float x, [FromRoute] float y) => Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));
        app.MapDelete("/B/{x:float}-{y:float}", (HttpContext context, [FromRoute] float x, [FromRoute] float y) => Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));
        //C
        app.MapGet("/C/{x:bool}", (HttpContext context, [FromRoute] bool x) => Results.Ok(new { path = context.Request.Path.Value, x = x }));
        app.MapPost("/C/{x:bool},{y:bool}", (HttpContext context, [FromRoute] bool x, [FromRoute] bool y) => Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));
        //D
        app.MapGet("/D/{x:datetime}", (HttpContext context, [FromRoute] DateTime x) => Results.Ok(new { path = context.Request.Path.Value, x = x }));
        app.MapPost("/D/{x:datetime}|{y:datetime}", (HttpContext context, [FromRoute] DateTime x, [FromRoute] DateTime y) => Results.Ok(new { path = context.Request.Path.Value, x = x, y = y }));
        //E
        app.MapGet("/E/12-{x:minlength(1)}", (HttpContext context, [FromRoute] string x) => Results.Ok(new { path = context.Request.Path.Value, x = x }));
        app.MapPut("/E/{x:alpha:minlength(2):maxlength(12)}", (HttpContext context, [FromRoute] string x) => Results.Ok(new { path = context.Request.Path.Value, x = x }));
        //F
        app.MapGet("/F/{x:regex(^([a-zA-Z0-9._%+-]+)@[a-zA-Z0-9.-]+.by$)}", (HttpContext context, [FromRoute] string x) => Results.Ok(new { path = context.Request.Path.Value, x = x }));

        app.MapFallback((HttpContext ctx) =>
        {
            return Results.NotFound(new { message = $"path {ctx.Request.Path.Value} not supported" });
        })                                                                                                                                                                                                                                                       ;
        app.Map("/Error", (HttpContext ctx) =>
        {
            Exception? ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
            return Results.Ok(new { message = ex?.Message });
        });

        app.Run();
    }
}