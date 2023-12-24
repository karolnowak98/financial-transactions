using server.Operations.Transactions.Dtos;

namespace server.Operations.Transactions.Queries.Single;

public record GetTransactionByIdQuery(Guid TransactionId) : IQuery<Result<TransactionDto>>;