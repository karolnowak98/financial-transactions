using server.Operations.Transactions.Queries.Single;

namespace server.Web.Transactions.Get;

[Authorize(Roles = StaticAppUserRoles.User)]
public class GetBalanceByCategory(ISender sender)
    : Endpoint<GetBalanceByCategoryRequest, GetBalanceByCategoryResponse>
{
    public override void Configure()
    {
        Get(GetBalanceByCategoryRequest.Route);
    }

    public override async Task HandleAsync(GetBalanceByCategoryRequest req, CancellationToken ct)
    {
        var currentUserId = HttpContext.GetCurrentUserId();
        
        if ((currentUserId == null) || !HttpContext.IsUser())
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        
        var query = new GetBalanceByCategoryQuery(currentUserId.Value, req.Type);
        var result = await sender.Send(query, ct);

        if (result.IsSuccess)
        {
            Response = new GetBalanceByCategoryResponse { Balance = result.Value };
        }
    }
}