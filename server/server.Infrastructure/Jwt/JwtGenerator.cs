using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace server.Infrastructure.Jwt;

public class JwtGenerator(IConfiguration configuration) : IJwtGenerator
{
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

    public string? GenerateToken(IEnumerable<Claim> claims, int hoursToExpire = 1)
    {
        var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? "defaultSecret"));
        var issuer = _configuration["JWT:ValidIssuer"] ?? "defaultIssuer";
        var audience = _configuration["JWT:ValidAudience"] ?? "defaultAudience";

        var tokenObj = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddHours(hoursToExpire),
            claims: claims,
            signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenObj);

        return token;
    }
}