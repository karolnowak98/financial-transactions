using server.Operations.Transactions.Queries.List;

namespace server.Web.Transactions.Get;

[Authorize(Roles = StaticAppUserRoles.User)]
public class GetAllTransactionsForUser(ISender sender) : Endpoint<GetAllForUserRequest, GetAllForUserResponse>
{
    public override void Configure()
    {
        Get(GetAllForUserRequest.Route);
    }

    public override async Task HandleAsync(GetAllForUserRequest req, CancellationToken ct)
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
            Response = new GetAllForUserResponse { Transactions = result.Value.ToList()};
        }
    }
}