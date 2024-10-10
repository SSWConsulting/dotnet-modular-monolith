using Common.SharedKernel.Domain.Interfaces;

namespace Modules.Warehouse.Products.Domain;

internal record ProductCreatedEvent(ProductId Product, string ProductName) : IDomainEvent
{
    public static ProductCreatedEvent Create(Product product) => new(product.Id, product.Name);
}
