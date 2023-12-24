namespace server.Web.Transactions.Get;

public class GetTransactionByIdRequest
{
    public const string Route = "/Transactions/{Id:Guid}";
    public static string BuildRoute(Guid id) => Route.Replace("{Id:Guid}", id.ToString());

    public Guid Id { get; set; }
}