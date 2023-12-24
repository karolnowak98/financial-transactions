using server.Core.TransactionAggregate;
using server.Core.TransactionAggregate.Specifications;

namespace server.Operations.Transactions.Queries.Single;

public class GetCategoryIdByTypeQueryHandler(IReadRepository<TransactionCategory> repo)
    : IQueryHandler<GetCategoryIdByTypeQuery, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(GetCategoryIdByTypeQuery req, CancellationToken ct)
    {
        var spec = new CategoryIdByTypeSpec(req.Type);

        var category = await repo.FirstOrDefaultAsync(spec, ct);

        if (category == null)
        {
            return Result.NotFound($"Category with type: {req.Type} not found.");
        }

        return Result.Success(category.Id);
    }
}