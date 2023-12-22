using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace server.UseCases;

public static class UseCasesModule
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}