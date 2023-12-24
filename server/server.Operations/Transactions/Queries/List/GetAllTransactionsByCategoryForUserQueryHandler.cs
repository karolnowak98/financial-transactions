using server.Core.TransactionAggregate;
using server.Core.TransactionAggregate.Specifications;
using server.Operations.Transactions.Dtos;

namespace server.Operations.Transactions.Queries.List;

public class GetAllTransactionsByCategoryForUserQueryHandler(IReadRepository<Transaction> repo, IMapper mapper)
    : IQueryHandler<GetAllTransactionsByCategoryForUserQuery, Result<IEnumerable<TransactionDto>>>
{
    public async Task<Result<IEnumerable<TransactionDto>>> Handle(GetAllTransactionsByCategoryForUserQuery req, CancellationToken ct)
    {
        var spec = new TransactionsForUserByCategorySpec(req.UserId, req.Type);
        var entities = await repo.ListAsync(spec, ct);
        var dtos = entities.Select(t => t.MapToTransactionDto(mapper)).ToList();

        return dtos;
    }
}