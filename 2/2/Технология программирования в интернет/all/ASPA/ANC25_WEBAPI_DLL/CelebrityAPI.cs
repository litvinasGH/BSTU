using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text.Json;
using static DAL_Celebrity_MSSQL.DAL_Celebrity_MSSQL;

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
            builder.Services.AddSingleton<CelebrityTitles>();
            builder.Services.AddSingleton<CountryCodes>((p) => new CountryCodes
            (p.GetRequiredService<IOptions<CelebritiesConfig>>().Value.ISO3166alpha2Path));
            return builder.Services;
        }
        public class CountryCodes : List<CountryCodes.ISOCountryCodes>
        {
            public record ISOCountryCodes(string code, string countryLabel);
            public CountryCodes(string jsonCountryCodesPath) : base()
            {
                if (File.Exists(jsonCountryCodesPath))
                {
                    FileStream fs = new FileStream(jsonCountryCodesPath, FileMode.OpenOrCreate, FileAccess.Read);
                    List<ISOCountryCodes>? cc = JsonSerializer.DeserializeAsync<List<ISOCountryCodes>>(fs).Result;
                    if (cc != null) this.AddRange(cc);
                }
            }

        }
        public class CelebrityTitles
        {
            public string? Title = "Celebrities";
            public string? Head = "Celebrities Dictionary Internet Service";
            public string? Copyright = "BSTU";
        }
    }
}
