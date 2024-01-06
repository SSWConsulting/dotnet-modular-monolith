namespace Modules.Warehouse.Domain.Categories;

public interface ICategoryRepository
{
    bool CategoryExists(string categoryName);
}
