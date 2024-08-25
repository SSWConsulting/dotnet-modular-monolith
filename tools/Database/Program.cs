using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Warehouse;
using Modules.Warehouse.Common.Persistence;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.AddSingleton(TimeProvider.System);

    services.AddDbContext<WarehouseDbContext>(options =>
    {
        options.UseSqlServer(context.Configuration.GetConnectionString("Warehouse"), opt =>
        {
            opt.MigrationsAssembly(typeof(WarehouseModule).Assembly.FullName);
        });
    });

    services.AddScoped<WarehouseDbContextInitializer>();
});

var app = builder.Build();
app.Start();

// Initialise and seed database
using var scope = app.Services.CreateScope();
var initializer = scope.ServiceProvider.GetRequiredService<WarehouseDbContextInitializer>();
await initializer.InitializeAsync();
await initializer.SeedAsync();
