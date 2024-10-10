using Ardalis.Specification;

namespace Modules.Warehouse.Products.Domain;

internal class ProductByIdSpec : Specification<Product>
{
    public ProductByIdSpec(ProductId id) : base()
    {
        Query.Where(i => i.Id == id);
    }
}
