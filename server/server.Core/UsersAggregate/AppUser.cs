namespace server.Core.UsersAggregate;

public class AppUser : IdentityUser<Guid>, IAggregateRoot
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
}