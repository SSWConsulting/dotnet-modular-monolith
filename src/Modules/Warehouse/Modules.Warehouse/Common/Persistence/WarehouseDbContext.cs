using Microsoft.EntityFrameworkCore;
using Modules.Warehouse.Products.Domain;
using Modules.Warehouse.Storage.Domain;

namespace Modules.Warehouse.Common.Persistence;

internal class WarehouseDbContext : DbContext
{
    // private readonly EntitySaveChangesInterceptor _saveChangesInterceptor;
    // private readonly OutboxInterceptor _outboxInterceptor;

    internal DbSet<Aisle> Aisles => Set<Aisle>();

    internal DbSet<Product> Products => Set<Product>();

    // Needs to be public for the Database project
    public WarehouseDbContext(DbContextOptions options /*EntitySaveChangesInterceptor saveChangesInterceptor, OutboxInterceptor outboxInterceptor*/) : base(options)
    {
        // _saveChangesInterceptor = saveChangesInterceptor;
        // _outboxInterceptor = outboxInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("warehouse");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.AddInterceptors(
        //     _saveChangesInterceptor,
        //     _outboxInterceptor);
    }

    // public Task<int> SaveChangesAsync() => this
    // {
    //     throw new NotImplementedException();
    // }
}
