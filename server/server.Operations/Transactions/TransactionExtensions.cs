using server.Core.TransactionAggregate;
using server.Operations.Transactions.Dtos;

namespace server.Operations.Transactions;

public static class TransactionExtensions
{
    public static TransactionDto MapToTransactionDto(this Transaction transaction, IMapper mapper)
    {
        var dto = mapper.Map<TransactionDto>(transaction);

        if (transaction.Category != null)
        {
            dto.CategoryType = transaction.Category.Type;
        }

        return dto;
    }
}