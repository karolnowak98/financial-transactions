using Microsoft.EntityFrameworkCore.Design;

namespace server.Infrastructure.Data;

//Class used for terminal migrations
public class DesignTimeAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = configuration.GetConnectionString("SqlServerConnection");

        builder.UseSqlServer(connectionString);

        return new AppDbContext(builder.Options);
    }
}