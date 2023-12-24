using Microsoft.AspNetCore.Identity;
using server.Core.UsersAggregate;
using server.Infrastructure.Data.Config;
using server.Infrastructure.UserAggregate.Specifications;

namespace server.Operations.Users.Commands;

public class RegisterUserCommandHandler(IReadRepository<AppUser> repo, UserManager<AppUser> userManager, IMapper mapper) 
    : IRequestHandler<RegisterUserCommand, Result>
{
    public async Task<Result> Handle(RegisterUserCommand req, CancellationToken ct)
    {
        var spec = new UserByEmailSpec(req.RegisterDto.Email);
        var userExists = await repo.AnyAsync(spec, ct);

        if (userExists)
        {
            return Result.Conflict("User with that email already exists.");
        }

        var registerDto = req.RegisterDto;
        var user = mapper.Map<AppUser>(registerDto);
        user.SecurityStamp = Guid.NewGuid().ToString();
        user.UserName = registerDto.Email;

        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            var errorMessage = string.Join(Environment.NewLine, result.Errors.Select(error => "# " + error.Description));

            return Result.Conflict(errorMessage);
        }

        await userManager.AddToRoleAsync(user, StaticAppUserRoles.User);

        return Result.Success();
    }
}