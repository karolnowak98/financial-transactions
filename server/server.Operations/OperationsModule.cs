using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace server.Operations;

public static class OperationsModule
{
    public static void AddOperationsServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}