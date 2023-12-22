namespace server.Web.Users;

public class LoginUserResponse(string jwtToken)
{
    public string JwtToken { get; set; } = jwtToken;
}