using Microsoft.AspNetCore.Authorization;
using server.Infrastructure.Data.Config;
using server.Operations.Transactions.Commands.Create;

namespace server.Web.Transactions.Create;

[Authorize(Roles = StaticAppUserRoles.Admin)]
public class CreateTransactionCategories(ISender sender) : Endpoint<CreateTransactionCategoriesRequest>
{
    public override void Configure()
    {
        Post(CreateTransactionCategoriesRequest.Route);
    }

    public override async Task HandleAsync(CreateTransactionCategoriesRequest req, CancellationToken ct)
    {
        if (!HttpContext.IsAdmin())
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var command = new CreateUniqueCategoriesCommand();
        await sender.Send(command, ct);
    }
}