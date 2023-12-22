using FluentValidation;
using server.Core.TransactionAggregate;
using server.Infrastructure.Data.Config;

namespace server.Web.Transactions;

public class CreateTransactionValidator : Validator<CreateTransactionRequest>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.TransactionDto.Amount)
            .NotEmpty()
            .WithMessage("Amount is required.")
            .GreaterThan(DataSchemaConstants.DefaultMinAmount)
            .LessThan(DataSchemaConstants.DefaultDescriptionMaxLength);

        RuleFor(x => x.TransactionDto.DateTime)
            .NotEmpty()
            .WithMessage("Date is required.");

        RuleFor(x => x.TransactionDto.UserId)
            .NotEmpty()
            .WithMessage("User is required.");

        RuleFor(x => x.TransactionDto.CategoryType)
            .Empty()
            .When(x => x.TransactionDto.CategoryType == null)
            .NotEmpty()
            .Must(value => Enum.TryParse<TransactionCategoryType>(value?.ToString(), out _))
            .WithMessage("Invalid CategoryType.");

        RuleFor(x => x.TransactionDto.Description)
            .MaximumLength(DataSchemaConstants.DefaultDescriptionMaxLength);
    }
}