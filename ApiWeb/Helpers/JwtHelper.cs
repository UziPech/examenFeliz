using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiWeb.Helpers;

public static class JwtHelper
{
    public static string GenerateToken(string username, string role, IConfiguration config)
    {
        var jwtSettings = config.GetSection("Jwt");
        var keyValue = jwtSettings["Key"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        if (string.IsNullOrWhiteSpace(keyValue))
            throw new ArgumentException("La clave JWT ('Key') no está configurada en appsettings.json");
        if (string.IsNullOrWhiteSpace(issuer))
            throw new ArgumentException("El issuer JWT ('Issuer') no está configurado en appsettings.json");
        if (string.IsNullOrWhiteSpace(audience))
            throw new ArgumentException("El audience JWT ('Audience') no está configurado en appsettings.json");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Obtener minutos de expiración, por defecto 60 si no existe o es inválido
        var expireMinutes = double.TryParse(jwtSettings["DurationInMinutes"], out var result) ? result : 60;

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
