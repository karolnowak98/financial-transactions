using Microsoft.AspNetCore.Authorization;
using server.Infrastructure.Data.Config;
using server.Operations.Transactions.Queries.Single;

namespace server.Web.Transactions.Get;

[Authorize(Roles = StaticAppUserRoles.User)]
public class GetTotalBalance(ISender sender) : Endpoint<GetTotalBalanceRequest, GetTotalBalanceResponse>
{
    public override void Configure()
    {
        Get(GetTotalBalanceRequest.Route);
    }

    public override async Task HandleAsync(GetTotalBalanceRequest req, CancellationToken ct)
    {
        var currentUserId = HttpContext.GetCurrentUserId();
        
        if ((currentUserId == null) || !HttpContext.IsUser())
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        
        var query = new GetTotalBalanceQuery(currentUserId.Value);
        var result = await sender.Send(query, ct);

        if (result.IsSuccess)
        {
            Response = new GetTotalBalanceResponse { TotalBalance = result.Value };
        }
    }
}