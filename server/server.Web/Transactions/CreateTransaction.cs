using server.UseCases.Transactions.Commands;

namespace server.Web.Transactions;

public class CreateTransaction(ISender sender) : Endpoint<CreateTransactionRequest>
{
    public override void Configure()
    {
        Post(CreateTransactionRequest.Route);
        Claims("AdminId", "UserId");
        Roles("Admin", "User");
        Permissions("CreateTransactionPermission");
        Summary(s =>
        {
            s.ExampleRequest = new CreateTransactionRequest();
        });
    }

    public override async Task HandleAsync(CreateTransactionRequest req, CancellationToken ct)
    {
        var result = await sender.Send(new CreateTransactionCommand(req.TransactionDto), ct);

        if (result.IsSuccess)
        {
            await SendOkAsync(ct);
        }
        
        else
        {
            await SendErrorsAsync(400, ct);
        }
    }
}