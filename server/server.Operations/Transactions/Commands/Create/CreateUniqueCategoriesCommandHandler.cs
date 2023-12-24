using server.Core.TransactionAggregate;
using server.Core.TransactionAggregate.Specifications;

namespace server.Operations.Transactions.Commands.Create;

public class CreateUniqueCategoriesCommandHandler(IRepository<TransactionCategory> repo) 
    : ICommandHandler<CreateUniqueCategoriesCommand, Result>
{
    public async Task<Result> Handle(CreateUniqueCategoriesCommand req, CancellationToken ct)
    {
        var categories = new List<TransactionCategory>();
        
        foreach (TransactionCategoryType type in Enum.GetValues(typeof(TransactionCategoryType)))
        {
            var spec = new CategoryByTypeSpec(type);
            var existingCategory = await repo.FirstOrDefaultAsync(spec, ct);

            if (existingCategory == null)
            {
                categories.Add(new TransactionCategory(Guid.NewGuid(), type));
            }
        }

        await repo.AddRangeAsync(categories, ct);
        return Result.Success();
    }
}