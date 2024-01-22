namespace Modules.Warehouse.Domain.Categories;

// TODO: Might be able to remove this
public interface ICategoryRepository
{
    bool CategoryExists(string categoryName);
}
