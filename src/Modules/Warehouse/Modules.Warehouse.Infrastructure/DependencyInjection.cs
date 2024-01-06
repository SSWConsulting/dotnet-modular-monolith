using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Warehouse.Application.Common.Interfaces;
using Modules.Warehouse.Infrastructure.Persistence;

namespace Modules.Warehouse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<IWarehouseDbContext, WarehouseDbContext>(options =>
            options.UseSqlServer(connectionString, builder =>
            {
                builder.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName);
                builder.EnableRetryOnFailure();
            }));

        //services.AddSingleton<IDateTime, DateTimeService>();
        services.AddScoped<WarehouseDbContextInitializer>();
        // services.AddScoped<EntitySaveChangesInterceptor>();
        // services.AddScoped<DispatchDomainEventsInterceptor>();
        // services.AddScoped<OutboxInterceptor>();

        return services;
    }
}
