using Ardalis.Specification;

namespace Modules.Catalog.Products.Domain;

internal class ProductByIdSpec : Specification<Product>
{
    public ProductByIdSpec(ProductId id)
    {
        Query
            .Include(p => p.Categories)
            .Where(p => p.Id == id);
    }
}