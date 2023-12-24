using FluentValidation;

namespace server.Web.Users;

public class LoginUserValidator : Validator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(x =>x.LoginDto.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");
        
        RuleFor(x => x.LoginDto.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}