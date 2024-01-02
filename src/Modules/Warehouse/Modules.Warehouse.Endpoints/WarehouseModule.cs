namespace Modules.Warehouse.Endpoints;

public static class WarhouseModule
{
    public static void AddWarehouseServices(this IServiceCollection services)
    {
    }

    public static void UseWarehouseModule(this WebApplication app)
    {
        app.MapGet("/api/products", () =>
            {
                var products = Enumerable.Range(1, 5).Select(index => new ProductDto
                (
                    $"Product {index}",
                    $"Product {index} description",
                    index * 10.0m,
                    index * 10
                ));

                return products;
            })
            .WithName("GetProducts")
            .WithTags("Warehouse")
            .WithOpenApi();
    }
}

record ProductDto(string Name, string Description, decimal Price, int Quantity);