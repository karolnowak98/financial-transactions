using server.Core.TransactionAggregate;
using server.Core.TransactionAggregate.Specifications;
using server.Operations.Transactions.Dtos;

namespace server.Operations.Transactions.Queries.Single;

public class GetTransactionByIdQueryHandler(IReadRepository<Transaction> repo, IMapper mapper) 
    : IQueryHandler<GetTransactionByIdQuery, Result<TransactionDto>>
{
    public async Task<Result<TransactionDto>> Handle(GetTransactionByIdQuery req, CancellationToken ct)
    {
        var spec = new TransactionByIdSpec(req.TransactionId);
        var transaction = await repo.FirstOrDefaultAsync(spec, ct);

        if (transaction == null)
        {
            return Result.NotFound($"Transaction with id: {req.TransactionId} not found.");
        }

        return transaction.MapToTransactionDto(mapper);
    }
}