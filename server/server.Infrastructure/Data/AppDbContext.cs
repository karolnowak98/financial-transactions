using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using server.Core.TransactionAggregate;
using server.Core.UsersAggregate;

namespace server.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options), IAppDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Transaction>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TransactionCategory>()
            .HasIndex(c => c.Type)
            .IsUnique();
            
        builder.Entity<TransactionCategory>()
            .Property(e => e.Type)
            .HasConversion(new EnumToStringConverter<TransactionCategoryType>());
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public IEnumerable<Transaction> Transactions => Set<Transaction>();
    public IEnumerable<TransactionCategory> TransactionCategories => Set<TransactionCategory>();
}