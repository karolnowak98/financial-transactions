using System.Security.Claims;

namespace server.Web.Transactions;

public static class TransactionsExtensions
{
    public static bool IsUser(this HttpContext context)
        => context.User.IsInRole(StaticAppUserRoles.User);
    
    public static bool IsAdmin(this HttpContext context)
        => context.User.IsInRole(StaticAppUserRoles.Admin);

    public static Guid? GetCurrentUserId(this HttpContext context)
    {
        var userIdString = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (Guid.TryParse(userIdString, out var userId))
        {
            return userId;
        }

        return null;
    }
}