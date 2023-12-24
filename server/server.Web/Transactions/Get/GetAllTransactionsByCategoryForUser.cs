using server.Operations.Transactions.Queries.List;

namespace server.Web.Transactions.Get;

[Authorize(Roles = StaticAppUserRoles.User)]
public class GetAllTransactionsByCategoryForUser(ISender sender)
    : Endpoint<GetAllTransactionsByCategoryForUserRequest, GetAllTransactionsByCategoryForUserResponse>
{
    public override void Configure()
    {
        Get(GetAllTransactionsByCategoryForUserRequest.Route);
    }

    public override async Task HandleAsync(GetAllTransactionsByCategoryForUserRequest req, CancellationToken ct)
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
            Response = new GetAllTransactionsByCategoryForUserResponse { Transactions = result.Value.ToList()};
        }
    }
}