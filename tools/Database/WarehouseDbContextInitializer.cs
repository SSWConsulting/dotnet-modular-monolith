using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Warehouse.Common.Persistence;
using Modules.Warehouse.Storage.Domain;

namespace Database;

internal class WarehouseDbContextInitializer
{
    private readonly ILogger<WarehouseDbContextInitializer> _logger;
    private readonly WarehouseDbContext _dbContext;

    // private const int NumProducts = 20;
    // private const int NumCategories = 5;
    // private const int NumCustomers = 20;
    // private const int NumOrders = 20;

    private const int NumAisles = 10;
    private const int NumShelves = 5;
    private const int NumBays = 20;

    public WarehouseDbContextInitializer(ILogger<WarehouseDbContextInitializer> logger, WarehouseDbContext dbContext)
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

    public async Task SeedAsync()
    {
        await SeedAisles();
        // await SeedCategoriesAsync();
        // await SeedProductsAsync();
        // await SeedCustomersAsync();
        // await SeedOrdersAsync();
    }

    private async Task SeedAisles()
    {
        if (await _dbContext.Aisles.AnyAsync())
            return;

        var faker = new Faker<Aisle>()
            .CustomInstantiator(f => Aisle.Create($"Aisle {Guid.NewGuid()}", NumBays, NumShelves));

        var aisles = faker.Generate(NumAisles);
        _dbContext.Aisles.AddRange(aisles);
        await _dbContext.SaveChangesAsync();
    }

    // private async Task SeedCustomersAsync()
    // {
    //     if (await _dbContext.Customers.AnyAsync())
    //         return;
    //
    //     var customerFaker = new Faker<Customer>()
    //         .CustomInstantiator(f => Customer.Create(f.Person.Email, f.Person.FirstName, f.Person.LastName));
    //
    //     var customers = customerFaker.Generate(NumCustomers);
    //     _dbContext.Customers.AddRange(customers);
    //     await _dbContext.SaveChangesAsync();
    // }

    // private async Task SeedProductsAsync()
    // {
    //     if (await _dbContext.Products.AnyAsync())
    //         return;
    //
    //     var categories = await _dbContext.Categories.ToListAsync();
    //
    //     var moneyFaker = new Faker<Money>()
    //         .CustomInstantiator(f => new Money(f.PickRandom(Currency.Currencies), f.Finance.Amount()));
    //
    //     var skuFaker = new Faker<Sku>()
    //         .CustomInstantiator(f => Sku.Create(f.Commerce.Ean8())!);
    //
    //     var productRepository = new ProductRepository(_dbContext);
    //
    //     var faker = new Faker<Product>()
    //         .CustomInstantiator(f => Product.Create(f.Commerce.ProductName(), moneyFaker.Generate(),
    //             skuFaker.Generate(), f.PickRandom(categories).Id, productRepository));
    //
    //     var products = faker.Generate(NumProducts);
    //     _dbContext.Products.AddRange(products);
    //     await _dbContext.SaveChangesAsync();
    // }

    // private async Task SeedOrdersAsync()
    // {
    //     if (await _dbContext.Orders.AnyAsync())
    //         return;
    //
    //     var customerIds = _dbContext.Customers.Select(c => c.Id).ToList();
    //
    //     var orderFaker = new Faker<Order>()
    //         .CustomInstantiator(f => Order.Create(f.PickRandom(customerIds)));
    //
    //     var orders = orderFaker.Generate(NumOrders);
    //     _dbContext.Orders.AddRange(orders);
    //     await _dbContext.SaveChangesAsync();
    // }

    // private async Task SeedCategoriesAsync()
    // {
    //     if (await _dbContext.Categories.AnyAsync())
    //         return;
    //
    //     var categoryFaker = new Faker<Category>()
    //         .CustomInstantiator(f => Category.Create(f.Commerce.Categories(1)[0], new CategoryRepository(_dbContext)));
    //
    //     var categories = categoryFaker.Generate(NumCategories);
    //     _dbContext.Categories.AddRange(categories);
    //     await _dbContext.SaveChangesAsync();
    // }
}
