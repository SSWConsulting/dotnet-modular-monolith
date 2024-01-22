using Common.Endpoints.Interfaces;

namespace Modules.Orders.Endpoints;

public static class OrdersEndpoints //: IMapEndpoints
{
    // public static void MapCategoryEndpoints(this WebApplication app)
    // {
    //
    // }

    public static void MapOrderEndpoints(this WebApplication app)
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
