using server.Operations.Users.Dtos;

namespace server.Web.Users;

public class LoginUserRequest
{
    public const string Route = "/login";
    
    [FromBody]
    public LoginDto LoginDto { set; get; }
}