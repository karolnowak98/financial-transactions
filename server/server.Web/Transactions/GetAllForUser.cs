using server.UseCases.Transactions.Queries;

namespace server.Web.Transactions;

public class GetAllForUser(ISender mediator) : Endpoint<GetAllForUserRequest, GetAllForUserResponse>
{
    public override void Configure()
    {
        Get(GetAllForUserRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllForUserRequest req, CancellationToken ct)
    {
        var query = new GetAllForUserQuery(req.UserId);
        var result = await mediator.Send(query, ct);

        if (result.IsSuccess)
        {
            Response = new GetAllForUserResponse(result.Value.ToList());
        }
    }
}