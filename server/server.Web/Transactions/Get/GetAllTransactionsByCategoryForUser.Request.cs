using server.Core.TransactionAggregate;

namespace server.Web.Transactions.Get;

public class GetAllTransactionsByCategoryForUserRequest
{
    public const string Route = "/transactions/{Type}";

    [FromRoute]
    public TransactionCategoryType Type { get; set; }
}