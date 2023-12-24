namespace server.Web.Transactions.Delete;

public class DeleteTransactionRequest
{
    public const string Route = "/Transactions/Delete/{TransactionId:Guid}";
    public static string BuildRoute(Guid transactionId) => Route.Replace("{TransactionId:Guid}", transactionId.ToString());

    public Guid TransactionId { get; set; }
}