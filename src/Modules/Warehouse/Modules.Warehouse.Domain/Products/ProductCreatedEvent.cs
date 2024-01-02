using SharedKernel.Domain.Base;
using SharedKernel.Domain.Identifiers;

namespace Modules.Warehouse.Domain.Products;

public record ProductCreatedEvent(ProductId Product, string ProductName) : DomainEvent
{
    public static ProductCreatedEvent Create(Product product) => new(product.Id, product.Name);
}