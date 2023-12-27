namespace server.Web;

public static class ErrorMessages
{
    //Users
    public const string RequiredFirstName = "First name is required.";
    public const string RequiredLastName = "Last name is required.";
    public const string RequiredEmail = "Email is required.";
    public const string RequiredPassword = "Password is required.";
    public const string InvalidEmailFormat = "Invalid email format.";
    public const string PasswordMustContainDigit = "Password must contain a digit.";
    public const string PasswordMustContainLowercase = "Password must contain a lowercase letter.";
    public const string PasswordMustContainUppercase = "Password must contain an uppercase letter.";
    public const string PasswordMustContainNonAlphanumeric = "Password must contain a non-alphanumeric character.";
    
    public static readonly string PasswordMustContainAtLeast 
        = $"Password must contain at least {DataSchemaConstants.DefaultPasswordMinLength} characters.";
    public static readonly string PasswordMustContainLessThan 
        = $"Password must contain less than {DataSchemaConstants.DefaultPasswordMaxLength} characters.";

    public static readonly string FirstNameMustContainAtLeast
        = $"First name must contain at least {DataSchemaConstants.DefaultMinNameLength} characters.";

    public static readonly string FirstNameMustContainLessThan
        = $"First name must contain less than {DataSchemaConstants.DefaultMaxNameLength} characters.";

    public static readonly string LastNameMustContainAtLeast
        = $"Last name must contain at least {DataSchemaConstants.DefaultMinNameLength} characters.";

    public static readonly string LastNameMustContainLessThan
        = $"Last name must contain less than {DataSchemaConstants.DefaultMaxNameLength} characters.";

    //Transactions
    public const string InvalidCategoryType = "Invalid CategoryType.";
    public const string RequiredTransactionId = "TransactionId is required.";
    public const string RequiredDateTime = "Date is required.";
    public const string RequiredAmount = "Amount is required.";
}