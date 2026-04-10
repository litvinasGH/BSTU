using Microsoft.AspNetCore.Http; 
using Ocelot.LoadBalancer.Interfaces;
using Ocelot.Responses;
using Ocelot.Values;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateway;

public class WeightedLoadBalancer : ILoadBalancer
{
    private static readonly object Locker = new();
    private int _index;

    private static readonly int[] Pattern = { 0, 0, 0, 0, 0, 1, 1, 1, 2, 2 };

    private static readonly List<ServiceHostAndPort> Hosts = new()
    {
        new ServiceHostAndPort("localhost", 3001), // X
        new ServiceHostAndPort("localhost", 3002), // Y
        new ServiceHostAndPort("localhost", 3003)  // Z
    };

    public WeightedLoadBalancer() { }

    public string Type => nameof(WeightedLoadBalancer);

    public Task<Response<ServiceHostAndPort>> LeaseAsync(HttpContext context)
    {
        lock (Locker)
        {
            var pick = Pattern[_index % Pattern.Length];
            _index++;

            if (pick >= Hosts.Count)
                pick = Hosts.Count - 1;

            return Task.FromResult<Response<ServiceHostAndPort>>(
                new OkResponse<ServiceHostAndPort>(Hosts[pick])
            );
        }
    }

    public void Release(ServiceHostAndPort hostAndPort) { }
}