namespace Modules.Orders.Endpoints;

public static class OrdersModule
{
    public static void AddOrdersServices(this IServiceCollection services)
    {
    }

    public static void UseOrdersModule(this WebApplication app)
    {
        app.MapGet("/api/orders", () =>
            {
                var orders = Enumerable.Range(1, 5).Select(index => new OrderDto
                (
                    $"Order {index}",
                    $"Order {index} description"
                ));

                return orders;
            })
            .WithName("GetOrders")
            .WithTags("Orders")
            .WithOpenApi();
    }
}

record OrderDto(string Name, string Description);