namespace server.Core.TransactionAggregate.Specifications;

public sealed class CategoryIdByTypeSpec : Specification<TransactionCategory>
{
    public CategoryIdByTypeSpec(TransactionCategoryType type)
    {
        Query.Where(transaction => transaction.Type == type);
    }
}