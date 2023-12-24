namespace server.Operations.Transactions.Queries.Single;

public record GetTotalBalanceQuery(Guid UserId) : IQuery<Result<decimal>>;