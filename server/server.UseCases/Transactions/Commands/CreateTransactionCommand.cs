using server.UseCases.Transactions.Dtos;

namespace server.UseCases.Transactions.Commands;

public record CreateTransactionCommand(TransactionDto TransactionDto) : ICommand<Result>;