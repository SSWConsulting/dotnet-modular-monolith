using Ardalis.Specification;

namespace Modules.Catelog.Categories.Domain;

internal class CategoryByIdSpec : Specification<Category>, ISingleResultSpecification<Category>
{
    public CategoryByIdSpec(CategoryId id) : base()
    {
        Query.Where(i => i.Id == id);
    }
}
