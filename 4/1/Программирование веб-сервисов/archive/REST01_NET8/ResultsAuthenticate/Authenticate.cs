using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ResultsAuthenticate;

public sealed class AppUser : IdentityUser
{
}

public sealed class AuthDbContext : IdentityDbContext<AppUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }
}

public sealed record SignInRequest(
    string? Login,
    string? Password);

public sealed class Authenticate
{
    private readonly UserManager<AppUser> _users;
    private readonly IConfiguration _configuration;

    public Authenticate(
        UserManager<AppUser> users,
        IConfiguration configuration)
    {
        _users = users;
        _configuration = configuration;
    }

    public async Task<string?> SignInAsync(
        string login,
        string password)
    {
        var user = await _users.FindByNameAsync(login);

        if (user is null)
        {
            return null;
        }

        var passwordIsValid =
            await _users.CheckPasswordAsync(user, password);

        if (!passwordIsValid)
        {
            return null;
        }

        var roles = await _users.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(ClaimTypes.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(
            roles.Select(role =>
                new Claim(ClaimTypes.Role, role)));

        var jwtKey = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException(
                "JWT key is missing.");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}

public static class AuthSeeder
{
    public static async Task SeedAsync(
        IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var db = scope.ServiceProvider
            .GetRequiredService<AuthDbContext>();

        await db.Database.EnsureCreatedAsync();

        var roles = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        var users = scope.ServiceProvider
            .GetRequiredService<UserManager<AppUser>>();

        var roleNames = new[]
        {
            "READER",
            "WRITER"
        };

        foreach (var roleName in roleNames)
        {
            if (await roles.RoleExistsAsync(roleName))
            {
                continue;
            }

            var result = await roles.CreateAsync(
                new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(
                        "; ",
                        result.Errors.Select(error =>
                            error.Description)));
            }
        }

        var accounts = new[]
        {
            new
            {
                Name = "reader",
                Role = "READER",
                Password = "Reader123!"
            },
            new
            {
                Name = "writer",
                Role = "WRITER",
                Password = "Writer123!"
            }
        };

        foreach (var account in accounts)
        {
            var user = await users.FindByNameAsync(account.Name);

            if (user is null)
            {
                user = new AppUser
                {
                    UserName = account.Name
                };

                var created = await users.CreateAsync(
                    user,
                    account.Password);

                if (!created.Succeeded)
                {
                    throw new InvalidOperationException(
                        string.Join(
                            "; ",
                            created.Errors.Select(error =>
                                error.Description)));
                }
            }

            if (await users.IsInRoleAsync(
                    user,
                    account.Role))
            {
                continue;
            }

            var assigned = await users.AddToRoleAsync(
                user,
                account.Role);

            if (!assigned.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(
                        "; ",
                        assigned.Errors.Select(error =>
                            error.Description)));
            }
        }
    }
}
