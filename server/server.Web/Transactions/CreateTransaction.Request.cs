using server.UseCases.Transactions.Dtos;

namespace server.Web.Transactions;

public class CreateTransactionRequest
{
    public const string Route = "/Transactions/Create";
    
    public TransactionDto TransactionDto { get; set; }
}