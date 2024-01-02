namespace Modules.Inventory.Endpoints;

public static class InventoryModule
{
    public static void AddInventoryServices(this IServiceCollection services)
    {
    }

    public static void UseInventoryModule(this WebApplication app)
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
            .WithTags("Inventory")
            .WithOpenApi();
    }
}

record ProductDto(string Name, string Description, decimal Price, int Quantity);