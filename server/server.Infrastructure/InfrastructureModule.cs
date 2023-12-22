using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using server.Core.UsersAggregate;
using server.Infrastructure.Data;
using server.Infrastructure.Data.Config;
using server.Infrastructure.Jwt;

namespace server.Infrastructure;

public static class InfrastructureModule
{
    private const string SqlServerConnection = "SqlServerConnection";
    
    //TODO consider breaking it down into smaller methods
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlConnection  = configuration.GetConnectionString("SqlServerConnection");

        Guard.Against.Null(sqlConnection, message: $"Connection string '{SqlServerConnection}' not found.");
        
        services.AddDbContext<AppDbContext>((_, options) =>
        {
            options.UseSqlServer(sqlConnection);
        });

        //TODO consider what about that
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        
        services
            .AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = DataSchemaConstants.DefaultPasswordMinLength;
        });
        
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = new List<CultureInfo> { new("en") };
            options.SupportedUICultures = new List<CultureInfo> { new("en") };
        });
        
        services.AddScoped<AppDbContextInitializer>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
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