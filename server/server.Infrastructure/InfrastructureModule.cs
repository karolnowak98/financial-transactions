using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using server.Core.UsersAggregate;
using server.Infrastructure.Data;
using server.Infrastructure.Jwt;
using StackExchange.Redis;

namespace server.Infrastructure;

public static class InfrastructureModule
{
    private const string SqlServerConnection = "SqlServerConnection";
    
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlConnection  = configuration.GetConnectionString("SqlServerConnection");

        Guard.Against.Null(sqlConnection, message: $"Connection string '{SqlServerConnection}' not found.");
        
        services.AddDbContext<AppDbContext>((_, options) =>
        {
            options.UseSqlServer(sqlConnection);
        });

        services
            .AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = DataSchemaConstants.DefaultPasswordMinLength;
        });
        
        services.AddScoped<AppDbContextInitializer>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("RedisDB"));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    }

    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();

        await initializer.InitialiseAsync();
        await initializer.SeedAsync();
    }
}