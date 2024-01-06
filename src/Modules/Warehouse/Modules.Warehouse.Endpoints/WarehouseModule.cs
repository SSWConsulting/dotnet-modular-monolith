﻿using Modules.Warehouse.Application;
using Modules.Warehouse.Infrastructure;
using Modules.Warehouse.Infrastructure.Persistence;

namespace Modules.Warehouse.Endpoints;

public static class WarehouseModule
{
    public static void AddWarehouseServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);
    }

    public static async Task UseWarehouseModule(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            // Initialise and seed database
            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<WarehouseDbContextInitializer>();
            await initializer.InitializeAsync();
            await initializer.SeedAsync();
        }

        app.MapProductEndpoints();
    }
}
