using server.UseCases.Transactions.Dtos;

namespace server.Web.Transactions;

public class GetAllForUserResponse(List<TransactionDto> transactions)
{
    public List<TransactionDto> Transactions { get; set; } = transactions;
}