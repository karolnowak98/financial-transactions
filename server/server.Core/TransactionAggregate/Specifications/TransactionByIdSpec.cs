namespace server.Core.TransactionAggregate.Specifications;

public sealed class TransactionByIdSpec : Specification<Transaction>
{
    public TransactionByIdSpec(Guid transactionId)
    {
        Query.Where(t => t.Id == transactionId)
            .Include(t => t.Category);
    }
}