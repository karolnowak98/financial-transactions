namespace server.Core.TransactionAggregate.Specifications;

public sealed class CategoryByTypeSpec : Specification<TransactionCategory>
{
    public CategoryByTypeSpec(TransactionCategoryType type)
    {
        Query.Where(category => category.Type == type);
    }
}