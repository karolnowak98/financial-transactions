using server.Core.TransactionAggregate;
using server.Core.TransactionAggregate.Specifications;
using server.UseCases.Transactions.Dtos;

namespace server.UseCases.Transactions.Queries;

public class GetAllTransactionsForUserQueryHandler(IReadRepository<Transaction> repository, IMapper mapper)
    : IRequestHandler<GetAllForUserQuery, Result<IEnumerable<TransactionDto>>>
{
    public async Task<Result<IEnumerable<TransactionDto>>> Handle(GetAllForUserQuery request, CancellationToken ct)
    {
        var spec = new TransactionsByUserIdWithCategoryTypeSpec(request.UserId);
        var entities = await repository.ListAsync(spec, ct);

        List<TransactionDto> dtos = [];

        foreach (var transactionDto in entities.Select(mapper.Map<TransactionDto>))
        {
            //TODO Set up correct category type
            transactionDto.CategoryType = TransactionCategoryType.Food;
                
            dtos.Add(transactionDto);
        }

        return dtos;
    }
}