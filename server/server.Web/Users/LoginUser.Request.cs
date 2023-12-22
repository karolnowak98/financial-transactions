using server.UseCases.Users.Dtos;

namespace server.Web.Users;

public class LoginUserRequest
{
    public const string Route = "/Login";
    
    public LoginDto LoginDto { set; get; }
}