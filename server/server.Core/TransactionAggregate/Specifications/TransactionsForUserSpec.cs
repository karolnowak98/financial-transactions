namespace server.Core.TransactionAggregate.Specifications;

public sealed class TransactionsForUserSpec : Specification<Transaction>
{
    public TransactionsForUserSpec(Guid userId)
    {
        Query.Where(t => t.UserId == userId)
            .Include(t => t.Category);
    }
}