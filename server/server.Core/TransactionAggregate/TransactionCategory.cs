namespace server.Core.TransactionAggregate;

public class TransactionCategory(Guid id, TransactionCategoryType type) : IAggregateRoot
{
    public Guid Id { get; init; } = id;
    public TransactionCategoryType Type { get; init; } = type;
    
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}