using Microsoft.EntityFrameworkCore;
using Modules.Warehouse.Domain.Categories;
using Modules.Warehouse.Domain.Products;

namespace Modules.Warehouse.Application.Common.Interfaces;

public interface IWarehouseDbContext
{
    public DbSet<Category> Categories { get; }
    
    public DbSet<Product> Products { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
