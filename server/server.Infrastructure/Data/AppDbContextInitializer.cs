using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using server.Core.UsersAggregate;
using server.Infrastructure.Data.Config;

namespace server.Infrastructure.Data;

public class AppDbContextInitializer(
    ILogger<AppDbContextInitializer> logger, AppDbContext context,
    UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

     public async Task SeedAsync()
     {
         try
         {
             await TrySeedAsync();
         }
         catch (Exception ex)
         {
             logger.LogError(ex, "An error occurred while seeding the database.");
             throw;
         }
     }

     private async Task TrySeedAsync()
     {
         var adminRole = new AppRole { Name = StaticAppUserRoles.Admin};
         var userRole = new AppRole { Name = StaticAppUserRoles.User};
    
         if (roleManager.Roles.All(r => r.Name != adminRole.Name))
         {
             await roleManager.CreateAsync(adminRole);
         }

         if (roleManager.Roles.All(r => r.Name != userRole.Name))
         {
             await roleManager.CreateAsync(userRole);
         }
    
         var administrator = new AppUser { UserName = "admin@localhost", Email = "admin@localhost", FirstName = "Admin", LastName = "Admin"};
    
         if (userManager.Users.All(u => u.UserName != administrator.UserName))
         {
             await userManager.CreateAsync(administrator, "Administrator123!");
             if (!string.IsNullOrWhiteSpace(adminRole.Name))
             {
                 await userManager.AddToRolesAsync(administrator, new [] { adminRole.Name });
             }
         }
    
         //TODO seed some sample transactions
         // if (!context.TodoLists.Any())
         // {
         //     context.TodoLists.Add(new TodoList
         //     {
         //         Title = "Todo List",
         //         Items =
         //         {
         //             new TodoItem { Title = "Make a todo list 📃" },
         //             new TodoItem { Title = "Check off the first item ✅" },
         //             new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
         //             new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
         //         }
         //     });
         //
         //     await context.SaveChangesAsync();
         // }
     }
}