using server.Core.TransactionAggregate;
using server.Operations.Transactions.Dtos;

namespace server.Operations.Transactions.Queries.List;

public record GetAllTransactionsByCategoryForUserQuery(Guid UserId, TransactionCategoryType Type) 
    : IQuery<Result<IEnumerable<TransactionDto>>>;
