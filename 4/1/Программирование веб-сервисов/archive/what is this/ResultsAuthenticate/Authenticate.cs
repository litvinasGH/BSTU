using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ResultsAuthenticate;

public class Authenticate
{
    private const string SecretKey =
        "REST01_SECRET_KEY_12345678901234567890";

    public string? SignIn(string login, string password)
    {
        string? role = GetRole(login, password);

        if (role == null)
        {
            return null;
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, login),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(SecretKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    private static string? GetRole(
        string login,
        string password)
    {
        if (login == "reader" && password == "1234")
        {
            return "READER";
        }

        if (login == "writer" && password == "1234")
        {
            return "WRITER";
        }

        return null;
    }
}