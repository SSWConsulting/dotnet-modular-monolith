using Modules.Warehouse.Common.Persistence;
using Modules.Warehouse.Messages;
using Modules.Warehouse.Products.Domain;
using Modules.Warehouse.Storage.Domain;

namespace Modules.Warehouse.Storage.Events;

internal class ProductStoredEventHandler : INotificationHandler<ProductStoredEvent>
{
    private readonly IPublisher _publisher;
    private readonly WarehouseDbContext _dbContext;

    public ProductStoredEventHandler(IPublisher publisher, WarehouseDbContext dbContext)
    {
        _publisher = publisher;
        _dbContext = dbContext;
    }

    public async Task Handle(ProductStoredEvent notification, CancellationToken cancellationToken)
    {
        var product = _dbContext.Products
            .WithSpecification(new ProductByIdSpec(notification.ProductId))
            .First();

        var integrationEvent = new ProductStoredIntegrationEvent(product.Id.Value, product.Name, product.Sku.Value);
        await _publisher.Publish(integrationEvent, cancellationToken);
    }
}
