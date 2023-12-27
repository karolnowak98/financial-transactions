namespace server.Web.Transactions.Delete;

public class DeleteTransactionValidator : Validator<DeleteTransactionRequest>
{
    public DeleteTransactionValidator()
    {
        RuleFor(x => x.TransactionId)
            .NotEmpty()
            .WithMessage(ErrorMessages.RequiredTransactionId);
    }
}