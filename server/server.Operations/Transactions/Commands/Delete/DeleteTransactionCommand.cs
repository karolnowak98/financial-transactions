namespace server.Operations.Transactions.Commands.Delete;

public record DeleteTransactionCommand(Guid TransactionId) : ICommand<Result>;