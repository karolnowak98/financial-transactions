using System.ComponentModel.DataAnnotations;
using server.Core.TransactionAggregate;

namespace server.Operations.Transactions.Dtos;

public class CreateTransactionDto
{
    [Required]
    public decimal Amount { get; init; }
    [Required]
    public DateTime DateTime { get; init; }
    public string? Description { get; init; } = string.Empty;
    
    public TransactionCategoryType? CategoryType { get; set; }
}