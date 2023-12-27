using server.Operations.Transactions.Dtos;
using server.Operations.Transactions.Queries.List;

namespace server.Web.Transactions.Get;

[Authorize(Roles = StaticAppUserRoles.User)]
public class GetAllTransactions(ISender sender) : Endpoint<GetAllTransactionsRequest, IEnumerable<TransactionDto>>
{
    public override void Configure()
    {
        Get(GetAllTransactionsRequest.Route);
    }

    public override async Task HandleAsync(GetAllTransactionsRequest req, CancellationToken ct)
    {
        var currentUserId = HttpContext.GetCurrentUserId();
        
        if ((currentUserId == null) || !HttpContext.IsUser())
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        
        var query = new GetAllTransactionsForUserQuery(currentUserId.Value);
        var result = await sender.Send(query, ct);

        if (result.IsSuccess)
        {
            Response = result.Value;
        }
    }
}