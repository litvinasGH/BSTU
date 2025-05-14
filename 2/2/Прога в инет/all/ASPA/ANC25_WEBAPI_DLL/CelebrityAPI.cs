using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using static DAL_Celebrity_MSSQL.DAL_Celebrity_MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANC25_WEBAPl_DLL
{
    public static class CelebritiesAPIExtensions
    {
        public static IServiceCollection AddCelebritiesConfiguration(this WebApplicationBuilder builder, string celebrityJson = "Celebrities.config.json")
        {
            builder.Configuration.AddJsonFile(celebrityJson);
            return builder.Services.Configure<CelebritiesConfig>(builder.Configuration.GetSection("Celebrities"));
        }
        public static IServiceCollection AddCelebritiesServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRepository, Repository>(provider =>
            {
                CelebritiesConfig config = provider.GetRequiredService<IOptions<CelebritiesConfig>>().Value;
                return new Repository(config.ConnectionString);
            });
            return builder.Services;
        }
    }
}
