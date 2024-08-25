using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Warehouse.Products.Endpoints;
using Modules.Warehouse.Storage.UseCases;

namespace Modules.Warehouse;

public static class WarehouseModule
{
    public static void AddWarehouse(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(WarehouseModule).Assembly;

        services.AddValidatorsFromAssembly(applicationAssembly);

        // Todo: Move to feature DI
        // services.AddTransient<IProductRepository, ProductRepository>();
    }

    public static void UseWarehouse(this WebApplication app)
    {
        CreateAisleCommand.Endpoint.MapEndpoint(app);

        // TODO: Move to feature DI
        app.MapProductEndpoints();
    }
}
