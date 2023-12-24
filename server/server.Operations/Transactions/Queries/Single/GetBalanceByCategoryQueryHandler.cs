using server.Core.TransactionAggregate;
using server.Core.TransactionAggregate.Specifications;

namespace server.Operations.Transactions.Queries.Single;

public class GetBalanceByCategoryQueryHandler(IReadRepository<Transaction> repo) : IQueryHandler<GetBalanceByCategoryQuery, Result<decimal>>
{
    public async Task<Result<decimal>> Handle(GetBalanceByCategoryQuery req, CancellationToken ct)
    {
        var spec = new TransactionsForUserByCategorySpec(req.UserId, req.Type);

        var transactions = await repo.ListAsync(spec, ct);

        var balance = transactions.Sum(t => t.Amount);

        return balance;
    }
}