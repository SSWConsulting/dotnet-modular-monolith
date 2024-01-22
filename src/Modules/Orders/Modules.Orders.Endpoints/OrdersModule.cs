using Common.Endpoints;
using Common.Endpoints.Interfaces;

namespace Modules.Orders.Endpoints;

public static class OrdersModule //: IModule
{
    public static void AddOrdersServices(this IServiceCollection services, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }

    public static void UseOrdersModule(this WebApplication app)
    {
        app.MapOrderEndpoints();
    }
}

record OrderDto(string Name, string Description);
