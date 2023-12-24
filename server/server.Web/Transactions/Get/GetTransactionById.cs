using server.Operations.Transactions.Queries.Single;

namespace server.Web.Transactions.Get;

[Authorize(Roles = StaticAppUserRoles.User)]
public class GetTransactionById(ISender sender)
    : Endpoint<GetTransactionByIdRequest, GetTransactionByIdResponse>
{
    public override void Configure()
    {
        Get(GetTransactionByIdRequest.Route);
    }

    public override async Task HandleAsync(GetTransactionByIdRequest req, CancellationToken ct)
    {
        var currentUserId = HttpContext.GetCurrentUserId();
        
        if ((currentUserId == null) || !HttpContext.IsUser())
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        
        var query = new GetTransactionByIdQuery(req.Id);
        var result = await sender.Send(query, ct);

        if (result.Value.UserId != currentUserId.Value)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        if (result.IsSuccess)
        {
            Response = new GetTransactionByIdResponse { Transaction = result.Value};
        }
    }
}