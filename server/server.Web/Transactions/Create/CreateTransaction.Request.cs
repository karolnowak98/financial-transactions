using server.Operations.Transactions.Dtos;

namespace server.Web.Transactions.Create;

public class CreateTransactionRequest
{
    public const string Route = "/transactions/create";
    
    [FromBody]
    public CreateTransactionDto CreateTransactionDto { get; set; }
}