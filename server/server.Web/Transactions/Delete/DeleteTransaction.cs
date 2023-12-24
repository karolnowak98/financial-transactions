using server.Operations.Transactions.Commands.Delete;

namespace server.Web.Transactions.Delete;

[Authorize(Roles = StaticAppUserRoles.User)]
public class DeleteTransaction(ISender sender) : Endpoint<DeleteTransactionRequest>
{
    public override void Configure()
    {
        Delete(DeleteTransactionRequest.Route);
    }

    public override async Task HandleAsync(DeleteTransactionRequest req, CancellationToken ct)
    {
        if (!HttpContext.IsUser())
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        
        var command = new DeleteTransactionCommand(req.TransactionId);
        
        var result = await sender.Send(command, ct);

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(ct);
        }
    }
}