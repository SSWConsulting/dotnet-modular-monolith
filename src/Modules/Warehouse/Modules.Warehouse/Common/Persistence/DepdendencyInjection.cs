using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Warehouse.Common.Persistence.Interceptors;

namespace Modules.Warehouse.Common.Persistence;

internal static class DepdendencyInjection
{
    internal static void AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Warehouse");
        services.AddDbContext<WarehouseDbContext>(options =>
        {
            options.UseSqlServer(connectionString, builder =>
            {
                builder.MigrationsAssembly(typeof(WarehouseModule).Assembly.FullName);
                builder.EnableRetryOnFailure();
            });

            options.AddInterceptors(services.BuildServiceProvider().GetRequiredService<EntitySaveChangesInterceptor>());
        });


        //services.AddSingleton<IDateTime, DateTimeService>();
        // TODO: Consider moving to up.ps1
        // services.AddScoped<WarehouseDbContextInitializer>();
        services.AddScoped<EntitySaveChangesInterceptor>();
        // services.AddScoped<DispatchDomainEventsInterceptor>();
        // services.AddScoped<OutboxInterceptor>();
    }
}
