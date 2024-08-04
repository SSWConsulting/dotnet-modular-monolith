using Common.SharedKernel.Domain.Base;

namespace Modules.Warehouse.Products.Domain;

internal record ProductCreatedEvent(ProductId Product, string ProductName) : DomainEvent
{
    public static ProductCreatedEvent Create(Product product) => new(product.Id, product.Name);
}
