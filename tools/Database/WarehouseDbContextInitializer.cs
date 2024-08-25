using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Warehouse.Common.Persistence;
using Modules.Warehouse.Products.Domain;
using Modules.Warehouse.Storage.Domain;

namespace Database;

internal class WarehouseDbContextInitializer
{
    private readonly ILogger<WarehouseDbContextInitializer> _logger;
    private readonly WarehouseDbContext _dbContext;

    private const int NumProducts = 20;
    private const int NumAisles = 10;
    private const int NumShelves = 5;
    private const int NumBays = 20;

    internal WarehouseDbContextInitializer(ILogger<WarehouseDbContextInitializer> logger, WarehouseDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    internal async Task InitializeAsync()
    {
        try
        {
            if (_dbContext.Database.IsSqlServer())
            {
                // TODO: Move to migrations
                await _dbContext.Database.EnsureDeletedAsync();
                await _dbContext.Database.EnsureCreatedAsync();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while migrating or initializing the database");
            throw;
        }
    }

    internal async Task SeedAsync()
    {
        await SeedAisles();
        await SeedProductsAsync();
    }

    private async Task SeedAisles()
    {
        if (await _dbContext.Aisles.AnyAsync())
            return;



        for (int i = 1; i <= NumAisles; i++)
        {
            var aisle = Aisle.Create($"Aisle {i}", NumBays, NumShelves);
            _dbContext.Aisles.Add(aisle);
        }

        await _dbContext.SaveChangesAsync();
    }

    private async Task SeedProductsAsync()
    {
        if (await _dbContext.Products.AnyAsync())
            return;

        // var moneyFaker = new Faker<Money>()
            // .CustomInstantiator(f => new Money(f.PickRandom(Currency.Currencies), f.Finance.Amount()));

        var skuFaker = new Faker<Sku>()
            .CustomInstantiator(f => Sku.Create(f.Commerce.Ean8())!);

        var faker = new Faker<Product>()
            .CustomInstantiator(f => Product.Create(f.Commerce.ProductName(), skuFaker.Generate()));

        var products = faker.Generate(NumProducts);
        _dbContext.Products.AddRange(products);
        await _dbContext.SaveChangesAsync();
    }
}
