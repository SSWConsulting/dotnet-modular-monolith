using Ardalis.Specification;

namespace Modules.Warehouse.Products.Domain;

internal class ProductByIdSpec : Specification<Product>, ISingleResultSpecification<Product>
{
    public ProductByIdSpec(ProductId id) : base()
    {
        Query.Where(i => i.Id == id);
    }
}
