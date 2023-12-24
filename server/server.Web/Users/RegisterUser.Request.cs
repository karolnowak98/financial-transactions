using server.Operations.Users.Dtos;

namespace server.Web.Users;

internal class RegisterUserRequest
{
    public const string Route = "/register";
    
    [FromBody]
    public RegisterDto RegisterDto { get; set; }
}