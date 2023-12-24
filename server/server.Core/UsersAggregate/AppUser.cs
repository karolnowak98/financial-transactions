using System.ComponentModel.DataAnnotations;

namespace server.Core.UsersAggregate;

public class AppUser : IdentityUser<Guid>, IAggregateRoot
{
    [Required]
    public string FirstName { get; init; }
    [Required]
    public string LastName { get; init; }
}