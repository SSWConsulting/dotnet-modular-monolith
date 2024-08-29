using ErrorOr;

namespace Modules.Catalog.Categories.Domain;

public static class CategoryErrors
{
    public static readonly Error DuplicateName = Error.Validation(
        "Category.DuplicateName",
        "Can't create categories with duplicate names");
}
