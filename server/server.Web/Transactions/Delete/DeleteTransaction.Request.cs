namespace server.Web.Transactions.Delete;

public class DeleteTransactionRequest
{
    public const string Route = "/transactions/delete/{TransactionId:Guid}";

    [FromRoute]
    public Guid TransactionId { get; set; }
}