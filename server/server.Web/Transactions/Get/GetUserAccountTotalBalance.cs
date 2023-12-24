using Microsoft.AspNetCore.Authorization;
using server.Infrastructure.Data.Config;
using server.Operations.Transactions.Queries.Single;

namespace server.Web.Transactions.Get;

[Authorize(Roles = StaticAppUserRoles.User)]
public class GetUserAccountTotalBalance(ISender sender) 
    : Endpoint<GetUserAccountBalanceRequest, GetUserAccountTotalBalanceResponse>
{
    public override void Configure()
    {
        Get(GetUserAccountBalanceRequest.Route);
    }

    public override async Task HandleAsync(GetUserAccountBalanceRequest req, CancellationToken ct)
    {
        var currentUserId = HttpContext.GetCurrentUserId();
        
        if ((currentUserId == null) || !HttpContext.IsUser())
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        
        var query = new GetUserAccountTotalBalanceQuery(currentUserId.Value);
        var result = await sender.Send(query, ct);

        if (result.IsSuccess)
        {
            Response = new GetUserAccountTotalBalanceResponse { TotalBalance = result.Value };
        }
    }
}