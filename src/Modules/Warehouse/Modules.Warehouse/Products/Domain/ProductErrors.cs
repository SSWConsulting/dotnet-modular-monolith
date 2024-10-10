using ErrorOr;

namespace Modules.Warehouse.Products.Domain;

public static class ProductErrors
{
    public static readonly Error CantRemoveMoreStockThanExists = Error.Validation(
        "Product.CantRemoveMoreStockThanExists",
        "Can't remove more stock than the warehouse has on hand");
}