namespace server.Web.Users;

public class LoginUserValidator : Validator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        RuleFor(u => u.LoginDto.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.RequiredEmail)
            .EmailAddress()
            .WithMessage(ErrorMessages.InvalidEmailFormat);

        RuleFor(u => u.LoginDto.Password)
            .NotEmpty()
            .WithMessage(ErrorMessages.RequiredPassword)
            .MinimumLength(DataSchemaConstants.DefaultPasswordMinLength)
            .WithMessage(ErrorMessages.PasswordMustContainAtLeast)
            .MaximumLength(DataSchemaConstants.DefaultPasswordMaxLength)
            .WithMessage(ErrorMessages.PasswordMustContainLessThan);
    }
}