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
                    Name: $"Order {index}",
                    Description: $"Order {index} description"
                ));

                return orders;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();
    }
}

record OrderDto(string Name, string Description);