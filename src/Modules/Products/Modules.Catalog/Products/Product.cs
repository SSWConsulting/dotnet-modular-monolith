using Common.SharedKernel.Domain.Base;
using Common.SharedKernel.Domain.Entities;
using Modules.Catalog.Categories.Domain;

namespace Modules.Catalog.Products;

internal record ProductId(Guid Value);

internal class Product : AggregateRoot<ProductId>
{
    private string _name = string.Empty;

    private string _sku = string.Empty;

    private Money _price = Money.Default;

    private List<Category> _categories = [];
}
