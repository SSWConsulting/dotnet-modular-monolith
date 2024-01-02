namespace Modules.Warehouse.Domain.Categories;

public interface ICategoryService
{
    bool CategoryExists(string categoryName);
}
