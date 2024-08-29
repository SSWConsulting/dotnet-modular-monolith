using ErrorOr;

namespace Modules.Catalog.Products.Domain;

public static class ProductErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Product.NotFound",
        "Product with the specified ID does not exist.");
}
