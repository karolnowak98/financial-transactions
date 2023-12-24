using server.Core.TransactionAggregate;

namespace server.Operations.Transactions.Commands.Delete;

public class DeleteTransactionCommandHandler(IRepository<Transaction> repo) : ICommandHandler<DeleteTransactionCommand, Result>
{
    public async Task<Result> Handle(DeleteTransactionCommand req, CancellationToken ct)
    {
        var transaction = await repo.GetByIdAsync(req.TransactionId, ct);

        if (transaction == null)
        {
            return Result.NotFound();
        }

        await repo.DeleteAsync(transaction, ct);
        
        return Result.Success();
    }
}