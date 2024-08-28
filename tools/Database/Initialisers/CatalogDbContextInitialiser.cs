using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Catalog.Common.Persistence;
using Modules.Warehouse.Products.Domain;
using ProductId = Modules.Catalog.Products.ProductId;

namespace Database.Initialisers;

internal class CatalogDbContextInitialiser
{
    private readonly ILogger<CatalogDbContextInitialiser> _logger;
    private readonly CatalogDbContext _dbContext;

    // public constructor needed for DI
    public CatalogDbContextInitialiser(ILogger<CatalogDbContextInitialiser> logger, CatalogDbContext dbContext)
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

    internal async Task SeedAsync(IEnumerable<Product> warehouseProducts)
    {
        await SeedProductsAsync(warehouseProducts);
    }

    private async Task SeedProductsAsync(IEnumerable<Product> warehouseProducts)
    {
        if (await _dbContext.Products.AnyAsync())
            return;

        // Usually integration events would propagate products to the catalog
        // However, to simplify test data seed, we'll manually pass products into the catalog
        foreach (var warehouseProduct in warehouseProducts)
        {
            var catalogProduct = Modules.Catalog.Products.Product.Create(
                warehouseProduct.Name,
                warehouseProduct.Sku.Value,
                new ProductId(warehouseProduct.Id.Value));

            _dbContext.Products.Add(catalogProduct);
        }

        await _dbContext.SaveChangesAsync();
    }
}
