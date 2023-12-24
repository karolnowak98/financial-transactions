using server.Operations.Transactions.Dtos;

namespace server.Operations.Transactions.Commands.Create;

public record CreateTransactionCommand(Guid UserId, CreateTransactionDto CreateTransactionDto) : ICommand<Result<TransactionDto>>;