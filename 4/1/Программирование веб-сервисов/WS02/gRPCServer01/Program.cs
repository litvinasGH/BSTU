using gRPCServer01.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<CalculatorService>();

app.MapGet("/", () => "gRPC Calculator Server is running");

app.Run();