using Modules.Warehouse.Application.Common.Interfaces;
using Modules.Warehouse.Domain.Categories;

namespace Modules.Warehouse.Application.Categories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IWarehouseDbContext _dbContext;

    public CategoryRepository(IWarehouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool CategoryExists(string categoryName)
    {
        return _dbContext.Categories.Any(c => c.Name == categoryName);
    }
}
