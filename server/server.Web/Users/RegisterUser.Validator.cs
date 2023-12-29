namespace server.Web.Users;

public class RegisterUserValidator : Validator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.RegisterDto.FirstName)
            .NotEmpty()
            .WithMessage(ErrorMessages.RequiredFirstName)
            .MinimumLength(DataSchemaConstants.DefaultMinNameLength)
            .WithMessage(ErrorMessages.FirstNameMustContainAtLeast)
            .MaximumLength(DataSchemaConstants.DefaultMaxNameLength)
            .WithMessage(ErrorMessages.FirstNameMustContainLessThan);
        
        RuleFor(x => x.RegisterDto.LastName)
            .NotEmpty()
            .WithMessage(ErrorMessages.RequiredLastName)
            .MinimumLength(DataSchemaConstants.DefaultMinNameLength)
            .WithMessage(ErrorMessages.LastNameMustContainAtLeast)
            .MaximumLength(DataSchemaConstants.DefaultMaxNameLength)
            .WithMessage(ErrorMessages.LastNameMustContainLessThan);
        
        RuleFor(x => x.RegisterDto.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.RequiredEmail)
            .EmailAddress()
            .WithMessage(ErrorMessages.InvalidEmailFormat);

        RuleFor(x => x.RegisterDto.Password)
            .NotEmpty()
            .WithMessage(ErrorMessages.RequiredPassword)
            .MinimumLength(DataSchemaConstants.DefaultPasswordMinLength)
            .WithMessage(ErrorMessages.PasswordMustContainAtLeast)
            .MaximumLength(DataSchemaConstants.DefaultPasswordMaxLength)
            .WithMessage(ErrorMessages.PasswordMustContainLessThan)
            .Must(ContainsDigit).WithMessage(ErrorMessages.PasswordMustContainDigit)
            .Must(ContainsLowercase).WithMessage(ErrorMessages.PasswordMustContainLowercase)
            .Must(ContainsUppercase).WithMessage(ErrorMessages.PasswordMustContainUppercase)
            .Must(ContainsNonAlphanumeric).WithMessage(ErrorMessages.PasswordMustContainNonAlphanumeric);
    }
    
    private static bool ContainsDigit(string password) => password.Any(char.IsDigit);
    private static bool ContainsLowercase(string password) => password.Any(char.IsLower);
    private static bool ContainsUppercase(string password) => password.Any(char.IsUpper);
    private static bool ContainsNonAlphanumeric(string password) => password.Any(ch => !char.IsLetterOrDigit(ch));
}