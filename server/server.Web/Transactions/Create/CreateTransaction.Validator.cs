using server.Core.TransactionAggregate;

namespace server.Web.Transactions.Create;

public class CreateTransactionValidator : Validator<CreateTransactionRequest>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.CreateTransactionDto.Amount)
            .NotEmpty()
            .WithMessage("Amount is required.")
            .GreaterThan(DataSchemaConstants.DefaultMinTransactionAmount)
            .LessThan(DataSchemaConstants.DefaultMaxTransactionAmount);

        RuleFor(x => x.CreateTransactionDto.DateTime)
            .NotEmpty()
            .WithMessage("Date is required.");

        RuleFor(x => x.CreateTransactionDto.CategoryType)
            .Empty()
            .When(x => x.CreateTransactionDto.CategoryType == null)
            .NotEmpty()
            .Must(value => Enum.TryParse<TransactionCategoryType>(value?.ToString(), out _))
            .WithMessage("Invalid CategoryType.");

        RuleFor(x => x.CreateTransactionDto.Description)
            .MaximumLength(DataSchemaConstants.DefaultDescriptionMaxLength);
    }
}