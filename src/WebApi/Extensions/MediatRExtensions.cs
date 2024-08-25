using Common.SharedKernel.Behaviours;
using Modules.Warehouse;
using System.Reflection;

namespace WebApi.Extensions;

public static class MediatRExtensions
{
    private static readonly Assembly[] _assemblies =
    [
        typeof(WarehouseModule).Assembly
    ];

    public static void AddMediatR(this IServiceCollection services)
    {
        // Common MediatR behaviors across all modules
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(_assemblies);
            config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
        });
    }
}
