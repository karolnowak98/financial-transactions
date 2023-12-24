namespace server.Web.Transactions.Get;

public class GetTransactionByIdRequest
{
    public const string Route = "/transactions/{Id:Guid}";

    [FromRoute]
    public Guid Id { get; set; }
}