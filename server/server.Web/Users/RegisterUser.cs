using server.Operations.Users.Commands;

namespace server.Web.Users;

public class RegisterUser(ISender sender) : Endpoint<RegisterUserRequest>
{
    public override void Configure()
    {
        Post(RegisterUserRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterUserRequest req, CancellationToken ct)
    {
        var command = new RegisterUserCommand(req.RegisterDto);
        var result = await sender.Send(command, ct);

        if(result.Status == ResultStatus.Conflict)
        {
            await SendAsync(result.Errors,409, ct);
            return;
        }
        
        if (result.IsSuccess)
        {
            await SendOkAsync(ct);
        }
    }
}