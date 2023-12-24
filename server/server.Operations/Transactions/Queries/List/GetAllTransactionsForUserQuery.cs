using server.Operations.Transactions.Dtos;

namespace server.Operations.Transactions.Queries.List;

public record GetAllTransactionsForUserQuery(Guid UserId) : IQuery<Result<IEnumerable<TransactionDto>>>;