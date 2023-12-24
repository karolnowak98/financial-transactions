using server.Core.TransactionAggregate;
using server.Core.UsersAggregate;
using server.Operations.Transactions.Dtos;
using server.Operations.Users.Dtos;

namespace server.Operations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateTransactionDto, Transaction>();
        CreateMap<TransactionDto, Transaction>();
        CreateMap<Transaction, TransactionDto>();
        
        CreateMap<RegisterDto, AppUser>();
    }
}