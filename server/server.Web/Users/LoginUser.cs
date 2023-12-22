using server.UseCases.Users.Commands;

namespace server.Web.Users;

public class LoginUser(ISender sender) : Endpoint<LoginUserRequest, LoginUserResponse>
{
    public override void Configure()
    {
        Post(LoginUserRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
    {
        var command = new LoginUserCommand(req.LoginDto);
        var result = await sender.Send(command, ct);

        if(result.Status == ResultStatus.Unauthorized)
        {
            await SendErrorsAsync(401, ct);
            return;
        }
        
        if (result.IsSuccess)
        {
            await SendOkAsync(ct);
        }
    }
}