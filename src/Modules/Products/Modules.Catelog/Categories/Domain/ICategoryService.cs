namespace Modules.Catelog.Categories.Domain;

internal interface ICategoryRepository
{
    bool CategoryExists(string categoryName);
}
