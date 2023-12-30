namespace server.Core.UsersAggregate.Specifications;

public sealed class UserByEmailSpec : Specification<AppUser>
{
    public UserByEmailSpec(string email)
    {
        Query.Where(u => u.Email == email);
    }
}