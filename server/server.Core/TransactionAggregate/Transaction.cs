namespace server.Core.TransactionAggregate;

public class Transaction : IAggregateRoot
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string? Description { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; }
    public TransactionCategory? Category { get; set; }
}