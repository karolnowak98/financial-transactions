using server.Core.TransactionAggregate;

namespace server.Operations.Transactions.Dtos;

public class TransactionDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public decimal Amount { get; init; }
    public DateTime DateTime { get; init; }
    public string? Description { get; init; } = string.Empty;
    public TransactionCategoryType? CategoryType { get; set; }
}