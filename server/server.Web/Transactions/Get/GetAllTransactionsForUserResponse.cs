using server.Operations.Transactions.Dtos;

namespace server.Web.Transactions.Get;

public class GetAllForUserResponse
{
    public List<TransactionDto> Transactions { get; set; }
    
    public GetAllForUserResponse()
    {
        Transactions = new List<TransactionDto>();
    }
}