using server.Core.TransactionAggregate;
using server.Core.TransactionAggregate.Specifications;

namespace server.Operations.Transactions.Queries.Single;

public class GetUserAccountBalanceQueryHandler(IReadRepository<Transaction> repo)
    : IQueryHandler<GetUserAccountTotalBalanceQuery, Result<decimal>>
{
    
    public async Task<Result<decimal>> Handle(GetUserAccountTotalBalanceQuery req, CancellationToken ct)
    {
        var spec = new TransactionsForUserSpec(req.UserId);

        var transactions = await repo.ListAsync(spec, ct);

        var totalBalance = transactions.Sum(t => t.Amount);

        return totalBalance;
    }
}