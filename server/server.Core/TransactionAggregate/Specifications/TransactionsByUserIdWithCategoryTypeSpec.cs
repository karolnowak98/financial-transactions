namespace server.Core.TransactionAggregate.Specifications;

public sealed class TransactionsByUserIdWithCategoryTypeSpec : Specification<Transaction>
{
    public TransactionsByUserIdWithCategoryTypeSpec(Guid userId)
    {
        Query.Where(transaction => transaction.UserId == userId);
    }
}