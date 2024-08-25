using Common.SharedKernel.Domain.Base;

namespace Modules.Catalog.Categories.Domain;

internal class Category : AggregateRoot<CategoryId>
{
    /// <summary>
    /// Name should be unique
    /// </summary>
    public string Name { get; private set; } = default!;

    private Category() { }

    // NOTE: Need to use a factory, as EF does not let owned entities (i.e Money & Sku) be passed via the constructor
    public static Category Create(string name)
    {
        var category = new Category
        {
            Id = new CategoryId(Guid.NewGuid()),
        };

        category.UpdateName(name);
        category.AddDomainEvent(new CategoryCreatedEvent(category.Id, category.Name));

        return category;
    }

    private void UpdateName(string name)
    {
        // name.ThrowIfNull();
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
    }
}
