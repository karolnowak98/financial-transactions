using server.Core.TransactionAggregate;

namespace server.Operations.Transactions.Queries.Single;

public record GetBalanceByCategoryQuery(Guid UserId, TransactionCategoryType Type) : IQuery<Result<decimal>>;