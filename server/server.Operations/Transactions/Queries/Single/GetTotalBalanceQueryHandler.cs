using server.Core.TransactionAggregate;
using server.Core.TransactionAggregate.Specifications;

namespace server.Operations.Transactions.Queries.Single;

public class GetTotalBalanceQueryHandler(IReadRepository<Transaction> repo)
    : IQueryHandler<GetTotalBalanceQuery, Result<decimal>>
{
    
    public async Task<Result<decimal>> Handle(GetTotalBalanceQuery req, CancellationToken ct)
    {
        var spec = new TransactionsForUserSpec(req.UserId);

        var transactions = await repo.ListAsync(spec, ct);

        var totalBalance = transactions.Sum(t => t.Amount);

        return totalBalance;
    }
}