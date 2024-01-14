using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Warehouse.Application.Common.Behaviours;
using Modules.Warehouse.Application.Products;
using Modules.Warehouse.Domain.Products;

namespace Modules.Warehouse.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(DependencyInjection).Assembly;

        services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(applicationAssembly);
            config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
        });

        services.AddTransient<IProductRepository, ProductRepository>();

        return services;
    }
}
