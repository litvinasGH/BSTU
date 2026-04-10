using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using ApiGateway;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services
    .AddOcelot(builder.Configuration)
   .AddCustomLoadBalancer<WeightedLoadBalancer>();


var app = builder.Build();

await app.UseOcelot();

app.Run();


/*

"LoadBalancerOptions": {
    "Type": "RoundRobin"
}

"LoadBalancerOptions": {
    "Type": "WeightedLoadBalancer"
}

"LoadBalancerOptions": {
    "Type": "CookieStickySessions",
    "Key": "session"
}

*/

