using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using server.Core.TransactionAggregate;

namespace server.Operations.Transactions.Dtos;

public class CreateTransactionDto
{
    [Required]
    public decimal Amount { get; init; }
    [Required]
    public DateTime DateTime { get; init; }
    public string? Description { get; init; } = string.Empty;
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TransactionCategoryType? CategoryType { get; set; }
}