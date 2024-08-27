using Common.SharedKernel.Domain.Base;
using Modules.Warehouse.Products.Domain;

namespace Modules.Warehouse.Storage.Domain;

internal record ShelfId(Guid Value) : IStronglyTypedId<Guid>;

internal class Shelf : Entity<ShelfId>
{
    public string Name { get; private set; } = null!;

    public ProductId? ProductId { get; private set; }

    public bool IsEmpty => ProductId is null;

    public static Shelf Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new Shelf
        {
            Id = new ShelfId(Guid.NewGuid()),
            Name = name
        };
    }

    public void AssignProduct(ProductId productId)
    {
        ArgumentNullException.ThrowIfNull(productId);
        ProductId = productId;
    }
}
