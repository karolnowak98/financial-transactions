using server.Core.TransactionAggregate;

namespace server.Operations.Transactions.Queries.Single;

public record GetCategoryIdByTypeQuery(TransactionCategoryType Type) : IQuery<Result<Guid>>;