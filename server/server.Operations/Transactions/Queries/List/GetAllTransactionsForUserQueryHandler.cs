using server.Core.TransactionAggregate;
using server.Core.TransactionAggregate.Specifications;
using server.Operations.Transactions.Dtos;

namespace server.Operations.Transactions.Queries.List;

public class GetAllTransactionsForUserQueryHandler(IReadRepository<Transaction> repository, IMapper mapper)
    : IQueryHandler<GetAllTransactionsForUserQuery, Result<IEnumerable<TransactionDto>>>
{
    public async Task<Result<IEnumerable<TransactionDto>>> Handle(GetAllTransactionsForUserQuery request, CancellationToken ct)
    {
        var spec = new TransactionsForUserSpec(request.UserId);
        var entities = await repository.ListAsync(spec, ct);
        var dtos = entities.Select(t => t.MapToTransactionDto(mapper)).ToList();
        
        return dtos;
    }
    
}