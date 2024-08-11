namespace Modules.Catalog.Categories.Domain;

internal interface ICategoryRepository
{
    bool CategoryExists(string categoryName);
}
