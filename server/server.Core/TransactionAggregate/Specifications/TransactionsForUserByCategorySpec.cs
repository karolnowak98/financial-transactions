namespace server.Core.TransactionAggregate.Specifications;

public sealed class TransactionsForUserByCategorySpec : Specification<Transaction>
{
    public TransactionsForUserByCategorySpec(Guid userId, TransactionCategoryType type)
    {
        Query.Where(t => t.UserId == userId
                         && t.Category != null
                         && t.Category.Type == type)
            .Include(t => t.Category);
    }
}