using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FoodStock.IAM.Application.Internal.OutboundServices;
using FoodStock.IAM.Domain.Model.Aggregates;
using Microsoft.IdentityModel.Tokens;

namespace FoodStock.IAM.Infrastructure.Tokens;

public class TokenService : ITokenService
{
    private const string Secret = "super-secret-key-change-in-config";
    private const string Issuer = "FoodStock.IAM";
    private const string Audience = "FoodStock.Client";

    public string Generate(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("name", user.Name ?? string.Empty),
            new Claim("plan", user.Plan ?? string.Empty)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}