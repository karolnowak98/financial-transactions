using server.Core.TransactionAggregate;
using server.Operations.Transactions.Dtos;
using server.Operations.Transactions.Queries.Single;

namespace server.Operations.Transactions.Commands.Create;

public class CreateTransactionCommandHandler(IRepository<Transaction> repo, IMapper mapper, ISender sender) 
    : ICommandHandler<CreateTransactionCommand, Result<TransactionDto>>
{
    public async Task<Result<TransactionDto>> Handle(CreateTransactionCommand req, CancellationToken ct)
    {
        var transaction = mapper.Map<Transaction>(req.CreateTransactionDto);

        transaction.Id = Guid.NewGuid();
        transaction.UserId = req.UserId;

        var categoryType = req.CreateTransactionDto.CategoryType;

        if (categoryType != null)
        {
            var query = new GetCategoryIdByTypeQuery(categoryType.Value);
            var result = await sender.Send(query, ct);

            if (result.IsSuccess)
            {
                transaction.CategoryId = result.Value;
            }
        }
        
        await repo.AddAsync(transaction, ct);

        return Result.Success(transaction.MapToTransactionDto(mapper));
    }
}