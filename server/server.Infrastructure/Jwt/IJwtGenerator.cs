namespace server.Infrastructure.Jwt;

public interface IJwtGenerator
{ 
    string? GenerateToken(IEnumerable<Claim> claims, int hoursToExpire = 1);
}