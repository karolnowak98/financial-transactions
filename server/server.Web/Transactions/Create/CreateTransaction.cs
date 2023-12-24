using Microsoft.AspNetCore.Authorization;
using server.Infrastructure.Data.Config;
using server.Operations.Transactions.Commands.Create;
using server.Web.Transactions.Get;

namespace server.Web.Transactions.Create;

[Authorize(Roles = StaticAppUserRoles.User)]
public class CreateTransaction(ISender sender) : Endpoint<CreateTransactionRequest>
{
    public override void Configure()
    {
        Post(CreateTransactionRequest.Route);
    }

    public override async Task HandleAsync(CreateTransactionRequest req, CancellationToken ct)
    {
        var currentUserId = HttpContext.GetCurrentUserId();
        
        if ((currentUserId == null) || !HttpContext.IsUser())
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        
        var result = await sender.Send(new CreateTransactionCommand(currentUserId.Value, req.CreateTransactionDto), ct);

        if (result.IsSuccess)
        {
            await SendCreatedAtAsync<GetTransactionById>(new { result.Value.Id }, "", cancellation: ct);
            return;
        }

        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(ct);
        }
        else
        {
            await SendErrorsAsync(400, ct);
        }
    }
}