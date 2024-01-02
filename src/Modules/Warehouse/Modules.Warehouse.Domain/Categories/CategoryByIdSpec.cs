using Ardalis.Specification;

namespace Modules.Warehouse.Domain.Categories;

public class CategoryByIdSpec : Specification<Category>, ISingleResultSpecification<Category>
{
    public CategoryByIdSpec(CategoryId id) : base()
    {
        Query.Where(i => i.Id == id);
    }
}
