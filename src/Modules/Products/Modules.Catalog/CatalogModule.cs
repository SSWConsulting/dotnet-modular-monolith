using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Catalog;

public static class CatalogModule
{
    public static void AddCatalog(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(CatalogModule).Assembly;

        services.AddHttpContextAccessor();

        services.AddValidatorsFromAssembly(applicationAssembly);

        // services.AddPersistence(configuration);
    }

    public static void UseCatalog(this WebApplication app)
    {
        // app.UseInfrastructureMiddleware();

        // // TODO: Consider source generation or reflection for endpoint mapping
        // CreateAisleCommand.Endpoint.MapEndpoint(app);
        // CreateProductCommand.Endpoint.MapEndpoint(app);
        // AllocateStorageCommand.Endpoint.MapEndpoint(app);
        // GetItemLocationQuery.Endpoint.MapEndpoint(app);
    }
}
