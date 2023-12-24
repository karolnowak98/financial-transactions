using server.Core.TransactionAggregate;

namespace server.Web.Transactions.Get;

public class GetBalanceByCategoryRequest
{
    public const string Route = "/Balance/{Type}";
    public static string BuildRoute(TransactionCategoryType type) => Route.Replace("{Type}", type.ToString());

    public TransactionCategoryType Type { get; set; }
}