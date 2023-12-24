using server.Operations.Users.Dtos;

namespace server.Web.Users;

public class RegisterUserRequest
{
    public const string Route = "/Register";
    
    public RegisterDto RegisterDto { get; set; }
}