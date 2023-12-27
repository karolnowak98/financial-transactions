using server.Operations.Transactions.Dtos;
using server.Operations.Transactions.Queries.List;

namespace server.Web.Transactions.Get;

[Authorize(Roles = StaticAppUserRoles.User)]
public class GetAllTransactionsByCategory(ISender sender)
    : Endpoint<GetAllTransactionsByCategoryRequest, IEnumerable<TransactionDto>>
{
    public override void Configure()
    {
        Get(GetAllTransactionsByCategoryRequest.Route);
    }

    public override async Task HandleAsync(GetAllTransactionsByCategoryRequest req, CancellationToken ct)
    {
        var currentUserId = HttpContext.GetCurrentUserId();
        
        if (currentUserId == null || !HttpContext.IsUser())
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        
        var query = new GetAllTransactionsByCategoryForUserQuery(currentUserId.Value, req.Type);
        var result = await sender.Send(query, ct);

        if (result.IsSuccess)
        {
            Response = result.Value;
        }
    }
}