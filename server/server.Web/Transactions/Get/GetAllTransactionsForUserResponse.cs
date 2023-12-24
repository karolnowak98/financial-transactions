using server.Operations.Transactions.Dtos;

namespace server.Web.Transactions.Get;

public class GetAllForUserResponse
{
    public List<TransactionDto> Transactions { get; set; } = [];
}