namespace server.Core.TransactionAggregate;

public class Transaction : IAggregateRoot
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public decimal Amount { get; init; }
    public DateTime DateTime { get; init; }
    public string? Description { get; init; } = string.Empty;
    public Guid? CategoryId { get; init; }
}