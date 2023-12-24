using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Core.TransactionAggregate;

namespace server.Infrastructure.Data.Config;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(t => t.Amount)
            .HasColumnType("decimal(10, 2)")
            .HasPrecision(10, 2)
            .HasMaxLength(DataSchemaConstants.DefaultMaxTransactionAmount)
            .HasDefaultValue(0);

        builder.Property(t => t.Description)
            .IsRequired(false)
            .HasMaxLength(DataSchemaConstants.DefaultDescriptionMaxLength)
            .HasDefaultValue(string.Empty);

        builder.Property(t => t.CategoryId)
            .IsRequired(false)
            .HasDefaultValue(Guid.Empty);
    }
}