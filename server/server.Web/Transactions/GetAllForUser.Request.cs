namespace server.Web.Transactions;

public class GetAllForUserRequest
{
    public const string Route = "/Transactions";
    
    public Guid UserId { get; set; }
}