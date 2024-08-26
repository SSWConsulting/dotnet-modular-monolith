using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Warehouse.Common.Persistence;
using Modules.Warehouse.Products.UseCases;
using Modules.Warehouse.Storage.UseCases;

namespace Modules.Warehouse;

public static class WarehouseModule
{
    public static void AddWarehouse(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(WarehouseModule).Assembly;

        services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddPersistence(configuration);
    }

    public static void UseWarehouse(this WebApplication app)
    {
        // TODO: Consider source generation or reflection for endpoint mapping
        CreateAisleCommand.Endpoint.MapEndpoint(app);
        CreateProductCommand.Endpoint.MapEndpoint(app);
        AllocateStorageCommand.Endpoint.MapEndpoint(app);
    }
}
