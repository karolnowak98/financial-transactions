using System.ComponentModel.DataAnnotations;

namespace server.Operations.Users.Dtos;

public class LoginDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}