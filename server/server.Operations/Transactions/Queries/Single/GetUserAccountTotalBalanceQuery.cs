namespace server.Operations.Transactions.Queries.Single;

public record GetUserAccountTotalBalanceQuery(Guid UserId) : IQuery<Result<decimal>>;