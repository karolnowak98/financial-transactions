namespace server.Core.TransactionAggregate;

public class TransactionCategory : IAggregateRoot
{
    public Guid Id { get; init; }
    public TransactionCategoryType Type { get; init; }
}