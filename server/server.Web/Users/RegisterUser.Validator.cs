namespace server.Web.Users;

public class RegisterUserValidator : Validator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.RegisterDto.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MinimumLength(DataSchemaConstants.DefaultMinNameLength)
            .WithMessage($"First name must contain at least {DataSchemaConstants.DefaultMinNameLength} characters.");
        
        RuleFor(x => x.RegisterDto.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MinimumLength(DataSchemaConstants.DefaultMinNameLength)
            .WithMessage($"Last name must contain at least {DataSchemaConstants.DefaultMinNameLength} characters.");
        
        RuleFor(x => x.RegisterDto.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.RegisterDto.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(DataSchemaConstants.DefaultPasswordMinLength)
            .WithMessage($"Password must contain at least {DataSchemaConstants.DefaultPasswordMinLength} characters.")
            .MaximumLength(DataSchemaConstants.DefaultPasswordMaxLength)
            .Must(ContainsDigit).WithMessage("Password must contain a digit.")
            .Must(ContainsLowercase).WithMessage("Password must contain a lowercase letter.")
            .Must(ContainsUppercase).WithMessage("Password must contain an uppercase letter.")
            .Must(ContainsNonAlphanumeric).WithMessage("Password must contain a non-alphanumeric character.");
    }
    
    private static bool ContainsDigit(string password) => password.Any(char.IsDigit);
    private static bool ContainsLowercase(string password) => password.Any(char.IsLower);
    private static bool ContainsUppercase(string password) => password.Any(char.IsUpper);
    private static bool ContainsNonAlphanumeric(string password) => password.Any(ch => !char.IsLetterOrDigit(ch));
}