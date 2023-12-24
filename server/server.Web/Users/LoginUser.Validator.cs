namespace server.Web.Users;

public class LoginUserValidator : Validator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(u => u.LoginDto.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(u => u.LoginDto.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(DataSchemaConstants.DefaultPasswordMinLength)
            .WithMessage($"Password must contain at least {DataSchemaConstants.DefaultPasswordMinLength} characters.")
            .MaximumLength(DataSchemaConstants.DefaultPasswordMaxLength)
            .WithMessage($"Password must contain less than {DataSchemaConstants.DefaultPasswordMaxLength} characters.");
    }
}