using server.Core.TransactionAggregate;

namespace server.Web.Transactions.Create;

public class CreateTransactionValidator : Validator<CreateTransactionRequest>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.CreateTransactionDto.Amount)
            .NotEmpty()
            .WithMessage(ErrorMessages.RequiredAmount)
            .GreaterThan(DataSchemaConstants.DefaultMinTransactionAmount)
            .LessThan(DataSchemaConstants.DefaultMaxTransactionAmount);

        RuleFor(x => x.CreateTransactionDto.DateTime)
            .NotEmpty()
            .WithMessage(ErrorMessages.RequiredDateTime);

        RuleFor(x => x.CreateTransactionDto.CategoryType)
            .Must(value => value == null || Enum.TryParse<TransactionCategoryType>(value.ToString(), out _))
            .WithMessage(ErrorMessages.InvalidCategoryType);

        RuleFor(x => x.CreateTransactionDto.Description)
            .MaximumLength(DataSchemaConstants.DefaultDescriptionMaxLength);
    }
}