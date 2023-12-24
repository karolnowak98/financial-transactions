using server.Operations.Transactions.Dtos;

namespace server.Web.Transactions.Get;

public class GetTransactionByIdResponse
{
    public TransactionDto Transaction { get; set; }
}