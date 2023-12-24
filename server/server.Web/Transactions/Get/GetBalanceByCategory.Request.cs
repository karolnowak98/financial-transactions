using server.Core.TransactionAggregate;

namespace server.Web.Transactions.Get;

public class GetBalanceByCategoryRequest
{
    public const string Route = "/Balance/{Type}";

    [FromRoute]
    public TransactionCategoryType Type { get; set; }
}