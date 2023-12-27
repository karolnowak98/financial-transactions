namespace server.Web.Transactions.Get;

public class GetTransactionByIdValidator : Validator<GetTransactionByIdRequest>
{
    public GetTransactionByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ErrorMessages.RequiredTransactionId);
    }
}