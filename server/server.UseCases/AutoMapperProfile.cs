using server.Core.TransactionAggregate;
using server.UseCases.Transactions.Dtos;

namespace server.UseCases;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TransactionDto, Transaction>();
        CreateMap<Transaction, TransactionDto>();
    }
}