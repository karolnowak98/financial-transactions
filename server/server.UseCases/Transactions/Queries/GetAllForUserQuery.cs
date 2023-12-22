using server.UseCases.Transactions.Dtos;

namespace server.UseCases.Transactions.Queries;

public record GetAllForUserQuery(Guid UserId) : IRequest<Result<IEnumerable<TransactionDto>>>;