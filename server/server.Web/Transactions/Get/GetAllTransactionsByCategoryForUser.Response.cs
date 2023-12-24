using server.Operations.Transactions.Dtos;

namespace server.Web.Transactions.Get;

public class GetAllTransactionsByCategoryForUserResponse
{
    public List<TransactionDto> Transactions { get; set; }
    
    public GetAllTransactionsByCategoryForUserResponse()
    {
        Transactions = new List<TransactionDto>();
    }
}