using server.Core.TransactionAggregate;

namespace server.Web.Transactions.Get;

public class GetAllTransactionsByCategoryForUserRequest
{
    public const string Route = "/Transactions/{Type}";
    public static string BuildRoute(TransactionCategoryType type) => Route.Replace("{Type}", type.ToString());

    public TransactionCategoryType Type { get; set; }
}