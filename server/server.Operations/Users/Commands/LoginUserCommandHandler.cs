using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using server.Core.UsersAggregate;
using server.Infrastructure.Jwt;
using server.Infrastructure.UserAggregate.Specifications;

namespace server.Operations.Users.Commands;

public class LoginUserCommandHandler(UserManager<AppUser> userManager, IReadRepository<AppUser> repo, IJwtGenerator jwtGenerator)  
    : IRequestHandler<LoginUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginUserCommand req, CancellationToken ct)
    {
        var spec = new UserByEmailSpec(req.LoginDto.Email);

        var user = await repo.FirstOrDefaultAsync(spec, ct);

        if (user?.Email == null)
        {
            return Result.Unauthorized();
        }

        var isPasswordCorrect = await userManager.CheckPasswordAsync(user, req.LoginDto.Password);

        if (!isPasswordCorrect)
        {
            return Result.Unauthorized();
        }

        var userRoles = await userManager.GetRolesAsync(user);
        
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new("JwtId", Guid.NewGuid().ToString())
        };
            
        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = jwtGenerator.GenerateToken(authClaims);

        if (token == null)
        {
            return Result.Unauthorized();
        }

        return Result.Success(token);
    }
}