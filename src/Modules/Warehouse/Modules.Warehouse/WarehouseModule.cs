using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Warehouse.Common.Persistence;
using Modules.Warehouse.Products.Endpoints;
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
        CreateAisleCommand.Endpoint.MapEndpoint(app);
        app.MapProductEndpoints();
    }
}
