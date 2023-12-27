using server.Core.TransactionAggregate;

namespace server.Web.Transactions.Get;

public class GetBalanceByCategoryValidator : Validator<GetBalanceByCategoryRequest>
{
    public GetBalanceByCategoryValidator()
    {
        RuleFor(x => x.Type)
            .Must(value => Enum.TryParse<TransactionCategoryType>(value.ToString(), out _))
            .WithMessage(ErrorMessages.InvalidCategoryType);
    }
}